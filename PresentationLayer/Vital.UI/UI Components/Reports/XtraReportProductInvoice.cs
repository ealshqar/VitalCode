
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
    public partial class XtraReportProductInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportProductInvoice()
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

        private void xrLabel1_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = ((Patient) bindingSourcePatient.DataSource).Number + " - " + 
                      ((Invoice) bindingSourceInvoice.DataSource).Number;
        }

        /// <summary>
        /// Handles hiding cell's text when value is zero for calculation cells
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculationCell_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            try
            {
                var cell = (XRTableCell)sender;
                var test = (Test)bindingSourceTest.DataSource;
                var tag = cell.Tag.ToString();
                decimal value = 0;
                decimal.TryParse(ExpressionHelper.GetPropertyValue(test, tag).ToString(), out value);

                if (value == 0)
                {
                    cell.Text = string.Empty;
                }
            }
            catch (Exception){}            
        }

        private void DetailReportAddress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!(bool)ShowAddressInfo.Value)
            {
                e.Cancel = true;
            }
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
