package com.cacti.cactiphone.data

import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity(tableName = "pending")
data class PendingSaveAction(
    @PrimaryKey (autoGenerate = true)
    val id: Long = 0,
    val action: Int = 0,
    val cactusId: Long = 0,
    val photoId: Long = 0,
) {
    companion object {

        const val ACTION_INSERT = 1
        const val ACTION_UPDATE = 2
        const val ACTION_DELETE = 3

    }

}