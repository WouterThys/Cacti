package com.cacti.cactiphone.viewmodel

import androidx.lifecycle.MediatorLiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import com.cacti.cactiphone.App
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.CactusWithPhoto
import com.cacti.cactiphone.data.Photo
import com.cacti.cactiphone.repository.CactusRepo
import com.cacti.cactiphone.repository.PhotoRepo
import com.cacti.cactiphone.repository.data.Resource
import com.cacti.cactiphone.repository.web.CallbackService
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject

@HiltViewModel
class MainViewModel @Inject constructor(
    private val app: App,
    private val callbackService: CallbackService,
    private val cactusRepo: CactusRepo,
    private val photoRepo: PhotoRepo,
) : ViewModel() {

    val cactusList = MediatorLiveData<Resource<List<CactusWithPhoto>>>().apply {

        var cactusResource: Resource<List<Cactus>>? = null
        var photoResource: Resource<List<Photo>>? = null

        fun update() {
            if (cactusResource?.status == Resource.Status.LOADING) {
                this.postValue(Resource.loading(this.value?.data))
                return
            }
            if (photoResource?.status == Resource.Status.LOADING) {
                this.postValue(Resource.loading(this.value?.data))
                return
            }
            if (cactusResource?.status == Resource.Status.ERROR) {
                this.postValue(Resource.error(this.value?.message ?: ""))
                return
            }
            if (photoResource?.status == Resource.Status.ERROR) {
                this.postValue(Resource.error(this.value?.message ?: ""))
                return
            }

            cactusResource?.data?.let { cacti ->
                photoResource?.data?.let { photos ->

                    val result = ArrayList<CactusWithPhoto>()

                    for (cactus in cacti) {
                        // Don't show unknown
                        if (cactus.id <= 1) continue

                        // Check photo
                        var photo: Photo? = null
                        if (cactus.photoId > 1) {
                            photo = photos.first { p -> p.id == cactus.photoId }
                        }

                        // Add to list
                        result.add(CactusWithPhoto(cactus, photo))
                    }

                    this.postValue(Resource.success(result))
                }
            }
        }

        addSource(cactusRepo.data) {
            cactusResource = it
            update()
        }
        addSource(photoRepo.data) {
            photoResource = it
            update()
        }
    }

    val selectedCactusId = MutableLiveData<Long>(0)

    suspend fun refresh() {
        cactusRepo.refresh()
        photoRepo.refresh()
    }

    fun findBarcode(barcode: String?) {
        barcode?.toLongOrNull()?.let {
            launchOnIo {
                val found = cactusRepo.getByIdAsync(it)
                selectedCactusId.postValue(found?.id ?: 0)
            }
        }
    }

    init {
        launchOnIo {
            callbackService.register()
        }
    }
}