using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using Vital.Business.Shared.DomainObjects.AutoTestDestination;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Reports
{
    public partial class XtraReportAutoTest : XtraReport
    {
        #region Private Members

        private TreeList _treeListTestingResults;

        #endregion

        #region Public Properties

        /// <summary>
        /// Current Auto Test
        /// </summary>
        public AutoTest CurrentAutoTest
        {
            get
            {
                return bindingSourceAutoTest.DataSource as AutoTest;
            }
        }

        public TreeList TestingResultsTree { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public XtraReportAutoTest()
        {
            InitializeComponent();
            SetCustomFonts();
            GenerateTestingResultsTreeList();
        }

        #endregion

        #region Logic

        /// <summary>
        /// Handle setting custom fonts included with Vital without installation on user computer
        /// </summary>
        private void SetCustomFonts()
        {
            reportProgressBarPreliminary.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 10F);
            reportProgressBarSummary.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 10F);
            xrLabeClinicName.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 9.75F);
            xrLabel1.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 18F);
            xrLabel2.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 10F);
            xrLabelClientName.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 16F);
            xrPageInfo1.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 9.75F);
            xrPageInfoReportDate.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            xrTableCell12.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 10F);
            xrTableCell14.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 16F);
            xrTableCell15.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 10F);
            xrTableCell19.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 16F);
            xrTableCell21.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 16F);
            xrTableCell23.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 16F);
            xrTableCellPointName.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 10F);
            xrTableRow1.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            xrTableRow20.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            xrTableRow6.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 10F);
        }

        /// <summary>
        /// Generates the testing results tree list UI
        /// </summary>
        private void GenerateTestingResultsTreeList()
        {
            //IMPORTANT: Showing Hierarchical Tree Based Testing Results In Report
            /* This report contains data (AutoTestResults) that are supposed to be shown in Parent/Child structure, however XtraReport do not
             * have a dedicated Tree report control and there are workarounds available like building a custom report control or using dynamic
             * sub-reports however the solution we used is to simply print a TreeList control and include its print result as part of the report.
             * This behaviour requires generating the TreeList control and customizing its appearance to work well with the report. In the logic below
             * we copied the designer code of the TreeList used in XtraUserControlAutoTestResults and created an instance here to be used in report. We initially
             * preferred to create an instance of the user control instead of writing the code below to build the tree programatically however that didn't work because
             * the TreeList inside the user control already had a parent control appearantly and when we included it in the report it didn't expand and remained folded
             * and when we tried to build the tree by code as below the tree worked finally and so we will use the code below. Using the user control would have been easier
             * and more convenient but it didn't work and so we have to live with the approach below.
             * 
             * The advantage with building the tree below is that we can use our own custom tree instance and we don't create dependency on any TreeList instance used in the
             * UI which makes it subject to changes in future which might conflict with the report requirements, the separation is good for the report.
             * 
             * 
             * 
            */

            //TreeList General
            //---------------------------------------------------------------------------
            _treeListTestingResults = new TreeList();
            _treeListTestingResults.Name = "_treeListTestingResults";
            _treeListTestingResults.KeyFieldName = "StructureId";
            _treeListTestingResults.ParentFieldName = "StructureParentId";
            _treeListTestingResults.Location = new Point(0, 0);
            _treeListTestingResults.Size = new Size(653, 560);
            _treeListTestingResults.Dock = DockStyle.Fill;
            _treeListTestingResults.TabIndex = 0;
            _treeListTestingResults.NodeCellStyle += _treeListTestingResults_NodeCellStyle;
            //---------------------------------------------------------------------------

            //RepositoryItems
            var repositoryItemMemoEditNotes = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
            {
                Name = "repositoryItemMemoEditNotes"
            };
            _treeListTestingResults.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemMemoEditNotes });
            //---------------------------------------------------------------------------

            //TreeList Columns
            //---------------------------------------------------------------------------
            var treeListColumnName = new TreeListColumn();
            treeListColumnName.Caption = "Results";
            treeListColumnName.FieldName = "AutoItem.Name";
            treeListColumnName.Name = "treeListColumnName";
            treeListColumnName.Visible = true;
            treeListColumnName.VisibleIndex = 0;

            var treeListColumnNotes = new TreeListColumn();
            treeListColumnNotes.Caption = "Notes";
            treeListColumnNotes.FieldName = "Notes";
            treeListColumnNotes.Name = "TreeListColumnNotes";
            treeListColumnNotes.ColumnEdit = repositoryItemMemoEditNotes;
            treeListColumnNotes.Visible = true;
            treeListColumnNotes.VisibleIndex = 1;

            var treeListColumnIsDeleted = new TreeListColumn();
            treeListColumnIsDeleted.Caption = "IsDeleted";
            treeListColumnIsDeleted.FieldName = "IsDeletedMemory";
            treeListColumnIsDeleted.Name = "treeListColumnIsDeleted";
            //---------------------------------------------------------------------------
            
            //Add columns to the TreeList
            _treeListTestingResults.Columns.AddRange(new[] { treeListColumnName, treeListColumnNotes, treeListColumnIsDeleted });

            //TreeList Behavior
            //---------------------------------------------------------------------------
            _treeListTestingResults.OptionsBehavior.AutoFocusNewNode = true;
            _treeListTestingResults.OptionsBehavior.Editable = false;
            _treeListTestingResults.OptionsBehavior.EnableFiltering = true;
            _treeListTestingResults.OptionsMenu.EnableColumnMenu = false;
            _treeListTestingResults.OptionsMenu.EnableFooterMenu = false;
            _treeListTestingResults.OptionsSelection.EnableAppearanceFocusedCell = false;
            _treeListTestingResults.OptionsView.EnableAppearanceEvenRow = true;
            _treeListTestingResults.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;

            //Very important to make sure the report doesn't show deleted items in case the user viewed the report after deleting an item and before saving
            _treeListTestingResults.ActiveFilterCriteria = new BinaryOperator(ExpressionHelper.GetPropertyName(() => CurrentAutoTest.IsDeletedMemory), false.ToString());
            //---------------------------------------------------------------------------

            //TreeList Appearence [IMPORTANT: Notice that we set the fonts using GetCustomFont method to make sure they show up correctly]
            //---------------------------------------------------------------------------
            _treeListTestingResults.Appearance.FocusedRow.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            _treeListTestingResults.Appearance.HeaderPanel.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            _treeListTestingResults.Appearance.Row.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 10F);
            _treeListTestingResults.AppearancePrint.HeaderPanel.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            _treeListTestingResults.AppearancePrint.Row.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 10F);

            _treeListTestingResults.AppearancePrint.HeaderPanel.BackColor = Color.Transparent;
            _treeListTestingResults.AppearancePrint.HeaderPanel.BorderColor = Color.Transparent;
            _treeListTestingResults.Appearance.HeaderPanel.BorderColor = Color.Transparent;
            _treeListTestingResults.Appearance.HeaderPanel.Options.UseBorderColor = true;
            _treeListTestingResults.AppearancePrint.HeaderPanel.BorderColor = Color.Transparent;
            _treeListTestingResults.AppearancePrint.HeaderPanel.Options.UseBorderColor = true;

            _treeListTestingResults.Appearance.FocusedRow.Options.UseFont = true;
            _treeListTestingResults.Appearance.HeaderPanel.Options.UseFont = true;
            _treeListTestingResults.Appearance.Row.Options.UseFont = true;
            _treeListTestingResults.AppearancePrint.HeaderPanel.Options.UseFont = true;
            _treeListTestingResults.OptionsPrint.PrintPageHeader = false;
            _treeListTestingResults.OptionsPrint.PrintReportFooter = false;
            _treeListTestingResults.OptionsPrint.PrintTreeButtons = false;
            _treeListTestingResults.OptionsPrint.PrintHorzLines = true;
            _treeListTestingResults.OptionsPrint.PrintVertLines = false;
            _treeListTestingResults.OptionsPrint.UsePrintStyles = true;
            _treeListTestingResults.AppearancePrint.Lines.BackColor = Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            
            
            //_treeListTestingResults.AppearancePrint.Row.BackColor = Color.Transparent;
            //_treeListTestingResults.AppearancePrint.Row.BorderColor = Color.Transparent;
            //_treeListTestingResults.AppearancePrint.FooterPanel.BackColor = Color.Transparent;
            //_treeListTestingResults.AppearancePrint.FooterPanel.BorderColor = Color.Transparent;
            //_treeListTestingResults.AppearancePrint.GroupFooter.BackColor = Color.Transparent;
            //_treeListTestingResults.AppearancePrint.GroupFooter.BorderColor = Color.Transparent;
            //---------------------------------------------------------------------------
        }

        void _treeListTestingResults_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            e.Appearance.BorderColor = Color.Transparent;
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
                //xrTableCellPatientName.Visible = false;
                //xrTableCellPatientNameLabel.Visible = false;
                //xrTableCellClientNameFooterLabel.Visible = false;
                //xrTableCellClientNameFooterValue.Visible = false;
            }

            if ((bool)HideLogo.Value)
            {
                //xrTableRowLogo.Visible = false;
                xrSubreportHeader.Visible = false;
                //ReportHeader1.HeightF = ReportHeader1.HeightF - xrTableRowLogo.HeightF;
            }
        }

        /// <summary>
        /// Handle printing readings section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailReportPreliminarySummary_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = !CurrentAutoTest.TestHasStage(StageKey.Preliminary) || !CurrentAutoTest.Readings.Any();
        }

        /// <summary>
        /// Handle printing testing results section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailReportTestResults_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = !CurrentAutoTest.TestHasStage(StageKey.Testing) || !CurrentAutoTest.TestingResults.Any();
        }

        /// <summary>
        /// Handle printing results section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailReportTestingResultTree_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = !CurrentAutoTest.TestHasStage(StageKey.Testing) || !CurrentAutoTest.TestingResults.Any();
        }

        /// <summary>
        /// Handle printing products section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailReportProducts_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = !CurrentAutoTest.TestHasStage(StageKey.Dosage) || !CurrentAutoTest.CheckedProducts.Any();
        }

        /// <summary>
        /// Handle printing notes section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailReportNotes_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = string.IsNullOrEmpty(CurrentAutoTest.Notes);
        }

        /// <summary>
        /// Handle showing the testing results tree list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraReportAutoTest_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {   
            //Validate that TreeList control is initialized
            if (_treeListTestingResults != null)
            {
                //Set TreeList datasource here because we can't set it during initialiation in constructor since it won't be set yet
                _treeListTestingResults.DataSource = CurrentAutoTest.TestingResults;

                //Add the TreeList control to the detail section for testing results
                _treeListTestingResults.Parent = new Form();
                var wcc = new WinControlContainer();
                DetailTestingResultTree.Controls.Add(wcc);
                wcc.WinControl = _treeListTestingResults;

                //IMPORTANT:
                //Initialize and expand the tree control, otherwise it won't show correctly or it will show up folded
                _treeListTestingResults.ForceInitialize();
                _treeListTestingResults.ExpandAll();
            }
            else
            {
                e.Cancel = true;
            }
        }

        #endregion
    }
}