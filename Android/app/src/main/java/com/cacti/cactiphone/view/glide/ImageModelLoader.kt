package com.cacti.cactiphone.view.glide

import com.bumptech.glide.load.Options
import com.bumptech.glide.load.model.ModelLoader
import com.bumptech.glide.signature.ObjectKey
import java.nio.ByteBuffer

class ImageModelLoader : ModelLoader<String, ByteBuffer> {
    override fun buildLoadData(model: String, width: Int, height: Int, options: Options): ModelLoader.LoadData<ByteBuffer> {
        val key = "code:${model};width:$width;height:$height"
        return ModelLoader.LoadData(ObjectKey(key), ImageDataFetcher(model))
    }

    override fun handles(model: String): Boolean {
        if(model.startsWith("/data") || model.startsWith("/storage")){
            return false
        }
        return true
    }
}