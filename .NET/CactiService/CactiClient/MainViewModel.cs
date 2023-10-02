using CactiClient.Model;
using CactiClient.WebClient;
using Common.Proto;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraVerticalGrid.Rows;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CactiClient
{
    [POCOViewModel()]
    public class MainViewModel
    {
        private readonly GrpcChannel _channel;
        private readonly CactiService _service;


        public virtual bool IsLoading { get; set; }
        public virtual BindingList<CactusView> Cacti { get; protected set; } = new();
        public virtual List<CactusView> Selection { get; set; } = new();
        


        public virtual IMessageBoxService MessageBoxService { get { throw new NotImplementedException(); } }
        public virtual IDialogService DialogService { get { throw new NotImplementedException(); } }
        public virtual IDispatcherService DispatcherService { get { throw new NotImplementedException(); } }


        public MainViewModel()
        {
            // TODO: from settings
            _channel = GrpcChannel.ForAddress("http://localhost:5002");
            _service = new CactiService(_channel);
        }

        public void Load()
        {
            IsLoading = true;
            UpdateCommands();
            Task.Factory.StartNew(async (disp) =>
            {
                var cactiList = await _service.GetAll();
                if (cactiList != null)
                {
                    var mappedList = new List<CactusView>();
                    Parallel.ForEach(cactiList, (c) => 
                    {
                        if (c.Id > 1)
                        {
                            mappedList.Add(Mapper.Map(c));
                        }
                    });

                    if (disp == null) return;
                    _ = ((IDispatcherService)disp).BeginInvoke(() =>
                    {
                        Cacti = new BindingList<CactusView>(mappedList.ToList());
                        StartLoadingImages();

                        IsLoading = false;
                        UpdateCommands();
                    });
                }
            }, DispatcherService);
        }

        private void UpdateCommands()
        {
            this.RaiseCanExecuteChanged(m => m.Add());
            this.RaiseCanExecuteChanged(m => m.Edit(Selected));
            this.RaiseCanExecuteChanged(m => m.Delete(Selected));
        }

        private void StartLoadingImages()
        {
        }

        public virtual void OnSelectionChanged() 
        {
            UpdateCommands();
        }

        public virtual CactusView? Selected
        {
            get
            {
                if (Selection != null && Selection.Count > 0)
                {
                    return Selection[0];
                }
                return default;
            }
        }

        #region Commands

        public virtual bool CanAdd() 
        { 
            return !IsLoading;
        }
        public virtual void Add() 
        { 
        
        }


        public virtual bool CanEdit(CactusView? cactus) 
        {
            return !IsLoading && cactus != null && cactus.Id > 1;
        }
        public virtual void Edit(CactusView? cactus) 
        { 
            if (cactus == null) return;
            if (cactus.Id <= 1) return;


        }


        public virtual bool CanDelete(CactusView? cactus) 
        {
            return !IsLoading && cactus != null && cactus.Id > 1;
        }
        public virtual void Delete(CactusView? cactus) 
        {
            if (cactus == null) return;
            if (cactus.Id <= 1) return;


        }

        #endregion

    }
}
