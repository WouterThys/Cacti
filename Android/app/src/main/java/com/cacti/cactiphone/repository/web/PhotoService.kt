package com.cacti.cactiphone.repository.web

import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.Photo
import com.cacti.cactiphone.repository.data.Resource
import com.cacti.services.generated.CactusesGrpcKt
import com.cacti.services.generated.DeleteCactusRequest
import com.cacti.services.generated.DeletePhotoRequest
import com.cacti.services.generated.GetAllCactusRequest
import com.cacti.services.generated.GetAllPhotoRequest
import com.cacti.services.generated.PhotosGrpcKt
import com.cacti.services.generated.UpdateCactusRequest
import com.cacti.services.generated.UpdatePhotoRequest
import java.util.Date
import javax.inject.Inject

class PhotoService @Inject constructor(
    private val service: PhotosGrpcKt.PhotosCoroutineStub
) {

    suspend fun getAll(ids: List<Long>? = null) : Resource<List<Photo>> {
        return try {
            val requestBuilder = GetAllPhotoRequest.newBuilder()
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

    suspend fun save(photo: Photo) : Resource<Photo> {
        return try {
            val requestBuilder = UpdatePhotoRequest.newBuilder()
            requestBuilder.data = map(photo)
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
            val requestBuilder = DeletePhotoRequest.newBuilder()
            requestBuilder.id = id
            val request = requestBuilder.build()
            val reply = service.delete(request)
            Resource.success(reply.id)
        } catch (ex: Exception) {
            Resource.error(ex.message ?: "Delete error")
        }
    }

    companion object {

        fun map(grpc: com.cacti.generated.Photo) =
            Photo(
                id = grpc.id,
                code = grpc.code,
                path = grpc.path,
                lastModified = Date(grpc.lastModified.seconds)
            )

        fun map(grpc: Photo) : com.cacti.generated.Photo {
            val photo = com.cacti.generated.Photo.newBuilder()
            photo.id = grpc.id
            photo.code = grpc.code
            photo.path = grpc.path
            //photo.lastModified = Date(grpc.lastModified.seconds)

            return photo.build()
        }

    }

}