package com.cacti.cactiphone.view.adapters

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.bumptech.glide.Glide
import com.cacti.cactiphone.AppConstants.UNKNOWN_ID
import com.cacti.cactiphone.R
import com.cacti.cactiphone.data.Photo
import com.cacti.cactiphone.databinding.LayoutItemPhotoBinding

class PhotoAdapter : RecyclerView.Adapter<PhotoAdapter.ViewPagerViewHolder>() {

    init {
        setHasStableIds(true)
    }

    private val photos = arrayListOf<Photo>()

    fun submit(list: List<Photo>) {
        photos.clear()
        if (list.isEmpty()) {
            photos.add(Photo(id = UNKNOWN_ID))
        } else {
            photos.addAll(list)
        }
        notifyDataSetChanged()
    }

    fun getPhoto(position: Int) : Photo? {
        if (position in 0..<itemCount) {
            return photos[position]
        } else {
            return null
        }
    }

    inner class ViewPagerViewHolder(private val binding: LayoutItemPhotoBinding) :
        RecyclerView.ViewHolder(binding.root) {

        fun setData(photo: Photo) {

            if (photo.id == UNKNOWN_ID) {
                Glide.with(binding.root.context)
                    .load(R.drawable.cactus_icon_128)
                    .into(binding.ivImage)
            } else {
                Glide.with(binding.root.context)
                    .load(photo.path)
                    .into(binding.ivImage)
            }

        }

    }

    override fun getItemCount(): Int = photos.count()

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewPagerViewHolder {

        val binding = LayoutItemPhotoBinding.inflate(
            LayoutInflater.from(parent.context),
            parent,
            false
        )

        return ViewPagerViewHolder(binding)
    }

    override fun onBindViewHolder(holder: ViewPagerViewHolder, position: Int) {

        getPhoto(position)?.let {

            holder.setData(it)

        } ?: run {

        }
    }

}