using CactiClient.ViewModel.Info;

namespace CactiClient.View.Info
{
    public partial class InfoView : DevExpress.XtraEditors.XtraUserControl
    {
        public InfoView()
        {
            InitializeComponent();
            if (!mvvmContext.IsDesignMode)
            {
                dataLayoutControl1.OptionsView.IsReadOnly = DevExpress.Utils.DefaultBoolean.True;

                InitializeBindings();
            }
        }

        protected void InitializeBindings()
        {
            var fluent = mvvmContext.OfType<InfoViewModel>();

            fluent.SetObjectDataSourceBinding(bindingSource);



            fluent.ViewModel.Load();

        }
    }
}
