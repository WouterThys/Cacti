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
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            ItemForCode = new DevExpress.XtraLayout.LayoutControlItem();
            ItemForDescription = new DevExpress.XtraLayout.LayoutControlItem();
            ItemForLocation = new DevExpress.XtraLayout.LayoutControlItem();
            ImagePictureEdit = new DevExpress.XtraEditors.PictureEdit();
            ItemForImage = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsCactus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataLayoutControl1).BeginInit();
            dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CodeTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DescriptionTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LocationTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForCode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForDescription).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForLocation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ImagePictureEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
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
            dataLayoutControl1.DataSource = bsCactus;
            dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            dataLayoutControl1.Name = "dataLayoutControl1";
            dataLayoutControl1.Root = Root;
            dataLayoutControl1.Size = new System.Drawing.Size(359, 408);
            dataLayoutControl1.TabIndex = 0;
            dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // CodeTextEdit
            // 
            CodeTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bsCactus, "Code", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            CodeTextEdit.Location = new System.Drawing.Point(232, 12);
            CodeTextEdit.Name = "CodeTextEdit";
            CodeTextEdit.Size = new System.Drawing.Size(115, 20);
            CodeTextEdit.StyleController = dataLayoutControl1;
            CodeTextEdit.TabIndex = 0;
            // 
            // DescriptionTextEdit
            // 
            DescriptionTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bsCactus, "Description", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            DescriptionTextEdit.Location = new System.Drawing.Point(232, 36);
            DescriptionTextEdit.Name = "DescriptionTextEdit";
            DescriptionTextEdit.Size = new System.Drawing.Size(115, 20);
            DescriptionTextEdit.StyleController = dataLayoutControl1;
            DescriptionTextEdit.TabIndex = 2;
            // 
            // LocationTextEdit
            // 
            LocationTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bsCactus, "Location", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            LocationTextEdit.Location = new System.Drawing.Point(232, 60);
            LocationTextEdit.Name = "LocationTextEdit";
            LocationTextEdit.Size = new System.Drawing.Size(115, 20);
            LocationTextEdit.StyleController = dataLayoutControl1;
            LocationTextEdit.TabIndex = 3;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(359, 408);
            Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.AllowDrawBackground = false;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { ItemForCode, ItemForDescription, ItemForLocation, ItemForImage, emptySpaceItem1, emptySpaceItem2 });
            layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup1.Name = "autoGeneratedGroup0";
            layoutControlGroup1.Size = new System.Drawing.Size(339, 388);
            // 
            // ItemForCode
            // 
            ItemForCode.Control = CodeTextEdit;
            ItemForCode.Location = new System.Drawing.Point(155, 0);
            ItemForCode.Name = "ItemForCode";
            ItemForCode.Size = new System.Drawing.Size(184, 24);
            ItemForCode.Text = "Code";
            ItemForCode.TextSize = new System.Drawing.Size(53, 13);
            // 
            // ItemForDescription
            // 
            ItemForDescription.Control = DescriptionTextEdit;
            ItemForDescription.Location = new System.Drawing.Point(155, 24);
            ItemForDescription.Name = "ItemForDescription";
            ItemForDescription.Size = new System.Drawing.Size(184, 24);
            ItemForDescription.Text = "Description";
            ItemForDescription.TextSize = new System.Drawing.Size(53, 13);
            // 
            // ItemForLocation
            // 
            ItemForLocation.Control = LocationTextEdit;
            ItemForLocation.Location = new System.Drawing.Point(155, 48);
            ItemForLocation.Name = "ItemForLocation";
            ItemForLocation.Size = new System.Drawing.Size(184, 24);
            ItemForLocation.Text = "Location";
            ItemForLocation.TextSize = new System.Drawing.Size(53, 13);
            // 
            // ImagePictureEdit
            // 
            ImagePictureEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bsCactus, "Image", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            ImagePictureEdit.Location = new System.Drawing.Point(12, 12);
            ImagePictureEdit.Name = "ImagePictureEdit";
            ImagePictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            ImagePictureEdit.Size = new System.Drawing.Size(151, 148);
            ImagePictureEdit.StyleController = dataLayoutControl1;
            ImagePictureEdit.TabIndex = 1;
            // 
            // ItemForImage
            // 
            ItemForImage.Control = ImagePictureEdit;
            ItemForImage.Location = new System.Drawing.Point(0, 0);
            ItemForImage.Name = "ItemForImage";
            ItemForImage.Size = new System.Drawing.Size(155, 152);
            ItemForImage.StartNewLine = true;
            ItemForImage.Text = "Image";
            ItemForImage.TextSize = new System.Drawing.Size(0, 0);
            ItemForImage.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new System.Drawing.Point(155, 72);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(184, 80);
            emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.AllowHotTrack = false;
            emptySpaceItem2.Location = new System.Drawing.Point(0, 152);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new System.Drawing.Size(339, 236);
            emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // CactusEditData
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(dataLayoutControl1);
            Name = "CactusEditData";
            Size = new System.Drawing.Size(359, 408);
            ((System.ComponentModel.ISupportInitialize)mvvmContext).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsCactus).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataLayoutControl1).EndInit();
            dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)CodeTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)DescriptionTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)LocationTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForCode).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForDescription).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForLocation).EndInit();
            ((System.ComponentModel.ISupportInitialize)ImagePictureEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
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
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}
