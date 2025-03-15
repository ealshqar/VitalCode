using System;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.UI.UI_Components.User_Controls.Modules.BaseModules;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlPrintingOptions : XtraUserControlBaseSettingsBar
    {
        #region Properties

        public bool ShowEDS
        {
            get { return checkEditPrintEDS.Checked; }
        }

        public bool ShowResults
        {
            get { return checkEditPrintItemTesting.Checked; }
        }

        /// <summary>
        /// An Option that can be used for quick printing of short or long report without changing option
        /// </summary>
        public bool ShowDescriptionTemporaryOption { get; set; }

        public bool ShowDescription
        {
            get { return checkEditPrintDescription.Checked; }
        }

        public bool ShowFullName
        {
            get { return checkEditFullPointName.Checked; }
        }

        public bool ShowDateWithoutTime
        {
            get { return checkEditShowDateWithoutTime.Checked; }
        }
        
        public bool ShowMeridian
        {
            get { return checkEditShowMeridian.Checked; }
        }

        public bool HidePatientName
        {
            get { return checkEditHidePatientName.Checked; }
        }

        public bool HideLogo
        {
            get { return checkEditHideLogo.Checked; }
        }

        public bool ShowNotes
        {
            get { return checkEditShowNotes.Checked; }
        }

        public bool GroupCustomDilutions
        {
            get { return checkEditGroupDilutions.Checked; }
        }

        public bool ShowTechnicianInfo
        {
            get { return checkEditShowTechnicianInfo.Checked; }
        }

        public bool ShowAddressInfo
        {
            get { return checkEditShowAddressInfo.Checked; }
        }

        public bool ShowDescriptionCheck { get; set; }

        public bool ShowPriceInfoCheck { get; set; }

        public bool ShowEDSCheck { get; set; }

        public bool ShowResultsCheck { get; set; }

        public bool ShowFullNameCheck { get; set; }

        public bool ShowDateWithoutTimeCheck { get; set; }

        public bool ShowMeridianCheck { get; set; }

        public bool ShowNotesCheck { get; set; }

        public bool ShowGroupCustomDilution { get; set; }

        public bool ShowTechnicianInfoCheck { get; set; }

        public bool ShowAddressInfoCheck { get; set; }

        #endregion

        #region Constructor

        public XtraUserControlPrintingOptions()
        {
            ShowPriceInfoCheck = true;
            InitializeComponent();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Set binding for the controls.
        /// </summary>
        protected override void SetBinding()
        {
            checkEditPrintEDS.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.PrintingSettings, SettingKeys.ReportPrintEdsReadings);
            checkEditPrintEDS.Tag = SettingKeys.ReportPrintEdsReadings;
            checkEditPrintItemTesting.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.PrintingSettings, SettingKeys.ReportPrintTestResults);
            checkEditPrintItemTesting.Tag = SettingKeys.ReportPrintTestResults;
            checkEditPrintDescription.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.PrintingSettings, SettingKeys.ReportPrintDescriptiveText);
            checkEditPrintDescription.Tag = SettingKeys.ReportPrintDescriptiveText;
            checkEditFullPointName.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.PrintingSettings, SettingKeys.ReportUseFullPointName);
            checkEditFullPointName.Tag = SettingKeys.ReportUseFullPointName;
            checkEditShowMeridian.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.PrintingSettings, SettingKeys.ReportShowMeridian);
            checkEditShowMeridian.Tag = SettingKeys.ReportShowMeridian;
            checkEditShowDateWithoutTime.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.PrintingSettings, SettingKeys.ReportShowDateWithoutTime);
            checkEditShowDateWithoutTime.Tag = SettingKeys.ReportShowDateWithoutTime;
            checkEditHidePatientName.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.PrintingSettings, SettingKeys.ReportHidePatientName);
            checkEditHidePatientName.Tag = SettingKeys.ReportHidePatientName;
            checkEditShowNotes.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.PrintingSettings, SettingKeys.ReportShowTestNotes);
            checkEditShowNotes.Tag = SettingKeys.ReportShowTestNotes;
            checkEditHideLogo.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.PrintingSettings, SettingKeys.ReportHideLogo);
            checkEditHideLogo.Tag = SettingKeys.ReportHideLogo;
            checkEditGroupDilutions.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.PrintingSettings, SettingKeys.ReportGroupDilutions);
            checkEditGroupDilutions.Tag = SettingKeys.ReportGroupDilutions;
            checkEditShowTechnicianInfo.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.PrintingSettings, SettingKeys.ShowTechnicianInfo);
            checkEditShowTechnicianInfo.Tag = SettingKeys.ShowTechnicianInfo;
            checkEditShowAddressInfo.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.PrintingSettings, SettingKeys.ShowAddressInfo);
            checkEditShowAddressInfo.Tag = SettingKeys.ShowAddressInfo;
        }

        /// <summary>
        /// Set the controls visibility.
        /// </summary>
        private void SetControlsVisbility()
        {
            layoutControlItemShowEDS.Visibility = ShowEDSCheck ? LayoutVisibility.Always : LayoutVisibility.Never;
            layoutControlItemShowResults.Visibility = ShowResultsCheck ? LayoutVisibility.Always : LayoutVisibility.Never;
            layoutControlItemShowFullName.Visibility = ShowFullNameCheck ? LayoutVisibility.Always : LayoutVisibility.Never;
            layoutControlItemShowMeridian.Visibility = ShowMeridianCheck ? LayoutVisibility.Always : LayoutVisibility.Never;
            layoutControlItemShowDateWithoutTime.Visibility = ShowDateWithoutTimeCheck ? LayoutVisibility.Always : LayoutVisibility.Never;
            layoutControlItemShowNotes.Visibility = ShowNotesCheck ? LayoutVisibility.Always : LayoutVisibility.Never;
            layoutControlItemShowDescriptiveText.Visibility = ShowDescriptionCheck ? LayoutVisibility.Always : LayoutVisibility.Never;
            layoutControlItemGroupDilutions.Visibility = ShowGroupCustomDilution ? LayoutVisibility.Always : LayoutVisibility.Never;
            layoutControlItemShowTechnicianInfo.Visibility = ShowTechnicianInfoCheck ? LayoutVisibility.Always : LayoutVisibility.Never;
            layoutControlItemShowAddressInfo.Visibility = ShowAddressInfoCheck ? LayoutVisibility.Always : LayoutVisibility.Never;
        }

        /// <summary>
        /// Update the options.
        /// </summary>
        public void UpdateOptions()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.UpdatingPrintingSetting);

            SetBinding();

            UiHelperClass.HideSplash();
        }

        #endregion

        #region Events
        
        /// <summary>
        /// Handles loading event behavior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraUserControlPrintingOptions_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(XtraUserControlPrintingOptions_Load), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SetControlsVisbility();
            }
        }

        /// <summary>
        /// Handel the check change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEditPrintEDS_CheckedChanged(object sender, System.EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(checkEditPrintEDS_CheckedChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (IsInitialized)
                    UiHelperClass.UpdateCheckSettingValue(CachableDataEnum.PrintingSettings, sender as CheckEdit, SettingsManagerObject);
            }
        }

        #endregion                
    }
}
