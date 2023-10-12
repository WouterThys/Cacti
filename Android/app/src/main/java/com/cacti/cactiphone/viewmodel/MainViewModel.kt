package com.cacti.cactiphone.viewmodel

import androidx.lifecycle.MediatorLiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.map
import com.cacti.cactiphone.App
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.CactusWithPhoto
import com.cacti.cactiphone.data.Photo
import com.cacti.cactiphone.repository.CactusRepo
import com.cacti.cactiphone.repository.PendingRepo
import com.cacti.cactiphone.repository.PhotoRepo
import com.cacti.cactiphone.repository.data.Resource
import com.cacti.cactiphone.repository.web.CallbackService
import com.cacti.cactiphone.view.utils.CactusDataMediator
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject

@HiltViewModel
class MainViewModel @Inject constructor(
    private val app: App,
    private val callbackService: CallbackService,
    private val cactusRepo: CactusRepo,
    private val photoRepo: PhotoRepo,
    private val pendingRepo: PendingRepo,
) : ViewModel() {

    private val filterText = MutableLiveData<String?>(null)

    val cactusList = CactusDataMediator(cactusRepo.data, photoRepo.data, filterText)

    val selectedCactusId = MutableLiveData<Long>(0)

    val cactiCount = cactusList.map { it.data?.size ?: 0 }


    suspend fun refresh() {

        pendingRepo.clearPendingData()

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

    init {
        launchOnIo {
            callbackService.register()
        }
    }
}