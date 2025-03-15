namespace Vital.UI.UI_Components.User_Controls.Modules
{
    partial class XtraUserControlFrequencyTestResults
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
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlResults = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStripFrequencyTestResult = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDeleteFrequencyTestResult = new System.Windows.Forms.ToolStripMenuItem();
            this.gridViewResults = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnResult = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTimesPerWeek = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEditNoPerWeek = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumnNumberOfWeeks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNotes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButtonOpen = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleButtonDelete = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlResults)).BeginInit();
            this.contextMenuStripFrequencyTestResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNoPerWeek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.simpleButtonDelete);
            this.layoutControl1.Controls.Add(this.simpleButtonOpen);
            this.layoutControl1.Controls.Add(this.gridControlResults);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(530, 173, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(484, 401);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControlResults
            // 
            this.gridControlResults.ContextMenuStrip = this.contextMenuStripFrequencyTestResult;
            this.gridControlResults.Location = new System.Drawing.Point(2, 2);
            this.gridControlResults.MainView = this.gridViewResults;
            this.gridControlResults.Name = "gridControlResults";
            this.gridControlResults.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEditNoPerWeek});
            this.gridControlResults.Size = new System.Drawing.Size(450, 397);
            this.gridControlResults.TabIndex = 9;
            this.gridControlResults.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewResults});
            this.gridControlResults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridControlResults_KeyDown);
            // 
            // contextMenuStripFrequencyTestResult
            // 
            this.contextMenuStripFrequencyTestResult.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeleteFrequencyTestResult});
            this.contextMenuStripFrequencyTestResult.Name = "contextMenuStrip1";
            this.contextMenuStripFrequencyTestResult.Size = new System.Drawing.Size(108, 26);
            this.contextMenuStripFrequencyTestResult.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripFrequencyTestResult_Opening);
            this.contextMenuStripFrequencyTestResult.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.simpleButtonGenerateFrequencyTestResult__ItemClicked);
            // 
            // toolStripMenuItemDeleteFrequencyTestResult
            // 
            this.toolStripMenuItemDeleteFrequencyTestResult.Image = global::Vital.UI.Properties.Resources.delete;
            this.toolStripMenuItemDeleteFrequencyTestResult.Name = "toolStripMenuItemDeleteFrequencyTestResult";
            this.toolStripMenuItemDeleteFrequencyTestResult.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItemDeleteFrequencyTestResult.Text = "Delete";
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
            this.gridColumnResult,
            this.gridColumnTimesPerWeek,
            this.gridColumnNumberOfWeeks,
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
            this.gridViewResults.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewResults.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewResults.OptionsView.ShowDetailButtons = false;
            this.gridViewResults.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewResults.OptionsView.ShowGroupPanel = false;
            this.gridViewResults.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            this.gridViewResults.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridViewResults_SelectionChanged);
            this.gridViewResults.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewResults_FocusedRowChanged);
            this.gridViewResults.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewResults_CellValueChanging);
            // 
            // gridColumnResult
            // 
            this.gridColumnResult.Caption = "Result";
            this.gridColumnResult.FieldName = "Item.Name";
            this.gridColumnResult.Name = "gridColumnResult";
            this.gridColumnResult.OptionsColumn.AllowEdit = false;
            this.gridColumnResult.OptionsColumn.AllowFocus = false;
            this.gridColumnResult.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnResult.OptionsFilter.AllowFilter = false;
            this.gridColumnResult.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumnResult.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.gridColumnResult.Visible = true;
            this.gridColumnResult.VisibleIndex = 0;
            this.gridColumnResult.Width = 94;
            // 
            // gridColumnTimesPerWeek
            // 
            this.gridColumnTimesPerWeek.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnTimesPerWeek.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnTimesPerWeek.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnTimesPerWeek.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnTimesPerWeek.Caption = "Times Per Week";
            this.gridColumnTimesPerWeek.ColumnEdit = this.repositoryItemSpinEditNoPerWeek;
            this.gridColumnTimesPerWeek.FieldName = "TimesPerWeek";
            this.gridColumnTimesPerWeek.Name = "gridColumnTimesPerWeek";
            this.gridColumnTimesPerWeek.Visible = true;
            this.gridColumnTimesPerWeek.VisibleIndex = 1;
            this.gridColumnTimesPerWeek.Width = 112;
            // 
            // repositoryItemSpinEditNoPerWeek
            // 
            this.repositoryItemSpinEditNoPerWeek.AutoHeight = false;
            this.repositoryItemSpinEditNoPerWeek.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditNoPerWeek.IsFloatValue = false;
            this.repositoryItemSpinEditNoPerWeek.Mask.EditMask = "N00";
            this.repositoryItemSpinEditNoPerWeek.MaxValue = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.repositoryItemSpinEditNoPerWeek.Name = "repositoryItemSpinEditNoPerWeek";
            // 
            // gridColumnNumberOfWeeks
            // 
            this.gridColumnNumberOfWeeks.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnNumberOfWeeks.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNumberOfWeeks.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnNumberOfWeeks.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNumberOfWeeks.Caption = "Number Of Weeks";
            this.gridColumnNumberOfWeeks.ColumnEdit = this.repositoryItemSpinEditNoPerWeek;
            this.gridColumnNumberOfWeeks.FieldName = "NumberOfWeeks";
            this.gridColumnNumberOfWeeks.Name = "gridColumnNumberOfWeeks";
            this.gridColumnNumberOfWeeks.Visible = true;
            this.gridColumnNumberOfWeeks.VisibleIndex = 2;
            this.gridColumnNumberOfWeeks.Width = 120;
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
            this.gridColumnNotes.VisibleIndex = 3;
            this.gridColumnNotes.Width = 136;
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
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(484, 401);
            this.layoutControlGroup1.Text = "Root";
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
            // simpleButtonOpen
            // 
            this.simpleButtonOpen.Image = global::Vital.UI.Properties.Resources.Patient_Open;
            this.simpleButtonOpen.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonOpen.Location = new System.Drawing.Point(456, 2);
            this.simpleButtonOpen.Name = "simpleButtonOpen";
            this.simpleButtonOpen.Size = new System.Drawing.Size(26, 22);
            this.simpleButtonOpen.StyleController = this.layoutControl1;
            toolTipTitleItem2.Text = "Open  (Enter)";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Open selected result.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.simpleButtonOpen.SuperTip = superToolTip2;
            this.simpleButtonOpen.TabIndex = 23;
            this.simpleButtonOpen.Click += new System.EventHandler(this.simpleButtonOpen_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButtonOpen;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(454, 0);
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
            this.emptySpaceItem1.Location = new System.Drawing.Point(454, 52);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(30, 349);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleButtonDelete
            // 
            this.simpleButtonDelete.Image = global::Vital.UI.Properties.Resources.delete;
            this.simpleButtonDelete.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonDelete.Location = new System.Drawing.Point(456, 28);
            this.simpleButtonDelete.Name = "simpleButtonDelete";
            this.simpleButtonDelete.Size = new System.Drawing.Size(26, 22);
            this.simpleButtonDelete.StyleController = this.layoutControl1;
            toolTipTitleItem1.Text = "Delete (Delete)";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Delete Selected Results";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.simpleButtonDelete.SuperTip = superToolTip1;
            this.simpleButtonDelete.TabIndex = 24;
            this.simpleButtonDelete.Click += new System.EventHandler(this.simpleButtonDelete_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButtonDelete;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(454, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(30, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // XtraUserControlFrequencyTestResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraUserControlFrequencyTestResults";
            this.Size = new System.Drawing.Size(484, 401);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlResults)).EndInit();
            this.contextMenuStripFrequencyTestResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNoPerWeek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControlResults;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewResults;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnResult;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditNoPerWeek;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNotes;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFrequencyTestResult;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteFrequencyTestResult;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTimesPerWeek;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNumberOfWeeks;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOpen;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDelete;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}
