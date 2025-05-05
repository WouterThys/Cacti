package com.cacti.cactiphone.repository

import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.Photo
import java.io.File
import java.io.IOException
import java.text.SimpleDateFormat
import java.util.Date
import java.util.Locale
import javax.inject.Inject

class PhotoRepo @Inject constructor(private val photoDir: String) {

    // TODO: this one should query the local photo in the directory structure
    //

    private fun getCactusDir(cactus: Cactus) : File {
        if (cactus.code.isBlank()) throw IOException("Cactus code is empty")
        val dir = File(photoDir, cactus.code.replace(' ', '_'))
        if (!dir.exists()) {
            dir.mkdirs()
        }
        return dir
    }

    @Throws(IOException::class)
    fun getNewImagePath(cactus: Cactus, index: Int) : File {
        // Create an image file name
        val loc = Locale.getDefault()
        val timeStamp: String = SimpleDateFormat("yyyyMMdd_HHmmss", loc).format(Date())
        val storageDir: File = getCactusDir(cactus)
        return File.createTempFile(
            "CACTUS_${index}_${timeStamp}", /* prefix */
            ".jpg", /* suffix */
            storageDir /* directory */
        )
    }

    fun getPhotos(cactus: Cactus) : List<Photo> {

        val result = arrayListOf<Photo>()

        if (cactus.code.isNotBlank()) {
            val dir = getCactusDir(cactus)
            dir.listFiles()?.forEach { file ->
                result.add(Photo(
                    id = Date().time,
                    code = file.nameWithoutExtension,
                    path = file.absolutePath,
                    lastModified = Date(file.lastModified())
                ))
            }

            result.sortBy { f -> f.code }
        }

        return result
    }

    fun add(cactus: Cactus, image: File) {
        // Not needed here but might be important when saving to server..
    }

    fun deleteFor(cactus: Cactus) {
        if (cactus.code.isNotBlank()) {
            val dir = getCactusDir(cactus)
            dir.delete()
        }
    }
}