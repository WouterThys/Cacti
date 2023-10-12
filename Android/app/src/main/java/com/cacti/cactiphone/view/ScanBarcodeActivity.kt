package com.cacti.cactiphone.view

import android.content.Intent
import android.content.pm.PackageManager
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.activity.result.contract.ActivityResultContracts
import androidx.camera.core.Camera
import androidx.camera.core.CameraSelector
import androidx.camera.core.ExperimentalGetImage
import androidx.camera.core.ImageAnalysis
import androidx.camera.core.ImageProxy
import androidx.camera.core.Preview
import androidx.camera.lifecycle.ProcessCameraProvider
import androidx.camera.view.PreviewView
import androidx.core.content.ContextCompat
import com.cacti.cactiphone.R
import com.cacti.cactiphone.view.utils.BarcodeRequestContract.Companion.BARCODE_RESULT_CODE
import com.cacti.cactiphone.view.utils.BarcodeRequestContract.Companion.BARCODE_RESULT_ERROR
import com.cacti.cactiphone.view.utils.BarcodeRequestContract.Companion.BARCODE_RESULT_MESSAGE
import com.cacti.cactiphone.view.utils.BarcodeRequestContract.Companion.BARCODE_RESULT_OK
import com.google.mlkit.vision.barcode.BarcodeScanner
import com.google.mlkit.vision.barcode.BarcodeScannerOptions
import com.google.mlkit.vision.barcode.BarcodeScanning
import com.google.mlkit.vision.barcode.ZoomSuggestionOptions
import com.google.mlkit.vision.barcode.common.Barcode
import com.google.mlkit.vision.common.InputImage
import java.util.concurrent.Executors

class ScanBarcodeActivity : AppCompatActivity(), ZoomSuggestionOptions.ZoomCallback {

    private lateinit var cameraPreview: PreviewView
    private var camera: Camera? = null

    // Results
    private var barcode: String = ""
    private var message: String = ""

    private var onPermissionResult: ((Boolean) -> Unit)? = null
    private val permissionRequest =
        registerForActivityResult(ActivityResultContracts.RequestMultiplePermissions()) { map ->
            onPermissionResult?.let { it(map.all { pair -> pair.value }) }
        }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_scan_barcode)

        cameraPreview = findViewById(R.id.cameraView)

        checkPermission(arrayOf(android.Manifest.permission.CAMERA)) { granted ->
            if (granted) {
                setupCamera()
            } else {
                //message = ""
                //setBarcodeResult(BARCODE_RESULT_ERROR)
            }
        }
    }

    private fun checkPermission(permissions: Array<String>, onPermissionResult: (Boolean) -> Unit) {
        this.onPermissionResult = onPermissionResult
        val deniedPermissions = ArrayList<String>()
        for (p in permissions) {
            if (ContextCompat.checkSelfPermission(this, p) != PackageManager.PERMISSION_GRANTED) {
                deniedPermissions.add(p)
            }
        }

        // request for permission which are not granted yet
        if (deniedPermissions.any()) {
            permissionRequest.launch(deniedPermissions.toTypedArray())
        } else {
            onPermissionResult(true)
        }
    }

    private fun setupCamera() {
        val cameraProviderFuture = ProcessCameraProvider.getInstance(this)
        cameraProviderFuture.addListener({

            val cameraProvider = cameraProviderFuture.get()

            // setting up the preview use case
            val previewUseCase = Preview.Builder()
                .build()
                .also {
                    it.setSurfaceProvider(cameraPreview.surfaceProvider)
                }

            // Set all barcode types enabled, but better to not add all?
            val options = BarcodeScannerOptions
                .Builder()
                //.enableAllPotentialBarcodes()
                .setBarcodeFormats(Barcode.FORMAT_QR_CODE)
                .setZoomSuggestionOptions(
                    ZoomSuggestionOptions.Builder(this)
                        .setMaxSupportedZoomRatio(2.0F)
                        .build()
                )
                .build()

            // getClient() creates a new instance of the MLKit barcode scanner with the specified options
            val scanner = BarcodeScanning.getClient(options)

            // setting up the analysis use case
            val analysisUseCase = ImageAnalysis.Builder()
                .setBackpressureStrategy(ImageAnalysis.STRATEGY_KEEP_ONLY_LATEST)
                .build()

            // define the actual functionality of our analysis use case
            analysisUseCase.setAnalyzer(Executors.newSingleThreadExecutor()) { imageProxy ->
                processImageProxy(scanner, imageProxy)
            }

            // configure to use the back camera
            val cameraSelector = CameraSelector.DEFAULT_BACK_CAMERA

            try {
                // Unbind use cases before rebinding
                cameraProvider.unbindAll()

                cameraProvider.bindToLifecycle(
                    this,
                    cameraSelector,
                    previewUseCase,
                    analysisUseCase
                )
            } catch (ex: Exception) {
                ex.printStackTrace()
                message = ex.message.toString()
                setBarcodeResult(BARCODE_RESULT_ERROR)
            }

        }, ContextCompat.getMainExecutor(this))
    }


    @androidx.annotation.OptIn(androidx.camera.core.ExperimentalGetImage::class)
    private fun processImageProxy(barcodeScanner: BarcodeScanner, imageProxy: ImageProxy) {
        imageProxy.image?.let { image ->
            val inputImage = InputImage.fromMediaImage(image, imageProxy.imageInfo.rotationDegrees)

            barcodeScanner.process(inputImage)
                .addOnSuccessListener { barcodeList ->
                    barcodeList.firstOrNull()?.let { foundBarcode ->
                        if (!foundBarcode.rawValue.isNullOrEmpty()) {
                            barcode = foundBarcode.rawValue.toString()
                            message = ""
                            setBarcodeResult(BARCODE_RESULT_OK)
                        }
                    }
                }
                .addOnFailureListener {
                    // This failure will happen if the barcode scanning model
                    // fails to download from Google Play Services
                    it.printStackTrace()
                    message = it.message.toString()
                    setBarcodeResult(BARCODE_RESULT_ERROR)

                }
                .addOnCompleteListener {
                    // When the image is from CameraX analysis use case, must
                    // call image.close() on received images when finished
                    // using them. Otherwise, new images may not be received
                    // or the camera may stall.
                    imageProxy.image?.close()
                    imageProxy.close()

                }
        }
    }

    override fun setZoom(zoom: Float): Boolean {
        camera?.cameraControl?.setZoomRatio(zoom)
        return true
    }

    private fun setBarcodeResult(resultCode: Int) {
        val intent = Intent().apply {
            putExtra(BARCODE_RESULT_CODE, barcode)
            putExtra(BARCODE_RESULT_MESSAGE, message)
        }
        setResult(resultCode, intent)
        finish()
    }
}