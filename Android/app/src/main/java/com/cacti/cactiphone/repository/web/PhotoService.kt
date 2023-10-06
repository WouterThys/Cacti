package com.cacti.cactiphone.repository.web

import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.Photo
import com.cacti.cactiphone.repository.data.Resource
import com.cacti.services.generated.CactusesGrpcKt
import com.cacti.services.generated.GetAllCactusRequest
import com.cacti.services.generated.GetAllPhotoRequest
import com.cacti.services.generated.PhotosGrpcKt
import java.util.Date
import javax.inject.Inject

class PhotoService @Inject constructor(
    private val service: PhotosGrpcKt.PhotosCoroutineStub
) {

    suspend fun getAll(ids: List<Long>? = null) : Resource<List<Photo>> {
        val requestBuilder = GetAllPhotoRequest.newBuilder()
        ids?.let {
            requestBuilder.addAllIds(ids)
        }
        val request = requestBuilder.build()
        val reply = service.getAll(request)

        val result = reply.dataList.map { grpc -> Photo(
            id = grpc.id,
            code = grpc.code,
            path = grpc.path,
            lastModified = Date(grpc.lastModified.seconds)
        ) }
        return Resource.success(result)
    }

}