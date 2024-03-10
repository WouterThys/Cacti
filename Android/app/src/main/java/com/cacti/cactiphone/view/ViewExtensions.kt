package com.cacti.cactiphone.view

import android.app.Activity
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.Fragment
import androidx.lifecycle.Lifecycle
import androidx.lifecycle.lifecycleScope
import androidx.lifecycle.repeatOnLifecycle
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

fun AppCompatActivity.runWhenResumed(action: suspend () -> Unit) {
    lifecycleScope.launch {
        // Suspend the coroutine until the lifecycle is DESTROYED.
        // repeatOnLifecycle launches the block in a new coroutine every time the
        // lifecycle is in the RESUMED state (or above) and cancels it when it's STOPPED.
        repeatOnLifecycle(Lifecycle.State.RESUMED) {
            // Safely collect from locations when the lifecycle is RESUMED
            // and stop collecting when the lifecycle is STOPPED
            action()
        }
        // Note: at this point, the lifecycle is DESTROYED!
    }
}

fun Activity.showToast(message: String) =
    Toast.makeText(this, message, Toast.LENGTH_LONG).show()