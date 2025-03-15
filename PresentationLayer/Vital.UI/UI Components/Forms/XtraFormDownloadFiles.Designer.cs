namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormDownloadFiles
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
            this.progressBarControlCurrentFile = new DevExpress.XtraEditors.ProgressBarControl();
            this.progressBarControlAllFiles = new DevExpress.XtraEditors.ProgressBarControl();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroupMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.simpleLabelItemInfo = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItemCurrentProgress = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItemCurrentFile = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItemAllFilesProgress = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItemAllFiles = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
            this.layoutControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControlCurrentFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControlAllFiles.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCurrentProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemCurrentFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAllFilesProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemAllFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMain
            // 
            this.layoutControlMain.AllowCustomizationMenu = false;
            this.layoutControlMain.Controls.Add(this.progressBarControlCurrentFile);
            this.layoutControlMain.Controls.Add(this.progressBarControlAllFiles);
            this.layoutControlMain.Controls.Add(this.simpleButtonCancel);
            this.layoutControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMain.Name = "layoutControlMain";
            this.layoutControlMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(761, 72, 250, 350);
            this.layoutControlMain.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControlMain.Root = this.layoutControlGroupMain;
            this.layoutControlMain.Size = new System.Drawing.Size(482, 78);
            this.layoutControlMain.TabIndex = 0;
            this.layoutControlMain.Text = "layoutControl1";
            // 
            // progressBarControlCurrentFile
            // 
            this.progressBarControlCurrentFile.EditValue = "0";
            this.progressBarControlCurrentFile.Location = new System.Drawing.Point(72, 36);
            this.progressBarControlCurrentFile.Name = "progressBarControlCurrentFile";
            this.progressBarControlCurrentFile.Properties.ShowTitle = true;
            this.progressBarControlCurrentFile.Size = new System.Drawing.Size(306, 14);
            this.progressBarControlCurrentFile.StyleController = this.layoutControlMain;
            this.progressBarControlCurrentFile.TabIndex = 6;
            // 
            // progressBarControlAllFiles
            // 
            this.progressBarControlAllFiles.EditValue = "0";
            this.progressBarControlAllFiles.Location = new System.Drawing.Point(72, 54);
            this.progressBarControlAllFiles.Name = "progressBarControlAllFiles";
            this.progressBarControlAllFiles.Properties.ShowTitle = true;
            this.progressBarControlAllFiles.Size = new System.Drawing.Size(306, 12);
            this.progressBarControlAllFiles.StyleController = this.layoutControlMain;
            this.progressBarControlAllFiles.TabIndex = 5;
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Location = new System.Drawing.Point(382, 36);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(88, 30);
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
            this.simpleLabelItemInfo,
            this.layoutControlItemCurrentProgress,
            this.simpleLabelItemCurrentFile,
            this.layoutControlItemAllFilesProgress,
            this.simpleLabelItemAllFiles,
            this.layoutControlItemCancel});
            this.layoutControlGroupMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMain.Name = "Root";
            this.layoutControlGroupMain.Size = new System.Drawing.Size(482, 78);
            this.layoutControlGroupMain.Text = "Root";
            this.layoutControlGroupMain.TextVisible = false;
            // 
            // simpleLabelItemInfo
            // 
            this.simpleLabelItemInfo.AllowHotTrack = false;
            this.simpleLabelItemInfo.CustomizationFormText = "Info";
            this.simpleLabelItemInfo.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItemInfo.MinSize = new System.Drawing.Size(111, 17);
            this.simpleLabelItemInfo.Name = "simpleLabelItemInfo";
            this.simpleLabelItemInfo.Size = new System.Drawing.Size(462, 24);
            this.simpleLabelItemInfo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItemInfo.Text = " ";
            this.simpleLabelItemInfo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.simpleLabelItemInfo.TextSize = new System.Drawing.Size(50, 20);
            // 
            // layoutControlItemCurrentProgress
            // 
            this.layoutControlItemCurrentProgress.Control = this.progressBarControlCurrentFile;
            this.layoutControlItemCurrentProgress.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItemCurrentProgress.Location = new System.Drawing.Point(60, 24);
            this.layoutControlItemCurrentProgress.Name = "layoutControlItemCurrentProgress";
            this.layoutControlItemCurrentProgress.Size = new System.Drawing.Size(310, 18);
            this.layoutControlItemCurrentProgress.Text = "layoutControlItemCurrentProgress";
            this.layoutControlItemCurrentProgress.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCurrentProgress.TextToControlDistance = 0;
            this.layoutControlItemCurrentProgress.TextVisible = false;
            // 
            // simpleLabelItemCurrentFile
            // 
            this.simpleLabelItemCurrentFile.AllowHotTrack = false;
            this.simpleLabelItemCurrentFile.CustomizationFormText = "Current File";
            this.simpleLabelItemCurrentFile.Location = new System.Drawing.Point(0, 24);
            this.simpleLabelItemCurrentFile.Name = "simpleLabelItemCurrentFile";
            this.simpleLabelItemCurrentFile.Size = new System.Drawing.Size(60, 17);
            this.simpleLabelItemCurrentFile.Text = "Current File";
            this.simpleLabelItemCurrentFile.TextSize = new System.Drawing.Size(56, 13);
            // 
            // layoutControlItemAllFilesProgress
            // 
            this.layoutControlItemAllFilesProgress.Control = this.progressBarControlAllFiles;
            this.layoutControlItemAllFilesProgress.CustomizationFormText = "Progress";
            this.layoutControlItemAllFilesProgress.Location = new System.Drawing.Point(60, 42);
            this.layoutControlItemAllFilesProgress.MinSize = new System.Drawing.Size(54, 16);
            this.layoutControlItemAllFilesProgress.Name = "layoutControlItemAllFilesProgress";
            this.layoutControlItemAllFilesProgress.Size = new System.Drawing.Size(310, 16);
            this.layoutControlItemAllFilesProgress.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemAllFilesProgress.Text = "Progress";
            this.layoutControlItemAllFilesProgress.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemAllFilesProgress.TextToControlDistance = 0;
            this.layoutControlItemAllFilesProgress.TextVisible = false;
            // 
            // simpleLabelItemAllFiles
            // 
            this.simpleLabelItemAllFiles.AllowHotTrack = false;
            this.simpleLabelItemAllFiles.CustomizationFormText = "All Files";
            this.simpleLabelItemAllFiles.Location = new System.Drawing.Point(0, 41);
            this.simpleLabelItemAllFiles.Name = "simpleLabelItemAllFiles";
            this.simpleLabelItemAllFiles.Size = new System.Drawing.Size(60, 17);
            this.simpleLabelItemAllFiles.Text = "All Files";
            this.simpleLabelItemAllFiles.TextSize = new System.Drawing.Size(56, 13);
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.simpleButtonCancel;
            this.layoutControlItemCancel.CustomizationFormText = "Cancel";
            this.layoutControlItemCancel.Location = new System.Drawing.Point(370, 24);
            this.layoutControlItemCancel.MinSize = new System.Drawing.Size(47, 26);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(92, 34);
            this.layoutControlItemCancel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemCancel.Text = "Cancel";
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextToControlDistance = 0;
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // XtraFormDownloadFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 78);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XtraFormDownloadFiles";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Download";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XtraFormUpdate_FormClosing);
            this.Load += new System.EventHandler(this.XtraFormUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
            this.layoutControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControlCurrentFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControlAllFiles.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCurrentProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemCurrentFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAllFilesProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemAllFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMain;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControlAllFiles;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAllFilesProgress;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemInfo;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControlCurrentFile;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCurrentProgress;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemAllFiles;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemCurrentFile;
    }
}