namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormVitalForce
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraFormVitalForce));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barCRUD = new DevExpress.XtraBars.Bar();
            this.barButtonItemDone = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barTestStatus = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.layoutControlVitalForce = new DevExpress.XtraLayout.LayoutControl();
            this.labelControlTip = new DevExpress.XtraEditors.LabelControl();
            this.gridControlVitalForce = new DevExpress.XtraGrid.GridControl();
            this.gridViewVitalForce = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.repositoryItemCheckEditHasChilds = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.spinEditItemaPerGroup = new DevExpress.XtraEditors.SpinEdit();
            this.simpleButtonNextGroup = new DevExpress.XtraEditors.SimpleButton();
            this.xtraUserControlReadingGaugeVitalForce = new Vital.UI.UI_Components.User_Controls.Modules.XtraUserControlReadingGauge();
            this.simpleButtonPreviousGroup = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroupMajorVitalForce = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemReadingGauge = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemItemsPerGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItemItemName = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.gridColumnVitalForce = new DevExpress.XtraGrid.Columns.GridColumn();
            this.timerAutoClose = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlVitalForce)).BeginInit();
            this.layoutControlVitalForce.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlVitalForce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVitalForce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditHasChilds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditItemaPerGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMajorVitalForce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReadingGauge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemItemsPerGroup)).BeginInit();
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
            toolTipTitleItem1.Text = "Save (Ctrl + S)";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "This button will save changes done on the current test.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.barButtonItemDone.SuperTip = superToolTip1;
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
            // layoutControlVitalForce
            // 
            this.layoutControlVitalForce.AllowCustomizationMenu = false;
            this.layoutControlVitalForce.Controls.Add(this.labelControlTip);
            this.layoutControlVitalForce.Controls.Add(this.gridControlVitalForce);
            this.layoutControlVitalForce.Controls.Add(this.spinEditItemaPerGroup);
            this.layoutControlVitalForce.Controls.Add(this.simpleButtonNextGroup);
            this.layoutControlVitalForce.Controls.Add(this.xtraUserControlReadingGaugeVitalForce);
            this.layoutControlVitalForce.Controls.Add(this.simpleButtonPreviousGroup);
            this.layoutControlVitalForce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlVitalForce.Location = new System.Drawing.Point(0, 39);
            this.layoutControlVitalForce.Name = "layoutControlVitalForce";
            this.layoutControlVitalForce.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(241, 162, 250, 350);
            this.layoutControlVitalForce.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControlVitalForce.Root = this.layoutControlGroupMajorVitalForce;
            this.layoutControlVitalForce.Size = new System.Drawing.Size(484, 439);
            this.layoutControlVitalForce.TabIndex = 0;
            this.layoutControlVitalForce.Text = "Vital Force";
            // 
            // labelControlTip
            // 
            this.labelControlTip.AllowHtmlString = true;
            this.labelControlTip.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelControlTip.Location = new System.Drawing.Point(12, 12);
            this.labelControlTip.Name = "labelControlTip";
            this.labelControlTip.Size = new System.Drawing.Size(460, 19);
            this.labelControlTip.StyleController = this.layoutControlVitalForce;
            this.labelControlTip.TabIndex = 6;
            this.labelControlTip.Text = "Press [Enter] continue after finding the vital force.";
            // 
            // gridControlVitalForce
            // 
            this.gridControlVitalForce.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.False;
            this.gridControlVitalForce.Location = new System.Drawing.Point(12, 83);
            this.gridControlVitalForce.MainView = this.gridViewVitalForce;
            this.gridControlVitalForce.Name = "gridControlVitalForce";
            this.gridControlVitalForce.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageEdit1,
            this.repositoryItemCheckEditHasChilds});
            this.gridControlVitalForce.Size = new System.Drawing.Size(314, 310);
            this.gridControlVitalForce.TabIndex = 1;
            this.gridControlVitalForce.TabStop = false;
            this.gridControlVitalForce.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewVitalForce});
            // 
            // gridViewVitalForce
            // 
            this.gridViewVitalForce.Appearance.FocusedRow.BackColor = System.Drawing.Color.Orange;
            this.gridViewVitalForce.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FloralWhite;
            this.gridViewVitalForce.Appearance.FocusedRow.BorderColor = System.Drawing.Color.Silver;
            this.gridViewVitalForce.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gridViewVitalForce.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewVitalForce.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gridViewVitalForce.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridViewVitalForce.Appearance.SelectedRow.BackColor = System.Drawing.Color.Orange;
            this.gridViewVitalForce.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FloralWhite;
            this.gridViewVitalForce.Appearance.SelectedRow.BorderColor = System.Drawing.Color.Silver;
            this.gridViewVitalForce.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gridViewVitalForce.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridViewVitalForce.Appearance.SelectedRow.Options.UseBorderColor = true;
            this.gridViewVitalForce.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridViewVitalForce.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnItem});
            this.gridViewVitalForce.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridViewVitalForce.GridControl = this.gridControlVitalForce;
            this.gridViewVitalForce.Name = "gridViewVitalForce";
            this.gridViewVitalForce.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridViewVitalForce.OptionsBehavior.Editable = false;
            this.gridViewVitalForce.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridViewVitalForce.OptionsBehavior.ReadOnly = true;
            this.gridViewVitalForce.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewVitalForce.OptionsCustomization.AllowColumnResizing = false;
            this.gridViewVitalForce.OptionsCustomization.AllowFilter = false;
            this.gridViewVitalForce.OptionsCustomization.AllowGroup = false;
            this.gridViewVitalForce.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewVitalForce.OptionsCustomization.AllowSort = false;
            this.gridViewVitalForce.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewVitalForce.OptionsDetail.SmartDetailHeight = true;
            this.gridViewVitalForce.OptionsMenu.EnableColumnMenu = false;
            this.gridViewVitalForce.OptionsNavigation.AutoMoveRowFocus = false;
            this.gridViewVitalForce.OptionsNavigation.UseTabKey = false;
            this.gridViewVitalForce.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewVitalForce.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewVitalForce.OptionsSelection.MultiSelect = true;
            this.gridViewVitalForce.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewVitalForce.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewVitalForce.OptionsView.ShowGroupPanel = false;
            this.gridViewVitalForce.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridViewVitalForce_CustomDrawCell);
            this.gridViewVitalForce.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridViewVitalForce_SelectionChanged);
            this.gridViewVitalForce.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewVitalForce_FocusedRowChanged);
            // 
            // gridColumnItem
            // 
            this.gridColumnItem.Caption = "Vital Force";
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
            // spinEditItemaPerGroup
            // 
            this.spinEditItemaPerGroup.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditItemaPerGroup.Location = new System.Drawing.Point(243, 59);
            this.spinEditItemaPerGroup.MenuManager = this.barManager;
            this.spinEditItemaPerGroup.Name = "spinEditItemaPerGroup";
            this.spinEditItemaPerGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditItemaPerGroup.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEditItemaPerGroup.Properties.IsFloatValue = false;
            this.spinEditItemaPerGroup.Properties.Mask.EditMask = "\\d+";
            this.spinEditItemaPerGroup.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.spinEditItemaPerGroup.Properties.Mask.ShowPlaceHolders = false;
            this.spinEditItemaPerGroup.Size = new System.Drawing.Size(83, 20);
            this.spinEditItemaPerGroup.StyleController = this.layoutControlVitalForce;
            this.spinEditItemaPerGroup.TabIndex = 4;
            this.spinEditItemaPerGroup.TabStop = false;
            this.spinEditItemaPerGroup.Spin += new DevExpress.XtraEditors.Controls.SpinEventHandler(this.spinEditItemaPerGroup_Spin);
            // 
            // simpleButtonNextGroup
            // 
            this.simpleButtonNextGroup.AllowFocus = false;
            this.simpleButtonNextGroup.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonNextGroup.Image")));
            this.simpleButtonNextGroup.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonNextGroup.Location = new System.Drawing.Point(12, 397);
            this.simpleButtonNextGroup.Name = "simpleButtonNextGroup";
            this.simpleButtonNextGroup.Size = new System.Drawing.Size(143, 30);
            this.simpleButtonNextGroup.StyleController = this.layoutControlVitalForce;
            this.simpleButtonNextGroup.TabIndex = 2;
            this.simpleButtonNextGroup.TabStop = false;
            this.simpleButtonNextGroup.Click += new System.EventHandler(this.simpleButtonNextGroup_Click);
            // 
            // xtraUserControlReadingGaugeVitalForce
            // 
            this.xtraUserControlReadingGaugeVitalForce.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraUserControlReadingGaugeVitalForce.Appearance.Options.UseBackColor = true;
            this.xtraUserControlReadingGaugeVitalForce.Location = new System.Drawing.Point(330, 59);
            this.xtraUserControlReadingGaugeVitalForce.Name = "xtraUserControlReadingGaugeVitalForce";
            this.xtraUserControlReadingGaugeVitalForce.ReadingValueTimeLine = 0F;
            this.xtraUserControlReadingGaugeVitalForce.ShowLocationGauge = false;
            this.xtraUserControlReadingGaugeVitalForce.ShowYesNoLabel = true;
            this.xtraUserControlReadingGaugeVitalForce.Size = new System.Drawing.Size(142, 368);
            this.xtraUserControlReadingGaugeVitalForce.TabIndex = 0;
            this.xtraUserControlReadingGaugeVitalForce.TabStop = false;
            // 
            // simpleButtonPreviousGroup
            // 
            this.simpleButtonPreviousGroup.AllowFocus = false;
            this.simpleButtonPreviousGroup.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonPreviousGroup.Image")));
            this.simpleButtonPreviousGroup.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonPreviousGroup.Location = new System.Drawing.Point(159, 397);
            this.simpleButtonPreviousGroup.Name = "simpleButtonPreviousGroup";
            this.simpleButtonPreviousGroup.Size = new System.Drawing.Size(167, 30);
            this.simpleButtonPreviousGroup.StyleController = this.layoutControlVitalForce;
            this.simpleButtonPreviousGroup.TabIndex = 3;
            this.simpleButtonPreviousGroup.TabStop = false;
            this.simpleButtonPreviousGroup.Click += new System.EventHandler(this.simpleButtonPreviousGroup_Click);
            // 
            // layoutControlGroupMajorVitalForce
            // 
            this.layoutControlGroupMajorVitalForce.CustomizationFormText = "Vital Force";
            this.layoutControlGroupMajorVitalForce.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMajorVitalForce.GroupBordersVisible = false;
            this.layoutControlGroupMajorVitalForce.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemReadingGauge,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItemItemsPerGroup,
            this.simpleLabelItemItemName,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.layoutControlGroupMajorVitalForce.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMajorVitalForce.Name = "Root";
            this.layoutControlGroupMajorVitalForce.Size = new System.Drawing.Size(484, 439);
            this.layoutControlGroupMajorVitalForce.Text = "Vital Force";
            this.layoutControlGroupMajorVitalForce.TextVisible = false;
            // 
            // layoutControlItemReadingGauge
            // 
            this.layoutControlItemReadingGauge.Control = this.xtraUserControlReadingGaugeVitalForce;
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
            // layoutControlItemItemsPerGroup
            // 
            this.layoutControlItemItemsPerGroup.Control = this.spinEditItemaPerGroup;
            this.layoutControlItemItemsPerGroup.CustomizationFormText = "Items per group";
            this.layoutControlItemItemsPerGroup.Location = new System.Drawing.Point(151, 47);
            this.layoutControlItemItemsPerGroup.MinSize = new System.Drawing.Size(135, 24);
            this.layoutControlItemItemsPerGroup.Name = "layoutControlItemItemsPerGroup";
            this.layoutControlItemItemsPerGroup.Size = new System.Drawing.Size(167, 24);
            this.layoutControlItemItemsPerGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemItemsPerGroup.Text = "Items per group";
            this.layoutControlItemItemsPerGroup.TextSize = new System.Drawing.Size(77, 13);
            // 
            // simpleLabelItemItemName
            // 
            this.simpleLabelItemItemName.AllowHotTrack = false;
            this.simpleLabelItemItemName.CustomizationFormText = "Item Name";
            this.simpleLabelItemItemName.Location = new System.Drawing.Point(0, 47);
            this.simpleLabelItemItemName.MinSize = new System.Drawing.Size(81, 17);
            this.simpleLabelItemItemName.Name = "simpleLabelItemItemName";
            this.simpleLabelItemItemName.Size = new System.Drawing.Size(151, 24);
            this.simpleLabelItemItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItemItemName.Text = "Item Name";
            this.simpleLabelItemItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.simpleLabelItemItemName.TextSize = new System.Drawing.Size(77, 13);
            this.simpleLabelItemItemName.TrimClientAreaToControl = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridControlVitalForce;
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
            // XtraFormVitalForce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 501);
            this.Controls.Add(this.layoutControlVitalForce);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(500, 540);
            this.MinimumSize = new System.Drawing.Size(500, 540);
            this.Name = "XtraFormVitalForce";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vital Force";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XtraFormVitalForce_FormClosing);
            this.Load += new System.EventHandler(this.XtraFormVitalForce_Load);
            this.Shown += new System.EventHandler(this.XtraFormVitalForce_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XtraFormVitalForce_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.XtraFormVitalForce_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlVitalForce)).EndInit();
            this.layoutControlVitalForce.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlVitalForce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVitalForce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditHasChilds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditItemaPerGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMajorVitalForce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReadingGauge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemItemsPerGroup)).EndInit();
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
        private DevExpress.XtraLayout.LayoutControl layoutControlVitalForce;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMajorVitalForce;        
        private User_Controls.Modules.XtraUserControlReadingGauge xtraUserControlReadingGaugeVitalForce;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemReadingGauge;
        private DevExpress.XtraEditors.SimpleButton simpleButtonNextGroup;
        private DevExpress.XtraEditors.SimpleButton simpleButtonPreviousGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemItemName;
        private DevExpress.XtraEditors.SpinEdit spinEditItemaPerGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemItemsPerGroup;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        public DevExpress.XtraGrid.GridControl gridControlVitalForce;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewVitalForce;
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