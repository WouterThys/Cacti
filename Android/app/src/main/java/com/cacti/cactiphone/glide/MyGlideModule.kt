package com.cacti.cactiphone.glide

import android.content.Context
import android.os.Environment
import android.os.Looper
import androidx.swiperefreshlayout.widget.CircularProgressDrawable
import com.bumptech.glide.Glide
import com.bumptech.glide.GlideBuilder
import com.bumptech.glide.Registry
import com.bumptech.glide.annotation.GlideModule
import com.bumptech.glide.load.engine.cache.ExternalPreferredCacheDiskCacheFactory
import com.bumptech.glide.load.model.ModelLoader
import com.bumptech.glide.load.model.ModelLoaderFactory
import com.bumptech.glide.load.model.MultiModelLoaderFactory
import com.bumptech.glide.module.AppGlideModule
import com.bumptech.glide.request.RequestOptions
import com.cacti.cactiphone.R
import com.cacti.cactiphone.data.Photo
import com.cacti.cactiphone.repository.web.FileService
import dagger.hilt.EntryPoint
import dagger.hilt.InstallIn
import dagger.hilt.android.EntryPointAccessors
import dagger.hilt.components.SingletonComponent
import okhttp3.OkHttpClient
import java.io.InputStream
import java.nio.ByteBuffer

@GlideModule
class MyGlideModule : AppGlideModule() {

    @EntryPoint
    @InstallIn(SingletonComponent::class)
    internal interface MyAppGlideModuleEntryPoint {
        fun provideFileService(): FileService
    }


    override fun registerComponents(context: Context, glide: Glide, registry: Registry) {
        super.registerComponents(context, glide, registry)

        val appContext = context.applicationContext
        val entryPoint = EntryPointAccessors.fromApplication(
            appContext,
            MyAppGlideModuleEntryPoint::class.java
        )

        registry.prepend(Photo::class.java, ByteBuffer::class.java, PhotoModelLoaderFactory(entryPoint.provideFileService()))
    }

    override fun applyOptions(context: Context, builder: GlideBuilder) {
        if (Looper.myLooper() == Looper.getMainLooper()) {

            // Progress circular
            val progressDrawable = CircularProgressDrawable(context)
            progressDrawable.strokeWidth = 5f
            progressDrawable.centerRadius = 30f
            //progressDrawable.setColorSchemeColors(context.getColor(R.color.colorAccent))
            progressDrawable.start()

            // Some options and icons
            val options: RequestOptions = RequestOptions()
                .placeholder(progressDrawable)
                .error(android.R.color.transparent)
                .fallback(R.drawable.cactus_icon_128)
            builder.setDefaultRequestOptions(options)
        }
        super.applyOptions(context, builder)
    }

    class PhotoModelLoaderFactory(private val fileService: FileService) : ModelLoaderFactory<Photo, ByteBuffer> {
        override fun build(multiFactory: MultiModelLoaderFactory): ModelLoader<Photo, ByteBuffer> {
            return ImageModelLoader(fileService)
        }

        override fun teardown() {

        }

    }

}