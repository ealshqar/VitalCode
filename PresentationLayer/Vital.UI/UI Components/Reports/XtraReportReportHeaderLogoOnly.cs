using System;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace Vital.UI.UI_Components.Reports
{
    public partial class XtraReportReportHeaderLogoOnly : XtraReport
    {
        public XtraReportReportHeaderLogoOnly()
        {
            InitializeComponent();
        }
        public Image LogoImage
        {
            set { xrPictureBoxLogo.Image = value; }            
        }

        public double ConvertFromPixel(ReportUnit ru, double value)
        {
            double ratio = 0;
            ratio = ru == ReportUnit.HundredthsOfAnInch ? 100 : 254;
            var g = Graphics.FromHwnd(IntPtr.Zero);
            double dpiX = g.DpiX;
            g.Dispose();
            return value * ratio / dpiX;
        }

        private void xrPictureBoxLogo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var pb = sender as XRPictureBox;

            if (pb != null && pb.Image != null)
            {
                try
                {
                    var reportUnit = ((XtraReport)pb.Report).ReportUnit;
                    var imageWidth = ConvertFromPixel(reportUnit, pb.Image.Size.Width);
                    var imageHeight = ConvertFromPixel(reportUnit, pb.Image.Size.Height);

                    var ratio = imageHeight / pb.Size.Height;
                    var unitratio = reportUnit == ReportUnit.HundredthsOfAnInch ? 100 : 254;
                    var right = (int)(pb.Size.Width - imageWidth / ratio);
                    var padding = new PaddingInfo(0, right<0?0:right, 0, 0, unitratio<0?0:unitratio);
                    pb.Padding = padding;
                }
                catch (Exception){}                
            }            
        }

        private void detailBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Align the report clinic info to the right side regardless or report width
            //xrRichTextClinicInfo.LocationFloat = 
            //    new DevExpress.Utils.PointFloat(xrRichTextClinicInfo.LocationFloat.X + 
            //                    (MasterReport.PageWidth - (xrRichTextClinicInfo.LocationFloat.X + xrRichTextClinicInfo.WidthF)), 0F);
        }
    }
}
