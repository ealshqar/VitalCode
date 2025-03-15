namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormItemDescription
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
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraFormItemDescription));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barCRUD = new DevExpress.XtraBars.Bar();
            this.barButtonItemSave = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridColumnVitalForce = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItemDescription = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroupItemDescription = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.memoEditItemDescription = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlVitalForce = new DevExpress.XtraLayout.LayoutControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupItemDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditItemDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlVitalForce)).BeginInit();
            this.layoutControlVitalForce.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barCRUD});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemSave,
            this.barButtonItemCancel});
            this.barManager.MaxItemId = 9;
            this.barManager.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barManager_ItemClick);
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
            this.barCRUD.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemCancel, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barCRUD.OptionsBar.AllowQuickCustomization = false;
            this.barCRUD.OptionsBar.DisableClose = true;
            this.barCRUD.OptionsBar.DisableCustomization = true;
            this.barCRUD.OptionsBar.DrawDragBorder = false;
            this.barCRUD.OptionsBar.UseWholeRow = true;
            this.barCRUD.Text = "Tools";
            // 
            // barButtonItemSave
            // 
            this.barButtonItemSave.Caption = "Save";
            this.barButtonItemSave.Enabled = false;
            this.barButtonItemSave.Glyph = global::Vital.UI.Properties.Resources.Test_Save;
            this.barButtonItemSave.Id = 3;
            this.barButtonItemSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.barButtonItemSave.Name = "barButtonItemSave";
            toolTipTitleItem2.Text = "Save (Ctrl + S)";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "This button will save changes done on the current test.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.barButtonItemSave.SuperTip = superToolTip2;
            this.barButtonItemSave.Tag = "Save";
            // 
            // barButtonItemCancel
            // 
            this.barButtonItemCancel.Caption = "Cancel";
            this.barButtonItemCancel.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemCancel.Glyph")));
            this.barButtonItemCancel.Id = 8;
            this.barButtonItemCancel.Name = "barButtonItemCancel";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(456, 39);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 281);
            this.barDockControlBottom.Size = new System.Drawing.Size(456, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 39);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 242);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(456, 39);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 242);
            // 
            // gridColumnVitalForce
            // 
            this.gridColumnVitalForce.Caption = "Vital Force";
            this.gridColumnVitalForce.FieldName = "Name";
            this.gridColumnVitalForce.Name = "gridColumnVitalForce";
            this.gridColumnVitalForce.Visible = true;
            this.gridColumnVitalForce.VisibleIndex = 0;
            // 
            // layoutControlItemDescription
            // 
            this.layoutControlItemDescription.CustomizationFormText = "Item Description";
            this.layoutControlItemDescription.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlItemDescription.GroupBordersVisible = false;
            this.layoutControlItemDescription.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupItemDescription});
            this.layoutControlItemDescription.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemDescription.Name = "layoutControlItemDescription";
            this.layoutControlItemDescription.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlItemDescription.Size = new System.Drawing.Size(456, 242);
            this.layoutControlItemDescription.Text = "Vital Force";
            this.layoutControlItemDescription.TextVisible = false;
            // 
            // layoutControlGroupItemDescription
            // 
            this.layoutControlGroupItemDescription.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroupItemDescription.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroupItemDescription.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroupItemDescription.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroupItemDescription.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupItemDescription.Name = "layoutControlGroupItemDescription";
            this.layoutControlGroupItemDescription.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroupItemDescription.Size = new System.Drawing.Size(452, 238);
            this.layoutControlGroupItemDescription.Text = "XItem Description";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.memoEditItemDescription;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(446, 210);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // memoEditItemDescription
            // 
            this.memoEditItemDescription.Location = new System.Drawing.Point(7, 29);
            this.memoEditItemDescription.MenuManager = this.barManager;
            this.memoEditItemDescription.Name = "memoEditItemDescription";
            this.memoEditItemDescription.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.memoEditItemDescription.Size = new System.Drawing.Size(442, 206);
            this.memoEditItemDescription.StyleController = this.layoutControlVitalForce;
            this.memoEditItemDescription.TabIndex = 6;
            // 
            // layoutControlVitalForce
            // 
            this.layoutControlVitalForce.AllowCustomizationMenu = false;
            this.layoutControlVitalForce.Controls.Add(this.memoEditItemDescription);
            this.layoutControlVitalForce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlVitalForce.Location = new System.Drawing.Point(0, 39);
            this.layoutControlVitalForce.Name = "layoutControlVitalForce";
            this.layoutControlVitalForce.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(241, 162, 250, 350);
            this.layoutControlVitalForce.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControlVitalForce.Root = this.layoutControlItemDescription;
            this.layoutControlVitalForce.Size = new System.Drawing.Size(456, 242);
            this.layoutControlVitalForce.TabIndex = 0;
            this.layoutControlVitalForce.Text = "Vital Force";
            // 
            // XtraFormItemDescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 281);
            this.Controls.Add(this.layoutControlVitalForce);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "XtraFormItemDescription";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "XItem Description";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XtraFormItemDescription_FormClosing);
            this.Load += new System.EventHandler(this.XtraFormItemDescription_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XtraFormItemDescription_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupItemDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditItemDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlVitalForce)).EndInit();
            this.layoutControlVitalForce.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraBars.BarManager barManager;
        public DevExpress.XtraBars.Bar barCRUD;
        protected DevExpress.XtraBars.BarButtonItem barButtonItemSave;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCancel;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnVitalForce;
        private DevExpress.XtraLayout.LayoutControl layoutControlVitalForce;
        private DevExpress.XtraEditors.MemoEdit memoEditItemDescription;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlItemDescription;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupItemDescription;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}