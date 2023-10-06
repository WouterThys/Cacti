package com.cacti.cactiphone.view.utils

import android.os.Handler
import android.os.Looper
import android.view.View
import android.view.ViewConfiguration

abstract class OnDoubleClickListener internal constructor() : View.OnClickListener,
    View.OnLongClickListener {

    private val doubleClickTimeout: Int = ViewConfiguration.getDoubleTapTimeout()
    private val handler: Handler
    private var firstClickTime: Long
    var isAllowDoubleClick = false

    abstract fun onDoubleClick(v: View?)
    abstract fun onSingleClick(v: View?)
    abstract override fun onLongClick(v: View?): Boolean

    override fun onClick(view: View) {
        if (isAllowDoubleClick) {
            val now = System.currentTimeMillis()
            if (now - firstClickTime < doubleClickTimeout) {
                handler.removeCallbacksAndMessages(null)
                firstClickTime = 0L
                onDoubleClick(view)
            } else {
                firstClickTime = now
                handler.postDelayed({
                    onSingleClick(view)
                    firstClickTime = 0L
                }, doubleClickTimeout.toLong())
            }
        } else {
            onSingleClick(view)
        }
    }

    init {
        firstClickTime = 0L
        handler = Handler(Looper.getMainLooper())
    }
}