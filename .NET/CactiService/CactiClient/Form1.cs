using CactiClient.Model;
using DevExpress.Utils.MVVM.Services;
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
                InitializeServices();
                InitializeBindings();
            }
        }

        protected void InitializeServices()
        {
            //mvvmContext.RegisterService(DocumentManagerService.Create(tabbedView1));
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

        //protected override void OnFirstLoad()
        //{
        //    base.OnFirstLoad();
        //    ShowProgressPanel(true);
        //}

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

            fluent.SetTrigger(m => m.IsLoading, (loading) => ShowProgressPanel(loading));

            fluent.SetObjectDataSourceBinding(bsCacti, m => m.Cacti);

            fluent.WithEvent<TileViewItemClickEventArgs>(tileView, "ItemClick").SetBinding(
                m => m.Selection,
                e => new List<CactusView>(tileView.GetSelectedRows().Select(r => tileView.GetRow(r) as CactusView)));

            fluent.BindCommand(barButtonItem1, m => m.Add());
            fluent.BindCommand(barButtonItem2, m => m.Edit(null), m => m.Selected);
            fluent.BindCommand(barButtonItem3, m => m.Delete(null), m => m.Selected);

        }

        #region Loading Overlay

        private IOverlaySplashScreenHandle? handle = null;

        private void ShowProgressPanel(bool loading)
        {
            if (loading)
            {
                handle = SplashScreenManager.ShowOverlayForm(this);
            }
            else
            {
                if (handle != null)
                {
                    SplashScreenManager.CloseOverlayForm(handle);
                    handle = null;
                }
            }
        }


        #endregion

    }
}
