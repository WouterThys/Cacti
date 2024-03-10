package com.cacti.cactiphone.view

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.widget.SearchView
import androidx.camera.core.Camera
import androidx.camera.view.PreviewView
import androidx.fragment.app.viewModels
import androidx.navigation.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.cacti.cactiphone.AppConstants.ADD_ID
import com.cacti.cactiphone.AppConstants.UNKNOWN_ID
import com.cacti.cactiphone.MainActivity
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

    private lateinit var cactusAdapter: CactusAdapter

    private val scanBarcodeRequest =
        registerForActivityResult(BarcodeRequestContract()) {
            handleBarcodeResult(it)
        }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        cactusAdapter = CactusAdapter(requireContext()).apply {
            setHasStableIds(true)
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        // Inflate the layout for this fragment
        _binding = FragmentCactusListBinding.inflate(inflater, container, false)

        binding.toolbar.title = getString(R.string.name)

        setupSwipeRefresh()
        setupRecyclerView()
        setupMenu()
        setupFabs()

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
                viewModel.clearSelected()
                moveToCactusEdit(it)
            }
        }

        viewModel.cactiCount.observe(viewLifecycleOwner) {
            binding.toolbar.subtitle = "$it cacti"
        }

        viewModel.pendingCount.observe(viewLifecycleOwner) {
            viewModel.newPending(it)
//            if (it > 0) {
//                binding.fabRefresh.show()
//            } else {
//                binding.fabRefresh.hide()
//            }
        }

        //binding.fabRefresh.hide()
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

        // below line is to get our menu item.
        val searchItem = binding.toolbar.menu.findItem(R.id.menu_item_search)

        // getting search view of our item.
        val searchView : SearchView = searchItem.actionView as SearchView

        // below line is to call set on query text listener method.
        searchView.setOnQueryTextListener(object : SearchView.OnQueryTextListener {
            override fun onQueryTextSubmit(query: String): Boolean {
                return false
            }

            override fun onQueryTextChange(newText: String): Boolean {
                // inside on query text change method we are
                // calling a method to filter our recycler view.
                viewModel.filter(newText)
                return false
            }
        })

        binding.toolbar.setOnMenuItemClickListener { item ->
            return@setOnMenuItemClickListener when (item.itemId) {
//                R.id.menu_item_add -> {
//                    moveToCactusEdit(ADD_ID)
//                    true
//                }
//                R.id.menu_item_scan -> {
//                    scanBarcodeRequest.launch(null)
//                    true
//                }
                R.id.menu_item_settings -> {
                    (activity as? MainActivity)?.showHostDialog()
                    true
                }
                else -> {
                    false
                }
            }
        }
    }

    private fun setupFabs() {

        binding.fabAdd.setOnClickListener {
            moveToCactusEdit(ADD_ID)
        }

        binding.fabBarcode.setOnClickListener {
            scanBarcodeRequest.launch(null)
        }

    }

}