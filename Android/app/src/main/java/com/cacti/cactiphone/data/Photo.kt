package com.cacti.cactiphone.data

import androidx.room.Entity
import androidx.room.PrimaryKey
import java.util.Date

@Entity(tableName = "photos")
data class Photo (
    @PrimaryKey
    val id: Long = 0,
    val code: String = "",
    val path: String = "",
    val lastModified: Date = Date(0),
)