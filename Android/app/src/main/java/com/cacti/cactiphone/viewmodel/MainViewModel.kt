package com.cacti.cactiphone.viewmodel

import androidx.lifecycle.ViewModel
import com.cacti.cactiphone.App
import com.cacti.cactiphone.repository.web.CactusService
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject

@HiltViewModel
class MainViewModel @Inject constructor(
    val app: App,
    val cactusService: CactusService, // TODO: for test, replace with repo
) : ViewModel() {

    init {
        launchOnIo {
            val all = cactusService.getAll()
            println(all)
        }
    }

}