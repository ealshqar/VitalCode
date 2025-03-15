
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils.OAuth;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Vital.Business.Shared.DomainObjects.Invoices;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.DomainObjects.PatientSchedules;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.DomainObjects.VitalForceSheet;
using Vital.Business.Shared.Shared;

namespace Vital.UI.UI_Components.Reports
{
    public partial class XtraReportVFS : DevExpress.XtraReports.UI.XtraReport
    {
        #region Constructor

        public XtraReportVFS()
        {
            InitializeComponent();
        }

        #endregion

        #region Logic

        /// <summary>
        /// Sets a picturebox image
        /// </summary>
        /// <param name="changeType"></param>
        /// <param name="isPrevious"></param>
        private void SetPictureBoxImage(string changeType,XRPictureBox pictureBox)
        {
            Image resultImage = null;

            if (changeType == NormalChangeType.Value.ToString())
            {
                resultImage = Vital.UI.Properties.Resources.Normal;
            }
            else if (changeType == ElevationChangeType.Value.ToString())
            {
                resultImage = Vital.UI.Properties.Resources.Elevation;
            }
            else if (changeType == DecreaseChangeType.Value.ToString())
            {
                resultImage = Vital.UI.Properties.Resources.Decrease;
            }

            pictureBox.Image = resultImage;
        }

        /// <summary>
        /// Sets a major issue picture
        /// </summary>
        /// <param name="changeType"></param>
        /// <param name="isPrevious"></param>
        private void SetMajorIssuesPictureBoxImage(FourFactorState state, XRPictureBox pictureBox)
        {
            Image resultImage = null;

            switch (state)
            {
                case FourFactorState.Balanced:
                    pictureBox.Image = Vital.UI.Properties.Resources.CircleBalanced;
                    break;
                case FourFactorState.UnBalanced:
                    pictureBox.Image = Vital.UI.Properties.Resources.CircleUnbalanced;
                    break;
                case FourFactorState.Clear:
                    pictureBox.Image = Vital.UI.Properties.Resources.CircleClear;
                    break;
            }
        }

        /// <summary>
        /// Sets cell text as formatted text
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="vfsItem"></param>
        private void SetCellTextFormatted(XRTableCell cell, VFSItem vfsItem)
        {
            if (vfsItem == null || vfsItem.IsOnFlyItem) return;

            Lookup typeLookup = null;

            if (cell == null || string.IsNullOrEmpty(cell.Text)) return;

            typeLookup = cell.Tag.ToString() == "C1" || cell.Tag.ToString() == "P1" ? vfsItem.VFSItemSource.V1TypeLookup :
                                                                                      vfsItem.VFSItemSource.V2TypeLookup;

            if (typeLookup == null) return;

            var enumType = EnumNameResolver.GetValueFromDescription<VFSSourceItemValueType>(typeLookup.Value);

            var decimalValue = decimal.Parse(cell.Text);

            switch (enumType)
            {
                case VFSSourceItemValueType.Integer:
                    var value = int.Parse(decimalValue.ToString("N0"));
                    cell.Text = value.ToString("n0");
                    break;
                case VFSSourceItemValueType.Decimal:
                    cell.Text = (decimalValue % 1) == 0 ? decimalValue.ToString("N0") : decimalValue.ToString("N2");
                    break;
                case VFSSourceItemValueType.Lookup:
                case VFSSourceItemValueType.Percentage:
                    var valuePercent = decimalValue * 100;
                    cell.Text = (valuePercent % 1) == 0 ? decimalValue.ToString("P0") : decimalValue.ToString("P");
                    break;
            }
        }

        #endregion

        #region Handlers

        #region General

        /// <summary>
        /// Handle report log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if ((bool)HideLogo.Value)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handle address Printing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailReportAddress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!(bool)ShowAddressInfo.Value)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Main Header Pring Option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = PrintingSystem.Document.PageCount != 0;
            xrTableRow33.Visible = !((bool) HidePatientName.Value);
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if ((bool)HidePatientName.Value)
            {
                xrTableCellClientNameFooterLabel.Visible = false;
                xrTableCellClientNameFooterValue.Visible = false;
            }
        }

        #endregion

        #region Section 1

        /// <summary>
        /// Handle detail printing in section 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailVFSItemsSection1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var vfsItem = DetailReportVFSItemsSection1.GetCurrentRow() as VFSItem;

            if (vfsItem == null) return;

            var value1Type = VFSSourceItemValueType.None;

