namespace Vital.UI.UI_Components.Reports
{
    public partial class XtraReportCapsolT : DevExpress.XtraReports.UI.XtraReport
    {
        #region Constructors

        public XtraReportCapsolT()
        {
            InitializeComponent();
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handles the showing/hiding of patient name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if ((bool)HidePatientName.Value)
            {
                xrTableCellPatientName.Visible = false;
                xrTableCellPatientNameLabel.Visible = false;
                xrTableCellClientNameFooterLabel.Visible = false;
                xrTableCellClientNameFooterValue.Visible = false;
            }

            if ((bool)HideLogo.Value)
            {
                xrTableRowLogo.Visible = false;
                xrSubreportHeader.Visible = false;
                ReportHeader1.HeightF = ReportHeader1.HeightF - xrTableRowLogo.HeightF;
            }
        }

        #endregion
    }
}
