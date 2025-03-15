namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormRatios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraFormRatios));
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barCRUD = new DevExpress.XtraBars.Bar();
            this.barButtonItemDone = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barTestStatus = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.layoutControlRatio = new DevExpress.XtraLayout.LayoutControl();
            this.labelControlTip = new DevExpress.XtraEditors.LabelControl();
            this.gridControlRatio = new DevExpress.XtraGrid.GridControl();
            this.gridViewRatio = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.repositoryItemCheckEditHasChilds = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.simpleButtonNextGroup = new DevExpress.XtraEditors.SimpleButton();
            this.xtraUserControlReadingGaugeRatio = new Vital.UI.UI_Components.User_Controls.Modules.XtraUserControlReadingGauge();
            this.simpleButtonPreviousGroup = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroupMajorRatio = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemReadingGauge = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItemItemName = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.gridColumnVitalForce = new DevExpress.XtraGrid.Columns.GridColumn();
            this.timerAutoClose = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlRatio)).BeginInit();
            this.layoutControlRatio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditHasChilds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMajorRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReadingGauge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barCRUD,
            this.barTestStatus});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemDone,
            this.barButtonItemCancel});
            this.barManager.MaxItemId = 9;
            this.barManager.StatusBar = this.barTestStatus;
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemDone, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemCancel, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barCRUD.OptionsBar.AllowQuickCustomization = false;
            this.barCRUD.OptionsBar.DisableClose = true;
            this.barCRUD.OptionsBar.DisableCustomization = true;
            this.barCRUD.OptionsBar.DrawDragBorder = false;
            this.barCRUD.OptionsBar.UseWholeRow = true;
            this.barCRUD.Text = "Tools";
            // 
            // barButtonItemDone
            // 
            this.barButtonItemDone.Caption = "Done";
            this.barButtonItemDone.Enabled = false;
            this.barButtonItemDone.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemDone.Glyph")));
            this.barButtonItemDone.Id = 3;
            this.barButtonItemDone.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.barButtonItemDone.Name = "barButtonItemDone";
            toolTipTitleItem2.Text = "Save (Ctrl + S)";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "This button will save changes done on the current test.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.barButtonItemDone.SuperTip = superToolTip2;
            this.barButtonItemDone.Tag = "Save";
            // 
            // barButtonItemCancel
            // 
            this.barButtonItemCancel.Caption = "Cancel";
            this.barButtonItemCancel.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemCancel.Glyph")));
            this.barButtonItemCancel.Id = 8;
            this.barButtonItemCancel.Name = "barButtonItemCancel";
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
            this.barDockControlTop.Size = new System.Drawing.Size(484, 39);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 478);
            this.barDockControlBottom.Size = new System.Drawing.Size(484, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 39);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 439);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(484, 39);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 439);
            // 
            // layoutControlRatio
            // 
            this.layoutControlRatio.AllowCustomizationMenu = false;
            this.layoutControlRatio.Controls.Add(this.labelControlTip);
            this.layoutControlRatio.Controls.Add(this.gridControlRatio);
            this.layoutControlRatio.Controls.Add(this.simpleButtonNextGroup);
            this.layoutControlRatio.Controls.Add(this.xtraUserControlReadingGaugeRatio);
            this.layoutControlRatio.Controls.Add(this.simpleButtonPreviousGroup);
            this.layoutControlRatio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlRatio.Location = new System.Drawing.Point(0, 39);
            this.layoutControlRatio.Name = "layoutControlRatio";
            this.layoutControlRatio.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(241, 162, 250, 350);
            this.layoutControlRatio.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControlRatio.Root = this.layoutControlGroupMajorRatio;
            this.layoutControlRatio.Size = new System.Drawing.Size(484, 439);
            this.layoutControlRatio.TabIndex = 0;
            this.layoutControlRatio.Text = "Vital Force";
            // 
            // labelControlTip
            // 
            this.labelControlTip.AllowHtmlString = true;
            this.labelControlTip.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelControlTip.Location = new System.Drawing.Point(12, 12);
            this.labelControlTip.Name = "labelControlTip";
            this.labelControlTip.Size = new System.Drawing.Size(460, 19);
            this.labelControlTip.StyleController = this.layoutControlRatio;
            this.labelControlTip.TabIndex = 6;
            this.labelControlTip.Text = "Press [Enter] continue after finding the ratio.";
            // 
            // gridControlRatio
            // 
            this.gridControlRatio.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.False;
            this.gridControlRatio.Location = new System.Drawing.Point(12, 83);
            this.gridControlRatio.MainView = this.gridViewRatio;
            this.gridControlRatio.Name = "gridControlRatio";
            this.gridControlRatio.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageEdit1,
            this.repositoryItemCheckEditHasChilds});
            this.gridControlRatio.Size = new System.Drawing.Size(314, 310);
            this.gridControlRatio.TabIndex = 1;
            this.gridControlRatio.TabStop = false;
            this.gridControlRatio.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRatio});
            // 
            // gridViewRatio
            // 
            this.gridViewRatio.Appearance.FocusedRow.BackColor = System.Drawing.Color.Orange;
            this.gridViewRatio.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FloralWhite;
            this.gridViewRatio.Appearance.FocusedRow.BorderColor = System.Drawing.Color.Silver;
            this.gridViewRatio.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gridViewRatio.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewRatio.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gridViewRatio.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridViewRatio.Appearance.SelectedRow.BackColor = System.Drawing.Color.Orange;
            this.gridViewRatio.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FloralWhite;
            this.gridViewRatio.Appearance.SelectedRow.BorderColor = System.Drawing.Color.Silver;
            this.gridViewRatio.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gridViewRatio.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridViewRatio.Appearance.SelectedRow.Options.UseBorderColor = true;
            this.gridViewRatio.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridViewRatio.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnItem});
            this.gridViewRatio.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridViewRatio.GridControl = this.gridControlRatio;
            this.gridViewRatio.Name = "gridViewRatio";
            this.gridViewRatio.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridViewRatio.OptionsBehavior.Editable = false;
            this.gridViewRatio.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridViewRatio.OptionsBehavior.ReadOnly = true;
            this.gridViewRatio.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewRatio.OptionsCustomization.AllowColumnResizing = false;
            this.gridViewRatio.OptionsCustomization.AllowFilter = false;
            this.gridViewRatio.OptionsCustomization.AllowGroup = false;
            this.gridViewRatio.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewRatio.OptionsCustomization.AllowSort = false;
            this.gridViewRatio.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewRatio.OptionsDetail.SmartDetailHeight = true;
            this.gridViewRatio.OptionsMenu.EnableColumnMenu = false;
            this.gridViewRatio.OptionsNavigation.AutoMoveRowFocus = false;
            this.gridViewRatio.OptionsNavigation.UseTabKey = false;
            this.gridViewRatio.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewRatio.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewRatio.OptionsSelection.MultiSelect = true;
            this.gridViewRatio.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewRatio.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewRatio.OptionsView.ShowGroupPanel = false;
            this.gridViewRatio.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridViewRatio_CustomDrawCell);
            this.gridViewRatio.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridViewRatio_SelectionChanged);
            this.gridViewRatio.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewRatio_FocusedRowChanged);
            // 
            // gridColumnItem
            // 
            this.gridColumnItem.Caption = "Ratios";
            this.gridColumnItem.FieldName = "Name";
            this.gridColumnItem.Name = "gridColumnItem";
            this.gridColumnItem.OptionsColumn.AllowEdit = false;
            this.gridColumnItem.OptionsColumn.ReadOnly = true;
            this.gridColumnItem.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumnItem.Visible = true;
            this.gridColumnItem.VisibleIndex = 0;
            this.gridColumnItem.Width = 161;
            // 
            // repositoryItemImageEdit1
            // 
            this.repositoryItemImageEdit1.AutoHeight = false;
            this.repositoryItemImageEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageEdit1.Name = "repositoryItemImageEdit1";
            // 
            // repositoryItemCheckEditHasChilds
            // 
            this.repositoryItemCheckEditHasChilds.AutoHeight = false;
            this.repositoryItemCheckEditHasChilds.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.repositoryItemCheckEditHasChilds.Name = "repositoryItemCheckEditHasChilds";
            this.repositoryItemCheckEditHasChilds.PictureChecked = ((System.Drawing.Image)(resources.GetObject("repositoryItemCheckEditHasChilds.PictureChecked")));
            // 
            // simpleButtonNextGroup
            // 
            this.simpleButtonNextGroup.AllowFocus = false;
            this.simpleButtonNextGroup.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonNextGroup.Image")));
            this.simpleButtonNextGroup.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonNextGroup.Location = new System.Drawing.Point(12, 397);
            this.simpleButtonNextGroup.Name = "simpleButtonNextGroup";
            this.simpleButtonNextGroup.Size = new System.Drawing.Size(143, 30);
            this.simpleButtonNextGroup.StyleController = this.layoutControlRatio;
            this.simpleButtonNextGroup.TabIndex = 2;
            this.simpleButtonNextGroup.TabStop = false;
            this.simpleButtonNextGroup.Click += new System.EventHandler(this.simpleButtonNextGroup_Click);
            // 
            // xtraUserControlReadingGaugeRatio
            // 
            this.xtraUserControlReadingGaugeRatio.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraUserControlReadingGaugeRatio.Appearance.Options.UseBackColor = true;
            this.xtraUserControlReadingGaugeRatio.Location = new System.Drawing.Point(330, 59);
            this.xtraUserControlReadingGaugeRatio.Name = "xtraUserControlReadingGaugeRatio";
            this.xtraUserControlReadingGaugeRatio.ReadingValueTimeLine = 0F;
            this.xtraUserControlReadingGaugeRatio.ShowLocationGauge = false;
            this.xtraUserControlReadingGaugeRatio.ShowYesNoLabel = true;
            this.xtraUserControlReadingGaugeRatio.Size = new System.Drawing.Size(142, 368);
            this.xtraUserControlReadingGaugeRatio.TabIndex = 0;
            this.xtraUserControlReadingGaugeRatio.TabStop = false;
            // 
            // simpleButtonPreviousGroup
            // 
            this.simpleButtonPreviousGroup.AllowFocus = false;
            this.simpleButtonPreviousGroup.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonPreviousGroup.Image")));
            this.simpleButtonPreviousGroup.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonPreviousGroup.Location = new System.Drawing.Point(159, 397);
            this.simpleButtonPreviousGroup.Name = "simpleButtonPreviousGroup";
            this.simpleButtonPreviousGroup.Size = new System.Drawing.Size(167, 30);
            this.simpleButtonPreviousGroup.StyleController = this.layoutControlRatio;
            this.simpleButtonPreviousGroup.TabIndex = 3;
            this.simpleButtonPreviousGroup.TabStop = false;
            this.simpleButtonPreviousGroup.Click += new System.EventHandler(this.simpleButtonPreviousGroup_Click);
            // 
            // layoutControlGroupMajorRatio
            // 
            this.layoutControlGroupMajorRatio.CustomizationFormText = "Vital Force";
            this.layoutControlGroupMajorRatio.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMajorRatio.GroupBordersVisible = false;
            this.layoutControlGroupMajorRatio.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemReadingGauge,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.simpleLabelItemItemName,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.layoutControlGroupMajorRatio.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMajorRatio.Name = "layoutControlGroupMajorRatio";
            this.layoutControlGroupMajorRatio.Size = new System.Drawing.Size(484, 439);
            this.layoutControlGroupMajorRatio.Text = "Vital Force";
            this.layoutControlGroupMajorRatio.TextVisible = false;
            // 
            // layoutControlItemReadingGauge
            // 
            this.layoutControlItemReadingGauge.Control = this.xtraUserControlReadingGaugeRatio;
            this.layoutControlItemReadingGauge.CustomizationFormText = "Reading Gauge";
            this.layoutControlItemReadingGauge.Location = new System.Drawing.Point(318, 47);
            this.layoutControlItemReadingGauge.MaxSize = new System.Drawing.Size(146, 0);
            this.layoutControlItemReadingGauge.MinSize = new System.Drawing.Size(146, 24);
            this.layoutControlItemReadingGauge.Name = "layoutControlItemReadingGauge";
            this.layoutControlItemReadingGauge.Size = new System.Drawing.Size(146, 372);
            this.layoutControlItemReadingGauge.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemReadingGauge.Text = "Reading Gauge";
            this.layoutControlItemReadingGauge.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemReadingGauge.TextToControlDistance = 0;
            this.layoutControlItemReadingGauge.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButtonPreviousGroup;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(147, 385);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(38, 34);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(171, 34);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButtonNextGroup;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 385);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(38, 34);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(147, 34);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // simpleLabelItemItemName
            // 
            this.simpleLabelItemItemName.AllowHotTrack = false;
            this.simpleLabelItemItemName.CustomizationFormText = "Item Name";
            this.simpleLabelItemItemName.Location = new System.Drawing.Point(0, 47);
            this.simpleLabelItemItemName.MinSize = new System.Drawing.Size(81, 17);
            this.simpleLabelItemItemName.Name = "simpleLabelItemItemName";
            this.simpleLabelItemItemName.Size = new System.Drawing.Size(318, 24);
            this.simpleLabelItemItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItemItemName.Text = "Item Name";
            this.simpleLabelItemItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.simpleLabelItemItemName.TextSize = new System.Drawing.Size(77, 13);
            this.simpleLabelItemItemName.TrimClientAreaToControl = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridControlRatio;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 71);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(318, 314);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.labelControlTip;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(438, 23);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(464, 23);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 23);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(464, 24);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // gridColumnVitalForce
            // 
            this.gridColumnVitalForce.Caption = "Vital Force";
            this.gridColumnVitalForce.FieldName = "Name";
            this.gridColumnVitalForce.Name = "gridColumnVitalForce";
            this.gridColumnVitalForce.Visible = true;
            this.gridColumnVitalForce.VisibleIndex = 0;
            // 
            // timerAutoClose
            // 
            this.timerAutoClose.Tick += new System.EventHandler(this.timerAutoClose_Tick);
            // 
            // XtraFormRatios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 501);
            this.Controls.Add(this.layoutControlRatio);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(500, 540);
            this.MinimumSize = new System.Drawing.Size(500, 540);
            this.Name = "XtraFormRatios";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ratios";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XtraFormRatio_FormClosing);
            this.Load += new System.EventHandler(this.XtraFormRatio_Load);
            this.Shown += new System.EventHandler(this.XtraFormRatio_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XtraFormRatio_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.XtraFormRatio_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlRatio)).EndInit();
            this.layoutControlRatio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditHasChilds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMajorRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReadingGauge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraBars.BarManager barManager;
        public DevExpress.XtraBars.Bar barCRUD;
        protected DevExpress.XtraBars.BarButtonItem barButtonItemDone;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCancel;
        private DevExpress.XtraBars.Bar barTestStatus;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraLayout.LayoutControl layoutControlRatio;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMajorRatio;        
        private User_Controls.Modules.XtraUserControlReadingGauge xtraUserControlReadingGaugeRatio;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemReadingGauge;
        private DevExpress.XtraEditors.SimpleButton simpleButtonNextGroup;
        private DevExpress.XtraEditors.SimpleButton simpleButtonPreviousGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemItemName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        public DevExpress.XtraGrid.GridControl gridControlRatio;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRatio;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditHasChilds;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit repositoryItemImageEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnVitalForce;
        private DevExpress.XtraEditors.LabelControl labelControlTip;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.Timer timerAutoClose;
    }
}