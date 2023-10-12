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
import kotlinx.coroutines.launch
import java.io.ByteArrayOutputStream
import java.io.File
import java.io.FileInputStream
import java.nio.ByteBuffer

class ImageDataFetcher(private val fileService: FileService, val photo: Photo) : DataFetcher<ByteBuffer> {

    private var buffer: ByteBuffer? = null
    private var flow : Flow<FileData>? = null

    override fun loadData(priority: Priority, callback: DataFetcher.DataCallback<in ByteBuffer>) {

        if (photo.path.startsWith("/storage")) {
            val file = File(photo.path)
            if (file.exists()) {
                val inStream = FileInputStream(file)
                val buffer = ByteArray(DEFAULT_BUFFER_SIZE)
                val outStream = ByteArrayOutputStream(DEFAULT_BUFFER_SIZE)
                var read: Int
                while (true) {
                    read = inStream.read(buffer)
                    if (read == -1) break
                    outStream.write(buffer, 0, read)
                }
                callback.onDataReady(ByteBuffer.wrap(outStream.toByteArray()))
                return
            }
        }


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