using CactiClient.Model;
using CactiClient.ViewModel.Cactus;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Tile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CactiClient.View.Cactus
{
    public partial class ImportCactiView : DevExpress.XtraEditors.XtraUserControl
    {
        public ImportCactiView()
        {
            InitializeComponent();
            if (!mvvmContext.IsDesignMode)
            {
                InitializeBindings();
            }
        }

        protected void InitializeBindings()
        {
            var fluent = mvvmContext.OfType<ImportCactiViewModel>();

            fluent.SetObjectDataSourceBinding(bindingSource);



            fluent.ViewModel.Load();

        }
    }
}
