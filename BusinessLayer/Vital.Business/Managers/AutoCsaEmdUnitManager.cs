using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Vital.Business.Repositories.DatabaseRepositories.Lookups;
using Vital.Business.Repositories.DatabaseRepositories.Settings;
using Vital.Business.Repositories.HardwareRepositories.AutoCsaEmdUnit;
using Vital.Business.Repositories.HardwareRepositories.Prototype;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects.AutoTestSource;
using Vital.Business.Shared.DomainObjects.Hardware;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class AutoCsaEmdUnitManager : BaseManager
    {
        #region Private Variables

        private readonly IAutoCsaEmdUnitHardwareRepository _autoCsaEmdUnitRepository;
        private readonly IPrototypeRepository _prototypeRepository;
        private readonly ISettingRepository _settingRepository;
        private int _yesLookupId;
        private bool _isImprinting;

        #endregion

        #region Singleton Code

        private static AutoCsaEmdUnitManager _instance;

        /// <summary>
        /// Returns the instance of the AutoCsaEmdUnitManager.
        /// </summary>
        public static AutoCsaEmdUnitManager Instance
        {
            get { return _instance ?? (_instance = new AutoCsaEmdUnitManager()); }
        }

        #endregion
        
        #region Constructors

        /// <summary>
        /// CsaEmdUnitManager Constructor.
        /// </summary>
        public AutoCsaEmdUnitManager()
        {
            _autoCsaEmdUnitRepository = new AutoCsaEmdUnitHardwareRepository();

            _prototypeRepository = new PrototypeHardwareRepository();

            _settingRepository = new SettingDatabaseRepository();

            _isImprinting = true;

            FillLookupIds();

            RefreshSettings();

            //Initialize CurrentReading to avoid possible object reference exception
            CurrentReading = new AutoCSAReadingModel(0, 0, 0);
        }

        #endregion

        #region Events

        #region CSA Events

        /// <summary>
        /// Event thrown when some reading ready for show.
        /// </summary>
        public event MeterValueChangedHandle MeterValueChanged;

        /// <summary>
        /// Event thrown when the CSA Resetting Finished
        /// </summary>
        public event OnCSAResettingFinishedHandle CSAResettingFinished;

        /// <summary>
        /// Event thrown when the CSA device being disconnected.
        /// </summary>
        public event OnCSADisconnected CSADisconnected;

        /// <summary>
        /// Event thrown when the CSA device being connected.
        /// </summary>
        public event OnCSAConnected CSAConnected;

        /// <summary>
        /// Event thrown when the reading being stabled.
        /// </summary>
        public event OnReadingStabled ReadingStabled;

        /// <summary>
        /// Event thrown when the reading is opened and the HW stopped sending readings.
        /// </summary>
        public event OnReadingStopped ReadingStopped;

        /// <summary>
        /// Event thrown when trying to connect CSA to a port number.
        /// </summary>
        public event OnCSADetecting CSADetecting;

        #endregion

        #region Prototype Events

        /// <summary>
        /// Event thrown when the Prototype device being disconnected.
        /// </summary>
        public event OnPrototypeDisconnected PrototypeDisconnected;

        /// <summary>
        /// Event thrown when the Prototype device being connected.
        /// </summary>
        public event OnPrototypeConnected PrototypeConnected;

        /// <summary>
        /// Event thrown when trying to connect Prototype to a port number.
        /// </summary>
        public event OnPrototypeDetecting PrototypeDetecting;

        /// <summary>
        /// Event thrown when any response came from the prototype.
        /// </summary>
        public event OnPrototypResponseReceived PrototypResponseReceived;

        #endregion
       
        #endregion

        #region Public Properties

        #region CSA Public Properties

        /// <summary>
        /// Gets the current reading mode.
        /// </summary>
        public AutoCSAReadingMode ReadingMode { get; private set; }

        /// <summary>
        /// Get the is CSA connection open value.
        /// </summary>
        public bool IsCSAConnectionOpen
        {
            get
            {
                return _autoCsaEmdUnitRepository.IsConnectionOpen;
            }
        }

        /// <summary>
        /// Get is reading opened value.
        /// </summary>
        public bool IsReadingOpened
        {
            get
            {
                return _autoCsaEmdUnitRepository.IsReadingOpened;
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
                return _autoCsaEmdUnitRepository.IsConnectionResetting;
            }
        }

        /// <summary>
        /// Gets or sets the Minimum Reading to Register value.
        /// </summary>
        public int MinimumReadingtoRegister
        {
            get
            {
                return _autoCsaEmdUnitRepository == null ? 0 : _autoCsaEmdUnitRepository.MinimumReadingtoRegister;
            }
            set
            {
                if (_autoCsaEmdUnitRepository == null) return;
                _autoCsaEmdUnitRepository.MinimumReadingtoRegister = value;
            }
        }

        /// <summary>
        /// Gets if the Csa Emd Unit Connected
        /// </summary>
        public bool IsCsaEmdUnitConnected
        {
            get
            {
                return _autoCsaEmdUnitRepository.IsCsaEmdUnitConnected;
            }
        }

        /// <summary>
        /// The timeout (In milliseconds) of checking for CSA connection status.
        /// </summary>
        public int CsaDisconnectedTimeout
        {
            get
            {
                return _autoCsaEmdUnitRepository.DisconnectedTimeout;
            }
            private set
            {
                _autoCsaEmdUnitRepository.DisconnectedTimeout = value;
            }
        }

        /// <summary>
        /// Gets auto com port detection 
        /// </summary>
        public bool CSAAutoComPortDetection
        {
            get { return _autoCsaEmdUnitRepository.AutoComPortDetection; }
            set { _autoCsaEmdUnitRepository.AutoComPortDetection = value; }
        }

        /// <summary>
        /// Minimum difference between readings to register a new one..
        /// </summary>
        public int ReadingStabilityRange
        {
            get
            {
                return _autoCsaEmdUnitRepository.ReadingStabilityRange;
            }
            set
            {
                _autoCsaEmdUnitRepository.ReadingStabilityRange = value;
            }
        }

        /// <summary>
        /// Stability period (In milliseconds) before reading being done.
        /// </summary>
        public int ReadingStabilityTimeout
        {
            get
            {
                return _autoCsaEmdUnitRepository.ReadingStabilityTimeout;
            }
            private set
            {
                _autoCsaEmdUnitRepository.ReadingStabilityTimeout = value;
            }
        }

        /// <summary>
        /// The CSA communication port number.
        /// </summary>
        public int CSAComPortNumber
        {
            get
            {
                return _autoCsaEmdUnitRepository.ComPortNumber;
            }
            private set
            {
                _autoCsaEmdUnitRepository.ComPortNumber = value;
            }
        }

        /// <summary>
        /// The communication port number.
        /// </summary>
        public BroadcastMethodologies BroadcastMethodology
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns true if the csa unit has a reading.
        /// </summary>
        public bool HasReading
        {
            get
            {
                return _autoCsaEmdUnitRepository.HasReading;
            }
        }

        /// <summary>
        /// Gets the current reading model.
        /// </summary>
        public AutoCSAReadingModel CurrentReading { get; private set; }

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
        /// Gets setting broadcasting status.
        /// </summary>
        public bool IsBroadcastingOn
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets broadcasting status.
        /// </summary>
        public bool IsBroadcasting
        {
            get
            {
                return _autoCsaEmdUnitRepository.IsBroadcasting;
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

        #endregion

        #region Prototype Public Properties

        /// <summary>
        /// Gets if the Prototype Connected
        /// </summary>
        public bool IsPrototypeConnected
        {
            get
            {
                return _prototypeRepository.IsProtprtpeConnected;
            }
        }

        /// <summary>
        /// Get the is Prototype connection open value.
        /// </summary>
        public bool IsPrototypeConnectionOpen
        {
            get
            {
                return _prototypeRepository.IsConnectionOpen;
            }
        }

        /// <summary>
        /// The Prototype communication port number.
        /// </summary>
        public int PrototypeComPortNumber
        {
            get
            {
                return _prototypeRepository.ComPortNumber;
            }
            private set
            {
                _prototypeRepository.ComPortNumber = value;
            }
        }

        /// <summary>
        /// Gets auto com port detection for Prototype.
        /// </summary>
        public bool PrototypeAutoComPortDetection
        {
            get { return _prototypeRepository.AutoComPortDetection; }
            set { _prototypeRepository.AutoComPortDetection = value; }
        }

        /// <summary>
        /// The timeout (In milliseconds) of checking for Prototype connection status.
        /// </summary>
        public int PrototypeDisconnectedTimeout
        {
            get
            {
                return _prototypeRepository.DisconnectedTimeout;
            }
            private set
            {
                _prototypeRepository.DisconnectedTimeout = value;
            }
        }

        /// <summary>
        /// Gets the current testing point.
        /// </summary>
        public TestingPoint CurrentTestingPoint { get; private set; }

        #endregion

        #endregion

        #region Methods

        #region CSA Methods

        /// <summary>
        /// Set the reading mode.
        /// </summary>
        /// <param name="mode">The reading mode.</param>
        public void SetReadingMode(AutoCSAReadingMode mode)
        {
            switch (mode)
            {
                case AutoCSAReadingMode.Continuous:
                    _autoCsaEmdUnitRepository.ReadingStabilityEnabled = false;
                    break;
                case AutoCSAReadingMode.Mixed:
                case AutoCSAReadingMode.StableReading:
                    _autoCsaEmdUnitRepository.ReadingStabilityEnabled = true;
                    break;
            }

            ReadingMode = mode;
        }

        /// <summary>
        /// Open the connection with the CSA device. [ If the connection succeeded the process result will carry the true. Else, will throw an exception.]
        /// </summary>
        /// <returns></returns>
        public ProcessResult OpenCSAConnection(SerialPortConnectionFilter connectionFilter)
        {
            Check.Argument.IsNotNull(() => connectionFilter);
            Check.Argument.IsNotNegative(connectionFilter.ComPortNumber, "Port Number should not be negative.");

            try
            {
                InitCSAHardwareHandlers();

                var result = _autoCsaEmdUnitRepository.OpenConnection(connectionFilter.ComPortNumber,
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
        /// Open the connection with the CSA device. [ If the connection succeeded the process result will carry the true. Else, will throw an exception.]
        /// </summary>
        /// <returns></returns>
        public ProcessResult OpenCSAConnection()
        {
            var filter = new SerialPortConnectionFilter(HardwareType.CSA)
            {
                Timeout = 1500,
                ComPortNumber = CSAComPortNumber,
                AutoComPortDetection = CSAAutoComPortDetection
            };

            var isOpend = OpenCSAConnection(filter);

            return isOpend;
        }

        /// <summary>
        /// Refresh current CSA connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult RefreshCSAConnection(SerailPortNumberFilter connectionFilter)
        {
            Check.Argument.IsNotNegative(connectionFilter.ComPortNumber, "Port Number should not be negative.");

            CloseCSAConnection();

            CSAComPortNumber = connectionFilter.ComPortNumber;
            CSAAutoComPortDetection = connectionFilter.AutoComPortDetection ?? false;

            return OpenCSAConnection();

        }

        /// <summary>
        /// Refresh current CSA connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult RefreshCSAConnection()
        {
            return RefreshCSAConnection(new SerailPortNumberFilter { ComPortNumber = CSAComPortNumber, AutoComPortDetection = CSAAutoComPortDetection });
        }

        /// <summary>
        /// Close the CSA connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult CloseCSAConnection()
        {
            try
            {
                _autoCsaEmdUnitRepository.CloseReading();

                var result = _autoCsaEmdUnitRepository.CloseConnection();

                if (!result.IsSucceed) return result;

                _autoCsaEmdUnitRepository.Disconnected -= _csaEmdUnitRepository_Disconnected;

                _autoCsaEmdUnitRepository.Connected -= _csaEmdUnitRepository_Connected;

                _autoCsaEmdUnitRepository.Detecting -= _csaEmdUnitRepository_Detecting;

                _autoCsaEmdUnitRepository.ReadingStopped -= _csaEmdUnitRepository_ReadingStopped;

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
        /// Cancel the auto CSA connection detection operation.
        /// </summary>
        /// <returns></returns>
        public ProcessResult CancelCSAAutoDetection()
        {
            try
            {
                return _autoCsaEmdUnitRepository.CancelAutoDetection();
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Activate the CSA connection.
        /// </summary>
        /// <param name="onReadingStopped"></param>
        /// <param name="onReadingStabled"></param>
        /// <param name="meterValueChangedHandle"></param>
        public void ActivateCSAConnection(MeterValueChangedHandle meterValueChangedHandle = null, OnReadingStopped onReadingStopped = null, OnReadingStabled onReadingStabled = null)
        {
            try
            {
                OpenCSAConnection();

                if (onReadingStopped != null)
                    ReadingStopped += onReadingStopped;

                if (onReadingStabled != null)
                    ReadingStabled += onReadingStabled;

                if (meterValueChangedHandle != null)
                    MeterValueChanged += meterValueChangedHandle;
            }
            catch
            {

            }
        }

        /// <summary>
        /// Dispose the current CSA connection. [ Not closing ]
        /// </summary>
        public void DisposeCSAConnection(MeterValueChangedHandle meterValueChangedHandle = null, OnReadingStopped onReadingStopped = null, OnReadingStabled onReadingStabled = null)
        {
            try
            {
                StopReading();
                if (onReadingStopped != null)
                    ReadingStopped -= onReadingStopped;

                if (onReadingStabled != null)
                    ReadingStabled -= onReadingStabled;

                if (meterValueChangedHandle != null)
                    MeterValueChanged -= meterValueChangedHandle;
            }
            catch
            {

            }
        }

        /// <summary>
        /// Reset the CSA connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult StartCSAResetting()
        {
            try
            {
                var result = _autoCsaEmdUnitRepository.StartConnectionReset();

                if (!result.IsSucceed) return result;

                _autoCsaEmdUnitRepository.ConnectionResetFinished -= _csaEmdUnitRepository_ResettingFinished;

                _autoCsaEmdUnitRepository.ConnectionResetFinished += _csaEmdUnitRepository_ResettingFinished;

                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Stop the in progress CSA resetting.
        /// </summary>
        /// <returns></returns>
        public ProcessResult StopCSAResetting()
        {
            try
            {
                var result = _autoCsaEmdUnitRepository.StopConnectionReset();

                if (!result.IsSucceed) return result;

                _autoCsaEmdUnitRepository.ConnectionResetFinished -= _csaEmdUnitRepository_ResettingFinished;

                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Starts the reading from the CSA device. 
        /// </summary>
        /// <returns></returns>
        public void StartReading()
        {
            try
            {
                if (IsReadingOn) return;

                //Reset reading to prevent system from using last stable reading as the reading of the next item to be scanned
                CurrentReading = new AutoCSAReadingModel(0, 0, 0);

                //_csaEmdUnitRepository.MeterValueChanged -= _csaEmdUnitRepository_MeterValueChanged;

                _autoCsaEmdUnitRepository.MeterValueChanged += _csaEmdUnitRepository_MeterValueChanged;

                //_csaEmdUnitRepository.ReadingDone -= _csaEmdUnitRepository_ReadingDone;

                _autoCsaEmdUnitRepository.ReadingStabled += _csaEmdUnitRepository_ReadingStabled;

                /*IMPORTANT: Temp Code - We set the testing point on each reading to tell the emulator to send a new reading 
                 * on each session to avoid consistency in the reading while testing many items on same point. */
                //ToDo: Remove the call.
                SetPoint(CurrentTestingPoint);

                _autoCsaEmdUnitRepository.OpenReading();

                IsReadingOn = true;

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Stops the reading from the CSA device. 
        /// </summary>
        /// <returns></returns>
        public void StopReading()
        {
            try
            {
                if (!IsReadingOn) return;

                _autoCsaEmdUnitRepository.MeterValueChanged -= _csaEmdUnitRepository_MeterValueChanged;
                _autoCsaEmdUnitRepository.ReadingStabled -= _csaEmdUnitRepository_ReadingStabled;

                _autoCsaEmdUnitRepository.CloseReading();

                IsReadingOn = false;

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Force to remove CSA connection handlers.
        /// </summary>
        public void ForceRemoveUserHandlers()
        {
            var readingStoppedDelg = ReadingStopped as Delegate;
            ReadingStopped = Delegate.RemoveAll(readingStoppedDelg, readingStoppedDelg) as OnReadingStopped;

            var readingStabledDelg = ReadingStabled as Delegate;
            ReadingStabled = Delegate.RemoveAll(readingStabledDelg, readingStabledDelg) as OnReadingStabled;

            var meterValueChangedDelg = MeterValueChanged as Delegate;
            MeterValueChanged = Delegate.RemoveAll(meterValueChangedDelg, meterValueChangedDelg) as MeterValueChangedHandle;
        }

        /// <summary>
        /// Clear the Instance Caches and CSA readings.
        /// </summary>
        /// <returns></returns>
        public void Clear()
        {
            try
            {
                _autoCsaEmdUnitRepository.Clear();
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
        public ProcessResult Broadcast(List<AutoItem> items, bool isImprinting = false, bool flush = true)
        {
            try
            {
                if (!IsCsaEmdUnitConnected)
                {
                    FlushBroadcastBuffer();
                    _isImprinting = false;
                    return ProcessResult.Failed;
                }

                if (!isImprinting && (!IsBroadcastingOn || IsImprinting))
                    return ProcessResult.Failed;

                //Imprinting had been finished.
                _isImprinting = isImprinting;

                if (_isImprinting)
                    items = items.Where(i => i.IsImprintable).ToList();

                List<string> messages;

                if (flush)
                    FlushBroadcastBuffer();

                switch (BroadcastMethodology)
                {
                    case BroadcastMethodologies.BlankString:
                        items.ForEach(i => _autoCsaEmdUnitRepository.Broadcast(i.Name));
                        break;
                    case BroadcastMethodologies.VitalDegreeOfAffect:
                        messages = GenerateBroadcastMessage(items, BroadcastMethodologies.VitalDegreeOfAffect);
                        messages.ForEach(msg => _autoCsaEmdUnitRepository.Broadcast(msg));
                        break;

                    case BroadcastMethodologies.VitalIngredientHashCode:
                        messages = GenerateBroadcastMessage(items, BroadcastMethodologies.VitalMessageIngredients);
                        messages.ForEach(msg => _autoCsaEmdUnitRepository.Broadcast(msg.GetHashCode().ToString()));
                        break;

                    case BroadcastMethodologies.VitalMessage:
                        items.ForEach(i => _autoCsaEmdUnitRepository.Broadcast(i.Id.ToString()));
                        break;

                    case BroadcastMethodologies.VitalMessageIngredients:
                        messages = GenerateBroadcastMessage(items, BroadcastMethodologies.VitalMessageIngredients);
                        messages.ForEach(msg => _autoCsaEmdUnitRepository.Broadcast(msg));
                        break;

                    case BroadcastMethodologies.VitalPointHashCode:
                        messages = GenerateBroadcastMessage(items, BroadcastMethodologies.VitalDegreeOfAffect);
                        messages.ForEach(msg => _autoCsaEmdUnitRepository.Broadcast(msg.GetHashCode().ToString()));
                        break;
                }

                return ProcessResult.Succeed;
            }
            catch
            {
                return ProcessResult.Failed;
            }
        }

        /// <summary>
        /// Flush the broadcast buffer.
        /// </summary>
        /// <returns></returns>
        public ProcessResult FlushBroadcastBuffer()
        {
            try
            {
                return _autoCsaEmdUnitRepository.FlushBroadcastBuffer();
            }
            catch
            {
                return ProcessResult.Failed;

            }
        }

        #endregion

        #region Prototype Methods

        /// <summary>
        /// Open the connection with the Prototype device. [ If the connection succeeded the process result will carry the true. Else, will throw an exception.]
        /// </summary>
        /// <returns></returns>
        public ProcessResult OpenPrototypeConnection(SerialPortConnectionFilter connectionFilter)
        {
            Check.Argument.IsNotNull(() => connectionFilter);
            Check.Argument.IsNotNegative(connectionFilter.ComPortNumber, "Port Number should not be negative.");

            try
            {
                InitPrototypeHardwareHandlers();

                var result = _prototypeRepository.OpenConnection(connectionFilter.ComPortNumber,
                                                                  connectionFilter.BaudRate,
                                                                  connectionFilter.DataBit, 
                                                                  connectionFilter.Timeout,
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
        /// Open the connection with the Prototype device. [ If the connection succeeded the process result will carry the true. Else, will throw an exception.]
        /// </summary>
        /// <returns></returns>
        public ProcessResult OpenPrototypeConnection()
        {
            var filter = new SerialPortConnectionFilter(HardwareType.Prototype)
            {
                ComPortNumber = PrototypeComPortNumber,
                AutoComPortDetection = CSAAutoComPortDetection
            };

            var isOpend = OpenPrototypeConnection(filter);

            return isOpend;
        }

        /// <summary>
        /// Refresh current Prototype connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult RefreshPrototypeConnection(SerailPortNumberFilter connectionFilter)
        {
            Check.Argument.IsNotNegative(connectionFilter.ComPortNumber, "Port Number should not be negative.");

            ClosePrototypeConnection();

            PrototypeComPortNumber = connectionFilter.ComPortNumber;

            return OpenPrototypeConnection();

        }

        /// <summary>
        /// Refresh current Prototype connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult RefreshPrototypeConnection()
        {
            return RefreshPrototypeConnection(new SerailPortNumberFilter { ComPortNumber = CSAComPortNumber, AutoComPortDetection = CSAAutoComPortDetection });
        }

        /// <summary>
        /// Close the Prototype connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult ClosePrototypeConnection()
        {
            try
            {
                var result = _prototypeRepository.CloseConnection();

                if (!result.IsSucceed) return result;

                _prototypeRepository.Disconnected -= _prototypeRepository_Disconnected;

                _prototypeRepository.Connected -= _prototypeRepository_Connected;

                _prototypeRepository.Detecting -= _prototypeRepository_Detecting;

                _prototypeRepository.ResponseReceived -= _prototypeRepository_ResponseReceived;

                //ForceRemoveUserHandlers();

                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Set the testing point.
        /// </summary>
        /// <param name="point">The testing point.</param>
        /// <returns></returns>
        public ProcessResult SetPoint(TestingPoint point)
        {
            try
            {
                /*IMPORTANT: Temp Code - We use force set to the point to allow the emulator to send a new reading on each reading session 
                 * to avoid consistency in the reading while testing many items on same point. */
                //ToDo: Remove the force set of the setPoint call.
                var result =  _prototypeRepository.SetPoint(point.HWIdentifier, true);

                if (result.IsSucceed)
                    CurrentTestingPoint = point;

                return result;

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Cancel and reset the prototype, turn of all indicators, disconnect points and cancel the current command.
        /// </summary>
        /// <returns></returns>
        public ProcessResult ResetPrototype()
        {
            try
            {
                return _prototypeRepository.SendCommand(PrototypeProtocol.Commands[PrototypeCommand.Reset]);

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Perform a moisture check.
        /// </summary>
        /// <returns></returns>
        public ProcessResult PerformMoistureCehck()
        {
            try
            {
                return _prototypeRepository.SendCommand(PrototypeProtocol.Commands[PrototypeCommand.MoistureCheck]);

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Perform pressure check.
        /// </summary>
        /// <returns></returns>
        public ProcessResult PerformPressureCheck()
        {
            try
            {
                return _prototypeRepository.SendCommand(PrototypeProtocol.Commands[PrototypeCommand.PressureCheck]);

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        #endregion

        #region GeneralMethods

        /// <summary>
        /// Set the testing point and start the reading.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public ProcessResult StartReading(TestingPoint point)
        {
            try
            {
                var setPointResult = SetPoint(point);

                if (!setPointResult.IsSucceed)
                    return setPointResult;

                StartReading();

                return ProcessResult.Succeed;

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
        public BindingList<ComPortInfo> GetComPorts(bool allowsAutoDetection = true)
        {
            try
            {
                var result = _autoCsaEmdUnitRepository.GetComPorts();

                if (!allowsAutoDetection) 
                    return result;

                var autoCom = new ComPortInfo { Id = 0, Name = StaticKeys.AutoDetectionText };
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
        /// Refresh the settings values.
        /// </summary>
        public void RefreshSettings()
        {
            CloseCSAConnection();

            var lookupsRepository = new LookupDatabaseRepository();

            var onLokkup =
                    lookupsRepository.LoadLookups(Enum.GetName(typeof(LookupTypes), LookupTypes.OnOff),
                                                  Enum.GetName(typeof(OnOffEnum), OnOffEnum.On)).FirstOrDefault();

            RefreshHwProfileSettings();
            RefreshPortNumberSettings();
            EdsAutoPlayWaitingTime = IntegerSettingsLoader(_settingRepository, SettingKeys.EdsNextPointNavigationTime);
            BeepFrequency = IntegerSettingsLoader(_settingRepository, SettingKeys.Frequency);
            ItemTestingAutoPlayWaitingTime = IntegerSettingsLoader(_settingRepository, SettingKeys.ItemTestingNavigationTime);

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
            CloseCSAConnection();

            CSAComPortNumber = IntegerSettingsLoader(_settingRepository, SettingKeys.CommunicationsPort);            
            CSAAutoComPortDetection = CSAComPortNumber == 0;

            PrototypeComPortNumber = IntegerSettingsLoader(_settingRepository, SettingKeys.AutoTestDeviceComPort);
            PrototypeAutoComPortDetection = PrototypeComPortNumber == 0;
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
            //ToDo: Change this to custom setting for the prototype.
            PrototypeDisconnectedTimeout = defaultCsaDisconnectedTimeoutStng;
            ReadingStabilityTimeout = defaultStabilityTimeoutStng;
            ReadingStabilityRange = defaultStabilityRangeStng;
            MinimumReadingtoRegister = defaultMinReadingStng;
        }

        #endregion

        #endregion

        #region Private Helper

        #region CSA Private Helper

        /// <summary>
        /// Sets the CSA Hw handlers.
        /// </summary>
        private void InitCSAHardwareHandlers()
        {
            _autoCsaEmdUnitRepository.Disconnected -= _csaEmdUnitRepository_Disconnected;
            _autoCsaEmdUnitRepository.Disconnected += _csaEmdUnitRepository_Disconnected;

            _autoCsaEmdUnitRepository.Connected -= _csaEmdUnitRepository_Connected;
            _autoCsaEmdUnitRepository.Connected += _csaEmdUnitRepository_Connected;

            _autoCsaEmdUnitRepository.ReadingStopped -= _csaEmdUnitRepository_ReadingStopped;
            _autoCsaEmdUnitRepository.ReadingStopped += _csaEmdUnitRepository_ReadingStopped;

            _autoCsaEmdUnitRepository.Detecting -= _csaEmdUnitRepository_Detecting;
            _autoCsaEmdUnitRepository.Detecting += _csaEmdUnitRepository_Detecting;
        }

        /// <summary>
        /// Generate The broadcast message depends on his supper type.
        /// </summary>
        private static List<string> GenerateBroadcastMessage(List<AutoItem> items, BroadcastMethodologies superBroadcastType)
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

        #endregion

        #region Prototype Private Helper

        /// <summary>
        /// Sets the Prototype Hw handlers.
        /// </summary>
        private void InitPrototypeHardwareHandlers()
        {
            _prototypeRepository.Disconnected -= _prototypeRepository_Disconnected;
            _prototypeRepository.Disconnected += _prototypeRepository_Disconnected;

            _prototypeRepository.Connected -= _prototypeRepository_Connected;
            _prototypeRepository.Connected += _prototypeRepository_Connected;

            _prototypeRepository.Detecting -= _prototypeRepository_Detecting;
            _prototypeRepository.Detecting += _prototypeRepository_Detecting;

            _prototypeRepository.ResponseReceived -= _prototypeRepository_ResponseReceived;
            _prototypeRepository.ResponseReceived += _prototypeRepository_ResponseReceived;
        }

        #endregion

        #region General Private Helper

        /// <summary>
        /// Fills local lookup ids.
        /// </summary>
        private void FillLookupIds()
        {
            var lookupsRepository = new LookupDatabaseRepository();

            var yesLookup = lookupsRepository.LoadLookups(EnumNameResolver.Resolve(LookupTypes.YesNo),
                                                                           EnumNameResolver.Resolve(YesNoEnum.Yes)).FirstOrDefault();

            if (yesLookup == null)
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

        #endregion

        #endregion

        #region Handlers

        #region CSA Handlers

        /// <summary>
        /// ResettingFinished Handler.
        /// </summary>
        /// <param name="sender"></param>
        void _csaEmdUnitRepository_ResettingFinished(object sender)
        {
            if (CSAResettingFinished == null) return;
            CSAResettingFinished(sender);
        }

        /// <summary>
        /// MeterValueChanged Handler.
        /// </summary>
        void _csaEmdUnitRepository_MeterValueChanged(object sender, int reading, int min, int max)
        {
            CurrentReading = new AutoCSAReadingModel(reading, min, max);
            if (MeterValueChanged == null) return;
            MeterValueChanged(sender, reading, min, max);
        }

        /// <summary>
        /// Disconnected Handler.
        /// </summary>
        /// <param name="sender"></param>
        void _csaEmdUnitRepository_Disconnected(object sender)
        {
            if (CSADisconnected == null) return;
            CSADisconnected(sender);
        }

        /// <summary>
        /// ReadingStabled Handler.
        /// </summary>
        void _csaEmdUnitRepository_ReadingStabled(object sender, int reading, int min, int max, int fall, int rais)
        {
            if (reading <= 0) 
                return;

            if (CrossLayersSharedLogic.IsAcceptableReading(reading))
                reading = 50;

            CurrentReading = new AutoCSAReadingModel(reading, min, max, true);

            if (ReadingStabled != null)
                ReadingStabled(sender, reading, min, max, fall, rais);

            if (ReadingMode != AutoCSAReadingMode.Mixed)
                StopReading();

        }

        /// <summary>
        /// Connected Handler.
        /// </summary>
        /// <param name="sender"></param>
        void _csaEmdUnitRepository_Connected(object sender)
        {
            _autoCsaEmdUnitRepository.OpenReading();

            if (CSAConnected == null)
                return;

            CSAConnected(this);

        }

        /// <summary>
        /// Handel the trying to connect event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="comPortNumber"></param>
        void _csaEmdUnitRepository_Detecting(object sender, int comPortNumber)
        {

            if (CSADetecting == null)
                return;

            CSADetecting(this, comPortNumber);
        }

        /// <summary>
        /// Handel the event when tester release the csa tools from the patient body. 
        /// </summary>
        /// <param name="sender"></param>
        void _csaEmdUnitRepository_ReadingStopped(object sender)
        {
            if (ReadingStopped != null)
                ReadingStopped(this);
        }

        #endregion

        #region Prototypr Handlers

        /// <summary>
        /// Disconnected Handler.
        /// </summary>
        /// <param name="sender"></param>
        void _prototypeRepository_Disconnected(object sender)
        {
            if (PrototypeDisconnected == null) return;
            PrototypeDisconnected(sender);
        }

        /// <summary>
        /// Handel the trying to connect event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="comPortNumber"></param>
        void _prototypeRepository_Detecting(object sender, int comPortNumber)
        {

            if (PrototypeDetecting == null)
                return;

            PrototypeDetecting(this, comPortNumber);
        }

        /// <summary>
        /// Handel the prototype response received event.
        /// </summary>
        void _prototypeRepository_ResponseReceived(object sender, PrototypeResponse response, string originData)
        {

            if (PrototypResponseReceived == null)
                return;

            PrototypResponseReceived(this, response, originData);
        }

        /// <summary>
        /// Connected Handler.
        /// </summary>
        /// <param name="sender"></param>
        void _prototypeRepository_Connected(object sender)
        {
            if (PrototypeConnected == null)
                return;

            PrototypeConnected(this);

        }

        #endregion

        #endregion

        #region Event Handler Delegates

        #region CSA Event Handler Delegates

        /// <summary>
        /// Delegate for Disconnected handler method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnCSADisconnected(object sender);

        /// <summary>
        /// Delegate for connected handler method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnCSAConnected(object sender);

        /// <summary>
        /// Delegate for OnDetecting handle method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="comPortNumber"></param>
        public delegate void OnCSADetecting(object sender, int comPortNumber);

        /// <summary>
        /// Delegate for OnResettingFineshedHandle handler method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnCSAResettingFinishedHandle(object sender);

        /// <summary>
        /// Delegate for MeterValueChangedEvent handler method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="reading">Last reading.</param>
        /// <param name="min">The Min value.</param>
        /// <param name="max">The Max value.</param>
        public delegate void MeterValueChangedHandle(object sender, int reading, int min, int max);

        /// <summary>
        /// Delegate for OnReadingStabled handler method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reading">Last reading.</param>
        /// <param name="min">The Min value.</param>
        /// <param name="max">The Max value.</param>
        /// <param name="fall">The Fall value.</param>
        /// <param name="rais">The Rais value.</param>
        public delegate void OnReadingStabled(object sender, int reading, int min, int max, int fall, int rais);

        /// <summary>
        /// Delegate for OnReadingStopped handle method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnReadingStopped(object sender);

        #endregion

        #region Prototype Event Handler Delegates

        /// <summary>
        /// Delegate for Prototype Disconnected handler method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnPrototypeDisconnected(object sender);

        /// <summary>
        /// Delegate for Prototype connected handler method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnPrototypeConnected(object sender);

        /// <summary>
        /// Delegate for Prototype OnDetecting handle method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="comPortNumber"></param>
        public delegate void OnPrototypeDetecting(object sender, int comPortNumber);

        /// <summary>
        /// Delegate for Prototype response received handler method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnPrototypResponseReceived(object sender, PrototypeResponse response, string originData);

        #endregion

        #endregion
    }

}
