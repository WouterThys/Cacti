package com.cacti.cactiphone.view

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.viewModels
import androidx.navigation.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.cacti.cactiphone.AppConstants.UNKNOWN_ID
import com.cacti.cactiphone.R
import com.cacti.cactiphone.databinding.FragmentCactusListBinding
import com.cacti.cactiphone.repository.data.Resource
import com.cacti.cactiphone.view.adapters.CactusAdapter
import com.cacti.cactiphone.view.utils.RecyclerClickSupport
import com.cacti.cactiphone.viewmodel.MainViewModel
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class CactusListFragment : Fragment() {

    private val viewModel: MainViewModel by viewModels({ requireActivity() })

    private var _binding: FragmentCactusListBinding? = null
    private val binding get() = _binding!!

    private val cactusAdapter = CactusAdapter().apply {
        setHasStableIds(true)
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

        RecyclerClickSupport.addTo(binding.rvCacti).setOnItemClickListener(object : RecyclerClickSupport.OnSingleClickListener{
            override fun onItemClicked(recyclerView: RecyclerView?, position: Int, v: View?) {
                val cactusId = cactusAdapter.getItemId(position)
                if (cactusId > UNKNOWN_ID) {
                    val action = CactusListFragmentDirections
                        .actionCactusListFragmentToCactusEditFragment()
                    action.constantParamsCACTUSID = cactusId

                    view?.findNavController()?.navigate(action)
                }
            }
        })

        binding.rvCacti.setHasFixedSize(true)
        binding.rvCacti.layoutManager = LinearLayoutManager(context)
        binding.rvCacti.adapter = cactusAdapter
    }

    companion object {

        @JvmStatic
        fun newInstance() = CactusListFragment()
    }
}