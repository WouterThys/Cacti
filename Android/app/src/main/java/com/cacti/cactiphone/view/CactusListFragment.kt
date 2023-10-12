package com.cacti.cactiphone.view

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.camera.core.Camera
import androidx.camera.view.PreviewView
import androidx.fragment.app.viewModels
import androidx.navigation.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.cacti.cactiphone.AppConstants.UNKNOWN_ID
import com.cacti.cactiphone.R
import com.cacti.cactiphone.databinding.FragmentCactusListBinding
import com.cacti.cactiphone.repository.data.Resource
import com.cacti.cactiphone.view.adapters.CactusAdapter
import com.cacti.cactiphone.view.utils.BarcodeRequestContract
import com.cacti.cactiphone.view.utils.RecyclerClickSupport
import com.cacti.cactiphone.viewmodel.MainViewModel
import com.google.mlkit.vision.barcode.BarcodeScannerOptions
import com.google.mlkit.vision.barcode.BarcodeScanning
import com.google.mlkit.vision.barcode.common.Barcode
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class CactusListFragment : Fragment() {

    private val viewModel: MainViewModel by viewModels({ requireActivity() })

    private var _binding: FragmentCactusListBinding? = null
    private val binding get() = _binding!!

    private val cactusAdapter = CactusAdapter().apply {
        setHasStableIds(true)
    }

    private val scanBarcodeRequest =
        registerForActivityResult(BarcodeRequestContract()) {
            handleBarcodeResult(it)
        }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        // Inflate the layout for this fragment
        _binding = FragmentCactusListBinding.inflate(inflater, container, false)

        setupSwipeRefresh()
        setupRecyclerView()
        setupMenu()

        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        viewModel.cactusList.observe(viewLifecycleOwner) {
            when (it.status) {
                Resource.Status.LOADING -> {
                    binding.swipeContainer.isRefreshing = true
                }
                Resource.Status.ERROR -> {
                    binding.swipeContainer.isRefreshing = false
                    showToast("Alles gaat mis: ${it.message}")
                }
                Resource.Status.SUCCESS -> {
                    cactusAdapter.submit(it.data)
                    binding.swipeContainer.isRefreshing = false
                }
            }
        }

        viewModel.selectedCactusId.observe(viewLifecycleOwner) {
            if (it > UNKNOWN_ID) {
                moveToCactusEdit(it)
            }
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }

    private fun handleBarcodeResult(result: Result<String>) {
        if (result.isSuccess) {
            viewModel.findBarcode(result.getOrNull())
        } else {
            showToast("Error: " + result.exceptionOrNull()?.message)
        }
    }

    private fun moveToCactusEdit(id: Long) {
        val action = CactusListFragmentDirections
            .actionCactusListFragmentToCactusEditFragment()
        action.constantParamsCACTUSID = id

        view?.findNavController()?.navigate(action)
    }

    private fun setupSwipeRefresh() {
        binding.swipeContainer.setOnRefreshListener {
            launchOnIo {
                viewModel.refresh()
            }
        }
        //binding.swipeContainer.setColorSchemeResources(R.color.colorAccent)
    }

    private fun setupRecyclerView() {

        RecyclerClickSupport.addTo(binding.rvCacti).setOnItemClickListener(object : RecyclerClickSupport.OnSingleClickListener{
            override fun onItemClicked(recyclerView: RecyclerView?, position: Int, v: View?) {
                val cactusId = cactusAdapter.getItemId(position)
                if (cactusId > UNKNOWN_ID) {
                    moveToCactusEdit(cactusId)
                }
            }
        })

        binding.rvCacti.setHasFixedSize(true)
        binding.rvCacti.layoutManager = LinearLayoutManager(context)
        binding.rvCacti.adapter = cactusAdapter
    }

    private fun setupMenu() {
        binding.toolbar.inflateMenu(R.menu.menu_list_cactus)
        binding.toolbar.setOnMenuItemClickListener { item ->
            return@setOnMenuItemClickListener when (item.itemId) {
                R.id.menu_item_add -> {
                    // TODO saveData(false)
                    true
                }
                R.id.menu_item_scan -> {
                    scanBarcodeRequest.launch(null)
                    true
                }
                else -> {
                    false
                }
            }
        }
    }


    companion object {

        @JvmStatic
        fun newInstance() = CactusListFragment()
    }
}