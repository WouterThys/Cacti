package com.cacti.cactiphone.view.utils

import androidx.lifecycle.LiveData
import androidx.lifecycle.MediatorLiveData
import com.cacti.cactiphone.data.BaseCactus
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.CactusWithPhoto
import com.cacti.cactiphone.data.PendingCactus
import com.cacti.cactiphone.data.Photo
import com.cacti.cactiphone.repository.data.Resource
import java.util.Locale

class CactusDataMediator(
    cactusLiveData: LiveData<Resource<List<Cactus>>>,
    pendingLiveData: LiveData<List<PendingCactus>>,
    photoLiveData: LiveData<Resource<List<Photo>>>,
    filterLiveData: LiveData<String?>,
) : MediatorLiveData<Resource<List<CactusWithPhoto>>>() {

    private var cactusResource: Resource<List<Cactus>>? = null
    private var pendingData: List<PendingCactus>? = null
    private var photoResource: Resource<List<Photo>>? = null
    private var filterText: String? = null

    fun forceLoading() {
        postValue(Resource.loading(value?.data))
    }

    private fun checkStatus(resource: Resource<*>?) : Boolean {
        if (resource == null) {
            this.postValue(Resource.loading(this.value?.data))
            return false
        }
        if (resource.status == Resource.Status.LOADING) {
            this.postValue(Resource.loading(this.value?.data))
            return false
        }
        if (resource.status == Resource.Status.ERROR) {
            this.postValue(Resource.error(this.value?.message ?: ""))
            return false
        }

        return true
    }


    private fun hasText(text: String?, filter: String) : Boolean {
        if (!text.isNullOrEmpty() &&
            text.uppercase(Locale.getDefault()).contains(filter)) {

            println("Text $text contains $filter")

            return true
        }
        return false
    }

    private fun includeAfterFilter(cactus: BaseCactus) : Boolean {
        if (filterText.isNullOrEmpty()) return true

        filterText?.let { filter ->

            return hasText(cactus.code, filter) ||
                    hasText(cactus.description, filter) ||
                    hasText(cactus.location, filter) ||
                    hasText(cactus.barcodes, filter) ||
                    hasText(cactus.lastModified.toString(), filter)

        } ?: kotlin.run {
            return false
        }

    }

    private fun update() {
        if (checkStatus(cactusResource) && checkStatus(photoResource)) {
            cactusResource?.data?.let { cacti ->
                photoResource?.data?.let { photos ->
                    pendingData?.let { pending ->

                        val result = ArrayList<CactusWithPhoto>()

                        for (cactus in cacti) {
                            // Don't show unknown
                            if (cactus.id <= 1) continue
                            checkAndAdd(cactus, photos, result)
                        }

                        for (cactus in pending) {
                            checkAndAdd(cactus, photos, result)
                        }

                        this.postValue(Resource.success(result))
                    }
                }
            }
        }
    }

    private fun checkAndAdd(cactus: BaseCactus, photos: List<Photo>, result: ArrayList<CactusWithPhoto>) {
        if (includeAfterFilter(cactus)) {

            // Check photo
            var photo: Photo? = null
            if (cactus.photoId > 1) {
                photo = photos.firstOrNull { p -> p.id == cactus.photoId }
            } else if (cactus is PendingCactus && cactus.filePath.isNotEmpty()) {
                photo = Photo(0, "", cactus.filePath)
            }

            // Add to list
            result.add(CactusWithPhoto(cactus, photo))
        }
    }

    init {
        addSource(cactusLiveData) {
            cactusResource = it
            update()
        }

        addSource(pendingLiveData) {
            pendingData = it
            update()
        }

        addSource(photoLiveData) {
            photoResource = it
            update()
        }

        addSource(filterLiveData) {
            filterText = it?.uppercase()
            update()
        }
    }
}