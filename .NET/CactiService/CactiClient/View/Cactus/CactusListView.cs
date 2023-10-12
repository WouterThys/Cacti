using CactiClient.Model;
using CactiClient.ViewModel.Cactus;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraSplashScreen;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace CactiClient.View.Cactus
{
    public partial class CactusListView : DevExpress.XtraEditors.XtraUserControl
    {
        public CactusListView()
        {
            InitializeComponent();

            if (!mvvmContext.IsDesignMode)
            {
                InitializeLayouts();
                InitializeBindings();
            }
        }

        private void InitializeLayouts()
        {
            tileView.OptionsFind.AlwaysVisible = true;
            tileView.OptionsBehavior.EditingMode = TileViewEditingMode.Disabled;

            barButtonItem1.ItemClick += BarButtonItem1_ItemClick;
        }

        private bool tst = false;

        private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tst)
            {
                tst = false;
            }
            else
            {
                tst = true;
            }
        }

        protected void InitializeBindings()
        {
            var fluent = mvvmContext.OfType<CactusListViewModel>();

            fluent.SetTrigger(m => m.IsLoading, (loading) => ShowProgressPanel(loading));

            fluent.SetObjectDataSourceBinding(bsCacti, m => m.Cacti);

            fluent.WithEvent<TileViewItemClickEventArgs>(tileView, "ItemClick").SetBinding(
                m => m.Selection,
                e => new List<CactusView>(tileView.GetSelectedRows().Select(r => tileView.GetRow(r) as CactusView)));

            fluent.WithEvent<TileViewItemClickEventArgs>(tileView, "ItemDoubleClick").EventToCommand(
                m => m.Edit(null),
                m => m.Selected);

            fluent.BindCommand(bbiAdd, m => m.Add());
            fluent.BindCommand(bbiEdit, m => m.Edit(null), m => m.Selected);
            fluent.BindCommand(bbiDelete, m => m.Delete(null), m => m.Selected);
            fluent.BindCommand(bbiImport, m => m.Import());

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
