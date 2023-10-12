package com.cacti.cactiphone.repository

import androidx.lifecycle.LiveData
import androidx.lifecycle.liveData
import androidx.lifecycle.map
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.Photo
import com.cacti.cactiphone.repository.data.Resource
import com.cacti.cactiphone.repository.database.CactusDao
import com.cacti.cactiphone.repository.database.PhotoDao
import com.cacti.cactiphone.repository.web.CactusService
import com.cacti.cactiphone.repository.web.PhotoService
import kotlinx.coroutines.Dispatchers
import javax.inject.Inject

class PhotoRepo @Inject constructor(
    private val webSource: PhotoService,
    private val dbSource: PhotoDao,
) {

    val data: LiveData<Resource<List<Photo>>> = liveData(Dispatchers.IO) {
        // Set loading
        emit(Resource.loading())

        // Load from database
        val source = dbSource.getAll().map{
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
                println("REPO: ${getRepoName()} SV - Loaded ${res.data?.size} form")
            }
            Resource.Status.ERROR -> {
                emit(Resource.error(res.message!!))
                emitSource(source) // Emit database source again
                System.err.println(res.message)
            }
            else -> {}
        }
    }

    private fun getRepoName() : String {
        return this::class.java.simpleName
    }

    fun getById(id: Long) = dbSource.getById(id)

    suspend fun refresh(): Resource<List<Photo>> {
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

    suspend fun save(photo: Photo) : Resource<Photo> {
        // Update source
        val saved = webSource.save(photo)
        if (saved.status == Resource.Status.SUCCESS) {
            saved.data?.let {
                it.needsSave = false
                dbSource.save(listOf(it))
            }
        }
        return saved
    }

    suspend fun delete(id: Long) {
        // Update source
        val deleted = webSource.delete(id)
        if (deleted.status == Resource.Status.SUCCESS) {
            deleted.data?.let { dbSource.delete(listOf(it)) }
        }
    }

    suspend fun getByIdAsync(id: Long) = dbSource.getByIdAsync(id)
}