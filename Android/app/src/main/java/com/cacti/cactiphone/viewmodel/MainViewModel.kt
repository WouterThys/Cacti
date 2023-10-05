package com.cacti.cactiphone.viewmodel

import androidx.lifecycle.ViewModel
import com.cacti.cactiphone.App
import com.cacti.cactiphone.repository.CactusRepo
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject

@HiltViewModel
class MainViewModel @Inject constructor(
    private val app: App,
    private val cactusRepo: CactusRepo, // TODO: for test, replace with repo
) : ViewModel() {

    val cactusList = cactusRepo.data


    suspend fun refresh() {
        cactusRepo.refresh()
    }
}