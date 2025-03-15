namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormDescriptionMessage
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
            this.simpleButtonClose = new DevExpress.XtraEditors.SimpleButton();
            this.memoEditMessage = new DevExpress.XtraEditors.MemoEdit();
            this.memoEditDescription = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroupMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemDescription = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemMessage = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
            this.layoutControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditMessage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMain
            // 
            this.layoutControlMain.AllowCustomizationMenu = false;
            this.layoutControlMain.Controls.Add(this.simpleButtonClose);
            this.layoutControlMain.Controls.Add(this.memoEditMessage);
            this.layoutControlMain.Controls.Add(this.memoEditDescription);
            this.layoutControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMain.Name = "layoutControlMain";
            this.layoutControlMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(364, 251, 250, 350);
            this.layoutControlMain.Root = this.layoutControlGroupMain;
            this.layoutControlMain.Size = new System.Drawing.Size(560, 448);
            this.layoutControlMain.TabIndex = 0;
            this.layoutControlMain.Text = "layoutControl1";
            // 
            // simpleButtonClose
            // 
            this.simpleButtonClose.Location = new System.Drawing.Point(458, 414);
            this.simpleButtonClose.Name = "simpleButtonClose";
            this.simpleButtonClose.Size = new System.Drawing.Size(90, 22);
            this.simpleButtonClose.StyleController = this.layoutControlMain;
            this.simpleButtonClose.TabIndex = 6;
            this.simpleButtonClose.Text = "Close";
            this.simpleButtonClose.Click += new System.EventHandler(this.simpleButtonClose_Click);
            // 
            // memoEditMessage
            // 
            this.memoEditMessage.EditValue = "";
            this.memoEditMessage.Location = new System.Drawing.Point(12, 12);
            this.memoEditMessage.Name = "memoEditMessage";
            this.memoEditMessage.Properties.ReadOnly = true;
            this.memoEditMessage.Size = new System.Drawing.Size(536, 47);
            this.memoEditMessage.StyleController = this.layoutControlMain;
            this.memoEditMessage.TabIndex = 5;
            // 
            // memoEditDescription
            // 
            this.memoEditDescription.Location = new System.Drawing.Point(12, 68);
            this.memoEditDescription.Name = "memoEditDescription";
            this.memoEditDescription.Properties.ReadOnly = true;
            this.memoEditDescription.Size = new System.Drawing.Size(536, 342);
            this.memoEditDescription.StyleController = this.layoutControlMain;
            this.memoEditDescription.TabIndex = 4;
            // 
            // layoutControlGroupMain
            // 
            this.layoutControlGroupMain.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroupMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMain.GroupBordersVisible = false;
            this.layoutControlGroupMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemDescription,
            this.layoutControlItemMessage,
            this.splitterItem1,
            this.layoutControlItem1,
            this.emptySpaceItem1});
            this.layoutControlGroupMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMain.Name = "Root";
            this.layoutControlGroupMain.Size = new System.Drawing.Size(560, 448);
            this.layoutControlGroupMain.Text = "Root";
            this.layoutControlGroupMain.TextVisible = false;
            // 
            // layoutControlItemDescription
            // 
            this.layoutControlItemDescription.Control = this.memoEditDescription;
            this.layoutControlItemDescription.CustomizationFormText = "layoutControlItemDescription";
            this.layoutControlItemDescription.Location = new System.Drawing.Point(0, 56);
            this.layoutControlItemDescription.Name = "layoutControlItemDescription";
            this.layoutControlItemDescription.Size = new System.Drawing.Size(540, 346);
            this.layoutControlItemDescription.Text = "layoutControlItemDescription";
            this.layoutControlItemDescription.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemDescription.TextToControlDistance = 0;
            this.layoutControlItemDescription.TextVisible = false;
            // 
            // layoutControlItemMessage
            // 
            this.layoutControlItemMessage.Control = this.memoEditMessage;
            this.layoutControlItemMessage.CustomizationFormText = "layoutControlItemMessage";
            this.layoutControlItemMessage.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemMessage.Name = "layoutControlItemMessage";
            this.layoutControlItemMessage.Size = new System.Drawing.Size(540, 51);
            this.layoutControlItemMessage.Text = "layoutControlItemMessage";
            this.layoutControlItemMessage.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemMessage.TextToControlDistance = 0;
            this.layoutControlItemMessage.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.CustomizationFormText = "splitterItem1";
            this.splitterItem1.Location = new System.Drawing.Point(0, 51);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(540, 5);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButtonClose;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(446, 402);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(94, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 402);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(446, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // XtraFormDescriptionMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 448);
            this.Controls.Add(this.layoutControlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "XtraFormDescriptionMessage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Title";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XtraFormDescriptionMessage_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
            this.layoutControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEditMessage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMain;
        private DevExpress.XtraEditors.MemoEdit memoEditDescription;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDescription;
        private DevExpress.XtraEditors.MemoEdit memoEditMessage;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMessage;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}