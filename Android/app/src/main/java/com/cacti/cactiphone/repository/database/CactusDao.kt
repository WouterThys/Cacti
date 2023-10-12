package com.cacti.cactiphone.repository.database

import androidx.lifecycle.LiveData
import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import androidx.room.Transaction
import com.cacti.cactiphone.data.Cactus

@Dao
abstract class CactusDao {


    @Query("SELECT * FROM cacti")
    abstract fun getAll() : LiveData<List<Cactus>>

    @Query("SELECT * FROM cacti WHERE id = :id")
    abstract fun getById(id: Long) : LiveData<Cactus?>

    @Query("SELECT * FROM cacti WHERE id = :id")
    abstract suspend fun getByIdAsync(id: Long) : Cactus?

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    abstract suspend fun save(ts: Iterable<Cactus>)

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    abstract suspend fun save(cactus: Cactus) : Long

    @Query("DELETE FROM cacti")
    abstract suspend fun deleteAll()

    @Query("DELETE FROM cacti WHERE id IN(:ids)")
    abstract suspend fun delete(ids: List<Long>)

    @Transaction
    open suspend fun refreshAll(ts: Iterable<Cactus>) {
        deleteAll()
        save(ts)
    }


}