            if (!vfsItem.IsOnFlyItem && vfsItem.VFSItemSource.V1TypeLookup != null)
            {
                value1Type = EnumNameResolver.GetValueFromDescription<VFSSourceItemValueType>(vfsItem.VFSItemSource.V1TypeLookup.Value);
            }

            var showChangeTypeImages = !vfsItem.IsOnFlyItem &&
                                       value1Type == VFSSourceItemValueType.Lookup;

            var showNormalColumnDetail = EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(vfsItem.GroupLookup.Value) !=
                                         VFSSourceItemGroup.OrganAndGlandsTwo && value1Type != VFSSourceItemValueType.Lookup;

            var showTwoColumnWithPic = EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(vfsItem.GroupLookup.Value) ==
                                       VFSSourceItemGroup.OrganAndGlandsTwo && value1Type == VFSSourceItemValueType.Lookup;

            xrTableSection1Normal.Visible = showNormalColumnDetail;
            xrTableSection1TwoColumn.Visible = !showNormalColumnDetail && showTwoColumnWithPic;
            xrTableSection1TwoColumnNoPic.Visible = !showNormalColumnDetail && !showTwoColumnWithPic;

            xrPictureBoxP.Visible = showChangeTypeImages;
            xrPictureBoxC.Visible = showChangeTypeImages;

