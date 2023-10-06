package com.cacti.cactiphone.repository.web

import com.cacti.cactiphone.repository.database.CactusDao
import com.cacti.cactiphone.repository.database.PhotoDao
import com.cacti.services.generated.CallbacksGrpcKt
import com.cacti.services.generated.SubscribeRequest
import com.cacti.services.generated.UpdateAction
import com.cacti.services.generated.UpdateMessage
import io.grpc.StatusException
import kotlinx.coroutines.flow.collectLatest
import javax.inject.Inject

class CallbackService @Inject constructor(
    private val service: CallbacksGrpcKt.CallbacksCoroutineStub,
    private val cactusDao: CactusDao,
    private val photoDao: PhotoDao,
) {
    suspend fun register() {
        val request = SubscribeRequest.newBuilder().build()
        try {
            val flow = service.subscribe(request)
            flow.collectLatest {
                when (it.dataCase) {
                    UpdateMessage.DataCase.CACTUS -> {
                        if (it.action == UpdateAction.Insert || it.action == UpdateAction.Update) {
                            cactusDao.save(listOf(CactusService.map(it.cactus)))
                        } else {
                            cactusDao.delete(listOf( it.cactus.id))
                        }
                    }
                    UpdateMessage.DataCase.PHOTO -> {
                        if (it.action == UpdateAction.Insert || it.action == UpdateAction.Update) {
                            photoDao.save(listOf(PhotoService.map(it.photo)))
                        } else {
                            photoDao.delete(listOf( it.photo.id))
                        }
                    }
                    else -> { /* Do nothing.. */ }
                }
            }
        } catch (se: StatusException) {
            se.printStackTrace()
        }

    }
}