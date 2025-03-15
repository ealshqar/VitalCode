
using System;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using Vital.Business.Shared.DomainObjects.Invoices;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.DomainObjects.PatientSchedules;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Shared;

namespace Vital.UI.UI_Components.Reports
{
    public partial class XtraReportImprinting : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportImprinting()
        {
            InitializeComponent();
        }

        private void ReportHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if ((bool)HideLogo.Value)
            {
                xrTableRowLogo.Visible = false;
                xrSubreportHeader.Visible = false;
                ReportHeader1.HeightF = ReportHeader1.HeightF - xrTableRowLogo.HeightF;
            }
        }

        //Bind cell and set formatting based on Adjustment type
        private void XtraReportProductInvoice_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //((XtraReport)this).StyleSheetPath = Application.StartupPath + @"\" + "ReportStyleSheet.repss";           
        }
    
        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if ((bool)HidePatientName.Value)
            {
                xrTableCellClientNameFooterLabel.Visible = false;
                xrTableCellClientNameFooterValue.Visible = false;
            }
        }
    }
}
