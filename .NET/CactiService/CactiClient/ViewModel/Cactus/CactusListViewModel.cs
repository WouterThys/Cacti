using CactiClient.Model;
using CactiClient.WebClient;
using Common.Proto;
using Common.Services;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CactiClient.ViewModel.Cactus
{
    [POCOViewModel]
    public class CactusListViewModel : BaseViewModel, INotifyChanged
    {
        public static CactusListViewModel Create()
        {
            return ViewModelSource.Create(() => new CactusListViewModel());
        }

        public override string Name => "CactusListViewModel";
        public override string Title => "Lijst";
        public override string ViewName => "CactusListView";

        private readonly CactiService _service = CactiService.GetInstance();
        private readonly CallbackService _callback = CallbackService.GetInstance();


        public virtual BindingList<CactusView> Cacti { get; protected set; } = new();
        public virtual List<CactusView> Selection { get; set; } = new();


        protected CactusListViewModel() 
        {
            _callback.RegisterCallback(this);
        }


        public override void Load()
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

        public override void UpdateCommands()
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
            var cactus = new CactusView() { Id = -10 };
            var model = EditCactusViewModel.Create(cactus);
            model.SetParentViewModel(this);
            ShowDocument(model);
        }


        public virtual bool CanEdit(CactusView? cactus)
        {
            return !IsLoading && cactus != null && cactus.Id > 1;
        }
        public virtual void Edit(CactusView? cactus)
        {
            if (cactus == null) return;
            if (cactus.Id <= 1) return;

            var model = EditCactusViewModel.Create(cactus);
            model.SetParentViewModel(this);
            ShowDocument(model);
        }


        public virtual bool CanDelete(CactusView? cactus)
        {
            return !IsLoading && cactus != null && cactus.Id > 1;
        }
        public virtual void Delete(CactusView? cactus)
        {
            if (cactus == null) return;
            if (cactus.Id <= 1) return;

            var res = MessageBoxService.ShowMessage(
                $"Cactus {cactus.Code} weg doen? :( :(", 
                "Weg :(", 
                MessageButton.YesNo, 
                MessageIcon.Question);

            if (res == MessageResult.Yes) 
            {
                Task.Factory.StartNew(async () => { await _service.Delete(cactus.Id); });
            }
        }

        #endregion

        #region Callback

        public Task NotifyChanged<T>(UpdateAction type, T data)
        {
            if (data is Common.Proto.Cactus cactus) 
            {
                var mapped = Mapper.Map(cactus);

                switch (type)
                {
                    case UpdateAction.Insert:
                        Cacti.Add(mapped);
                        break;
                    case UpdateAction.Update:
                        var index = Cacti.IndexOf(mapped);
                        Cacti[index] = mapped;
                        Cacti.ResetItem(index);
                        break;
                    case UpdateAction.Delete:
                        Cacti.Remove(mapped);
                        break;
                }
            }
            return Task.CompletedTask;
        }

        #endregion
    }
}
