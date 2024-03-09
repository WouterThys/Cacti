package com.cacti.cactiphone.viewmodel

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.map
import com.cacti.cactiphone.App
import com.cacti.cactiphone.AppSettings
import com.cacti.cactiphone.repository.CactusRepo
import com.cacti.cactiphone.repository.PhotoRepo
import com.cacti.cactiphone.repository.web.CallbackService
import com.cacti.cactiphone.view.utils.CactusDataMediator
import dagger.hilt.android.lifecycle.HiltViewModel
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


    fun hasHost() : Boolean {
        return settings.getHost().isNotBlank()
    }

    fun setHost(host: String) {
        settings.setHost(host)
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