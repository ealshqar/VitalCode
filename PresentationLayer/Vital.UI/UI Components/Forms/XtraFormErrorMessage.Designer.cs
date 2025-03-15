using System.Windows.Forms;

namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormErrorMessage
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
            this.layoutControlError = new DevExpress.XtraLayout.LayoutControl();
            this.memoEditHeaderMessage = new DevExpress.XtraEditors.MemoEdit();
            this.simpleButtonCopy = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonClose = new DevExpress.XtraEditors.SimpleButton();
            this.memoEditError = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroupError = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCopy = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemErrorText = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlError)).BeginInit();
            this.layoutControlError.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditHeaderMessage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditError.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemErrorText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlError
            // 
            this.layoutControlError.AllowCustomizationMenu = false;
            this.layoutControlError.AutoScroll = false;
            this.layoutControlError.Controls.Add(this.memoEditHeaderMessage);
            this.layoutControlError.Controls.Add(this.simpleButtonCopy);
            this.layoutControlError.Controls.Add(this.simpleButtonClose);
            this.layoutControlError.Controls.Add(this.memoEditError);
            this.layoutControlError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlError.Location = new System.Drawing.Point(0, 0);
            this.layoutControlError.Name = "layoutControlError";
            this.layoutControlError.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(703, 199, 250, 350);
            this.layoutControlError.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControlError.Root = this.layoutControlGroupError;
            this.layoutControlError.Size = new System.Drawing.Size(575, 410);
            this.layoutControlError.TabIndex = 0;
            this.layoutControlError.Text = "layoutControl1";
            // 
            // memoEditHeaderMessage
            // 
            this.memoEditHeaderMessage.EditValue = "An error occured";
            this.memoEditHeaderMessage.Location = new System.Drawing.Point(41, 12);
            this.memoEditHeaderMessage.Name = "memoEditHeaderMessage";
            this.memoEditHeaderMessage.Properties.ReadOnly = true;
            this.memoEditHeaderMessage.Size = new System.Drawing.Size(522, 41);
            this.memoEditHeaderMessage.StyleController = this.layoutControlError;
            this.memoEditHeaderMessage.TabIndex = 5;
            // 
            // simpleButtonCopy
            // 
            this.simpleButtonCopy.Location = new System.Drawing.Point(380, 376);
            this.simpleButtonCopy.Name = "simpleButtonCopy";
            this.simpleButtonCopy.Size = new System.Drawing.Size(183, 22);
            this.simpleButtonCopy.StyleController = this.layoutControlError;
            this.simpleButtonCopy.TabIndex = 6;
            this.simpleButtonCopy.Text = "Copy Error to Clipboard";
            this.simpleButtonCopy.Click += new System.EventHandler(this.simpleButtonCopy_Click);
            // 
            // simpleButtonClose
            // 
            this.simpleButtonClose.Location = new System.Drawing.Point(12, 376);
            this.simpleButtonClose.Name = "simpleButtonClose";
            this.simpleButtonClose.Size = new System.Drawing.Size(131, 22);
            this.simpleButtonClose.StyleController = this.layoutControlError;
            this.simpleButtonClose.TabIndex = 5;
            this.simpleButtonClose.Text = "Close";
            this.simpleButtonClose.Click += new System.EventHandler(this.simpleButtonClose_Click);
            // 
            // memoEditError
            // 
            this.memoEditError.Location = new System.Drawing.Point(12, 62);
            this.memoEditError.Name = "memoEditError";
            this.memoEditError.Properties.ReadOnly = true;
            this.memoEditError.Size = new System.Drawing.Size(551, 310);
            this.memoEditError.StyleController = this.layoutControlError;
            this.memoEditError.TabIndex = 4;
            // 
            // layoutControlGroupError
            // 
            this.layoutControlGroupError.CustomizationFormText = "Root";
            this.layoutControlGroupError.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupError.ExpandOnDoubleClick = true;
            this.layoutControlGroupError.GroupBordersVisible = false;
            this.layoutControlGroupError.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemClose,
            this.layoutControlItemCopy,
            this.layoutControlItemErrorText,
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.splitterItem1});
            this.layoutControlGroupError.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupError.Name = "Root";
            this.layoutControlGroupError.Size = new System.Drawing.Size(575, 410);
            this.layoutControlGroupError.Text = "Root";
            this.layoutControlGroupError.TextVisible = false;
            // 
            // layoutControlItemClose
            // 
            this.layoutControlItemClose.Control = this.simpleButtonClose;
            this.layoutControlItemClose.CustomizationFormText = "Close";
            this.layoutControlItemClose.Location = new System.Drawing.Point(0, 364);
            this.layoutControlItemClose.Name = "layoutControlItemClose";
            this.layoutControlItemClose.Size = new System.Drawing.Size(135, 26);
            this.layoutControlItemClose.Text = "Close";
            this.layoutControlItemClose.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemClose.TextToControlDistance = 0;
            this.layoutControlItemClose.TextVisible = false;
            // 
            // layoutControlItemCopy
            // 
            this.layoutControlItemCopy.Control = this.simpleButtonCopy;
            this.layoutControlItemCopy.CustomizationFormText = "Copy to Clipboard";
            this.layoutControlItemCopy.Location = new System.Drawing.Point(368, 364);
            this.layoutControlItemCopy.Name = "layoutControlItemCopy";
            this.layoutControlItemCopy.Size = new System.Drawing.Size(187, 26);
            this.layoutControlItemCopy.Text = "Copy to Clipboard";
            this.layoutControlItemCopy.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCopy.TextToControlDistance = 0;
            this.layoutControlItemCopy.TextVisible = false;
            // 
            // layoutControlItemErrorText
            // 
            this.layoutControlItemErrorText.Control = this.memoEditError;
            this.layoutControlItemErrorText.CustomizationFormText = "Error Text";
            this.layoutControlItemErrorText.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItemErrorText.Name = "layoutControlItemErrorText";
            this.layoutControlItemErrorText.Size = new System.Drawing.Size(555, 314);
            this.layoutControlItemErrorText.Text = "Error Text";
            this.layoutControlItemErrorText.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemErrorText.TextToControlDistance = 0;
            this.layoutControlItemErrorText.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(135, 364);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(233, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.memoEditHeaderMessage;
            this.layoutControlItem1.CustomizationFormText = " ";
            this.layoutControlItem1.Image = global::Vital.UI.Properties.Resources.Error;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(555, 45);
            this.layoutControlItem1.Text = " ";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(24, 16);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.CustomizationFormText = "splitterItem1";
            this.splitterItem1.Location = new System.Drawing.Point(0, 45);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(555, 5);
            // 
            // XtraFormErrorMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 410);
            this.Controls.Add(this.layoutControlError);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(849, 624);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(591, 448);
            this.Name = "XtraFormErrorMessage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "An Error Occured";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlError)).EndInit();
            this.layoutControlError.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEditHeaderMessage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditError.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCopy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemErrorText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlError;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupError;
        private DevExpress.XtraEditors.MemoEdit memoEditError;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemClose;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCopy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCopy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemErrorText;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.MemoEdit memoEditHeaderMessage;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
    }
}