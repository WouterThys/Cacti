package com.cacti.cactiphone.view.glide

import com.bumptech.glide.Priority
import com.bumptech.glide.load.DataSource
import com.bumptech.glide.load.data.DataFetcher
import io.grpc.internal.Stream
import java.nio.ByteBuffer

class ImageDataFetcher(var picId: String) : DataFetcher<Stream> {
    override fun loadData(priority: Priority, callback: DataFetcher.DataCallback<in Stream>) {


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
        //
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