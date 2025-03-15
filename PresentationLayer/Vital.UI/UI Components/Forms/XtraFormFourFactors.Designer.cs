namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormFourFactors
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraFormFourFactors));
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
            this.pictureEditItemPicture = new DevExpress.XtraEditors.PictureEdit();
            this.labelControlTip = new DevExpress.XtraEditors.LabelControl();
            this.gridControlFourFactors = new DevExpress.XtraGrid.GridControl();
            this.gridViewFourFactors = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnIsBalanced = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBoxFourFactors = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageListFourFactors = new System.Windows.Forms.ImageList(this.components);
            this.gridColumnFactorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnReading = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageEditisbalanced = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.repositoryItemPictureEditIsBalcanced = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.xtraUserControlReadingGaugeVitalForce = new Vital.UI.UI_Components.User_Controls.Modules.XtraUserControlReadingGauge();
            this.layoutControlItemPotency = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupMajorVitalForce = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemReadingGauge = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemFourFactors = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItemItemName = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemImage = new DevExpress.XtraLayout.LayoutControlItem();
            this.timerAutoClose = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlVitalForce)).BeginInit();
            this.layoutControlVitalForce.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditItemPicture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFourFactors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFourFactors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxFourFactors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEditisbalanced)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEditIsBalcanced)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPotency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMajorVitalForce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReadingGauge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFourFactors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemImage)).BeginInit();
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemCancel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
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
            this.barButtonItemCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            this.barDockControlTop.Size = new System.Drawing.Size(501, 39);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 430);
            this.barDockControlBottom.Size = new System.Drawing.Size(501, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 39);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 391);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(501, 39);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 391);
            // 
            // layoutControlVitalForce
            // 
            this.layoutControlVitalForce.AllowCustomizationMenu = false;
            this.layoutControlVitalForce.Controls.Add(this.pictureEditItemPicture);
            this.layoutControlVitalForce.Controls.Add(this.labelControlTip);
            this.layoutControlVitalForce.Controls.Add(this.gridControlFourFactors);
            this.layoutControlVitalForce.Controls.Add(this.xtraUserControlReadingGaugeVitalForce);
            this.layoutControlVitalForce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlVitalForce.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemPotency,
            this.layoutControlItem2,
            this.layoutControlItem1});
            this.layoutControlVitalForce.Location = new System.Drawing.Point(0, 39);
            this.layoutControlVitalForce.Name = "layoutControlVitalForce";
            this.layoutControlVitalForce.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(669, 158, 250, 350);
            this.layoutControlVitalForce.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControlVitalForce.Root = this.layoutControlGroupMajorVitalForce;
            this.layoutControlVitalForce.Size = new System.Drawing.Size(501, 391);
            this.layoutControlVitalForce.TabIndex = 0;
            this.layoutControlVitalForce.Text = "Vital Force";
            // 
            // pictureEditItemPicture
            // 
            this.pictureEditItemPicture.EditValue = global::Vital.UI.Properties.Resources.Hand;
            this.pictureEditItemPicture.Location = new System.Drawing.Point(12, 157);
            this.pictureEditItemPicture.Name = "pictureEditItemPicture";
            this.pictureEditItemPicture.Properties.NullText = "No Image";
            this.pictureEditItemPicture.Properties.ReadOnly = true;
            this.pictureEditItemPicture.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEditItemPicture.Size = new System.Drawing.Size(319, 222);
            this.pictureEditItemPicture.StyleController = this.layoutControlVitalForce;
            this.pictureEditItemPicture.TabIndex = 8;
            // 
            // labelControlTip
            // 
            this.labelControlTip.AllowHtmlString = true;
            this.labelControlTip.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelControlTip.Location = new System.Drawing.Point(12, 12);
            this.labelControlTip.Name = "labelControlTip";
            this.labelControlTip.Size = new System.Drawing.Size(414, 19);
            this.labelControlTip.StyleController = this.layoutControlVitalForce;
            this.labelControlTip.TabIndex = 7;
            this.labelControlTip.Text = "Press [Enter] to continue after finding four factors.";
            // 
            // gridControlFourFactors
            // 
            this.gridControlFourFactors.Location = new System.Drawing.Point(12, 52);
            this.gridControlFourFactors.MainView = this.gridViewFourFactors;
            this.gridControlFourFactors.MenuManager = this.barManager;
            this.gridControlFourFactors.Name = "gridControlFourFactors";
            this.gridControlFourFactors.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageEditisbalanced,
            this.repositoryItemPictureEditIsBalcanced,
            this.repositoryItemImageComboBoxFourFactors});
            this.gridControlFourFactors.Size = new System.Drawing.Size(319, 101);
            this.gridControlFourFactors.TabIndex = 0;
            this.gridControlFourFactors.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFourFactors});
            // 
            // gridViewFourFactors
            // 
            this.gridViewFourFactors.Appearance.FocusedRow.BackColor = System.Drawing.Color.Orange;
            this.gridViewFourFactors.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FloralWhite;
            this.gridViewFourFactors.Appearance.FocusedRow.BorderColor = System.Drawing.Color.Silver;
            this.gridViewFourFactors.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gridViewFourFactors.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewFourFactors.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gridViewFourFactors.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridViewFourFactors.Appearance.SelectedRow.BackColor = System.Drawing.Color.Orange;
            this.gridViewFourFactors.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FloralWhite;
            this.gridViewFourFactors.Appearance.SelectedRow.BorderColor = System.Drawing.Color.Silver;
            this.gridViewFourFactors.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gridViewFourFactors.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridViewFourFactors.Appearance.SelectedRow.Options.UseBorderColor = true;
            this.gridViewFourFactors.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridViewFourFactors.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnIsBalanced,
            this.gridColumnFactorName,
            this.gridColumnReading});
            this.gridViewFourFactors.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridViewFourFactors.GridControl = this.gridControlFourFactors;
            this.gridViewFourFactors.Name = "gridViewFourFactors";
            this.gridViewFourFactors.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewFourFactors.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewFourFactors.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewFourFactors.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridViewFourFactors.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridViewFourFactors.OptionsBehavior.Editable = false;
            this.gridViewFourFactors.OptionsBehavior.ReadOnly = true;
            this.gridViewFourFactors.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewFourFactors.OptionsCustomization.AllowColumnResizing = false;
            this.gridViewFourFactors.OptionsCustomization.AllowFilter = false;
            this.gridViewFourFactors.OptionsCustomization.AllowGroup = false;
            this.gridViewFourFactors.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewFourFactors.OptionsCustomization.AllowSort = false;
            this.gridViewFourFactors.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewFourFactors.OptionsMenu.EnableColumnMenu = false;
            this.gridViewFourFactors.OptionsNavigation.UseTabKey = false;
            this.gridViewFourFactors.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewFourFactors.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewFourFactors.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewFourFactors.OptionsView.ShowDetailButtons = false;
            this.gridViewFourFactors.OptionsView.ShowGroupPanel = false;
            this.gridViewFourFactors.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewFourFactors_RowClick);
            this.gridViewFourFactors.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewFourFactors_FocusedRowChanged);
            // 
            // gridColumnIsBalanced
            // 
            this.gridColumnIsBalanced.ColumnEdit = this.repositoryItemImageComboBoxFourFactors;
            this.gridColumnIsBalanced.FieldName = "BalancingState";
            this.gridColumnIsBalanced.MaxWidth = 20;
            this.gridColumnIsBalanced.Name = "gridColumnIsBalanced";
            this.gridColumnIsBalanced.OptionsColumn.AllowEdit = false;
            this.gridColumnIsBalanced.OptionsColumn.AllowFocus = false;
            this.gridColumnIsBalanced.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnIsBalanced.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumnIsBalanced.OptionsColumn.AllowMove = false;
            this.gridColumnIsBalanced.OptionsColumn.AllowShowHide = false;
            this.gridColumnIsBalanced.OptionsColumn.AllowSize = false;
            this.gridColumnIsBalanced.OptionsColumn.FixedWidth = true;
            this.gridColumnIsBalanced.OptionsColumn.ReadOnly = true;
            this.gridColumnIsBalanced.OptionsColumn.ShowCaption = false;
            this.gridColumnIsBalanced.OptionsColumn.ShowInCustomizationForm = false;
            this.gridColumnIsBalanced.OptionsColumn.ShowInExpressionEditor = false;
            this.gridColumnIsBalanced.Visible = true;
            this.gridColumnIsBalanced.VisibleIndex = 0;
            this.gridColumnIsBalanced.Width = 18;
            // 
            // repositoryItemImageComboBoxFourFactors
            // 
            this.repositoryItemImageComboBoxFourFactors.AutoHeight = false;
            this.repositoryItemImageComboBoxFourFactors.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBoxFourFactors.LargeImages = this.imageListFourFactors;
            this.repositoryItemImageComboBoxFourFactors.Name = "repositoryItemImageComboBoxFourFactors";
            this.repositoryItemImageComboBoxFourFactors.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            this.repositoryItemImageComboBoxFourFactors.SmallImages = this.imageListFourFactors;
            // 
            // imageListFourFactors
            // 
            this.imageListFourFactors.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListFourFactors.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListFourFactors.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // gridColumnFactorName
            // 
            this.gridColumnFactorName.Caption = "Factor Point";
            this.gridColumnFactorName.FieldName = "Factor.Name";
            this.gridColumnFactorName.Name = "gridColumnFactorName";
            this.gridColumnFactorName.Visible = true;
            this.gridColumnFactorName.VisibleIndex = 1;
            this.gridColumnFactorName.Width = 136;
            // 
            // gridColumnReading
            // 
            this.gridColumnReading.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnReading.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumnReading.Caption = "Reading";
            this.gridColumnReading.FieldName = "Reading";
            this.gridColumnReading.Name = "gridColumnReading";
            this.gridColumnReading.OptionsColumn.AllowFocus = false;
            this.gridColumnReading.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumnReading.Visible = true;
            this.gridColumnReading.VisibleIndex = 2;
            this.gridColumnReading.Width = 136;
            // 
            // repositoryItemImageEditisbalanced
            // 
            this.repositoryItemImageEditisbalanced.AutoHeight = false;
            this.repositoryItemImageEditisbalanced.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageEditisbalanced.Name = "repositoryItemImageEditisbalanced";
            this.repositoryItemImageEditisbalanced.ReadOnly = true;
            this.repositoryItemImageEditisbalanced.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            // 
            // repositoryItemPictureEditIsBalcanced
            // 
            this.repositoryItemPictureEditIsBalcanced.Name = "repositoryItemPictureEditIsBalcanced";
            // 
            // xtraUserControlReadingGaugeVitalForce
            // 
            this.xtraUserControlReadingGaugeVitalForce.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraUserControlReadingGaugeVitalForce.Appearance.Options.UseBackColor = true;
            this.xtraUserControlReadingGaugeVitalForce.Location = new System.Drawing.Point(335, 35);
            this.xtraUserControlReadingGaugeVitalForce.Name = "xtraUserControlReadingGaugeVitalForce";
            this.xtraUserControlReadingGaugeVitalForce.ReadingValueTimeLine = 0F;
            this.xtraUserControlReadingGaugeVitalForce.ShowLocationGauge = false;
            this.xtraUserControlReadingGaugeVitalForce.ShowYesNoLabel = true;
            this.xtraUserControlReadingGaugeVitalForce.Size = new System.Drawing.Size(154, 344);
            this.xtraUserControlReadingGaugeVitalForce.TabIndex = 1;
            this.xtraUserControlReadingGaugeVitalForce.TabStop = false;
            // 
            // layoutControlItemPotency
            // 
            this.layoutControlItemPotency.CustomizationFormText = "Potency";
            this.layoutControlItemPotency.Location = new System.Drawing.Point(457, 0);
            this.layoutControlItemPotency.Name = "layoutControlItemPotency";
            this.layoutControlItemPotency.Size = new System.Drawing.Size(169, 359);
            this.layoutControlItemPotency.Text = "Potency";
            this.layoutControlItemPotency.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemPotency.TextToControlDistance = 0;
            this.layoutControlItemPotency.TextVisible = false;
            this.layoutControlItemPotency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(457, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(84, 34);
            this.layoutControlItem2.Text = "Down";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            this.layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(457, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(169, 419);
            this.layoutControlItem1.Text = "Up";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            this.layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlGroupMajorVitalForce
            // 
            this.layoutControlGroupMajorVitalForce.CustomizationFormText = "Vital Force";
            this.layoutControlGroupMajorVitalForce.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMajorVitalForce.GroupBordersVisible = false;
            this.layoutControlGroupMajorVitalForce.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemReadingGauge,
            this.layoutControlItemFourFactors,
            this.simpleLabelItemItemName,
            this.layoutControlItem3,
            this.layoutControlItemImage});
            this.layoutControlGroupMajorVitalForce.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMajorVitalForce.Name = "Root";
            this.layoutControlGroupMajorVitalForce.Size = new System.Drawing.Size(501, 391);
            this.layoutControlGroupMajorVitalForce.Text = "Vital Force";
            this.layoutControlGroupMajorVitalForce.TextVisible = false;
            // 
            // layoutControlItemReadingGauge
            // 
            this.layoutControlItemReadingGauge.Control = this.xtraUserControlReadingGaugeVitalForce;
            this.layoutControlItemReadingGauge.CustomizationFormText = "Reading Gauge";
            this.layoutControlItemReadingGauge.Location = new System.Drawing.Point(323, 23);
            this.layoutControlItemReadingGauge.Name = "layoutControlItemReadingGauge";
            this.layoutControlItemReadingGauge.Size = new System.Drawing.Size(158, 348);
            this.layoutControlItemReadingGauge.Text = "Reading Gauge";
            this.layoutControlItemReadingGauge.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemReadingGauge.TextToControlDistance = 0;
            this.layoutControlItemReadingGauge.TextVisible = false;
            // 
            // layoutControlItemFourFactors
            // 
            this.layoutControlItemFourFactors.Control = this.gridControlFourFactors;
            this.layoutControlItemFourFactors.CustomizationFormText = "Four Factors";
            this.layoutControlItemFourFactors.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItemFourFactors.Name = "layoutControlItemFourFactors";
            this.layoutControlItemFourFactors.Size = new System.Drawing.Size(323, 105);
            this.layoutControlItemFourFactors.Text = "Four Factors";
            this.layoutControlItemFourFactors.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemFourFactors.TextToControlDistance = 0;
            this.layoutControlItemFourFactors.TextVisible = false;
            // 
            // simpleLabelItemItemName
            // 
            this.simpleLabelItemItemName.AllowHotTrack = false;
            this.simpleLabelItemItemName.CustomizationFormText = "Item Name";
            this.simpleLabelItemItemName.Location = new System.Drawing.Point(0, 23);
            this.simpleLabelItemItemName.Name = "simpleLabelItemItemName";
            this.simpleLabelItemItemName.Size = new System.Drawing.Size(323, 17);
            this.simpleLabelItemItemName.Text = "Item Name";
            this.simpleLabelItemItemName.TextSize = new System.Drawing.Size(52, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.labelControlTip;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(481, 23);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItemImage
            // 
            this.layoutControlItemImage.Control = this.pictureEditItemPicture;
            this.layoutControlItemImage.CustomizationFormText = "Image";
            this.layoutControlItemImage.Location = new System.Drawing.Point(0, 145);
            this.layoutControlItemImage.Name = "layoutControlItemImage";
            this.layoutControlItemImage.Size = new System.Drawing.Size(323, 226);
            this.layoutControlItemImage.Text = "Image";
            this.layoutControlItemImage.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemImage.TextToControlDistance = 0;
            this.layoutControlItemImage.TextVisible = false;
            // 
            // timerAutoClose
            // 
            this.timerAutoClose.Tick += new System.EventHandler(this.timerAutoClose_Tick);
            // 
            // XtraFormFourFactors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 453);
            this.Controls.Add(this.layoutControlVitalForce);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(517, 492);
            this.MinimumSize = new System.Drawing.Size(517, 487);
            this.Name = "XtraFormFourFactors";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Four Factors";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XtraFormFourFactors_FormClosing);
            this.Load += new System.EventHandler(this.XtraFormFourFactors_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XtraFormFourFactors_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlVitalForce)).EndInit();
            this.layoutControlVitalForce.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditItemPicture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFourFactors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFourFactors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxFourFactors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEditisbalanced)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEditIsBalcanced)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPotency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMajorVitalForce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReadingGauge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFourFactors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemImage)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gridControlFourFactors;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFourFactors;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemFourFactors;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFactorName;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemItemName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPotency;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnReading;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIsBalanced;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit repositoryItemImageEditisbalanced;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEditIsBalcanced;
        private DevExpress.XtraEditors.LabelControl labelControlTip;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.PictureEdit pictureEditItemPicture;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemImage;
        private System.Windows.Forms.Timer timerAutoClose;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBoxFourFactors;
        private System.Windows.Forms.ImageList imageListFourFactors;
    }
}