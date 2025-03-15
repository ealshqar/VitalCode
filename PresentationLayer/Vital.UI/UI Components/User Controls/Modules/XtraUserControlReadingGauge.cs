using System;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraGauges.Core.Drawing;
using DevExpress.XtraLayout.Utils;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.UI_Components.BaseForms;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlReadingGauge : DevExpress.XtraEditors.XtraUserControl
    {
        #region Constructors

        /// <summary>
        /// The Constructor.
        /// </summary>
        public XtraUserControlReadingGauge()
        {
            InitializeComponent();
        }

        #endregion

        #region Fields

        private TestBarStateEnum _currentBarStatus;

        #endregion

        #region Properties

        /// <summary>
        /// Sets The current reading value on the gauges.
        /// </summary>
        public float ReadingValue
        {
            set 
            {
                SetReadingValue(value);
            }
            private get
            {
                return linearScaleComponentReading.Value;
            }
        }

        /// <summary>
        /// Sets The current reading value on the gauges.
        /// </summary>
        public float ReadingValueTimeLine
        {
            set
            {
                SetReadingValue(value);
            }
            get
            {
                return linearScaleComponentReading.Value;
            }
        }

        /// <summary>
        /// Sets the current reading location on the gauges.
        /// </summary>
        public Lookup LocationLookup
        {
            set
            {
                var locationText = value != null && value.Value != null ? value.Value : String.Empty;
                digitalGaugeHand.DigitCount = locationText.Length > 0 ? locationText.Length : 5;
                digitalGaugeHand.Text = locationText;
            }
        }

        /// <summary>
        /// The yes and no label indicator will be shown or not.
        /// </summary>
        public bool ShowYesNoLabel
        {
            get;
            set;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Boolean ShowLocationGauge { get; set; }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Updates the reading value for the gauges.
        /// </summary>
        /// <param name="locationLookup">The location lookup.</param>
        /// <param name="readingValue">The reading value.</param>
        public void UpdateReading(Lookup locationLookup, float readingValue)
        {
            ReadingValue = readingValue;
            LocationLookup = locationLookup;
        }

        /// <summary>
        /// Clear the controls.
        /// </summary>
        public void Clear()
        {
            ReadingValue = 0;
            LocationLookup = null;
        }

        /// <summary>
        /// Set the status test status bar.
        /// </summary>
        public void SetReadingStatusBarMode(TestBarStateEnum state, string actionName, float secondsToWait)
        {
            switch (state)
            {
                case TestBarStateEnum.TakeReading:
                    labelControlAutoTestStatus.Text = StaticKeys.Waiting;
                    layoutControlItemTestStatus.ContentVisible = true;
                    layoutControlItemProgressBar.ContentVisible = false;
                    layoutControlItemReadingInProgress.ContentVisible = false;
                    simpleSeparator.ContentVisible = false;
                    break;
                case TestBarStateEnum.Ready:
                    labelControlAutoTestStatus.Text = StaticKeys.Ready;
                    layoutControlItemTestStatus.ContentVisible = true;
                    layoutControlItemProgressBar.ContentVisible = false;
                    layoutControlItemReadingInProgress.ContentVisible = false;
                    simpleSeparator.ContentVisible = false;
                    break;
                case TestBarStateEnum.Reading:
                    labelControlAutoTestStatus.Text = StaticKeys.Reading;
                    layoutControlItemTestStatus.ContentVisible = true;
                    layoutControlItemProgressBar.ContentVisible = true;
                    layoutControlItemReadingInProgress.ContentVisible = true;
                    layoutControlItemProgressBar.Visibility = LayoutVisibility.Never;
                    layoutControlItemReadingInProgress.Visibility = LayoutVisibility.Always;
                    SetReadingStatusBarMode(TestBarStateEnum.HideYesNo, string.Empty, 0);
                    break;
                case TestBarStateEnum.WaitMoving:
                    progressBarControlWaiting.Properties.Maximum = CsaEmdUnitManager.Instance.EdsAutoPlayWaitingTime;
                    progressBarControlWaiting.Properties.Minimum = 0;
                    labelControlAutoTestStatus.Text = StaticKeys.Moving;
                    layoutControlItemTestStatus.ContentVisible = true;
                    layoutControlItemProgressBar.ContentVisible = true;
                    layoutControlItemReadingInProgress.ContentVisible = true;
                    progressBarControlWaiting.EditValue = CsaEmdUnitManager.Instance.EdsAutoPlayWaitingTime - (secondsToWait * 1000);
                    layoutControlItemProgressBar.Visibility = LayoutVisibility.Always;
                    layoutControlItemReadingInProgress.Visibility = LayoutVisibility.Never;
                    break;
                case TestBarStateEnum.WaitBeforTakeAction:
                    progressBarControlWaiting.Properties.Maximum = CsaEmdUnitManager.Instance.ItemTestingAutoPlayWaitingTime;
                    progressBarControlWaiting.Properties.Minimum = 0;
                    labelControlAutoTestStatus.Text = actionName + " ...";
                    progressBarControlWaiting.EditValue = CsaEmdUnitManager.Instance.ItemTestingAutoPlayWaitingTime - (secondsToWait * 1000);
                    layoutControlItemTestStatus.ContentVisible = true;
                    layoutControlItemProgressBar.ContentVisible = true;
                    layoutControlItemReadingInProgress.ContentVisible = true;
                    layoutControlItemProgressBar.Visibility = LayoutVisibility.Always;
                    layoutControlItemReadingInProgress.Visibility = LayoutVisibility.Never;
                    break;
                case TestBarStateEnum.HideStatus:
                    layoutControlItemTestStatus.ContentVisible = false;
                    layoutControlItemProgressBar.ContentVisible = false;
                    layoutControlItemReadingInProgress.ContentVisible = false;
                    simpleSeparator.ContentVisible = false;
                    break;
                case TestBarStateEnum.WaitingToRelease:
                    labelControlAutoTestStatus.Text = StaticKeys.Release;
                    layoutControlItemTestStatus.ContentVisible = true;
                    layoutControlItemProgressBar.ContentVisible = false;
                    layoutControlItemReadingInProgress.ContentVisible = false;
                    simpleSeparator.ContentVisible = false;
                    if (ShowYesNoLabel)
                        SetReadingStatusBarMode(
                            CrossLayersSharedLogic.IsAcceptableReading((int)ReadingValue)
                                ? TestBarStateEnum.Yes
                                : TestBarStateEnum.No, string.Empty, 0);
                    break;
                case TestBarStateEnum.Yes:
                    simpleLabelItemYesNo.Text = StaticKeys.Yes;
                    simpleLabelItemYesNo.AppearanceItemCaption.ForeColor = Color.Green;
                    break;
                case TestBarStateEnum.No:
                    simpleLabelItemYesNo.Text = StaticKeys.No;
                    simpleLabelItemYesNo.AppearanceItemCaption.ForeColor = Color.DarkRed;
                    break;
                case TestBarStateEnum.HideYesNo:
                    simpleLabelItemYesNo.Text = StaticKeys.HideYesNo;
                    simpleLabelItemYesNo.AppearanceItemCaption.ForeColor = Color.Empty;
                    break;
            }

            _currentBarStatus = state;
        }

        #endregion

        #region PrivateMethods

        /// <summary>
        /// Sets the reading value.
        /// </summary>
        /// <param name="value"></param>
        private void SetReadingValue(float value)
        {
            digitalGaugeReading.Text = value.ToString();
            linearScaleComponentReading.Value = value;
            linearScaleLevelComponentReading.Shader = new StyleShader
            {
                StyleColor1 = VitalBaseForm.GetRangeColor((int)value),
                StyleColor2 = Color.White
            };

            if (value == 0)
            {
                SetReadingStatusBarMode(TestBarStateEnum.HideYesNo, string.Empty, 0);
            }
            else if(_currentBarStatus != TestBarStateEnum.Reading)
            {
                SetReadingStatusBarMode( ShowYesNoLabel ? (
                        CrossLayersSharedLogic.IsAcceptableReading((int)ReadingValue)
                            ? TestBarStateEnum.Yes
                            : TestBarStateEnum.No ): TestBarStateEnum.HideYesNo, string.Empty, 0);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handle the load event logic
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void XtraUserControlReadingGauge_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(XtraUserControlReadingGauge_Load), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!DesignMode)
                {
                    SetReadingStatusBarMode(TestBarStateEnum.Ready, string.Empty, 0);
                    SetReadingStatusBarMode(TestBarStateEnum.HideYesNo, string.Empty, 0);
                }

                layoutControlItemLocationGauge.Visibility = (ShowLocationGauge)
                                                                ? LayoutVisibility.Always
                                                                : LayoutVisibility.Never;
            }
        }

        #endregion
    }
}
