package com.cacti.cactiphone.viewmodel

import android.net.Uri
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.SavedStateHandle
import androidx.lifecycle.ViewModel
import androidx.lifecycle.map
import androidx.lifecycle.switchMap
import com.cacti.cactiphone.App
import com.cacti.cactiphone.AppConstants
import com.cacti.cactiphone.AppConstants.UNKNOWN_ID
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.PendingSaveAction
import com.cacti.cactiphone.data.Photo
import com.cacti.cactiphone.repository.CactusRepo
import com.cacti.cactiphone.repository.PendingRepo
import com.cacti.cactiphone.repository.PhotoRepo
import com.cacti.cactiphone.repository.data.Resource
import com.cacti.cactiphone.repository.web.FileService
import dagger.hilt.android.lifecycle.HiltViewModel
import java.io.File
import javax.inject.Inject

@HiltViewModel
class EditCactusViewModel@Inject constructor(
    private val app: App,
    private val cactusRepo: CactusRepo,
    private val photoRepo: PhotoRepo,
    private val fileService: FileService,
    private val pendingRepo: PendingRepo,
    private val stateHandle: SavedStateHandle,
) : ViewModel() {

    private val cactusId = MutableLiveData(stateHandle.get<Long>(AppConstants.KEY_CACTUS_ID) ?: UNKNOWN_ID)
    val cactus = cactusId.switchMap {
        if (it < UNKNOWN_ID) {
            MutableLiveData(Cactus())
        } else {
            cactusRepo.getById(it)
        }
    }

    val photo = cactus.switchMap { it?.let { cactus ->
        if (cactus.photoId > UNKNOWN_ID) {
            photoRepo.getById(cactus.photoId)
        } else {
            null
        }
    } }

    suspend fun save(cactus: Cactus?, photoFile: File?) {
        val test = cactusRepo.isConnected()
        if (test.isSuccess()) {
            val savedPhoto = save(photoFile)
            cactus?.photoId = savedPhoto?.id ?: cactus?.photoId ?: UNKNOWN_ID
            save(cactus)
        } else {
            // Save to temp db
            pendingRepo.addPending(cactus, photo.value, photoFile, PendingSaveAction.ACTION_UPDATE)
        }
    }

    private suspend fun save(cactus: Cactus?) {
        cactus?.let {
            val res = cactusRepo.save(it)
            if (res.status == Resource.Status.SUCCESS) {
                cactusId.postValue(res.data?.id ?: UNKNOWN_ID)
            }
        }
    }

    private suspend fun save(photoFile: File?) : Photo? {

        photoFile?.let {
            // Save image
            val path = fileService.save(it)
            var res: Resource<Photo?> = Resource.loading()

            // Check if we have photo
            photo.value?.let { photo ->
                // We have photo, update it
                photo.path = path
                res = photoRepo.save(photo)
            } ?: run {
                // New photo, create one
                val photo = Photo(
                    code = it.name,
                    path = path,
                )
                res = photoRepo.save(photo)
            }

            if (res.status == Resource.Status.SUCCESS) {
                return res.data
            }

        }

        return null
    }

    suspend fun delete() {
        val test = cactusRepo.isConnected()
        if (test.isSuccess()) {
            cactusId.value?.let { cactusRepo.delete(it) }
        } else {
            // Save to pending
            cactus.value?.let { cactusToDelete ->
                if (cactusToDelete.id > UNKNOWN_ID) {
                    pendingRepo.addPending(cactusToDelete, null, null, PendingSaveAction.ACTION_DELETE)
                }
            }
        }
    }

    fun dataChanged(
            code: String,
            description: String,
            location: String,
            photoPath: String
    ) : Boolean {
        this.cactus.value?.let {

            photo.value?.let { photo ->
                // We have photo, but path has changed
                if (photo.path != photoPath) {
                    return true
                }
            } ?: kotlin.run {
                // We don't have photo, and now we have
                if (photoPath.isNotEmpty()) {
                    return true
                }
            }


            return it.code != code ||
                    it.description != description ||
                    it.location != location

        } ?: run {
            return false
        }
    }

}