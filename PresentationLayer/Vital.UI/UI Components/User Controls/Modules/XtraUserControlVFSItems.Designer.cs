namespace Vital.UI.UI_Components.User_Controls.Modules
{
    partial class XtraUserControlVFSItems
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.gridControlVFSItems = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStripVFSItems = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.gridViewVFSItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnSection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEditSection = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumnGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEditGroup = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumnValue1P = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnValue2P = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnValue1C = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnValue2C = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnIdealRange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnComments = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSearchLookUpEditItem = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnOnFlyItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnOnFlyDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBoxChangeType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageListChangeType = new System.Windows.Forms.ImageList(this.components);
            this.toolTipControllerVFSItems = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlVFSItems)).BeginInit();
            this.contextMenuStripVFSItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVFSItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxChangeType)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlVFSItems
            // 
            this.gridControlVFSItems.ContextMenuStrip = this.contextMenuStripVFSItems;
            this.gridControlVFSItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlVFSItems.Location = new System.Drawing.Point(0, 0);
            this.gridControlVFSItems.MainView = this.gridViewVFSItems;
            this.gridControlVFSItems.Name = "gridControlVFSItems";
            this.gridControlVFSItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSearchLookUpEditItem,
            this.repositoryItemLookUpEditSection,
            this.repositoryItemLookUpEditGroup,
            this.repositoryItemImageComboBoxChangeType});
            this.gridControlVFSItems.Size = new System.Drawing.Size(777, 514);
            this.gridControlVFSItems.TabIndex = 0;
            this.gridControlVFSItems.ToolTipController = this.toolTipControllerVFSItems;
            this.gridControlVFSItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewVFSItems});
            // 
            // contextMenuStripVFSItems
            // 
            this.contextMenuStripVFSItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAdd,
            this.toolStripMenuItemDelete});
            this.contextMenuStripVFSItems.Name = "contextMenuStripVFSItems";
            this.contextMenuStripVFSItems.Size = new System.Drawing.Size(200, 48);
            this.contextMenuStripVFSItems.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripVFSItems_Opening);
            this.contextMenuStripVFSItems.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStripVFSItems_ItemClicked);
            // 
            // toolStripMenuItemAdd
            // 
            this.toolStripMenuItemAdd.Image = global::Vital.UI.Properties.Resources.add16x16;
            this.toolStripMenuItemAdd.Name = "toolStripMenuItemAdd";
            this.toolStripMenuItemAdd.ShortcutKeyDisplayString = "Insert, ~ or Ctrl + I";
            this.toolStripMenuItemAdd.Size = new System.Drawing.Size(199, 22);
            this.toolStripMenuItemAdd.Text = "Add";
            this.toolStripMenuItemAdd.ToolTipText = "Insert, ~ or Ctrl + I";
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Image = global::Vital.UI.Properties.Resources.delete;
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.ShortcutKeyDisplayString = "Delete Key";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(199, 22);
            this.toolStripMenuItemDelete.Text = "Delete";
            this.toolStripMenuItemDelete.ToolTipText = "Delete Key";
            // 
            // gridViewVFSItems
            // 
            this.gridViewVFSItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSection,
            this.gridColumnGroup,
            this.gridColumnValue1P,
            this.gridColumnValue2P,
            this.gridColumnItem,
            this.gridColumnValue1C,
            this.gridColumnValue2C,
            this.gridColumnIdealRange,
            this.gridColumnComments});
            this.gridViewVFSItems.GridControl = this.gridControlVFSItems;
            this.gridViewVFSItems.GroupCount = 2;
            this.gridViewVFSItems.Name = "gridViewVFSItems";
            this.gridViewVFSItems.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewVFSItems.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridViewVFSItems.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewVFSItems.OptionsCustomization.AllowFilter = false;
            this.gridViewVFSItems.OptionsCustomization.AllowGroup = false;
            this.gridViewVFSItems.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewVFSItems.OptionsCustomization.AllowSort = false;
            this.gridViewVFSItems.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewVFSItems.OptionsFind.AllowFindPanel = false;
            this.gridViewVFSItems.OptionsMenu.EnableColumnMenu = false;
            this.gridViewVFSItems.OptionsMenu.EnableFooterMenu = false;
            this.gridViewVFSItems.OptionsNavigation.AutoFocusNewRow = true;
            this.gridViewVFSItems.OptionsNavigation.UseTabKey = false;
            this.gridViewVFSItems.OptionsView.ColumnAutoWidth = false;
            this.gridViewVFSItems.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewVFSItems.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewVFSItems.OptionsView.ShowGroupPanel = false;
            this.gridViewVFSItems.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnSection, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnGroup, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridViewVFSItems.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.gridViewVFSItems_CustomDrawGroupRow);
            this.gridViewVFSItems.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridViewVFSItems_CustomRowCellEdit);
            this.gridViewVFSItems.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridViewVFSItems_ShowingEditor);
            this.gridViewVFSItems.ShownEditor += new System.EventHandler(this.gridViewVFSItems_ShownEditor);
            this.gridViewVFSItems.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewVFSItems_FocusedRowChanged);
            this.gridViewVFSItems.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewVFSItems_CellValueChanging);
            this.gridViewVFSItems.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridViewVFSItems_CustomColumnDisplayText);
            this.gridViewVFSItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridViewVFSItems_KeyDown);
            // 
            // gridColumnSection
            // 
            this.gridColumnSection.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnSection.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnSection.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnSection.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnSection.Caption = "Section";
            this.gridColumnSection.ColumnEdit = this.repositoryItemLookUpEditSection;
            this.gridColumnSection.FieldName = "SectionLookup.Id";
            this.gridColumnSection.Name = "gridColumnSection";
            this.gridColumnSection.OptionsColumn.AllowEdit = false;
            this.gridColumnSection.OptionsColumn.AllowMove = false;
            this.gridColumnSection.OptionsColumn.AllowSize = false;
            this.gridColumnSection.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.gridColumnSection.Visible = true;
            this.gridColumnSection.VisibleIndex = 0;
            // 
            // repositoryItemLookUpEditSection
            // 
            this.repositoryItemLookUpEditSection.AutoHeight = false;
            this.repositoryItemLookUpEditSection.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEditSection.DisplayMember = "Value";
            this.repositoryItemLookUpEditSection.Name = "repositoryItemLookUpEditSection";
            this.repositoryItemLookUpEditSection.NullText = "";
            this.repositoryItemLookUpEditSection.ValueMember = "Id";
            // 
            // gridColumnGroup
            // 
            this.gridColumnGroup.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnGroup.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnGroup.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnGroup.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnGroup.Caption = "Group";
            this.gridColumnGroup.ColumnEdit = this.repositoryItemLookUpEditGroup;
            this.gridColumnGroup.FieldName = "GridGroupLookup.Id";
            this.gridColumnGroup.Name = "gridColumnGroup";
            this.gridColumnGroup.OptionsColumn.AllowEdit = false;
            this.gridColumnGroup.OptionsColumn.AllowMove = false;
            this.gridColumnGroup.OptionsColumn.AllowSize = false;
            this.gridColumnGroup.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.gridColumnGroup.Visible = true;
            this.gridColumnGroup.VisibleIndex = 0;
            // 
            // repositoryItemLookUpEditGroup
            // 
            this.repositoryItemLookUpEditGroup.AutoHeight = false;
            this.repositoryItemLookUpEditGroup.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEditGroup.DisplayMember = "Value";
            this.repositoryItemLookUpEditGroup.Name = "repositoryItemLookUpEditGroup";
            this.repositoryItemLookUpEditGroup.NullText = "";
            this.repositoryItemLookUpEditGroup.ValueMember = "Id";
            // 
            // gridColumnValue1P
            // 
            this.gridColumnValue1P.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnValue1P.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnValue1P.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnValue1P.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnValue1P.Caption = "Value 1 Previous";
            this.gridColumnValue1P.FieldName = "PreviousV1";
            this.gridColumnValue1P.Name = "gridColumnValue1P";
            this.gridColumnValue1P.OptionsColumn.AllowEdit = false;
            this.gridColumnValue1P.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnValue1P.OptionsColumn.AllowMove = false;
            this.gridColumnValue1P.Visible = true;
            this.gridColumnValue1P.VisibleIndex = 0;
            this.gridColumnValue1P.Width = 146;
            // 
            // gridColumnValue2P
            // 
            this.gridColumnValue2P.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnValue2P.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnValue2P.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnValue2P.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnValue2P.Caption = "Value 2 Previous";
            this.gridColumnValue2P.FieldName = "PreviousV2";
            this.gridColumnValue2P.Name = "gridColumnValue2P";
            this.gridColumnValue2P.OptionsColumn.AllowEdit = false;
            this.gridColumnValue2P.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnValue2P.OptionsColumn.AllowMove = false;
            this.gridColumnValue2P.Visible = true;
            this.gridColumnValue2P.VisibleIndex = 1;
            this.gridColumnValue2P.Width = 104;
            // 
            // gridColumnItem
            // 
            this.gridColumnItem.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnItem.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnItem.Caption = "Item";
            this.gridColumnItem.FieldName = "Item.Name";
            this.gridColumnItem.Name = "gridColumnItem";
            this.gridColumnItem.OptionsColumn.AllowEdit = false;
            this.gridColumnItem.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnItem.OptionsColumn.AllowMove = false;
            this.gridColumnItem.Visible = true;
            this.gridColumnItem.VisibleIndex = 2;
            this.gridColumnItem.Width = 84;
            // 
            // gridColumnValue1C
            // 
            this.gridColumnValue1C.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnValue1C.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnValue1C.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnValue1C.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnValue1C.Caption = "Value 1 Current";
            this.gridColumnValue1C.FieldName = "CurrentV1";
            this.gridColumnValue1C.Name = "gridColumnValue1C";
            this.gridColumnValue1C.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnValue1C.OptionsColumn.AllowMove = false;
            this.gridColumnValue1C.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridColumnValue1C.Visible = true;
            this.gridColumnValue1C.VisibleIndex = 3;
            this.gridColumnValue1C.Width = 100;
            // 
            // gridColumnValue2C
            // 
            this.gridColumnValue2C.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnValue2C.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnValue2C.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnValue2C.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnValue2C.Caption = "Value 2 Current";
            this.gridColumnValue2C.FieldName = "CurrentV2";
            this.gridColumnValue2C.Name = "gridColumnValue2C";
            this.gridColumnValue2C.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnValue2C.OptionsColumn.AllowMove = false;
            this.gridColumnValue2C.Visible = true;
            this.gridColumnValue2C.VisibleIndex = 4;
            this.gridColumnValue2C.Width = 100;
            // 
            // gridColumnIdealRange
            // 
            this.gridColumnIdealRange.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnIdealRange.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnIdealRange.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnIdealRange.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnIdealRange.Caption = "Ideal Range";
            this.gridColumnIdealRange.FieldName = "IdealRange1";
            this.gridColumnIdealRange.Name = "gridColumnIdealRange";
            this.gridColumnIdealRange.OptionsColumn.AllowEdit = false;
            this.gridColumnIdealRange.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnIdealRange.OptionsColumn.AllowMove = false;
            this.gridColumnIdealRange.Visible = true;
            this.gridColumnIdealRange.VisibleIndex = 5;
            this.gridColumnIdealRange.Width = 120;
            // 
            // gridColumnComments
            // 
            this.gridColumnComments.Caption = "Comments";
            this.gridColumnComments.FieldName = "Comments";
            this.gridColumnComments.Name = "gridColumnComments";
            this.gridColumnComments.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnComments.OptionsColumn.AllowMove = false;
            this.gridColumnComments.Visible = true;
            this.gridColumnComments.VisibleIndex = 6;
            this.gridColumnComments.Width = 125;
            // 
            // repositoryItemSearchLookUpEditItem
            // 
            this.repositoryItemSearchLookUpEditItem.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.False;
            this.repositoryItemSearchLookUpEditItem.AutoHeight = false;
            this.repositoryItemSearchLookUpEditItem.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, "", null, null, true)});
            this.repositoryItemSearchLookUpEditItem.DisplayMember = "Name";
            this.repositoryItemSearchLookUpEditItem.Name = "repositoryItemSearchLookUpEditItem";
            this.repositoryItemSearchLookUpEditItem.NullText = "Please Select an Item to Insert";
            this.repositoryItemSearchLookUpEditItem.NullValuePrompt = "Please Select an Item to Insert";
            this.repositoryItemSearchLookUpEditItem.NullValuePromptShowForEmptyValue = true;
            this.repositoryItemSearchLookUpEditItem.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.repositoryItemSearchLookUpEditItem.ShowFooter = false;
            this.repositoryItemSearchLookUpEditItem.ValueMember = "Id";
            this.repositoryItemSearchLookUpEditItem.View = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnOnFlyItem,
            this.gridColumnOnFlyDescription});
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnOnFlyItem
            // 
            this.gridColumnOnFlyItem.Caption = "Item";
            this.gridColumnOnFlyItem.FieldName = "Name";
            this.gridColumnOnFlyItem.Name = "gridColumnOnFlyItem";
            this.gridColumnOnFlyItem.Visible = true;
            this.gridColumnOnFlyItem.VisibleIndex = 0;
            // 
            // gridColumnOnFlyDescription
            // 
            this.gridColumnOnFlyDescription.Caption = "Description";
            this.gridColumnOnFlyDescription.FieldName = "Description";
            this.gridColumnOnFlyDescription.Name = "gridColumnOnFlyDescription";
            this.gridColumnOnFlyDescription.Visible = true;
            this.gridColumnOnFlyDescription.VisibleIndex = 1;
            // 
            // repositoryItemImageComboBoxChangeType
            // 
            this.repositoryItemImageComboBoxChangeType.AutoHeight = false;
            toolTipTitleItem2.Appearance.Image = global::Vital.UI.Properties.Resources.refresh;
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            toolTipTitleItem2.Image = global::Vital.UI.Properties.Resources.refresh;
            toolTipTitleItem2.Text = "Switch Change Type (Space)";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Click this button or hit space key to quickly cycle through the different change " +
    "types.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.repositoryItemImageComboBoxChangeType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "D", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::Vital.UI.Properties.Resources.refresh, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, "", "Switch", superToolTip2, true)});
            this.repositoryItemImageComboBoxChangeType.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemImageComboBoxChangeType.LargeImages = this.imageListChangeType;
            this.repositoryItemImageComboBoxChangeType.Name = "repositoryItemImageComboBoxChangeType";
            this.repositoryItemImageComboBoxChangeType.SmallImages = this.imageListChangeType;
            this.repositoryItemImageComboBoxChangeType.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemImageComboBoxChangeType_ButtonClick);
            this.repositoryItemImageComboBoxChangeType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.repositoryItemImageComboBoxChangeType_KeyDown);
            // 
            // imageListChangeType
            // 
            this.imageListChangeType.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListChangeType.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListChangeType.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolTipControllerVFSItems
            // 
            this.toolTipControllerVFSItems.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipControllerVFSItems_GetActiveObjectInfo);
            // 
            // XtraUserControlVFSItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlVFSItems);
            this.Name = "XtraUserControlVFSItems";
            this.Size = new System.Drawing.Size(777, 514);
            this.Load += new System.EventHandler(this.XtraUserControlVFSItems_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlVFSItems)).EndInit();
            this.contextMenuStripVFSItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVFSItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBoxChangeType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlVFSItems;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripVFSItems;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEditItem;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditSection;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditGroup;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnOnFlyItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnOnFlyDescription;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewVFSItems;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSection;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnGroup;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnValue1P;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnValue2P;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnValue1C;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnValue2C;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIdealRange;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnComments;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBoxChangeType;
        private System.Windows.Forms.ImageList imageListChangeType;
        private DevExpress.Utils.ToolTipController toolTipControllerVFSItems;
    }
}
