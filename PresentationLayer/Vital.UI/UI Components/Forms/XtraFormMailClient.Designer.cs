namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormMailClient
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
            this.barCRUD = new DevExpress.XtraBars.Bar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.xtraUserControlVitalRichEditMail = new Vital.UI.UI_Components.User_Controls.Modules.XtraUserControlVitalRichEdit();
            this.textEditSubject = new DevExpress.XtraEditors.TextEdit();
            this.barManagerMailClient = new DevExpress.XtraBars.BarManager(this.components);
            this.barMailClient = new DevExpress.XtraBars.Bar();
            this.barButtonItemSend = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.textEditTechnicianPhone = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEditTechnicianEmail = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerMailClient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTechnicianPhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTechnicianEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // barCRUD
            // 
            this.barCRUD.BarItemHorzIndent = 2;
            this.barCRUD.BarItemVertIndent = 2;
            this.barCRUD.BarName = "Tools";
            this.barCRUD.DockCol = 0;
            this.barCRUD.DockRow = 0;
            this.barCRUD.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barCRUD.FloatLocation = new System.Drawing.Point(59, 153);
            this.barCRUD.OptionsBar.AllowQuickCustomization = false;
            this.barCRUD.OptionsBar.DisableClose = true;
            this.barCRUD.OptionsBar.DisableCustomization = true;
            this.barCRUD.OptionsBar.DrawDragBorder = false;
            this.barCRUD.OptionsBar.UseWholeRow = true;
            this.barCRUD.Text = "Tools";
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.textEditTechnicianEmail);
            this.layoutControl1.Controls.Add(this.textEditTechnicianPhone);
            this.layoutControl1.Controls.Add(this.xtraUserControlVitalRichEditMail);
            this.layoutControl1.Controls.Add(this.textEditSubject);
            this.layoutControl1.Controls.Add(this.textEditName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup;
            this.layoutControl1.Size = new System.Drawing.Size(931, 611);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // xtraUserControlVitalRichEditMail
            // 
            this.xtraUserControlVitalRichEditMail.Location = new System.Drawing.Point(10, 150);
            this.xtraUserControlVitalRichEditMail.Name = "xtraUserControlVitalRichEditMail";
            this.xtraUserControlVitalRichEditMail.ReadOnly = true;
            this.xtraUserControlVitalRichEditMail.Size = new System.Drawing.Size(911, 451);
            this.xtraUserControlVitalRichEditMail.TabIndex = 4;
            // 
            // textEditSubject
            // 
            this.textEditSubject.Location = new System.Drawing.Point(49, 112);
            this.textEditSubject.MenuManager = this.barManagerMailClient;
            this.textEditSubject.Name = "textEditSubject";
            this.textEditSubject.Size = new System.Drawing.Size(872, 20);
            this.textEditSubject.StyleController = this.layoutControl1;
            this.textEditSubject.TabIndex = 3;
            // 
            // barManagerMailClient
            // 
            this.barManagerMailClient.AllowCustomization = false;
            this.barManagerMailClient.AllowQuickCustomization = false;
            this.barManagerMailClient.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barMailClient});
            this.barManagerMailClient.DockControls.Add(this.barDockControlTop);
            this.barManagerMailClient.DockControls.Add(this.barDockControlBottom);
            this.barManagerMailClient.DockControls.Add(this.barDockControlLeft);
            this.barManagerMailClient.DockControls.Add(this.barDockControlRight);
            this.barManagerMailClient.Form = this;
            this.barManagerMailClient.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barButtonItemSend});
            this.barManagerMailClient.MaxItemId = 2;
            // 
            // barMailClient
            // 
            this.barMailClient.BarName = "CustomMailClient";
            this.barMailClient.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.barMailClient.DockCol = 0;
            this.barMailClient.DockRow = 0;
            this.barMailClient.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barMailClient.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemSend)});
            this.barMailClient.OptionsBar.AllowQuickCustomization = false;
            this.barMailClient.OptionsBar.DisableClose = true;
            this.barMailClient.OptionsBar.DisableCustomization = true;
            this.barMailClient.OptionsBar.DrawDragBorder = false;
            this.barMailClient.OptionsBar.DrawSizeGrip = true;
            this.barMailClient.OptionsBar.UseWholeRow = true;
            this.barMailClient.Text = "Bar";
            // 
            // barButtonItemSend
            // 
            this.barButtonItemSend.Caption = "Send";
            this.barButtonItemSend.Glyph = global::Vital.UI.Properties.Resources.send_small;
            this.barButtonItemSend.Id = 1;
            this.barButtonItemSend.Name = "barButtonItemSend";
            this.barButtonItemSend.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemSend.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSend_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(931, 39);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 650);
            this.barDockControlBottom.Size = new System.Drawing.Size(931, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 39);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 611);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(931, 39);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 611);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Send";
            this.barButtonItem1.Glyph = global::Vital.UI.Properties.Resources.done;
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // textEditName
            // 
            this.textEditName.Location = new System.Drawing.Point(49, 29);
            this.textEditName.MenuManager = this.barManagerMailClient;
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new System.Drawing.Size(872, 20);
            this.textEditName.StyleController = this.layoutControl1;
            this.textEditName.TabIndex = 0;
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.CustomizationFormText = "layoutControlGroup";
            this.layoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup.GroupBordersVisible = false;
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlGroup2});
            this.layoutControlGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup.Name = "layoutControlGroup";
            this.layoutControlGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup.Size = new System.Drawing.Size(931, 611);
            this.layoutControlGroup.Text = "layoutControlGroup";
            this.layoutControlGroup.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Sender";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(931, 83);
            this.layoutControlGroup1.Text = "Technician Info";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textEditName;
            this.layoutControlItem3.CustomizationFormText = "Name";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(915, 24);
            this.layoutControlItem3.Text = "Name";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(36, 13);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Message";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.emptySpaceItem5,
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 83);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup2.Size = new System.Drawing.Size(931, 528);
            this.layoutControlGroup2.Text = "Message";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.textEditSubject;
            this.layoutControlItem4.CustomizationFormText = "Title";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(915, 24);
            this.layoutControlItem4.Text = "Subject";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(36, 13);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItem5.Location = new System.Drawing.Point(0, 24);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(1122, 14);
            this.emptySpaceItem5.Text = "emptySpaceItem5";
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.xtraUserControlVitalRichEditMail;
            this.layoutControlItem1.CustomizationFormText = "Mail";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 38);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(915, 455);
            this.layoutControlItem1.Text = "Mail";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // textEditTechnicianPhone
            // 
            this.textEditTechnicianPhone.Location = new System.Drawing.Point(507, 53);
            this.textEditTechnicianPhone.Name = "textEditTechnicianPhone";
            this.textEditTechnicianPhone.Properties.Mask.EditMask = "((\\d{3}))-(\\d{3})-(\\d{4})( ext:\\d{0,5})?";
            this.textEditTechnicianPhone.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.textEditTechnicianPhone.Properties.Mask.ShowPlaceHolders = false;
            this.textEditTechnicianPhone.Size = new System.Drawing.Size(414, 20);
            this.textEditTechnicianPhone.StyleController = this.layoutControl1;
            this.textEditTechnicianPhone.TabIndex = 2;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEditTechnicianPhone;
            this.layoutControlItem2.CustomizationFormText = "Phone";
            this.layoutControlItem2.Location = new System.Drawing.Point(458, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(457, 24);
            this.layoutControlItem2.Text = "Phone";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(36, 13);
            // 
            // textEditTechnicianEmail
            // 
            this.textEditTechnicianEmail.Location = new System.Drawing.Point(49, 53);
            this.textEditTechnicianEmail.Name = "textEditTechnicianEmail";
            this.textEditTechnicianEmail.Properties.Mask.EditMask = "([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+" +
    "))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)";
            this.textEditTechnicianEmail.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.textEditTechnicianEmail.Properties.Mask.ShowPlaceHolders = false;
            this.textEditTechnicianEmail.Size = new System.Drawing.Size(415, 20);
            this.textEditTechnicianEmail.StyleController = this.layoutControl1;
            this.textEditTechnicianEmail.TabIndex = 1;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.textEditTechnicianEmail;
            this.layoutControlItem5.CustomizationFormText = "Email";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(458, 24);
            this.layoutControlItem5.Text = "Email";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(36, 13);
            // 
            // XtraFormMailClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 650);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "XtraFormMailClient";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Feedback";
            this.Load += new System.EventHandler(this.XtraFormMailClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerMailClient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTechnicianPhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTechnicianEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraBars.Bar barCRUD;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup;
        private DevExpress.XtraBars.BarManager barManagerMailClient;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private User_Controls.Modules.XtraUserControlVitalRichEdit xtraUserControlVitalRichEditMail;
        private DevExpress.XtraEditors.TextEdit textEditSubject;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.Bar barMailClient;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSend;
        private DevExpress.XtraEditors.TextEdit textEditTechnicianPhone;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit textEditTechnicianEmail;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}