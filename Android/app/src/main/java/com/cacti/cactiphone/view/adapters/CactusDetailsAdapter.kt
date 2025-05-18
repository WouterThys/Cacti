package com.cacti.cactiphone.view.adapters

import android.content.Context
import android.graphics.drawable.Drawable
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.content.ContextCompat
import androidx.databinding.DataBindingUtil
import androidx.recyclerview.widget.AsyncListDiffer
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.RecyclerView
import com.bumptech.glide.Glide
import com.cacti.cactiphone.R
import com.cacti.cactiphone.data.CactusWithPhotos
import com.cacti.cactiphone.databinding.LayoutItemCactusFullBinding
import java.io.File

class CactusDetailsAdapter(context: Context) : ICactusAdapter, RecyclerView.Adapter<CactusDetailsAdapter.CactusHolder>() {

    private val emptyIconDrawable: Drawable =
        ContextCompat.getDrawable(context, R.drawable.cactus_icon_128)!!

    private val asyncListDiffer by lazy {
        AsyncListDiffer(this,
            object : DiffUtil.ItemCallback<CactusWithPhotos>() {
                override fun areItemsTheSame(
                    oldItem: CactusWithPhotos,
                    newItem: CactusWithPhotos
                ): Boolean {
                    return oldItem.cactus.id == newItem.cactus.id
                }

                override fun areContentsTheSame(
                    oldItem: CactusWithPhotos,
                    newItem: CactusWithPhotos
                ): Boolean {
                    var res = oldItem.cactus.code == newItem.cactus.code
                            && oldItem.cactus.description == newItem.cactus.description
                            && oldItem.cactus.location == newItem.cactus.location
                            && oldItem.cactus.photoId == newItem.cactus.photoId
                            && oldItem.photos.size == newItem.photos.size

                    if (res && newItem.photos.isNotEmpty()) {
                        res = oldItem.photos[0].code == newItem.photos[0].code
                    }

                    return res
                }
            }
        )
    }

    override fun getAdapter(): RecyclerView.Adapter<*> {
        return this
    }

    override fun submit(data: List<CactusWithPhotos>?) {
        data?.let {
            asyncListDiffer.submitList(ArrayList(data))
        }
    }

    private fun getItem(position: Int): CactusWithPhotos? {
        return try {
            asyncListDiffer.currentList[position]
        } catch (e: Exception) {
            e.printStackTrace()
            null
        }
    }

    override fun getItemCount(): Int {
        return asyncListDiffer.currentList.size
    }

    override fun getItemId(position: Int): Long {
        val item = getItem(position)
        return item?.cactus?.id ?: 0
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): CactusHolder {
        val binding: LayoutItemCactusFullBinding = DataBindingUtil.inflate(
            LayoutInflater.from(parent.context),
            R.layout.layout_item_cactus_full,
            parent,
            false
        )
        return CactusHolder(binding.root)
    }

    override fun onBindViewHolder(holder: CactusHolder, position: Int) {
        val cactus = getItem(position)
        holder.binding?.let {
            it.cactus = cactus?.cactus
            // Pending
            it.vPendingAction.visibility = View.INVISIBLE

            // Photo
            cactus?.photos?.firstOrNull()?.let { photo ->
                if (photo.path.isNotEmpty()) {
                    Glide.with(holder.itemView)
                        .load(File(photo.path))
                        .into(it.ivPhoto)
                }
            } ?: kotlin.run {
                // Remove image
                it.ivPhoto.setImageDrawable(emptyIconDrawable)
            }
        }
    }

    class CactusHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        var binding: LayoutItemCactusFullBinding? = DataBindingUtil.bind(itemView)
    }
}