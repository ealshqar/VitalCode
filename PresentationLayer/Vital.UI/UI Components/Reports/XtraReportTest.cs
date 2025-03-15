using System.Linq;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Readings;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Reports
{
    public partial class XtraReportTest : DevExpress.XtraReports.UI.XtraReport
    {
        #region Fields

        private readonly Lookup _leftListPointLookup;
        private readonly Lookup _rightListPointLookup;

        #endregion

        #region Constructors

        public XtraReportTest()
        {
            InitializeComponent();

            var listPointsLookups = UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.ListPoints));

            _leftListPointLookup = listPointsLookups.FirstOrDefault(l => l.Value == EnumNameResolver.Resolve(ListPoints.Left));
            _rightListPointLookup = listPointsLookups.FirstOrDefault(l => l.Value == EnumNameResolver.Resolve(ListPoints.Right));
        }

        #endregion

        #region Handlers
        
        /// <summary>
        /// Handel BeforePrint event for the DetailReportTestIssues.
        /// </summary>
        private void DetailReportTestIssues_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!(bool)ShowTestResults.Value)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handel BeforePrint event for the DetailTestIssues.
        /// </summary>
        private void DetailTestIssues_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!(bool)ShowDescription.Value)
            {                
                IssueDescriptionRow.Visible = false;
                DetailTestIssues.HeightF = DetailTestIssues.HeightF - IssueDescriptionRow.HeightF;                
            }

            var currentIssue = DetailReportTestIssues.GetCurrentRow() as TestIssue;

            if (currentIssue == null || currentIssue.IsMainIssue)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handel BeforePrint event for the DetailTestResults.
        /// </summary>
        private void DetailTestResults_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!(bool)ShowDescription.Value)
            {
                ResultDescriptionRow.Visible = false;
                DetailTestResults.HeightF = DetailTestResults.HeightF - ResultDescriptionRow.HeightF;
            }
        }

        /// <summary>
        /// Handel BeforePrint event for the DetailIssuesResults.
        /// </summary>
        private void DetailIssuesResults_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!(bool)ShowDescription.Value)
            {
                xrTableRowProductDescription.Visible = false;
                DetailIssuesResults.HeightF = DetailIssuesResults.HeightF - xrTableRowProductDescription.HeightF;
            }
        }

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

        private void GroupFooterTest_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!(bool)ShowNotes.Value)
            {
                e.Cancel = true;
            }
        }

        private void GroupHeaderTechnicianInfo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = !(bool)ShowTechnicianInfo.Value;
        }

        private void DetailReportProductTesting_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var currentIssue = DetailReportTestIssues.GetCurrentRow() as TestIssue;

            if (currentIssue == null || !currentIssue.IsMainIssue)
            {
                e.Cancel = true;
            }
        }

        private void DetailReportReadingPointSets_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!(bool)ShowEDS.Value)
            {
                e.Cancel = true;
            }
        }

        private void DetailPointSetReading_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var currentReading = DetailReportPointSetReading.GetCurrentRow() as Reading;

            if (currentReading != null)
            {
                xrTableCellLocation.Text = currentReading.ListPointLookupId == _leftListPointLookup.Id? _leftListPointLookup.Value:_rightListPointLookup.Value;
            }
        }

        #endregion

        private void DetailTests_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCellTestDate.DataBindings[0].FormatString = (bool)ShowDateWithoutTime.Value ? "{0:M/d/yyyy}" : "{0:M/d/yyyy hh:mm tt}";
        }
    }
}
