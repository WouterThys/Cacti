using CactiClient.Model;
using CactiClient.WebClient;
using Common.Proto;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using Google.Api;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Threading.Tasks;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace CactiClient.ViewModel.Cactus
{
    [POCOViewModel]
    public class EditCactusViewModel : BaseViewModel
    {
        public static EditCactusViewModel Create(CactusView original)
        {
            return ViewModelSource.Create(() => new EditCactusViewModel(original));
        }

        // Model variables
        public override string Name => "EditCactusViewModel";
        public override string Title => "Aanpassen";
        public override string ViewName => "CactusEditView";


        // Private stuff
        private readonly CactiService _service = CactiService.GetInstance();
        private bool _closeOnLoad = false;
        private bool _forceClose = false;

        // Public MVVM variables
        public virtual CactusView Original { get; set; }
        public virtual CactusView? Editable { get; set; }


        protected EditCactusViewModel(CactusView cactus)
        {
            Original = cactus;
        }

        public override void Load()
        {
            if (_closeOnLoad) 
            {
                OnClose(new CancelEventArgs());
                return;
            }


            if (Original != null)
            {
                var originalString = JsonConvert.SerializeObject(Original);
                if (Editable == null) 
                {
                    Editable = JsonConvert.DeserializeObject<CactusView>(originalString);
                }
                else 
                {
                    JsonConvert.PopulateObject(originalString, Editable);
                    this.RaisePropertiesChanged();
                }
            }
            IsLoading = false;
            UpdateCommands();
        }

        public override void UpdateCommands()
        {
            this.RaiseCanExecuteChanged(m => m.Save());
            this.RaiseCanExecuteChanged(m => m.SaveAndClose());
            this.RaiseCanExecuteChanged(m => m.Reset());
            this.RaiseCanExecuteChanged(m => m.Delete());
        }

        private bool PropertiesChanged()
        {
            var originalString = JsonConvert.SerializeObject(Original);
            var editableString = JsonConvert.SerializeObject(Editable);

            return !originalString.Equals(editableString);
        }

        public override void OnClose(CancelEventArgs e)
        {
            if (!_forceClose && PropertiesChanged())
            {
                var res = MessageBoxService.ShowMessage(
                    "Er is vanalles aangepast, aanpassingen opslaan?",
                    "Close",
                    MessageButton.YesNoCancel,
                    MessageIcon.Warning,
                    MessageResult.No);

                if (res == MessageResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (res == MessageResult.Yes)
                {
                    e.Cancel = true;
                    _closeOnLoad = true;
                    Save();
                }
            }
            else 
            {
                DocumentOwner?.Close(this);
            }
            _closeOnLoad = false;
            _forceClose = false;
            base.OnClose(e);
        }

        #region Commands

        public virtual bool CanSave() 
        {
            return !IsLoading && Editable != null && PropertiesChanged();
        }
        public virtual void Save() 
        {
            if (Editable == null) return;

            IsLoading = true;
            Task.Factory.StartNew(async (disp) => 
            {
                if (Editable == null) return;

                var toSave = Mapper.Map(Editable);
                var saved = await _service.Save(toSave);

                if (disp == null) return;
                _ = ((IDispatcherService)disp).BeginInvoke(() => 
                {
                    Original = Mapper.Map(saved);

                    Load();
                });
            }, DispatcherService);
        }


        public virtual bool CanSaveAndClose()
        {
            return CanSave();
        }
        public virtual void SaveAndClose()
        {
            _closeOnLoad = true;
            Save();
        }


        public virtual bool CanReset()
        {
            return !IsLoading && Editable != null && PropertiesChanged();
        }
        public virtual void Reset()
        {
            Load();
        }


        public virtual bool CanDelete() 
        {
            return !IsLoading && Editable != null && Editable.Id > 1;
        }
        public virtual void Delete()
        {
            if (Editable == null) return;

            var res = MessageBoxService.ShowMessage(
                            $"Cactus {Editable.Code} weg doen? :( :(",
                            "Weg :(",
                            MessageButton.YesNo,
                            MessageIcon.Question);

            if (res == MessageResult.Yes)
            {
                Task.Factory.StartNew(async (disp) => 
                { 
                    await _service.Delete(Editable.Id);

                    if (disp == null) return;
                    await ((IDispatcherService)disp).BeginInvoke(() => 
                    {
                        _forceClose = true;
                        OnClose(new CancelEventArgs());
                    });

                }, DispatcherService);
            }
        }


        #endregion
    }
}
