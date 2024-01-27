package com.cacti.cactiphone.data

import androidx.room.Entity
import androidx.room.PrimaryKey
import java.util.Date

@Entity(tableName = "cacti")
data class Cactus(
    @PrimaryKey (autoGenerate = true)
    override var id: Long = 0,
    override var code: String = "",
    override var description: String = "",
    override var location: String = "",
    override var barcodes: String = "",
    override var photoId: Long = 0,
    override var lastModified: Date = Date(0)
) : BaseCactus(id)