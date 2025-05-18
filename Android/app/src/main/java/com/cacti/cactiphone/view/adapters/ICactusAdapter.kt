package com.cacti.cactiphone.view.adapters

import androidx.recyclerview.widget.RecyclerView
import com.cacti.cactiphone.data.CactusWithPhotos

interface ICactusAdapter {

    fun submit(data: List<CactusWithPhotos>?)

    fun getItemId(position: Int): Long

    fun getAdapter() : RecyclerView.Adapter<*>
}