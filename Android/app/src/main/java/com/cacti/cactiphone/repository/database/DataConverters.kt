package com.cacti.cactiphone.repository.database

import androidx.room.TypeConverter
import java.util.Date

class DataConverters {

    @TypeConverter
    fun fromTimestamp(value: Long?): Date? {
        return if (value == null) null else Date(value)
    }


    @TypeConverter
    fun dateToTimestamp(date: Date?): Long? {
        return date?.time
    }

}