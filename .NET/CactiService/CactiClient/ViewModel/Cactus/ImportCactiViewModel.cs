using CactiClient.Model;
using CactiClient.WebClient;
using Common.Proto;
using Common.Services;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CactiClient.ViewModel.Cactus
{
    [POCOViewModel]
    public class ImportCactiViewModel : BaseViewModel
    {
        public static ImportCactiViewModel Create()
        {
            return ViewModelSource.Create(() => new ImportCactiViewModel());
        }

        public override string Name => "ImportCactiViewModel";
        public override string Title => "Importeer";
        public override string ViewName => "ImportCactiView";

        private readonly CactiService _cactiService = CactiService.GetInstance();
        private readonly PhotoService _photoService = PhotoService.GetInstance();
        private readonly FileService _fileService = FileService.GetInstance();
        private readonly CallbackService _callback = CallbackService.GetInstance();


        public virtual string Step { get; set; } = "Selecteer afbeeldingen";
        public virtual string Description { get; set; } = "";
        public virtual string Info { get; set; } = "";
        public virtual int MajorProgress { get; set; }
        public virtual int MinorProgress { get; set; }

        public virtual List<string> Errors { get; set; } = new();


        protected ImportCactiViewModel()
        {
            
        }

        public override void Load()
        {
            OpenFileDialog openFileDialog = new()
            {
                InitialDirectory = "Pictures",
                Multiselect = true,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the path of specified file.
                CreateFromFiles(openFileDialog.FileNames);
            }
        }

        public override void UpdateCommands()
        {
            base.UpdateCommands();
        }

        private async Task CreateFromFiles(IEnumerable<string> files)
        {
            if (files == null || !files.Any()) return;

            int numFiles = files.Count();

            Info = $"{numFiles} afbeeldingen geselecteerd";
            Errors.Clear();

            // 1. upload all files
            // 2. create photos for each file
            // 3. create cactus for each photo

            var fileResult = await StoreImagesOnServer(files);
            if (fileResult.Item1)
            {
                var photoResult = await CreatePhotosOnServer(fileResult.Item2);
                if (photoResult.Item1) 
                {
                    var cactusResult = await CreateCactiOnServer(photoResult.Item2);
                    if (cactusResult.Item1)
                    {
                        // Done!!
                        MessageBoxService.ShowMessage(
                            $"{cactusResult.Item2.Count()} cacti aangemaakt",
                            "Klaar",
                            MessageButton.OK,
                            MessageIcon.Information);

                        OnClose(new System.ComponentModel.CancelEventArgs());
                    }
                }
                else
                {
                    // Show errors
                }
            }
            else
            {
                // Show errors
            }
        }

        private async Task<Tuple<bool, List<string>>> StoreImagesOnServer(IEnumerable<string> fileNames)
        {
            List<string> savedImages = new();
            MinorProgress = 0;
            MajorProgress = 0;
            Step = "Uploading images";

            int count = 0;
            int numFiles = fileNames.Count();

            foreach (string filePath in fileNames)
            {
                count++;
                MajorProgress = (count * 100) / numFiles;
                Description = $"Uploading {filePath}";

                if (!File.Exists(filePath)) 
                {
                    Errors.Add($"File {filePath} does not exist..");
                    continue;
                }

                if (!Utils.IsImage(filePath))
                {
                    Errors.Add($"File {filePath} is not an image..");
                    continue;
                }

                // Read the contents of the file into a stream.
                var path = await _fileService.Save(filePath, (percent) => 
                {
                    MinorProgress = percent;
                });
                savedImages.Add(path);
            }

            return Tuple.Create(true, savedImages);
        }

        private async Task<Tuple<bool, List<Photo>>> CreatePhotosOnServer(IEnumerable<string> fileNames)
        {
            List<Photo> savedPhotos = new();
            MinorProgress = 0;
            MajorProgress = 0;
            Step = "Creating photos";

            int count = 0;
            int numFiles = fileNames.Count();

            foreach (string filePath in fileNames)
            {
                count++;
                MajorProgress = (count * 100) / numFiles;
                Description = $"Creating photo for {filePath}";

                var photo = await _photoService.Save(new Photo() 
                { 
                    Code = Path.GetFileName(filePath),
                    Path = filePath,
                });


                savedPhotos.Add(photo);
            }

            return Tuple.Create(true, savedPhotos);
        }
    
        private async Task<Tuple<bool, List<Common.Proto.Cactus>>> CreateCactiOnServer(IEnumerable<Photo> photos)
        {
            List<Common.Proto.Cactus> savedCacti = new();
            MinorProgress = 0;
            MajorProgress = 0;
            Step = "Creating cacti";

            int count = 0;
            int numFiles = photos.Count();

            foreach (var photo in photos)
            {
                count++;
                MajorProgress = (count * 100) / numFiles;
                Description = $"Creating cactus for {photo.Code}";

                var cactus = await _cactiService.Save(new Common.Proto.Cactus()
                {
                    Code = photo.Code,
                    PhotoId = photo.Id,
                    Description = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                    Location = "",
                    Barcodes = "",
                });

                savedCacti.Add(cactus);
            }

            return Tuple.Create(true, savedCacti);
        }
    }
}
