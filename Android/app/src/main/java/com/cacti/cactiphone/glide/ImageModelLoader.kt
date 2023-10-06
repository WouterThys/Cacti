package com.cacti.cactiphone.glide

import com.bumptech.glide.load.Options
import com.bumptech.glide.load.model.ModelLoader
import com.bumptech.glide.signature.ObjectKey
import com.cacti.cactiphone.data.Photo
import com.cacti.cactiphone.repository.web.FileService
import java.io.InputStream
import java.nio.ByteBuffer

class ImageModelLoader(private val fileService: FileService) : ModelLoader<Photo, ByteBuffer> {
    override fun buildLoadData(model: Photo, width: Int, height: Int, options: Options): ModelLoader.LoadData<ByteBuffer> {
        val key = "id:${model.id};path:${model.path};w:$width;h:$height"
        return ModelLoader.LoadData(ObjectKey(key), ImageDataFetcher(fileService, model))
    }

    override fun handles(model: Photo): Boolean {

        if (model.id <= 1
            || model.path.isEmpty()
            || model.path.startsWith("/data")
            || model.path.startsWith("/storage")
            ) {
            return false
        }
        return true
    }
}