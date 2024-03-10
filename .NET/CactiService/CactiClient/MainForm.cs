using DevExpress.LookAndFeel;
using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Globalization;

namespace CactiClient
{
    public partial class MainView : RibbonForm
    {
        public MainView()
        {
            InitializeComponent();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Utils.Drawing.Helpers.Win32SubclasserException.Allow = false;
            if (!mvvmContext.IsDesignMode)
            {
                InitializeCultureInfo();
                InitializeLayouts();
                InitializeServices();
                InitializeBindings();
            }
        }

        protected void InitializeLayouts()
        {
            documentManager.RibbonAndBarsMergeStyle = RibbonAndBarsMergeStyle.WhenNotFloating;
            Ribbon.MdiMergeStyle = RibbonMdiMergeStyle.Always;
            Ribbon.Merge += Ribbon_Merge;

            tabbedView1.FloatDocumentsAlwaysOnTop = DevExpress.Utils.DefaultBoolean.False;
        }

        protected void InitializeServices()
        {
            mvvmContext.RegisterService(DocumentManagerService.Create(tabbedView1));
            mvvmContext.RegisterService(MessageBoxService.Create(DefaultMessageBoxServiceType.XtraMessageBox));
            mvvmContext.RegisterService("FloatingDocumentService", WindowedDocumentManagerService.CreateXtraFormService(this));
            //mvvmContext.RegisterDefaultService("ErrorManagerService", new ErrorManagerService(mvvmContext.GetViewModel<MainViewModel>()));
            //mvvmContext.RegisterService("DisplayMessageService", new DisplayMessageService(this));

            tabbedView1.DocumentGroupProperties.ShowTabHeader = false;
        }

        protected virtual void InitializeCultureInfo()
        {
            CultureInfo cultureInfo = new CultureInfo("nl-BE");
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }

        private void Ribbon_Merge(object sender, RibbonMergeEventArgs e)
        {
            if (e.MergeOwner.MergedPages.Count > 0)
            {
                e.MergeOwner.SelectedPage = e.MergeOwner.MergedPages[0];
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!mvvmContext.IsDesignMode)
            {
                var fluent = mvvmContext.OfType<MainViewModel>();
                fluent.ViewModel.Load();
            }
        }

        protected void InitializeBindings()
        {
            var fluent = mvvmContext.OfType<MainViewModel>();

            fluent.BindCommand(bbiInfo, m => m.ShowInfo());

            // Layout
            UserLookAndFeel.Default.StyleChanged += Default_StyleChanged;
            string skin = Properties.Settings.Default.Theme;
            if (!string.IsNullOrEmpty(skin))
            {
                UserLookAndFeel defaultLF = UserLookAndFeel.Default;
                defaultLF.SkinName = skin;
            }
        }

        private void Default_StyleChanged(object? sender, EventArgs e)
        {
            UserLookAndFeel defaultLF = UserLookAndFeel.Default;
            Properties.Settings.Default.Theme = defaultLF.SkinName;
            Properties.Settings.Default.Save();
        }



    }
}
