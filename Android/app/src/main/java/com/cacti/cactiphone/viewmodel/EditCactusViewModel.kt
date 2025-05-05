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
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.Dispatchers
import java.io.File
import java.util.UUID
import javax.inject.Inject

@HiltViewModel
class EditCactusViewModel@Inject constructor(
    private val app: App,
    private val cactusRepo: CactusRepo,
    private val photoRepo: PhotoRepo,
    private val stateHandle: SavedStateHandle,
) : ViewModel() {

    private val cactusId = MutableLiveData(stateHandle.get<Long>(AppConstants.KEY_CACTUS_ID) ?: UNKNOWN_ID)
    private var isInsert: Boolean = false
    private var androidId: String = ""

    val cactus: LiveData<Cactus> = cactusId.switchMap { id -> liveData(Dispatchers.IO) {
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
    } }

    private val _photos = MutableLiveData<List<Photo>>()
    val photos : LiveData<List<Photo>> = _photos

    fun addPhoto(image: File) {
        if (image.exists()) {
            cactus.value?.let {
                photoRepo.add(it, image)
                _photos.postValue(photoRepo.getPhotos(it))
            }
        }
    }

    fun getNewImageFile() : File {

        cactus.value?.let { cactus ->

            if (cactus.code.isNotBlank()) {
                val photoCount = photos.value?.count() ?: 0
                return photoRepo.getNewImagePath(cactus, photoCount)
            } else {
                throw Exception("Cactus code is empty")
            }

        } ?: run {
            throw Exception("Invalid cactus")
        }

    }

    suspend fun save() {
        cactus.value?.let {
            cactusRepo.save(it)
            // TODO: photos
        }
    }

    suspend fun delete() {
        cactus.value?.let {
            cactusRepo.delete(it.id)
            photoRepo.deleteFor(it)
        }
    }



}