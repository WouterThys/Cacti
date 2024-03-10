package com.cacti.cactiphone.viewmodel

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.map
import com.cacti.cactiphone.App
import com.cacti.cactiphone.utils.AppSettings
import com.cacti.cactiphone.repository.CactusRepo
import com.cacti.cactiphone.repository.PhotoRepo
import com.cacti.cactiphone.repository.web.CallbackService
import com.cacti.cactiphone.view.utils.EventBus
import com.cacti.cactiphone.view.utils.CactusDataMediator
import dagger.hilt.android.lifecycle.HiltViewModel
import io.grpc.internal.SharedResourceHolder.Resource
import javax.inject.Inject

@HiltViewModel
class MainViewModel @Inject constructor(
    private val app: App,
    private val settings: AppSettings,
    private val callbackService: CallbackService,
    private val cactusRepo: CactusRepo,
    private val photoRepo: PhotoRepo,
) : ViewModel() {

    private val filterText = MutableLiveData<String?>(null)

    val cactusList = CactusDataMediator(cactusRepo.data, cactusRepo.pending, photoRepo.data, filterText)

    val selectedCactusId = MutableLiveData<Long>(0)

    val cactiCount = cactusList.map { it.data?.size ?: 0 }

    val pendingCount = cactusRepo.pendingCount

    // Events
    val mainEvents = EventBus()

    sealed class MainEvent {
        class ErrorEvent(val data: String) : MainEvent()
        class AddressChanged(val data: String) : MainEvent()

    }


    fun hasHost() : Boolean {
        return settings.getHost().isNotBlank()
    }

    fun getHost() : String {
        return settings.getHost()
    }

    fun setHost(host: String) {
        settings.setHost(host)

        launchOnIo {
            mainEvents.post(MainEvent.AddressChanged(settings.getHost()))
        }
    }

    fun testHost() {
        launchOnIo {
            val res = cactusRepo.isConnected()

            if (res.status != com.cacti.cactiphone.repository.data.Resource.Status.SUCCESS) {
                mainEvents.post(MainEvent.ErrorEvent("Not connected"))
            }
        }
    }

    suspend fun refresh() {
        cactusRepo.refresh()
        photoRepo.refresh()
    }

    fun clearSelected() {
        selectedCactusId.postValue(0)
    }

    fun findBarcode(barcode: String?) {
        barcode?.toLongOrNull()?.let {
            launchOnIo {
                val found = cactusRepo.getByIdAsync(it)
                selectedCactusId.postValue(found?.id ?: 0)
            }
        }
    }

    fun filter(text: String?) {
        filterText.postValue(text)
    }

    fun newPending(count: Int) {
        if (count > 0) {
            launchOnIo {
                cactusRepo.trySendPending()
            }
        }
    }

    init {
        launchOnIo {
            callbackService.register()
        }
    }
}