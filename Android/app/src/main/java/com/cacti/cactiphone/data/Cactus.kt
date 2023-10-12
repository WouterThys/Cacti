package com.cacti.cactiphone.data

import androidx.room.Entity
import androidx.room.PrimaryKey
import java.util.Date

@Entity(tableName = "cacti")
data class Cactus(
    @PrimaryKey
    val id: Long = 0,
    var code: String = "",
    var description: String = "",
    var location: String = "",
    val barcodes: String = "",
    var photoId: Long = 0,
    var lastModified: Date = Date(0),
)