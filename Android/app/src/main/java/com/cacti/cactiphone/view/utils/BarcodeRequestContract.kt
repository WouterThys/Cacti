package com.cacti.cactiphone.view.utils

import android.content.Context
import android.content.Intent
import androidx.activity.result.contract.ActivityResultContract
import com.cacti.cactiphone.view.ScanBarcodeActivity

class BarcodeRequestContract: ActivityResultContract<Void?, Result<String>>() {

    override fun createIntent(context: Context, input: Void?): Intent =
        Intent(context, ScanBarcodeActivity::class.java)


    override fun parseResult(resultCode: Int, intent: Intent?): Result<String> {
        val barcode = intent?.getStringExtra(BARCODE_RESULT_CODE) ?: ""
        val message = intent?.getStringExtra(BARCODE_RESULT_MESSAGE) ?: ""
        return when (resultCode) {
            BARCODE_RESULT_OK -> {
                Result.success(barcode)
            }
            else -> {
                Result.failure(Exception(message))
            }
        }
    }

    companion object {

        const val BARCODE_RESULT_CODE = "BarcodeRequestContract_BARCODE"
        const val BARCODE_RESULT_MESSAGE = "BarcodeRequestContract_MESSAGE"

        const val BARCODE_RESULT_OK = 1
        const val BARCODE_RESULT_ERROR = -1

    }

}