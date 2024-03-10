package com.cacti.cactiphone

import android.app.AlarmManager
import android.app.PendingIntent
import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.widget.EditText
import android.widget.LinearLayout
import androidx.activity.viewModels
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity
import com.cacti.cactiphone.view.runWhenResumed
import com.cacti.cactiphone.view.showToast
import com.cacti.cactiphone.viewmodel.MainViewModel
import dagger.hilt.android.AndroidEntryPoint
import kotlin.system.exitProcess


@AndroidEntryPoint
class MainActivity : AppCompatActivity() {

    private val viewModel: MainViewModel by viewModels()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        runWhenResumed {
            viewModel.mainEvents.subscribe<MainViewModel.MainEvent.AddressChanged> {
               restart()
            }
        }

        runWhenResumed {
            viewModel.mainEvents.subscribe<MainViewModel.MainEvent.ErrorEvent> {
                showToast(it.data)
            }
        }
    }

    override fun onResume() {
        super.onResume()

        if (!viewModel.hasHost()) {
            showHostDialog()
        }

        viewModel.testHost()
    }

    private fun restart() {
        val startActivity = Intent(this, MainActivity::class.java)
        val pendingIntentId = 123456
        val pendingIntent = PendingIntent.getActivity(
            this,
            pendingIntentId,
            startActivity,
            PendingIntent.FLAG_CANCEL_CURRENT or PendingIntent.FLAG_IMMUTABLE
        )

        val mgr : AlarmManager = getSystemService(Context.ALARM_SERVICE) as AlarmManager
        mgr.set(AlarmManager.RTC, System.currentTimeMillis() + 500, pendingIntent)

        //exitProcess(0)
        System.exit(0)
    }

    fun showHostDialog() {

        val lp = LinearLayout.LayoutParams(
            LinearLayout.LayoutParams.WRAP_CONTENT,
            LinearLayout.LayoutParams.WRAP_CONTENT
        )
        lp.setMargins(16, 8, 16, 8)

        val taskEditText = EditText(this)
        taskEditText.layoutParams = lp
        taskEditText.setText(viewModel.getHost())

        val dialog: AlertDialog = AlertDialog.Builder(this)
            .setTitle("Host")
            .setMessage("Change host?\nApp will be restarted.")
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