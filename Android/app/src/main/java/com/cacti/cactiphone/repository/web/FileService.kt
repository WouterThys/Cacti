package com.cacti.cactiphone.repository.web

import com.cacti.cactiphone.AppConstants.FILE_DATA_SIZE
import com.cacti.cactiphone.data.FileData
import com.cacti.services.generated.FilesGrpcKt
import com.cacti.services.generated.LoadRequest
import com.cacti.services.generated.SaveRequest
import com.google.protobuf.ByteString
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.emitAll
import kotlinx.coroutines.flow.flow
import kotlinx.coroutines.flow.flowOf
import kotlinx.coroutines.flow.flowOn
import kotlinx.coroutines.flow.map
import kotlinx.coroutines.flow.onCompletion
import java.io.File
import java.io.FileInputStream
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

    suspend fun save(file: File) : String {

        // First create path flow
        var requestBuilder = SaveRequest.newBuilder()
        requestBuilder.path = file.name
        val pathFlow = flowOf(requestBuilder.build())

        // Data flow
        val dataFlow = fileAsFileDataFlow(file).map { fileData ->
            requestBuilder = SaveRequest.newBuilder()
            requestBuilder.data = fileData
            requestBuilder.build()
        }

        // Combine flows
        val flow = pathFlow.onCompletion { emitAll(dataFlow) }

        // Go!
        val reply = service.save(flow)

        return reply.path

    }


    private fun fileAsFileDataFlow(file: File) = flow<com.cacti.services.generated.FileData> {
        var inputStream: FileInputStream? = null

        try {

            inputStream = FileInputStream(file)

            val buffer = ByteArray(FILE_DATA_SIZE)
            var rc = inputStream.read(buffer)
            while (rc != -1) {

                val fileDataBuilder = com.cacti.services.generated.FileData.newBuilder()
                fileDataBuilder.size = rc
                fileDataBuilder.data = ByteString.copyFrom(buffer)

                emit(fileDataBuilder.build())

                rc = inputStream.read(buffer)
            }
            inputStream.close()
        } catch (ex: Exception) {
            ex.printStackTrace()
        } finally {
            inputStream?.close()
        }

    }.flowOn(Dispatchers.IO)

}