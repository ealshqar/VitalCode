namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormAbout
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
            this.layoutControlAbout = new DevExpress.XtraLayout.LayoutControl();
            this.memoEditReleaseNotes = new DevExpress.XtraEditors.MemoEdit();
            this.hyperLinkEditURL = new DevExpress.XtraEditors.HyperLinkEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroupAbout = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemURL = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItemCopyright = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemImage = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItemVitalExpert = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItemVerisonInfo = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlAbout)).BeginInit();
            this.layoutControlAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditReleaseNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditURL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupAbout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemURL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemCopyright)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemVitalExpert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemVerisonInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlAbout
            // 
            this.layoutControlAbout.AllowCustomizationMenu = false;
            this.layoutControlAbout.Controls.Add(this.memoEditReleaseNotes);
            this.layoutControlAbout.Controls.Add(this.hyperLinkEditURL);
            this.layoutControlAbout.Controls.Add(this.pictureEdit1);
            this.layoutControlAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlAbout.Location = new System.Drawing.Point(0, 0);
            this.layoutControlAbout.Name = "layoutControlAbout";
            this.layoutControlAbout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(575, 81, 250, 350);
            this.layoutControlAbout.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControlAbout.Root = this.layoutControlGroupAbout;
            this.layoutControlAbout.Size = new System.Drawing.Size(501, 392);
            this.layoutControlAbout.TabIndex = 0;
            this.layoutControlAbout.Text = "layoutControl1";
            // 
            // memoEditReleaseNotes
            // 
            this.memoEditReleaseNotes.EditValue = "";
            this.memoEditReleaseNotes.Location = new System.Drawing.Point(14, 220);
            this.memoEditReleaseNotes.Name = "memoEditReleaseNotes";
            this.memoEditReleaseNotes.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.memoEditReleaseNotes.Properties.Appearance.Options.UseBackColor = true;
            this.memoEditReleaseNotes.Properties.ReadOnly = true;
            this.memoEditReleaseNotes.Size = new System.Drawing.Size(474, 113);
            this.memoEditReleaseNotes.StyleController = this.layoutControlAbout;
            this.memoEditReleaseNotes.TabIndex = 6;
            this.memoEditReleaseNotes.TabStop = false;
            // 
            // hyperLinkEditURL
            // 
            this.hyperLinkEditURL.EditValue = "vital@standardenzyme.com";
            this.hyperLinkEditURL.Location = new System.Drawing.Point(12, 338);
            this.hyperLinkEditURL.Name = "hyperLinkEditURL";
            this.hyperLinkEditURL.Properties.Appearance.Options.UseTextOptions = true;
            this.hyperLinkEditURL.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.hyperLinkEditURL.Size = new System.Drawing.Size(477, 20);
            this.hyperLinkEditURL.StyleController = this.layoutControlAbout;
            this.hyperLinkEditURL.TabIndex = 5;
            this.hyperLinkEditURL.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyperLinkEditURL_OpenLink);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::Vital.UI.Properties.Resources.VITAL_LOGO;
            this.pictureEdit1.Location = new System.Drawing.Point(12, 12);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit1.Size = new System.Drawing.Size(477, 129);
            this.pictureEdit1.StyleController = this.layoutControlAbout;
            this.pictureEdit1.TabIndex = 4;
            // 
            // layoutControlGroupAbout
            // 
            this.layoutControlGroupAbout.CustomizationFormText = "Root";
            this.layoutControlGroupAbout.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupAbout.GroupBordersVisible = false;
            this.layoutControlGroupAbout.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemURL,
            this.simpleLabelItemCopyright,
            this.layoutControlGroup1,
            this.layoutControlItemImage,
            this.simpleLabelItemVitalExpert,
            this.simpleLabelItemVerisonInfo});
            this.layoutControlGroupAbout.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupAbout.Name = "Root";
            this.layoutControlGroupAbout.Size = new System.Drawing.Size(501, 392);
            this.layoutControlGroupAbout.Text = "Root";
            this.layoutControlGroupAbout.TextVisible = false;
            // 
            // layoutControlItemURL
            // 
            this.layoutControlItemURL.Control = this.hyperLinkEditURL;
            this.layoutControlItemURL.CustomizationFormText = "URL";
            this.layoutControlItemURL.Location = new System.Drawing.Point(0, 326);
            this.layoutControlItemURL.Name = "layoutControlItemURL";
            this.layoutControlItemURL.Size = new System.Drawing.Size(481, 24);
            this.layoutControlItemURL.Text = "URL";
            this.layoutControlItemURL.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemURL.TextToControlDistance = 0;
            this.layoutControlItemURL.TextVisible = false;
            // 
            // simpleLabelItemCopyright
            // 
            this.simpleLabelItemCopyright.AllowHotTrack = false;
            this.simpleLabelItemCopyright.AppearanceItemCaption.Font = new System.Drawing.Font("Calibri", 11F);
            this.simpleLabelItemCopyright.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItemCopyright.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItemCopyright.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItemCopyright.CustomizationFormText = "Copyright";
            this.simpleLabelItemCopyright.Location = new System.Drawing.Point(0, 350);
            this.simpleLabelItemCopyright.Name = "simpleLabelItemCopyright";
            this.simpleLabelItemCopyright.Size = new System.Drawing.Size(481, 22);
            this.simpleLabelItemCopyright.Text = "Copyright (C) 2012 Standard Equipment Company. All rights reserved.";
            this.simpleLabelItemCopyright.TextSize = new System.Drawing.Size(426, 18);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlGroup1.CustomizationFormText = "Release Notes";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 188);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(481, 138);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "Release Notes";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem2.Control = this.memoEditReleaseNotes;
            this.layoutControlItem2.CustomizationFormText = "Release Notes";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(478, 117);
            this.layoutControlItem2.Text = "Release Notes";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItemImage
            // 
            this.layoutControlItemImage.Control = this.pictureEdit1;
            this.layoutControlItemImage.CustomizationFormText = "Image";
            this.layoutControlItemImage.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemImage.Name = "layoutControlItemImage";
            this.layoutControlItemImage.Size = new System.Drawing.Size(481, 133);
            this.layoutControlItemImage.Text = "Image";
            this.layoutControlItemImage.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemImage.TextToControlDistance = 0;
            this.layoutControlItemImage.TextVisible = false;
            // 
            // simpleLabelItemVitalExpert
            // 
            this.simpleLabelItemVitalExpert.AllowHotTrack = false;
            this.simpleLabelItemVitalExpert.AppearanceItemCaption.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItemVitalExpert.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItemVitalExpert.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItemVitalExpert.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItemVitalExpert.CustomizationFormText = "Vital";
            this.simpleLabelItemVitalExpert.Location = new System.Drawing.Point(0, 133);
            this.simpleLabelItemVitalExpert.Name = "simpleLabelItemVitalExpert";
            this.simpleLabelItemVitalExpert.Size = new System.Drawing.Size(481, 33);
            this.simpleLabelItemVitalExpert.Text = "Vital";
            this.simpleLabelItemVitalExpert.TextSize = new System.Drawing.Size(426, 29);
            // 
            // simpleLabelItemVerisonInfo
            // 
            this.simpleLabelItemVerisonInfo.AllowHotTrack = false;
            this.simpleLabelItemVerisonInfo.AppearanceItemCaption.Font = new System.Drawing.Font("Calibri", 11F);
            this.simpleLabelItemVerisonInfo.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItemVerisonInfo.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItemVerisonInfo.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItemVerisonInfo.CustomizationFormText = "Version Info";
            this.simpleLabelItemVerisonInfo.Location = new System.Drawing.Point(0, 166);
            this.simpleLabelItemVerisonInfo.Name = "simpleLabelItemVerisonInfo";
            this.simpleLabelItemVerisonInfo.Size = new System.Drawing.Size(481, 22);
            this.simpleLabelItemVerisonInfo.Text = "Version Info";
            this.simpleLabelItemVerisonInfo.TextSize = new System.Drawing.Size(426, 18);
            // 
            // XtraFormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 392);
            this.Controls.Add(this.layoutControlAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "XtraFormAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Vital";
            this.Load += new System.EventHandler(this.XtraFormAbout_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XtraFormAbout_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlAbout)).EndInit();
            this.layoutControlAbout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEditReleaseNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditURL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupAbout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemURL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemCopyright)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemVitalExpert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemVerisonInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlAbout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupAbout;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemImage;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemCopyright;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEditURL;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemURL;
        private DevExpress.XtraEditors.MemoEdit memoEditReleaseNotes;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemVitalExpert;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemVerisonInfo;
    }
}