namespace Vital.UI.UI_Components.User_Controls.Modules
{
    partial class XtraUserControlAutoTestResults
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
            this.treeListTestingResults = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumnName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.TreeListColumnNotes = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoEditNotes = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.treeListColumnIsDeleted = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.treeListTestingResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEditNotes)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListTestingResults
            // 
            this.treeListTestingResults.Appearance.FocusedRow.Font = new System.Drawing.Font("Open Sans Semibold", 12F);
            this.treeListTestingResults.Appearance.FocusedRow.Options.UseFont = true;
            this.treeListTestingResults.Appearance.HeaderPanel.Font = new System.Drawing.Font("Open Sans Light", 12F);
            this.treeListTestingResults.Appearance.HeaderPanel.Options.UseFont = true;
            this.treeListTestingResults.Appearance.Row.Font = new System.Drawing.Font("Open Sans Light", 12F);
            this.treeListTestingResults.Appearance.Row.Options.UseFont = true;
            this.treeListTestingResults.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnName,
            this.TreeListColumnNotes,
            this.treeListColumnIsDeleted});
            this.treeListTestingResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListTestingResults.KeyFieldName = "StructureId";
            this.treeListTestingResults.Location = new System.Drawing.Point(0, 0);
            this.treeListTestingResults.Name = "treeListTestingResults";
            this.treeListTestingResults.OptionsBehavior.AutoFocusNewNode = true;
            this.treeListTestingResults.OptionsBehavior.EnableFiltering = true;
            this.treeListTestingResults.OptionsMenu.EnableColumnMenu = false;
            this.treeListTestingResults.OptionsMenu.EnableFooterMenu = false;
            this.treeListTestingResults.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeListTestingResults.OptionsView.EnableAppearanceEvenRow = true;
            this.treeListTestingResults.OptionsView.ShowFilterPanelMode = DevExpress.XtraTreeList.ShowFilterPanelMode.Never;
            this.treeListTestingResults.ParentFieldName = "StructureParentId";
            this.treeListTestingResults.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEditNotes});
            this.treeListTestingResults.Size = new System.Drawing.Size(653, 560);
            this.treeListTestingResults.TabIndex = 0;
            this.treeListTestingResults.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeListTestingResults_MouseDown);
            // 
            // treeListColumnName
            // 
            this.treeListColumnName.Caption = "Results";
            this.treeListColumnName.FieldName = "AutoItem.Name";
            this.treeListColumnName.Name = "treeListColumnName";
            this.treeListColumnName.OptionsColumn.AllowMove = false;
            this.treeListColumnName.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.treeListColumnName.OptionsColumn.AllowSize = false;
            this.treeListColumnName.OptionsColumn.AllowSort = false;
            this.treeListColumnName.OptionsColumn.ReadOnly = true;
            this.treeListColumnName.OptionsColumn.ShowInCustomizationForm = false;
            this.treeListColumnName.Visible = true;
            this.treeListColumnName.VisibleIndex = 0;
            // 
            // TreeListColumnNotes
            // 
            this.TreeListColumnNotes.Caption = "Notes";
            this.TreeListColumnNotes.ColumnEdit = this.repositoryItemMemoEditNotes;
            this.TreeListColumnNotes.FieldName = "Notes";
            this.TreeListColumnNotes.Name = "TreeListColumnNotes";
            this.TreeListColumnNotes.Visible = true;
            this.TreeListColumnNotes.VisibleIndex = 1;
            // 
            // repositoryItemMemoEditNotes
            // 
            this.repositoryItemMemoEditNotes.Name = "repositoryItemMemoEditNotes";
            // 
            // treeListColumnIsDeleted
            // 
            this.treeListColumnIsDeleted.Caption = "IsDeleted";
            this.treeListColumnIsDeleted.FieldName = "IsDeletedMemory";
            this.treeListColumnIsDeleted.Name = "treeListColumnIsDeleted";
            // 
            // XtraUserControlAutoTestResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeListTestingResults);
            this.Name = "XtraUserControlAutoTestResults";
            this.Size = new System.Drawing.Size(653, 560);
            ((System.ComponentModel.ISupportInitialize)(this.treeListTestingResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEditNotes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeListTestingResults;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnIsDeleted;
        public DevExpress.XtraTreeList.Columns.TreeListColumn TreeListColumnNotes;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEditNotes;
    }
}
