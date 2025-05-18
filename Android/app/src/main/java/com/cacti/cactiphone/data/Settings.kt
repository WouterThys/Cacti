package com.cacti.cactiphone.data

import android.app.Activity
import android.content.Context

class Settings private constructor() {

    companion object {

        fun create() : Settings {
            return Settings()
        }

        const val CACTI_SETTINGS = "CACTI_SETTINGS"
        const val ADAPTER_TYPE = "CACTI_SETTING_ADAPTER_TYPE"

    }


    enum class AdapterType {
        Grid2,
        Grid3,
        Details
    }


    fun getAdapterType(activity: Activity) : AdapterType {
        val sharedPref = activity.getSharedPreferences(CACTI_SETTINGS, Context.MODE_PRIVATE)
        val type = sharedPref.getInt(ADAPTER_TYPE, 0)
        return when(type) {
            0 -> AdapterType.Grid2
            1 -> AdapterType.Grid3
            2 -> AdapterType.Details

            else -> AdapterType.Grid2
        }
    }

    fun setAdapterType(activity: Activity, type: AdapterType) {
        val sharedPref = activity.getSharedPreferences(CACTI_SETTINGS, Context.MODE_PRIVATE)
        with (sharedPref.edit()) {
            putInt(ADAPTER_TYPE, type.ordinal)
            apply()
        }
    }
}