package com.cacti.cactiphone.repository.web

import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.repository.data.Resource
import com.cacti.services.generated.CactusesGrpcKt
import com.cacti.services.generated.GetAllCactusRequest
import java.util.Date
import javax.inject.Inject

class CactusService @Inject constructor(
    private val service: CactusesGrpcKt.CactusesCoroutineStub
) {

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
            Resource.error(ex.message ?: "Error!")
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
                lastModified = Date(grpc.lastModified.seconds)
            )

    }

}