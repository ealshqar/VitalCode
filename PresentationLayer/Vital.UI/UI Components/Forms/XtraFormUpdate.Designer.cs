namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormUpdate
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
            this.layoutControlMain = new DevExpress.XtraLayout.LayoutControl();
            this.progressBarControlUpdate = new DevExpress.XtraEditors.ProgressBarControl();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroupMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemProgress = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItemInfo = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
            this.layoutControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControlUpdate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMain
            // 
            this.layoutControlMain.AllowCustomizationMenu = false;
            this.layoutControlMain.Controls.Add(this.progressBarControlUpdate);
            this.layoutControlMain.Controls.Add(this.simpleButtonCancel);
            this.layoutControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMain.Name = "layoutControlMain";
            this.layoutControlMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(761, 72, 250, 350);
            this.layoutControlMain.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControlMain.Root = this.layoutControlGroupMain;
            this.layoutControlMain.Size = new System.Drawing.Size(482, 83);
            this.layoutControlMain.TabIndex = 0;
            this.layoutControlMain.Text = "layoutControl1";
            // 
            // progressBarControlUpdate
            // 
            this.progressBarControlUpdate.EditValue = "0";
            this.progressBarControlUpdate.Location = new System.Drawing.Point(12, 49);
            this.progressBarControlUpdate.Name = "progressBarControlUpdate";
            this.progressBarControlUpdate.Properties.ShowTitle = true;
            this.progressBarControlUpdate.Size = new System.Drawing.Size(346, 22);
            this.progressBarControlUpdate.StyleController = this.layoutControlMain;
            this.progressBarControlUpdate.TabIndex = 5;
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Location = new System.Drawing.Point(362, 49);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(108, 22);
            this.simpleButtonCancel.StyleController = this.layoutControlMain;
            this.simpleButtonCancel.TabIndex = 4;
            this.simpleButtonCancel.Text = "Cancel";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // layoutControlGroupMain
            // 
            this.layoutControlGroupMain.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroupMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMain.GroupBordersVisible = false;
            this.layoutControlGroupMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemCancel,
            this.layoutControlItemProgress,
            this.simpleLabelItemInfo});
            this.layoutControlGroupMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMain.Name = "Root";
            this.layoutControlGroupMain.Size = new System.Drawing.Size(482, 83);
            this.layoutControlGroupMain.Text = "Root";
            this.layoutControlGroupMain.TextVisible = false;
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.simpleButtonCancel;
            this.layoutControlItemCancel.CustomizationFormText = "Cancel";
            this.layoutControlItemCancel.Location = new System.Drawing.Point(350, 37);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(112, 26);
            this.layoutControlItemCancel.Text = "Cancel";
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextToControlDistance = 0;
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // layoutControlItemProgress
            // 
            this.layoutControlItemProgress.Control = this.progressBarControlUpdate;
            this.layoutControlItemProgress.CustomizationFormText = "Progress";
            this.layoutControlItemProgress.Location = new System.Drawing.Point(0, 37);
            this.layoutControlItemProgress.MinSize = new System.Drawing.Size(54, 16);
            this.layoutControlItemProgress.Name = "layoutControlItemProgress";
            this.layoutControlItemProgress.Size = new System.Drawing.Size(350, 26);
            this.layoutControlItemProgress.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemProgress.Text = "Progress";
            this.layoutControlItemProgress.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemProgress.TextToControlDistance = 0;
            this.layoutControlItemProgress.TextVisible = false;
            // 
            // simpleLabelItemInfo
            // 
            this.simpleLabelItemInfo.AllowHotTrack = false;
            this.simpleLabelItemInfo.CustomizationFormText = "Info";
            this.simpleLabelItemInfo.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItemInfo.MinSize = new System.Drawing.Size(111, 17);
            this.simpleLabelItemInfo.Name = "simpleLabelItemInfo";
            this.simpleLabelItemInfo.Size = new System.Drawing.Size(462, 37);
            this.simpleLabelItemInfo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItemInfo.Text = " ";
            this.simpleLabelItemInfo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.simpleLabelItemInfo.TextSize = new System.Drawing.Size(50, 20);
            // 
            // XtraFormUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 83);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XtraFormUpdate";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XtraFormUpdate_FormClosing);
            this.Load += new System.EventHandler(this.XtraFormUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
            this.layoutControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControlUpdate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMain;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControlUpdate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemProgress;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemInfo;
    }
}