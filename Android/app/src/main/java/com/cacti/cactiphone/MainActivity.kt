package com.cacti.cactiphone

import android.content.ContentValues
import android.content.DialogInterface
import android.database.sqlite.SQLiteDatabase
import android.os.Bundle
import android.widget.EditText
import androidx.activity.viewModels
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity
import com.cacti.cactiphone.viewmodel.MainViewModel
import dagger.hilt.android.AndroidEntryPoint


@AndroidEntryPoint
class MainActivity : AppCompatActivity() {

    private val viewModel: MainViewModel by viewModels()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
    }

    override fun onResume() {
        super.onResume()

        if (!viewModel.hasHost()) {
            showHostDialog()
        }

    }

    private fun showHostDialog() {
        val taskEditText = EditText(this)
        val dialog: AlertDialog = AlertDialog.Builder(this)
            .setTitle("Host")
            .setMessage("Change host?")
            .setView(taskEditText)
            .setPositiveButton("Ok") { _, _ ->
                val host = taskEditText.text.toString()
                viewModel.setHost(host)
            }
            .setNegativeButton("Cancel", null)
            .create()
        dialog.show()
    }

}