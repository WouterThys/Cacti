package com.cacti.cactiphone.view.utils

import androidx.lifecycle.LiveData
import androidx.lifecycle.MediatorLiveData
import com.cacti.cactiphone.data.BaseCactus
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.CactusWithPhotos
import com.cacti.cactiphone.data.Photo
import com.cacti.cactiphone.repository.CactusRepo
import com.cacti.cactiphone.repository.PhotoRepo
import com.cacti.cactiphone.repository.data.Resource
import java.util.Locale

class CactusDataMediator(
    private val cactusRepo: CactusRepo,
    private val photoRepo: PhotoRepo,
    filterLiveData: LiveData<String?>,
) : MediatorLiveData<Resource<List<CactusWithPhotos>>>() {

    private var cactusResource: Resource<List<Cactus>>? = null
    private var filterText: String? = null

    private fun checkStatus(resource: Resource<*>?): Boolean {
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


    private fun hasText(text: String?, filter: String): Boolean {
        if (!text.isNullOrEmpty() &&
            text.uppercase(Locale.getDefault()).contains(filter)
        ) {

            println("Text $text contains $filter")

            return true
        }
        return false
    }

    private fun includeAfterFilter(cactus: BaseCactus): Boolean {
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

    fun update() {
        if (checkStatus(cactusResource)) {
            cactusResource?.data?.let { cacti ->

                val result = ArrayList<CactusWithPhotos>()

                for (cactus in cacti) {
                    // Don't show unknown
                    if (cactus.id <= 1) continue

                    val photos = photoRepo.getPhotos(cactus)

                    checkAndAdd(cactus, photos, result)
                }

                this.postValue(Resource.success(result))
            }
        }
    }

    private fun checkAndAdd(
        cactus: Cactus,
        photos: List<Photo>,
        result: ArrayList<CactusWithPhotos>
    ) {
        if (includeAfterFilter(cactus)) {

            // Add to list
            result.add(CactusWithPhotos(cactus, photos))
        }
    }

    init {
        addSource(cactusRepo.getData()) {
            cactusResource = it
            update()
        }

        addSource(filterLiveData) {
            filterText = it?.uppercase()
            update()
        }
    }
}