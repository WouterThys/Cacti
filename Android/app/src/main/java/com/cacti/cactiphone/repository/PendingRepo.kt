package com.cacti.cactiphone.repository

import com.cacti.cactiphone.AppConstants.UNKNOWN_ID
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.PendingSaveAction
import com.cacti.cactiphone.data.Photo
import com.cacti.cactiphone.repository.database.CactusDao
import com.cacti.cactiphone.repository.database.PendingDao
import com.cacti.cactiphone.repository.database.PhotoDao
import com.cacti.cactiphone.repository.web.CactusService
import com.cacti.cactiphone.repository.web.FileService
import java.io.File
import javax.inject.Inject

class PendingRepo @Inject constructor(
    private val cactusRepo: CactusRepo,
    private val photoRepo: PhotoRepo,

    private val cactusDao: CactusDao,
    private val photoDao: PhotoDao,

    private val pendingDao: PendingDao,
    private val fileService: FileService,
) {

    suspend fun clearPendingData() {

        val test = cactusRepo.isConnected()
        if (test.isSuccess()) {

            val pendingList = pendingDao.getAll()

            for (pending in pendingList) {
                if (pending.cactusId > UNKNOWN_ID) {

                    when (pending.action) {
                        PendingSaveAction.ACTION_INSERT -> {
                            savePending(pending, true)
                        }
                        PendingSaveAction.ACTION_UPDATE -> {
                            savePending(pending, false)
                        }
                        PendingSaveAction.ACTION_DELETE -> {
                            deletePending(pending)
                        }
                    }

                }
            }

            pendingDao.deleteAll()

        }

    }

    private suspend fun savePending(pending: PendingSaveAction, insert: Boolean) {
        val cactus = cactusRepo.getByIdAsync(pending.cactusId)
        var photo: Photo? = null

        if (pending.photoId > UNKNOWN_ID) {
            photo = photoRepo.getByIdAsync(pending.photoId)
        }

        cactus?.let { cactusToSave ->

            cactusDao.delete(listOf(cactusToSave.id))

            var savedPhoto: Photo? = null

            photo?.let { photoToSave ->

                if (photoToSave.path.isNotEmpty()) {
                    val file = File(photoToSave.path)
                    if (file.exists()) {
                        val path = fileService.save(file)
                        photoToSave.path = path
                    }
                }
                photoDao.delete(listOf(photo.id))
                photo.id = 0
                savedPhoto = photoRepo.save(photo).data

            }

            cactusToSave.photoId = savedPhoto?.id ?: cactusToSave.photoId

            if (insert) {
                // Clear id to make sure server will insert
                cactusToSave.id = 0
            }
            cactusRepo.save(cactusToSave)
        }
    }

    private suspend fun deletePending(pending: PendingSaveAction) {
        cactusRepo.delete(pending.cactusId)
    }

    suspend fun addPending(cactus: Cactus?, photo: Photo?, photoFile: File?, action: Int) {
        cactus?.let {

            when (action) {
                PendingSaveAction.ACTION_UPDATE,
                PendingSaveAction.ACTION_INSERT -> {
                    createPendingSaveAction(cactus, photo, photoFile)
                }
                PendingSaveAction.ACTION_DELETE -> {
                    createPendingDeleteAction(cactus)
                }
            }
        }
    }

    private suspend fun createPendingDeleteAction(cactus: Cactus?) {
        cactus?.let {
            // Delete from db
            cactusDao.delete(listOf(it.id))

            // Create pending
            pendingDao.save(PendingSaveAction(
                cactusId = it.id,
                photoId = 0,
                action = PendingSaveAction.ACTION_DELETE
            ))
        }
    }

    private suspend fun createPendingSaveAction(cactus: Cactus, photo: Photo?, photoFile: File?) {

        var photoId = 0L

        // Save photo
        photo?.let {
            // We have photo, update it
            photo.path = photoFile?.path ?: photo.path
            photo.needsSave = true
            photoId = photoDao.save(photo)
        } ?: run {
            // New photo, create one
            photoFile?.let { file ->
                val newPhoto = Photo(
                    code = file.name,
                    path = file.path,
                    needsSave = true,
                )
                photoId = photoDao.save(newPhoto)
            }
        }

        cactus.photoId = photoId
        cactus.needsSave = true

        val action = if (cactus.id > UNKNOWN_ID) PendingSaveAction.ACTION_UPDATE else PendingSaveAction.ACTION_INSERT
        val cactusId = cactusDao.save(cactus)

        pendingDao.save(PendingSaveAction(
            cactusId = cactusId,
            photoId = photoId,
            action = action,
        ))
    }
}