namespace CactiClient.View.Info
{
    partial class InfoView
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
            bindingSource = new System.Windows.Forms.BindingSource(components);
            dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            VersionTextEdit = new DevExpress.XtraEditors.TextEdit();
            ItemForVersion = new DevExpress.XtraLayout.LayoutControlItem();
            LayoutTextEdit = new DevExpress.XtraEditors.TextEdit();
            ItemForLayout = new DevExpress.XtraLayout.LayoutControlItem();
            AddressTextEdit = new DevExpress.XtraEditors.TextEdit();
            ItemForAddress = new DevExpress.XtraLayout.LayoutControlItem();
            InstallDirectoryTextEdit = new DevExpress.XtraEditors.TextEdit();
            ItemForInstallDirectory = new DevExpress.XtraLayout.LayoutControlItem();
            CacheDirectoryTextEdit = new DevExpress.XtraEditors.TextEdit();
            ItemForCacheDirectory = new DevExpress.XtraLayout.LayoutControlItem();
            MachineIpTextEdit = new DevExpress.XtraEditors.TextEdit();
            ItemForMachineIp = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataLayoutControl1).BeginInit();
            dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)VersionTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForVersion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LayoutTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForLayout).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AddressTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForAddress).BeginInit();
            ((System.ComponentModel.ISupportInitialize)InstallDirectoryTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForInstallDirectory).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CacheDirectoryTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForCacheDirectory).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MachineIpTextEdit.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemForMachineIp).BeginInit();
            SuspendLayout();
            // 
            // mvvmContext
            // 
            mvvmContext.ContainerControl = this;
            mvvmContext.ViewModelType = typeof(ViewModel.Info.InfoViewModel);
            // 
            // bindingSource
            // 
            bindingSource.DataSource = typeof(ViewModel.Info.InfoViewModel);
            // 
            // dataLayoutControl1
            // 
            dataLayoutControl1.Controls.Add(VersionTextEdit);
            dataLayoutControl1.Controls.Add(LayoutTextEdit);
            dataLayoutControl1.Controls.Add(AddressTextEdit);
            dataLayoutControl1.Controls.Add(InstallDirectoryTextEdit);
            dataLayoutControl1.Controls.Add(CacheDirectoryTextEdit);
            dataLayoutControl1.Controls.Add(MachineIpTextEdit);
            dataLayoutControl1.DataSource = bindingSource;
            dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            dataLayoutControl1.Name = "dataLayoutControl1";
            dataLayoutControl1.Root = Root;
            dataLayoutControl1.Size = new System.Drawing.Size(430, 427);
            dataLayoutControl1.TabIndex = 0;
            dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(430, 427);
            Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.AllowDrawBackground = false;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { ItemForVersion, ItemForLayout, ItemForAddress, ItemForInstallDirectory, ItemForCacheDirectory, ItemForMachineIp });
            layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup1.Name = "autoGeneratedGroup0";
            layoutControlGroup1.Size = new System.Drawing.Size(410, 407);
            // 
            // VersionTextEdit
            // 
            VersionTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bindingSource, "Version", true));
            VersionTextEdit.Location = new System.Drawing.Point(101, 12);
            VersionTextEdit.Name = "VersionTextEdit";
            VersionTextEdit.Size = new System.Drawing.Size(317, 20);
            VersionTextEdit.StyleController = dataLayoutControl1;
            VersionTextEdit.TabIndex = 4;
            // 
            // ItemForVersion
            // 
            ItemForVersion.Control = VersionTextEdit;
            ItemForVersion.Location = new System.Drawing.Point(0, 0);
            ItemForVersion.Name = "ItemForVersion";
            ItemForVersion.Size = new System.Drawing.Size(410, 24);
            ItemForVersion.Text = "Version";
            ItemForVersion.TextSize = new System.Drawing.Size(77, 13);
            // 
            // LayoutTextEdit
            // 
            LayoutTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bindingSource, "Layout", true));
            LayoutTextEdit.Location = new System.Drawing.Point(101, 36);
            LayoutTextEdit.Name = "LayoutTextEdit";
            LayoutTextEdit.Size = new System.Drawing.Size(317, 20);
            LayoutTextEdit.StyleController = dataLayoutControl1;
            LayoutTextEdit.TabIndex = 5;
            // 
            // ItemForLayout
            // 
            ItemForLayout.Control = LayoutTextEdit;
            ItemForLayout.Location = new System.Drawing.Point(0, 24);
            ItemForLayout.Name = "ItemForLayout";
            ItemForLayout.Size = new System.Drawing.Size(410, 24);
            ItemForLayout.Text = "Layout";
            ItemForLayout.TextSize = new System.Drawing.Size(77, 13);
            // 
            // AddressTextEdit
            // 
            AddressTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bindingSource, "Address", true));
            AddressTextEdit.Location = new System.Drawing.Point(101, 60);
            AddressTextEdit.Name = "AddressTextEdit";
            AddressTextEdit.Size = new System.Drawing.Size(317, 20);
            AddressTextEdit.StyleController = dataLayoutControl1;
            AddressTextEdit.TabIndex = 6;
            // 
            // ItemForAddress
            // 
            ItemForAddress.Control = AddressTextEdit;
            ItemForAddress.Location = new System.Drawing.Point(0, 48);
            ItemForAddress.Name = "ItemForAddress";
            ItemForAddress.Size = new System.Drawing.Size(410, 24);
            ItemForAddress.Text = "Address";
            ItemForAddress.TextSize = new System.Drawing.Size(77, 13);
            // 
            // InstallDirectoryTextEdit
            // 
            InstallDirectoryTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bindingSource, "InstallDirectory", true));
            InstallDirectoryTextEdit.Location = new System.Drawing.Point(101, 84);
            InstallDirectoryTextEdit.Name = "InstallDirectoryTextEdit";
            InstallDirectoryTextEdit.Size = new System.Drawing.Size(317, 20);
            InstallDirectoryTextEdit.StyleController = dataLayoutControl1;
            InstallDirectoryTextEdit.TabIndex = 7;
            // 
            // ItemForInstallDirectory
            // 
            ItemForInstallDirectory.Control = InstallDirectoryTextEdit;
            ItemForInstallDirectory.Location = new System.Drawing.Point(0, 72);
            ItemForInstallDirectory.Name = "ItemForInstallDirectory";
            ItemForInstallDirectory.Size = new System.Drawing.Size(410, 24);
            ItemForInstallDirectory.Text = "Install Directory";
            ItemForInstallDirectory.TextSize = new System.Drawing.Size(77, 13);
            // 
            // CacheDirectoryTextEdit
            // 
            CacheDirectoryTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bindingSource, "CacheDirectory", true));
            CacheDirectoryTextEdit.Location = new System.Drawing.Point(101, 108);
            CacheDirectoryTextEdit.Name = "CacheDirectoryTextEdit";
            CacheDirectoryTextEdit.Size = new System.Drawing.Size(317, 20);
            CacheDirectoryTextEdit.StyleController = dataLayoutControl1;
            CacheDirectoryTextEdit.TabIndex = 8;
            // 
            // ItemForCacheDirectory
            // 
            ItemForCacheDirectory.Control = CacheDirectoryTextEdit;
            ItemForCacheDirectory.Location = new System.Drawing.Point(0, 96);
            ItemForCacheDirectory.Name = "ItemForCacheDirectory";
            ItemForCacheDirectory.Size = new System.Drawing.Size(410, 24);
            ItemForCacheDirectory.Text = "Cache Directory";
            ItemForCacheDirectory.TextSize = new System.Drawing.Size(77, 13);
            // 
            // MachineIpTextEdit
            // 
            MachineIpTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", bindingSource, "MachineIp", true));
            MachineIpTextEdit.Location = new System.Drawing.Point(101, 132);
            MachineIpTextEdit.Name = "MachineIpTextEdit";
            MachineIpTextEdit.Size = new System.Drawing.Size(317, 20);
            MachineIpTextEdit.StyleController = dataLayoutControl1;
            MachineIpTextEdit.TabIndex = 9;
            // 
            // ItemForMachineIp
            // 
            ItemForMachineIp.Control = MachineIpTextEdit;
            ItemForMachineIp.Location = new System.Drawing.Point(0, 120);
            ItemForMachineIp.Name = "ItemForMachineIp";
            ItemForMachineIp.Size = new System.Drawing.Size(410, 287);
            ItemForMachineIp.Text = "Machine Ip";
            ItemForMachineIp.TextSize = new System.Drawing.Size(77, 13);
            // 
            // InfoView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(dataLayoutControl1);
            Name = "InfoView";
            Size = new System.Drawing.Size(430, 427);
            ((System.ComponentModel.ISupportInitialize)mvvmContext).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataLayoutControl1).EndInit();
            dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)VersionTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForVersion).EndInit();
            ((System.ComponentModel.ISupportInitialize)LayoutTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForLayout).EndInit();
            ((System.ComponentModel.ISupportInitialize)AddressTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForAddress).EndInit();
            ((System.ComponentModel.ISupportInitialize)InstallDirectoryTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForInstallDirectory).EndInit();
            ((System.ComponentModel.ISupportInitialize)CacheDirectoryTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForCacheDirectory).EndInit();
            ((System.ComponentModel.ISupportInitialize)MachineIpTextEdit.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemForMachineIp).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private System.Windows.Forms.BindingSource bindingSource;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.TextEdit VersionTextEdit;
        private DevExpress.XtraEditors.TextEdit LayoutTextEdit;
        private DevExpress.XtraEditors.TextEdit AddressTextEdit;
        private DevExpress.XtraEditors.TextEdit InstallDirectoryTextEdit;
        private DevExpress.XtraEditors.TextEdit CacheDirectoryTextEdit;
        private DevExpress.XtraEditors.TextEdit MachineIpTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForVersion;
        private DevExpress.XtraLayout.LayoutControlItem ItemForLayout;
        private DevExpress.XtraLayout.LayoutControlItem ItemForAddress;
        private DevExpress.XtraLayout.LayoutControlItem ItemForInstallDirectory;
        private DevExpress.XtraLayout.LayoutControlItem ItemForCacheDirectory;
        private DevExpress.XtraLayout.LayoutControlItem ItemForMachineIp;
    }
}
