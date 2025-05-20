package com.cacti.cactiphone.viewmodel

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.SavedStateHandle
import androidx.lifecycle.ViewModel
import androidx.lifecycle.liveData
import androidx.lifecycle.switchMap
import com.cacti.cactiphone.App
import com.cacti.cactiphone.AppConstants
import com.cacti.cactiphone.AppConstants.UNKNOWN_ID
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.Photo
import com.cacti.cactiphone.repository.CactusRepo
import com.cacti.cactiphone.repository.PhotoRepo
import com.cacti.cactiphone.utils.QrUtils
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.Dispatchers
import java.io.File
import java.util.UUID
import javax.inject.Inject

@HiltViewModel
class EditCactusViewModel @Inject constructor(
    private val app: App,
    private val cactusRepo: CactusRepo,
    private val photoRepo: PhotoRepo,
    private val stateHandle: SavedStateHandle,
) : ViewModel() {

    private val cactusId =
        MutableLiveData(stateHandle.get<Long>(AppConstants.KEY_CACTUS_ID) ?: UNKNOWN_ID)
    private var isInsert: Boolean = false
    private var androidId: String = ""
    private var currentPhotoIndex = 0

    val cactus: LiveData<Cactus> = cactusId.switchMap { id ->
        liveData(Dispatchers.IO) {
            val result = if (id < UNKNOWN_ID) {
                // Create new cactus
                isInsert = true
                androidId = UUID.randomUUID().toString()
                Cactus(androidId = androidId)
            } else {
                // Load from repo
                isInsert = false
                val found = cactusRepo.getById(id) ?: Cactus()
                androidId = found.androidId
                found
            }

            println("XXX EDIT: id= ${result.id} AndroidId = $androidId -> insert=${isInsert}")

            emit(result)
        }
    }

    private val _photos = MutableLiveData<List<Photo>>()
    val photos: LiveData<List<Photo>> = _photos

    fun getPhotoCount(): Int {
        return _photos.value?.count() ?: 0
    }

    fun setCurrentPhotoIndex(position: Int) {
        currentPhotoIndex = position
    }

    fun getCurrentPhotoIndex(): Int {
        return currentPhotoIndex
    }

    fun refreshPhotos(cactus: Cactus) {
        currentPhotoIndex = 0
        _photos.postValue(photoRepo.getPhotos(cactus))
    }

    fun currentCode() : String {
        return cactus.value?.code ?: ""
    }

    fun validateCode(newCode: String): Boolean {
        var result = false
        cactus.value?.let {
            result = photoRepo.canCreateDir(it, newCode)
        }
        return result
    }

    fun codeChanged(oldCode: String, newCode: String) {
        cactus.value?.let {
            it.code = newCode
            if (oldCode.isNotBlank() && it.code.isNotBlank()) {
                photoRepo.renameDir(it, oldCode)
            }
            _photos.postValue(photoRepo.getPhotos(it))
        }
    }

    fun addPhoto(image: File) {
        if (image.exists()) {
            cactus.value?.let {
                currentPhotoIndex = getPhotoCount()
                photoRepo.add(it, image)
                _photos.postValue(photoRepo.getPhotos(it))
            }
        }
    }

    fun deletePhoto(photo: Photo?) {
        cactus.value?.let { cactus ->
            photo?.let { photo ->
                photoRepo.delete(cactus, photo)
            }
            currentPhotoIndex = 0
            _photos.postValue(photoRepo.getPhotos(cactus))
        }
    }

    fun setCurrentAsFirstPhoto() {
        cactus.value?.let {
            if (currentPhotoIndex > 0) {

                val currentFirst = photos.value?.firstOrNull()
                val newFirst = photos.value?.get(currentPhotoIndex)

                if (currentFirst != null && newFirst != null) {

                    photoRepo.setAsFirst(it, currentFirst, newFirst)
                    currentPhotoIndex = 0

                    _photos.postValue(photoRepo.getPhotos(it))
                }
            }
        }
    }

    fun getNewImageFile(): File {

        cactus.value?.let { cactus ->

            if (cactus.code.isNotBlank()) {

                val photos = this.photos.value ?: emptyList()
                var index = 0
                var proposedPrefix = photoRepo.getPrefix(index)
                var found =
                    photos.firstOrNull { f -> f.code.startsWith(proposedPrefix, ignoreCase = true) }
                while (found != null) {
                    index++
                    proposedPrefix = photoRepo.getPrefix(index)
                    found = photos.firstOrNull { f ->
                        f.code.startsWith(
                            proposedPrefix,
                            ignoreCase = true
                        )
                    }
                }

                return photoRepo.getNewImagePath(cactus, index)
            } else {
                throw Exception("Cactus code is empty")
            }

        } ?: run {
            throw Exception("Invalid cactus")
        }

    }

    private fun saveQrCode(cactus: Cactus) {
        try {
            val bitmap = QrUtils.generateQrCode(cactus.id.toString())
            val dir = photoRepo.getCactusDir(cactus)
            val file = File(dir.absolutePath, cactus.id.toString() + ".jpg")
            if (!file.exists()) {
                QrUtils.writeBitmap(app, bitmap, file.absolutePath)
            }
        } catch (ex: Exception) {
            ex.printStackTrace()
        }
    }

    suspend fun save() {
        cactus.value?.let {
            cactusRepo.save(it)
            saveQrCode(it)
        }
    }

    suspend fun delete() {
        cactus.value?.let {
            cactusRepo.delete(it.id)
            photoRepo.deleteFor(it)
        }
    }


}