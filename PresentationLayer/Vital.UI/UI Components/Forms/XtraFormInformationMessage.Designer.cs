namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormInformationMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraFormInformationMessage));
            this.layoutControlMain = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonClose = new DevExpress.XtraEditors.SimpleButton();
            this.memoEditMessage = new DevExpress.XtraEditors.MemoEdit();
            this.memoEditHeader = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroupMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemHeader = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemMessage = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
            this.layoutControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditMessage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditHeader.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMain
            // 
            this.layoutControlMain.Controls.Add(this.simpleButtonClose);
            this.layoutControlMain.Controls.Add(this.memoEditMessage);
            this.layoutControlMain.Controls.Add(this.memoEditHeader);
            this.layoutControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMain.Name = "layoutControlMain";
            this.layoutControlMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(556, 179, 250, 350);
            this.layoutControlMain.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControlMain.Root = this.layoutControlGroupMain;
            this.layoutControlMain.Size = new System.Drawing.Size(499, 430);
            this.layoutControlMain.TabIndex = 0;
            this.layoutControlMain.Text = "layoutControl1";
            // 
            // simpleButtonClose
            // 
            this.simpleButtonClose.Location = new System.Drawing.Point(402, 396);
            this.simpleButtonClose.Name = "simpleButtonClose";
            this.simpleButtonClose.Size = new System.Drawing.Size(85, 22);
            this.simpleButtonClose.StyleController = this.layoutControlMain;
            this.simpleButtonClose.TabIndex = 3;
            this.simpleButtonClose.Text = "Close";
            this.simpleButtonClose.Click += new System.EventHandler(this.simpleButtonClose_Click);
            // 
            // memoEditMessage
            // 
            this.memoEditMessage.Location = new System.Drawing.Point(12, 80);
            this.memoEditMessage.Name = "memoEditMessage";
            this.memoEditMessage.Properties.ReadOnly = true;
            this.memoEditMessage.Size = new System.Drawing.Size(475, 312);
            this.memoEditMessage.StyleController = this.layoutControlMain;
            this.memoEditMessage.TabIndex = 2;
            // 
            // memoEditHeader
            // 
            this.memoEditHeader.Location = new System.Drawing.Point(39, 12);
            this.memoEditHeader.Name = "memoEditHeader";
            this.memoEditHeader.Properties.ReadOnly = true;
            this.memoEditHeader.Size = new System.Drawing.Size(448, 59);
            this.memoEditHeader.StyleController = this.layoutControlMain;
            this.memoEditHeader.TabIndex = 1;
            // 
            // layoutControlGroupMain
            // 
            this.layoutControlGroupMain.CustomizationFormText = "Root";
            this.layoutControlGroupMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMain.GroupBordersVisible = false;
            this.layoutControlGroupMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemHeader,
            this.layoutControlItemMessage,
            this.splitterItem1,
            this.layoutControlItem1,
            this.emptySpaceItem1});
            this.layoutControlGroupMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMain.Name = "Root";
            this.layoutControlGroupMain.Size = new System.Drawing.Size(499, 430);
            this.layoutControlGroupMain.Text = "Root";
            this.layoutControlGroupMain.TextVisible = false;
            // 
            // layoutControlItemHeader
            // 
            this.layoutControlItemHeader.Control = this.memoEditHeader;
            this.layoutControlItemHeader.CustomizationFormText = " ";
            this.layoutControlItemHeader.Image = global::Vital.UI.Properties.Resources.Test_Info;
            this.layoutControlItemHeader.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemHeader.Name = "layoutControlItemHeader";
            this.layoutControlItemHeader.Size = new System.Drawing.Size(479, 63);
            this.layoutControlItemHeader.Text = " ";
            this.layoutControlItemHeader.TextSize = new System.Drawing.Size(24, 16);
            // 
            // layoutControlItemMessage
            // 
            this.layoutControlItemMessage.Control = this.memoEditMessage;
            this.layoutControlItemMessage.CustomizationFormText = "Message";
            this.layoutControlItemMessage.Location = new System.Drawing.Point(0, 68);
            this.layoutControlItemMessage.Name = "layoutControlItemMessage";
            this.layoutControlItemMessage.Size = new System.Drawing.Size(479, 316);
            this.layoutControlItemMessage.Text = "Message";
            this.layoutControlItemMessage.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemMessage.TextToControlDistance = 0;
            this.layoutControlItemMessage.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.CustomizationFormText = "splitterItem1";
            this.splitterItem1.Location = new System.Drawing.Point(0, 63);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(479, 5);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButtonClose;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(390, 384);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(89, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 384);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(390, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // XtraFormInformationMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 430);
            this.Controls.Add(this.layoutControlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XtraFormInformationMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Information Message";
            this.Load += new System.EventHandler(this.XtraFormInformationMessage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
            this.layoutControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEditMessage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditHeader.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMain;
        private DevExpress.XtraEditors.MemoEdit memoEditMessage;
        private DevExpress.XtraEditors.MemoEdit memoEditHeader;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemHeader;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMessage;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}