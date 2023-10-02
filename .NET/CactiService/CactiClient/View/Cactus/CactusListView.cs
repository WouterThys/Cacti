using CactiClient.Model;
using CactiClient.ViewModel.Cactus;
using DevExpress.Utils.MVVM;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraSplashScreen;
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
    public partial class CactusListView : DevExpress.XtraEditors.XtraUserControl
    {
        public CactusListView()
        {
            InitializeComponent();

            if (!mvvmContext.IsDesignMode)
            {
                InitializeBindings();
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

            fluent.BindCommand(bbiAdd, m => m.Add());
            fluent.BindCommand(bbiEdit, m => m.Edit(null), m => m.Selected);
            fluent.BindCommand(bbiDelete, m => m.Delete(null), m => m.Selected);

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
