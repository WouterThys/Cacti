using System.Drawing;

namespace CactiClient.Model
{
    public class CactusView
    {
        public long Id { get; set; } = 0;
        public string Code { get; set; } = "";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "";
        public string Barcodes { get; set; } = "";


        public Image? Image { get; set; } = null;
        public bool ImageLoading { get; set; }

    }
}
