package com.cacti.cactiphone

import androidx.preference.PreferenceManager

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
        }
    }
}