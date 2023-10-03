package com.cacti.cactiphone.repository.database

import androidx.lifecycle.LiveData
import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import com.cacti.cactiphone.data.Cactus

@Dao
abstract class CactiDao {


    @Query("SELECT * FROM cacti")
    abstract fun getAll() : LiveData<List<Cactus>>

    @Query("SELECT * FROM cacti WHERE id = :id")
    abstract fun getById(id: Long) : LiveData<Cactus?>

    @Query("SELECT * FROM cacti WHERE id = :id")
    abstract suspend fun getByIdAsync(id: Long) : Cactus?

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    abstract suspend fun save(ts: Iterable<Cactus>)


}