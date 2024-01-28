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
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition1 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition2 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition3 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition1 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition2 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition3 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition4 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition5 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableSpan tableSpan1 = new DevExpress.XtraEditors.TableLayout.TableSpan();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement2 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement3 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement4 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CactusListView));
            colImage = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colCode = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colDescription = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colLocation = new DevExpress.XtraGrid.Columns.TileViewColumn();
            gridControl = new DevExpress.XtraGrid.GridControl();
            bsCacti = new System.Windows.Forms.BindingSource(components);
            tileView = new DevExpress.XtraGrid.Views.Tile.TileView();
            colId = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colBarcodes = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colImageLoading = new DevExpress.XtraGrid.Columns.TileViewColumn();
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            bbiAdd = new DevExpress.XtraBars.BarButtonItem();
            bbiEdit = new DevExpress.XtraBars.BarButtonItem();
            bbiDelete = new DevExpress.XtraBars.BarButtonItem();
            bbiImport = new DevExpress.XtraBars.BarButtonItem();
            barButtonGroup1 = new DevExpress.XtraBars.BarButtonGroup();
            cciList = new DevExpress.XtraBars.BarCheckItem();
            ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(components);
            ((System.ComponentModel.ISupportInitialize)gridControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsCacti).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tileView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).BeginInit();
            SuspendLayout();
            // 
            // colImage
            // 
            colImage.FieldName = "Image";
            colImage.Name = "colImage";
            colImage.Visible = true;
            colImage.VisibleIndex = 5;
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
            tableColumnDefinition1.Length.Value = 140D;
            tableColumnDefinition2.Length.Value = 112D;
            tableColumnDefinition3.Length.Value = 170D;
            tileView.TileColumns.Add(tableColumnDefinition1);
            tileView.TileColumns.Add(tableColumnDefinition2);
            tileView.TileColumns.Add(tableColumnDefinition3);
            tableRowDefinition1.Length.Value = 57D;
            tableRowDefinition2.Length.Value = 45D;
            tableRowDefinition3.Length.Value = 44D;
            tableRowDefinition4.Length.Value = 42D;
            tableRowDefinition5.Length.Value = 48D;
            tileView.TileRows.Add(tableRowDefinition1);
            tileView.TileRows.Add(tableRowDefinition2);
            tileView.TileRows.Add(tableRowDefinition3);
            tileView.TileRows.Add(tableRowDefinition4);
            tileView.TileRows.Add(tableRowDefinition5);
            tableSpan1.ColumnSpan = 2;
            tableSpan1.RowSpan = 5;
            tileView.TileSpans.Add(tableSpan1);
            tileViewItemElement1.Column = colImage;
            tileViewItemElement1.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement1.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement1.StretchHorizontal = true;
            tileViewItemElement1.Text = "colImage";
            tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement1.TextLocation = new System.Drawing.Point(20, 0);
            tileViewItemElement2.AnchorIndent = 12;
            tileViewItemElement2.Appearance.Normal.FontSizeDelta = 2;
            tileViewItemElement2.Appearance.Normal.FontStyleDelta = System.Drawing.FontStyle.Bold;
            tileViewItemElement2.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement2.Column = colCode;
            tileViewItemElement2.ColumnIndex = 2;
            tileViewItemElement2.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement2.Text = "colCode";
            tileViewItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileViewItemElement2.TextLocation = new System.Drawing.Point(20, 0);
            tileViewItemElement3.Column = colDescription;
            tileViewItemElement3.ColumnIndex = 2;
            tileViewItemElement3.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement3.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement3.RowIndex = 1;
            tileViewItemElement3.Text = "colDescription";
            tileViewItemElement3.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileViewItemElement3.TextLocation = new System.Drawing.Point(20, 0);
            tileViewItemElement4.Column = colLocation;
            tileViewItemElement4.ColumnIndex = 2;
            tileViewItemElement4.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement4.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement4.RowIndex = 2;
            tileViewItemElement4.Text = "colLocation";
            tileViewItemElement4.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileViewItemElement4.TextLocation = new System.Drawing.Point(20, 0);
            tileView.TileTemplate.Add(tileViewItemElement1);
            tileView.TileTemplate.Add(tileViewItemElement2);
            tileView.TileTemplate.Add(tileViewItemElement3);
            tileView.TileTemplate.Add(tileViewItemElement4);
            // 
            // colId
            // 
            colId.FieldName = "Id";
            colId.Name = "colId";
            colId.Visible = true;
            colId.VisibleIndex = 0;
            // 
            // colBarcodes
            // 
            colBarcodes.FieldName = "Barcodes";
            colBarcodes.Name = "colBarcodes";
            colBarcodes.Visible = true;
            colBarcodes.VisibleIndex = 4;
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
            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, bbiAdd, bbiEdit, bbiDelete, bbiImport, barButtonGroup1, cciList });
            ribbonControl1.Location = new System.Drawing.Point(0, 0);
            ribbonControl1.MaxItemId = 12;
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
            // bbiImport
            // 
            bbiImport.Caption = "Import";
            bbiImport.Id = 4;
            bbiImport.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("bbiImport.ImageOptions.SvgImage");
            bbiImport.Name = "bbiImport";
            // 
            // barButtonGroup1
            // 
            barButtonGroup1.Caption = "barButtonGroup1";
            barButtonGroup1.Id = 6;
            barButtonGroup1.Name = "barButtonGroup1";
            // 
            // cciList
            // 
            cciList.Caption = "List";
            cciList.Id = 11;
            cciList.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("cciList.ImageOptions.SvgImage");
            cciList.Name = "cciList";
            // 
            // ribbonPage
            // 
            ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1, ribbonPageGroup2, ribbonPageGroup3 });
            ribbonPage.Name = "ribbonPage";
            ribbonPage.Text = "Cacti";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.ItemLinks.Add(bbiAdd);
            ribbonPageGroup1.ItemLinks.Add(bbiEdit);
            ribbonPageGroup1.ItemLinks.Add(bbiDelete);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "Thuis";
            // 
            // ribbonPageGroup2
            // 
            ribbonPageGroup2.ItemLinks.Add(bbiImport);
            ribbonPageGroup2.Name = "ribbonPageGroup2";
            ribbonPageGroup2.Text = "Data";
            // 
            // ribbonPageGroup3
            // 
            ribbonPageGroup3.ItemLinks.Add(barButtonGroup1);
            ribbonPageGroup3.ItemLinks.Add(cciList);
            ribbonPageGroup3.Name = "ribbonPageGroup3";
            ribbonPageGroup3.Text = "View";
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
        private DevExpress.XtraBars.BarButtonItem bbiImport;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup1;
        private DevExpress.XtraBars.BarCheckItem cciList;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
    }
}
