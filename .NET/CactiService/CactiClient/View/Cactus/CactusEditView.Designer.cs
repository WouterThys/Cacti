﻿namespace CactiClient.View.Cactus
{
    partial class CactusEditView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CactusEditView));
            ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            bbiSave = new DevExpress.XtraBars.BarButtonItem();
            bbiSaveAndClose = new DevExpress.XtraBars.BarButtonItem();
            bbiReset = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            cactusEditData = new CactusEditData();
            mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(components);
            ((System.ComponentModel.ISupportInitialize)ribbonControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).BeginInit();
            SuspendLayout();
            // 
            // ribbonControl
            // 
            ribbonControl.ExpandCollapseItem.Id = 0;
            ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl.ExpandCollapseItem, bbiSave, bbiSaveAndClose, bbiReset });
            ribbonControl.Location = new System.Drawing.Point(0, 0);
            ribbonControl.MaxItemId = 4;
            ribbonControl.Name = "ribbonControl";
            ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
            ribbonControl.Size = new System.Drawing.Size(412, 150);
            // 
            // barButtonItem1
            // 
            bbiSave.Caption = "Save";
            bbiSave.Id = 1;
            bbiSave.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem1.ImageOptions.SvgImage");
            bbiSave.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            bbiSaveAndClose.Caption = "SaveClose";
            bbiSaveAndClose.Id = 2;
            bbiSaveAndClose.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem2.ImageOptions.SvgImage");
            bbiSaveAndClose.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            bbiReset.Caption = "Reset";
            bbiReset.Id = 3;
            bbiReset.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem3.ImageOptions.SvgImage");
            bbiReset.Name = "barButtonItem3";
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1 });
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "Thuis";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.ItemLinks.Add(bbiSave);
            ribbonPageGroup1.ItemLinks.Add(bbiSaveAndClose);
            ribbonPageGroup1.ItemLinks.Add(bbiReset);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "Thuis";
            // 
            // cactusEditData
            // 
            cactusEditData.Dock = System.Windows.Forms.DockStyle.Fill;
            cactusEditData.Location = new System.Drawing.Point(0, 150);
            cactusEditData.Name = "cactusEditData";
            cactusEditData.Size = new System.Drawing.Size(412, 255);
            cactusEditData.TabIndex = 1;
            // 
            // mvvmContext
            // 
            mvvmContext.ContainerControl = this;
            mvvmContext.ViewModelType = typeof(ViewModel.Cactus.EditCactusViewModel);
            // 
            // CactusEditView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(cactusEditData);
            Controls.Add(ribbonControl);
            Name = "CactusEditView";
            Size = new System.Drawing.Size(412, 405);
            ((System.ComponentModel.ISupportInitialize)ribbonControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)mvvmContext).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private CactusEditData cactusEditData;
        private DevExpress.XtraBars.BarButtonItem bbiSave;
        private DevExpress.XtraBars.BarButtonItem bbiSaveAndClose;
        private DevExpress.XtraBars.BarButtonItem bbiReset;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
    }
}