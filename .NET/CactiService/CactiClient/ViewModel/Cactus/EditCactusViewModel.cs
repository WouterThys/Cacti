using CactiClient.Model;
using CactiClient.WebClient;
using Common.Proto;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.BarCodes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private readonly PhotoService _photoService = PhotoService.GetInstance();
        private readonly FileService _fileService = FileService.GetInstance();

        private bool _closeOnLoad = false;
        private bool _forceClose = false;

        private readonly List<Photo> _photosToSave = new(); 


        // Public MVVM variables
        private CactusView Original { get; set; }
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
                    LoadBarcode();
                    if (Editable.HasPhoto) 
                    {
                        LoadPhoto();
                    }
                }
                else 
                {
                    JsonConvert.PopulateObject(originalString, Editable);
                    this.RaisePropertiesChanged();
                }
            }
            IsLoading = false;
            _photosToSave.Clear();

            UpdateCommands();
        }

        public override void UpdateCommands()
        {
            this.RaiseCanExecuteChanged(m => m.Save());
            this.RaiseCanExecuteChanged(m => m.SaveAndClose());
            this.RaiseCanExecuteChanged(m => m.Reset());
            this.RaiseCanExecuteChanged(m => m.Delete());

            this.RaiseCanExecuteChanged(m => m.OpenFile());
        }

        private bool PropertiesChanged()
        {
            if (_photosToSave.Count > 0) 
            {
                return true;
            }

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

        private void LoadPhoto() 
        {
            if (Editable == null) return;
            if (!Editable.HasPhoto) return;

            Editable.ImageLoading = true;
            Task.Factory.StartNew(async (disp) => 
            {
                if (Editable == null) return;
                if (!Editable.HasPhoto) return;

                var photo = await _photoService.Get(Editable.PhotoId);
                if (photo == null) return;

                var path = await _fileService.Load(photo.Path);

                Editable.Image = Image.FromFile(path);

                if (disp == null) return;
                await ((IDispatcherService)disp).BeginInvoke(() => 
                {
                    Editable.ImageLoading = false;
                    this.RaisePropertyChanged(m => m.Editable);
                });

            }, DispatcherService);
        }

        private async Task SavePhotos() 
        {
            if (_photosToSave == null) return;
            foreach (var photo in _photosToSave ) 
            {
                if (!string.IsNullOrEmpty(photo.Path)) 
                {
                    var path = await _fileService.Save(photo.Path);
                    photo.Path = path;
                }

                var savedPhoto = await _photoService.Save(photo);
                if (savedPhoto != null && Editable != null)
                {
                    Editable.PhotoId = savedPhoto.Id;
                }
            }
            _photosToSave.Clear();
        }


        private void LoadBarcode()
        {
            if (Editable == null) return;
            if (Editable.Id <= 1) return;

            // GeneratedBarcode qrCode = QRCodeWriter.CreateQrCode(Editable.Id.ToString());

            BarCode barCode = new()
            {
                Symbology = Symbology.QRCode,
                CodeText = Editable.Id.ToString(),
                BackColor = Color.White,
                ForeColor = Color.Black,
                RotationAngle = 0,
                DpiX = 72,
                DpiY = 72,
                Module = 2f
            };
            barCode.CodeBinaryData = Encoding.Default.GetBytes(barCode.CodeText);


            Editable.Barcode = barCode.BarCodeImage;
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

                await SavePhotos();

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


        public virtual bool CanOpenFile()
        {
            return !IsLoading && Editable != null && Editable.Id > 1;
        }
        public virtual void OpenFile()
        {
            if (Editable == null) return;

            OpenFileDialog openFileDialog = new()
            {
                InitialDirectory = "Pictures",
                Multiselect = false,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the path of specified file.
                string filePath = openFileDialog.FileName;
                string code = Path.GetFileName(filePath);

                if (Utils.IsImage(filePath))
                {
                    MessageBoxService.ShowMessage($"{filePath} is geen afbeelding. Opnieuw!", "Afbeelding", MessageButton.OK, MessageIcon.Error);
                    return;
                }

                // Read the contents of the file into a stream.
                var fileStream = openFileDialog.OpenFile();
                Editable.Image = Image.FromStream(fileStream);

                // For now only 1 photo allowed
                _photosToSave.Clear();
                _photosToSave.Add(new Photo() 
                { 
                    Id = -1,
                    Code = code,
                    Path = filePath
                });

                this.RaisePropertyChanged(m => m.Editable);
                UpdateCommands();
            }
        }


        #endregion
    }
}
