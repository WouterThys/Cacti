package com.cacti.cactiphone.repository.database

import androidx.lifecycle.LiveData
import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import com.cacti.cactiphone.data.Cactus
import com.cacti.cactiphone.data.PendingSaveAction


@Dao
abstract class PendingDao {

    @Query("SELECT * FROM pending")
    abstract suspend fun getAll() : List<PendingSaveAction>

    @Query("SELECT * FROM pending WHERE id = :id")
    abstract fun getById(id: Long) : LiveData<PendingSaveAction?>

    @Query("SELECT * FROM pending WHERE id = :id")
    abstract suspend fun getByIdAsync(id: Long) : PendingSaveAction?

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    abstract suspend fun save(pendingSaveAction: PendingSaveAction)

    @Query("DELETE FROM pending")
    abstract suspend fun deleteAll()

    @Query("DELETE FROM pending WHERE id IN(:ids)")
    abstract suspend fun delete(ids: List<Long>)

}