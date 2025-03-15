using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraGauges.Core.Model;
using DevExpress.XtraGauges.Win.Base;
using DevExpress.XtraLayout;
using Vital.Business.Shared.DomainObjects.AppInfos;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;
using Vital.UI.UI_Components.User_Controls.Modules.BaseModules;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlShippingTimeBar : XtraUserControlBaseSettingsBar
    {
        #region PrivateMemebers

        private string _shippingTimeZoneId;
        private DateTime _shippingOpenAtTime;
        private DateTime _shippingCloseAtTime;
        private string _localTimeZoneLabel;
        private string _shippingTimeZoneLabel;
        private List<DayOfWeek> _shippingWeekEndDays;

        #endregion

        #region Constructors

        public XtraUserControlShippingTimeBar()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// gets if editor active.
        /// </summary>
        public bool IsEditorActive
        {
            get { return spinEditRemindBeforeShippingCloseHours.IsEditorActive; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update the options.
        /// </summary>
        public void UpdateOptions()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.UpdatingPrintingSetting);

            SetBinding();

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Set controls binding.
        /// </summary>
        protected override void SetBinding()
        {
            checkEditRemindBeforeShippingClose.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.ShippingOrderSettings, SettingKeys.RemindBeforeShippingClose);
            checkEditRemindBeforeShippingClose.Tag = SettingKeys.RemindBeforeShippingClose;
            checkEditRemindToOrderBeforeClosingTest.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.ShippingOrderSettings, SettingKeys.RemindToOrderBeforeClosingTest);
            checkEditRemindToOrderBeforeClosingTest.Tag = SettingKeys.RemindToOrderBeforeClosingTest;
            checkEditRemindWhenShippingClose.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.ShippingOrderSettings, SettingKeys.RemindWhenShippingClose);
            checkEditRemindWhenShippingClose.Tag = SettingKeys.RemindWhenShippingClose;

            int hoursBeforeRemind;
            var hoursBeforeRemindStng = UiHelperClass.GetSetting(CachableDataEnum.ShippingOrderSettings, SettingKeys.RemindBeforeShippingCloseHours);

            if (hoursBeforeRemindStng != null && int.TryParse(hoursBeforeRemindStng.Value.ToString(), out hoursBeforeRemind))
            {
                spinEditRemindBeforeShippingCloseHours.Value = hoursBeforeRemind;
                spinEditRemindBeforeShippingCloseHours.Tag = SettingKeys.RemindBeforeShippingCloseHours;
            }

            SetupTimeingSettings();
            UpdateControlsStatus();
            SetupAlerts();
           
        }

        /// <summary>
        /// Setup the shipping alerts.
        /// </summary>
        private void SetupAlerts()
        {
            AlertsHelper.Instance.RemoveSchedule(AlertKey.ShippingClosesAfter);
            AlertsHelper.Instance.RemoveSchedule(AlertKey.ShippingCloses);

            var localTime = DateTime.Now;
            var shippingZone = TimeZoneInfo.FindSystemTimeZoneById(_shippingTimeZoneId);
            var shippingTime = TimeZoneInfo.ConvertTime(localTime, shippingZone);

            if (checkEditRemindBeforeShippingClose.Checked && !_shippingWeekEndDays.Contains(shippingTime.DayOfWeek))
            {
                var remindBefore = spinEditRemindBeforeShippingCloseHours.Value;
                var alertTime = TimeZoneInfo.ConvertTime(_shippingCloseAtTime, shippingZone, TimeZoneInfo.Local).Subtract(new TimeSpan((int)remindBefore, 0, 0));
                AlertsHelper.Instance.Schedule(FindForm(), StaticKeys.ShippingAlertTitle, string.Format(StaticKeys.ShippingClosesAfterAlert, remindBefore, remindBefore > 1 ? "s": string.Empty), alertTime, AlertKey.ShippingClosesAfter, 0, Resources.BellBlue);
            }

            if (checkEditRemindWhenShippingClose.Checked && !_shippingWeekEndDays.Contains(shippingTime.DayOfWeek))
            {
                var alertTime = TimeZoneInfo.ConvertTime(_shippingCloseAtTime, shippingZone, TimeZoneInfo.Local);
                AlertsHelper.Instance.Schedule(FindForm(), StaticKeys.ShippingAlertTitle, StaticKeys.ShippingClosesAlert, alertTime, AlertKey.ShippingCloses, 0, Resources.BellBlue);
            }
            
        }

        /// <summary>
        /// Setup time(clock settings)
        /// </summary>
        private void SetupTimeingSettings()
        {
            var shippingAppInfo = CacheHelper.SetOrGetCachableData(CachableDataEnum.ShippingOrdersTimeInfo) as BindingList<AppInfo>;

            if(shippingAppInfo == null)
                return;

            var shippingTimeZoneIdInof = shippingAppInfo.FirstOrDefault(s => s.Property.Equals(EnumNameResolver.Resolve(AppInfoKeys.ShippingTimeZoneId)));
            var shippingCloseAt = shippingAppInfo.FirstOrDefault(s => s.Property.Equals(EnumNameResolver.Resolve(AppInfoKeys.ShippingCloseAt)));
            var shippingOpenAt = shippingAppInfo.FirstOrDefault(s => s.Property.Equals(EnumNameResolver.Resolve(AppInfoKeys.ShippingOpenAt)));
            var shippingTimeZoneNameInfo = shippingAppInfo.FirstOrDefault(s => s.Property.Equals(EnumNameResolver.Resolve(AppInfoKeys.ShippingTimeZoneDisplayName)));
            var shippingWeekEndDays = shippingAppInfo.FirstOrDefault(s => s.Property.Equals(EnumNameResolver.Resolve(AppInfoKeys.ShippingWeekEndDays)));

            _shippingTimeZoneId = shippingTimeZoneIdInof != null ? shippingTimeZoneIdInof.Value : string.Empty;

            var localTimeZoneName = TimeZone.CurrentTimeZone.StandardName.Replace("Standard", "").Replace("Time", "").Trim();
            var shippingTimeZoneName = shippingTimeZoneNameInfo != null ? shippingTimeZoneNameInfo.Value : string.Empty;

            labelComponentShippingRegion.Text = shippingTimeZoneName;
            labelComponentLocalRegion.Text = localTimeZoneName;

            simpleLabelItemShippingLocation.Text = _shippingTimeZoneLabel = shippingTimeZoneName + " {0}";
            simpleLabelItemLocalLocation.Text = _localTimeZoneLabel = localTimeZoneName + " {0}";


            DateTime shippingCloseAtTime;
            if(shippingCloseAt != null &&  DateTime.TryParse(shippingCloseAt.Value, out shippingCloseAtTime))
            {
                _shippingCloseAtTime = shippingCloseAtTime;
            }

            DateTime shippingOpenAtTime;
            if (shippingOpenAt != null && DateTime.TryParse(shippingOpenAt.Value, out shippingOpenAtTime))
            {
                _shippingOpenAtTime = shippingOpenAtTime;
            }

            //Analyze for weekend days.
            _shippingWeekEndDays = new List<DayOfWeek>();
            if (shippingWeekEndDays != null)
            {
                var shippingWeekendDaysArray = shippingWeekEndDays.Value.Split('|');
                foreach (var weekendDay in shippingWeekendDaysArray)
                {
                    _shippingWeekEndDays.Add(EnumNameResolver.LookupAsEnum<DayOfWeek>(weekendDay));
                }
            }

            timerClock.Enabled = true;

        }

        /// <summary>
        /// Update clock with the passed parameters.
        /// </summary>
        private static void UpdateClock(DateTime dt, IArcScale hScale, IArcScale mScale, IArcScale sScale, LabelComponent apmLabel, SimpleLabelItem labelLocation, string zoneName, LabelComponent labelDayOfWeek)
        {
            var isAm = dt.Hour < 12;
            var hour = isAm ? dt.Hour : dt.Hour - 12;
            var min = dt.Minute;
            var sec = dt.Second;
            hScale.Value = hour + min / 60.0f;
            mScale.Value = (min + sec / 60.0f) / 5f;
            sScale.Value = sec / 5.0f;
            apmLabel.Text = isAm ? StaticKeys.TimeAmText : StaticKeys.TimePmText;
            labelLocation.Text = string.Format(zoneName, apmLabel.Text);
            labelDayOfWeek.Text = dt.DayOfWeek.ToString();
        }

        /// <summary>
        /// Update the shipping status text based on passed time
        /// </summary>
        private void UpdateShippingStatusText(DateTime shippingTime)
        {
            if (_shippingWeekEndDays.Contains(shippingTime.DayOfWeek))
            {
                var localTime = DateTime.Now;
                if (_shippingWeekEndDays.Contains(localTime.DayOfWeek))
                {
                    labelControlComingAction.Text = string.Format(StaticKeys.ShippingActionWeekend,
                        _shippingWeekEndDays.Last().NextDayOfWeek());
                }
                else
                {
                    var timeReminded = _shippingOpenAtTime.TimeDuration(shippingTime);
                    labelControlComingAction.Text = string.Format(StaticKeys.ShippingActionIn,
                        StaticKeys.ShippingActionOpen, timeReminded, timeReminded.GetPeriodName());
                }
            }
            else if (shippingTime < _shippingCloseAtTime && shippingTime > _shippingOpenAtTime)
            {

                var timeReminded = _shippingCloseAtTime.TimeDuration(shippingTime);
                labelControlComingAction.Text = string.Format(StaticKeys.ShippingActionIn,
                    StaticKeys.ShippingActionClose, timeReminded, timeReminded.GetPeriodName());

            }
            else
            {
                if (_shippingWeekEndDays.Contains(shippingTime.NextDayOfWeek()) && shippingTime.TimeCompareTo(_shippingOpenAtTime) > 0 && shippingTime.TimeCompareTo(_shippingCloseAtTime) > 0)//shippingTime > _shippingOpenAtTime)
                {
                    labelControlComingAction.Text = string.Format(StaticKeys.ShippingActionWeekend,
                        _shippingWeekEndDays.Last().NextDayOfWeek());
                }
                else
                {
                    var timeReminded = _shippingOpenAtTime.TimeDuration(shippingTime);
                    labelControlComingAction.Text = string.Format(StaticKeys.ShippingActionIn,
                        StaticKeys.ShippingActionOpen, timeReminded, timeReminded.GetPeriodName());
                }
            }
        }

        /// <summary>
        /// Update control status ( Enabled || Disabled )
        /// </summary>
        private void UpdateControlsStatus()
        {
            spinEditRemindBeforeShippingCloseHours.Enabled = checkEditRemindBeforeShippingClose.Checked;
        }

        #endregion

        #region Events

        /// <summary>
        /// Handle timer ticks to update clocks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerClock_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsHandleCreated && InvokeRequired)
                {
                    try
                    {
                        if (IsDisposed) return;
                        Invoke(new EventHandler(timerClock_Tick), sender, e);
                    }
                    catch
                    {
                        // Nothing to do, form had been disposed.
                    }
                }
                else
                {
                    var localTime = DateTime.Now;
                    UpdateClock(localTime, circularGaugeLocalClock.Scales[0], circularGaugeLocalClock.Scales[1], circularGaugeLocalClock.Scales[2], labelComponentLocalApm, simpleLabelItemLocalLocation, _localTimeZoneLabel, labelComponentLocalDayOfWeek);

                    var shippingZone = TimeZoneInfo.FindSystemTimeZoneById(_shippingTimeZoneId);
                    var shippingTime = TimeZoneInfo.ConvertTime(localTime, shippingZone);
                    UpdateShippingStatusText(shippingTime);
                    UpdateClock(shippingTime, circularGaugeShippingClock.Scales[0], circularGaugeShippingClock.Scales[1], circularGaugeShippingClock.Scales[2], labelComponentShippingApm, simpleLabelItemShippingLocation, _shippingTimeZoneLabel, labelComponentShippingDayOfWeek);
                }
            }
            catch
            {
                //This catch added to be in the safe side if any exception happened while updating the clocks.
            }
        }

        /// <summary>
        /// Handel the check change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEdit_CheckedChanged(object sender, System.EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(checkEdit_CheckedChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (IsInitialized)
                {
                    UiHelperClass.UpdateCheckSettingValue(CachableDataEnum.ShippingOrderSettings, sender as CheckEdit, SettingsManagerObject);
                    UpdateControlsStatus();
                    SetupAlerts();
                }
            }
        }

        /// <summary>
        /// Handle changing for spin edit to save changes.
        /// </summary>
        private void spinEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new DevExpress.XtraEditors.Controls.ChangingEventHandler(spinEdit_EditValueChanging), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsInitialized) 
                    return;

                var spinEdit = sender as SpinEdit;
                if (spinEdit == null || spinEdit.Tag == null)
                    return;

                UiHelperClass.SaveChange(CachableDataEnum.ShippingOrderSettings, (SettingKeys)(spinEdit.Tag), e.NewValue,SettingsManagerObject);
            }
        }

        /// <summary>
        /// Handle changing for spin edit to refresh alerts
        /// </summary>
        private void spinEditRemindBeforeShippingCloseHours_EditValueChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(spinEditRemindBeforeShippingCloseHours_EditValueChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsInitialized)
                    return;

                SetupAlerts();
            }
        }

        #endregion

    }
}
