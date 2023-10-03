package com.cacti.cactiphone.viewmodel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.Job
import kotlinx.coroutines.joinAll
import kotlinx.coroutines.launch

fun ViewModel.launchOnIo(func: suspend () -> Unit) =
    viewModelScope.launch(Dispatchers.IO) {
        func()
    }

fun ViewModel.launchOnDefault(func: suspend () -> Unit) =
    viewModelScope.launch(Dispatchers.Default) {
        func()
    }

fun ViewModel.launchOnMain(func: suspend () -> Unit) =
    viewModelScope.launch(Dispatchers.Main) {
        func()
    }

suspend fun ViewModel.awaitAll(vararg jobs: Job) {
    jobs.asList().joinAll()
}