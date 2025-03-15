namespace Vital.UI.UI_Components.Forms.DataManagement
{
    partial class frmItemRelations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmItemRelations));
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlSelectedItems = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStripSelectedItems = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDeleteSelectedItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridViewSelectedItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnRelationType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControlItems = new DevExpress.XtraGrid.GridControl();
            this.gridViewItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumnItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookupEditTypes = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSelectedItems)).BeginInit();
            this.contextMenuStripSelectedItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSelectedItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupEditTypes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
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
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(759, 39);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 381);
            this.barDockControlBottom.Size = new System.Drawing.Size(759, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 39);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 342);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(759, 39);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 342);
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.btnSelect);
            this.layoutControl1.Controls.Add(this.gridControlSelectedItems);
            this.layoutControl1.Controls.Add(this.gridControlItems);
            this.layoutControl1.Controls.Add(this.lookupEditTypes);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(759, 342);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnSelect
            // 
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.Location = new System.Drawing.Point(294, 296);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(72, 22);
            this.btnSelect.StyleController = this.layoutControl1;
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Select";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // gridControlSelectedItems
            // 
            this.gridControlSelectedItems.ContextMenuStrip = this.contextMenuStripSelectedItems;
            this.gridControlSelectedItems.Location = new System.Drawing.Point(394, 43);
            this.gridControlSelectedItems.MainView = this.gridViewSelectedItems;
            this.gridControlSelectedItems.MenuManager = this.barManager;
            this.gridControlSelectedItems.Name = "gridControlSelectedItems";
            this.gridControlSelectedItems.Size = new System.Drawing.Size(341, 275);
            this.gridControlSelectedItems.TabIndex = 3;
            this.gridControlSelectedItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSelectedItems});
            // 
            // contextMenuStripSelectedItems
            // 
            this.contextMenuStripSelectedItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeleteSelectedItem});
            this.contextMenuStripSelectedItems.Name = "contextMenuStrip1";
            this.contextMenuStripSelectedItems.Size = new System.Drawing.Size(108, 26);
            this.contextMenuStripSelectedItems.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripOpening);
            this.contextMenuStripSelectedItems.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ContextMenuStripItemClicked);
            // 
            // toolStripMenuItemDeleteSelectedItem
            // 
            this.toolStripMenuItemDeleteSelectedItem.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemDeleteSelectedItem.Image")));
            this.toolStripMenuItemDeleteSelectedItem.Name = "toolStripMenuItemDeleteSelectedItem";
            this.toolStripMenuItemDeleteSelectedItem.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItemDeleteSelectedItem.Text = "Delete";
            // 
            // gridViewSelectedItems
            // 
            this.gridViewSelectedItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName,
            this.gridColumnType,
            this.gridColumnRelationType});
            this.gridViewSelectedItems.GridControl = this.gridControlSelectedItems;
            this.gridViewSelectedItems.Name = "gridViewSelectedItems";
            this.gridViewSelectedItems.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridViewSelectedItems.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridViewSelectedItems.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.gridViewSelectedItems.OptionsCustomization.AllowFilter = false;
            this.gridViewSelectedItems.OptionsCustomization.AllowGroup = false;
            this.gridViewSelectedItems.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewSelectedItems.OptionsMenu.EnableColumnMenu = false;
            this.gridViewSelectedItems.OptionsNavigation.UseTabKey = false;
            this.gridViewSelectedItems.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewSelectedItems.OptionsView.ShowAutoFilterRow = true;
            this.gridViewSelectedItems.OptionsView.ShowDetailButtons = false;
            this.gridViewSelectedItems.OptionsView.ShowGroupPanel = false;
            this.gridViewSelectedItems.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "Item";
            this.gridColumnName.FieldName = "Child.Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.OptionsColumn.AllowEdit = false;
            this.gridColumnName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnName.OptionsColumn.AllowMove = false;
            this.gridColumnName.OptionsColumn.AllowShowHide = false;
            this.gridColumnName.OptionsColumn.AllowSize = false;
            this.gridColumnName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 0;
            // 
            // gridColumnType
            // 
            this.gridColumnType.Caption = "Type";
            this.gridColumnType.FieldName = "Child.TypeLookup.Value";
            this.gridColumnType.Name = "gridColumnType";
            this.gridColumnType.OptionsColumn.AllowEdit = false;
            this.gridColumnType.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnType.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnType.OptionsColumn.AllowMove = false;
            this.gridColumnType.OptionsColumn.AllowShowHide = false;
            this.gridColumnType.OptionsColumn.AllowSize = false;
            this.gridColumnType.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumnType.OptionsColumn.ReadOnly = true;
            this.gridColumnType.Visible = true;
            this.gridColumnType.VisibleIndex = 1;
            // 
            // gridColumnRelationType
            // 
            this.gridColumnRelationType.Caption = "Relation Type";
            this.gridColumnRelationType.FieldName = "RelationType.Value";
            this.gridColumnRelationType.Name = "gridColumnRelationType";
            this.gridColumnRelationType.OptionsColumn.AllowEdit = false;
            this.gridColumnRelationType.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnRelationType.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnRelationType.OptionsColumn.AllowMove = false;
            this.gridColumnRelationType.OptionsColumn.AllowShowHide = false;
            this.gridColumnRelationType.OptionsColumn.AllowSize = false;
            this.gridColumnRelationType.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumnRelationType.OptionsColumn.ReadOnly = true;
            this.gridColumnRelationType.Visible = true;
            this.gridColumnRelationType.VisibleIndex = 2;
            // 
            // gridControlItems
            // 
            this.gridControlItems.Location = new System.Drawing.Point(24, 67);
            this.gridControlItems.MainView = this.gridViewItems;
            this.gridControlItems.MenuManager = this.barManager;
            this.gridControlItems.Name = "gridControlItems";
            this.gridControlItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControlItems.Size = new System.Drawing.Size(342, 225);
            this.gridControlItems.TabIndex = 1;
            this.gridControlItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewItems});
            // 
            // gridViewItems
            // 
            this.gridViewItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSelected,
            this.gridColumnItemName});
            this.gridViewItems.GridControl = this.gridControlItems;
            this.gridViewItems.Name = "gridViewItems";
            this.gridViewItems.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridViewItems.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridViewItems.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridViewItems.OptionsCustomization.AllowFilter = false;
            this.gridViewItems.OptionsCustomization.AllowGroup = false;
            this.gridViewItems.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewItems.OptionsMenu.EnableColumnMenu = false;
            this.gridViewItems.OptionsNavigation.UseTabKey = false;
            this.gridViewItems.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewItems.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridViewItems.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewItems.OptionsView.ShowAutoFilterRow = true;
            this.gridViewItems.OptionsView.ShowDetailButtons = false;
            this.gridViewItems.OptionsView.ShowGroupPanel = false;
            this.gridViewItems.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            this.gridViewItems.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.gridViewItems_MouseWheel);
            // 
            // gridColumnSelected
            // 
            this.gridColumnSelected.Caption = " ";
            this.gridColumnSelected.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumnSelected.FieldName = "IsChecked";
            this.gridColumnSelected.Name = "gridColumnSelected";
            this.gridColumnSelected.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnSelected.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumnSelected.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnSelected.OptionsColumn.AllowMove = false;
            this.gridColumnSelected.OptionsColumn.AllowShowHide = false;
            this.gridColumnSelected.OptionsColumn.AllowSize = false;
            this.gridColumnSelected.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnSelected.Visible = true;
            this.gridColumnSelected.VisibleIndex = 0;
            this.gridColumnSelected.Width = 55;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // gridColumnItemName
            // 
            this.gridColumnItemName.Caption = "Item Name";
            this.gridColumnItemName.FieldName = "Name";
            this.gridColumnItemName.Name = "gridColumnItemName";
            this.gridColumnItemName.OptionsColumn.AllowEdit = false;
            this.gridColumnItemName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnItemName.OptionsColumn.AllowMove = false;
            this.gridColumnItemName.OptionsColumn.AllowShowHide = false;
            this.gridColumnItemName.OptionsColumn.AllowSize = false;
            this.gridColumnItemName.Visible = true;
            this.gridColumnItemName.VisibleIndex = 1;
            this.gridColumnItemName.Width = 312;
            // 
            // lookupEditTypes
            // 
            this.lookupEditTypes.Location = new System.Drawing.Point(88, 43);
            this.lookupEditTypes.MenuManager = this.barManager;
            this.lookupEditTypes.Name = "lookupEditTypes";
            this.lookupEditTypes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookupEditTypes.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Value", "Items")});
            this.lookupEditTypes.Properties.DisplayMember = "Value";
            this.lookupEditTypes.Properties.NullText = "";
            this.lookupEditTypes.Properties.ValueMember = "Id";
            this.lookupEditTypes.Size = new System.Drawing.Size(278, 20);
            this.lookupEditTypes.StyleController = this.layoutControl1;
            this.lookupEditTypes.TabIndex = 0;
            this.lookupEditTypes.EditValueChanged += new System.EventHandler(this.lookupEditTypes_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(759, 342);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Items";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.emptySpaceItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(370, 322);
            this.layoutControlGroup2.Text = "Items";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControlItems;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(346, 229);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.lookupEditTypes;
            this.layoutControlItem1.CustomizationFormText = "Items Type  ";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(346, 24);
            this.layoutControlItem1.Text = "Items Type  ";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSelect;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(270, 253);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(76, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(76, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(76, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 253);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(270, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "Selected Items";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup3.Location = new System.Drawing.Point(370, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(369, 322);
            this.layoutControlGroup3.Text = "Selected Items";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridControlSelectedItems;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(345, 279);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmItemRelations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 404);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmItemRelations";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Item Relations";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmItemRelations_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSelectedItems)).EndInit();
            this.contextMenuStripSelectedItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSelectedItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupEditTypes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraBars.BarButtonItem barButtonItemDone;
        private DevExpress.XtraBars.Bar barTestStatus;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.Bar barCRUD;
        public DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCancel;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.LookUpEdit lookupEditTypes;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControlSelectedItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSelectedItems;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.GridControl gridControlItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItems;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItemName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSelectedItems;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteSelectedItem;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnRelationType;


    }
}
