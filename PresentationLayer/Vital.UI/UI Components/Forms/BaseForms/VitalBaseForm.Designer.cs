namespace Vital.UI.UI_Components.BaseForms
{
    partial class VitalBaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VitalBaseForm));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem5 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem5 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip6 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem6 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem6 = new DevExpress.Utils.ToolTipItem();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barCRUD = new DevExpress.XtraBars.Bar();
            this.barButtonItemSave = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSaveAndClose = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemUndo = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDisable = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barTestStatus = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.toolTipControllerBase = new DevExpress.Utils.ToolTipController(this.components);
            this.imageListBase = new System.Windows.Forms.ImageList(this.components);
            this.dxErrorProviderMain = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProviderMain)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.AllowShowToolbarsPopup = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barCRUD,
            this.barTestStatus});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemEdit,
            this.barButtonItemDisable,
            this.barButtonItemSave,
            this.barButtonItemDelete,
            this.barButtonItemSaveAndClose,
            this.barButtonItemUndo});
            this.barManager.MaxItemId = 8;
            this.barManager.StatusBar = this.barTestStatus;
            this.barManager.ToolTipController = this.toolTipControllerBase;
            this.barManager.UseF10KeyForMenu = false;
            this.barManager.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barManagerTest_ItemClick);
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemSaveAndClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemUndo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemDisable, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemDelete, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
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
            this.barButtonItemSave.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemSave.Glyph")));
            this.barButtonItemSave.Id = 3;
            this.barButtonItemSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.barButtonItemSave.Name = "barButtonItemSave";
            toolTipTitleItem1.Text = "Save (Ctrl + S)";
            toolTipItem1.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipItem1.Appearance.Options.UseImage = true;
            toolTipItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolTipItem1.Image")));
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "This button will save changes done on the current test.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.barButtonItemSave.SuperTip = superToolTip1;
            this.barButtonItemSave.Tag = "Save";
            // 
            // barButtonItemSaveAndClose
            // 
            this.barButtonItemSaveAndClose.Caption = "Save And Close";
            this.barButtonItemSaveAndClose.Enabled = false;
            this.barButtonItemSaveAndClose.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemSaveAndClose.Glyph")));
            this.barButtonItemSaveAndClose.Id = 5;
            this.barButtonItemSaveAndClose.Name = "barButtonItemSaveAndClose";
            toolTipTitleItem2.Text = "Save And Close";
            toolTipItem2.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            toolTipItem2.Appearance.Options.UseImage = true;
            toolTipItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolTipItem2.Image")));
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "This button will save changes on the current test and close the form.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.barButtonItemSaveAndClose.SuperTip = superToolTip2;
            this.barButtonItemSaveAndClose.Tag = "SaveAndClose";
            // 
            // barButtonItemUndo
            // 
            this.barButtonItemUndo.Caption = "Undo";
            this.barButtonItemUndo.Enabled = false;
            this.barButtonItemUndo.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemUndo.Glyph")));
            this.barButtonItemUndo.Id = 6;
            this.barButtonItemUndo.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U));
            this.barButtonItemUndo.Name = "barButtonItemUndo";
            toolTipTitleItem3.Text = "Undo (Ctrl + U)";
            toolTipItem3.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            toolTipItem3.Appearance.Options.UseImage = true;
            toolTipItem3.Image = ((System.Drawing.Image)(resources.GetObject("toolTipItem3.Image")));
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "This button will undo changes done on the currrent test.";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.barButtonItemUndo.SuperTip = superToolTip3;
            this.barButtonItemUndo.Tag = "Undo";
            // 
            // barButtonItemEdit
            // 
            this.barButtonItemEdit.Caption = "Edit";
            this.barButtonItemEdit.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemEdit.Glyph")));
            this.barButtonItemEdit.Id = 1;
            this.barButtonItemEdit.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E));
            this.barButtonItemEdit.Name = "barButtonItemEdit";
            toolTipTitleItem4.Text = "Edit (Ctrl + E)";
            toolTipItem4.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image3")));
            toolTipItem4.Appearance.Options.UseImage = true;
            toolTipItem4.Image = ((System.Drawing.Image)(resources.GetObject("toolTipItem4.Image")));
            toolTipItem4.LeftIndent = 6;
            toolTipItem4.Text = "This button will enable the current test for editing.";
            superToolTip4.Items.Add(toolTipTitleItem4);
            superToolTip4.Items.Add(toolTipItem4);
            this.barButtonItemEdit.SuperTip = superToolTip4;
            this.barButtonItemEdit.Tag = "Edit";
            // 
            // barButtonItemDisable
            // 
            this.barButtonItemDisable.Caption = "Disable";
            this.barButtonItemDisable.Enabled = false;
            this.barButtonItemDisable.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemDisable.Glyph")));
            this.barButtonItemDisable.Id = 2;
            this.barButtonItemDisable.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D));
            this.barButtonItemDisable.Name = "barButtonItemDisable";
            toolTipTitleItem5.Text = "Disable (Ctrl + D)";
            toolTipItem5.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image4")));
            toolTipItem5.Appearance.Options.UseImage = true;
            toolTipItem5.Image = ((System.Drawing.Image)(resources.GetObject("toolTipItem5.Image")));
            toolTipItem5.LeftIndent = 6;
            toolTipItem5.Text = "This button will disable editing of the current test.";
            superToolTip5.Items.Add(toolTipTitleItem5);
            superToolTip5.Items.Add(toolTipItem5);
            this.barButtonItemDisable.SuperTip = superToolTip5;
            this.barButtonItemDisable.Tag = "Disable";
            // 
            // barButtonItemDelete
            // 
            this.barButtonItemDelete.Caption = "Delete";
            this.barButtonItemDelete.Enabled = false;
            this.barButtonItemDelete.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemDelete.Glyph")));
            this.barButtonItemDelete.Id = 4;
            this.barButtonItemDelete.Name = "barButtonItemDelete";
            toolTipTitleItem6.Text = "Delete";
            toolTipItem6.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image5")));
            toolTipItem6.Appearance.Options.UseImage = true;
            toolTipItem6.Image = ((System.Drawing.Image)(resources.GetObject("toolTipItem6.Image")));
            toolTipItem6.LeftIndent = 6;
            toolTipItem6.Text = "This button will delete the current test.";
            superToolTip6.Items.Add(toolTipTitleItem6);
            superToolTip6.Items.Add(toolTipItem6);
            this.barButtonItemDelete.SuperTip = superToolTip6;
            this.barButtonItemDelete.Tag = "Delete";
            // 
            // barTestStatus
            // 
            this.barTestStatus.BarName = "Status bar";
            this.barTestStatus.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.barTestStatus.DockCol = 0;
            this.barTestStatus.DockRow = 0;
            this.barTestStatus.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.barTestStatus.OptionsBar.AllowQuickCustomization = false;
            this.barTestStatus.OptionsBar.DisableClose = true;
            this.barTestStatus.OptionsBar.DisableCustomization = true;
            this.barTestStatus.OptionsBar.DrawDragBorder = false;
            this.barTestStatus.OptionsBar.UseWholeRow = true;
            this.barTestStatus.Text = "Status bar";
            this.barTestStatus.Visible = false;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1276, 39);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 651);
            this.barDockControlBottom.Size = new System.Drawing.Size(1276, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 39);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 612);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1276, 39);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 612);
            // 
            // toolTipControllerBase
            // 
            this.toolTipControllerBase.AllowHtmlText = true;
            this.toolTipControllerBase.AutoPopDelay = 7000;
            this.toolTipControllerBase.CloseOnClick = DevExpress.Utils.DefaultBoolean.True;
            this.toolTipControllerBase.IconSize = DevExpress.Utils.ToolTipIconSize.Large;
            this.toolTipControllerBase.ImageIndex = 0;
            this.toolTipControllerBase.ImageList = this.imageListBase;
            this.toolTipControllerBase.ShowBeak = true;
            this.toolTipControllerBase.ToolTipStyle = DevExpress.Utils.ToolTipStyle.Windows7;
            // 
            // imageListBase
            // 
            this.imageListBase.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListBase.ImageStream")));
            this.imageListBase.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListBase.Images.SetKeyName(0, "1365685492_ktip.png");
            // 
            // dxErrorProviderMain
            // 
            this.dxErrorProviderMain.ContainerControl = this;
            // 
            // VitalBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 674);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.KeyPreview = true;
            this.Name = "VitalBaseForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Deactivate += new System.EventHandler(this.VitalBaseForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VitalBaseForm_FormClosing);
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.Shown += new System.EventHandler(this.VitalBaseForm_Shown);
            this.LocationChanged += new System.EventHandler(this.VitalBaseForm_LocationChanged);
            this.Resize += new System.EventHandler(this.VitalBaseForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProviderMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraBars.BarButtonItem barButtonItemEdit;
        protected DevExpress.XtraBars.BarButtonItem barButtonItemDisable;
        protected DevExpress.XtraBars.BarButtonItem barButtonItemSave;
        protected DevExpress.XtraBars.BarButtonItem barButtonItemSaveAndClose;
        protected DevExpress.XtraBars.BarButtonItem barButtonItemUndo;
        public DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        private DevExpress.XtraBars.Bar barTestStatus;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.Bar barCRUD;
        public DevExpress.XtraBars.BarManager barManager;
        public DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProviderMain;
        private DevExpress.Utils.ToolTipController toolTipControllerBase;
        private System.Windows.Forms.ImageList imageListBase;


    }
}
