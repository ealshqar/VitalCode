using DevExpress.XtraReports.UI;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.Invoices;
using Vital.UI.UI_Components.Reports;

namespace Vital.UI.UI_Components.Forms.BaseForms
{
    public partial class XtraFormReportViewer : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        /// <summary>
        /// Gets or sets the current report.
        /// </summary>
        public XtraReport CurrentReport
        {
            get; set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// The constructor.
        /// </summary>
        public XtraFormReportViewer()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the report.
        /// </summary>
        /// <param name="report">The report.</param>
        public void SetReport(XtraReport report)
        {
            printBarManagerMain.PrintControl.PrintingSystem = report.PrintingSystem;
            
            CurrentReport = report;

            report.CreateDocument();
        }
        
        #endregion
        
        #region Private Methods

        /// <summary>
        /// Executes the print invoice logic.
        /// </summary>
        private void PrintInvoiceLogic()
        {
            var reportTypeName = CurrentReport.GetType().Name;

            if (reportTypeName != typeof(XtraReportProductInvoice).Name)
                return;

            var invoiceReport = (XtraReportProductInvoice) CurrentReport;

            var invoiceObj = (Invoice) invoiceReport.bindingSourceInvoice.DataSource;

            if (!invoiceObj.IsFirstTimeAfterClosing) return;

            var invoicesManager = new InvoicesManager();

            if (invoicesManager.SaveInvoice(invoiceObj).IsSucceed)
                invoiceObj.IsFirstTimeAfterClosing = false;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handles the print button click of the report viewer.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void printButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintInvoiceLogic();
        }

        /// <summary>
        /// Handles the print preview button click of the report viewer.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void printPreviewBarItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintInvoiceLogic();
        }

        #endregion
    }
}