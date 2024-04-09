package com.cacti.cactiphone.data

import androidx.room.Entity
import androidx.room.PrimaryKey
import com.cacti.cactiphone.repository.database.PendingCactusDao
import java.util.Date

@Entity(tableName = "pending_cacti")
data class PendingCactus(
    @PrimaryKey(autoGenerate = true)
    override var id: Long = 0,
    override var androidId: String = "",
    override var code: String = "",
    override var description: String = "",
    override var location: String = "",
    override var barcodes: String = "",
    override var photoId: Long = 0,
    override var lastModified: Date = Date(0),
    override var fathersCode: String = "",
    override var mothersCode: String = "",
    override var crossingsNumber: String = "",
    var filePath: String = "",
    var pendingAction: Int = 0,
) : BaseCactus(id) {
    companion object {

        const val ACTION_DONE   = 0
        const val ACTION_INSERT = 1
        const val ACTION_UPDATE = 2
        const val ACTION_DELETE = 3

        fun create(cactus: BaseCactus) : PendingCactus {
            val pendingCactus = PendingCactus()

            pendingCactus.id = cactus.id
            pendingCactus.code = cactus.code
            pendingCactus.description = cactus.description
            pendingCactus.androidId = cactus.androidId
            pendingCactus.location = cactus.location
            pendingCactus.barcodes = cactus.barcodes
            pendingCactus.photoId = cactus.photoId
            pendingCactus.lastModified = cactus.lastModified
            pendingCactus.fathersCode = cactus.fathersCode
            pendingCactus.mothersCode = cactus.mothersCode
            pendingCactus.crossingsNumber = cactus.crossingsNumber

            return pendingCactus
        }

        fun create(id: Long) : PendingCactus {
            val pendingCactus = PendingCactus()

            pendingCactus.id = id

            return pendingCactus
        }

    }

}