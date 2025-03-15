using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Vital.Business.Shared.AmplitudeHelper
{
    public class Amplitude
    {
        #region PrivateMembers

        private static Amplitude _instance;

        private string _eventLogUrl;
        private string _apiKey;

        private readonly List<AmplitudeEvent> _eventsqQueue;
        private readonly object _eventsQueueLocker = new object();

        private long _sessionId;
        private string _country;
        private string _language;
        private string _osName;
        private string _osVersion;
        private string _platformVersion;
        private string _userId;
        private string _deviceId;

        private bool _isInstanceReady;

        private bool _postEventsImmediately;
        private bool _useAsBackgroundProcess;

        #endregion

        #region PrivateConstants

        private const string SpecialEventApiProperty = "special";
        private const string StartSessionEventType = "session_start";
        private const string EndSessionEventType = "session_end";

        #endregion

        #region PublicProperties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static Amplitude Instance
        {
            get { return _instance ?? (_instance = new Amplitude()); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Amplitude SDK constructor.
        /// </summary>
        private Amplitude()
        {
            _eventsqQueue = new List<AmplitudeEvent>();
        }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Setups the instance.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="userId">The user Id.</param>
        /// <param name="deviceId">The device Id.</param>
        /// <param name="platformVersion">The platform version.</param>
        /// <param name="postEventsImmediately">Post the events immediately, true => deactivate bulk sending queue.</param>
        /// <param name="useAsBackgroundProcess">Use as background process so the operations will be enforced to exit when the main process [Application] exited.</param>
        public void Setup(string apiUrl, string apiKey, string userId, string deviceId, string platformVersion, bool postEventsImmediately = true, bool useAsBackgroundProcess = true)
        {
            try
            {
                _eventLogUrl = apiUrl;
                _apiKey = apiKey;
                _sessionId = CurrentTimeMillis();
                _userId = userId;
                _deviceId = deviceId;
                _platformVersion = platformVersion;
                _country = GetCountry();
                _language = GetLanguage();
                _osName = GetOsName();
                _osVersion = GetOsArchitecture();
                _postEventsImmediately = postEventsImmediately;
                _useAsBackgroundProcess = useAsBackgroundProcess;

                if (!_isInstanceReady && !_postEventsImmediately)
                    StartPostEventsThread();

                _isInstanceReady = true;

            }
            catch
            {

            }
        }

        /// <summary>
        /// Setups and starts the user session.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="userId">The user Id.</param>
        /// <param name="deviceId">The device Id.</param>
        /// <param name="platformVersion">The platform version.</param>
        /// <param name="postEventsImmediately">Post the events immediately, true => deactivate bulk sending queue.</param>
        /// <param name="useAsBackgroundProcess">Use as background process so the operations will be enforced to exit when the main process [Application] exited.</param>
        public void StartSession(string apiUrl, string apiKey, string userId, string deviceId, string platformVersion, bool postEventsImmediately = true, bool useAsBackgroundProcess = false)
        {
            try
            {
                Setup(apiUrl, apiKey, userId, deviceId, platformVersion, postEventsImmediately, useAsBackgroundProcess);
                StartSession();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Starts the user session.
        /// </summary>
        public bool StartSession()
        {
            try
            {
                if (!_isInstanceReady)
                    return false;

                var apiProperties = new Dictionary<string, object> { { SpecialEventApiProperty, StartSessionEventType } };
                Log(new AmplitudeEvent { EventType = StartSessionEventType, ApiProperties = apiProperties });
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Ends the user session.
        /// </summary>
        public bool EndSession()
        {
            try
            {
                if (_sessionId <= 0)
                    return false;

                var apiProperties = new Dictionary<string, object> { { SpecialEventApiProperty, EndSessionEventType } };
                Log(new AmplitudeEvent { EventType = EndSessionEventType, ApiProperties = apiProperties });
                _sessionId = 0;
                EnforceSendAllEvents();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Logs an event.
        /// </summary>
        /// <param name="logEvent">The event.</param>
        public void Log(AmplitudeEvent logEvent)
        {
            lock (_eventsQueueLocker)
            {
                logEvent.SessionId = _sessionId;

                if (_postEventsImmediately)
                {
                    StartPostSingleEventThread(logEvent);
                }
                else
                {
                    _eventsqQueue.Add(logEvent);
                }
            }
        }

        #endregion

        #region PrivateWorkers

        /// <summary>
        /// Starts the post events thread.
        /// </summary>
        private void StartPostEventsThread()
        {
            try
            {
                var thread = new Thread(act =>
                {
                    while (_sessionId > 0)
                    {
                        PostEventsThreadWorker();
                        Thread.Sleep(5000);
                    }
                }) { IsBackground = _useAsBackgroundProcess };

                thread.Start();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Starts the post events thread.
        /// </summary>
        private void StartPostSingleEventThread(AmplitudeEvent ampEvent)
        {
            try
            {
                var thread = new Thread(act => PostEvents(new List<AmplitudeEvent> { ampEvent })) { IsBackground = _useAsBackgroundProcess };
                thread.Start();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Enforce to send all the events.
        /// </summary>
        private void EnforceSendAllEvents()
        {
            try
            {
                var thread = new Thread(act => PostEventsThreadWorker()) { IsBackground = _useAsBackgroundProcess };
                thread.Start();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Post events thread worker.
        /// </summary>
        private void PostEventsThreadWorker()
        {
            try
            {
                if (!_isInstanceReady)
                    return;

                lock (_eventsQueueLocker)
                {
                    if (_eventsqQueue.Count <= 0)
                        return;

                    PostEvents(_eventsqQueue);
                    _eventsqQueue.Clear();
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Post events worker.
        /// </summary>
        /// <param name="ampEvents">The events.</param>
        private void PostEvents(IEnumerable<AmplitudeEvent> ampEvents)
        {
            try
            {
                var events = ampEvents.Select(evt => new Dictionary<string, object>
                {
                    {"event_type", evt.EventType},
                    {"event_properties", evt.EventProperties ?? new Dictionary<string, object>()},
                    {"api_properties", evt.ApiProperties ?? new Dictionary<string, object>()},
                    {"user_properties", evt.UserProperties ?? new Dictionary<string, object>()},
                    {"timestamp", GetTimeMillis(evt.DateTime ?? DateTime.UtcNow).ToString()},
                    {"app_version", _platformVersion},
                    {"user_id", _userId},
                    {"device_id", _deviceId},
                    {"session_id", evt.SessionId},
                    {"os_name", _osName},
                    {"os_version", _osVersion},
                    {"country", _country},
                    {"language", _language}
                }).ToList();

                using (var client = new WebClient())
                {
                    client.UploadValues(_eventLogUrl, new NameValueCollection()
                    {
                        {"api_key", _apiKey},
                        {"event", JsonConvert.SerializeObject(events)},
                    });
                }
            }
            catch
            {
                //Suppress exceptions to prevent showing it to user in case something wrong happened
            }


        }

        /// <summary>
        /// Gets the current timestamp.
        /// </summary>
        /// <returns></returns>
        private static long CurrentTimeMillis()
        {
            return GetTimeMillis(DateTime.UtcNow);
        }

        /// <summary>
        /// Gets the timestamp based on dateTime.
        /// </summary>
        /// <param name="utcDateTime">The dateTime in UTC.</param>
        /// <returns></returns>
        private static long GetTimeMillis(DateTime utcDateTime)
        {
            return (long)(utcDateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        /// <summary>
        /// Gets the OS name.
        /// </summary>
        /// <returns></returns>
        private static string GetOsName()
        {
            try
            {
                var key = Microsoft.Win32.Registry.LocalMachine;
                var skey = key.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion");
                return skey == null ? string.Empty : skey.GetValue("ProductName").ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the OS Architecture [x65 or x64].
        /// </summary>
        /// <returns></returns>
        private static string GetOsArchitecture()
        {
            try
            {
                return Environment.Is64BitOperatingSystem ? "x64" : "x86";
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the user language.
        /// </summary>
        /// <returns></returns>
        private static string GetLanguage()
        {
            try
            {
                return CultureInfo.CurrentCulture.NativeName;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the user country.
        /// </summary>
        /// <returns></returns>
        private static string GetCountry()
        {
            try
            {
                return new RegionInfo(CultureInfo.CurrentCulture.LCID).NativeName;
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
