package com.cacti.cactiphone.repository.data

import androidx.core.text.isDigitsOnly

data class Resource<out T>(val status: Status, val data: T?, var message: String?, var code: Int = 0) {

    enum class Status {
        LOADING,
        SUCCESS,
        ERROR,
    }

    override fun toString(): String {
        return "Resource(status=$status, data=$data, message=$message)"
    }

    fun isReady() : Boolean {
        return data != null && status >= Status.SUCCESS
    }

    private fun toHumanReadableString(camelCaseString: String): String {
        val regex = Regex("([a-z])([A-Z]+)")
        return regex.replace(camelCaseString, "$1 $2")
            .replaceFirstChar { it.uppercase() }
    }

    companion object {

        fun <T> success(data: T): Resource<T> =
            Resource(Status.SUCCESS, data, null)

        fun <T> error(message: String, data: T? = null, code: Int = 0) : Resource<T> =
            Resource(Status.ERROR, data, message, code)

        fun <T> loading(data: T? = null, code: Int = 0) : Resource<T> =
            Resource(Status.LOADING, data, null, code)

    }

}