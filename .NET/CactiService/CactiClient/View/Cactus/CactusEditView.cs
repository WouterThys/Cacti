using CactiClient.ViewModel.Cactus;
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
    public partial class CactusEditView : DevExpress.XtraEditors.XtraUserControl
    {
        public CactusEditView()
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!mvvmContext.IsDesignMode) 
            { 
                InitializeBindings();
            }
        }

        private void InitializeBindings() 
        {
            var fluent = mvvmContext.OfType<EditCactusViewModel>();

            fluent.SetObjectDataSourceBinding(cactusEditData.bsCactus, m => m.Editable, m => m.UpdateCommands());

            fluent.BindCommand(bbiSave, m => m.Save());
            fluent.BindCommand(bbiSaveAndClose, m => m.SaveAndClose());
            fluent.BindCommand(bbiReset, m => m.Reset());

            fluent.ViewModel.Load();
        }
    }
}
