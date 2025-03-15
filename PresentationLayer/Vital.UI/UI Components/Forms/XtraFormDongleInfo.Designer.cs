namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormDongleInfo
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraFormDongleInfo));
            this.layoutControlDongleInfo = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonDongleReset = new DevExpress.XtraEditors.SimpleButton();
            this.textEditRemainingTime = new DevExpress.XtraEditors.TextEdit();
            this.pictureEditDongleIcon = new DevExpress.XtraEditors.PictureEdit();
            this.textEditExpirationDate = new DevExpress.XtraEditors.TextEdit();
            this.textEditDongleNumber = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroupAbout = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemNumber = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDongleIcon = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemRemainingTime = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemExpirationDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroupDongleResetReminder = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemDongleReset = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItemDongleResetInfo = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlDongleInfo)).BeginInit();
            this.layoutControlDongleInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditRemainingTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditDongleIcon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditExpirationDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDongleNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupAbout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDongleIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRemainingTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemExpirationDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupDongleResetReminder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDongleReset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemDongleResetInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlDongleInfo
            // 
            this.layoutControlDongleInfo.AllowCustomizationMenu = false;
            this.layoutControlDongleInfo.Controls.Add(this.simpleButtonDongleReset);
            this.layoutControlDongleInfo.Controls.Add(this.textEditRemainingTime);
            this.layoutControlDongleInfo.Controls.Add(this.pictureEditDongleIcon);
            this.layoutControlDongleInfo.Controls.Add(this.textEditExpirationDate);
            this.layoutControlDongleInfo.Controls.Add(this.textEditDongleNumber);
            this.layoutControlDongleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlDongleInfo.Location = new System.Drawing.Point(0, 0);
            this.layoutControlDongleInfo.Name = "layoutControlDongleInfo";
            this.layoutControlDongleInfo.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(575, 81, 250, 350);
            this.layoutControlDongleInfo.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControlDongleInfo.Root = this.layoutControlGroupAbout;
            this.layoutControlDongleInfo.Size = new System.Drawing.Size(535, 120);
            this.layoutControlDongleInfo.TabIndex = 0;
            this.layoutControlDongleInfo.Text = "layoutControl1";
            // 
            // simpleButtonDongleReset
            // 
            this.simpleButtonDongleReset.Image = global::Vital.UI.Properties.Resources.DongleInfo16;
            this.simpleButtonDongleReset.Location = new System.Drawing.Point(425, 84);
            this.simpleButtonDongleReset.Name = "simpleButtonDongleReset";
            this.simpleButtonDongleReset.Size = new System.Drawing.Size(98, 24);
            this.simpleButtonDongleReset.StyleController = this.layoutControlDongleInfo;
            toolTipTitleItem1.Appearance.Image = global::Vital.UI.Properties.Resources.DongleInfo16;
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Image = global::Vital.UI.Properties.Resources.DongleInfo16;
            toolTipTitleItem1.Text = "Dongle Reset";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = resources.GetString("toolTipItem1.Text");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.simpleButtonDongleReset.SuperTip = superToolTip1;
            this.simpleButtonDongleReset.TabIndex = 4;
            this.simpleButtonDongleReset.TabStop = false;
            this.simpleButtonDongleReset.Text = "Dongle Reset";
            this.simpleButtonDongleReset.Click += new System.EventHandler(this.simpleButtonDongleReset_Click);
            // 
            // textEditRemainingTime
            // 
            this.textEditRemainingTime.Location = new System.Drawing.Point(158, 60);
            this.textEditRemainingTime.Name = "textEditRemainingTime";
            this.textEditRemainingTime.Properties.ReadOnly = true;
            this.textEditRemainingTime.Size = new System.Drawing.Size(365, 20);
            this.textEditRemainingTime.StyleController = this.layoutControlDongleInfo;
            this.textEditRemainingTime.TabIndex = 2;
            this.textEditRemainingTime.TabStop = false;
            // 
            // pictureEditDongleIcon
            // 
            this.pictureEditDongleIcon.EditValue = global::Vital.UI.Properties.Resources.DongleInfo48;
            this.pictureEditDongleIcon.Location = new System.Drawing.Point(12, 12);
            this.pictureEditDongleIcon.Name = "pictureEditDongleIcon";
            this.pictureEditDongleIcon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEditDongleIcon.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEditDongleIcon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEditDongleIcon.Size = new System.Drawing.Size(44, 68);
            this.pictureEditDongleIcon.StyleController = this.layoutControlDongleInfo;
            this.pictureEditDongleIcon.TabIndex = 0;
            // 
            // textEditExpirationDate
            // 
            this.textEditExpirationDate.Location = new System.Drawing.Point(158, 36);
            this.textEditExpirationDate.Name = "textEditExpirationDate";
            this.textEditExpirationDate.Properties.ReadOnly = true;
            this.textEditExpirationDate.Size = new System.Drawing.Size(365, 20);
            this.textEditExpirationDate.StyleController = this.layoutControlDongleInfo;
            this.textEditExpirationDate.TabIndex = 2;
            this.textEditExpirationDate.TabStop = false;
            // 
            // textEditDongleNumber
            // 
            this.textEditDongleNumber.Location = new System.Drawing.Point(158, 12);
            this.textEditDongleNumber.Name = "textEditDongleNumber";
            this.textEditDongleNumber.Properties.ReadOnly = true;
            this.textEditDongleNumber.Size = new System.Drawing.Size(365, 20);
            this.textEditDongleNumber.StyleController = this.layoutControlDongleInfo;
            this.textEditDongleNumber.TabIndex = 1;
            this.textEditDongleNumber.TabStop = false;
            // 
            // layoutControlGroupAbout
            // 
            this.layoutControlGroupAbout.CustomizationFormText = "Root";
            this.layoutControlGroupAbout.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupAbout.GroupBordersVisible = false;
            this.layoutControlGroupAbout.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemNumber,
            this.layoutControlItemDongleIcon,
            this.layoutControlItemRemainingTime,
            this.layoutControlItemExpirationDate,
            this.emptySpaceItem1,
            this.layoutControlGroupDongleResetReminder});
            this.layoutControlGroupAbout.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupAbout.Name = "Root";
            this.layoutControlGroupAbout.Size = new System.Drawing.Size(535, 120);
            this.layoutControlGroupAbout.Text = "Root";
            this.layoutControlGroupAbout.TextVisible = false;
            // 
            // layoutControlItemNumber
            // 
            this.layoutControlItemNumber.Control = this.textEditDongleNumber;
            this.layoutControlItemNumber.CustomizationFormText = "Dongle Number";
            this.layoutControlItemNumber.Image = global::Vital.UI.Properties.Resources.locked;
            this.layoutControlItemNumber.Location = new System.Drawing.Point(48, 0);
            this.layoutControlItemNumber.Name = "layoutControlItemNumber";
            this.layoutControlItemNumber.Size = new System.Drawing.Size(467, 24);
            this.layoutControlItemNumber.Text = "Dongle Number";
            this.layoutControlItemNumber.TextSize = new System.Drawing.Size(95, 16);
            // 
            // layoutControlItemDongleIcon
            // 
            this.layoutControlItemDongleIcon.Control = this.pictureEditDongleIcon;
            this.layoutControlItemDongleIcon.CustomizationFormText = "layoutControlItemDongleIcon";
            this.layoutControlItemDongleIcon.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemDongleIcon.MinSize = new System.Drawing.Size(24, 24);
            this.layoutControlItemDongleIcon.Name = "layoutControlItemDongleIcon";
            this.layoutControlItemDongleIcon.Size = new System.Drawing.Size(48, 72);
            this.layoutControlItemDongleIcon.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemDongleIcon.Text = "layoutControlItemDongleIcon";
            this.layoutControlItemDongleIcon.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemDongleIcon.TextToControlDistance = 0;
            this.layoutControlItemDongleIcon.TextVisible = false;
            // 
            // layoutControlItemRemainingTime
            // 
            this.layoutControlItemRemainingTime.Control = this.textEditRemainingTime;
            this.layoutControlItemRemainingTime.CustomizationFormText = "Remaining Time";
            this.layoutControlItemRemainingTime.Image = global::Vital.UI.Properties.Resources.BlueBell16;
            this.layoutControlItemRemainingTime.Location = new System.Drawing.Point(48, 48);
            this.layoutControlItemRemainingTime.Name = "layoutControlItemRemainingTime";
            this.layoutControlItemRemainingTime.Size = new System.Drawing.Size(467, 24);
            this.layoutControlItemRemainingTime.Text = "Remaining Time";
            this.layoutControlItemRemainingTime.TextSize = new System.Drawing.Size(95, 16);
            // 
            // layoutControlItemExpirationDate
            // 
            this.layoutControlItemExpirationDate.Control = this.textEditExpirationDate;
            this.layoutControlItemExpirationDate.CustomizationFormText = "Expiration Date";
            this.layoutControlItemExpirationDate.Image = global::Vital.UI.Properties.Resources.clock;
            this.layoutControlItemExpirationDate.Location = new System.Drawing.Point(48, 24);
            this.layoutControlItemExpirationDate.Name = "layoutControlItemExpirationDate";
            this.layoutControlItemExpirationDate.Size = new System.Drawing.Size(467, 24);
            this.layoutControlItemExpirationDate.Text = "Expiration Date";
            this.layoutControlItemExpirationDate.TextSize = new System.Drawing.Size(95, 16);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 72);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(48, 28);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroupDongleResetReminder
            // 
            this.layoutControlGroupDongleResetReminder.CustomizationFormText = "layoutControlGroupDongleResetReminder";
            this.layoutControlGroupDongleResetReminder.GroupBordersVisible = false;
            this.layoutControlGroupDongleResetReminder.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemDongleReset,
            this.simpleLabelItemDongleResetInfo});
            this.layoutControlGroupDongleResetReminder.Location = new System.Drawing.Point(48, 72);
            this.layoutControlGroupDongleResetReminder.Name = "layoutControlGroupDongleResetReminder";
            this.layoutControlGroupDongleResetReminder.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroupDongleResetReminder.Size = new System.Drawing.Size(467, 28);
            this.layoutControlGroupDongleResetReminder.Text = "layoutControlGroupDongleResetReminder";
            this.layoutControlGroupDongleResetReminder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItemDongleReset
            // 
            this.layoutControlItemDongleReset.Control = this.simpleButtonDongleReset;
            this.layoutControlItemDongleReset.CustomizationFormText = "layoutControlItemDongleReset";
            this.layoutControlItemDongleReset.Location = new System.Drawing.Point(365, 0);
            this.layoutControlItemDongleReset.MaxSize = new System.Drawing.Size(102, 28);
            this.layoutControlItemDongleReset.MinSize = new System.Drawing.Size(102, 28);
            this.layoutControlItemDongleReset.Name = "layoutControlItemDongleReset";
            this.layoutControlItemDongleReset.Size = new System.Drawing.Size(102, 28);
            this.layoutControlItemDongleReset.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemDongleReset.Text = "layoutControlItemDongleReset";
            this.layoutControlItemDongleReset.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemDongleReset.TextToControlDistance = 0;
            this.layoutControlItemDongleReset.TextVisible = false;
            // 
            // simpleLabelItemDongleResetInfo
            // 
            this.simpleLabelItemDongleResetInfo.AllowHotTrack = false;
            this.simpleLabelItemDongleResetInfo.CustomizationFormText = "LabelsimpleLabelItemDongleResetInfo";
            this.simpleLabelItemDongleResetInfo.Image = global::Vital.UI.Properties.Resources.warning;
            this.simpleLabelItemDongleResetInfo.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItemDongleResetInfo.Name = "simpleLabelItemDongleResetInfo";
            this.simpleLabelItemDongleResetInfo.Size = new System.Drawing.Size(365, 28);
            this.simpleLabelItemDongleResetInfo.Text = "Your dongle is about to expire, click \"Dongle Reset\" to reset.";
            this.simpleLabelItemDongleResetInfo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItemDongleResetInfo.TextSize = new System.Drawing.Size(311, 16);
            // 
            // XtraFormDongleInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 120);
            this.Controls.Add(this.layoutControlDongleInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "XtraFormDongleInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dongle Information";
            this.Load += new System.EventHandler(this.XtraFormAbout_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XtraFormAbout_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlDongleInfo)).EndInit();
            this.layoutControlDongleInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditRemainingTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditDongleIcon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditExpirationDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDongleNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupAbout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDongleIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRemainingTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemExpirationDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupDongleResetReminder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDongleReset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemDongleResetInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlDongleInfo;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupAbout;
        private DevExpress.XtraEditors.TextEdit textEditDongleNumber;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemNumber;
        private DevExpress.XtraEditors.TextEdit textEditExpirationDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemExpirationDate;
        private DevExpress.XtraEditors.PictureEdit pictureEditDongleIcon;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDongleIcon;
        private DevExpress.XtraEditors.TextEdit textEditRemainingTime;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRemainingTime;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemDongleResetInfo;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDongleReset;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDongleReset;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupDongleResetReminder;
    }
}