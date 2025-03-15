namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormDisclaimer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraFormDisclaimer));
            this.layoutControlDisclaimer = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonAcceptDisclaimer = new DevExpress.XtraEditors.SimpleButton();
            this.memoEditText = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemText = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.defaultLookAndFeelDisclaimer = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlDisclaimer)).BeginInit();
            this.layoutControlDisclaimer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlDisclaimer
            // 
            this.layoutControlDisclaimer.AllowCustomizationMenu = false;
            this.layoutControlDisclaimer.Controls.Add(this.simpleButtonAcceptDisclaimer);
            this.layoutControlDisclaimer.Controls.Add(this.memoEditText);
            this.layoutControlDisclaimer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlDisclaimer.Location = new System.Drawing.Point(0, 0);
            this.layoutControlDisclaimer.Name = "layoutControlDisclaimer";
            this.layoutControlDisclaimer.Root = this.layoutControlGroup1;
            this.layoutControlDisclaimer.Size = new System.Drawing.Size(566, 370);
            this.layoutControlDisclaimer.TabIndex = 0;
            this.layoutControlDisclaimer.Text = "layoutControl1";
            // 
            // simpleButtonAcceptDisclaimer
            // 
            this.simpleButtonAcceptDisclaimer.Location = new System.Drawing.Point(12, 336);
            this.simpleButtonAcceptDisclaimer.Name = "simpleButtonAcceptDisclaimer";
            this.simpleButtonAcceptDisclaimer.Size = new System.Drawing.Size(542, 22);
            this.simpleButtonAcceptDisclaimer.StyleController = this.layoutControlDisclaimer;
            this.simpleButtonAcceptDisclaimer.TabIndex = 1;
            this.simpleButtonAcceptDisclaimer.Text = "Accept Disclaimer";
            this.simpleButtonAcceptDisclaimer.Click += new System.EventHandler(this.simpleButtonAcceptDisclaimer_Click);
            // 
            // memoEditText
            // 
            this.memoEditText.EditValue = resources.GetString("memoEditText.EditValue");
            this.memoEditText.Location = new System.Drawing.Point(12, 12);
            this.memoEditText.Name = "memoEditText";
            this.memoEditText.Properties.ReadOnly = true;
            this.memoEditText.Size = new System.Drawing.Size(542, 320);
            this.memoEditText.StyleController = this.layoutControlDisclaimer;
            this.memoEditText.TabIndex = 0;
            this.memoEditText.TabStop = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemText,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(566, 370);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemText
            // 
            this.layoutControlItemText.Control = this.memoEditText;
            this.layoutControlItemText.CustomizationFormText = "Disclaimer Text";
            this.layoutControlItemText.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemText.Name = "layoutControlItemText";
            this.layoutControlItemText.Size = new System.Drawing.Size(546, 324);
            this.layoutControlItemText.Text = "Disclaimer Text";
            this.layoutControlItemText.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemText.TextToControlDistance = 0;
            this.layoutControlItemText.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButtonAcceptDisclaimer;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 324);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(546, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // defaultLookAndFeelDisclaimer
            // 
            this.defaultLookAndFeelDisclaimer.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // XtraFormDisclaimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 370);
            this.Controls.Add(this.layoutControlDisclaimer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XtraFormDisclaimer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Disclaimer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XtraFormDisclaimer_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlDisclaimer)).EndInit();
            this.layoutControlDisclaimer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEditText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlDisclaimer;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.MemoEdit memoEditText;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemText;
        private DevExpress.XtraEditors.SimpleButton simpleButtonAcceptDisclaimer;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeelDisclaimer;
    }
}