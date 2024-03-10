using CactiClient.Model;
using CactiClient.ViewModel;
using CactiClient.ViewModel.Cactus;
using CactiClient.ViewModel.Info;
using CactiClient.WebClient;
using Common.Proto;
using DevExpress.Dialogs.Core.View;
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

        private readonly CactiService _CactiService;
        private readonly CallbackService _CallbackService;
        private readonly PhotoService _PhotoService;
        private readonly FileService _FileService;


        public override string Name => "MainViewModel";
        public override string Title => "Thuis";
        public override string ViewName => "MainView";


        private CactusListViewModel CactusListViewModel { get; }


        public MainViewModel()
        {
            // TODO: from settings
            _channel = GrpcChannel.ForAddress(Properties.Settings.Default.Address);
            _CactiService = CactiService.Initialize(_channel);
            _CallbackService = CallbackService.Initialize(_channel);
            _PhotoService = PhotoService.Initialize(_channel);
            _FileService = FileService.Initialize(_channel);
            _CallbackService.Register();

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

        public virtual bool CanShowInfo()
        {
            return true;
        }

        public virtual void ShowInfo()
        {
            var model = InfoViewModel.Create();
            DialogService.ShowDialog(MessageButton.OKCancel, model.Title, model.ViewName, model);
        }




        #endregion

        
    }
}
