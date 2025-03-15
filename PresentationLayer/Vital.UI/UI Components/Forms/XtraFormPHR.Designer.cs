namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormPHR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraFormPHR));
            this.webBrowserPHR = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowserPHR
            // 
            this.webBrowserPHR.AllowWebBrowserDrop = false;
            this.webBrowserPHR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserPHR.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserPHR.Location = new System.Drawing.Point(0, 0);
            this.webBrowserPHR.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPHR.Name = "webBrowserPHR";
            this.webBrowserPHR.Size = new System.Drawing.Size(1141, 568);
            this.webBrowserPHR.TabIndex = 4;
            this.webBrowserPHR.WebBrowserShortcutsEnabled = false;
            // 
            // XtraFormPHR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1141, 568);
            this.Controls.Add(this.webBrowserPHR);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "XtraFormPHR";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Exclusive Free Trial - PhysiciansHR";
            this.Load += new System.EventHandler(this.XtraFormPHR_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserPHR;
    }
}