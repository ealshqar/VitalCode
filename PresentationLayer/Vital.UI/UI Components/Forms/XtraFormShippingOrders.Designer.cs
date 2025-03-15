namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormShippingOrders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraFormShippingOrders));
            this.layoutControlShippingOrders = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlShippingOrders = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStripShippingOrders = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemNewOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDeleteOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSendOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.gridViewShippingOrders = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnOrderNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSentDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnOrderSent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumnTarget = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnOrderComments = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.simpleButtonSendShippingOrder = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroupShippingOrders = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemShippingOrdersGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlShippingOrders)).BeginInit();
            this.layoutControlShippingOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlShippingOrders)).BeginInit();
            this.contextMenuStripShippingOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewShippingOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupShippingOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemShippingOrdersGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlShippingOrders
            // 
            this.layoutControlShippingOrders.AllowCustomizationMenu = false;
            this.layoutControlShippingOrders.Controls.Add(this.gridControlShippingOrders);
            this.layoutControlShippingOrders.Controls.Add(this.simpleButtonSendShippingOrder);
            this.layoutControlShippingOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlShippingOrders.Location = new System.Drawing.Point(0, 0);
            this.layoutControlShippingOrders.Name = "layoutControlShippingOrders";
            this.layoutControlShippingOrders.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControlShippingOrders.Root = this.layoutControlGroupShippingOrders;
            this.layoutControlShippingOrders.Size = new System.Drawing.Size(798, 570);
            this.layoutControlShippingOrders.TabIndex = 1;
            this.layoutControlShippingOrders.Text = "layoutControl1";
            // 
            // gridControlShippingOrders
            // 
            this.gridControlShippingOrders.ContextMenuStrip = this.contextMenuStripShippingOrders;
            this.gridControlShippingOrders.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControlShippingOrders.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControlShippingOrders.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControlShippingOrders.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControlShippingOrders.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControlShippingOrders.Location = new System.Drawing.Point(2, 28);
            this.gridControlShippingOrders.MainView = this.gridViewShippingOrders;
            this.gridControlShippingOrders.Name = "gridControlShippingOrders";
            this.gridControlShippingOrders.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.repositoryItemSpinEdit1,
            this.repositoryItemCheckEdit1});
            this.gridControlShippingOrders.Size = new System.Drawing.Size(794, 540);
            this.gridControlShippingOrders.TabIndex = 18;
            this.gridControlShippingOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewShippingOrders});
            // 
            // contextMenuStripShippingOrders
            // 
            this.contextMenuStripShippingOrders.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemNewOrder,
            this.toolStripMenuItemOpenOrder,
            this.toolStripMenuItemDeleteOrder,
            this.toolStripMenuItemSendOrder});
            this.contextMenuStripShippingOrders.Name = "contextMenuStrip1";
            this.contextMenuStripShippingOrders.Size = new System.Drawing.Size(134, 92);
            this.contextMenuStripShippingOrders.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripOpening);
            this.contextMenuStripShippingOrders.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ContextMenuStrip_ItemClicked);
            // 
            // toolStripMenuItemNewOrder
            // 
            this.toolStripMenuItemNewOrder.Image = global::Vital.UI.Properties.Resources.Test_New;
            this.toolStripMenuItemNewOrder.Name = "toolStripMenuItemNewOrder";
            this.toolStripMenuItemNewOrder.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItemNewOrder.Text = "New";
            // 
            // toolStripMenuItemOpenOrder
            // 
            this.toolStripMenuItemOpenOrder.Image = global::Vital.UI.Properties.Resources.Patient_Open;
            this.toolStripMenuItemOpenOrder.Name = "toolStripMenuItemOpenOrder";
            this.toolStripMenuItemOpenOrder.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItemOpenOrder.Text = "Open";
            // 
            // toolStripMenuItemDeleteOrder
            // 
            this.toolStripMenuItemDeleteOrder.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemDeleteOrder.Image")));
            this.toolStripMenuItemDeleteOrder.Name = "toolStripMenuItemDeleteOrder";
            this.toolStripMenuItemDeleteOrder.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItemDeleteOrder.Text = "Delete";
            // 
            // toolStripMenuItemSendOrder
            // 
            this.toolStripMenuItemSendOrder.Image = global::Vital.UI.Properties.Resources.ShippingSmall;
            this.toolStripMenuItemSendOrder.Name = "toolStripMenuItemSendOrder";
            this.toolStripMenuItemSendOrder.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItemSendOrder.Text = "Send Order";
            // 
            // gridViewShippingOrders
            // 
            this.gridViewShippingOrders.Appearance.FocusedCell.BackColor = System.Drawing.Color.Transparent;
            this.gridViewShippingOrders.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewShippingOrders.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnOrderNumber,
            this.gridColumnSentDate,
            this.gridColumnOrderSent,
            this.gridColumnTarget,
            this.gridColumnOrderComments});
            this.gridViewShippingOrders.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewShippingOrders.GridControl = this.gridControlShippingOrders;
            this.gridViewShippingOrders.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Price", this.gridColumnOrderSent, "Subtotal:  {0:c2}")});
            this.gridViewShippingOrders.Name = "gridViewShippingOrders";
            this.gridViewShippingOrders.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewShippingOrders.OptionsBehavior.Editable = false;
            this.gridViewShippingOrders.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewShippingOrders.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewShippingOrders.OptionsFind.AllowFindPanel = false;
            this.gridViewShippingOrders.OptionsMenu.EnableColumnMenu = false;
            this.gridViewShippingOrders.OptionsMenu.EnableFooterMenu = false;
            this.gridViewShippingOrders.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewShippingOrders.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridViewShippingOrders.OptionsView.ShowAutoFilterRow = true;
            this.gridViewShippingOrders.OptionsView.ShowDetailButtons = false;
            this.gridViewShippingOrders.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewShippingOrders.OptionsView.ShowGroupPanel = false;
            this.gridViewShippingOrders.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewShippingOrders_RowStyle);
            this.gridViewShippingOrders.DoubleClick += new System.EventHandler(this.gridViewShippingOrders_DoubleClick);
            // 
            // gridColumnOrderNumber
            // 
            this.gridColumnOrderNumber.Caption = "Number";
            this.gridColumnOrderNumber.FieldName = "Number";
            this.gridColumnOrderNumber.Name = "gridColumnOrderNumber";
            this.gridColumnOrderNumber.Visible = true;
            this.gridColumnOrderNumber.VisibleIndex = 0;
            this.gridColumnOrderNumber.Width = 90;
            // 
            // gridColumnSentDate
            // 
            this.gridColumnSentDate.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnSentDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnSentDate.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnSentDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnSentDate.Caption = "Sent Date";
            this.gridColumnSentDate.FieldName = "SentDate";
            this.gridColumnSentDate.Name = "gridColumnSentDate";
            this.gridColumnSentDate.Visible = true;
            this.gridColumnSentDate.VisibleIndex = 1;
            this.gridColumnSentDate.Width = 118;
            // 
            // gridColumnOrderSent
            // 
            this.gridColumnOrderSent.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnOrderSent.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnOrderSent.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnOrderSent.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnOrderSent.Caption = "Order Sent?";
            this.gridColumnOrderSent.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumnOrderSent.FieldName = "Sent";
            this.gridColumnOrderSent.Name = "gridColumnOrderSent";
            this.gridColumnOrderSent.Visible = true;
            this.gridColumnOrderSent.VisibleIndex = 2;
            this.gridColumnOrderSent.Width = 105;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // gridColumnTarget
            // 
            this.gridColumnTarget.Caption = "Shipping Target";
            this.gridColumnTarget.FieldName = "ShippingTarget";
            this.gridColumnTarget.Name = "gridColumnTarget";
            this.gridColumnTarget.Visible = true;
            this.gridColumnTarget.VisibleIndex = 3;
            this.gridColumnTarget.Width = 89;
            // 
            // gridColumnOrderComments
            // 
            this.gridColumnOrderComments.Caption = "Comments";
            this.gridColumnOrderComments.FieldName = "Comments";
            this.gridColumnOrderComments.Name = "gridColumnOrderComments";
            this.gridColumnOrderComments.Visible = true;
            this.gridColumnOrderComments.VisibleIndex = 4;
            this.gridColumnOrderComments.Width = 287;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemLookUpEdit1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemLookUpEdit1.AppearanceDisabled.Options.UseTextOptions = true;
            this.repositoryItemLookUpEdit1.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemLookUpEdit1.AppearanceFocused.Options.UseTextOptions = true;
            this.repositoryItemLookUpEdit1.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemLookUpEdit1.AppearanceReadOnly.Options.UseTextOptions = true;
            this.repositoryItemLookUpEdit1.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Value", "Value")});
            this.repositoryItemLookUpEdit1.DisplayMember = "Value";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            this.repositoryItemLookUpEdit1.ValueMember = "Id";
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemSpinEdit1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit1.Mask.EditMask = "c";
            this.repositoryItemSpinEdit1.MaxValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // simpleButtonSendShippingOrder
            // 
            this.simpleButtonSendShippingOrder.Image = global::Vital.UI.Properties.Resources.ShippingSmall;
            this.simpleButtonSendShippingOrder.Location = new System.Drawing.Point(2, 2);
            this.simpleButtonSendShippingOrder.Name = "simpleButtonSendShippingOrder";
            this.simpleButtonSendShippingOrder.Size = new System.Drawing.Size(794, 22);
            this.simpleButtonSendShippingOrder.StyleController = this.layoutControlShippingOrders;
            this.simpleButtonSendShippingOrder.TabIndex = 18;
            this.simpleButtonSendShippingOrder.Text = "New Shipping Order";
            // 
            // layoutControlGroupShippingOrders
            // 
            this.layoutControlGroupShippingOrders.CustomizationFormText = "layoutControlGroupShippingOrders";
            this.layoutControlGroupShippingOrders.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupShippingOrders.GroupBordersVisible = false;
            this.layoutControlGroupShippingOrders.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemShippingOrdersGrid,
            this.layoutControlItem17});
            this.layoutControlGroupShippingOrders.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupShippingOrders.Name = "layoutControlGroupShippingOrders";
            this.layoutControlGroupShippingOrders.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroupShippingOrders.Size = new System.Drawing.Size(798, 570);
            this.layoutControlGroupShippingOrders.Text = "layoutControlGroupShippingOrders";
            this.layoutControlGroupShippingOrders.TextVisible = false;
            // 
            // layoutControlItemShippingOrdersGrid
            // 
            this.layoutControlItemShippingOrdersGrid.Control = this.gridControlShippingOrders;
            this.layoutControlItemShippingOrdersGrid.CustomizationFormText = "layoutControlItemShippingOrdersGrid";
            this.layoutControlItemShippingOrdersGrid.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemShippingOrdersGrid.Name = "layoutControlItemShippingOrdersGrid";
            this.layoutControlItemShippingOrdersGrid.Size = new System.Drawing.Size(798, 544);
            this.layoutControlItemShippingOrdersGrid.Text = "layoutControlItemShippingOrdersGrid";
            this.layoutControlItemShippingOrdersGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemShippingOrdersGrid.TextToControlDistance = 0;
            this.layoutControlItemShippingOrdersGrid.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.simpleButtonSendShippingOrder;
            this.layoutControlItem17.CustomizationFormText = "layoutControlItem17";
            this.layoutControlItem17.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(798, 26);
            this.layoutControlItem17.Text = "layoutControlItem17";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem17.TextToControlDistance = 0;
            this.layoutControlItem17.TextVisible = false;
            // 
            // XtraFormShippingOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 570);
            this.Controls.Add(this.layoutControlShippingOrders);
            this.Name = "XtraFormShippingOrders";
            this.Text = "XtraFormShippingOrders";
            this.Load += new System.EventHandler(this.XtraFormShippingOrders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlShippingOrders)).EndInit();
            this.layoutControlShippingOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlShippingOrders)).EndInit();
            this.contextMenuStripShippingOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewShippingOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupShippingOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemShippingOrdersGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlShippingOrders;
        private DevExpress.XtraGrid.GridControl gridControlShippingOrders;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewShippingOrders;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnOrderNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSentDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnOrderSent;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTarget;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnOrderComments;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSendShippingOrder;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupShippingOrders;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemShippingOrdersGrid;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripShippingOrders;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNewOrder;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenOrder;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteOrder;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSendOrder;
    }
}