namespace Vital.UI.UI_Components.User_Controls.Modules
{
    partial class XtraUserControlProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraUserControlProperties));
            this.gridControlProperties = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStripProperties = new System.Windows.Forms.ContextMenuStrip();
            this.deleteToolStripMenuItemDeleteProperty = new System.Windows.Forms.ToolStripMenuItem();
            this.gridViewProperties = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnProperty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEditProperties = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumnValue = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProperties)).BeginInit();
            this.contextMenuStripProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlProperties
            // 
            this.gridControlProperties.ContextMenuStrip = this.contextMenuStripProperties;
            this.gridControlProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlProperties.Location = new System.Drawing.Point(0, 0);
            this.gridControlProperties.MainView = this.gridViewProperties;
            this.gridControlProperties.Name = "gridControlProperties";
            this.gridControlProperties.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEditProperties});
            this.gridControlProperties.Size = new System.Drawing.Size(330, 340);
            this.gridControlProperties.TabIndex = 0;
            this.gridControlProperties.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewProperties});
            // 
            // contextMenuStripProperties
            // 
            this.contextMenuStripProperties.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItemDeleteProperty});
            this.contextMenuStripProperties.Name = "contextMenuStripProperties";
            this.contextMenuStripProperties.Size = new System.Drawing.Size(108, 26);
            this.contextMenuStripProperties.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripOpening);
            this.contextMenuStripProperties.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ContextMenuStripItemClicked);
            // 
            // deleteToolStripMenuItemDeleteProperty
            // 
            this.deleteToolStripMenuItemDeleteProperty.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItemDeleteProperty.Image")));
            this.deleteToolStripMenuItemDeleteProperty.Name = "deleteToolStripMenuItemDeleteProperty";
            this.deleteToolStripMenuItemDeleteProperty.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItemDeleteProperty.Text = "Delete";
            // 
            // gridViewProperties
            // 
            this.gridViewProperties.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnProperty,
            this.gridColumnValue});
            this.gridViewProperties.GridControl = this.gridControlProperties;
            this.gridViewProperties.Name = "gridViewProperties";
            this.gridViewProperties.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewProperties.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewProperties.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridViewProperties.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewProperties.OptionsMenu.EnableColumnMenu = false;
            this.gridViewProperties.OptionsNavigation.UseTabKey = false;
            this.gridViewProperties.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewProperties.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridViewProperties.OptionsView.ShowDetailButtons = false;
            this.gridViewProperties.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewProperties.OptionsView.ShowGroupPanel = false;
            this.gridViewProperties.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            this.gridViewProperties.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridViewProperties_ShowingEditor);
            this.gridViewProperties.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridViewProperties_InitNewRow);
            this.gridViewProperties.HiddenEditor += new System.EventHandler(this.gridViewProperties_HiddenEditor);
            this.gridViewProperties.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewProperties_CellValueChanged);
            this.gridViewProperties.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gridViewProperties_InvalidRowException);
            this.gridViewProperties.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gridViewProperties_ValidateRow);
            this.gridViewProperties.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.gridViewProperties_ValidatingEditor);
            // 
            // gridColumnProperty
            // 
            this.gridColumnProperty.Caption = "Property";
            this.gridColumnProperty.ColumnEdit = this.repositoryItemLookUpEditProperties;
            this.gridColumnProperty.FieldName = "Property.Id";
            this.gridColumnProperty.Name = "gridColumnProperty";
            this.gridColumnProperty.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedCell;
            this.gridColumnProperty.Visible = true;
            this.gridColumnProperty.VisibleIndex = 0;
            this.gridColumnProperty.Width = 180;
            // 
            // repositoryItemLookUpEditProperties
            // 
            this.repositoryItemLookUpEditProperties.AutoHeight = false;
            this.repositoryItemLookUpEditProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEditProperties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name")});
            this.repositoryItemLookUpEditProperties.DisplayMember = "Name";
            this.repositoryItemLookUpEditProperties.Name = "repositoryItemLookUpEditProperties";
            this.repositoryItemLookUpEditProperties.NullText = "";
            this.repositoryItemLookUpEditProperties.ThrowExceptionOnInvalidLookUpEditValueType = true;
            this.repositoryItemLookUpEditProperties.ValueMember = "Id";
            // 
            // gridColumnValue
            // 
            this.gridColumnValue.Caption = "Value";
            this.gridColumnValue.FieldName = "UnboundValue";
            this.gridColumnValue.Name = "gridColumnValue";
            this.gridColumnValue.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnValue.OptionsFilter.AllowFilter = false;
            this.gridColumnValue.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gridColumnValue.Visible = true;
            this.gridColumnValue.VisibleIndex = 1;
            this.gridColumnValue.Width = 132;
            // 
            // XtraUserControlProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlProperties);
            this.Name = "XtraUserControlProperties";
            this.Size = new System.Drawing.Size(330, 340);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProperties)).EndInit();
            this.contextMenuStripProperties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditProperties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlProperties;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProperties;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProperty;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnValue;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditProperties;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripProperties;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItemDeleteProperty;
    }
}
