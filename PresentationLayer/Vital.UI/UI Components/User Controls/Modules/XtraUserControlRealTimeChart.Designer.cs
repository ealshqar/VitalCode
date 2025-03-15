using DevExpress.Utils;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    partial class XtraUserControlRealTimeChart
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
            DevExpress.XtraCharts.SwiftPlotDiagram swiftPlotDiagram2 = new DevExpress.XtraCharts.SwiftPlotDiagram();
            DevExpress.XtraCharts.ConstantLine constantLine2 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView3 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView4 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButtonPlayPauseTimeline = new DevExpress.XtraEditors.SimpleButton();
            this.chartControlReadings = new DevExpress.XtraCharts.ChartControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemRealTimeChart = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.timerChart = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlReadings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRealTimeChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.simpleButtonPlayPauseTimeline);
            this.layoutControl1.Controls.Add(this.chartControlReadings);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(624, 108);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // simpleButtonPlayPauseTimeline
            // 
            this.simpleButtonPlayPauseTimeline.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.simpleButtonPlayPauseTimeline.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.simpleButtonPlayPauseTimeline.Appearance.Options.UseBackColor = true;
            this.simpleButtonPlayPauseTimeline.Image = global::Vital.UI.Properties.Resources.play_timeline;
            this.simpleButtonPlayPauseTimeline.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonPlayPauseTimeline.Location = new System.Drawing.Point(2, 2);
            this.simpleButtonPlayPauseTimeline.Name = "simpleButtonPlayPauseTimeline";
            this.simpleButtonPlayPauseTimeline.Size = new System.Drawing.Size(58, 104);
            this.simpleButtonPlayPauseTimeline.StyleController = this.layoutControl1;
            this.simpleButtonPlayPauseTimeline.TabIndex = 20;
            this.simpleButtonPlayPauseTimeline.Click += new System.EventHandler(this.simpleButtonPlayPauseTimeline_Click);
            // 
            // chartControlReadings
            // 
            this.chartControlReadings.CacheToMemory = true;
            this.chartControlReadings.CrosshairOptions.ArgumentLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(57)))), ((int)(((byte)(205)))));
            this.chartControlReadings.CrosshairOptions.ShowCrosshairLabels = false;
            this.chartControlReadings.CrosshairOptions.ValueLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(57)))), ((int)(((byte)(205)))));
            swiftPlotDiagram2.AxisX.DateTimeGridAlignment = DevExpress.XtraCharts.DateTimeMeasurementUnit.Millisecond;
            swiftPlotDiagram2.AxisX.DateTimeMeasureUnit = DevExpress.XtraCharts.DateTimeMeasurementUnit.Millisecond;
            swiftPlotDiagram2.AxisX.DateTimeOptions.Format = DevExpress.XtraCharts.DateTimeFormat.ShortTime;
            swiftPlotDiagram2.AxisX.Interlaced = true;
            swiftPlotDiagram2.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
            swiftPlotDiagram2.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
            swiftPlotDiagram2.AxisX.NumericOptions.Format = DevExpress.XtraCharts.NumericFormat.General;
            swiftPlotDiagram2.AxisX.Range.ScrollingRange.SideMarginsEnabled = true;
            swiftPlotDiagram2.AxisX.Range.SideMarginsEnabled = true;
            swiftPlotDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            constantLine2.AxisValueSerializable = "100";
            constantLine2.Name = "100";
            swiftPlotDiagram2.AxisY.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine2});
            swiftPlotDiagram2.AxisY.GridSpacing = 50D;
            swiftPlotDiagram2.AxisY.GridSpacingAuto = false;
            swiftPlotDiagram2.AxisY.NumericOptions.Format = DevExpress.XtraCharts.NumericFormat.General;
            swiftPlotDiagram2.AxisY.Range.Auto = false;
            swiftPlotDiagram2.AxisY.Range.MaxValueSerializable = "100";
            swiftPlotDiagram2.AxisY.Range.MinValueSerializable = "0";
            swiftPlotDiagram2.AxisY.Range.ScrollingRange.SideMarginsEnabled = true;
            swiftPlotDiagram2.AxisY.Range.SideMarginsEnabled = true;
            swiftPlotDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram2.Margins.Left = 40;
            swiftPlotDiagram2.Margins.Right = 30;
            this.chartControlReadings.Diagram = swiftPlotDiagram2;
            this.chartControlReadings.Legend.Visible = false;
            this.chartControlReadings.Location = new System.Drawing.Point(64, 2);
            this.chartControlReadings.Name = "chartControlReadings";
            this.chartControlReadings.RefreshDataOnRepaint = false;
            series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series2.LegendText = "Swift Plot Series";
            series2.Name = "Series 1";
            swiftPlotSeriesView3.Antialiasing = true;
            swiftPlotSeriesView3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            swiftPlotSeriesView3.LineStyle.Thickness = 2;
            series2.View = swiftPlotSeriesView3;
            this.chartControlReadings.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.chartControlReadings.SeriesTemplate.View = swiftPlotSeriesView4;
            this.chartControlReadings.Size = new System.Drawing.Size(558, 104);
            this.chartControlReadings.TabIndex = 19;
            this.chartControlReadings.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.False;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemRealTimeChart,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(624, 108);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemRealTimeChart
            // 
            this.layoutControlItemRealTimeChart.Control = this.chartControlReadings;
            this.layoutControlItemRealTimeChart.CustomizationFormText = "layoutControlItemRealTimeChart";
            this.layoutControlItemRealTimeChart.Location = new System.Drawing.Point(62, 0);
            this.layoutControlItemRealTimeChart.Name = "layoutControlItemRealTimeChart";
            this.layoutControlItemRealTimeChart.Size = new System.Drawing.Size(562, 108);
            this.layoutControlItemRealTimeChart.Text = "layoutControlItemRealTimeChart";
            this.layoutControlItemRealTimeChart.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemRealTimeChart.TextToControlDistance = 0;
            this.layoutControlItemRealTimeChart.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButtonPlayPauseTimeline;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(62, 58);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(62, 108);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // timerChart
            // 
            this.timerChart.Enabled = true;
            this.timerChart.Interval = 40;
            this.timerChart.Tick += new System.EventHandler(this.timerChart_Tick);
            // 
            // XtraUserControlRealTimeChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraUserControlRealTimeChart";
            this.Size = new System.Drawing.Size(624, 108);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlReadings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRealTimeChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraCharts.ChartControl chartControlReadings;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRealTimeChart;
        private System.Windows.Forms.Timer timerChart;
        private DevExpress.XtraEditors.SimpleButton simpleButtonPlayPauseTimeline;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
