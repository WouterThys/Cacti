package com.cacti.cactiphone.repository.database

import android.content.Context
import androidx.room.Database
import androidx.room.Room
import androidx.room.RoomDatabase
import androidx.room.TypeConverters
import com.cacti.cactiphone.data.Cactus

@Database(
    version = 1,
    entities = [
        Cactus::class
    ]
)
@TypeConverters(DataConverters::class)
abstract class MyDatabase : RoomDatabase() {

    abstract fun cactiDao(): CactiDao

    companion object {
        @Volatile
        private var instance: MyDatabase? = null

        fun getDatabase(context: Context): MyDatabase =
            instance ?: synchronized(this) {
                instance ?: buildDatabase(context).also { instance = it }
            }

        private fun buildDatabase(appContext: Context) =
            Room.databaseBuilder(appContext, MyDatabase::class.java, "CactiDb")
                .fallbackToDestructiveMigration()
                .build()

        fun recreate(context: Context) {
            try {
                context.deleteDatabase("CactiDb")
                instance = null
            } catch (e: Exception) {
                System.err.println(e)
            }
        }
    }
}