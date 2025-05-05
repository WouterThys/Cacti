package com.cacti.cactiphone.view.adapters

import android.content.Context
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import androidx.viewpager.widget.PagerAdapter
import androidx.viewpager.widget.ViewPager
import com.bumptech.glide.Glide
import com.cacti.cactiphone.AppConstants.UNKNOWN_ID
import com.cacti.cactiphone.R
import com.cacti.cactiphone.data.Photo


class PhotoPager(private val context: Context) : PagerAdapter() {

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
        if (position in 0..<count) {
            return photos[position]
        } else {
            return null
        }
    }

    override fun getCount(): Int {
        return photos.count()
    }

    override fun isViewFromObject(view: View, `object`: Any): Boolean {
        return view == (`object` as ImageView)
    }

    override fun instantiateItem(container: ViewGroup, position: Int): Any {
        val imageView = ImageView(context)
        imageView.setPadding(4, 4, 4, 4)
        imageView.scaleType = ImageView.ScaleType.CENTER_INSIDE

        getPhoto(position)?.let {

            if (it.id == UNKNOWN_ID) {
                Glide.with(context)
                    .load(R.drawable.cactus_icon_128)
                    .into(imageView)
            } else {
                Glide.with(context)
                    .load(it.path)
                    .into(imageView)
            }
        }

        (container as ViewPager).addView(imageView, 0)

        return imageView
    }

}