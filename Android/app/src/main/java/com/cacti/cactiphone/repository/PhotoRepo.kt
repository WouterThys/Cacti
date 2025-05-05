package com.cacti.cactiphone.repository

import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.Photo
import javax.inject.Inject

class PhotoRepo @Inject constructor() {

    // TODO: this one should query the local photo in the directory structure
    //

    fun getPhotos(cactus: Cactus) : List<Photo> {
        // TODO: load from disk
        return emptyList()
    }

    fun deleteFor(cactus: Cactus) {

    }

    fun getFirstPhoto(cactus: Cactus) : Photo? {
        return null
    }
}