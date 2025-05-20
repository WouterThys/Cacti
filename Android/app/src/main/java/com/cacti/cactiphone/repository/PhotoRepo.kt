package com.cacti.cactiphone.repository

import com.cacti.cactiphone.AppConstants.UNKNOWN_ID
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.Photo
import java.io.File
import java.io.IOException
import java.text.SimpleDateFormat
import java.util.Date
import java.util.Locale
import javax.inject.Inject

class PhotoRepo @Inject constructor(private val photoDir: String) {

    private fun getCactusDir(code: String, create: Boolean = true) : File {
        if (code.isBlank()) throw IOException("Cactus code is empty")
        val dir = File(photoDir, code.replace(' ', '_'))
        if (create && !dir.exists()) {
            dir.mkdirs()
        }
        return dir
    }

    fun getCactusDir(cactus: Cactus) : File {
        return getCactusDir(cactus.code)
    }

    fun getPrefix(index: Int) : String {
        return "CACTUS_${index}"
    }

    @Throws(IOException::class)
    fun getNewImagePath(cactus: Cactus, index: Int) : File {
        // Create an image file name
        val loc = Locale.getDefault()
        val timeStamp: String = SimpleDateFormat("yyyyMMdd_HHmmss", loc).format(Date())
        val storageDir: File = getCactusDir(cactus)
        return File(storageDir, "${getPrefix(index)}_${timeStamp}.jpg")
    }

    fun getPhotos(cactus: Cactus) : List<Photo> {

        val result = arrayListOf<Photo>()

        if (cactus.code.isNotBlank()) {
            val dir = getCactusDir(cactus)
            dir.listFiles()?.forEach { file ->
                if (file.name.startsWith("CACTUS_")) {
                    result.add(
                        Photo(
                            id = Date().time,
                            code = file.nameWithoutExtension,
                            path = file.absolutePath,
                            lastModified = Date(file.lastModified())
                        )
                    )
                }
            }

            result.sortBy { f -> f.code }
        }

        return result
    }

    fun add(cactus: Cactus, image: File) {
        // Not needed here but might be important when saving to server..
    }

    fun delete(cactus: Cactus, photo: Photo) {
        if (cactus.id > UNKNOWN_ID && photo.id > UNKNOWN_ID) {
            val imageFile = File(photo.path)
            if (imageFile.exists()) {
                imageFile.delete()
            }
        }
    }

    fun canCreateDir(cactus: Cactus, oldCode: String) : Boolean {
        val dir = getCactusDir(oldCode, false)
        return !dir.exists()
    }

    fun renameDir(cactus: Cactus, oldCode: String) {
        val oldDir = getCactusDir(oldCode)
        val newDir = getCactusDir(cactus)

        if (oldDir.exists()) {

            oldDir.renameTo(newDir)

        }
    }

    fun setAsFirst(cactus: Cactus, currentFirst: Photo, newFirst: Photo) {

        val directory = getCactusDir(cactus)

        val currentFirstPath = currentFirst.path
        var newFirstPath = newFirst.path

        if (!currentFirstPath.contains(directory.path) || !newFirstPath.contains(directory.path)) {
            return
        }

        val regex = Regex("CACTUS_\\d+")

        if (currentFirstPath.contains(getPrefix(0))) {
            // The first one starts with CACTUS_0.. update needed of this one too
            regex.find(newFirstPath)?.let { res ->
                // Something like CACTUS_5
                val currentNewPath = currentFirstPath.replace(getPrefix(0), res.value)
                if (File(currentFirstPath).renameTo(File(currentNewPath))) {
                    currentFirst.path = currentNewPath
                }
            }
        }

        // New 'CACTUS_0'
        newFirstPath = newFirstPath.replace(regex, getPrefix(0))
        if (File(newFirst.path).renameTo(File(newFirstPath))) {
            newFirst.path = newFirstPath
        }
    }

    fun deleteFor(cactus: Cactus) {
        if (cactus.code.isNotBlank()) {
            val dir = getCactusDir(cactus)
            dir.delete()
        }
    }
}