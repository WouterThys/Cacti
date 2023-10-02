using CactiClient.Model;
using CactiClient.ViewModel;
using CactiClient.ViewModel.Cactus;
using CactiClient.WebClient;
using Common.Proto;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraVerticalGrid.Rows;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CactiClient
{
    [POCOViewModel()]
    public class MainViewModel : BaseViewModel
    {
        private readonly GrpcChannel _channel;
        private readonly CactiService _service;


        public override string Name => "MainViewModel";
        public override string Title => "Thuis";
        public override string ViewName => "MainView";


        private CactusListViewModel CactusListViewModel { get; }


        public MainViewModel()
        {
            // TODO: from settings
            _channel = GrpcChannel.ForAddress("http://localhost:5002");
            _service = CactiService.Initialize(_channel);

            CactusListViewModel = CactusListViewModel.Create();
            CactusListViewModel.SetParentViewModel(this);
        }

        public override void Load()
        {
            ShowDocument(CactusListViewModel);
            CactusListViewModel.Load();
        }

        public override void UpdateCommands()
        {

        }




        #region Commands

        

        #endregion

        
    }
}
