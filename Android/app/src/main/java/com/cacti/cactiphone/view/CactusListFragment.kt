package com.cacti.cactiphone.view

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.viewModels
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.cacti.cactiphone.R
import com.cacti.cactiphone.databinding.FragmentCactusListBinding
import com.cacti.cactiphone.repository.data.Resource
import com.cacti.cactiphone.view.adapters.CactusAdapter
import com.cacti.cactiphone.viewmodel.MainViewModel

class CactusListFragment : Fragment() {

    private val viewModel: MainViewModel by viewModels({ requireActivity() })

    private var _binding: FragmentCactusListBinding? = null
    private val cactusAdapter = CactusAdapter().apply {
        setHasStableIds(true)
    }
    private val binding get() = _binding!!

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
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
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
//        RecyclerClickSupport.addTo(binding.rvCacti, true)
//            .setOnItemClickListener(object : RecyclerClickSupport.OnItemClickListener {
//                override fun onItemClicked(recyclerView: RecyclerView?, position: Int, v: View?) {
//                    if (dataAdapter.showCheckBoxes) {
//                        val id = dataAdapter.getItemId(position)
//                        dataAdapter.toggleSelection(id)
//                        viewModel.addToMultiSelect(dataAdapter.getSelection())
//                    } else {
//                        launchOnIo {
//                            val jo = viewModel.getJobOperation(dataAdapter.getItemId(position))
//                            safeLet(jo, (activity as MainActivity?)) { jobOperation, act ->
//                                UIUtil.hideKeyboard(act)
//                                act.moveToJobOperation(jobOperation, 0)
//                            }
//                        }
//                    }
//                }
//
//                override fun onItemDoubleClicked(
//                    recyclerView: RecyclerView?,
//                    position: Int,
//                    v: View?
//                ) {
//                    if (!dataAdapter.showCheckBoxes) {
//                        launchOnIo {
//                            viewModel.getJobOperation(dataAdapter.getItemId(position))?.let {
//                                viewModel.setFilters(it)
//                            }
//                        }
//                    } else {
//                        onItemClicked(recyclerView, position, v)
//                    }
//                }
//
//                override fun onItemLongClicked(
//                    recyclerView: RecyclerView?,
//                    position: Int,
//                    v: View?
//                ) {
//                    // Set state to multi select
//                    dataAdapter.clearSelection()
//                    viewModel.switchState(MainViewModel.ListViewState.MultipleJobs)
//                }
//            })

        binding.rvCacti.setHasFixedSize(true)
        binding.rvCacti.layoutManager = LinearLayoutManager(context)
        binding.rvCacti.adapter = cactusAdapter
    }

    companion object {

        @JvmStatic
        fun newInstance() = CactusListFragment()
    }
}