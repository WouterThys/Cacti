package com.cacti.cactiphone.view.adapters

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.databinding.DataBindingUtil
import androidx.recyclerview.widget.AsyncListDiffer
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.RecyclerView
import com.bumptech.glide.Glide
import com.cacti.cactiphone.R
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.CactusWithPhoto
import com.cacti.cactiphone.databinding.LayoutItemCactusBinding

class CactusAdapter() : RecyclerView.Adapter<CactusAdapter.CactusHolder>() {

    private val asyncListDiffer by lazy { AsyncListDiffer(this,
        object : DiffUtil.ItemCallback<CactusWithPhoto>() {
            override fun areItemsTheSame(oldItem: CactusWithPhoto, newItem: CactusWithPhoto): Boolean {
                return oldItem.cactus.id == newItem.cactus.id
            }

            override fun areContentsTheSame(oldItem: CactusWithPhoto, newItem: CactusWithPhoto): Boolean {
                return oldItem.cactus.code == newItem.cactus.code &&
                        oldItem.cactus.description == newItem.cactus.description &&
                        oldItem.cactus.location == newItem.cactus.location &&
                        oldItem.cactus.photoId == newItem.cactus.photoId
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
            cactus?.photo?.let { photo ->
                Glide.with(holder.itemView)
                    .load(photo)
                    .into(it.ivPhoto)
            } ?: kotlin.run {
                // Remove image
            }
        }
    }

    class CactusHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        var binding: LayoutItemCactusBinding? = DataBindingUtil.bind(itemView)
    }
}