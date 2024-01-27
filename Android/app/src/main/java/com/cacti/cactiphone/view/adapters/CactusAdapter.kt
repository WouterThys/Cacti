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
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.CactusWithPhoto
import com.cacti.cactiphone.data.PendingCactus
import com.cacti.cactiphone.databinding.LayoutItemCactusBinding
import java.io.File

class CactusAdapter(context: Context) : RecyclerView.Adapter<CactusAdapter.CactusHolder>() {

    private var emptyIconDrawable: Drawable

    init {
        emptyIconDrawable = ContextCompat.getDrawable(context, R.drawable.cactus_icon_128)!!
    }

    private val asyncListDiffer by lazy { AsyncListDiffer(this,
        object : DiffUtil.ItemCallback<CactusWithPhoto>() {
            override fun areItemsTheSame(oldItem: CactusWithPhoto, newItem: CactusWithPhoto): Boolean {
                return oldItem.cactus.id == newItem.cactus.id
            }

            override fun areContentsTheSame(oldItem: CactusWithPhoto, newItem: CactusWithPhoto): Boolean {
                var res = oldItem.cactus.code == newItem.cactus.code &&
                        oldItem.cactus.description == newItem.cactus.description &&
                        oldItem.cactus.location == newItem.cactus.location &&
                        oldItem.cactus.photoId == newItem.cactus.photoId &&
                        oldItem.photo?.id == newItem.photo?.id &&
                        oldItem.photo?.path == newItem.photo?.path

                if (res && (oldItem.cactus is PendingCactus) && (newItem.cactus is PendingCactus)) {
                    res = oldItem.cactus.pendingAction == newItem.cactus.pendingAction
                }

                return res
            }
        }
    )}

    fun submit(data: List<CactusWithPhoto>?) {
        data?.let {
            asyncListDiffer.submitList(ArrayList(data))
        }
    }

    private fun getItem(position: Int): CactusWithPhoto? {
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
        val binding: LayoutItemCactusBinding = DataBindingUtil.inflate(
            LayoutInflater.from(parent.context),
            R.layout.layout_item_cactus,
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
            if (cactus?.cactus != null && cactus.cactus is PendingCactus) {
                when (cactus.cactus.pendingAction) {
                    PendingCactus.ACTION_INSERT -> {
                        it.vPendingAction.background =
                            ContextCompat.getDrawable(it.cardView.context, R.color.green)
                    }
                    PendingCactus.ACTION_UPDATE -> {
                        it.vPendingAction.background = ContextCompat.getDrawable(it.cardView.context, R.color.blue)
                    }
                    else -> {
                        it.vPendingAction.background = ContextCompat.getDrawable(it.cardView.context, R.color.red)
                    }
                }
                it.vPendingAction.visibility = View.VISIBLE
            } else {
                it.vPendingAction.visibility = View.INVISIBLE
            }

            // Photo
            cactus?.photo?.let { photo ->
                if (photo.id > 0) {
                    Glide.with(holder.itemView)
                        .load(photo)
                        .into(it.ivPhoto)
                } else {
                    if (photo.path.isNotEmpty()) {
                        Glide.with(holder.itemView)
                            .load(File(photo.path))
                            .into(it.ivPhoto)
                    } else {
                        // Do nothing
                    }
                }
            } ?: kotlin.run {
                // Remove image
                it.ivPhoto.setImageDrawable(emptyIconDrawable)
            }
        }
    }

    class CactusHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        var binding: LayoutItemCactusBinding? = DataBindingUtil.bind(itemView)
    }
}