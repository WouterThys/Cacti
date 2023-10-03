package com.cacti.cactiphone.data

import androidx.room.Entity
import androidx.room.PrimaryKey
import java.util.Date

@Entity(tableName = "cacti")
data class Cactus(
    @PrimaryKey
    val id: Long = 0,
    val code: String = "",
    val description: String = "",
    val location: String = "",
    val barcodes: String = "",
    val photoId: Long = 0,
    val lastModified: Date = Date(0),
) {
}