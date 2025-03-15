namespace Vital.UI
{
    partial class XtraTabPageEntity
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer entities = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (entities != null))
            {
                entities.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraTabPageEntity));
            this.toolTipControllerMain = new DevExpress.Utils.ToolTipController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.errorImageList = new System.Windows.Forms.ImageList(this.components);
            this.EntityBarManager = new DevExpress.XtraBars.BarManager(this.components);
            this.buildingMainMenubar = new DevExpress.XtraBars.Bar();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.buildingStatusBar = new DevExpress.XtraBars.Bar();
            this.barStaticItemEditInfo = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dxErrorProviderMain = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.toolTipControllerStatic = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.EntityBarManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProviderMain)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTipControllerMain
            // 
            this.toolTipControllerMain.Rounded = true;
            this.toolTipControllerMain.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(846, 22);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 518);
            this.barDockControlBottom.Size = new System.Drawing.Size(846, 59);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 22);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 496);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(846, 22);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 496);
            // 
            // errorImageList
            // 
            this.errorImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("errorImageList.ImageStream")));
            this.errorImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.errorImageList.Images.SetKeyName(0, "messageboxerror.ico");
            // 
            // EntityBarManager
            // 
            this.EntityBarManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.buildingMainMenubar,
            this.buildingStatusBar});
            this.EntityBarManager.DockControls.Add(this.barDockControlTop);
            this.EntityBarManager.DockControls.Add(this.barDockControlBottom);
            this.EntityBarManager.DockControls.Add(this.barDockControlLeft);
            this.EntityBarManager.DockControls.Add(this.barDockControlRight);
            this.EntityBarManager.UseF10KeyForMenu = false;
            this.EntityBarManager.Form = this;
            this.EntityBarManager.Images = this.errorImageList;
            this.EntityBarManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItem1,
            this.barStaticItemEditInfo,
            this.barStaticItem2});
            this.EntityBarManager.MainMenu = this.buildingMainMenubar;
            this.EntityBarManager.MaxItemId = 15;
            this.EntityBarManager.StatusBar = this.buildingStatusBar;
            // 
            // buildingMainMenubar
            // 
            this.buildingMainMenubar.BarName = "Main menu";
            this.buildingMainMenubar.DockCol = 0;
            this.buildingMainMenubar.DockRow = 0;
            this.buildingMainMenubar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.buildingMainMenubar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1)});
            this.buildingMainMenubar.OptionsBar.MultiLine = true;
            this.buildingMainMenubar.OptionsBar.UseWholeRow = true;
            this.buildingMainMenubar.Text = "Main menu";
            this.buildingMainMenubar.Visible = false;
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Header Information";
            this.barStaticItem1.Id = 0;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // buildingStatusBar
            // 
            this.buildingStatusBar.BarName = "Status bar";
            this.buildingStatusBar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.buildingStatusBar.DockCol = 0;
            this.buildingStatusBar.DockRow = 0;
            this.buildingStatusBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.buildingStatusBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemEditInfo)});
            this.buildingStatusBar.OptionsBar.AllowQuickCustomization = false;
            this.buildingStatusBar.OptionsBar.DrawDragBorder = false;
            this.buildingStatusBar.OptionsBar.MultiLine = true;
            this.buildingStatusBar.OptionsBar.UseWholeRow = true;
            this.buildingStatusBar.Text = "Status bar";
            this.buildingStatusBar.Visible = false;
            // 
            // barStaticItemEditInfo
            // 
            this.barStaticItemEditInfo.Caption = "This tab currently is not editable, another related tab is being edited now, plea" +
                "se finish changes on that tab first to enable this one.";
            this.barStaticItemEditInfo.Glyph = ((System.Drawing.Image)(resources.GetObject("barStaticItemEditInfo.Glyph")));
            this.barStaticItemEditInfo.Id = 11;
            this.barStaticItemEditInfo.Name = "barStaticItemEditInfo";
            this.barStaticItemEditInfo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barStaticItemEditInfo.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Caption = "barStaticItem2";
            this.barStaticItem2.Id = 13;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // dxErrorProviderMain
            // 
            this.dxErrorProviderMain.ContainerControl = this;
            // 
            // toolTipControllerStatic
            // 
            this.toolTipControllerStatic.Rounded = true;
            this.toolTipControllerStatic.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
            // 
            // XtraTabPageEntity
            // 
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.LookAndFeel.SkinName = "Office 2010 Blue";
            this.Name = "XtraTabPageEntity";
            this.Size = new System.Drawing.Size(846, 577);
            ((System.ComponentModel.ISupportInitialize)(this.EntityBarManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProviderMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.IContainer components;
        private DevExpress.XtraBars.Bar buildingMainMenubar;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.Bar buildingStatusBar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        public DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProviderMain;
        public DevExpress.Utils.ToolTipController toolTipControllerMain;
        public DevExpress.XtraBars.BarManager EntityBarManager;
        public System.Windows.Forms.ImageList errorImageList;
        private DevExpress.XtraBars.BarStaticItem barStaticItemEditInfo;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.Utils.ToolTipController toolTipControllerStatic;
    }
}
