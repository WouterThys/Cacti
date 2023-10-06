package com.cacti.cactiphone.view.utils

import android.view.View
import androidx.recyclerview.widget.RecyclerView
import com.cacti.cactiphone.R
import com.cacti.cactiphone.safeLet

class RecyclerClickSupport private constructor(
    private val recyclerView: RecyclerView,
    allowDoubleClick: Boolean
) {
    interface OnSingleClickListener {
        fun onItemClicked(recyclerView: RecyclerView?, position: Int, v: View?)
    }

    interface OnItemClickListener : OnSingleClickListener {
        fun onItemDoubleClicked(recyclerView: RecyclerView?, position: Int, v: View?)
        fun onItemLongClicked(recyclerView: RecyclerView?, position: Int, v: View?)
    }

    private var itemClickListener: OnItemClickListener? = null
    private val doubleClickListener: OnDoubleClickListener = object : OnDoubleClickListener() {
        override fun onDoubleClick(v: View?) {
            safeLet(itemClickListener, v) { icl, view ->
                val holder = recyclerView.getChildViewHolder(view)
                icl.onItemDoubleClicked(recyclerView, holder.bindingAdapterPosition, view)
            }
        }

        override fun onSingleClick(v: View?) {
            safeLet(itemClickListener, v) { icl, view ->
                val holder = recyclerView.getChildViewHolder(view)
                icl.onItemClicked(recyclerView, holder.bindingAdapterPosition, view)
            }
        }

        override fun onLongClick(v: View?): Boolean {
            return safeLet(itemClickListener, v) { icl, view ->
                val holder = recyclerView.getChildViewHolder(view)
                icl.onItemLongClicked(recyclerView, holder.bindingAdapterPosition, view)
                true
            } ?: false
        }
    }
    private val attachStateChangeListener: RecyclerView.OnChildAttachStateChangeListener =
        object : RecyclerView.OnChildAttachStateChangeListener {
            override fun onChildViewAttachedToWindow(view: View) {
                if (itemClickListener != null) {
                    view.setOnClickListener(doubleClickListener)
                    view.setOnLongClickListener(doubleClickListener)
                }
            }

            override fun onChildViewDetachedFromWindow(view: View) {}
        }

    fun setOnItemClickListener(listener: OnSingleClickListener): RecyclerClickSupport {
        return setOnItemClickListener(object : OnItemClickListener {
            override fun onItemDoubleClicked(recyclerView: RecyclerView?, position: Int, v: View?) {
                onItemClicked(recyclerView, position, v)
            }

            override fun onItemLongClicked(recyclerView: RecyclerView?, position: Int, v: View?) {
                onItemClicked(recyclerView, position, v)
            }

            override fun onItemClicked(recyclerView: RecyclerView?, position: Int, v: View?) {
                listener.onItemClicked(recyclerView, position, v)
            }
        })
    }

    fun setOnItemClickListener(listener: OnItemClickListener): RecyclerClickSupport {
        itemClickListener = listener
        return this
    }

    private fun detach(recyclerView: RecyclerView) {
        recyclerView.removeOnChildAttachStateChangeListener(attachStateChangeListener)
        recyclerView.setTag(R.id.item_click_support, null)
    }

    companion object {
        @JvmOverloads
        fun addTo(
            recyclerView: RecyclerView,
            allowDoubleClick: Boolean = false
        ): RecyclerClickSupport {
            var support = recyclerView.getTag(R.id.item_click_support) as RecyclerClickSupport?
            if (support == null) {
                support = RecyclerClickSupport(recyclerView, allowDoubleClick)
            }
            return support
        }
    }

    init {
        doubleClickListener.isAllowDoubleClick = allowDoubleClick
        recyclerView.setTag(R.id.item_click_support, this)
        recyclerView.addOnChildAttachStateChangeListener(attachStateChangeListener)
    }
}