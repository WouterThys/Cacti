package com.cacti.cactiphone.utils

import androidx.preference.PreferenceManager
import com.cacti.cactiphone.App
import com.cacti.cactiphone.AppConstants
import com.cacti.cactiphone.AppConstants.PREF_SESSION_Adapter

class AppSettings(val app: App) {

    private fun getPreferences() = PreferenceManager.getDefaultSharedPreferences(app)


    private var _host: String? = null
    @Synchronized
    fun getHost() : String {
        //return "http://192.168.1.58:5002/"
        return _host ?: (getPreferences().getString(AppConstants.PREF_SESSION_Address, "") ?: "").also {
            _host = it
        }
    }

    @Synchronized
    fun setHost(value: String) {
        _host = value
        getPreferences().edit().apply {
            putString(AppConstants.PREF_SESSION_Address, value)
            apply()
            commit()
        }
    }

    enum class AdapterType {
        Grid2,
        Grid3,
        Details
    }


    fun getAdapterType() : AdapterType {
        val sharedPref = getPreferences()
        val type = sharedPref.getInt(PREF_SESSION_Adapter, 0)
        return when(type) {
            0 -> AdapterType.Grid2
            1 -> AdapterType.Grid3
            2 -> AdapterType.Details

            else -> AdapterType.Grid2
        }
    }

    fun setAdapterType(type: AdapterType) {
        val sharedPref = getPreferences()
        with (sharedPref.edit()) {
            putInt(PREF_SESSION_Adapter, type.ordinal)
            apply()
        }
    }
}