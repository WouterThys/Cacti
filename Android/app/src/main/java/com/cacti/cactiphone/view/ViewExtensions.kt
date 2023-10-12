package com.cacti.cactiphone.view

import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

fun Fragment.launchOnIo(func: suspend () -> Unit) =
    viewLifecycleOwner.lifecycleScope.launch(Dispatchers.IO) {
        func()
    }


fun Fragment.launchOnMain(func: suspend () -> Unit) =
    viewLifecycleOwner.lifecycleScope.launch(Dispatchers.Main) {
        func()
    }

fun Fragment.showToast(message: String) =
    launchOnMain {
        Toast.makeText(context, message, Toast.LENGTH_LONG).show()
    }

