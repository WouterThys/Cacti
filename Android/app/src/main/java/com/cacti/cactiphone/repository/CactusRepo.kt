package com.cacti.cactiphone.repository

import androidx.lifecycle.LiveData
import androidx.lifecycle.liveData
import androidx.lifecycle.map
import com.cacti.cactiphone.AppConstants.UNKNOWN_ID
import com.cacti.cactiphone.data.BaseCactus
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.repository.data.Resource
import com.cacti.cactiphone.repository.database.CactusDao
import kotlinx.coroutines.Dispatchers
import java.util.Date
import javax.inject.Inject

class CactusRepo @Inject constructor(
    private val dbSource: CactusDao,
) {

    @Synchronized
    fun getData() : LiveData<Resource<List<Cactus>>> = liveData(Dispatchers.IO) {

        emit(Resource.loading())
        dbSource.save(Cactus(id = UNKNOWN_ID, code = "Unknown"))

        val all = dbSource.getAll()

        emitSource(all.map { Resource.success(it) })
    }

    private fun getRepoName(): String {
        return this::class.java.simpleName
    }

    suspend fun save(cactus: Cactus): Cactus {

        cactus.lastModified = Date()

        val id = dbSource.save(cactus)
        cactus.id = id

        return cactus
    }

    suspend fun delete(cactusId: Long) {

        // Delete from source
        dbSource.delete(listOf(cactusId))

    }

    suspend fun getById(id: Long): Cactus? {
        val result: BaseCactus?
        result = dbSource.getByIdAsync(id)
        return result
    }

}