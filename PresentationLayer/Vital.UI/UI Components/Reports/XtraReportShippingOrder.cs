
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using Vital.Business.Shared.DomainObjects.Invoices;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.DomainObjects.PatientSchedules;
using Vital.Business.Shared.DomainObjects.ShippingOrders;
using Vital.Business.Shared.DomainObjects.Tests;

namespace Vital.UI.UI_Components.Reports
{
    public partial class XtraReportShippingOrder : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportShippingOrder()
        {
            InitializeComponent();
        }

        private void ReportHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if ((bool)HideLogo.Value || (bool)ForEmail.Value)
            {
                e.Cancel = true;
            }
        }

        //Bind cell and set formatting based on discount type
        private void XtraReportProductInvoice_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //((XtraReport)this).StyleSheetPath = Application.StartupPath + @"\" + "ReportStyleSheet.repss";           
        }

        private void DetailReportClientInfo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!((ShippingOrder) bindingSourceShippingOrder.DataSource).SendToClient)
            {
                e.Cancel = true;
            }
        }

        private void DetailReportTechicianInfo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (((ShippingOrder)bindingSourceShippingOrder.DataSource).SendToClient)
            {
                e.Cancel = true;
            }
        }

        private void DetailReportTechName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!(bool)ForEmail.Value)
            {
                e.Cancel = true;
            }
        }

        private void DetailHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!((ShippingOrder)bindingSourceShippingOrder.DataSource).Sent)
            {
                xrTableCell15.Visible = false;
                xrTableCell14.Visible = false;
                xrTableCell13.Visible = false;
            }

            if ((bool)ForEmail.Value)
            {
                xrTableCell15.Visible = false;
            }
        }       
    }
}
