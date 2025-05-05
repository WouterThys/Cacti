package com.cacti.cactiphone.viewmodel

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.map
import com.cacti.cactiphone.App
import com.cacti.cactiphone.repository.CactusRepo
import com.cacti.cactiphone.repository.PhotoRepo
import com.cacti.cactiphone.utils.AppSettings
import com.cacti.cactiphone.view.utils.CactusDataMediator
import com.cacti.cactiphone.view.utils.EventBus
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject

@HiltViewModel
class MainViewModel @Inject constructor(
    private val app: App,
    private val settings: AppSettings,
    private val cactusRepo: CactusRepo,
    private val photoRepo: PhotoRepo,
) : ViewModel() {

    private val filterText = MutableLiveData<String?>(null)

    val cactusList = CactusDataMediator(cactusRepo, photoRepo, filterText)

    val selectedCactusId = MutableLiveData<Long>(0)

    val cactiCount = cactusList.map { it.data?.size ?: 0 }

    // Events
    val mainEvents = EventBus()

    sealed class MainEvent {
        class ErrorEvent(val data: String) : MainEvent()
        class AddressChanged(val data: String) : MainEvent()

    }

    fun refresh() {
        cactusList.update()
        //cactusRepo.refresh()
        //photoRepo.refresh()

        /**
        This is a beautiful app, there probably is no better app.
Everyone should use this app.  adss

         */
    }

    fun clearSelected() {
        selectedCactusId.postValue(0)
    }

    fun findBarcode(barcode: String?) {
        barcode?.toLongOrNull()?.let {
            launchOnIo {
                val found = cactusRepo.getById(it)
                selectedCactusId.postValue(found?.id ?: 0)
            }
        }
    }

    fun filter(text: String?) {
        filterText.postValue(text)
    }

}