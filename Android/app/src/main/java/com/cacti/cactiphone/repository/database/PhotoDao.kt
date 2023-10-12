package com.cacti.cactiphone.repository.database

import androidx.lifecycle.LiveData
import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import androidx.room.Transaction
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.Photo

@Dao
abstract class PhotoDao {


    @Query("SELECT * FROM photos")
    abstract fun getAll() : LiveData<List<Photo>>

    @Query("SELECT * FROM photos WHERE id = :id")
    abstract fun getById(id: Long) : LiveData<Photo?>

    @Query("SELECT * FROM photos WHERE id = :id")
    abstract suspend fun getByIdAsync(id: Long) : Photo?

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    abstract suspend fun save(ts: Iterable<Photo>)

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    abstract suspend fun save(photo: Photo) : Long

    @Query("DELETE FROM photos")
    abstract suspend fun deleteAll()

    @Query("DELETE FROM photos WHERE id IN(:ids)")
    abstract suspend fun delete(ids: List<Long>)

    @Transaction
    open suspend fun refreshAll(ts: Iterable<Photo>) {
        deleteAll()
        save(ts)
    }


}