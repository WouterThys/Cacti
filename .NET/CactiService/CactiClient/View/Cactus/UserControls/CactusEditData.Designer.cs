namespace CactiClient.View.Cactus
{
    partial class CactusEditData
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
            mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(components);
            bsCactus = new System.Windows.Forms.BindingSource(components);
            dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            CodeTextEdit = new DevExpress.XtraEditors.TextEdit();
            DescriptionTextEdit = new DevExpress.XtraEditors.TextEdit();
            LocationTextEdit = new DevExpress.XtraEditors.TextEdit();
            ImagePictureEdit = new DevExpress.XtraEditors.PictureEdit();
            BarcodePictureEdit = new DevExpress.XtraEditors.PictureEdit();
            InfoTextEdit = new DevExpress.XtraEditors.TextEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            ItemForCode = new DevExpress.XtraLayout.LayoutControlItem();
            ItemForDescription = new DevExpress.XtraLayout.LayoutControlItem();
            ItemForLocation = new DevExpress.XtraLayout.LayoutControlItem();
            ItemForImage = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ItemForInfo = new DevExpress.XtraLayout.LayoutControlItem();
            ItemForBarcode = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            FathersCodeTextEdit = new DevExpress.XtraEditors.TextEdit();
            ItemForFathersCode = new DevExpress.XtraLayout.LayoutControlItem();
            MothersCodeTextEdit = new DevExpress.XtraEditors.TextEdit();
            ItemForMothersCode = new DevExpress.XtraLayout.LayoutControlItem();
            CrossingNumberTextEdit = new DevExpress.XtraEditors.TextEdit();
            ItemForCrossingNumber = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsCactus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataLayoutControl1).BeginInit();
            dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CodeTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DescriptionTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LocationTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ImagePictureEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BarcodePictureEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)InfoTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForCode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForDescription).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForLocation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForInfo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForBarcode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FathersCodeTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForFathersCode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MothersCodeTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForMothersCode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CrossingNumberTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForCrossingNumber).BeginInit();
            SuspendLayout();
            // 
            // mvvmContext
            // 
            mvvmContext.ContainerControl = this;
            // 
            // bsCactus
            // 
            bsCactus.DataSource = typeof(Model.CactusView);
            // 
            // dataLayoutControl1
            // 
            dataLayoutControl1.Controls.Add(CodeTextEdit);
            dataLayoutControl1.Controls.Add(DescriptionTextEdit);
            dataLayoutControl1.Controls.Add(LocationTextEdit);
            dataLayoutControl1.Controls.Add(ImagePictureEdit);
            dataLayoutControl1.Controls.Add(BarcodePictureEdit);
            dataLayoutControl1.Controls.Add(InfoTextEdit);
            dataLayoutControl1.Controls.Add(FathersCodeTextEdit);
            dataLayoutControl1.Controls.Add(MothersCodeTextEdit);
            dataLayoutControl1.Controls.Add(CrossingNumberTextEdit);
            dataLayoutControl1.DataSource = bsCactus;
            dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            dataLayoutControl1.Name = "dataLayoutControl1";
            dataLayoutControl1.Root = Root;
            dataLayoutControl1.Size = new System.Drawing.Size(439, 330);
            dataLayoutControl1.TabIndex = 0;
            dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // CodeTextEdit
            // 
            CodeTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bsCactus, "Code", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            CodeTextEdit.Location = new System.Drawing.Point(280, 12);
            CodeTextEdit.Name = "CodeTextEdit";
            CodeTextEdit.Size = new System.Drawing.Size(147, 20);
            CodeTextEdit.StyleController = dataLayoutControl1;
            CodeTextEdit.TabIndex = 0;
            // 
            // DescriptionTextEdit
            // 
            DescriptionTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bsCactus, "Description", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            DescriptionTextEdit.Location = new System.Drawing.Point(280, 36);
            DescriptionTextEdit.Name = "DescriptionTextEdit";
            DescriptionTextEdit.Size = new System.Drawing.Size(147, 20);
            DescriptionTextEdit.StyleController = dataLayoutControl1;
            DescriptionTextEdit.TabIndex = 2;
            // 
            // LocationTextEdit
            // 
            LocationTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bsCactus, "Location", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            LocationTextEdit.Location = new System.Drawing.Point(280, 60);
            LocationTextEdit.Name = "LocationTextEdit";
            LocationTextEdit.Size = new System.Drawing.Size(147, 20);
            LocationTextEdit.StyleController = dataLayoutControl1;
            LocationTextEdit.TabIndex = 3;
            // 
            // ImagePictureEdit
            // 
            ImagePictureEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bsCactus, "Image", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            ImagePictureEdit.Location = new System.Drawing.Point(12, 12);
            ImagePictureEdit.Name = "ImagePictureEdit";
            ImagePictureEdit.Properties.ShowEditMenuItem = DevExpress.Utils.DefaultBoolean.False;
            ImagePictureEdit.Properties.ShowMenu = false;
            ImagePictureEdit.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.False;
            ImagePictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            ImagePictureEdit.Size = new System.Drawing.Size(168, 284);
            ImagePictureEdit.StyleController = dataLayoutControl1;
            ImagePictureEdit.TabIndex = 1;
            // 
            // BarcodePictureEdit
            // 
            BarcodePictureEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bsCactus, "Barcode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            BarcodePictureEdit.Location = new System.Drawing.Point(353, 225);
            BarcodePictureEdit.Name = "BarcodePictureEdit";
            BarcodePictureEdit.Properties.ShowEditMenuItem = DevExpress.Utils.DefaultBoolean.False;
            BarcodePictureEdit.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.False;
            BarcodePictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            BarcodePictureEdit.Size = new System.Drawing.Size(74, 71);
            BarcodePictureEdit.StyleController = dataLayoutControl1;
            BarcodePictureEdit.TabIndex = 1;
            // 
            // InfoTextEdit
            // 
            InfoTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bsCactus, "Info", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            InfoTextEdit.Location = new System.Drawing.Point(12, 300);
            InfoTextEdit.Name = "InfoTextEdit";
            InfoTextEdit.Properties.Appearance.FontSizeDelta = -1;
            InfoTextEdit.Properties.Appearance.ForeColor = System.Drawing.Color.Silver;
            InfoTextEdit.Properties.Appearance.Options.UseFont = true;
            InfoTextEdit.Properties.Appearance.Options.UseForeColor = true;
            InfoTextEdit.Properties.ReadOnly = true;
            InfoTextEdit.Size = new System.Drawing.Size(415, 18);
            InfoTextEdit.StyleController = dataLayoutControl1;
            InfoTextEdit.TabIndex = 7;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(439, 330);
            Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.AllowDrawBackground = false;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { ItemForCode, ItemForDescription, ItemForLocation, ItemForImage, emptySpaceItem1, ItemForInfo, ItemForBarcode, emptySpaceItem4, ItemForFathersCode, ItemForMothersCode, ItemForCrossingNumber });
            layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup1.Name = "autoGeneratedGroup0";
            layoutControlGroup1.Size = new System.Drawing.Size(419, 310);
            // 
            // ItemForCode
            // 
            ItemForCode.Control = CodeTextEdit;
            ItemForCode.Location = new System.Drawing.Point(172, 0);
            ItemForCode.Name = "ItemForCode";
            ItemForCode.Size = new System.Drawing.Size(247, 24);
            ItemForCode.Text = "Naam";
            ItemForCode.TextSize = new System.Drawing.Size(84, 13);
            // 
            // ItemForDescription
            // 
            ItemForDescription.Control = DescriptionTextEdit;
            ItemForDescription.Location = new System.Drawing.Point(172, 24);
            ItemForDescription.Name = "ItemForDescription";
            ItemForDescription.Size = new System.Drawing.Size(247, 24);
            ItemForDescription.Text = "Omschrijving";
            ItemForDescription.TextSize = new System.Drawing.Size(84, 13);
            // 
            // ItemForLocation
            // 
            ItemForLocation.Control = LocationTextEdit;
            ItemForLocation.Location = new System.Drawing.Point(172, 48);
            ItemForLocation.Name = "ItemForLocation";
            ItemForLocation.Size = new System.Drawing.Size(247, 24);
            ItemForLocation.Text = "Lokatie";
            ItemForLocation.TextSize = new System.Drawing.Size(84, 13);
            // 
            // ItemForImage
            // 
            ItemForImage.Control = ImagePictureEdit;
            ItemForImage.Location = new System.Drawing.Point(0, 0);
            ItemForImage.Name = "ItemForImage";
            ItemForImage.Size = new System.Drawing.Size(172, 288);
            ItemForImage.StartNewLine = true;
            ItemForImage.Text = "Image";
            ItemForImage.TextSize = new System.Drawing.Size(0, 0);
            ItemForImage.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new System.Drawing.Point(172, 144);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(247, 69);
            emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ItemForInfo
            // 
            ItemForInfo.Control = InfoTextEdit;
            ItemForInfo.Location = new System.Drawing.Point(0, 288);
            ItemForInfo.Name = "ItemForInfo";
            ItemForInfo.Size = new System.Drawing.Size(419, 22);
            ItemForInfo.Text = "Info";
            ItemForInfo.TextSize = new System.Drawing.Size(0, 0);
            ItemForInfo.TextVisible = false;
            // 
            // ItemForBarcode
            // 
            ItemForBarcode.Control = BarcodePictureEdit;
            ItemForBarcode.Location = new System.Drawing.Point(341, 213);
            ItemForBarcode.Name = "ItemForBarcode";
            ItemForBarcode.Size = new System.Drawing.Size(78, 75);
            ItemForBarcode.StartNewLine = true;
            ItemForBarcode.Text = "Barcode";
            ItemForBarcode.TextSize = new System.Drawing.Size(0, 0);
            ItemForBarcode.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            emptySpaceItem4.AllowHotTrack = false;
            emptySpaceItem4.Location = new System.Drawing.Point(172, 213);
            emptySpaceItem4.Name = "emptySpaceItem4";
            emptySpaceItem4.Size = new System.Drawing.Size(169, 75);
            emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FathersCodeTextEdit
            // 
            FathersCodeTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bsCactus, "FathersCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            FathersCodeTextEdit.Location = new System.Drawing.Point(280, 84);
            FathersCodeTextEdit.Name = "FathersCodeTextEdit";
            FathersCodeTextEdit.Size = new System.Drawing.Size(147, 20);
            FathersCodeTextEdit.StyleController = dataLayoutControl1;
            FathersCodeTextEdit.TabIndex = 4;
            // 
            // ItemForFathersCode
            // 
            ItemForFathersCode.Control = FathersCodeTextEdit;
            ItemForFathersCode.Location = new System.Drawing.Point(172, 72);
            ItemForFathersCode.Name = "ItemForFathersCode";
            ItemForFathersCode.Size = new System.Drawing.Size(247, 24);
            ItemForFathersCode.Text = "Naam Vader";
            ItemForFathersCode.TextSize = new System.Drawing.Size(84, 13);
            // 
            // MothersCodeTextEdit
            // 
            MothersCodeTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bsCactus, "MothersCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            MothersCodeTextEdit.Location = new System.Drawing.Point(280, 108);
            MothersCodeTextEdit.Name = "MothersCodeTextEdit";
            MothersCodeTextEdit.Size = new System.Drawing.Size(147, 20);
            MothersCodeTextEdit.StyleController = dataLayoutControl1;
            MothersCodeTextEdit.TabIndex = 5;
            // 
            // ItemForMothersCode
            // 
            ItemForMothersCode.Control = MothersCodeTextEdit;
            ItemForMothersCode.Location = new System.Drawing.Point(172, 96);
            ItemForMothersCode.Name = "ItemForMothersCode";
            ItemForMothersCode.Size = new System.Drawing.Size(247, 24);
            ItemForMothersCode.Text = "Naam Moeder";
            ItemForMothersCode.TextSize = new System.Drawing.Size(84, 13);
            // 
            // CrossingNumberTextEdit
            // 
            CrossingNumberTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bsCactus, "CrossingNumber", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            CrossingNumberTextEdit.Location = new System.Drawing.Point(280, 132);
            CrossingNumberTextEdit.Name = "CrossingNumberTextEdit";
            CrossingNumberTextEdit.Size = new System.Drawing.Size(147, 20);
            CrossingNumberTextEdit.StyleController = dataLayoutControl1;
            CrossingNumberTextEdit.TabIndex = 6;
            // 
            // ItemForCrossingNumber
            // 
            ItemForCrossingNumber.Control = CrossingNumberTextEdit;
            ItemForCrossingNumber.Location = new System.Drawing.Point(172, 120);
            ItemForCrossingNumber.Name = "ItemForCrossingNumber";
            ItemForCrossingNumber.Size = new System.Drawing.Size(247, 24);
            ItemForCrossingNumber.Text = "Kruisings Nummer";
            ItemForCrossingNumber.TextSize = new System.Drawing.Size(84, 13);
            // 
            // CactusEditData
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(dataLayoutControl1);
            Name = "CactusEditData";
            Size = new System.Drawing.Size(439, 330);
            ((System.ComponentModel.ISupportInitialize)mvvmContext).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsCactus).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataLayoutControl1).EndInit();
            dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)CodeTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)DescriptionTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)LocationTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ImagePictureEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)BarcodePictureEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)InfoTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForCode).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForDescription).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForLocation).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForInfo).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForBarcode).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)FathersCodeTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForFathersCode).EndInit();
            ((System.ComponentModel.ISupportInitialize)MothersCodeTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForMothersCode).EndInit();
            ((System.ComponentModel.ISupportInitialize)CrossingNumberTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForCrossingNumber).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.TextEdit CodeTextEdit;
        private DevExpress.XtraEditors.TextEdit DescriptionTextEdit;
        private DevExpress.XtraEditors.TextEdit LocationTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForCode;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDescription;
        private DevExpress.XtraLayout.LayoutControlItem ItemForLocation;
        public System.Windows.Forms.BindingSource bsCactus;
        private DevExpress.XtraEditors.PictureEdit ImagePictureEdit;
        private DevExpress.XtraLayout.LayoutControlItem ItemForImage;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.PictureEdit BarcodePictureEdit;
        private DevExpress.XtraLayout.LayoutControlItem ItemForBarcode;
        private DevExpress.XtraEditors.TextEdit InfoTextEdit;
        private DevExpress.XtraLayout.LayoutControlItem ItemForInfo;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.TextEdit FathersCodeTextEdit;
        private DevExpress.XtraEditors.TextEdit MothersCodeTextEdit;
        private DevExpress.XtraEditors.TextEdit CrossingNumberTextEdit;
        private DevExpress.XtraLayout.LayoutControlItem ItemForFathersCode;
        private DevExpress.XtraLayout.LayoutControlItem ItemForMothersCode;
        private DevExpress.XtraLayout.LayoutControlItem ItemForCrossingNumber;
    }
}
