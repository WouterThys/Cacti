package com.cacti.cactiphone.repository.web

import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.FileData
import com.cacti.cactiphone.repository.data.Resource
import com.cacti.services.generated.CactusesGrpcKt
import com.cacti.services.generated.FilesGrpcKt
import com.cacti.services.generated.GetAllCactusRequest
import com.cacti.services.generated.LoadRequest
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map
import java.util.Date
import javax.inject.Inject

class FileService @Inject constructor(
    private val service: FilesGrpcKt.FilesCoroutineStub
) {

    fun load(fileName: String) : Flow<FileData> {

        val requestBuilder = LoadRequest.newBuilder()
        requestBuilder.path = fileName
        val request = requestBuilder.build()

        val replyFlow = service.load(request)

        return replyFlow.map {
            FileData(it.data.size, it.data.data.toByteArray())
        }

    }

    fun save(fileName: String) {

    }

}