
using System;
using Vital.Business.Shared.DomainObjects.PatientSchedules;

namespace Vital.UI.UI_Components.Reports
{
    public partial class XtraReportPatientSchedule : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportPatientSchedule()
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

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var hideDefaultInstructions = !((bool) ShowDefaultInstructions.Value);
            var hideSpecialInstructions = string.IsNullOrEmpty(((TestSchedule)bindingSourceTestSchedule.DataSource).SpecialInstructions);

            if (hideDefaultInstructions && hideSpecialInstructions)
            {
                e.Cancel = true;
            }

            if (hideDefaultInstructions)
            {
                xrLabel3.Visible = false;
                xrLine3.Visible = false;
            }

            if (hideSpecialInstructions)
            {
                xrLabel1.Visible = false;
                xrLabel5.Visible = false;
            }
        }

        private void GroupHeaderTechnicianInfo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = !(bool) ShowTechnicianInfo.Value;
        }

        private void BottomMargin_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if ((bool)HidePatientName.Value)
            {
                xrTableCellClientNameFooterLabel.Visible = false;
                xrTableCellClientNameFooterValue.Visible = false;
            }
        }
    }
}
