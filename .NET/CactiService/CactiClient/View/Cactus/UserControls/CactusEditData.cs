using DevExpress.XtraEditors;
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

        }
    }
}