            if (showChangeTypeImages)
            {
                var enumType = EnumNameResolver.GetValueFromDescription<VFSSourceItemValueType>(vfsItem.VFSItemSource.V1TypeLookup.Value);

                if (enumType == VFSSourceItemValueType.Lookup)
                {
                    SetPictureBoxImage(vfsItem.CurrentV1, xrPictureBoxC);
                    SetPictureBoxImage(vfsItem.PreviousV1, xrPictureBoxP);
                }
            }
        }

        /// <summary>
        /// Handle Header selection in section 1 based on group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailGroups_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var vfsItem = DetailReportVFSItemsSection1.GetCurrentRow() as VFSItem;

            var showTwoColumnHeader = vfsItem != null && 
                                     !vfsItem.IsOnFlyItem &&
                                      vfsItem.VFSItemSource.V1TypeLookup != null &&
                                      EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(vfsItem.GroupLookup.Value) ==
                                      VFSSourceItemGroup.OrganAndGlandsTwo;

            xrTableSection1HeaderNormal.Visible = !showTwoColumnHeader;
            xrTableSection1HeaderTwoColumn.Visible = showTwoColumnHeader;
        }

        /// <summary>
        /// Handle cell formatting in section 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrTableCell_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var vfsItem = DetailReportVFSItemsSection1.GetCurrentRow() as VFSItem;
            SetCellTextFormatted(sender as XRTableCell, vfsItem);
        }

        #endregion

        #region Section 3

        /// <summary>
        /// Handle Header selection in section 3 based on group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailSection3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var vfsItem = DetailReportVFSItemsSection3.GetCurrentRow() as VFSItem;

            var showpHColumnHeader = vfsItem != null &&
                                     !vfsItem.IsOnFlyItem &&
                                      vfsItem.VFSItemSource.V1TypeLookup != null &&
                                      EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(vfsItem.GroupLookup.Value) ==
                                      VFSSourceItemGroup.pH;

            xrTableSection3ServiceHeader.Visible = !showpHColumnHeader;
            xrTableSection3pHHeader.Visible = showpHColumnHeader;
        }

        /// <summary>
        /// Handle detail printing in section 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailVFSItemsSection3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var vfsItem = DetailReportVFSItemsSection3.GetCurrentRow() as VFSItem;

            if (vfsItem == null) return;

            var showpHColumnDetail = EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(vfsItem.GroupLookup.Value) ==
                                     VFSSourceItemGroup.pH;

            xrTableSection3ServiceDetail.Visible = !showpHColumnDetail;
            xrTableSection3pHDetail.Visible = showpHColumnDetail;
        }

        /// <summary>
        /// Handle cell formatting in section 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrTableCellSection3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var vfsItem = DetailReportVFSItemsSection3.GetCurrentRow() as VFSItem;
            SetCellTextFormatted(sender as XRTableCell, vfsItem);
        }

        #endregion

        #region Section 4

        /// <summary>
        /// Handles prinintg of section 4 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailReportSection4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var vfsItem = DetailReportVFSItemsSection3.GetCurrentRow() as VFSItem;
            var phGroup = (bindingSourceGroupsSection3.DataSource as BindingList<VFSReportGroup>)
                           .FirstOrDefault(g => EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(g.GroupLookup.Value) == VFSSourceItemGroup.pH);

            if (vfsItem == null || phGroup == null || phGroup.VFSItems == null)
            {
                e.Cancel = true;
                return;
            }

            var lastItem = phGroup.VFSItems.LastOrDefault();

            if (lastItem == null)
            {
                e.Cancel = true;
                return;
            }

            e.Cancel = vfsItem.Id != lastItem.Id;
        }

        /// <summary>
        /// Handles printing section 4 detail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailSection4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var vfsItem = DetailReportSection4.GetCurrentRow() as VFSItem;

            if (vfsItem == null) return;

            var value1Type = VFSSourceItemValueType.None;

            if (!vfsItem.IsOnFlyItem && vfsItem.VFSItemSource.V1TypeLookup != null)
            {
                value1Type = EnumNameResolver.GetValueFromDescription<VFSSourceItemValueType>(vfsItem.VFSItemSource.V1TypeLookup.Value);
            }

            var showTwoColumnWithPic = value1Type == VFSSourceItemValueType.Lookup;

            xrTableSection4DetailWithPic.Visible = showTwoColumnWithPic;
            xrTableSection4DetailWithNoPic.Visible = !showTwoColumnWithPic;

            if (showTwoColumnWithPic)
            {
                var enumType = EnumNameResolver.GetValueFromDescription<VFSSourceItemValueType>(vfsItem.VFSItemSource.V1TypeLookup.Value);

                if (enumType == VFSSourceItemValueType.Lookup)
                {
                    SetPictureBoxImage(vfsItem.CurrentV1, xrPictureBoxS4C);
                    SetPictureBoxImage(vfsItem.PreviousV1, xrPictureBoxS4P);
                }
            }
        }

        /// <summary>
        /// Handle cell formatting in section 4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrTableCellSection4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var vfsItem = DetailReportSection4.GetCurrentRow() as VFSItem;
            SetCellTextFormatted(sender as XRTableCell, vfsItem);
        }

        #endregion

        #region Major Issues

        /// <summary>
        /// Handles printing major issue line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailMajorIssues_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var majorIssue = DetailReportMajorIssues.GetCurrentRow() as TestResult;

            if (majorIssue == null) return;

            SetMajorIssuesPictureBoxImage(majorIssue.Lymphatic, xrPictureBoxLymphatic);
            SetMajorIssuesPictureBoxImage(majorIssue.Nerve, xrPictureBoxNerve);
            SetMajorIssuesPictureBoxImage(majorIssue.Organ, xrPictureBoxOrgan);
            SetMajorIssuesPictureBoxImage(majorIssue.Circulation, xrPictureBoxCirculation);
        }

        /// <summary>
        /// Handles printing major issues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailReportMajorIssues_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = bindingSourceTest.DataSource == null ||
                       ((bindingSourceTest.DataSource as Test) != null &&
                        (bindingSourceTest.DataSource as Test).TestIssues.Count == 0);
        }

        #endregion

        #region Secondary Issues

        /// <summary>
        /// Primary Issues show hide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailReportPrimaryIssues_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var vfs = bindingSourceVFS.DataSource as VFS;

            if (vfs == null ||
                vfs.PrimaryIssues == null ||
                vfs.PrimaryIssues.Count == 0)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// SecondaryIssues show hide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailReportSecondaryIssues_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var vfs = bindingSourceVFS.DataSource as VFS;

            if (vfs == null ||
                vfs.SecondaryIssues == null ||
                vfs.SecondaryIssues.Count == 0)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// ThyroidIssues show hide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailReportThyroidIssues_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var vfs = bindingSourceVFS.DataSource as VFS;

            if (vfs == null ||
                vfs.ThyroidIssues == null ||
                vfs.ThyroidIssues.Count == 0)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// MercuryIssues show hide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailReportMercuryIssues_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var vfs = bindingSourceVFS.DataSource as VFS;

            if (vfs == null ||
                vfs.MercuryIssues == null ||
                vfs.MercuryIssues.Count == 0)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region Notes

        /// <summary>
        /// Handle emotional stress show hide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailReportEmotionalIssues_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var vfs = bindingSourceVFS.DataSource as VFS;

            if (vfs == null || string.IsNullOrEmpty(vfs.EmotionalIssues))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handle notes show hide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailReportNotes_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var vfs = bindingSourceVFS.DataSource as VFS;

            if (vfs == null || string.IsNullOrEmpty(vfs.Notes))
            {
                e.Cancel = true;
            }
        }

        #endregion
        
        #endregion
    }
}
