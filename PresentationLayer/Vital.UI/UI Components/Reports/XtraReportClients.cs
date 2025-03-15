
using System;
using Vital.Business.Shared.DomainObjects.PatientSchedules;

namespace Vital.UI.UI_Components.Reports
{
    public partial class XtraReportClients : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportClients()
        {
            InitializeComponent();
        }

        private void ReportHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void BottomMargin_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }
    }
}
