namespace Vital.UI.UI_Components.User_Controls.Modules
{
    partial class XtraUserControlSpotCheckResults
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraUserControlSpotCheckResults));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonDelete = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonClearSelection = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOpen = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlResults = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStripSpotCheckResult = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItemOpenSpotCheckResult = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDeleteSpotCheckResult = new System.Windows.Forms.ToolStripMenuItem();
            this.gridViewResults = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnIsStarred = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEditIsStarred = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumnIvType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnMgPerMl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDosageRange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNoOfBags = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEditNoOfBags = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumnNoPerWeek = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEditNoPerWeek = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumnDosages = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEditDosages = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumnNotes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlResults)).BeginInit();
            this.contextMenuStripSpotCheckResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditIsStarred)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNoOfBags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNoPerWeek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditDosages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.simpleButtonDelete);
            this.layoutControl1.Controls.Add(this.simpleButtonClearSelection);
            this.layoutControl1.Controls.Add(this.simpleButtonOpen);
            this.layoutControl1.Controls.Add(this.gridControlResults);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(484, 401);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // simpleButtonDelete
            // 
            this.simpleButtonDelete.Image = global::Vital.UI.Properties.Resources.delete;
            this.simpleButtonDelete.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonDelete.Location = new System.Drawing.Point(456, 54);
            this.simpleButtonDelete.Name = "simpleButtonDelete";
            this.simpleButtonDelete.Size = new System.Drawing.Size(26, 22);
            this.simpleButtonDelete.StyleController = this.layoutControl1;
            toolTipTitleItem1.Text = "Delete (Delete)";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Delete Selected Results";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.simpleButtonDelete.SuperTip = superToolTip1;
            this.simpleButtonDelete.TabIndex = 23;
            this.simpleButtonDelete.Click += new System.EventHandler(this.simpleButtonDelete_Click);
            // 
            // simpleButtonClearSelection
            // 
            this.simpleButtonClearSelection.Image = global::Vital.UI.Properties.Resources.eraser;
            this.simpleButtonClearSelection.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonClearSelection.Location = new System.Drawing.Point(456, 28);
            this.simpleButtonClearSelection.Name = "simpleButtonClearSelection";
            this.simpleButtonClearSelection.Size = new System.Drawing.Size(26, 22);
            this.simpleButtonClearSelection.StyleController = this.layoutControl1;
            toolTipTitleItem2.Text = "Clear The User Selection. (F8)";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Clear selected results.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.simpleButtonClearSelection.SuperTip = superToolTip2;
            this.simpleButtonClearSelection.TabIndex = 21;
            this.simpleButtonClearSelection.Click += new System.EventHandler(this.simpleButtonClearSelection_Click);
            // 
            // simpleButtonOpen
            // 
            this.simpleButtonOpen.Image = global::Vital.UI.Properties.Resources.Patient_Open;
            this.simpleButtonOpen.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonOpen.Location = new System.Drawing.Point(456, 2);
            this.simpleButtonOpen.Name = "simpleButtonOpen";
            this.simpleButtonOpen.Size = new System.Drawing.Size(26, 22);
            this.simpleButtonOpen.StyleController = this.layoutControl1;
            toolTipTitleItem3.Text = "Open  (Enter)";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Open selected result.";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.simpleButtonOpen.SuperTip = superToolTip3;
            this.simpleButtonOpen.TabIndex = 22;
            this.simpleButtonOpen.Click += new System.EventHandler(this.simpleButtonOpen_Click);
            // 
            // gridControlResults
            // 
            this.gridControlResults.ContextMenuStrip = this.contextMenuStripSpotCheckResult;
            this.gridControlResults.Location = new System.Drawing.Point(2, 2);
            this.gridControlResults.MainView = this.gridViewResults;
            this.gridControlResults.Name = "gridControlResults";
            this.gridControlResults.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEditNoOfBags,
            this.repositoryItemSpinEditNoPerWeek,
            this.repositoryItemSpinEditDosages,
            this.repositoryItemCheckEditIsStarred});
            this.gridControlResults.Size = new System.Drawing.Size(450, 397);
            this.gridControlResults.TabIndex = 9;
            this.gridControlResults.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewResults});
            this.gridControlResults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridControlResults_KeyDown);
            // 
            // contextMenuStripSpotCheckResult
            // 
            this.contextMenuStripSpotCheckResult.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItemOpenSpotCheckResult,
            this.toolStripMenuItemDeleteSpotCheckResult});
            this.contextMenuStripSpotCheckResult.Name = "contextMenuStrip1";
            this.contextMenuStripSpotCheckResult.Size = new System.Drawing.Size(108, 48);
            this.contextMenuStripSpotCheckResult.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripSpotCheckResult_Opening);
            this.contextMenuStripSpotCheckResult.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.simpleButtonGenerateSpotCheckResult__ItemClicked);
            // 
            // openToolStripMenuItemOpenSpotCheckResult
            // 
            this.openToolStripMenuItemOpenSpotCheckResult.Image = global::Vital.UI.Properties.Resources.Patient_Open;
            this.openToolStripMenuItemOpenSpotCheckResult.Name = "openToolStripMenuItemOpenSpotCheckResult";
            this.openToolStripMenuItemOpenSpotCheckResult.Size = new System.Drawing.Size(107, 22);
            this.openToolStripMenuItemOpenSpotCheckResult.Text = "Open";
            // 
            // toolStripMenuItemDeleteSpotCheckResult
            // 
            this.toolStripMenuItemDeleteSpotCheckResult.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemDeleteSpotCheckResult.Image")));
            this.toolStripMenuItemDeleteSpotCheckResult.Name = "toolStripMenuItemDeleteSpotCheckResult";
            this.toolStripMenuItemDeleteSpotCheckResult.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItemDeleteSpotCheckResult.Text = "Delete";
            // 
            // gridViewResults
            // 
            this.gridViewResults.Appearance.FocusedRow.BackColor = System.Drawing.Color.Orange;
            this.gridViewResults.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FloralWhite;
            this.gridViewResults.Appearance.FocusedRow.BorderColor = System.Drawing.Color.Silver;
            this.gridViewResults.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gridViewResults.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewResults.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gridViewResults.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridViewResults.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Orange;
            this.gridViewResults.Appearance.HideSelectionRow.BackColor2 = System.Drawing.Color.FloralWhite;
            this.gridViewResults.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.Silver;
            this.gridViewResults.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridViewResults.Appearance.HideSelectionRow.Options.UseBorderColor = true;
            this.gridViewResults.Appearance.SelectedRow.BackColor = System.Drawing.Color.Orange;
            this.gridViewResults.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FloralWhite;
            this.gridViewResults.Appearance.SelectedRow.BorderColor = System.Drawing.Color.Silver;
            this.gridViewResults.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gridViewResults.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridViewResults.Appearance.SelectedRow.Options.UseBorderColor = true;
            this.gridViewResults.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridViewResults.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnIsStarred,
            this.gridColumnIvType,
            this.gridColumnMgPerMl,
            this.gridColumnDosageRange,
            this.gridColumnNoOfBags,
            this.gridColumnNoPerWeek,
            this.gridColumnDosages,
            this.gridColumnNotes});
            this.gridViewResults.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridViewResults.GridControl = this.gridControlResults;
            this.gridViewResults.Name = "gridViewResults";
            this.gridViewResults.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewResults.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewResults.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridViewResults.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridViewResults.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridViewResults.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewResults.OptionsCustomization.AllowGroup = false;
            this.gridViewResults.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewResults.OptionsCustomization.AllowSort = false;
            this.gridViewResults.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewResults.OptionsFind.AllowFindPanel = false;
            this.gridViewResults.OptionsMenu.EnableColumnMenu = false;
            this.gridViewResults.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridViewResults.OptionsNavigation.UseTabKey = false;
            this.gridViewResults.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewResults.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewResults.OptionsSelection.MultiSelect = true;
            this.gridViewResults.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewResults.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewResults.OptionsView.ShowDetailButtons = false;
            this.gridViewResults.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewResults.OptionsView.ShowGroupPanel = false;
            this.gridViewResults.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            this.gridViewResults.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridViewResults_SelectionChanged);
            this.gridViewResults.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewResults_CellValueChanging);
            // 
            // gridColumnIsStarred
            // 
            this.gridColumnIsStarred.Caption = " ";
            this.gridColumnIsStarred.ColumnEdit = this.repositoryItemCheckEditIsStarred;
            this.gridColumnIsStarred.FieldName = "Item.IsStarred";
            this.gridColumnIsStarred.MaxWidth = 40;
            this.gridColumnIsStarred.MinWidth = 40;
            this.gridColumnIsStarred.Name = "gridColumnIsStarred";
            this.gridColumnIsStarred.OptionsColumn.AllowEdit = false;
            this.gridColumnIsStarred.OptionsColumn.AllowFocus = false;
            this.gridColumnIsStarred.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumnIsStarred.OptionsColumn.ReadOnly = true;
            this.gridColumnIsStarred.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnIsStarred.OptionsFilter.AllowFilter = false;
            this.gridColumnIsStarred.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.gridColumnIsStarred.Visible = true;
            this.gridColumnIsStarred.VisibleIndex = 0;
            this.gridColumnIsStarred.Width = 40;
            // 
            // repositoryItemCheckEditIsStarred
            // 
            this.repositoryItemCheckEditIsStarred.AutoHeight = false;
            this.repositoryItemCheckEditIsStarred.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.repositoryItemCheckEditIsStarred.Name = "repositoryItemCheckEditIsStarred";
            this.repositoryItemCheckEditIsStarred.PictureChecked = global::Vital.UI.Properties.Resources.Star;
            // 
            // gridColumnIvType
            // 
            this.gridColumnIvType.Caption = "IV Type";
            this.gridColumnIvType.FieldName = "Item.Name";
            this.gridColumnIvType.Name = "gridColumnIvType";
            this.gridColumnIvType.OptionsColumn.AllowEdit = false;
            this.gridColumnIvType.OptionsColumn.AllowFocus = false;
            this.gridColumnIvType.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnIvType.OptionsFilter.AllowFilter = false;
            this.gridColumnIvType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumnIvType.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.gridColumnIvType.Visible = true;
            this.gridColumnIvType.VisibleIndex = 1;
            this.gridColumnIvType.Width = 93;
            // 
            // gridColumnMgPerMl
            // 
            this.gridColumnMgPerMl.Caption = "mg/mL";
            this.gridColumnMgPerMl.FieldName = "Item.MineralMGML";
            this.gridColumnMgPerMl.Name = "gridColumnMgPerMl";
            this.gridColumnMgPerMl.OptionsColumn.AllowEdit = false;
            this.gridColumnMgPerMl.OptionsColumn.AllowFocus = false;
            this.gridColumnMgPerMl.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnMgPerMl.OptionsFilter.AllowFilter = false;
            this.gridColumnMgPerMl.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.gridColumnMgPerMl.Visible = true;
            this.gridColumnMgPerMl.VisibleIndex = 2;
            // 
            // gridColumnDosageRange
            // 
            this.gridColumnDosageRange.Caption = "Dosage Range";
            this.gridColumnDosageRange.FieldName = "Item.MineralDosageRange";
            this.gridColumnDosageRange.Name = "gridColumnDosageRange";
            this.gridColumnDosageRange.OptionsColumn.AllowEdit = false;
            this.gridColumnDosageRange.OptionsColumn.AllowFocus = false;
            this.gridColumnDosageRange.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnDosageRange.OptionsFilter.AllowFilter = false;
            this.gridColumnDosageRange.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.gridColumnDosageRange.Visible = true;
            this.gridColumnDosageRange.VisibleIndex = 3;
            // 
            // gridColumnNoOfBags
            // 
            this.gridColumnNoOfBags.Caption = "# Of Bags";
            this.gridColumnNoOfBags.ColumnEdit = this.repositoryItemSpinEditNoOfBags;
            this.gridColumnNoOfBags.FieldName = "NumberOfBags";
            this.gridColumnNoOfBags.Name = "gridColumnNoOfBags";
            this.gridColumnNoOfBags.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumnNoOfBags.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNoOfBags.OptionsFilter.AllowFilter = false;
            this.gridColumnNoOfBags.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.gridColumnNoOfBags.Visible = true;
            this.gridColumnNoOfBags.VisibleIndex = 4;
            this.gridColumnNoOfBags.Width = 77;
            // 
            // repositoryItemSpinEditNoOfBags
            // 
            this.repositoryItemSpinEditNoOfBags.AutoHeight = false;
            this.repositoryItemSpinEditNoOfBags.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditNoOfBags.IsFloatValue = false;
            this.repositoryItemSpinEditNoOfBags.Mask.EditMask = "N00";
            this.repositoryItemSpinEditNoOfBags.MaxValue = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.repositoryItemSpinEditNoOfBags.Name = "repositoryItemSpinEditNoOfBags";
            // 
            // gridColumnNoPerWeek
            // 
            this.gridColumnNoPerWeek.Caption = "# Per Week";
            this.gridColumnNoPerWeek.ColumnEdit = this.repositoryItemSpinEditNoPerWeek;
            this.gridColumnNoPerWeek.FieldName = "NumberOfWeeks";
            this.gridColumnNoPerWeek.Name = "gridColumnNoPerWeek";
            this.gridColumnNoPerWeek.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumnNoPerWeek.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNoPerWeek.OptionsFilter.AllowFilter = false;
            this.gridColumnNoPerWeek.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumnNoPerWeek.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.gridColumnNoPerWeek.Visible = true;
            this.gridColumnNoPerWeek.VisibleIndex = 5;
            this.gridColumnNoPerWeek.Width = 71;
            // 
            // repositoryItemSpinEditNoPerWeek
            // 
            this.repositoryItemSpinEditNoPerWeek.AutoHeight = false;
            this.repositoryItemSpinEditNoPerWeek.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditNoPerWeek.IsFloatValue = false;
            this.repositoryItemSpinEditNoPerWeek.Mask.EditMask = "N00";
            this.repositoryItemSpinEditNoPerWeek.MaxValue = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.repositoryItemSpinEditNoPerWeek.Name = "repositoryItemSpinEditNoPerWeek";
            // 
            // gridColumnDosages
            // 
            this.gridColumnDosages.Caption = "Dosages (CC)";
            this.gridColumnDosages.ColumnEdit = this.repositoryItemSpinEditDosages;
            this.gridColumnDosages.FieldName = "Dosage";
            this.gridColumnDosages.Name = "gridColumnDosages";
            this.gridColumnDosages.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumnDosages.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnDosages.OptionsFilter.AllowFilter = false;
            this.gridColumnDosages.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.gridColumnDosages.Visible = true;
            this.gridColumnDosages.VisibleIndex = 6;
            this.gridColumnDosages.Width = 81;
            // 
            // repositoryItemSpinEditDosages
            // 
            this.repositoryItemSpinEditDosages.AutoHeight = false;
            this.repositoryItemSpinEditDosages.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditDosages.IsFloatValue = false;
            this.repositoryItemSpinEditDosages.Mask.EditMask = "N00";
            this.repositoryItemSpinEditDosages.MaxValue = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.repositoryItemSpinEditDosages.Name = "repositoryItemSpinEditDosages";
            // 
            // gridColumnNotes
            // 
            this.gridColumnNotes.Caption = "Notes";
            this.gridColumnNotes.FieldName = "Notes";
            this.gridColumnNotes.Name = "gridColumnNotes";
            this.gridColumnNotes.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumnNotes.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNotes.OptionsFilter.AllowFilter = false;
            this.gridColumnNotes.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.gridColumnNotes.Visible = true;
            this.gridColumnNotes.VisibleIndex = 7;
            this.gridColumnNotes.Width = 290;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(484, 401);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlResults;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(454, 401);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButtonDelete;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(454, 52);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(30, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(454, 78);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(30, 0);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(30, 10);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(30, 323);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonClearSelection;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(454, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(30, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonOpen;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(454, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(30, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // XtraUserControlSpotCheckResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraUserControlSpotCheckResults";
            this.Size = new System.Drawing.Size(484, 401);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlResults)).EndInit();
            this.contextMenuStripSpotCheckResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditIsStarred)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNoOfBags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNoPerWeek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditDosages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControlResults;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewResults;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIsStarred;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditIsStarred;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIvType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNoOfBags;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditNoOfBags;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNoPerWeek;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditNoPerWeek;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDosages;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditDosages;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNotes;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDelete;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClearSelection;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOpen;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnMgPerMl;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDosageRange;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSpotCheckResult;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItemOpenSpotCheckResult;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteSpotCheckResult;
    }
}
