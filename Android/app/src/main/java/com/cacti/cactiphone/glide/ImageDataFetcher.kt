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

        val buffer = ByteArrayOutputStream()
        flow = fileService.load(photo.path)

        GlobalScope.launch(Dispatchers.IO) {
            flow?.collect {
                buffer.write(it.data, 0, it.size)
            }

            callback.onDataReady(ByteBuffer.wrap(buffer.toByteArray()))
        }

//        val imageDataCallback: Callback<ProfileImage> = object : Callback<ProfileImage> {
//            override fun onResponse(call: Call<ProfileImage>, response: Response<ProfileImage>) {
//                try {
//                    val data: ByteArray = Base64.decode(response.body()!!.data, Base64.DEFAULT)
//                    val byteBuffer: ByteBuffer = ByteBuffer.wrap(data)
//                    callback.onDataReady(byteBuffer)
//                } catch (ex: Exception) {
//                    ex.printStackTrace()
//                }
//
//            }
//
//            override fun onFailure(call: Call<ProfileImage>, t: Throwable) {
//                //callback.onDataReady("")
//            }
//
//        }
//        ApiManager.getProfilePhoto(imageDataCallback, PrefsHelper.read(AppConstants.TOKEN)!!, picId)

    }

    override fun cleanup() {
        // TODO: close InputStream
        // inputStream?.close()
        // flow?.cancellable()
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