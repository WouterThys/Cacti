package com.cacti.cactiphone.glide

import android.content.Context
import android.os.Looper
import androidx.swiperefreshlayout.widget.CircularProgressDrawable
import com.bumptech.glide.GlideBuilder
import com.bumptech.glide.annotation.GlideModule
import com.bumptech.glide.module.AppGlideModule
import com.bumptech.glide.request.RequestOptions
import com.cacti.cactiphone.R

@GlideModule
class MyGlideModule : AppGlideModule() {

//    @EntryPoint
//    @InstallIn(SingletonComponent::class)
//    internal interface MyAppGlideModuleEntryPoint {
//        fun provideFileService(): FileService
//    }


//    override fun registerComponents(context: Context, glide: Glide, registry: Registry) {
//        super.registerComponents(context, glide, registry)
//
//        val appContext = context.applicationContext
//        val entryPoint = EntryPointAccessors.fromApplication(
//            appContext,
//            MyAppGlideModuleEntryPoint::class.java
//        )
//
//        registry.prepend(Photo::class.java, ByteBuffer::class.java, PhotoModelLoaderFactory(entryPoint.provideFileService()))
//    }

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

//    class PhotoModelLoaderFactory(private val fileService: FileService) : ModelLoaderFactory<Photo, ByteBuffer> {
//        override fun build(multiFactory: MultiModelLoaderFactory): ModelLoader<Photo, ByteBuffer> {
//            return ImageModelLoader(fileService)
//        }
//
//        override fun teardown() {
//
//        }
//
//    }

}