using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Vital.Business.Repositories.DatabaseRepositories.Lookups;
using Vital.Business.Repositories.DatabaseRepositories.Settings;
using Vital.Business.Repositories.HardwareRepositories.CsaEmdUnit;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects.Hardware;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class CsaEmdUnitManager : BaseManager
    {
        #region Private Variables

        private readonly ICsaEmdUnitRepository _csaEmdUnitRepository;
        private readonly ISettingRepository _settingRepository;
        private int _yesLookupId;
        private bool _isImprinting;

        #endregion

        #region Singleton Code

        private static CsaEmdUnitManager _instance;

        /// <summary>
        /// Returns the instance of the CsaEmdUnitManager.
        /// </summary>
        public static CsaEmdUnitManager Instance
        {
            get { return _instance ?? (_instance = new CsaEmdUnitManager()); }
        }

        #endregion
        
        #region Constructors

        /// <summary>
        /// CsaEmdUnitManager Constructor.
        /// </summary>
        public CsaEmdUnitManager()
        {
            _csaEmdUnitRepository = new CsaEmdUnitHardwareRepository();

            _settingRepository = new SettingDatabaseRepository();

            _isImprinting = true;

            FillLookupIds();

            RefreshSettings();

        }

        #endregion

        #region Events

        /// <summary>
        /// Event throws when some reading ready for show.
        /// </summary>
        public event MeterValueChangedHandle MeterValueChanged;

        /// <summary>
        /// Event throws when the Resetting Finished
        /// </summary>
        public event OnResettingFinishedHandle ResettingFinished;

        /// <summary>
        /// Event throws when the device being disconnected.
        /// </summary>
        public event OnDisconnected Disconnected;

        /// <summary>
        /// Event throws when the device being connected.
        /// </summary>
        public event OnConnected Connected;

        /// <summary>
        /// Event throws when trying to connect some port number.
        /// </summary>
        public event OnDetecting Detecting;

        /// <summary>
        /// Event throws when the reading being done.
        /// </summary>
        public event OnReadingDone ReadingDone;

        /// <summary>
        /// Event throws when the reading point being released.
        /// </summary>
        public event OnReleased Released;
 

        #endregion

        #region Public Properties

        /// <summary>
        /// Get the is connection open value.
        /// </summary>
        public bool IsConnectionOpen
        {
            get
            {
                return _csaEmdUnitRepository.IsConnectionOpen;
            }
        }

        /// <summary>
        /// Get is reading opened value.
        /// </summary>
        public bool IsReadingOpened
        {
            get
            {
                return _csaEmdUnitRepository.IsReadingOpened;
            }
        }

        /// <summary>
        /// Get is reading on value.
        /// </summary>
        public bool IsReadingOn
        {
            get;
            private set; 
        }

        /// <summary>
        /// Get the is resetting value.
        /// </summary>
        public bool IsResetting
        {
            get
            {
                return _csaEmdUnitRepository.IsResetting;
            }
        }


        /// <summary>
        /// Gets or sets the Minimum Reading to Register value.
        /// </summary>
        public int MinimumReadingtoRegister
        {
            get
            {
                return _csaEmdUnitRepository == null ? 0 : _csaEmdUnitRepository.MinimumReadingtoRegister;
            }
            set
            {
                if (_csaEmdUnitRepository == null) return;
                _csaEmdUnitRepository.MinimumReadingtoRegister = value;
            }
        }

        /// <summary>
        /// Gets or sets the output buffer for broadcasting.
        /// </summary>
        public BindingList<int> OutPutBuffer
        {
            get; set;
        }

        /// <summary>
        /// Gets if the Csa Emd Unit Connected
        /// </summary>
        public bool IsCsaEmdUnitConnected
        {
            get
            {
                return _csaEmdUnitRepository.IsCsaEmdUnitConnected;
            }
        }

        /// <summary>
        /// The timeout (In milliseconds) of checking for CSA connection status.
        /// </summary>
        public int CsaDisconnectedTimeout
        {
            get
            {
                return _csaEmdUnitRepository.CsaDisconnectedTimeout;
            }
            private set
            {
                _csaEmdUnitRepository.CsaDisconnectedTimeout = value;
            }
        }

        /// <summary>
        /// Gets auto com port detection 
        /// </summary>
        public bool AutoComPortDetection
        {
            get { return _csaEmdUnitRepository.AutoComPortDetection; }
            set { _csaEmdUnitRepository.AutoComPortDetection = value; }
        }

        /// <summary>
        /// Minimum difference between readings to register a new one..
        /// </summary>
        public int ReadingStabilityRange
        {
            get 
            {
                return _csaEmdUnitRepository.ReadingStabilityRange; 
            }
            set
            {
                _csaEmdUnitRepository.ReadingStabilityRange = value;
            }
        }

        /// <summary>
        /// Stability period (In milliseconds) before reading being done.
        /// </summary>
        public int ReadingStabilityTimeout
        {
            get
            {
                return _csaEmdUnitRepository.ReadingStabilityTimeout;
            }
            private set
            {
                _csaEmdUnitRepository.ReadingStabilityTimeout = value;
            }
        }

        /// <summary>
        /// The communication port number.
        /// </summary>
        public int ComPortNumber
        {
            get
            {
                return _csaEmdUnitRepository.ComPortNumber;
            }
            private set
            {
                _csaEmdUnitRepository.ComPortNumber = value;
            }
        }

        /// <summary>
        /// The communication port number.
        /// </summary>
        public BroadcastMethodologies BroadcastMethodology
        {
            get; private set;
        }


        /// <summary>
        /// Returns true if the csa unit has a reading.
        /// </summary>
        public bool HasReading
        {
            get
            {
                return _csaEmdUnitRepository.HasReading;
            }
        }

        /// <summary>
        /// Gets or sets the AutoPlayWaitingTime.
        /// </summary>
        public int EdsAutoPlayWaitingTime
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the AutoPlayWaitingTime.
        /// </summary>
        public int ItemTestingAutoPlayWaitingTime
        {
            get;
            private set;
        }


        /// <summary>
        /// Gets the BeepFrequency.
        /// </summary>
        public int BeepFrequency
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the BeepDuration.
        /// </summary>
        public int BeepDuration
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the NumberOfVitalForceItemsToJump.
        /// </summary>
        public int NumberOfVitalForceItemsToJump
        {
            get; 
            private set;
        }
        
        /// <summary>
        /// Gets setting broadcasting status.
        /// </summary>
        public bool IsBroadcastingOn
        {
            get; private set;
        }

        /// <summary>
        /// Gets broadcasting status.
        /// </summary>
        public bool IsBroadcasting
        {
            get
            {
                return _csaEmdUnitRepository.IsBroadcasting;
            }
        }

        /// <summary>
        /// Gets the IsImprinting value.
        /// </summary>
        public bool IsImprinting
        {
            get
            {
                //we can indicate if the imprinting process running or not using the IsBroadcasting flag, so if the broadcasting is running and status is imprinting so the imprinting not finished yet.
                return _isImprinting && IsBroadcasting;
            }
        }

        /// <summary>
        /// Gets the AutoOpenProductDosages setting value.
        /// </summary>
        public bool AutoOpenProductDosages 
        { 
            get; 
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Open the connection with the device. [ If the connection succeeded the process result will carry the true. Else, will throw an exception.]
        /// </summary>
        /// <returns></returns>
        public ProcessResult OpenConnection(SerialPortConnectionFilter connectionFilter)
        {
            Check.Argument.IsNotNull(() => connectionFilter);
            Check.Argument.IsNotNegative(connectionFilter.ComPortNumber, "Port Number should not be negative.");

            try
            {
                InitHardwareHandlers();

                var result = _csaEmdUnitRepository.OpenConnection(connectionFilter.ComPortNumber,
                                                                  connectionFilter.BaudRate,
                                                                  connectionFilter.DataBit, connectionFilter.Timeout,
                                                                  connectionFilter.Dtr, connectionFilter.Rts,
                                                                  connectionFilter.AutoComPortDetection);


                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Open the connection with the device. [ If the connection succeeded the process result will carry the true. Else, will throw an exception.]
        /// </summary>
        /// <returns></returns>
        public ProcessResult OpenConnection()
         {
             var filter = new SerialPortConnectionFilter(HardwareType.CSA)
                              {
                                  Timeout = 1500,
                                  ComPortNumber = ComPortNumber,
                                  AutoComPortDetection = AutoComPortDetection
                              };

             var isOpend = OpenConnection(filter);

             return isOpend;
         }

        /// <summary>
        /// Refresh current connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult RefreshConnection(SerailPortNumberFilter csaEmdUnitPortNumberFilter)
        {
            Check.Argument.IsNotNegative(csaEmdUnitPortNumberFilter.ComPortNumber, "Port Number should not be negative.");

            CloseConnection();

            ComPortNumber = csaEmdUnitPortNumberFilter.ComPortNumber;
            AutoComPortDetection = csaEmdUnitPortNumberFilter.AutoComPortDetection ?? false;

            return OpenConnection();

        }

        /// <summary>
        /// Refresh current connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult RefreshConnection()
        {
            return RefreshConnection(new SerailPortNumberFilter {ComPortNumber = ComPortNumber, AutoComPortDetection = AutoComPortDetection});
        }

        /// <summary>
        /// Close the connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult CloseConnection()
        {
            try
            {
                _csaEmdUnitRepository.CloseReading();

                var result = _csaEmdUnitRepository.CloseConnection();

                if (!result.IsSucceed) return result;

                _csaEmdUnitRepository.Disconnected -= _csaEmdUnitRepository_Disconnected;

                _csaEmdUnitRepository.Connected -= _csaEmdUnitRepository_Connected;

                _csaEmdUnitRepository.Detecting -= _csaEmdUnitRepository_Detecting;

                _csaEmdUnitRepository.Released -= _csaEmdUnitRepository_Released;

                ForceRemoveUserHandlers();

                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Cancel the auto connection detection operation.
        /// </summary>
        /// <returns></returns>
        public ProcessResult CancelAutoDetection()
        {
            try
            {
                return _csaEmdUnitRepository.CancelAutoDetection();
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Gets the com ports.
        /// </summary>
        /// <returns></returns>
        public BindingList<ComPortInfo> GetComPorts()
        {
            try
            {
                var autoCom = new ComPortInfo {Id = 0, Name = StaticKeys.AutoDetectionText};
                var result = _csaEmdUnitRepository.GetComPorts();
                result.Insert(0, autoCom);
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Activate the connection.
        /// </summary>
        /// <param name="onReleasedHandler"></param>
        /// <param name="onReadingDone"></param>
        /// <param name="meterValueChangedHandle"></param>
        public void ActivateConnection(OnReleased onReleasedHandler, OnReadingDone onReadingDone, MeterValueChangedHandle meterValueChangedHandle)
        {
            try
            {
                OpenConnection();
                Released += onReleasedHandler;
                ReadingDone += onReadingDone;
                MeterValueChanged += meterValueChangedHandle;
            }
            catch
            {
                
            }
        }

        /// <summary>
        /// Dispose the current CSA connection. [ Not closing ]
        /// </summary>
        public void DisposeConnection(OnReleased onReleasedHandler, OnReadingDone onReadingDone, MeterValueChangedHandle meterValueChangedHandle)
        {
            try
            {
                StopReading();
                Released -= onReleasedHandler;
                ReadingDone -= onReadingDone;
                MeterValueChanged -= meterValueChangedHandle;
            }
            catch
            {

            }
        }

        /// <summary>
        /// Reset the connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult StartResetting()
        {
            try
            {
                var result = _csaEmdUnitRepository.StartResetting();

                if (!result.IsSucceed) return result;

                _csaEmdUnitRepository.ResettingFinished -= _csaEmdUnitRepository_ResettingFinished;

                _csaEmdUnitRepository.ResettingFinished += _csaEmdUnitRepository_ResettingFinished;

                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Stop the in progress resetting.
        /// </summary>
        /// <returns></returns>
        public ProcessResult StopResetting()
        {
            try
            {
                var result = _csaEmdUnitRepository.StopResetting();

                if (!result.IsSucceed) return result;

                _csaEmdUnitRepository.ResettingFinished -= _csaEmdUnitRepository_ResettingFinished;

                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Starts the reading from the device. 
        /// </summary>
        /// <returns></returns>
        public void StartReading()
        {
            try
            {
                if(IsReadingOn) return;

                //_csaEmdUnitRepository.MeterValueChanged -= _csaEmdUnitRepository_MeterValueChanged;

                _csaEmdUnitRepository.MeterValueChanged += _csaEmdUnitRepository_MeterValueChanged;

                //_csaEmdUnitRepository.ReadingDone -= _csaEmdUnitRepository_ReadingDone;

                _csaEmdUnitRepository.ReadingDone += _csaEmdUnitRepository_ReadingDone;

                IsReadingOn = true;

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }


        /// <summary>
        /// Stops the reading from the device. 
        /// </summary>
        /// <returns></returns>
        public void StopReading()
        {
            try
            {
                if (!IsReadingOn) return;

                _csaEmdUnitRepository.MeterValueChanged -= _csaEmdUnitRepository_MeterValueChanged;
                _csaEmdUnitRepository.ReadingDone -= _csaEmdUnitRepository_ReadingDone;

                IsReadingOn = false;

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Force to remove connection handlers.
        /// </summary>
        public void ForceRemoveUserHandlers()
        {
            var releasedDelg = Released as Delegate;
            Released = Delegate.RemoveAll(releasedDelg, releasedDelg) as OnReleased;

            var readingDoneDelg = ReadingDone as Delegate;
            ReadingDone = Delegate.RemoveAll(readingDoneDelg, readingDoneDelg) as OnReadingDone;

            var meterValueChangedDelg = MeterValueChanged as Delegate;
            MeterValueChanged = Delegate.RemoveAll(meterValueChangedDelg, meterValueChangedDelg) as MeterValueChangedHandle;
        }

        /// <summary>
        /// Clear the Instance Caches and readings.
        /// </summary>
        /// <returns></returns>
        public void Clear()
        {
            try
            {
                _csaEmdUnitRepository.Clear();
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                //throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Broadcast a message.
        /// </summary>
        /// <param name="items">List of items to broadcast.</param>
        /// <param name="isImprinting">Is imprinting request.</param>
        /// <param name="flush">Flush the buffer of broadcasting before add items to broadcast.</param>
        /// <returns></returns>
        public ProcessResult Broadcast(List<Item> items, bool isImprinting = false, bool flush = true)
        {
            try
            {
                if(!IsCsaEmdUnitConnected)
                {
                    FlushBroadcastBuffer();
                    _isImprinting = false;
                    return ProcessResult.Failed;
                }

                if (!isImprinting && (!IsBroadcastingOn || IsImprinting)) 
                    return ProcessResult.Failed;

                //Imprinting had been finished.
                _isImprinting = isImprinting;

                List<string> messages;

                items = items.Where(i => i.Properties == null || !i.Properties.HasProperty(PropertiesEnum.DoNotEnergize, _yesLookupId.ToString())).ToList();

                if (flush)
                    FlushBroadcastBuffer();

                switch (BroadcastMethodology)
                {
                    case BroadcastMethodologies.BlankString:
                        items.ForEach(i => _csaEmdUnitRepository.Broadcast(i.Name));
                        break;
                    case BroadcastMethodologies.VitalDegreeOfAffect:
                        messages = GenerateBroadcastMessage(items, BroadcastMethodologies.VitalDegreeOfAffect);
                        messages.ForEach(msg => _csaEmdUnitRepository.Broadcast(msg));
                        break;

                    case BroadcastMethodologies.VitalIngredientHashCode:
                        messages = GenerateBroadcastMessage(items, BroadcastMethodologies.VitalMessageIngredients);
                        messages.ForEach(msg => _csaEmdUnitRepository.Broadcast(msg.GetHashCode().ToString()));
                        break;

                    case BroadcastMethodologies.VitalMessage:
                        items.ForEach(i => _csaEmdUnitRepository.Broadcast(i.Id.ToString()));
                        break;

                    case BroadcastMethodologies.VitalMessageIngredients:
                       messages = GenerateBroadcastMessage(items, BroadcastMethodologies.VitalMessageIngredients);
                       messages.ForEach(msg => _csaEmdUnitRepository.Broadcast(msg));
                        break;
  
                    case BroadcastMethodologies.VitalPointHashCode:
                        messages = GenerateBroadcastMessage(items, BroadcastMethodologies.VitalDegreeOfAffect);
                        messages.ForEach(msg => _csaEmdUnitRepository.Broadcast(msg.GetHashCode().ToString()));
                        break;
                }

                return ProcessResult.Succeed;
            }
            catch 
            {
                return ProcessResult.Failed;
            }
        }

        public ProcessResult FlushBroadcastBuffer()
        {
            try
            {
                return _csaEmdUnitRepository.FlushBroadcastBuffer();
            }
            catch
            {
                return ProcessResult.Failed;

            }
        }

        /// <summary>
        /// Refresh the settings values.
        /// </summary>
        public void RefreshSettings()
        {
            CloseConnection();

            var lookupsRepository = new LookupDatabaseRepository();

            var onLokkup =
                    lookupsRepository.LoadLookups(Enum.GetName(typeof (LookupTypes), LookupTypes.OnOff),
                                                  Enum.GetName(typeof (OnOffEnum), OnOffEnum.On)).FirstOrDefault();

            RefreshHwProfileSettings();
            RefreshPortNumberSettings();
            EdsAutoPlayWaitingTime = IntegerSettingsLoader(_settingRepository, SettingKeys.EdsNextPointNavigationTime);
            BeepDuration = IntegerSettingsLoader(_settingRepository, SettingKeys.Duration);
            BeepFrequency = IntegerSettingsLoader(_settingRepository, SettingKeys.Frequency);
            ItemTestingAutoPlayWaitingTime = IntegerSettingsLoader(_settingRepository, SettingKeys.ItemTestingNavigationTime);
            NumberOfVitalForceItemsToJump = IntegerSettingsLoader(_settingRepository, SettingKeys.NumberOfVitalForceItemsInGroup);
            AutoOpenProductDosages = IntegerSettingsLoader(_settingRepository, SettingKeys.AutoOpenProductDosages) == _yesLookupId;

            // Get the setting fro the broadcastMethodology and convert the lookup value to broadcastMethodologies enum.
            var broadcastMethodologysettingValue = IntegerSettingsLoader(_settingRepository, SettingKeys.BroadcastMethodology);

            var broadcastMethodologyLookup = lookupsRepository.LoadLockupById(broadcastMethodologysettingValue);

            IsBroadcastingOn = IntegerSettingsLoader(_settingRepository, SettingKeys.BroadcastingStatus) == (onLokkup != null ? onLokkup.Id : 0);

            BroadcastMethodology = EnumNameResolver.LookupAsEnum<BroadcastMethodologies>(broadcastMethodologyLookup.Value);

        }

        /// <summary>
        /// Refresh port number settings;
        /// </summary>
        public void RefreshPortNumberSettings()
        {
            CloseConnection();

            ComPortNumber = IntegerSettingsLoader(_settingRepository, SettingKeys.CommunicationsPort);
            AutoComPortDetection = ComPortNumber == 0;
        }

        /// <summary>
        /// Refresh hw settings that related to profile.
        /// </summary>
        public void RefreshHwProfileSettings()
        {
            var defaultMinReadingStng = IntegerSettingsLoader(_settingRepository, SettingKeys.MinimumReadingtoRegister);
            var defaultStabilityRangeStng = IntegerSettingsLoader(_settingRepository, SettingKeys.ReadingStabilityRange);
            var defaultStabilityTimeoutStng = IntegerSettingsLoader(_settingRepository, SettingKeys.ReadingStabilityTimeout);
            var defaultCsaDisconnectedTimeoutStng = IntegerSettingsLoader(_settingRepository, SettingKeys.CsaDisconnectedTimeout);

            CsaDisconnectedTimeout = defaultCsaDisconnectedTimeoutStng;
            ReadingStabilityTimeout = defaultStabilityTimeoutStng;
            ReadingStabilityRange = defaultStabilityRangeStng;
            MinimumReadingtoRegister = defaultMinReadingStng;
        }

        #endregion

        #region Private Helper

        /// <summary>
        /// Sets the Hw handlers.
        /// </summary>
        private void InitHardwareHandlers()
        {
            _csaEmdUnitRepository.Disconnected -= _csaEmdUnitRepository_Disconnected;
            _csaEmdUnitRepository.Disconnected += _csaEmdUnitRepository_Disconnected;

            _csaEmdUnitRepository.Connected -= _csaEmdUnitRepository_Connected;
            _csaEmdUnitRepository.Connected += _csaEmdUnitRepository_Connected;

            _csaEmdUnitRepository.Released -= _csaEmdUnitRepository_Released;
            _csaEmdUnitRepository.Released += _csaEmdUnitRepository_Released;

            _csaEmdUnitRepository.Detecting -= _csaEmdUnitRepository_Detecting;
            _csaEmdUnitRepository.Detecting += _csaEmdUnitRepository_Detecting;
        }


        /// <summary>
        /// Fills local lookup ids.
        /// </summary>
        private void FillLookupIds()
        {
            var lookupsRepository = new LookupDatabaseRepository();

            var yesLookup = lookupsRepository.LoadLookups(EnumNameResolver.Resolve(LookupTypes.YesNo),
                                                                           EnumNameResolver.Resolve(YesNoEnum.Yes)).FirstOrDefault();

            if(yesLookup == null)
                return;

            _yesLookupId = yesLookup.Id;
        }

        /// <summary>
        /// load an int setting.
        /// </summary>
        /// <param name="settingRepository"></param>
        /// <param name="settingKeys"></param>
        /// <returns></returns>
        private static int IntegerSettingsLoader(ISettingRepository settingRepository, SettingKeys settingKeys)
        {
            Check.Argument.IsNotNull(() => settingRepository);

            var setting = settingRepository.LoadSettingByKey(EnumNameResolver.Resolve(settingKeys));

            int settingValue = 0;

            if (setting == null) return settingValue;

            int.TryParse(setting.Value as string, out settingValue);

            return settingValue;
        }

        /// <summary>
        /// Generate The broadcast message depends on his supper type.
        /// </summary>
        private static List<string> GenerateBroadcastMessage(List<Item> items, BroadcastMethodologies superBroadcastType)
        {
            var messages = new List<string>();

            switch (superBroadcastType)
            {
                 //ToDo: Add real stuff as of below.
                //"effectedItemName=Degree;" // ex : TW-2ThyroidThymus=90;
                //ingredientItemName ex : Vitamin C;
                case BroadcastMethodologies.VitalDegreeOfAffect:
                    messages = items.Select(i => i.Name + "(Healing Message=DEGREE OF AFFECT;" + "?effectedItemName=Degree;?" + ")").ToList();
                    break;
                
                case BroadcastMethodologies.VitalMessageIngredients:
                    messages = items.Select(i => i.Name + "<INGREDIENTS>(" + "?ingredientItemName?" + ")").ToList();
                    break;
            }

            return messages;
        }

        /// <summary>
        /// Get com ports worker.
        /// </summary>
        /// <returns></returns>
        private BindingList<ComPortInfo> GetComPortsWorker()
        {
            return _csaEmdUnitRepository.GetComPorts();
        }

        #endregion

        #region Handlers

        /// <summary>
        /// ResettingFinished Handler.
        /// </summary>
        /// <param name="sender"></param>
        void _csaEmdUnitRepository_ResettingFinished(object sender)
        {
            if(ResettingFinished == null) return;
            ResettingFinished(sender);
        }

        /// <summary>
        /// MeterValueChanged Handler.
        /// </summary>
        void _csaEmdUnitRepository_MeterValueChanged(object sender, int reading, int min, int max)
        {
            if(MeterValueChanged == null) return;
            MeterValueChanged(sender, reading, min, max);
        }

        /// <summary>
        /// Disconnected Handler.
        /// </summary>
        /// <param name="sender"></param>
        void _csaEmdUnitRepository_Disconnected(object sender)
        {
            if (Disconnected == null) return;
            Disconnected(sender);
        }

        /// <summary>
        /// ReadingDone Handler.
        /// </summary>
        void _csaEmdUnitRepository_ReadingDone(object sender, int reading, int min, int max, int fall, int rais)
        {
            if (ReadingDone == null || reading <= 0) return;

            if (CrossLayersSharedLogic.IsAcceptableReading(reading))
                reading = 50;

            ReadingDone(sender, reading, min, max, fall, rais);

            if (BeepDuration > 0)
                Console.Beep(BeepFrequency, BeepDuration);

        }

        /// <summary>
        /// Connected Handler.
        /// </summary>
        /// <param name="sender"></param>
        void _csaEmdUnitRepository_Connected(object sender)
        {
            _csaEmdUnitRepository.OpenReading();

            if (Connected == null) 
                return;

            Connected(this);
            
        }

        /// <summary>
        /// Handel the trying to connect event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="comPortNumber"></param>
        void _csaEmdUnitRepository_Detecting(object sender, int comPortNumber)
        {
           
            if(Detecting == null)
                return;
            
            Detecting(this, comPortNumber);
        }

        /// <summary>
        /// Handel the event when tester release the csa tools from the patient body. 
        /// </summary>
        /// <param name="sender"></param>
        void _csaEmdUnitRepository_Released(object sender)
        {
            if (Released != null && !IsReadingOn)
            {
                Released(this);
                //_csaEmdUnitRepository.Released -= _csaEmdUnitRepository_Released;
            }

            //check why it was commented.
             //_csaEmdUnitRepository.ReadingDone -= _csaEmdUnitRepository_ReadingDone;

            
        }

        #endregion

        #region Event Handler Delegates

        /// <summary>
        /// Delegate for OnResettingFineshedHandle handler method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnResettingFinishedHandle(object sender);

        /// <summary>
        /// Delegate for MeterValueChangedEvent handler method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="reading">Last reading.</param>
        /// <param name="min">The Min value.</param>
        /// <param name="max">The Max value.</param>
        public delegate void MeterValueChangedHandle(object sender, int reading, int min, int max);

        /// <summary>
        /// Delegate for Disconnected handler method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnDisconnected(object sender);

        /// <summary>
        /// Delegate for connected handler method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnConnected(object sender);

        /// <summary>
        /// Delegate for OnReadingDone handler method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reading">Last reading.</param>
        /// <param name="min">The Min value.</param>
        /// <param name="max">The Max value.</param>
        /// <param name="fall">The Fall value.</param>
        /// <param name="rais">The Rais value.</param>
        public delegate void OnReadingDone(object sender, int reading, int min, int max, int fall, int rais);

        /// <summary>
        /// Delegate for OnReleased handle method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnReleased(object sender);

        /// <summary>
        /// Delegate for OnDetecting handle method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="comPortNumber"></param>
        public delegate void OnDetecting(object sender, int comPortNumber);


        #endregion
    }
}
