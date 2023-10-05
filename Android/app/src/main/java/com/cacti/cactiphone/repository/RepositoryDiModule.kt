package com.cacti.cactiphone.repository

import android.content.Context
import android.net.Uri
import com.cacti.cactiphone.repository.database.CactusDao
import com.cacti.cactiphone.repository.database.MyDatabase
import com.cacti.cactiphone.repository.web.CactusService
import com.cacti.generated.CactusKt
import com.cacti.services.generated.CactusesGrpcKt
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.qualifiers.ApplicationContext
import dagger.hilt.components.SingletonComponent
import io.grpc.ManagedChannel
import io.grpc.android.AndroidChannelBuilder
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.asExecutor
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object RepositoryDiModule {

    // region Database
    @Singleton
    @Provides
    fun provideDatabase(@ApplicationContext appContext: Context)
            = MyDatabase.getDatabase(appContext)

    @Singleton
    @Provides
    fun provideCactusDao(db: MyDatabase) = db.cactusDao()

    // endregion

    // region Web

    @Singleton
    @Provides
    fun provideChannel(uri: Uri): ManagedChannel {

        val builder = AndroidChannelBuilder.forAddress(uri.host, uri.port)
        //if (uri.scheme == "https") {
        //    builder.useTransportSecurity()
        //} else {
        builder.usePlaintext()
        //}
        //  builder.intercept(GrpcClientInterceptor())
        //builder.context(app)
        builder.executor(Dispatchers.IO.asExecutor())

        return builder.build()
    }

    @Singleton
    @Provides
    fun provideUserCoroutineStub(channel: ManagedChannel) =
        CactusesGrpcKt.CactusesCoroutineStub(channel)

    @Singleton
    @Provides
    fun provideUserService(stub: CactusesGrpcKt.CactusesCoroutineStub) =
        CactusService(stub)

    // endregion

    // region Repo

    @Singleton
    @Provides
    fun provideCactusRepo(remoteSource: CactusService, localSource: CactusDao) =
        CactusRepo(remoteSource, localSource)

    // endregion
}