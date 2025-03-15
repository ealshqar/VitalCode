namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormMeterCounterDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraFormMeterCounterDialog));
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.barManager = new DevExpress.XtraBars.BarManager();
            this.barCRUD = new DevExpress.XtraBars.Bar();
            this.barButtonItemDone = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barTestStatus = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.layoutControlCounter = new DevExpress.XtraLayout.LayoutControl();
            this.labelControlTip = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonDescrease = new DevExpress.XtraEditors.SimpleButton();
            this.xtraUserControlReadingGaugeMeterCounter = new Vital.UI.UI_Components.User_Controls.Modules.XtraUserControlReadingGauge();
            this.simpleButtonIncrease = new DevExpress.XtraEditors.SimpleButton();
            this.gaugeControlMeterCounter = new DevExpress.XtraGauges.Win.GaugeControl();
            this.digitalGaugeMeterCounter = new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalGauge();
            this.digitalBackgroundLayerComponent1 = new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent();
            this.layoutControlGroupCounter = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemReadingGauge = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItemQuestion = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItemMeterCounter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTip = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.digitalBackgroundLayerComponentMeterCounter = new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlCounter)).BeginInit();
            this.layoutControlCounter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.digitalGaugeMeterCounter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalBackgroundLayerComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupCounter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReadingGauge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemQuestion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMeterCounter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalBackgroundLayerComponentMeterCounter)).BeginInit();
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
            this.barManager.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barManager_ItemClick);
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
            this.barButtonItemDone.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemDone.Glyph")));
            this.barButtonItemDone.Id = 3;
            this.barButtonItemDone.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.barButtonItemDone.Name = "barButtonItemDone";
            toolTipTitleItem2.Text = "Save (Ctrl + S)";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "This button will save changes done on the current test.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.barButtonItemDone.SuperTip = superToolTip2;
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
            this.barTestStatus.Visible = false;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(573, 39);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 411);
            this.barDockControlBottom.Size = new System.Drawing.Size(573, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 39);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 372);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(573, 39);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 372);
            // 
            // layoutControlMajorIssuesNumber
            // 
            this.layoutControlCounter.AllowCustomizationMenu = false;
            this.layoutControlCounter.Controls.Add(this.labelControlTip);
            this.layoutControlCounter.Controls.Add(this.simpleButtonDescrease);
            this.layoutControlCounter.Controls.Add(this.xtraUserControlReadingGaugeMeterCounter);
            this.layoutControlCounter.Controls.Add(this.simpleButtonIncrease);
            this.layoutControlCounter.Controls.Add(this.gaugeControlMeterCounter);
            this.layoutControlCounter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlCounter.Location = new System.Drawing.Point(0, 39);
            this.layoutControlCounter.Name = "layoutControlMajorIssuesNumber";
            this.layoutControlCounter.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(732, 144, 250, 350);
            this.layoutControlCounter.Root = this.layoutControlGroupCounter;
            this.layoutControlCounter.Size = new System.Drawing.Size(573, 372);
            this.layoutControlCounter.TabIndex = 0;
            this.layoutControlCounter.Text = "Major Issues Number";
            // 
            // labelControlTip
            // 
            this.labelControlTip.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelControlTip.Location = new System.Drawing.Point(12, 12);
            this.labelControlTip.Name = "labelControlTip";
            this.labelControlTip.Size = new System.Drawing.Size(373, 19);
            this.labelControlTip.StyleController = this.layoutControlCounter;
            this.labelControlTip.TabIndex = 5;
            this.labelControlTip.Text = "Press [Enter] to continue after finding {XXX}.";
            // 
            // simpleButtonDescrease
            // 
            this.simpleButtonDescrease.AllowFocus = false;
            this.simpleButtonDescrease.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonDescrease.Image")));
            this.simpleButtonDescrease.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonDescrease.Location = new System.Drawing.Point(12, 330);
            this.simpleButtonDescrease.Name = "simpleButtonDescrease";
            this.simpleButtonDescrease.Size = new System.Drawing.Size(177, 30);
            this.simpleButtonDescrease.StyleController = this.layoutControlCounter;
            this.simpleButtonDescrease.TabIndex = 0;
            this.simpleButtonDescrease.TabStop = false;
            this.simpleButtonDescrease.Click += new System.EventHandler(this.simpleButtonDescrease_Click);
            // 
            // xtraUserControlReadingGaugeIssuesCount
            // 
            this.xtraUserControlReadingGaugeMeterCounter.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraUserControlReadingGaugeMeterCounter.Appearance.Options.UseBackColor = true;
            this.xtraUserControlReadingGaugeMeterCounter.Location = new System.Drawing.Point(390, 35);
            this.xtraUserControlReadingGaugeMeterCounter.Name = "xtraUserControlReadingGaugeIssuesCount";
            this.xtraUserControlReadingGaugeMeterCounter.ShowLocationGauge = false;
            this.xtraUserControlReadingGaugeMeterCounter.ShowYesNoLabel = true;
            this.xtraUserControlReadingGaugeMeterCounter.Size = new System.Drawing.Size(171, 325);
            this.xtraUserControlReadingGaugeMeterCounter.TabIndex = 3;
            this.xtraUserControlReadingGaugeMeterCounter.TabStop = false;
            // 
            // simpleButtonIncrease
            // 
            this.simpleButtonIncrease.AllowFocus = false;
            this.simpleButtonIncrease.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonIncrease.Image")));
            this.simpleButtonIncrease.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonIncrease.Location = new System.Drawing.Point(193, 330);
            this.simpleButtonIncrease.Name = "simpleButtonIncrease";
            this.simpleButtonIncrease.Size = new System.Drawing.Size(193, 30);
            this.simpleButtonIncrease.StyleController = this.layoutControlCounter;
            this.simpleButtonIncrease.TabIndex = 0;
            this.simpleButtonIncrease.TabStop = false;
            this.simpleButtonIncrease.Click += new System.EventHandler(this.simpleButtonIncrease_Click);
            // 
            // gaugeControlIssuesCount
            // 
            this.gaugeControlMeterCounter.AutoLayout = false;
            this.gaugeControlMeterCounter.BackColor = System.Drawing.Color.Transparent;
            this.gaugeControlMeterCounter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gaugeControlMeterCounter.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] {
            this.digitalGaugeMeterCounter});
            this.gaugeControlMeterCounter.Location = new System.Drawing.Point(12, 87);
            this.gaugeControlMeterCounter.Name = "gaugeControlIssuesCount";
            this.gaugeControlMeterCounter.Size = new System.Drawing.Size(374, 239);
            this.gaugeControlMeterCounter.TabIndex = 0;
            this.gaugeControlMeterCounter.TabStop = false;
            // 
            // digitalGaugeIssuesCount
            // 
            this.digitalGaugeMeterCounter.AppearanceOff.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#C8C8C8");
            this.digitalGaugeMeterCounter.AppearanceOn.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
            this.digitalGaugeMeterCounter.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent[] {
            this.digitalBackgroundLayerComponent1});
            this.digitalGaugeMeterCounter.Bounds = new System.Drawing.Rectangle(19, 6, 331, 212);
            this.digitalGaugeMeterCounter.DigitCount = 3;
            this.digitalGaugeMeterCounter.Name = "digitalGaugeIssuesCount";
            // 
            // digitalBackgroundLayerComponent1
            // 
            this.digitalBackgroundLayerComponent1.BottomRight = new DevExpress.XtraGauges.Core.Base.PointF2D(163.8875F, 99.9625F);
            this.digitalBackgroundLayerComponent1.Name = "digitalBackgroundLayerComponent13";
            this.digitalBackgroundLayerComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.DigitalBackgroundShapeSetType.Style16;
            this.digitalBackgroundLayerComponent1.TopLeft = new DevExpress.XtraGauges.Core.Base.PointF2D(20F, 0F);
            this.digitalBackgroundLayerComponent1.ZOrder = 1000;
            // 
            // layoutControlGroupMajorIssuesNumber
            // 
            this.layoutControlGroupCounter.CustomizationFormText = "Major Issues Number";
            this.layoutControlGroupCounter.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupCounter.GroupBordersVisible = false;
            this.layoutControlGroupCounter.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemReadingGauge,
            this.simpleLabelItemQuestion,
            this.layoutControlItemMeterCounter,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItemTip,
            this.emptySpaceItem1});
            this.layoutControlGroupCounter.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupCounter.Name = "Root";
            this.layoutControlGroupCounter.Size = new System.Drawing.Size(573, 372);
            this.layoutControlGroupCounter.Text = "Major Issues Number";
            this.layoutControlGroupCounter.TextVisible = false;
            // 
            // layoutControlItemReadingGauge
            // 
            this.layoutControlItemReadingGauge.Control = this.xtraUserControlReadingGaugeMeterCounter;
            this.layoutControlItemReadingGauge.CustomizationFormText = "Reading Gauge";
            this.layoutControlItemReadingGauge.Location = new System.Drawing.Point(378, 23);
            this.layoutControlItemReadingGauge.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItemReadingGauge.Name = "layoutControlItemReadingGauge";
            this.layoutControlItemReadingGauge.Size = new System.Drawing.Size(175, 329);
            this.layoutControlItemReadingGauge.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemReadingGauge.Text = "Reading Gauge";
            this.layoutControlItemReadingGauge.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemReadingGauge.TextToControlDistance = 0;
            this.layoutControlItemReadingGauge.TextVisible = false;
            // 
            // simpleLabelItemQuestion
            // 
            this.simpleLabelItemQuestion.AllowHotTrack = false;
            this.simpleLabelItemQuestion.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleLabelItemQuestion.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItemQuestion.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItemQuestion.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItemQuestion.CustomizationFormText = "Is there only 1 issue to be addressed?";
            this.simpleLabelItemQuestion.Location = new System.Drawing.Point(0, 48);
            this.simpleLabelItemQuestion.Name = "simpleLabelItemQuestion";
            this.simpleLabelItemQuestion.Size = new System.Drawing.Size(378, 27);
            this.simpleLabelItemQuestion.Text = " { QUSTION HERE } ?";
            this.simpleLabelItemQuestion.TextSize = new System.Drawing.Size(184, 23);
            // 
            // layoutControlItemNumberOfIssues
            // 
            this.layoutControlItemMeterCounter.Control = this.gaugeControlMeterCounter;
            this.layoutControlItemMeterCounter.CustomizationFormText = "layoutControlItemNumberOfIssues";
            this.layoutControlItemMeterCounter.Location = new System.Drawing.Point(0, 75);
            this.layoutControlItemMeterCounter.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItemMeterCounter.Name = "layoutControlItemNumberOfIssues";
            this.layoutControlItemMeterCounter.Size = new System.Drawing.Size(378, 243);
            this.layoutControlItemMeterCounter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemMeterCounter.Text = "layoutControlItemNumberOfIssues";
            this.layoutControlItemMeterCounter.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemMeterCounter.TextToControlDistance = 0;
            this.layoutControlItemMeterCounter.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButtonIncrease;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(181, 318);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(197, 34);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButtonDescrease;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 318);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(181, 34);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItemTip
            // 
            this.layoutControlItemTip.Control = this.labelControlTip;
            this.layoutControlItemTip.CustomizationFormText = "layoutControlItemTip";
            this.layoutControlItemTip.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemTip.Name = "layoutControlItemTip";
            this.layoutControlItemTip.Size = new System.Drawing.Size(553, 23);
            this.layoutControlItemTip.Text = "layoutControlItemTip";
            this.layoutControlItemTip.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemTip.TextToControlDistance = 0;
            this.layoutControlItemTip.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 23);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(378, 25);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // digitalBackgroundLayerComponentNumberOfIssues
            // 
            this.digitalBackgroundLayerComponentMeterCounter.BottomRight = new DevExpress.XtraGauges.Core.Base.PointF2D(158.825F, 106.075F);
            this.digitalBackgroundLayerComponentMeterCounter.Name = "digitalBackgroundLayerComponent13";
            this.digitalBackgroundLayerComponentMeterCounter.ShapeType = DevExpress.XtraGauges.Core.Model.DigitalBackgroundShapeSetType.Style16;
            this.digitalBackgroundLayerComponentMeterCounter.TopLeft = new DevExpress.XtraGauges.Core.Base.PointF2D(20F, 0F);
            this.digitalBackgroundLayerComponentMeterCounter.ZOrder = 1000;
            // 
            // XtraFormMeterCounterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 434);
            this.Controls.Add(this.layoutControlCounter);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(589, 468);
            this.Name = "XtraFormMeterCounterDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Meter Counter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XtraFormMeterCounterDialog_FormClosing);
            this.Load += new System.EventHandler(this.XtraFormMajorIssuesNumber_Load);
            this.Shown += new System.EventHandler(this.XtraFormMeterCounterDialog_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XtraFormMeterCounterDialog_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.XtraFormMeterCounterDialog_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlCounter)).EndInit();
            this.layoutControlCounter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.digitalGaugeMeterCounter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalBackgroundLayerComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupCounter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReadingGauge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemQuestion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMeterCounter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalBackgroundLayerComponentMeterCounter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraBars.BarManager barManager;
        public DevExpress.XtraBars.Bar barCRUD;
        protected DevExpress.XtraBars.BarButtonItem barButtonItemDone;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCancel;
        private DevExpress.XtraBars.Bar barTestStatus;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraLayout.LayoutControl layoutControlCounter;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupCounter;
        private DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent digitalBackgroundLayerComponentMeterCounter;
        private User_Controls.Modules.XtraUserControlReadingGauge xtraUserControlReadingGaugeMeterCounter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemReadingGauge;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemQuestion;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDescrease;
        private DevExpress.XtraEditors.SimpleButton simpleButtonIncrease;
        private DevExpress.XtraGauges.Win.GaugeControl gaugeControlMeterCounter;
        private DevExpress.XtraGauges.Win.Gauges.Digital.DigitalGauge digitalGaugeMeterCounter;
        private DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent digitalBackgroundLayerComponent1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMeterCounter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.LabelControl labelControlTip;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTip;
    }
}