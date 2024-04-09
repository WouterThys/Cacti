using System;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace CactiClient.Model
{
    public class CactusView : IEquatable<CactusView>
    {
        [JsonIgnore]
        private static readonly Image DefaultImage = Image.FromFile("cactus-icon-vector.jpg");

        [JsonIgnore]
        private static int NextInsertId = -100;


        public long Id { get; set; } = NextInsertId--;
        public string AndroidId { get; set; } = "";
        public string Code { get; set; } = "";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "";
        public string Barcodes { get; set; } = "";
        public long PhotoId { get; set; }
        public DateTime LastModified { get; set; }


        public string FathersCode { get; set; } = "";
        public string MothersCode { get; set; } = "";
        public string CrossingNumber { get; set; } = "";


        [JsonIgnore]
        public Image? Barcode { get; set; } = null;

        [JsonIgnore]
        public Image Image { get; private set; } = DefaultImage;

        [JsonIgnore]
        public bool ImageLoading { get; set; }

        [JsonIgnore]
        public bool HasPhoto => PhotoId > 1;

        [JsonIgnore]
        public string Info 
        { 
            get
            {
                if (Id > 1)
                {
                    return $"{Id} ({AndroidId}) - {LastModified:dd/MM/yyyy HH:mm}";
                }
                else
                {
                    return "Nieuwe cactus";
                }
            }
        }

        public void SetImage(Stream? fileStream) 
        {
            Image = DefaultImage;
            if (fileStream == null) return;

            var tmp = Image.FromStream(fileStream);
            TryRotate(tmp);
            Image = tmp;
        }

        public void SetImage(string path) 
        {
            Image = DefaultImage;

            if (string.IsNullOrEmpty(path)) return;
            if (!File.Exists(path)) return;

            var tmp = Image.FromFile(path);
            TryRotate(tmp);
            Image = tmp;
        }

        private static void TryRotate(Image img)
        {
            if (Array.IndexOf(img.PropertyIdList, 274) > -1)
            {
                var orientation = (int?)img.GetPropertyItem(274)?.Value?[0];
                switch (orientation)
                {
                    case 1:
                        // No rotation required.
                        break;
                    case 2:
                        img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case 3:
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 4:
                        img.RotateFlip(RotateFlipType.Rotate180FlipX);
                        break;
                    case 5:
                        img.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case 6:
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case 7:
                        img.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    case 8:
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }
                // This EXIF data is now invalid and should be removed.
                img.RemovePropertyItem(274);
            }
        }

        #region Equals


        public override bool Equals(object? obj)
        {
            return Equals(obj as CactusView);
        }

        public virtual bool Equals(CactusView? other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        #endregion

    }
}
