using CactiClient.ViewModel.Cactus;

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
