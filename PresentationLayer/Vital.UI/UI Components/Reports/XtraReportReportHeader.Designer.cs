namespace Vital.UI.UI_Components.Reports
{
    partial class XtraReportReportHeader
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraReportReportHeader));
            this.xtraReport1 = new DevExpress.XtraReports.UI.XtraReport();
            this.xtraReport2 = new DevExpress.XtraReports.UI.XtraReport();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrRichTextClinicInfo = new DevExpress.XtraReports.UI.XRRichText();
            this.xrPictureBoxLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.xtraReport1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraReport2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichTextClinicInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // xtraReport1
            // 
            this.xtraReport1.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.xtraReport1.Name = "xtraReport1";
            this.xtraReport1.PageHeight = 1100;
            this.xtraReport1.PageWidth = 850;
            this.xtraReport1.Version = "12.2";
            // 
            // xtraReport2
            // 
            this.xtraReport2.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.xtraReport2.Name = "xtraReport2";
            this.xtraReport2.PageHeight = 1100;
            this.xtraReport2.PageWidth = 850;
            this.xtraReport2.Version = "12.2";
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 0F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // detailBand1
            // 
            this.detailBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichTextClinicInfo,
            this.xrPictureBoxLogo});
            this.detailBand1.HeightF = 127.9669F;
            this.detailBand1.Name = "detailBand1";
            this.detailBand1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.detailBand1_BeforePrint);
            // 
            // xrRichTextClinicInfo
            // 
            this.xrRichTextClinicInfo.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrRichTextClinicInfo.LocationFloat = new DevExpress.Utils.PointFloat(550F, 0F);
            this.xrRichTextClinicInfo.Name = "xrRichTextClinicInfo";
            this.xrRichTextClinicInfo.SerializableRtfString = resources.GetString("xrRichTextClinicInfo.SerializableRtfString");
            this.xrRichTextClinicInfo.SizeF = new System.Drawing.SizeF(300F, 127.9669F);
            this.xrRichTextClinicInfo.Visible = false;
            // 
            // xrPictureBoxLogo
            // 
            this.xrPictureBoxLogo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBoxLogo.Name = "xrPictureBoxLogo";
            this.xrPictureBoxLogo.SizeF = new System.Drawing.SizeF(252.0833F, 127.9669F);
            this.xrPictureBoxLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrPictureBoxLogo.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPictureBoxLogo_BeforePrint);
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 0F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // XtraReportReportHeader
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.Version = "12.2";
            ((System.ComponentModel.ISupportInitialize)(this.xtraReport1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraReport2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichTextClinicInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XtraReport xtraReport1;
        private DevExpress.XtraReports.UI.XtraReport xtraReport2;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.DetailBand detailBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        public DevExpress.XtraReports.UI.XRPictureBox xrPictureBoxLogo;
        public DevExpress.XtraReports.UI.XRRichText xrRichTextClinicInfo;

    }
}
