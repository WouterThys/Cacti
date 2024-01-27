package com.cacti.cactiphone.repository.web

import androidx.room.Update
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.PendingCactus
import com.cacti.cactiphone.repository.data.Resource
import com.cacti.services.generated.CactusesGrpcKt
import com.cacti.services.generated.DeleteCactusRequest
import com.cacti.services.generated.GetAllCactusRequest
import com.cacti.services.generated.TestConnectionRequest
import com.cacti.services.generated.UpdateCactusRequest
import java.util.Date
import javax.inject.Inject

class CactusService @Inject constructor(
    private val service: CactusesGrpcKt.CactusesCoroutineStub
) {

    suspend fun isConnected() : Resource<Boolean> {
        return try {
            val requestBuilder = TestConnectionRequest.newBuilder()
            requestBuilder.test = "Hello there"
            val request = requestBuilder.build()
            val reply = service.testConnection(request)
            Resource.success(reply.test == request.test)
        } catch (ex: Exception) {
            Resource.error(ex.message ?: "Test error!")
        }
    }

    suspend fun getAll(ids: List<Long>? = null) : Resource<List<Cactus>> {
        return try {
            val requestBuilder = GetAllCactusRequest.newBuilder()
            ids?.let {
                requestBuilder.addAllIds(ids)
            }
            val request = requestBuilder.build()
            val reply = service.getAll(request)
            val result = reply.dataList.map { grpc -> map(grpc) }
            Resource.success(result)
        } catch (ex: Exception) {
            Resource.error(ex.message ?: "Load error!")
        }
    }

    suspend fun save(cactus: PendingCactus) : Resource<Cactus> {
        return try {
            val requestBuilder = UpdateCactusRequest.newBuilder()
            requestBuilder.data = map(cactus)
            val request = requestBuilder.build()
            val reply = service.update(request)
            val result = map(reply.data)
            Resource.success(result)
        } catch (ex: Exception) {
            Resource.error(ex.message ?: "Save error")
        }
    }

    suspend fun delete(id: Long) : Resource<Long> {
        return try {
            val requestBuilder = DeleteCactusRequest.newBuilder()
            requestBuilder.id = id
            val request = requestBuilder.build()
            val reply = service.delete(request)
            Resource.success(reply.id)
        } catch (ex: Exception) {
            Resource.error(ex.message ?: "Delete error")
        }
    }


    companion object {

        fun map(grpc: com.cacti.generated.Cactus) : Cactus =
            Cactus(
                id = grpc.id,
                code = grpc.code,
                description = grpc.description,
                location = grpc.location,
                barcodes = grpc.barcodes,
                photoId = grpc.photoId,
                lastModified = Date(grpc.lastModified.seconds),
            )

        fun map(cactus: PendingCactus) : com.cacti.generated.Cactus {
            val grpc = com.cacti.generated.Cactus.newBuilder()
            grpc.id = cactus.id
            grpc.code = cactus.code
            grpc.description = cactus.description
            grpc.location = cactus.location
            grpc.barcodes = cactus.barcodes
            grpc.photoId = cactus.photoId
            //cactus.lastModified = Date(grpc.lastModified.seconds)

            return grpc.build()
        }

    }

}