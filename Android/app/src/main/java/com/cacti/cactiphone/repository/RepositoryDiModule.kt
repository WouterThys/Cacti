package com.cacti.cactiphone.repository

import android.content.Context
import android.net.Uri
import com.cacti.cactiphone.repository.database.CactusDao
import com.cacti.cactiphone.repository.database.MyDatabase
import com.cacti.cactiphone.repository.database.PendingDao
import com.cacti.cactiphone.repository.database.PhotoDao
import com.cacti.cactiphone.repository.web.CactusService
import com.cacti.cactiphone.repository.web.CallbackService
import com.cacti.cactiphone.repository.web.FileService
import com.cacti.cactiphone.repository.web.PhotoService
import com.cacti.generated.CactusKt
import com.cacti.services.generated.CactusesGrpcKt
import com.cacti.services.generated.CallbacksGrpcKt
import com.cacti.services.generated.FilesGrpcKt
import com.cacti.services.generated.PhotosGrpcKt
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
    fun provideDatabase(@ApplicationContext appContext: Context) =
        MyDatabase.getDatabase(appContext)

    @Singleton
    @Provides
    fun provideCactusDao(db: MyDatabase) = db.cactusDao()

    @Singleton
    @Provides
    fun providePhotoDao(db: MyDatabase) = db.photoDao()

    @Singleton
    @Provides
    fun providePendingDao(db: MyDatabase) = db.pendingDao()

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
    fun provideCactusCoroutineStub(channel: ManagedChannel) =
        CactusesGrpcKt.CactusesCoroutineStub(channel)

    @Singleton
    @Provides
    fun provideCactusService(stub: CactusesGrpcKt.CactusesCoroutineStub) =
        CactusService(stub)

    @Singleton
    @Provides
    fun providePhotoCoroutineStub(channel: ManagedChannel) =
        PhotosGrpcKt.PhotosCoroutineStub(channel)

    @Singleton
    @Provides
    fun providePhotoService(stub: PhotosGrpcKt.PhotosCoroutineStub) =
        PhotoService(stub)


    @Singleton
    @Provides
    fun provideFileCoroutineStub(channel: ManagedChannel) =
        FilesGrpcKt.FilesCoroutineStub(channel)

    @Singleton
    @Provides
    fun provideFileService(stub: FilesGrpcKt.FilesCoroutineStub) =
        FileService(stub)


    @Singleton
    @Provides
    fun provideCallbackCoroutineStub(channel: ManagedChannel) =
        CallbacksGrpcKt.CallbacksCoroutineStub(channel)

    @Singleton
    @Provides
    fun provideCallbackService(
        stub: CallbacksGrpcKt.CallbacksCoroutineStub,
        cactusDao: CactusDao,
        photoDao: PhotoDao,
    ) =
        CallbackService(stub, cactusDao, photoDao)

    // endregion

    // region Repo

    @Singleton
    @Provides
    fun provideCactusRepo(remoteSource: CactusService, localSource: CactusDao) =
        CactusRepo(remoteSource, localSource)

    @Singleton
    @Provides
    fun providePhotoRepo(remoteSource: PhotoService, localSource: PhotoDao) =
        PhotoRepo(remoteSource, localSource)

    @Singleton
    @Provides
    fun providePendingRepo(
        cactusRepo: CactusRepo,
        photoRepo: PhotoRepo,
        cactusDao: CactusDao,
        photoDao: PhotoDao,
        pendingDao: PendingDao,
        fileService: FileService,
    ) =
        PendingRepo(cactusRepo, photoRepo, cactusDao, photoDao, pendingDao, fileService)


    // endregion
}