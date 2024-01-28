package com.cacti.cactiphone.data

import androidx.room.Entity
import androidx.room.PrimaryKey
import java.util.Date

abstract class BaseCactus(
    open var id: Long,
    open var androidId: String = "",
    open var code: String = "",
    open var description: String = "",
    open var location: String = "",
    open var barcodes: String = "",
    open var photoId: Long = 0,
    open var lastModified: Date = Date(0)
)