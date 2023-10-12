namespace CactiClient.View.Cactus
{
    partial class ImportCactiView
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
            dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            StepTextEdit = new DevExpress.XtraEditors.TextEdit();
            bindingSource = new System.Windows.Forms.BindingSource(components);
            InfoTextEdit = new DevExpress.XtraEditors.TextEdit();
            MajorProgressProgressBarControl = new DevExpress.XtraEditors.ProgressBarControl();
            MinorProgressProgressBarControl = new DevExpress.XtraEditors.ProgressBarControl();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            ItemForStep = new DevExpress.XtraLayout.LayoutControlItem();
            ItemForMajorProgress = new DevExpress.XtraLayout.LayoutControlItem();
            ItemForMinorProgress = new DevExpress.XtraLayout.LayoutControlItem();
            ItemForInfo = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            DescriptionTextEdit = new DevExpress.XtraEditors.TextEdit();
            ItemForDescription = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataLayoutControl1).BeginInit();
            dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)StepTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)InfoTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MajorProgressProgressBarControl.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MinorProgressProgressBarControl.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForStep).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForMajorProgress).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForMinorProgress).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForInfo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DescriptionTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForDescription).BeginInit();
            SuspendLayout();
            // 
            // mvvmContext
            // 
            mvvmContext.ContainerControl = this;
            mvvmContext.ViewModelType = typeof(ViewModel.Cactus.ImportCactiViewModel);
            // 
            // dataLayoutControl1
            // 
            dataLayoutControl1.Controls.Add(StepTextEdit);
            dataLayoutControl1.Controls.Add(InfoTextEdit);
            dataLayoutControl1.Controls.Add(MajorProgressProgressBarControl);
            dataLayoutControl1.Controls.Add(MinorProgressProgressBarControl);
            dataLayoutControl1.Controls.Add(DescriptionTextEdit);
            dataLayoutControl1.DataSource = bindingSource;
            dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            dataLayoutControl1.Name = "dataLayoutControl1";
            dataLayoutControl1.Root = Root;
            dataLayoutControl1.Size = new System.Drawing.Size(497, 263);
            dataLayoutControl1.TabIndex = 0;
            dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // StepTextEdit
            // 
            StepTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bindingSource, "Step", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            StepTextEdit.Location = new System.Drawing.Point(12, 12);
            StepTextEdit.Name = "StepTextEdit";
            StepTextEdit.Properties.ReadOnly = true;
            StepTextEdit.Size = new System.Drawing.Size(473, 20);
            StepTextEdit.StyleController = dataLayoutControl1;
            StepTextEdit.TabIndex = 0;
            // 
            // bindingSource
            // 
            bindingSource.DataSource = typeof(ViewModel.Cactus.ImportCactiViewModel);
            // 
            // InfoTextEdit
            // 
            InfoTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bindingSource, "Info", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            InfoTextEdit.Location = new System.Drawing.Point(12, 231);
            InfoTextEdit.Name = "InfoTextEdit";
            InfoTextEdit.Properties.Appearance.ForeColor = System.Drawing.Color.Gray;
            InfoTextEdit.Properties.Appearance.Options.UseForeColor = true;
            InfoTextEdit.Properties.ReadOnly = true;
            InfoTextEdit.Size = new System.Drawing.Size(473, 20);
            InfoTextEdit.StyleController = dataLayoutControl1;
            InfoTextEdit.TabIndex = 3;
            // 
            // MajorProgressProgressBarControl
            // 
            MajorProgressProgressBarControl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bindingSource, "MajorProgress", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            MajorProgressProgressBarControl.Location = new System.Drawing.Point(12, 60);
            MajorProgressProgressBarControl.Name = "MajorProgressProgressBarControl";
            MajorProgressProgressBarControl.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            MajorProgressProgressBarControl.Properties.AutoHeight = true;
            MajorProgressProgressBarControl.Properties.ShowTitle = true;
            MajorProgressProgressBarControl.Size = new System.Drawing.Size(473, 14);
            MajorProgressProgressBarControl.StyleController = dataLayoutControl1;
            MajorProgressProgressBarControl.TabIndex = 1;
            // 
            // MinorProgressProgressBarControl
            // 
            MinorProgressProgressBarControl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bindingSource, "MinorProgress", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            MinorProgressProgressBarControl.Location = new System.Drawing.Point(12, 79);
            MinorProgressProgressBarControl.Name = "MinorProgressProgressBarControl";
            MinorProgressProgressBarControl.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            MinorProgressProgressBarControl.Properties.AutoHeight = true;
            MinorProgressProgressBarControl.Properties.ShowTitle = true;
            MinorProgressProgressBarControl.Size = new System.Drawing.Size(473, 14);
            MinorProgressProgressBarControl.StyleController = dataLayoutControl1;
            MinorProgressProgressBarControl.TabIndex = 1;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(497, 263);
            Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.AllowDrawBackground = false;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { ItemForStep, ItemForMajorProgress, ItemForMinorProgress, ItemForInfo, emptySpaceItem1, ItemForDescription });
            layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup1.Name = "autoGeneratedGroup0";
            layoutControlGroup1.Size = new System.Drawing.Size(477, 243);
            // 
            // ItemForStep
            // 
            ItemForStep.Control = StepTextEdit;
            ItemForStep.Location = new System.Drawing.Point(0, 0);
            ItemForStep.Name = "ItemForStep";
            ItemForStep.Size = new System.Drawing.Size(477, 24);
            ItemForStep.Text = "Step";
            ItemForStep.TextSize = new System.Drawing.Size(0, 0);
            ItemForStep.TextVisible = false;
            // 
            // ItemForMajorProgress
            // 
            ItemForMajorProgress.Control = MajorProgressProgressBarControl;
            ItemForMajorProgress.Location = new System.Drawing.Point(0, 48);
            ItemForMajorProgress.Name = "ItemForMajorProgress";
            ItemForMajorProgress.Size = new System.Drawing.Size(477, 19);
            ItemForMajorProgress.Text = "Major Progress";
            ItemForMajorProgress.TextSize = new System.Drawing.Size(0, 0);
            ItemForMajorProgress.TextVisible = false;
            // 
            // ItemForMinorProgress
            // 
            ItemForMinorProgress.Control = MinorProgressProgressBarControl;
            ItemForMinorProgress.Location = new System.Drawing.Point(0, 67);
            ItemForMinorProgress.Name = "ItemForMinorProgress";
            ItemForMinorProgress.Size = new System.Drawing.Size(477, 20);
            ItemForMinorProgress.Text = "Minor Progress";
            ItemForMinorProgress.TextSize = new System.Drawing.Size(0, 0);
            ItemForMinorProgress.TextVisible = false;
            // 
            // ItemForInfo
            // 
            ItemForInfo.Control = InfoTextEdit;
            ItemForInfo.Location = new System.Drawing.Point(0, 219);
            ItemForInfo.Name = "ItemForInfo";
            ItemForInfo.Size = new System.Drawing.Size(477, 24);
            ItemForInfo.Text = "Info";
            ItemForInfo.TextSize = new System.Drawing.Size(0, 0);
            ItemForInfo.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new System.Drawing.Point(0, 87);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(477, 132);
            emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // DescriptionTextEdit
            // 
            DescriptionTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bindingSource, "Description", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            DescriptionTextEdit.Location = new System.Drawing.Point(12, 36);
            DescriptionTextEdit.Name = "DescriptionTextEdit";
            DescriptionTextEdit.Properties.ReadOnly = true;
            DescriptionTextEdit.Size = new System.Drawing.Size(473, 20);
            DescriptionTextEdit.StyleController = dataLayoutControl1;
            DescriptionTextEdit.TabIndex = 2;
            // 
            // ItemForDescription
            // 
            ItemForDescription.Control = DescriptionTextEdit;
            ItemForDescription.Location = new System.Drawing.Point(0, 24);
            ItemForDescription.Name = "ItemForDescription";
            ItemForDescription.Size = new System.Drawing.Size(477, 24);
            ItemForDescription.Text = "Description";
            ItemForDescription.TextSize = new System.Drawing.Size(0, 0);
            ItemForDescription.TextVisible = false;
            // 
            // ImportCactiView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(dataLayoutControl1);
            Name = "ImportCactiView";
            Size = new System.Drawing.Size(497, 263);
            ((System.ComponentModel.ISupportInitialize)mvvmContext).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataLayoutControl1).EndInit();
            dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)StepTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)InfoTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)MajorProgressProgressBarControl.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)MinorProgressProgressBarControl.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForStep).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForMajorProgress).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForMinorProgress).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForInfo).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)DescriptionTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForDescription).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private System.Windows.Forms.BindingSource bindingSource;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit StepTextEdit;
        private DevExpress.XtraEditors.TextEdit InfoTextEdit;
        private DevExpress.XtraEditors.ProgressBarControl MajorProgressProgressBarControl;
        private DevExpress.XtraEditors.ProgressBarControl MinorProgressProgressBarControl;
        private DevExpress.XtraLayout.LayoutControlItem ItemForStep;
        private DevExpress.XtraLayout.LayoutControlItem ItemForMajorProgress;
        private DevExpress.XtraLayout.LayoutControlItem ItemForMinorProgress;
        private DevExpress.XtraLayout.LayoutControlItem ItemForInfo;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.TextEdit DescriptionTextEdit;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDescription;
    }
}
