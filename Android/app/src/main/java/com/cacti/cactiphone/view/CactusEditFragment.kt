package com.cacti.cactiphone.view

import android.os.Bundle
import android.view.LayoutInflater
import android.view.MenuItem
import android.view.View
import android.view.ViewGroup
import android.widget.Toolbar
import androidx.activity.OnBackPressedCallback
import androidx.fragment.app.Fragment
import androidx.fragment.app.viewModels
import com.bumptech.glide.Glide
import com.cacti.cactiphone.R
import com.cacti.cactiphone.databinding.FragmentCactusEditBinding
import com.cacti.cactiphone.viewmodel.EditCactusViewModel
import dagger.hilt.android.AndroidEntryPoint
import java.util.Calendar


@AndroidEntryPoint
class CactusEditFragment : Fragment() {

    private val viewModel: EditCactusViewModel by viewModels()
    private var _binding: FragmentCactusEditBinding? = null
    private val binding get() = _binding!!

    private lateinit var backPressedCallback: OnBackPressedCallback

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        backPressedCallback = object : OnBackPressedCallback(true) {
            override fun handleOnBackPressed() {
                // in here you can do logic when backPress is clicked
                saveData(true)
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
                    saveData(false)
                    true
                }
                R.id.menu_item_delete -> {
                    // do something
                    deleteData()
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
            }
        }

        viewModel.photo.observe(viewLifecycleOwner) {
            it?.let { photo ->
                Glide.with(view)
                    .load(photo)
                    .into(binding.ivPhoto)
            }
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }

    private fun saveData(saveOnClose: Boolean) {
        launchOnIo {
            binding.cactus?.code = binding.etCode.text.toString()
            binding.cactus?.description = binding.etDescription.text.toString()
            binding.cactus?.location = binding.etLocation.text.toString()
            binding.cactus?.lastModified = Calendar.getInstance().time

            viewModel.save(binding.cactus)
        }.invokeOnCompletion {
            if (saveOnClose) {
                launchOnMain {
                    backPressedCallback.isEnabled = false
                    activity?.onBackPressedDispatcher?.onBackPressed()
                }
            }
        }
    }

    private fun deleteData() {
        launchOnIo {
            viewModel.delete()
        }.invokeOnCompletion {
            launchOnMain {
                backPressedCallback.isEnabled = false
                activity?.onBackPressedDispatcher?.onBackPressed()
            }
        }
    }
}