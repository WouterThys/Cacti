package com.cacti.cactiphone.glide

import com.bumptech.glide.Priority
import com.bumptech.glide.load.DataSource
import com.bumptech.glide.load.data.DataFetcher
import com.cacti.cactiphone.data.FileData
import com.cacti.cactiphone.data.Photo
import com.cacti.cactiphone.repository.web.FileService
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.cancellable
import kotlinx.coroutines.flow.flowOn
import kotlinx.coroutines.launch
import java.io.ByteArrayOutputStream
import java.io.InputStream
import java.io.OutputStream
import java.nio.ByteBuffer

class ImageDataFetcher(private val fileService: FileService, val photo: Photo) : DataFetcher<ByteBuffer> {

    private var buffer: ByteBuffer? = null
    private var flow : Flow<FileData>? = null

    override fun loadData(priority: Priority, callback: DataFetcher.DataCallback<in ByteBuffer>) {

        GlobalScope.launch(Dispatchers.IO) {

            val buffer = ByteArrayOutputStream()

            try {
                flow = fileService.load(photo.path)

                flow?.collect {
                    buffer.write(it.data, 0, it.size)
                }

                callback.onDataReady(ByteBuffer.wrap(buffer.toByteArray()))

            } catch (ex: Exception) {
                callback.onLoadFailed(ex)
            } finally {
                buffer.close()
            }

        }

    }

    override fun cleanup() {

    }

    override fun cancel() {
        //
    }

    override fun getDataClass(): Class<ByteBuffer> {
        return ByteBuffer::class.java
    }

    override fun getDataSource(): DataSource {
        return DataSource.REMOTE
    }

}