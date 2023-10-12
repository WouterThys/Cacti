package com.cacti.cactiphone.data

import androidx.room.Entity
import androidx.room.PrimaryKey
import java.util.Date

@Entity(tableName = "photos")
data class Photo (
    @PrimaryKey (autoGenerate = true)
    var id: Long = 0,
    val code: String = "",
    var path: String = "",
    val lastModified: Date = Date(0),
    var needsSave: Boolean = false,
)