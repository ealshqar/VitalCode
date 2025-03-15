namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormSelectItem
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
            this.layoutControlSelectItem = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonSelect = new DevExpress.XtraEditors.SimpleButton();
            this.searchLookUpEditSelectItem = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEditViewSelectItem = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroupSelectItem = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemSearchLookup = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSelect = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlSelectItem)).BeginInit();
            this.layoutControlSelectItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditSelectItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditViewSelectItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupSelectItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSearchLookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSelect)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlSelectItem
            // 
            this.layoutControlSelectItem.AllowCustomizationMenu = false;
            this.layoutControlSelectItem.Controls.Add(this.simpleButtonSelect);
            this.layoutControlSelectItem.Controls.Add(this.searchLookUpEditSelectItem);
            this.layoutControlSelectItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlSelectItem.Location = new System.Drawing.Point(0, 0);
            this.layoutControlSelectItem.Name = "layoutControlSelectItem";
            this.layoutControlSelectItem.Root = this.layoutControlGroupSelectItem;
            this.layoutControlSelectItem.Size = new System.Drawing.Size(563, 47);
            this.layoutControlSelectItem.TabIndex = 0;
            this.layoutControlSelectItem.Text = "layoutControl1";
            // 
            // simpleButtonSelect
            // 
            this.simpleButtonSelect.Location = new System.Drawing.Point(461, 12);
            this.simpleButtonSelect.Name = "simpleButtonSelect";
            this.simpleButtonSelect.Size = new System.Drawing.Size(90, 22);
            this.simpleButtonSelect.StyleController = this.layoutControlSelectItem;
            this.simpleButtonSelect.TabIndex = 5;
            this.simpleButtonSelect.Text = "Select";
            this.simpleButtonSelect.Click += new System.EventHandler(this.simpleButtonSelect_Click);
            // 
            // searchLookUpEditSelectItem
            // 
            this.searchLookUpEditSelectItem.Location = new System.Drawing.Point(12, 12);
            this.searchLookUpEditSelectItem.Name = "searchLookUpEditSelectItem";
            this.searchLookUpEditSelectItem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEditSelectItem.Properties.DisplayMember = "Name";
            this.searchLookUpEditSelectItem.Properties.NullText = "";
            this.searchLookUpEditSelectItem.Properties.NullValuePrompt = "Select an Item";
            this.searchLookUpEditSelectItem.Properties.NullValuePromptShowForEmptyValue = true;
            this.searchLookUpEditSelectItem.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.searchLookUpEditSelectItem.Properties.ShowFooter = false;
            this.searchLookUpEditSelectItem.Properties.ValueMember = "Id";
            this.searchLookUpEditSelectItem.Properties.View = this.searchLookUpEditViewSelectItem;
            this.searchLookUpEditSelectItem.Size = new System.Drawing.Size(445, 20);
            this.searchLookUpEditSelectItem.StyleController = this.layoutControlSelectItem;
            this.searchLookUpEditSelectItem.TabIndex = 4;
            this.searchLookUpEditSelectItem.Popup += new System.EventHandler(this.searchLookUpEditSelectItem_Popup);
            this.searchLookUpEditSelectItem.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(this.searchLookUpEditSelectItem_CloseUp);
            // 
            // searchLookUpEditViewSelectItem
            // 
            this.searchLookUpEditViewSelectItem.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnItemName,
            this.gridColumnDescription});
            this.searchLookUpEditViewSelectItem.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEditViewSelectItem.Name = "searchLookUpEditViewSelectItem";
            this.searchLookUpEditViewSelectItem.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEditViewSelectItem.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnItemName
            // 
            this.gridColumnItemName.Caption = "Name";
            this.gridColumnItemName.FieldName = "Name";
            this.gridColumnItemName.Name = "gridColumnItemName";
            this.gridColumnItemName.Visible = true;
            this.gridColumnItemName.VisibleIndex = 0;
            // 
            // gridColumnDescription
            // 
            this.gridColumnDescription.Caption = "Description";
            this.gridColumnDescription.FieldName = "Description";
            this.gridColumnDescription.Name = "gridColumnDescription";
            this.gridColumnDescription.Visible = true;
            this.gridColumnDescription.VisibleIndex = 1;
            // 
            // layoutControlGroupSelectItem
            // 
            this.layoutControlGroupSelectItem.CustomizationFormText = "layoutControlGroupSelectItem";
            this.layoutControlGroupSelectItem.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupSelectItem.GroupBordersVisible = false;
            this.layoutControlGroupSelectItem.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemSearchLookup,
            this.layoutControlItemSelect});
            this.layoutControlGroupSelectItem.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupSelectItem.Name = "layoutControlGroupSelectItem";
            this.layoutControlGroupSelectItem.Size = new System.Drawing.Size(563, 47);
            this.layoutControlGroupSelectItem.Text = "layoutControlGroupSelectItem";
            this.layoutControlGroupSelectItem.TextVisible = false;
            // 
            // layoutControlItemSearchLookup
            // 
            this.layoutControlItemSearchLookup.Control = this.searchLookUpEditSelectItem;
            this.layoutControlItemSearchLookup.CustomizationFormText = "Select an item";
            this.layoutControlItemSearchLookup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemSearchLookup.Name = "layoutControlItemSearchLookup";
            this.layoutControlItemSearchLookup.Size = new System.Drawing.Size(449, 27);
            this.layoutControlItemSearchLookup.Text = "Select an item";
            this.layoutControlItemSearchLookup.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSearchLookup.TextToControlDistance = 0;
            this.layoutControlItemSearchLookup.TextVisible = false;
            // 
            // layoutControlItemSelect
            // 
            this.layoutControlItemSelect.Control = this.simpleButtonSelect;
            this.layoutControlItemSelect.CustomizationFormText = "layoutControlItemSelect";
            this.layoutControlItemSelect.Location = new System.Drawing.Point(449, 0);
            this.layoutControlItemSelect.Name = "layoutControlItemSelect";
            this.layoutControlItemSelect.Size = new System.Drawing.Size(94, 27);
            this.layoutControlItemSelect.Text = "layoutControlItemSelect";
            this.layoutControlItemSelect.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSelect.TextToControlDistance = 0;
            this.layoutControlItemSelect.TextVisible = false;
            // 
            // XtraFormSelectItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 47);
            this.Controls.Add(this.layoutControlSelectItem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "XtraFormSelectItem";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select an item";
            this.Activated += new System.EventHandler(this.XtraFormSelectItem_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XtraFormSelectItem_FormClosing);
            this.Load += new System.EventHandler(this.XtraFormSelectItem_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XtraFormSelectItem_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlSelectItem)).EndInit();
            this.layoutControlSelectItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditSelectItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEditViewSelectItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupSelectItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSearchLookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSelect)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlSelectItem;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupSelectItem;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEditSelectItem;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEditViewSelectItem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSearchLookup;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSelect;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSelect;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnItemName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDescription;
    }
}