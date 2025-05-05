package com.cacti.cactiphone

import android.app.Application
import android.net.Uri
import android.os.Environment
import com.cacti.cactiphone.utils.AppSettings
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import java.io.File
import javax.inject.Named
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object AppDiModule {

    @Provides
    fun provideApplication(application: Application): App = application as App

    @Singleton
    @Provides
    fun provideAppSettings(app: App): AppSettings = AppSettings(app)

    @Singleton
    @Provides
    fun provideGrpcUri(settings: AppSettings) : Uri {

        //return "http://192.168.1.58:5002/"

        var host = settings.getHost()

        if (host.isBlank()) {
            host = "http://8.8.8.8:1234/"
        }

        if (!host.startsWith("http")) {
            host = "http://$host"
        }

        if (!host.endsWith("/")) {
            host = "$host/"
        }

        return Uri.parse(host)
    }

    @Provides
    @Named("ApplicationPhotoDir")
    fun providePublicStorageDirectory() : String {
        val basePath = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DCIM)
        val appPath = File(basePath, "Cacti")
        if (!appPath.mkdirs()) {
            System.err.println("Failed to create path $appPath")
        }

        return appPath.absolutePath
    }

}