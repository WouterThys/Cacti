package com.cacti.cactiphone.view.adapters

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.databinding.DataBindingUtil
import androidx.recyclerview.widget.AsyncListDiffer
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.RecyclerView
import com.cacti.cactiphone.R
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.databinding.LayoutItemCactusBinding

class CactusAdapter() : RecyclerView.Adapter<CactusAdapter.CactusHolder>() {

    private val asyncListDiffer by lazy { AsyncListDiffer(this,
        object : DiffUtil.ItemCallback<Cactus>() {
            override fun areItemsTheSame(oldItem: Cactus, newItem: Cactus): Boolean {
                return oldItem.id == newItem.id
            }

            override fun areContentsTheSame(oldItem: Cactus, newItem: Cactus): Boolean {
                return oldItem.code == newItem.code &&
                        oldItem.description == newItem.description &&
                        oldItem.location == newItem.location &&
                        oldItem.photoId == newItem.photoId
            }
        }
    )}

    fun submit(data: List<Cactus>?) {
        data?.let {
            asyncListDiffer.submitList(ArrayList(data))
        }
    }

    private fun getItem(position: Int): Cactus? {
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
            it.cactus = cactus
        }
    }

    class CactusHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        var binding: LayoutItemCactusBinding? = DataBindingUtil.bind(itemView)
    }
}