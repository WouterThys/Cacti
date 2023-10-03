package com.cacti.cactiphone

import android.app.Application
import android.net.Uri
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object AppDiModule {

    @Provides
    fun provideApplication(application: Application): App = application as App

    @Singleton
    @Provides
    fun provideGrpcUri(app: App/*, settings: AppSettings*/) : Uri {
        //return Uri.parse("http://${settings.getHostFromSettings()}/")
        return Uri.parse("http://192.168.1.63:5002/")
    }

}