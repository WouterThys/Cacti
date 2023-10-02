namespace CactiClient.View.Cactus
{
    partial class CactusListView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition4 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition5 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition6 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition6 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition7 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition8 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition9 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition10 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableSpan tableSpan2 = new DevExpress.XtraEditors.TableLayout.TableSpan();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement5 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement6 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement7 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement8 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CactusListView));
            gridControl = new DevExpress.XtraGrid.GridControl();
            bsCacti = new System.Windows.Forms.BindingSource(components);
            tileView = new DevExpress.XtraGrid.Views.Tile.TileView();
            colId = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colCode = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colDescription = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colLocation = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colBarcodes = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colImage = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colImageLoading = new DevExpress.XtraGrid.Columns.TileViewColumn();
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            bbiAdd = new DevExpress.XtraBars.BarButtonItem();
            bbiEdit = new DevExpress.XtraBars.BarButtonItem();
            bbiDelete = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(components);
            ((System.ComponentModel.ISupportInitialize)gridControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsCacti).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tileView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).BeginInit();
            SuspendLayout();
            // 
            // gridControl
            // 
            gridControl.DataSource = bsCacti;
            gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControl.Location = new System.Drawing.Point(0, 150);
            gridControl.MainView = tileView;
            gridControl.Name = "gridControl";
            gridControl.Size = new System.Drawing.Size(914, 378);
            gridControl.TabIndex = 0;
            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { tileView });
            // 
            // bsCacti
            // 
            bsCacti.DataSource = typeof(Model.CactusView);
            // 
            // tileView
            // 
            tileView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colId, colCode, colDescription, colLocation, colBarcodes, colImage, colImageLoading });
            tileView.GridControl = gridControl;
            tileView.Name = "tileView";
            tileView.OptionsTiles.ItemSize = new System.Drawing.Size(446, 226);
            tileView.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            tileView.OptionsTiles.RowCount = 0;
            tableColumnDefinition4.Length.Value = 140D;
            tableColumnDefinition5.Length.Value = 112D;
            tableColumnDefinition6.Length.Value = 170D;
            tileView.TileColumns.Add(tableColumnDefinition4);
            tileView.TileColumns.Add(tableColumnDefinition5);
            tileView.TileColumns.Add(tableColumnDefinition6);
            tableRowDefinition6.Length.Value = 57D;
            tableRowDefinition7.Length.Value = 45D;
            tableRowDefinition8.Length.Value = 44D;
            tableRowDefinition9.Length.Value = 42D;
            tableRowDefinition10.Length.Value = 48D;
            tileView.TileRows.Add(tableRowDefinition6);
            tileView.TileRows.Add(tableRowDefinition7);
            tileView.TileRows.Add(tableRowDefinition8);
            tileView.TileRows.Add(tableRowDefinition9);
            tileView.TileRows.Add(tableRowDefinition10);
            tableSpan2.ColumnSpan = 2;
            tableSpan2.RowSpan = 5;
            tileView.TileSpans.Add(tableSpan2);
            tileViewItemElement5.Column = colImage;
            tileViewItemElement5.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement5.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement5.Text = "colImage";
            tileViewItemElement5.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement6.Appearance.Normal.FontSizeDelta = 2;
            tileViewItemElement6.Appearance.Normal.FontStyleDelta = System.Drawing.FontStyle.Bold;
            tileViewItemElement6.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement6.Column = colCode;
            tileViewItemElement6.ColumnIndex = 2;
            tileViewItemElement6.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement6.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement6.Text = "colCode";
            tileViewItemElement6.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileViewItemElement7.Column = colDescription;
            tileViewItemElement7.ColumnIndex = 2;
            tileViewItemElement7.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement7.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement7.RowIndex = 1;
            tileViewItemElement7.Text = "colDescription";
            tileViewItemElement7.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileViewItemElement8.Column = colLocation;
            tileViewItemElement8.ColumnIndex = 2;
            tileViewItemElement8.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement8.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement8.RowIndex = 2;
            tileViewItemElement8.Text = "colLocation";
            tileViewItemElement8.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileView.TileTemplate.Add(tileViewItemElement5);
            tileView.TileTemplate.Add(tileViewItemElement6);
            tileView.TileTemplate.Add(tileViewItemElement7);
            tileView.TileTemplate.Add(tileViewItemElement8);
            // 
            // colId
            // 
            colId.FieldName = "Id";
            colId.Name = "colId";
            colId.Visible = true;
            colId.VisibleIndex = 0;
            // 
            // colCode
            // 
            colCode.FieldName = "Code";
            colCode.Name = "colCode";
            colCode.Visible = true;
            colCode.VisibleIndex = 1;
            // 
            // colDescription
            // 
            colDescription.FieldName = "Description";
            colDescription.Name = "colDescription";
            colDescription.Visible = true;
            colDescription.VisibleIndex = 2;
            // 
            // colLocation
            // 
            colLocation.FieldName = "Location";
            colLocation.Name = "colLocation";
            colLocation.Visible = true;
            colLocation.VisibleIndex = 3;
            // 
            // colBarcodes
            // 
            colBarcodes.FieldName = "Barcodes";
            colBarcodes.Name = "colBarcodes";
            colBarcodes.Visible = true;
            colBarcodes.VisibleIndex = 4;
            // 
            // colImage
            // 
            colImage.FieldName = "Image";
            colImage.Name = "colImage";
            colImage.Visible = true;
            colImage.VisibleIndex = 5;
            // 
            // colImageLoading
            // 
            colImageLoading.FieldName = "ImageLoading";
            colImageLoading.Name = "colImageLoading";
            colImageLoading.Visible = true;
            colImageLoading.VisibleIndex = 6;
            // 
            // ribbonControl1
            // 
            ribbonControl1.ExpandCollapseItem.Id = 0;
            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, bbiAdd, bbiEdit, bbiDelete });
            ribbonControl1.Location = new System.Drawing.Point(0, 0);
            ribbonControl1.MaxItemId = 4;
            ribbonControl1.Name = "ribbonControl1";
            ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage });
            ribbonControl1.Size = new System.Drawing.Size(914, 150);
            // 
            // bbiAdd
            // 
            bbiAdd.Caption = "Add";
            bbiAdd.Id = 1;
            bbiAdd.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("bbiAdd.ImageOptions.SvgImage");
            bbiAdd.Name = "bbiAdd";
            // 
            // bbiEdit
            // 
            bbiEdit.Caption = "Edit";
            bbiEdit.Id = 2;
            bbiEdit.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("bbiEdit.ImageOptions.SvgImage");
            bbiEdit.Name = "bbiEdit";
            // 
            // bbiDelete
            // 
            bbiDelete.Caption = "Delete";
            bbiDelete.Id = 3;
            bbiDelete.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("bbiDelete.ImageOptions.SvgImage");
            bbiDelete.Name = "bbiDelete";
            // 
            // ribbonPage
            // 
            ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1 });
            ribbonPage.Name = "ribbonPage";
            ribbonPage.Text = "Thuis";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.ItemLinks.Add(bbiAdd);
            ribbonPageGroup1.ItemLinks.Add(bbiEdit);
            ribbonPageGroup1.ItemLinks.Add(bbiDelete);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "Thuis";
            // 
            // mvvmContext
            // 
            mvvmContext.ContainerControl = this;
            mvvmContext.ViewModelType = typeof(ViewModel.Cactus.CactusListViewModel);
            // 
            // CactusListView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gridControl);
            Controls.Add(ribbonControl1);
            Name = "CactusListView";
            Size = new System.Drawing.Size(914, 528);
            ((System.ComponentModel.ISupportInitialize)gridControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsCacti).EndInit();
            ((System.ComponentModel.ISupportInitialize)tileView).EndInit();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Tile.TileView tileView;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem bbiAdd;
        private DevExpress.XtraBars.BarButtonItem bbiEdit;
        private DevExpress.XtraBars.BarButtonItem bbiDelete;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private System.Windows.Forms.BindingSource bsCacti;
        private DevExpress.XtraGrid.Columns.TileViewColumn colId;
        private DevExpress.XtraGrid.Columns.TileViewColumn colCode;
        private DevExpress.XtraGrid.Columns.TileViewColumn colDescription;
        private DevExpress.XtraGrid.Columns.TileViewColumn colLocation;
        private DevExpress.XtraGrid.Columns.TileViewColumn colBarcodes;
        private DevExpress.XtraGrid.Columns.TileViewColumn colImage;
        private DevExpress.XtraGrid.Columns.TileViewColumn colImageLoading;
    }
}
