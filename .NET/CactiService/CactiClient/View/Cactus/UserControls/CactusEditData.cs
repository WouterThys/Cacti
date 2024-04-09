using DevExpress.Utils.Menu;
using System.Collections.Generic;

namespace CactiClient.View.Cactus
{
    public partial class CactusEditData : DevExpress.XtraEditors.XtraUserControl
    {
        public CactusEditData()
        {
            InitializeComponent();

            if (!mvvmContext.IsDesignMode)
            {
                InitializeLayouts();
            }
        }

        private void InitializeLayouts()
        {
            BarcodePictureEdit.PopupMenuShowing += BarcodePictureEdit_PopupMenuShowing;
        }

        private void BarcodePictureEdit_PopupMenuShowing(object sender, DevExpress.XtraEditors.Events.PopupMenuShowingEventArgs e)
        {
            var list = new List<DXMenuItem>(e.PopupMenu.Items);

            foreach (var item in list)
            {
                if (item is DXMenuItem menuItem && menuItem.Tag.ToString() == DevExpress.XtraEditors.Controls.StringId.PictureEditMenuCopy.ToString())
                {
                    // Ok
                }
                else
                {
                    e.PopupMenu.Items.Remove(item);
                }
            }

        }
    }
}
