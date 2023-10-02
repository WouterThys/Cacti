using CactiClient.Model;
using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraVerticalGrid.Rows;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CactiClient
{
    public partial class MainView : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainView()
        {
            InitializeComponent();
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



        }



    }
}
