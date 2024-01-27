package com.cacti.cactiphone.repository.database

import androidx.lifecycle.LiveData
import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import androidx.room.Transaction
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.PendingCactus

@Dao
abstract class PendingCactusDao {

    @Query("SELECT * FROM pending_cacti")
    abstract fun getAll() : LiveData<List<PendingCactus>>

    @Query("SELECT * from pending_cacti LIMIT 1")
    abstract suspend fun getOne() : PendingCactus?

    @Query("SELECT count(*) FROM pending_cacti")
    abstract fun count() : LiveData<Int>

    @Query("SELECT * FROM pending_cacti WHERE id = :id")
    abstract fun getById(id: Long) : LiveData<PendingCactus?>

    @Query("SELECT * FROM pending_cacti WHERE id = :id")
    abstract suspend fun getByIdAsync(id: Long) : PendingCactus?

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    abstract suspend fun save(ts: Iterable<PendingCactus>)

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    abstract suspend fun save(cactus: PendingCactus) : Long

    @Query("DELETE FROM pending_cacti")
    abstract suspend fun deleteAll()

    @Query("DELETE FROM pending_cacti WHERE id IN(:ids)")
    abstract suspend fun delete(ids: List<Long>)

    @Transaction
    open suspend fun refreshAll(ts: Iterable<PendingCactus>) {
        deleteAll()
        save(ts)
    }


}