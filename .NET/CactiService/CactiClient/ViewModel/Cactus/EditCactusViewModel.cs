using CactiClient.Model;
using CactiClient.WebClient;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CactiClient.ViewModel.Cactus
{
    [POCOViewModel]
    public class EditCactusViewModel : BaseViewModel
    {
        public static EditCactusViewModel Create(CactusView original)
        {
            return ViewModelSource.Create(() => new EditCactusViewModel(original));
        }

        public override string Name => "EditCactusViewModel";
        public override string Title => "Aanpassen";
        public override string ViewName => "CactusEditView";

        private readonly CactiService _service = CactiService.GetInstance();


        public virtual CactusView Original { get; set; }
        public virtual CactusView? Editable { get; set; }


        protected EditCactusViewModel(CactusView cactus)
        {
            Original = cactus;
        }

        public override void Load()
        {
            if (Original != null)
            {
                var originalString = JsonConvert.SerializeObject(Original);
                Editable = JsonConvert.DeserializeObject<CactusView>(originalString);
            }
            IsLoading = false;
            UpdateCommands();
        }

        public override void UpdateCommands()
        {
            this.RaiseCanExecuteChanged(m => m.Save());
            this.RaiseCanExecuteChanged(m => m.SaveAndClose());
            this.RaiseCanExecuteChanged(m => m.Reset());
        }

        private bool PropertiesChanged()
        {
            var originalString = JsonConvert.SerializeObject(Original);
            var editableString = JsonConvert.SerializeObject(Editable);

            return originalString.Equals(editableString);
        }

        #region Commands

        public virtual bool CanSave() 
        {
            return !IsLoading && Editable != null && !PropertiesChanged();
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

        }


        public virtual bool CanReset()
        {
            return !IsLoading && Editable != null && !PropertiesChanged();
        }
        public virtual void Reset()
        {
            Load();
        }


        #endregion
    }
}
