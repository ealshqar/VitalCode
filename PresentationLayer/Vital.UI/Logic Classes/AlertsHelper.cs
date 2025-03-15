using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using DevExpress.XtraBars.Alerter;
using Vital.Business.Managers;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;

namespace Vital.UI.Logic_Classes
{
    public class AlertsHelper
    {
        #region Singleton

        private static AlertsHelper _instance;

        /// <summary>
        /// Gets the AlertsHelper Instance.
        /// </summary>
        public static AlertsHelper Instance
        {
            get { return _instance ?? (_instance = new AlertsHelper()); }
        }

        #endregion

        #region PrivateMembers

        private readonly int _defaultDuration;
        private int _currentMinute;
        private readonly Timer _schedulingTimer;
        private readonly List<ScheduleAlert> _scheduledAlerts;
        private readonly List<AlertKey> _shownInCurrentMinute;

        #endregion

        #region Constructors

        private AlertsHelper()
        {
            _scheduledAlerts = new List<ScheduleAlert>();
            _shownInCurrentMinute = new List<AlertKey>();

            var settingsManager = new SettingsManager();

            var defaultDurationSetting = settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.DefaultAlertDuration) });

            var defaultDuration = 0;

            if (defaultDurationSetting == null || !int.TryParse(defaultDurationSetting.Value.ToString(), out defaultDuration))
                _defaultDuration = 180000;

            _defaultDuration = defaultDuration;

            _schedulingTimer = new Timer { Interval = 3000 };

            _currentMinute = DateTime.Now.Minute;

            _schedulingTimer.Tick += _schedulingTimer_Tick;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Shows alert based on passed parameters.
        /// </summary>
        public void Show(Form owner, string title, string message, string hoverMessage = null, int duration = 0, Image image = null)
        {
            var alertControl = new AlertControl
            {
                AutoFormDelay = duration == 0 ? _defaultDuration : duration,
                AllowHotTrack = !string.IsNullOrEmpty(hoverMessage),
                FormShowingEffect = AlertFormShowingEffect.SlideVertical,
                ShowPinButton = false
            };

            alertControl.BeforeFormShow += alertControl_BeforeFormShow;
            alertControl.Show(owner, new AlertInfo(title, message, hoverMessage, image));
            PlayAlertSound();
        }

        /// <summary>
        /// Shows alert based on passed parameters.
        /// </summary>
        public void Show(Form owner, string title, string message, int duration = 0, Image image = null)
        {
            Show(owner, title, message, null, duration, image);
        }

        /// <summary>
        /// Schedule alert to show at specific time based on passed parameters.
        /// </summary>
        public void Schedule(Form owner, string title, string message, string hoverMessage, DateTime showAt, AlertKey key, int duration = 0, Image image = null)
        {
            _schedulingTimer.Enabled = true;
            _scheduledAlerts.Add(new ScheduleAlert { Key = key, Owner = owner, Title = title, Message = message, HoverMessage = hoverMessage, Duration = duration, ShowAt = showAt, Image = image });
        }

        /// <summary>
        /// Schedule alert to show at specific time based on passed parameters.
        /// </summary>
        public void Schedule(Form owner, string title, string message, DateTime showAt, AlertKey key, int duration = 0, Image image = null)
        {
            Schedule(owner, title, message, null, showAt, key, duration, image);
        }

        /// <summary>
        /// Schedule alert to show at specific time based on passed parameters.
        /// </summary>
        public void Schdule(ScheduleAlert scheduleAlert)
        {
            Schedule(scheduleAlert.Owner, scheduleAlert.Title, scheduleAlert.Message, scheduleAlert.HoverMessage, scheduleAlert.ShowAt, scheduleAlert.Key, scheduleAlert.Duration, scheduleAlert.Image);
        }

        /// <summary>
        /// Remove alert to based on passed key.
        /// </summary>
        public void RemoveSchedule(AlertKey key)
        {
            lock (_scheduledAlerts)
            {
                var alert = _scheduledAlerts.FirstOrDefault(a => a.Key == key);

                if (alert == null)
                    return;

                _scheduledAlerts.Remove(alert);
            }
        }

        /// <summary>
        /// Play Hint Sound.
        /// </summary>
        private void PlayAlertSound()
        {
            try
            {
                var player = new SoundPlayer { Stream = Properties.Resources.alert };
                player.Play();
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handle the schedule timer to check for scheduled alerts.
        /// </summary>
        private void _schedulingTimer_Tick(object sender, EventArgs e)
        {
            lock (_scheduledAlerts)
            {
                if (DateTime.Now.Minute != _currentMinute)
                {
                    _currentMinute = DateTime.Now.Minute;
                    _shownInCurrentMinute.Clear();
                }

                var needsAlert = _scheduledAlerts.Where(a => a.ShowAt.Hour == DateTime.Now.Hour && a.ShowAt.Minute == DateTime.Now.Minute).ToList();

                foreach (var scheduleAlert in needsAlert)
                {
                    //Check if current scheduleAlert shown in this minute to avoid showing duplication.
                    if (!_shownInCurrentMinute.Contains(scheduleAlert.Key))
                    {
                        Show(scheduleAlert.Owner, scheduleAlert.Title, scheduleAlert.Message, scheduleAlert.HoverMessage, scheduleAlert.Duration, scheduleAlert.Image);
                        _shownInCurrentMinute.Add(scheduleAlert.Key);
                    }
                    _scheduledAlerts.Remove(scheduleAlert);
                }

            }

        }

        /// <summary>
        /// Handel showing alert for customization.
        /// </summary>
        private void alertControl_BeforeFormShow(object sender, AlertFormEventArgs e)
        {
            e.AlertForm.OpacityLevel = 1;
        }

        #endregion
    }
}
