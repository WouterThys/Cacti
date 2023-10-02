namespace CactiClient
{
    partial class MainView
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

        #region Windows Form Designer generated code

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(components);
            gridControl = new DevExpress.XtraGrid.GridControl();
            bsCacti = new System.Windows.Forms.BindingSource(components);
            tileView = new DevExpress.XtraGrid.Views.Tile.TileView();
            colId = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colCode = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colDescription = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colLocation = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colBarcodes = new DevExpress.XtraGrid.Columns.TileViewColumn();
            colImage = new DevExpress.XtraGrid.Columns.TileViewColumn();
            barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsCacti).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tileView).BeginInit();
            SuspendLayout();
            // 
            // ribbonControl1
            // 
            ribbonControl1.ExpandCollapseItem.Id = 0;
            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, barButtonItem1, barButtonItem2, barButtonItem3 });
            ribbonControl1.Location = new System.Drawing.Point(0, 0);
            ribbonControl1.MaxItemId = 4;
            ribbonControl1.Name = "ribbonControl1";
            ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
            ribbonControl1.Size = new System.Drawing.Size(758, 158);
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1 });
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.ItemLinks.Add(barButtonItem1);
            ribbonPageGroup1.ItemLinks.Add(barButtonItem2);
            ribbonPageGroup1.ItemLinks.Add(barButtonItem3);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "HOME";
            // 
            // mvvmContext
            // 
            mvvmContext.ContainerControl = this;
            mvvmContext.ViewModelType = typeof(MainViewModel);
            // 
            // gridControl
            // 
            gridControl.DataSource = bsCacti;
            gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControl.Location = new System.Drawing.Point(0, 158);
            gridControl.MainView = tileView;
            gridControl.MenuManager = ribbonControl1;
            gridControl.Name = "gridControl";
            gridControl.Size = new System.Drawing.Size(758, 202);
            gridControl.TabIndex = 1;
            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { tileView });
            // 
            // bsCacti
            // 
            bsCacti.DataSource = typeof(Model.CactusView);
            // 
            // tileView
            // 
            tileView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colId, colCode, colDescription, colLocation, colBarcodes, colImage });
            tileView.GridControl = gridControl;
            tileView.Name = "tileView";
            tileView.OptionsTiles.ItemSize = new System.Drawing.Size(316, 120);
            tileView.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            tileView.OptionsTiles.RowCount = 0;
            tableColumnDefinition1.Length.Value = 97D;
            tableColumnDefinition2.Length.Value = 45D;
            tableColumnDefinition3.Length.Value = 150D;
            tileView.TileColumns.Add(tableColumnDefinition1);
            tileView.TileColumns.Add(tableColumnDefinition2);
            tileView.TileColumns.Add(tableColumnDefinition3);
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
            tileViewItemElement1.Text = "colImage";
            tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.Appearance.Normal.FontSizeDelta = 1;
            tileViewItemElement2.Appearance.Normal.FontStyleDelta = System.Drawing.FontStyle.Bold;
            tileViewItemElement2.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement2.Column = colCode;
            tileViewItemElement2.ColumnIndex = 2;
            tileViewItemElement2.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement2.Text = "colCode";
            tileViewItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileViewItemElement3.Column = colDescription;
            tileViewItemElement3.ColumnIndex = 2;
            tileViewItemElement3.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement3.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement3.RowIndex = 1;
            tileViewItemElement3.Text = "colDescription";
            tileViewItemElement3.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileViewItemElement4.Column = colLocation;
            tileViewItemElement4.ColumnIndex = 2;
            tileViewItemElement4.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement4.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement4.RowIndex = 2;
            tileViewItemElement4.Text = "colLocation";
            tileViewItemElement4.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopLeft;
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
            // barButtonItem1
            // 
            barButtonItem1.Caption = "Add";
            barButtonItem1.Id = 1;
            barButtonItem1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem1.ImageOptions.SvgImage");
            barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            barButtonItem2.Caption = "Edit";
            barButtonItem2.Id = 2;
            barButtonItem2.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem2.ImageOptions.SvgImage");
            barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            barButtonItem3.Caption = "Delete";
            barButtonItem3.Id = 3;
            barButtonItem3.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem3.ImageOptions.SvgImage");
            barButtonItem3.Name = "barButtonItem3";
            // 
            // MainView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(758, 360);
            Controls.Add(gridControl);
            Controls.Add(ribbonControl1);
            Name = "MainView";
            Ribbon = ribbonControl1;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsCacti).EndInit();
            ((System.ComponentModel.ISupportInitialize)tileView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Tile.TileView tileView;
        private System.Windows.Forms.BindingSource bsCacti;
        private DevExpress.XtraGrid.Columns.TileViewColumn colId;
        private DevExpress.XtraGrid.Columns.TileViewColumn colCode;
        private DevExpress.XtraGrid.Columns.TileViewColumn colDescription;
        private DevExpress.XtraGrid.Columns.TileViewColumn colLocation;
        private DevExpress.XtraGrid.Columns.TileViewColumn colBarcodes;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraGrid.Columns.TileViewColumn colImage;
    }
}

