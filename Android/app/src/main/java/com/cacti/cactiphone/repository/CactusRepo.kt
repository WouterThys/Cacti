package com.cacti.cactiphone.repository

import androidx.lifecycle.LiveData
import androidx.lifecycle.liveData
import androidx.lifecycle.map
import com.cacti.cactiphone.data.BaseCactus
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.PendingCactus
import com.cacti.cactiphone.data.Photo
import com.cacti.cactiphone.repository.data.Resource
import com.cacti.cactiphone.repository.database.CactusDao
import com.cacti.cactiphone.repository.database.PendingCactusDao
import com.cacti.cactiphone.repository.web.CactusService
import com.cacti.cactiphone.repository.web.FileService
import kotlinx.coroutines.Dispatchers
import java.io.File
import javax.inject.Inject

class CactusRepo @Inject constructor(
    private val webSource: CactusService,
    private val dbSource: CactusDao,
    private val pendingSource: PendingCactusDao,
    private val photoRepo: PhotoRepo,
    private val fileService: FileService,
) {

    val data: LiveData<Resource<List<Cactus>>> = liveData(Dispatchers.IO) {
        // Set loading
        emit(Resource.loading())

        // Load from database
        val source = dbSource.getAll().map {
            println("REPO: ${getRepoName()} SV - Loaded ${it.size} form db")
            Resource.success(it)
        }
        emitSource(source)

        // Since we are using Room here, updating the database will update the `fromDb` LiveData
        // that was obtained above. See Room's documentation for more details.
        // https://developer.android.com/training/data-storage/room/accessing-data#query-observable
        val res = webSource.getAll(null)
        when (res.status) {
            Resource.Status.SUCCESS -> {
                // Save to database
                res.data?.let { dbSource.refreshAll(it) }
            }

            Resource.Status.ERROR -> {
                emit(Resource.error(res.message!!))
                emitSource(source) // Emit database source again
                System.err.println(res.message)
            }

            else -> {}
        }
    }

    val pending: LiveData<List<PendingCactus>> = pendingSource.getAll()

    val pendingCount: LiveData<Int> = pendingSource.count()

    private fun getRepoName(): String {
        return this::class.java.simpleName
    }

    suspend fun isConnected() = webSource.isConnected()

    suspend fun refresh(): Resource<List<BaseCactus>> {
        val responseStatus = webSource.getAll(null)
        when (responseStatus.status) {
            Resource.Status.SUCCESS -> {
                // Save to database
                responseStatus.data?.let { dbSource.refreshAll(it) }
            }

            Resource.Status.ERROR -> {
                System.err.println(responseStatus.message)
            }

            else -> {}
        }
        return responseStatus
    }


    suspend fun insert(cactus: BaseCactus, photoFile: File?): PendingCactus =
        save(cactus, photoFile, PendingCactus.ACTION_INSERT)

    suspend fun update(cactus: BaseCactus, photoFile: File?): PendingCactus =
        save(cactus, photoFile, PendingCactus.ACTION_UPDATE)


    private suspend fun save(cactus: BaseCactus, photoFile: File?, action: Int): PendingCactus {
        // Remove from source db
        dbSource.delete(listOf(cactus.id))

        // Save to pending
        val pendingCactus = PendingCactus.create(cactus)
        pendingCactus.pendingAction = action
        pendingCactus.filePath = photoFile?.path ?: ""
        pendingSource.save(pendingCactus)

        return pendingCactus
    }

    suspend fun delete(cactusId: Long) {

        // Delete from source
        dbSource.delete(listOf(cactusId))

        // Always save to pending
        val pendingCactus = PendingCactus.create(cactusId)
        pendingCactus.pendingAction = PendingCactus.ACTION_DELETE
        pendingSource.save(pendingCactus)
    }

    suspend fun getByIdAsync(id: Long): BaseCactus? {
        var result: BaseCactus?
        result = dbSource.getByIdAsync(id)
        if (result == null) {
            result = pendingSource.getByIdAsync(id)
        }
        return result
    }

    suspend fun trySendPending() {
        val test = isConnected()
        if (test.isSuccess()) {
            // Load 1 from db
            val pendingCactus: PendingCactus? = pendingSource.getOne()
            if (test.isSuccess() && pendingCactus != null) {

                val idToDelete = pendingCactus.id

                when (pendingCactus.pendingAction) {
                    PendingCactus.ACTION_INSERT -> {

                        pendingCactus.id = 0
                        updatePendingCactus(pendingCactus)

                    }
                    PendingCactus.ACTION_UPDATE -> {

                        updatePendingCactus(pendingCactus)

                    }
                    PendingCactus.ACTION_DELETE -> {

                        deletePendingCactus(pendingCactus)

                    }
                }

                // TODO: check if success
                pendingSource.delete(listOf(idToDelete))
            }
        }
    }

    private suspend fun updatePendingCactus(pendingCactus: PendingCactus) {
        // Save photo
        val photoFile = File(pendingCactus.filePath)
        if (photoFile.exists()) {
            // file service ...
            val path = fileService.save(photoFile)
            val photo = Photo(0, photoFile.name, path)
            // photo repo...
            val photoRes = photoRepo.save(photo)
            if (photoRes.isSuccess()) {
                pendingCactus.photoId = photoRes.data?.id ?: 0
            }
        }

        // Save cactus
        val saved = webSource.save(pendingCactus)
        if (saved.status == Resource.Status.SUCCESS) {
            saved.data?.let {
                dbSource.save(listOf(it))
            }
        }
    }

    private suspend fun deletePendingCactus(pendingCactus: PendingCactus) {
        // Delete photo ..

        // Delete cactus
        val deleted = webSource.delete(pendingCactus.id)
        if (deleted.status == Resource.Status.SUCCESS) {
            deleted.data?.let {
                dbSource.delete(listOf(it))
            }
        }
    }

    // TODO: add watcher and timer or do this in vm and watch the pending??
}