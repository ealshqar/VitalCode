namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormAddAutoResult
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
            this.layoutControlMain = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonAddSelected = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonSearch = new DevExpress.XtraEditors.SimpleButton();
            this.textEditSearchKeyword = new DevExpress.XtraEditors.TextEdit();
            this.gridControlAutoItems = new DevExpress.XtraGrid.GridControl();
            this.gridViewAutoItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnChecked = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEditIsChecked = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroupMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemAutoItems = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSearchKeyword = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSearch = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemAddSelected = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
            this.layoutControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSearchKeyword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAutoItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAutoItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditIsChecked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAutoItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSearchKeyword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAddSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMain
            // 
            this.layoutControlMain.AllowCustomizationMenu = false;
            this.layoutControlMain.Controls.Add(this.simpleButtonCancel);
            this.layoutControlMain.Controls.Add(this.simpleButtonAddSelected);
            this.layoutControlMain.Controls.Add(this.simpleButtonSearch);
            this.layoutControlMain.Controls.Add(this.textEditSearchKeyword);
            this.layoutControlMain.Controls.Add(this.gridControlAutoItems);
            this.layoutControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMain.Name = "layoutControlMain";
            this.layoutControlMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(725, 357, 250, 350);
            this.layoutControlMain.Root = this.layoutControlGroupMain;
            this.layoutControlMain.Size = new System.Drawing.Size(496, 554);
            this.layoutControlMain.TabIndex = 0;
            this.layoutControlMain.Text = "layoutControl1";
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.Image = global::Vital.UI.Properties.Resources.delete;
            this.simpleButtonCancel.Location = new System.Drawing.Point(253, 530);
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(115, 22);
            this.simpleButtonCancel.StyleController = this.layoutControlMain;
            this.simpleButtonCancel.TabIndex = 8;
            this.simpleButtonCancel.Text = "Cancel";
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // simpleButtonAddSelected
            // 
            this.simpleButtonAddSelected.Image = global::Vital.UI.Properties.Resources.add16x16;
            this.simpleButtonAddSelected.Location = new System.Drawing.Point(372, 530);
            this.simpleButtonAddSelected.Name = "simpleButtonAddSelected";
            this.simpleButtonAddSelected.Size = new System.Drawing.Size(122, 22);
            this.simpleButtonAddSelected.StyleController = this.layoutControlMain;
            this.simpleButtonAddSelected.TabIndex = 7;
            this.simpleButtonAddSelected.Text = "Add Selected";
            this.simpleButtonAddSelected.Click += new System.EventHandler(this.simpleButtonAddSelected_Click);
            // 
            // simpleButtonSearch
            // 
            this.simpleButtonSearch.Image = global::Vital.UI.Properties.Resources.Search16;
            this.simpleButtonSearch.Location = new System.Drawing.Point(388, 2);
            this.simpleButtonSearch.Name = "simpleButtonSearch";
            this.simpleButtonSearch.Size = new System.Drawing.Size(106, 22);
            this.simpleButtonSearch.StyleController = this.layoutControlMain;
            this.simpleButtonSearch.TabIndex = 6;
            this.simpleButtonSearch.Text = "Search";
            this.simpleButtonSearch.Click += new System.EventHandler(this.simpleButtonSearch_Click);
            // 
            // textEditSearchKeyword
            // 
            this.textEditSearchKeyword.Location = new System.Drawing.Point(38, 2);
            this.textEditSearchKeyword.Name = "textEditSearchKeyword";
            this.textEditSearchKeyword.Properties.NullValuePrompt = "Search for an item to add";
            this.textEditSearchKeyword.Size = new System.Drawing.Size(346, 20);
            this.textEditSearchKeyword.StyleController = this.layoutControlMain;
            this.textEditSearchKeyword.TabIndex = 5;
            this.textEditSearchKeyword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditSearchKeyword_KeyDown);
            // 
            // gridControlAutoItems
            // 
            this.gridControlAutoItems.Location = new System.Drawing.Point(0, 26);
            this.gridControlAutoItems.MainView = this.gridViewAutoItems;
            this.gridControlAutoItems.Name = "gridControlAutoItems";
            this.gridControlAutoItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEditIsChecked});
            this.gridControlAutoItems.Size = new System.Drawing.Size(496, 502);
            this.gridControlAutoItems.TabIndex = 4;
            this.gridControlAutoItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAutoItems});
            // 
            // gridViewAutoItems
            // 
            this.gridViewAutoItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnChecked,
            this.gridColumnName,
            this.gridColumnDescription,
            this.gridColumnType});
            this.gridViewAutoItems.GridControl = this.gridControlAutoItems;
            this.gridViewAutoItems.Name = "gridViewAutoItems";
            this.gridViewAutoItems.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewAutoItems.OptionsCustomization.AllowGroup = false;
            this.gridViewAutoItems.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewAutoItems.OptionsView.ShowDetailButtons = false;
            this.gridViewAutoItems.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnChecked
            // 
            this.gridColumnChecked.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnChecked.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnChecked.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnChecked.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnChecked.Caption = "✓";
            this.gridColumnChecked.ColumnEdit = this.repositoryItemCheckEditIsChecked;
            this.gridColumnChecked.FieldName = "IsChecked";
            this.gridColumnChecked.Name = "gridColumnChecked";
            this.gridColumnChecked.Visible = true;
            this.gridColumnChecked.VisibleIndex = 0;
            this.gridColumnChecked.Width = 37;
            // 
            // repositoryItemCheckEditIsChecked
            // 
            this.repositoryItemCheckEditIsChecked.AutoHeight = false;
            this.repositoryItemCheckEditIsChecked.Name = "repositoryItemCheckEditIsChecked";
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "Name";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.OptionsColumn.AllowFocus = false;
            this.gridColumnName.OptionsColumn.ReadOnly = true;
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 1;
            this.gridColumnName.Width = 120;
            // 
            // gridColumnDescription
            // 
            this.gridColumnDescription.Caption = "Description";
            this.gridColumnDescription.FieldName = "Description";
            this.gridColumnDescription.Name = "gridColumnDescription";
            this.gridColumnDescription.OptionsColumn.AllowFocus = false;
            this.gridColumnDescription.OptionsColumn.ReadOnly = true;
            this.gridColumnDescription.Visible = true;
            this.gridColumnDescription.VisibleIndex = 3;
            this.gridColumnDescription.Width = 252;
            // 
            // gridColumnType
            // 
            this.gridColumnType.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnType.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnType.Caption = "Type";
            this.gridColumnType.FieldName = "Type.Value";
            this.gridColumnType.Name = "gridColumnType";
            this.gridColumnType.OptionsColumn.AllowFocus = false;
            this.gridColumnType.OptionsColumn.ReadOnly = true;
            this.gridColumnType.Visible = true;
            this.gridColumnType.VisibleIndex = 2;
            this.gridColumnType.Width = 69;
            // 
            // layoutControlGroupMain
            // 
            this.layoutControlGroupMain.CustomizationFormText = "layoutControlGroupMain";
            this.layoutControlGroupMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMain.GroupBordersVisible = false;
            this.layoutControlGroupMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemAutoItems,
            this.layoutControlItemSearchKeyword,
            this.layoutControlItemSearch,
            this.layoutControlItemAddSelected,
            this.emptySpaceItem1,
            this.layoutControlItemCancel});
            this.layoutControlGroupMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMain.Name = "Root";
            this.layoutControlGroupMain.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroupMain.Size = new System.Drawing.Size(496, 554);
            this.layoutControlGroupMain.Text = "Root";
            this.layoutControlGroupMain.TextVisible = false;
            // 
            // layoutControlItemAutoItems
            // 
            this.layoutControlItemAutoItems.Control = this.gridControlAutoItems;
            this.layoutControlItemAutoItems.CustomizationFormText = "layoutControlItemAutoItems";
            this.layoutControlItemAutoItems.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemAutoItems.Name = "layoutControlItemAutoItems";
            this.layoutControlItemAutoItems.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItemAutoItems.Size = new System.Drawing.Size(496, 502);
            this.layoutControlItemAutoItems.Text = "layoutControlItemAutoItems";
            this.layoutControlItemAutoItems.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemAutoItems.TextToControlDistance = 0;
            this.layoutControlItemAutoItems.TextVisible = false;
            // 
            // layoutControlItemSearchKeyword
            // 
            this.layoutControlItemSearchKeyword.Control = this.textEditSearchKeyword;
            this.layoutControlItemSearchKeyword.CustomizationFormText = "Search for items";
            this.layoutControlItemSearchKeyword.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemSearchKeyword.Name = "layoutControlItemSearchKeyword";
            this.layoutControlItemSearchKeyword.Size = new System.Drawing.Size(386, 26);
            this.layoutControlItemSearchKeyword.Text = "Search";
            this.layoutControlItemSearchKeyword.TextSize = new System.Drawing.Size(33, 13);
            // 
            // layoutControlItemSearch
            // 
            this.layoutControlItemSearch.Control = this.simpleButtonSearch;
            this.layoutControlItemSearch.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItemSearch.Location = new System.Drawing.Point(386, 0);
            this.layoutControlItemSearch.Name = "layoutControlItemSearch";
            this.layoutControlItemSearch.Size = new System.Drawing.Size(110, 26);
            this.layoutControlItemSearch.Text = "layoutControlItemSearch";
            this.layoutControlItemSearch.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSearch.TextToControlDistance = 0;
            this.layoutControlItemSearch.TextVisible = false;
            // 
            // layoutControlItemAddSelected
            // 
            this.layoutControlItemAddSelected.Control = this.simpleButtonAddSelected;
            this.layoutControlItemAddSelected.CustomizationFormText = "layoutControlItemAddSelected";
            this.layoutControlItemAddSelected.Location = new System.Drawing.Point(370, 528);
            this.layoutControlItemAddSelected.Name = "layoutControlItemAddSelected";
            this.layoutControlItemAddSelected.Size = new System.Drawing.Size(126, 26);
            this.layoutControlItemAddSelected.Text = "layoutControlItemAddSelected";
            this.layoutControlItemAddSelected.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemAddSelected.TextToControlDistance = 0;
            this.layoutControlItemAddSelected.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 528);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(251, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.simpleButtonCancel;
            this.layoutControlItemCancel.CustomizationFormText = "layoutControlItemCancel";
            this.layoutControlItemCancel.Location = new System.Drawing.Point(251, 528);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(119, 26);
            this.layoutControlItemCancel.Text = "layoutControlItemCancel";
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextToControlDistance = 0;
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // XtraFormAddAutoResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 554);
            this.Controls.Add(this.layoutControlMain);
            this.Name = "XtraFormAddAutoResult";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Result";
            this.Load += new System.EventHandler(this.XtraFormAddAutoResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
            this.layoutControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditSearchKeyword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAutoItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAutoItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditIsChecked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAutoItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSearchKeyword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAddSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMain;
        private DevExpress.XtraGrid.GridControl gridControlAutoItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAutoItems;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAutoItems;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDescription;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnChecked;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditIsChecked;
        private DevExpress.XtraEditors.TextEdit textEditSearchKeyword;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSearchKeyword;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSearch;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonAddSelected;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAddSelected;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
    }
}