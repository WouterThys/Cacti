package com.cacti.cactiphone.view

import android.app.Activity
import android.content.Intent
import android.content.pm.PackageManager
import android.graphics.BitmapFactory
import android.net.Uri
import android.os.Bundle
import android.provider.MediaStore
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.activity.OnBackPressedCallback
import androidx.activity.result.contract.ActivityResultContracts
import androidx.appcompat.app.AlertDialog
import androidx.core.content.ContextCompat
import androidx.core.content.FileProvider
import androidx.fragment.app.Fragment
import androidx.fragment.app.viewModels
import com.bumptech.glide.Glide
import com.cacti.cactiphone.AppConstants.UNKNOWN_ID
import com.cacti.cactiphone.BuildConfig
import com.cacti.cactiphone.R
import com.cacti.cactiphone.databinding.FragmentCactusEditBinding
import com.cacti.cactiphone.viewmodel.EditCactusViewModel
import dagger.hilt.android.AndroidEntryPoint
import java.io.File
import java.util.Calendar


@AndroidEntryPoint
class CactusEditFragment : Fragment() {

    private val viewModel: EditCactusViewModel by viewModels()
    private var _binding: FragmentCactusEditBinding? = null
    private val binding get() = _binding!!

    private lateinit var backPressedCallback: OnBackPressedCallback

    private var photoFile: File? = null
    private val CAPTURE_IMAGE_REQUEST = 420

    private var onPermissionResult: ((Boolean) -> Unit)? = null
    private val permissionRequest =
        registerForActivityResult(ActivityResultContracts.RequestMultiplePermissions()) { map ->
            onPermissionResult?.let { it(map.all { pair -> pair.value }) }
        }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        backPressedCallback = object : OnBackPressedCallback(true) {
            override fun handleOnBackPressed() {
                backPressed()
            }
        }

        activity?.onBackPressedDispatcher?.addCallback(this, backPressedCallback)
    }


    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentCactusEditBinding.inflate(inflater, container, false)

        binding.toolbar.inflateMenu(R.menu.menu_edit_cactus)
        binding.toolbar.setOnMenuItemClickListener { item ->
            return@setOnMenuItemClickListener when (item.itemId) {
                R.id.menu_item_save -> {
                    saveData()
                    true
                }

                R.id.menu_item_delete -> {
                    deleteData()
                    true
                }

                R.id.menu_item_photo -> {
                    takePicture()
                    true
                }

                else -> {
                    false
                }
            }
        }

        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        viewModel.cactus.observe(viewLifecycleOwner) {
            it?.let { cactus ->
                binding.cactus = cactus

                if (cactus.id > UNKNOWN_ID) {
                    binding.toolbar.title = cactus.code
                } else {
                    binding.toolbar.title = getString(R.string.new_)
                }
            }
        }

        viewModel.photo.observe(viewLifecycleOwner) {
            it?.let { photo ->
                Glide.with(view)
                    .load(photo.path)
                    .into(binding.ivPhoto)
            } ?: run {
                binding.ivPhoto.setImageDrawable(
                    ContextCompat.getDrawable(requireContext(), R.drawable.cactus_icon_128))
            }
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }

    private fun backPressed() {
        // Create the object of AlertDialog Builder class

        if (viewModel.dataChanged(
                binding.etCode.text.toString(),
                binding.etDescription.text.toString(),
                binding.etLocation.text.toString(),
                ""
            )
        ) {

            val dialog = AlertDialog.Builder(requireContext())
                .setMessage(getString(R.string.cactus_changed))
                .setTitle(getString(R.string.changed))
                .setCancelable(true)
                .setPositiveButton("Yes") { _, _ ->
                    saveData()
                }
                .setNegativeButton("No") { dialog, _ ->
                    dialog.cancel()
                    closeFragment()
                }
                .create()

            // Show the Alert Dialog box
            dialog.show()
        } else {
            // Go back
            closeFragment()
        }
    }

    private fun saveData(saveOnClose: Boolean = true) {
        launchOnIo {
            updateCactus()
            viewModel.save()
        }.invokeOnCompletion {
            if (saveOnClose) {
                closeFragment()
            }
        }
    }

    private fun closeFragment() {
        launchOnMain {
            backPressedCallback.isEnabled = false
            activity?.onBackPressedDispatcher?.onBackPressed()
        }
    }

    private fun updateCactus() {
        binding.cactus?.code = binding.etCode.text.toString()
        binding.cactus?.description = binding.etDescription.text.toString()
        binding.cactus?.location = binding.etLocation.text.toString()
        binding.cactus?.lastModified = Calendar.getInstance().time
        binding.cactus?.fathersCode = binding.etFather.text.toString()
        binding.cactus?.mothersCode = binding.etMother.text.toString()
        binding.cactus?.crossingsNumber = binding.etCrossing.text.toString()
    }

    private fun deleteData() {
        launchOnIo {
            viewModel.delete()
        }.invokeOnCompletion {
            closeFragment()
        }
    }

    private fun takePicture() {
        checkPermission(
            arrayOf(
                android.Manifest.permission.CAMERA
            )
        ) { granted ->
            if (granted) {
                try {
                    val takePictureIntent = Intent(MediaStore.ACTION_IMAGE_CAPTURE)
                    photoFile = createImageFile()
                    val photoURI = FileProvider.getUriForFile(
                        requireContext(),
                        BuildConfig.APPLICATION_ID + ".provider",
                        photoFile!!
                    )
                    takePictureIntent.putExtra(MediaStore.EXTRA_OUTPUT, photoURI)
                    startActivityForResult(takePictureIntent, CAPTURE_IMAGE_REQUEST)

                } catch (ex: Exception) {
                    // Error occurred while creating the File
                    showToast(ex.message.toString())
                }
            }
        }
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)
        photoFile?.let {
            if (requestCode == CAPTURE_IMAGE_REQUEST && resultCode == Activity.RESULT_OK) {
                galleryAddPic(it)
                viewModel.addPhoto(it)
            } else {
                it.delete()
                showToast("Request cancelled or something went wrong.")
            }
        }
        photoFile = null
    }

    private fun galleryAddPic(imageFile: File) {
        Intent(Intent.ACTION_MEDIA_SCANNER_SCAN_FILE).also { mediaScanIntent ->
            mediaScanIntent.data = Uri.fromFile(imageFile)
            requireActivity().sendBroadcast(mediaScanIntent)
        }
    }

    @Throws(Exception::class)
    private fun createImageFile(): File {
        // Create an image file name
        val newImageFile = viewModel.getNewImageFile()

        // Save a file: path for use with ACTION_VIEW intents
        currentPhotoPath = newImageFile.absolutePath

        return newImageFile
    }

    private fun checkPermission(permissions: Array<String>, onPermissionResult: (Boolean) -> Unit) {
        this.onPermissionResult = onPermissionResult
        val deniedPermissions = ArrayList<String>()
        for (p in permissions) {
            if (ContextCompat.checkSelfPermission(
                    requireActivity(),
                    p
                ) != PackageManager.PERMISSION_GRANTED
            ) {
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
}