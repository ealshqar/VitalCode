using DevExpress.XtraLayout.Utils;

namespace Vital.UI.UI_Components.Forms
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState1 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState2 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState3 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState4 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.xtraUserControlHwProfiles = new Vital.UI.UI_Components.User_Controls.Modules.XtraUserControlHwProfiles();
            this.gridControlServices = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStripServices = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDeleteService = new System.Windows.Forms.ToolStripMenuItem();
            this.gridViewServices = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnServiceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnServiceType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEditServiceType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumnServicePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEditServicePrice = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumnServiceIsDefault = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEditServiceIsDefault = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumnServiceDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnServiceComments = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonUseLogo = new DevExpress.XtraEditors.SimpleButton();
            this.pictureEditLogo = new DevExpress.XtraEditors.PictureEdit();
            this.buttonEditLogo = new DevExpress.XtraEditors.ButtonEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.tabbedControlGroupReportInfo = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutControlGroupLogoSettings = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemLogoImage = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroupLogo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemLogo = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupClinicInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.pictureEditSerialPort = new DevExpress.XtraEditors.PictureEdit();
            this.labelControlConnectionInfo = new DevExpress.XtraEditors.LabelControl();
            this.gaugeControlConnectionState = new DevExpress.XtraGauges.Win.GaugeControl();
            this.stateIndicatorGauge = new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorGauge();
            this.stateIndicatorComponentConnectionState = new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent();
            this.gridControlSettings = new DevExpress.XtraGrid.GridControl();
            this.gridViewSettings = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButtonRefreshConnection = new DevExpress.XtraEditors.SimpleButton();
            this.progressPanelRefreshConnection = new DevExpress.XtraWaitForm.ProgressPanel();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemSearching = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemConnectionInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroupRefresh = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.tabbedControlGroupReportSettings = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutControlGroupHwProfiles = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupReportSettings = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupServices = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemServices = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxErrorProviderServices = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProviderMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlServices)).BeginInit();
            this.contextMenuStripServices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewServices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditServiceType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditServicePrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditServiceIsDefault)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditLogo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditLogo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroupReportInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupLogoSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogoImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupClinicInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditSerialPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIndicatorGauge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIndicatorComponentConnectionState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSearching)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemConnectionInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroupReportSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupHwProfiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupReportSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupServices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemServices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProviderServices)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.layoutControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 39);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(4);
            this.panelControl1.Size = new System.Drawing.Size(743, 492);
            this.panelControl1.TabIndex = 4;
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.xtraUserControlHwProfiles);
            this.layoutControl1.Controls.Add(this.gridControlServices);
            this.layoutControl1.Controls.Add(this.layoutControl2);
            this.layoutControl1.Controls.Add(this.pictureEditSerialPort);
            this.layoutControl1.Controls.Add(this.labelControlConnectionInfo);
            this.layoutControl1.Controls.Add(this.gaugeControlConnectionState);
            this.layoutControl1.Controls.Add(this.gridControlSettings);
            this.layoutControl1.Controls.Add(this.simpleButtonRefreshConnection);
            this.layoutControl1.Controls.Add(this.progressPanelRefreshConnection);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(6, 6);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(635, 333, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(731, 480);
            this.layoutControl1.TabIndex = 8;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // xtraUserControlHwProfiles
            // 
            this.xtraUserControlHwProfiles.HwProfiles = null;
            this.xtraUserControlHwProfiles.Location = new System.Drawing.Point(16, 128);
            this.xtraUserControlHwProfiles.Name = "xtraUserControlHwProfiles";
            this.xtraUserControlHwProfiles.Size = new System.Drawing.Size(699, 336);
            this.xtraUserControlHwProfiles.TabIndex = 14;
            this.xtraUserControlHwProfiles.HwProfilesChanged += new Vital.UI.UI_Components.User_Controls.Modules.XtraUserControlHwProfiles.OnHwProfilesChanged(this.xtraUserControlHwProfiles_HwProfilesChanged);
            // 
            // gridControlServices
            // 
            this.gridControlServices.ContextMenuStrip = this.contextMenuStripServices;
            this.gridControlServices.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControlServices.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControlServices.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControlServices.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControlServices.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControlServices.Location = new System.Drawing.Point(16, 128);
            this.gridControlServices.MainView = this.gridViewServices;
            this.gridControlServices.Name = "gridControlServices";
            this.gridControlServices.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEditServiceType,
            this.repositoryItemSpinEditServicePrice,
            this.repositoryItemCheckEditServiceIsDefault});
            this.gridControlServices.Size = new System.Drawing.Size(699, 336);
            this.gridControlServices.TabIndex = 5;
            this.gridControlServices.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewServices});
            // 
            // contextMenuStripServices
            // 
            this.contextMenuStripServices.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeleteService});
            this.contextMenuStripServices.Name = "contextMenuStripTest";
            this.contextMenuStripServices.Size = new System.Drawing.Size(108, 26);
            this.contextMenuStripServices.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripOpening);
            this.contextMenuStripServices.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ContextMenuStripItemClicked);
            // 
            // toolStripMenuItemDeleteService
            // 
            this.toolStripMenuItemDeleteService.Enabled = false;
            this.toolStripMenuItemDeleteService.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemDeleteService.Image")));
            this.toolStripMenuItemDeleteService.Name = "toolStripMenuItemDeleteService";
            this.toolStripMenuItemDeleteService.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItemDeleteService.Text = "Delete";
            // 
            // gridViewServices
            // 
            this.gridViewServices.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnServiceName,
            this.gridColumnServiceType,
            this.gridColumnServicePrice,
            this.gridColumnServiceIsDefault,
            this.gridColumnServiceDescription,
            this.gridColumnServiceComments});
            this.gridViewServices.GridControl = this.gridControlServices;
            this.gridViewServices.Name = "gridViewServices";
            this.gridViewServices.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewServices.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewServices.OptionsCustomization.AllowGroup = false;
            this.gridViewServices.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewServices.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewServices.OptionsFind.AllowFindPanel = false;
            this.gridViewServices.OptionsMenu.EnableColumnMenu = false;
            this.gridViewServices.OptionsMenu.EnableFooterMenu = false;
            this.gridViewServices.OptionsNavigation.AutoFocusNewRow = true;
            this.gridViewServices.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewServices.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridViewServices.OptionsView.ShowAutoFilterRow = true;
            this.gridViewServices.OptionsView.ShowDetailButtons = false;
            this.gridViewServices.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewServices.OptionsView.ShowGroupPanel = false;
            this.gridViewServices.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            this.gridViewServices.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.GridViewServiceInitNewRow);
            this.gridViewServices.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewServices_CellValueChanging);
            // 
            // gridColumnServiceName
            // 
            this.gridColumnServiceName.Caption = "Service";
            this.gridColumnServiceName.FieldName = "Name";
            this.gridColumnServiceName.Name = "gridColumnServiceName";
            this.gridColumnServiceName.Visible = true;
            this.gridColumnServiceName.VisibleIndex = 0;
            this.gridColumnServiceName.Width = 173;
            // 
            // gridColumnServiceType
            // 
            this.gridColumnServiceType.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnServiceType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnServiceType.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnServiceType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnServiceType.Caption = "Type";
            this.gridColumnServiceType.ColumnEdit = this.repositoryItemLookUpEditServiceType;
            this.gridColumnServiceType.FieldName = "TypeLookup.Id";
            this.gridColumnServiceType.Name = "gridColumnServiceType";
            this.gridColumnServiceType.OptionsColumn.AllowEdit = false;
            this.gridColumnServiceType.OptionsColumn.ReadOnly = true;
            this.gridColumnServiceType.Visible = true;
            this.gridColumnServiceType.VisibleIndex = 1;
            this.gridColumnServiceType.Width = 78;
            // 
            // repositoryItemLookUpEditServiceType
            // 
            this.repositoryItemLookUpEditServiceType.Appearance.Options.UseTextOptions = true;
            this.repositoryItemLookUpEditServiceType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemLookUpEditServiceType.AppearanceDisabled.Options.UseTextOptions = true;
            this.repositoryItemLookUpEditServiceType.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemLookUpEditServiceType.AppearanceFocused.Options.UseTextOptions = true;
            this.repositoryItemLookUpEditServiceType.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemLookUpEditServiceType.AppearanceReadOnly.Options.UseTextOptions = true;
            this.repositoryItemLookUpEditServiceType.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemLookUpEditServiceType.AutoHeight = false;
            this.repositoryItemLookUpEditServiceType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEditServiceType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Value", "Value")});
            this.repositoryItemLookUpEditServiceType.DisplayMember = "Value";
            this.repositoryItemLookUpEditServiceType.Name = "repositoryItemLookUpEditServiceType";
            this.repositoryItemLookUpEditServiceType.NullText = "";
            this.repositoryItemLookUpEditServiceType.ValueMember = "Id";
            // 
            // gridColumnServicePrice
            // 
            this.gridColumnServicePrice.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnServicePrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnServicePrice.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnServicePrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnServicePrice.Caption = "Price";
            this.gridColumnServicePrice.ColumnEdit = this.repositoryItemSpinEditServicePrice;
            this.gridColumnServicePrice.DisplayFormat.FormatString = "c";
            this.gridColumnServicePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnServicePrice.FieldName = "Price";
            this.gridColumnServicePrice.Name = "gridColumnServicePrice";
            this.gridColumnServicePrice.Visible = true;
            this.gridColumnServicePrice.VisibleIndex = 2;
            this.gridColumnServicePrice.Width = 79;
            // 
            // repositoryItemSpinEditServicePrice
            // 
            this.repositoryItemSpinEditServicePrice.Appearance.Options.UseTextOptions = true;
            this.repositoryItemSpinEditServicePrice.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemSpinEditServicePrice.AutoHeight = false;
            this.repositoryItemSpinEditServicePrice.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditServicePrice.Mask.EditMask = "c";
            this.repositoryItemSpinEditServicePrice.MaxValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.repositoryItemSpinEditServicePrice.Name = "repositoryItemSpinEditServicePrice";
            this.repositoryItemSpinEditServicePrice.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.repositoryItemSpinEditServicePrice_EditValueChanging);
            // 
            // gridColumnServiceIsDefault
            // 
            this.gridColumnServiceIsDefault.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnServiceIsDefault.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnServiceIsDefault.Caption = "Is Default";
            this.gridColumnServiceIsDefault.ColumnEdit = this.repositoryItemCheckEditServiceIsDefault;
            this.gridColumnServiceIsDefault.FieldName = "IsDefault";
            this.gridColumnServiceIsDefault.Name = "gridColumnServiceIsDefault";
            this.gridColumnServiceIsDefault.Visible = true;
            this.gridColumnServiceIsDefault.VisibleIndex = 3;
            this.gridColumnServiceIsDefault.Width = 59;
            // 
            // repositoryItemCheckEditServiceIsDefault
            // 
            this.repositoryItemCheckEditServiceIsDefault.AutoHeight = false;
            this.repositoryItemCheckEditServiceIsDefault.Name = "repositoryItemCheckEditServiceIsDefault";
            this.repositoryItemCheckEditServiceIsDefault.CheckedChanged += new System.EventHandler(this.repositoryItemCheckEditServiceIsDefault_CheckedChanged);
            // 
            // gridColumnServiceDescription
            // 
            this.gridColumnServiceDescription.Caption = "Description";
            this.gridColumnServiceDescription.FieldName = "Description";
            this.gridColumnServiceDescription.Name = "gridColumnServiceDescription";
            this.gridColumnServiceDescription.Visible = true;
            this.gridColumnServiceDescription.VisibleIndex = 4;
            this.gridColumnServiceDescription.Width = 129;
            // 
            // gridColumnServiceComments
            // 
            this.gridColumnServiceComments.Caption = "Comments";
            this.gridColumnServiceComments.FieldName = "Comments";
            this.gridColumnServiceComments.Name = "gridColumnServiceComments";
            this.gridColumnServiceComments.Visible = true;
            this.gridColumnServiceComments.VisibleIndex = 5;
            this.gridColumnServiceComments.Width = 163;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.simpleButtonUseLogo);
            this.layoutControl2.Controls.Add(this.pictureEditLogo);
            this.layoutControl2.Controls.Add(this.buttonEditLogo);
            this.layoutControl2.Location = new System.Drawing.Point(16, 128);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(525, 334, 250, 350);
            this.layoutControl2.Root = this.Root;
            this.layoutControl2.Size = new System.Drawing.Size(699, 336);
            this.layoutControl2.TabIndex = 13;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // simpleButtonUseLogo
            // 
            this.simpleButtonUseLogo.Enabled = false;
            this.simpleButtonUseLogo.Location = new System.Drawing.Point(573, 68);
            this.simpleButtonUseLogo.Name = "simpleButtonUseLogo";
            this.simpleButtonUseLogo.Size = new System.Drawing.Size(99, 22);
            this.simpleButtonUseLogo.StyleController = this.layoutControl2;
            this.simpleButtonUseLogo.TabIndex = 7;
            this.simpleButtonUseLogo.Text = "Use Logo";
            this.simpleButtonUseLogo.Click += new System.EventHandler(this.simpleButtonUseLogo_Click);
            // 
            // pictureEditLogo
            // 
            this.pictureEditLogo.Location = new System.Drawing.Point(15, 117);
            this.pictureEditLogo.MenuManager = this.barManager;
            this.pictureEditLogo.Name = "pictureEditLogo";
            this.pictureEditLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEditLogo.Size = new System.Drawing.Size(669, 204);
            this.pictureEditLogo.StyleController = this.layoutControl2;
            this.pictureEditLogo.TabIndex = 6;
            // 
            // buttonEditLogo
            // 
            this.buttonEditLogo.Location = new System.Drawing.Point(27, 68);
            this.buttonEditLogo.MenuManager = this.barManager;
            this.buttonEditLogo.Name = "buttonEditLogo";
            this.buttonEditLogo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditLogo.Properties.ReadOnly = true;
            this.buttonEditLogo.Size = new System.Drawing.Size(526, 20);
            this.buttonEditLogo.StyleController = this.layoutControl2;
            this.buttonEditLogo.TabIndex = 4;
            this.buttonEditLogo.Click += new System.EventHandler(this.buttonEditLogo_Click);
            // 
            // Root
            // 
            this.Root.CustomizationFormText = "Root";
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.tabbedControlGroupReportInfo});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(699, 336);
            this.Root.Text = "Root";
            this.Root.TextVisible = false;
            // 
            // tabbedControlGroupReportInfo
            // 
            this.tabbedControlGroupReportInfo.CustomizationFormText = "tabbedControlGroupReportInfo";
            this.tabbedControlGroupReportInfo.Location = new System.Drawing.Point(0, 0);
            this.tabbedControlGroupReportInfo.Name = "tabbedControlGroupReportInfo";
            this.tabbedControlGroupReportInfo.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.tabbedControlGroupReportInfo.SelectedTabPage = this.layoutControlGroupLogoSettings;
            this.tabbedControlGroupReportInfo.SelectedTabPageIndex = 0;
            this.tabbedControlGroupReportInfo.Size = new System.Drawing.Size(679, 316);
            this.tabbedControlGroupReportInfo.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupLogoSettings,
            this.layoutControlGroupClinicInfo});
            this.tabbedControlGroupReportInfo.Text = "tabbedControlGroupReportInfo";
            this.tabbedControlGroupReportInfo.SelectedPageChanging += new DevExpress.XtraLayout.LayoutTabPageChangingEventHandler(this.tabbedControlGroupReportInfo_SelectedPageChanging);
            // 
            // layoutControlGroupLogoSettings
            // 
            this.layoutControlGroupLogoSettings.CustomizationFormText = "Logo";
            this.layoutControlGroupLogoSettings.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemLogoImage,
            this.emptySpaceItem6,
            this.layoutControlGroupLogo});
            this.layoutControlGroupLogoSettings.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupLogoSettings.Name = "layoutControlGroupLogoSettings";
            this.layoutControlGroupLogoSettings.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroupLogoSettings.Size = new System.Drawing.Size(673, 288);
            this.layoutControlGroupLogoSettings.Text = "Logo";
            // 
            // layoutControlItemLogoImage
            // 
            this.layoutControlItemLogoImage.Control = this.pictureEditLogo;
            this.layoutControlItemLogoImage.CustomizationFormText = "layoutControlItemLogoImage";
            this.layoutControlItemLogoImage.Location = new System.Drawing.Point(0, 80);
            this.layoutControlItemLogoImage.Name = "layoutControlItemLogoImage";
            this.layoutControlItemLogoImage.Size = new System.Drawing.Size(673, 208);
            this.layoutControlItemLogoImage.Text = "layoutControlItemLogoImage";
            this.layoutControlItemLogoImage.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemLogoImage.TextToControlDistance = 0;
            this.layoutControlItemLogoImage.TextVisible = false;
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.CustomizationFormText = "emptySpaceItem6";
            this.emptySpaceItem6.Location = new System.Drawing.Point(0, 69);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(673, 11);
            this.emptySpaceItem6.Text = "emptySpaceItem6";
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroupLogo
            // 
            this.layoutControlGroupLogo.CustomizationFormText = "\"\"";
            this.layoutControlGroupLogo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemLogo,
            this.emptySpaceItem5,
            this.layoutControlItem6});
            this.layoutControlGroupLogo.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupLogo.Name = "layoutControlGroupLogo";
            this.layoutControlGroupLogo.Size = new System.Drawing.Size(673, 69);
            this.layoutControlGroupLogo.Text = "Logo";
            // 
            // layoutControlItemLogo
            // 
            this.layoutControlItemLogo.Control = this.buttonEditLogo;
            this.layoutControlItemLogo.CustomizationFormText = "layoutControlItemLogo";
            this.layoutControlItemLogo.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemLogo.Name = "layoutControlItemLogo";
            this.layoutControlItemLogo.Size = new System.Drawing.Size(530, 26);
            this.layoutControlItemLogo.Text = "layoutControlItemLogo";
            this.layoutControlItemLogo.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemLogo.TextToControlDistance = 0;
            this.layoutControlItemLogo.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItem5.Location = new System.Drawing.Point(530, 0);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(16, 26);
            this.emptySpaceItem5.Text = "emptySpaceItem5";
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButtonUseLogo;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(546, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(103, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroupClinicInfo
            // 
            this.layoutControlGroupClinicInfo.CustomizationFormText = "Clinic Info";
            this.layoutControlGroupClinicInfo.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupClinicInfo.Name = "layoutControlGroupClinicInfo";
            this.layoutControlGroupClinicInfo.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroupClinicInfo.Size = new System.Drawing.Size(673, 288);
            this.layoutControlGroupClinicInfo.Text = "Clinic Info";
            this.layoutControlGroupClinicInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // pictureEditSerialPort
            // 
            this.pictureEditSerialPort.EditValue = ((object)(resources.GetObject("pictureEditSerialPort.EditValue")));
            this.pictureEditSerialPort.Location = new System.Drawing.Point(22, 37);
            this.pictureEditSerialPort.MenuManager = this.barManager;
            this.pictureEditSerialPort.Name = "pictureEditSerialPort";
            this.pictureEditSerialPort.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEditSerialPort.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEditSerialPort.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEditSerialPort.Properties.ReadOnly = true;
            this.pictureEditSerialPort.Properties.ShowMenu = false;
            this.pictureEditSerialPort.Size = new System.Drawing.Size(28, 41);
            this.pictureEditSerialPort.StyleController = this.layoutControl1;
            this.pictureEditSerialPort.TabIndex = 11;
            // 
            // labelControlConnectionInfo
            // 
            this.labelControlConnectionInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControlConnectionInfo.Location = new System.Drawing.Point(84, 37);
            this.labelControlConnectionInfo.Name = "labelControlConnectionInfo";
            this.labelControlConnectionInfo.Size = new System.Drawing.Size(251, 41);
            this.labelControlConnectionInfo.StyleController = this.layoutControl1;
            this.labelControlConnectionInfo.TabIndex = 9;
            this.labelControlConnectionInfo.Text = "Connected ( COM Port 1 )";
            // 
            // gaugeControlConnectionState
            // 
            this.gaugeControlConnectionState.AutoLayout = false;
            this.gaugeControlConnectionState.BackColor = System.Drawing.Color.Transparent;
            this.gaugeControlConnectionState.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gaugeControlConnectionState.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] {
            this.stateIndicatorGauge});
            this.gaugeControlConnectionState.Location = new System.Drawing.Point(619, 37);
            this.gaugeControlConnectionState.Name = "gaugeControlConnectionState";
            this.gaugeControlConnectionState.Size = new System.Drawing.Size(100, 42);
            this.gaugeControlConnectionState.TabIndex = 6;
            this.gaugeControlConnectionState.TabStop = false;
            // 
            // stateIndicatorGauge
            // 
            this.stateIndicatorGauge.Bounds = new System.Drawing.Rectangle(11, 4, 79, 34);
            this.stateIndicatorGauge.Indicators.AddRange(new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent[] {
            this.stateIndicatorComponentConnectionState});
            this.stateIndicatorGauge.Name = "stateIndicatorGauge";
            // 
            // stateIndicatorComponentConnectionState
            // 
            this.stateIndicatorComponentConnectionState.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(124F, 124F);
            this.stateIndicatorComponentConnectionState.Name = "stateIndicatorComponentConnectionStatus";
            this.stateIndicatorComponentConnectionState.Size = new System.Drawing.SizeF(200F, 200F);
            this.stateIndicatorComponentConnectionState.StateIndex = 1;
            indicatorState1.Name = "State1";
            indicatorState1.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight1;
            indicatorState2.Name = "State2";
            indicatorState2.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight2;
            indicatorState3.Name = "State3";
            indicatorState3.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight3;
            indicatorState4.Name = "State4";
            indicatorState4.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight4;
            this.stateIndicatorComponentConnectionState.States.AddRange(new DevExpress.XtraGauges.Core.Model.IIndicatorState[] {
            indicatorState1,
            indicatorState2,
            indicatorState3,
            indicatorState4});
            // 
            // gridControlSettings
            // 
            this.gridControlSettings.Location = new System.Drawing.Point(16, 128);
            this.gridControlSettings.MainView = this.gridViewSettings;
            this.gridControlSettings.MenuManager = this.barManager;
            this.gridControlSettings.Name = "gridControlSettings";
            this.gridControlSettings.Size = new System.Drawing.Size(699, 336);
            this.gridControlSettings.TabIndex = 7;
            this.gridControlSettings.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSettings});
            // 
            // gridViewSettings
            // 
            this.gridViewSettings.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewSettings.Appearance.GroupRow.Options.UseFont = true;
            this.gridViewSettings.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName,
            this.gridColumnValue,
            this.gridColumnDescription,
            this.gridColumnGroup});
            this.gridViewSettings.GridControl = this.gridControlSettings;
            this.gridViewSettings.GroupCount = 1;
            this.gridViewSettings.GroupFormat = "[#image]{1}";
            this.gridViewSettings.Name = "gridViewSettings";
            this.gridViewSettings.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridViewSettings.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridViewSettings.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.gridViewSettings.OptionsCustomization.AllowFilter = false;
            this.gridViewSettings.OptionsCustomization.AllowGroup = false;
            this.gridViewSettings.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewSettings.OptionsMenu.EnableColumnMenu = false;
            this.gridViewSettings.OptionsNavigation.UseTabKey = false;
            this.gridViewSettings.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewSettings.OptionsView.ShowAutoFilterRow = true;
            this.gridViewSettings.OptionsView.ShowDetailButtons = false;
            this.gridViewSettings.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewSettings.OptionsView.ShowGroupPanel = false;
            this.gridViewSettings.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            this.gridViewSettings.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnGroup, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridViewSettings.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridViewSettings_ShowingEditor);
            this.gridViewSettings.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewSettingsCellValueChanging);
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "Name";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.OptionsColumn.AllowEdit = false;
            this.gridColumnName.OptionsColumn.AllowMove = false;
            this.gridColumnName.OptionsColumn.AllowShowHide = false;
            this.gridColumnName.OptionsColumn.ReadOnly = true;
            this.gridColumnName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 0;
            // 
            // gridColumnValue
            // 
            this.gridColumnValue.Caption = "Value";
            this.gridColumnValue.CustomizationCaption = "Value";
            this.gridColumnValue.FieldName = "UnboundValue";
            this.gridColumnValue.Name = "gridColumnValue";
            this.gridColumnValue.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumnValue.OptionsColumn.AllowMove = false;
            this.gridColumnValue.OptionsColumn.AllowShowHide = false;
            this.gridColumnValue.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumnValue.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gridColumnValue.Visible = true;
            this.gridColumnValue.VisibleIndex = 1;
            // 
            // gridColumnDescription
            // 
            this.gridColumnDescription.Caption = "Description";
            this.gridColumnDescription.FieldName = "Description";
            this.gridColumnDescription.Name = "gridColumnDescription";
            this.gridColumnDescription.OptionsColumn.AllowEdit = false;
            this.gridColumnDescription.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumnDescription.OptionsColumn.AllowMove = false;
            this.gridColumnDescription.OptionsColumn.AllowShowHide = false;
            this.gridColumnDescription.OptionsColumn.ReadOnly = true;
            this.gridColumnDescription.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumnDescription.Visible = true;
            this.gridColumnDescription.VisibleIndex = 2;
            // 
            // gridColumnGroup
            // 
            this.gridColumnGroup.Caption = "Group";
            this.gridColumnGroup.FieldName = "SettingGroupLookup.Value";
            this.gridColumnGroup.Name = "gridColumnGroup";
            this.gridColumnGroup.OptionsColumn.AllowEdit = false;
            this.gridColumnGroup.OptionsColumn.AllowFocus = false;
            this.gridColumnGroup.OptionsColumn.AllowMove = false;
            this.gridColumnGroup.OptionsColumn.AllowShowHide = false;
            this.gridColumnGroup.OptionsColumn.ReadOnly = true;
            this.gridColumnGroup.OptionsColumn.ShowCaption = false;
            this.gridColumnGroup.OptionsColumn.ShowInCustomizationForm = false;
            this.gridColumnGroup.OptionsColumn.ShowInExpressionEditor = false;
            this.gridColumnGroup.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumnGroup.Visible = true;
            this.gridColumnGroup.VisibleIndex = 3;
            // 
            // simpleButtonRefreshConnection
            // 
            this.simpleButtonRefreshConnection.Image = global::Vital.UI.Properties.Resources.Patient_Refresh;
            this.simpleButtonRefreshConnection.Location = new System.Drawing.Point(54, 47);
            this.simpleButtonRefreshConnection.Name = "simpleButtonRefreshConnection";
            this.simpleButtonRefreshConnection.Size = new System.Drawing.Size(26, 22);
            this.simpleButtonRefreshConnection.StyleController = this.layoutControl1;
            toolTipTitleItem1.Text = "Retry.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            this.simpleButtonRefreshConnection.SuperTip = superToolTip1;
            this.simpleButtonRefreshConnection.TabIndex = 12;
            this.simpleButtonRefreshConnection.Click += new System.EventHandler(this.simpleButtonRefreshConnection_Click);
            // 
            // progressPanelRefreshConnection
            // 
            this.progressPanelRefreshConnection.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanelRefreshConnection.Appearance.Options.UseBackColor = true;
            this.progressPanelRefreshConnection.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.12F);
            this.progressPanelRefreshConnection.AppearanceCaption.Options.UseFont = true;
            this.progressPanelRefreshConnection.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.progressPanelRefreshConnection.AppearanceDescription.Options.UseFont = true;
            this.progressPanelRefreshConnection.Caption = "Searching";
            this.progressPanelRefreshConnection.Description = "COM Port {0} ...";
            this.progressPanelRefreshConnection.Location = new System.Drawing.Point(339, 37);
            this.progressPanelRefreshConnection.Name = "progressPanelRefreshConnection";
            this.progressPanelRefreshConnection.Size = new System.Drawing.Size(116, 41);
            this.progressPanelRefreshConnection.StyleController = this.layoutControl1;
            this.progressPanelRefreshConnection.TabIndex = 10;
            this.progressPanelRefreshConnection.Text = "progressPanel1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.tabbedControlGroupReportSettings});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup1.Size = new System.Drawing.Size(731, 480);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CaptionImage = global::Vital.UI.Properties.Resources.Test_Info;
            this.layoutControlGroup2.CustomizationFormText = "Connection State";
            this.layoutControlGroup2.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroup2.ExpandButtonVisible = true;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemSearching,
            this.layoutControlItemConnectionInfo,
            this.layoutControlItem3,
            this.simpleSeparator1,
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlGroupRefresh});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup2.Size = new System.Drawing.Size(727, 87);
            this.layoutControlGroup2.Text = "Connection State";
            // 
            // layoutControlItemSearching
            // 
            this.layoutControlItemSearching.Control = this.progressPanelRefreshConnection;
            this.layoutControlItemSearching.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItemSearching.Location = new System.Drawing.Point(327, 0);
            this.layoutControlItemSearching.MaxSize = new System.Drawing.Size(0, 45);
            this.layoutControlItemSearching.MinSize = new System.Drawing.Size(54, 45);
            this.layoutControlItemSearching.Name = "layoutControlItemSearching";
            this.layoutControlItemSearching.Size = new System.Drawing.Size(120, 46);
            this.layoutControlItemSearching.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemSearching.Text = "layoutControlItemSearching";
            this.layoutControlItemSearching.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSearching.TextToControlDistance = 0;
            this.layoutControlItemSearching.TextVisible = false;
            // 
            // layoutControlItemConnectionInfo
            // 
            this.layoutControlItemConnectionInfo.Control = this.labelControlConnectionInfo;
            this.layoutControlItemConnectionInfo.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.layoutControlItemConnectionInfo.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItemConnectionInfo.Location = new System.Drawing.Point(72, 0);
            this.layoutControlItemConnectionInfo.MaxSize = new System.Drawing.Size(0, 45);
            this.layoutControlItemConnectionInfo.MinSize = new System.Drawing.Size(151, 45);
            this.layoutControlItemConnectionInfo.Name = "layoutControlItemConnectionInfo";
            this.layoutControlItemConnectionInfo.Size = new System.Drawing.Size(255, 46);
            this.layoutControlItemConnectionInfo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemConnectionInfo.Text = "layoutControlItemConnectionInfo";
            this.layoutControlItemConnectionInfo.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemConnectionInfo.TextToControlDistance = 0;
            this.layoutControlItemConnectionInfo.TextVisible = false;
            this.layoutControlItemConnectionInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gaugeControlConnectionState;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(607, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(104, 46);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(104, 46);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(104, 46);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.CustomizationFormText = "simpleSeparator1";
            this.simpleSeparator1.Location = new System.Drawing.Point(605, 0);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(2, 46);
            this.simpleSeparator1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleSeparator1.Text = "simpleSeparator1";
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(447, 0);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(0, 46);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 46);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(158, 46);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.pictureEditSerialPort;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(10, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(32, 45);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(32, 45);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(32, 46);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(10, 46);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 46);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 46);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroupRefresh
            // 
            this.layoutControlGroupRefresh.CustomizationFormText = "layoutControlGroupRefresh";
            this.layoutControlGroupRefresh.GroupBordersVisible = false;
            this.layoutControlGroupRefresh.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem3,
            this.layoutControlItem4,
            this.emptySpaceItem4});
            this.layoutControlGroupRefresh.Location = new System.Drawing.Point(42, 0);
            this.layoutControlGroupRefresh.Name = "layoutControlGroupRefresh";
            this.layoutControlGroupRefresh.Size = new System.Drawing.Size(30, 46);
            this.layoutControlGroupRefresh.Text = "layoutControlGroupRefresh";
            this.layoutControlGroupRefresh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 36);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(30, 10);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(30, 10);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(30, 10);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButtonRefreshConnection;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 10);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(30, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem4.MaxSize = new System.Drawing.Size(30, 10);
            this.emptySpaceItem4.MinSize = new System.Drawing.Size(30, 10);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(30, 10);
            this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // tabbedControlGroupReportSettings
            // 
            this.tabbedControlGroupReportSettings.CustomizationFormText = "tabbedControlGroupReportSettings";
            this.tabbedControlGroupReportSettings.Location = new System.Drawing.Point(0, 87);
            this.tabbedControlGroupReportSettings.Name = "tabbedControlGroupReportSettings";
            this.tabbedControlGroupReportSettings.SelectedTabPage = this.layoutControlGroup3;
            this.tabbedControlGroupReportSettings.SelectedTabPageIndex = 0;
            this.tabbedControlGroupReportSettings.Size = new System.Drawing.Size(727, 389);
            this.tabbedControlGroupReportSettings.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlGroupReportSettings,
            this.layoutControlGroupServices,
            this.layoutControlGroupHwProfiles});
            this.tabbedControlGroupReportSettings.Text = "Report Settings";
            // 
            // layoutControlGroupHwProfiles
            // 
            this.layoutControlGroupHwProfiles.CaptionImage = global::Vital.UI.Properties.Resources.port16x16;
            this.layoutControlGroupHwProfiles.CustomizationFormText = "Hardware Profiles";
            this.layoutControlGroupHwProfiles.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7});
            this.layoutControlGroupHwProfiles.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupHwProfiles.Name = "layoutControlGroupHwProfiles";
            this.layoutControlGroupHwProfiles.Size = new System.Drawing.Size(703, 340);
            this.layoutControlGroupHwProfiles.Text = "Hardware Profiles";
            this.layoutControlGroupHwProfiles.Visibility = LayoutVisibility.Never;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.xtraUserControlHwProfiles;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(703, 340);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CaptionImage = global::Vital.UI.Properties.Resources.SettingsSmall;
            this.layoutControlGroup3.CustomizationFormText = "Settings";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup3.Size = new System.Drawing.Size(703, 340);
            this.layoutControlGroup3.Text = "Settings";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlSettings;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(703, 340);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroupReportSettings
            // 
            this.layoutControlGroupReportSettings.CaptionImage = global::Vital.UI.Properties.Resources.report_caption_2;
            this.layoutControlGroupReportSettings.CustomizationFormText = "Report Settings";
            this.layoutControlGroupReportSettings.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroupReportSettings.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupReportSettings.Name = "layoutControlGroupReportSettings";
            this.layoutControlGroupReportSettings.Size = new System.Drawing.Size(703, 340);
            this.layoutControlGroupReportSettings.Text = "Report Settings";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.layoutControl2;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(703, 340);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroupServices
            // 
            this.layoutControlGroupServices.CaptionImage = global::Vital.UI.Properties.Resources.SpotCheck;
            this.layoutControlGroupServices.CustomizationFormText = "Services";
            this.layoutControlGroupServices.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemServices});
            this.layoutControlGroupServices.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupServices.Name = "layoutControlGroupServices";
            this.layoutControlGroupServices.Size = new System.Drawing.Size(703, 340);
            this.layoutControlGroupServices.Text = "Services";
            // 
            // layoutControlItemServices
            // 
            this.layoutControlItemServices.Control = this.gridControlServices;
            this.layoutControlItemServices.CustomizationFormText = "layoutControlItemServices";
            this.layoutControlItemServices.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemServices.Name = "layoutControlItemServices";
            this.layoutControlItemServices.Size = new System.Drawing.Size(703, 340);
            this.layoutControlItemServices.Text = "layoutControlItemServices";
            this.layoutControlItemServices.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemServices.TextToControlDistance = 0;
            this.layoutControlItemServices.TextVisible = false;
            // 
            // dxErrorProviderServices
            // 
            this.dxErrorProviderServices.ContainerControl = this;
            this.dxErrorProviderServices.DataMember = "";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 531);
            this.Controls.Add(this.panelControl1);
            this.IsLoaded = true;
            this.Name = "frmSettings";
            this.ShowDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ShowEditHint = true;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSettings_FormClosing);
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.Shown += new System.EventHandler(this.frmSettings_Shown);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProviderMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlServices)).EndInit();
            this.contextMenuStripServices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewServices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditServiceType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditServicePrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditServiceIsDefault)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditLogo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditLogo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroupReportInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupLogoSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogoImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupClinicInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditSerialPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIndicatorGauge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIndicatorComponentConnectionState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSearching)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemConnectionInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroupReportSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupHwProfiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupReportSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupServices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemServices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProviderServices)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridControlSettings;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSettings;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnValue;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDescription;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnGroup;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGauges.Win.GaugeControl gaugeControlConnectionState;
        private DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorGauge stateIndicatorGauge;
        private DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent stateIndicatorComponentConnectionState;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanelRefreshConnection;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSearching;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraEditors.LabelControl labelControlConnectionInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemConnectionInfo;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.PictureEdit pictureEditSerialPort;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRefreshConnection;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRefresh;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroupReportSettings;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupReportSettings;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraEditors.PictureEdit pictureEditLogo;
        private DevExpress.XtraEditors.ButtonEdit buttonEditLogo;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton simpleButtonUseLogo;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroupReportInfo;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupClinicInfo;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupLogoSettings;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLogoImage;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupLogo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLogo;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupServices;
        private DevExpress.XtraGrid.GridControl gridControlServices;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewServices;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnServiceName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnServiceType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditServiceType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnServicePrice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnServiceIsDefault;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnServiceDescription;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnServiceComments;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemServices;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditServicePrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditServiceIsDefault;        
        private System.Windows.Forms.ContextMenuStrip contextMenuStripServices;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteService;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProviderServices;
        private User_Controls.Modules.XtraUserControlHwProfiles xtraUserControlHwProfiles;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupHwProfiles;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;        



    }
}
