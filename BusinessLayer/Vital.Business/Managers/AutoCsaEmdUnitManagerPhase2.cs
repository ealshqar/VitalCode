using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Vital.Business.Repositories.DatabaseRepositories.Lookups;
using Vital.Business.Repositories.DatabaseRepositories.Settings;
using Vital.Business.Repositories.HardwareRepositories.AutoCsaEmdUnit;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects.AutoTestSource;
using Vital.Business.Shared.DomainObjects.Hardware;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class AutoCsaEmdUnitManagerPhase2 : BaseManager
    {
        #region Private Variables

        private readonly IAutoCsaEmdUnitHardwareRepository _autoCsaEmdUnitRepository;
        private readonly ISettingRepository _settingRepository;
        private bool _isImprinting;
        private int _yesLookupId;
        private bool _waitingActivateTopPlate;
        private bool _waitingStartAutomation;
        private bool _waitingActivateManaualMode;

        #endregion

        #region Singleton Code

        private static AutoCsaEmdUnitManagerPhase2 _instance;

        /// <summary>
        /// Returns the instance of the AutoCsaEmdUnitManager.
        /// </summary>
        public static AutoCsaEmdUnitManagerPhase2 Instance
        {
            get { return _instance ?? (_instance = new AutoCsaEmdUnitManagerPhase2()); }
        }

        #endregion
        
        #region Constructors

        /// <summary>
        /// CsaEmdUnitManager Constructor.
        /// </summary>
        public AutoCsaEmdUnitManagerPhase2()
        {
            _autoCsaEmdUnitRepository = new AutoCsaEmdUnitHardwareRepository();

            _settingRepository = new SettingDatabaseRepository();

            _isImprinting = true;

            FillLookupIds();

            RefreshSettings();

            //Initialize CurrentReading to avoid possible object reference exception
            CurrentReading = new AutoCSAReadingModel(0, 0, 0);
        }

        #endregion

        #region Events

        /// <summary>
        /// Event thrown when some reading ready for show.
        /// </summary>
        public event MeterValueChangedHandle MeterValueChanged;

        /// <summary>
        /// Event thrown when the CSA Resetting Finished
        /// </summary>
        public event OnConnectionResetFinishedHandle ConnectionResetFinished;

        /// <summary>
        /// Event thrown when the CSA device being disconnected.
        /// </summary>
        public event OnDisconnected Disconnected;

        /// <summary>
        /// Event thrown when the CSA device being connected.
        /// </summary>
        public event OnConnected Connected;

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
        public event OnDetecting Detecting;

        /// <summary>
        /// Event thrown when response received from the HW.
        /// </summary>
        public event OnResponseReceived ResponseReceived;

       
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the current reading mode.
        /// </summary>
        public AutoCSAReadingMode ReadingMode { get; private set; }

        /// <summary>
        /// Get the is CSA connection open value.
        /// </summary>
        public bool IsConnectionOpen
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
        public bool IsConnectionResetting
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
        public int DisconnectedTimeout
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
        public bool AutoComPortDetection
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
        public int ComPortNumber
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
        /// Gets the current testing point.
        /// </summary>
        public TestingPoint CurrentTestingPoint { get; private set; }

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
        /// Gets the AutoOpenProductDosages setting value.
        /// </summary>
        public bool AutoOpenProductDosages 
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

        /// <summary>
        /// Gets the CSA mode.
        /// </summary>
        public AutoCSAMode CurrentMode { get; private set; }

        /// <summary>
        /// Check if the automation started.
        /// </summary>
        public bool? IsAutomationStarted { get; private set; }

        /// <summary>
        /// Check if the imprinting is active.
        /// </summary>
        public bool? IsImprintingActive { get; private set; }

        /// <summary>
        /// Check if the top plate is active.
        /// </summary>
        public bool? IsTopPlateActive { get; private set; }

        /// <summary>
        /// Check if the CSA has valid hinge.
        /// </summary>
        public bool? HasValidHinge { get; private set; }

        /// <summary>
        /// Check if the CSA has valid moisture.
        /// </summary>
        public bool? HasValidMoisture { get; private set; }

        /// <summary>
        /// Check if the CSA has valid pressure.
        /// </summary>
        public bool? HasValidPressure { get; private set; }

        /// <summary>
        /// Checks if the probes are connected.
        /// </summary>
        public bool AreProbesConnected { get; private set; }

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
                Timeout = 2000,
                ComPortNumber = ComPortNumber,
                AutoComPortDetection = AutoComPortDetection
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

            ComPortNumber = connectionFilter.ComPortNumber;
            AutoComPortDetection = connectionFilter.AutoComPortDetection ?? false;

            return OpenCSAConnection();

        }

        /// <summary>
        /// Refresh current CSA connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult RefreshCSAConnection()
        {
            return RefreshCSAConnection(new SerailPortNumberFilter { ComPortNumber = ComPortNumber, AutoComPortDetection = AutoComPortDetection });
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

                _autoCsaEmdUnitRepository.Disconnected -= _autoCsaEmdUnitRepository_Disconnected;

                _autoCsaEmdUnitRepository.Connected -= _autoCsaEmdUnitRepository_Connected;

                _autoCsaEmdUnitRepository.Detecting -= _autoCsaEmdUnitRepository_Detecting;

                _autoCsaEmdUnitRepository.ReadingStopped -= _autoCsaEmdUnitRepository_ReadingStopped;

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
        public ProcessResult StartConnectionReset()
        {
            try
            {
                var result = _autoCsaEmdUnitRepository.StartConnectionReset();

                if (!result.IsSucceed) return result;

                _autoCsaEmdUnitRepository.ConnectionResetFinished -= _autoCsaEmdUnitRepository_ConnectionResetFinished;

                _autoCsaEmdUnitRepository.ConnectionResetFinished += _autoCsaEmdUnitRepository_ConnectionResetFinished;

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
        public ProcessResult StopConnectionReset()
        {
            try
            {
                var result = _autoCsaEmdUnitRepository.StopConnectionReset();

                if (!result.IsSucceed) return result;

                _autoCsaEmdUnitRepository.ConnectionResetFinished -= _autoCsaEmdUnitRepository_ConnectionResetFinished;

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

                //_autoCsaEmdUnitRepository.MeterValueChanged -= _autoCsaEmdUnitRepository_MeterValueChanged;

                _autoCsaEmdUnitRepository.MeterValueChanged += _autoCsaEmdUnitRepository_MeterValueChanged;

                //_autoCsaEmdUnitRepository.ReadingDone -= _autoCsaEmdUnitRepository_ReadingDone;

                _autoCsaEmdUnitRepository.ReadingStabled += _autoCsaEmdUnitRepository_ReadingStabled;

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

                _autoCsaEmdUnitRepository.MeterValueChanged -= _autoCsaEmdUnitRepository_MeterValueChanged;
                _autoCsaEmdUnitRepository.ReadingStabled -= _autoCsaEmdUnitRepository_ReadingStabled;

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

        /// <summary>
        /// Activate the automation mode.
        /// </summary>
        /// <returns></returns>
        public ProcessResult ActivateAutomationMode()
        {
            try
            {
                return _autoCsaEmdUnitRepository.SendCommand(AutoCSAProtocol.Commands[AutoCSACommand.ActivateAutomationMode]);

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Activate the manual mode.
        /// </summary>
        /// <returns></returns>
        public ProcessResult ActivateManualMode()
        {
            try
            {
                var result = _autoCsaEmdUnitRepository.SendCommand(AutoCSAProtocol.Commands[AutoCSACommand.ActivateManualMode]);

                if (result.IsSucceed)
                    _waitingActivateManaualMode = true;

                return result;

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Activate the imprinting mode.
        /// </summary>
        /// <returns></returns>
        public ProcessResult ActivateImprintingMode()
        {
            try
            {
                return _autoCsaEmdUnitRepository.SendCommand(AutoCSAProtocol.Commands[AutoCSACommand.ActivateImprinting]);

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Activate the top plate.
        /// </summary>
        /// <returns></returns>
        public ProcessResult ActivateTopPlate()
        {
            try
            {
                var result =  _autoCsaEmdUnitRepository.SendCommand(AutoCSAProtocol.Commands[AutoCSACommand.ActivateTopPlate]);

                if (result.IsSucceed)
                    _waitingActivateTopPlate = true;

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
                var result = _autoCsaEmdUnitRepository.SetPoint(point.HWIdentifier, true);

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
        /// Perform a moisture check.
        /// </summary>
        /// <returns></returns>
        public ProcessResult PerformMoistureCehck()
        {
            try
            {
                return _autoCsaEmdUnitRepository.SendCommand(AutoCSAProtocol.Commands[AutoCSACommand.MoistureCheck]);

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
                return _autoCsaEmdUnitRepository.SendCommand(AutoCSAProtocol.Commands[AutoCSACommand.PressureCheck]);

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Perform hinge check.
        /// </summary>
        /// <returns></returns>
        public ProcessResult PerformHingeCheck()
        {
            try
            {
                return _autoCsaEmdUnitRepository.SendCommand(AutoCSAProtocol.Commands[AutoCSACommand.HingeCheck]);

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Performs HW reset.
        /// </summary>
        /// <returns></returns>
        public ProcessResult PerformReset()
        {
            try
            {
                return _autoCsaEmdUnitRepository.SendCommand(AutoCSAProtocol.Commands[AutoCSACommand.Reset]);

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Starts the automation.
        /// </summary>
        /// <returns></returns>
        public ProcessResult StartAutomation()
        {
            try
            {
                var result = _autoCsaEmdUnitRepository.SendCommand(AutoCSAProtocol.Commands[AutoCSACommand.StartAutomation]);

                if (result.IsSucceed)
                    _waitingStartAutomation = true;

                return result;

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new VitalHardwareException(exception);
            }
        }

        /// <summary>
        /// Stops the automation.
        /// </summary>
        /// <returns></returns>
        public ProcessResult StopAutomation()
        {
            try
            {
                return _autoCsaEmdUnitRepository.SendCommand(AutoCSAProtocol.Commands[AutoCSACommand.StopAutomation]);

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
            CloseCSAConnection();

            ComPortNumber = IntegerSettingsLoader(_settingRepository, SettingKeys.CommunicationsPort);            
            AutoComPortDetection = ComPortNumber == 0;
        }

        /// <summary>
        /// Refresh hw settings that related to profile.
        /// </summary>
        public void RefreshHwProfileSettings()
        {

            var defaultHwProfile = _settingRepository.LoadHwProfiles(string.Empty, string.Empty, 0, 0, 0, 0, null, true).FirstOrDefault();

            if (defaultHwProfile == null)
                throw new VitalHardwareException("Missing default hardware profile");

            DisconnectedTimeout = defaultHwProfile.DisconnectedTimeout;
            ReadingStabilityTimeout = defaultHwProfile.StabilityTimeout;
            ReadingStabilityRange = defaultHwProfile.StabilityRange;
            MinimumReadingtoRegister = defaultHwProfile.MinReading;
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
            _autoCsaEmdUnitRepository.Disconnected -= _autoCsaEmdUnitRepository_Disconnected;
            _autoCsaEmdUnitRepository.Disconnected += _autoCsaEmdUnitRepository_Disconnected;

            _autoCsaEmdUnitRepository.Connected -= _autoCsaEmdUnitRepository_Connected;
            _autoCsaEmdUnitRepository.Connected += _autoCsaEmdUnitRepository_Connected;

            _autoCsaEmdUnitRepository.ReadingStopped -= _autoCsaEmdUnitRepository_ReadingStopped;
            _autoCsaEmdUnitRepository.ReadingStopped += _autoCsaEmdUnitRepository_ReadingStopped;

            _autoCsaEmdUnitRepository.Detecting -= _autoCsaEmdUnitRepository_Detecting;
            _autoCsaEmdUnitRepository.Detecting += _autoCsaEmdUnitRepository_Detecting;

            _autoCsaEmdUnitRepository.ResponseReceived -= _autoCsaEmdUnitRepository_ResponseReceived;
            _autoCsaEmdUnitRepository.ResponseReceived += _autoCsaEmdUnitRepository_ResponseReceived;

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

            var settingValue = 0;

            if (setting == null) 
                return settingValue;

            int.TryParse(setting.Value as string, out settingValue);

            return settingValue;
        }

        #endregion

        #endregion

        #region Handlers

        /// <summary>
        /// ConnectionResetFinished Handler.
        /// </summary>
        /// <param name="sender"></param>
        void _autoCsaEmdUnitRepository_ConnectionResetFinished(object sender)
        {
            if (ConnectionResetFinished == null) return;
            ConnectionResetFinished(sender);
        }

        /// <summary>
        /// MeterValueChanged Handler.
        /// </summary>
        void _autoCsaEmdUnitRepository_MeterValueChanged(object sender, int reading, int min, int max)
        {
            CurrentReading = new AutoCSAReadingModel(reading, min, max);

            if (_waitingStartAutomation)
            {
                _waitingStartAutomation = false;
                IsAutomationStarted = true;
            }

            if (_waitingActivateTopPlate)
            {
                _waitingActivateTopPlate = false;
                IsTopPlateActive = true;
            }

            if (_waitingActivateManaualMode)
            {
                _waitingActivateManaualMode = false;
                CurrentMode = AutoCSAMode.Manual;
                IsAutomationStarted = false;
            }
            
            if (MeterValueChanged != null)
                MeterValueChanged(sender, reading, min, max);
        }

        /// <summary>
        /// Disconnected Handler.
        /// </summary>
        /// <param name="sender"></param>
        void _autoCsaEmdUnitRepository_Disconnected(object sender)
        {
            if (Disconnected == null) return;
            Disconnected(sender);
        }

        /// <summary>
        /// ReadingStabled Handler.
        /// </summary>
        void _autoCsaEmdUnitRepository_ReadingStabled(object sender, int reading, int min, int max, int fall, int rais)
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

            if (BeepDuration > 0)
                Console.Beep(BeepFrequency, BeepDuration);

        }

        /// <summary>
        /// Connected Handler.
        /// </summary>
        /// <param name="sender"></param>
        void _autoCsaEmdUnitRepository_Connected(object sender)
        {
            _autoCsaEmdUnitRepository.OpenReading();

            if (Connected == null)
                return;

            Connected(this);

        }

        /// <summary>
        /// Handel the trying to connect event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="comPortNumber"></param>
        void _autoCsaEmdUnitRepository_Detecting(object sender, int comPortNumber)
        {

            if (Detecting == null)
                return;

            Detecting(this, comPortNumber);
        }

        /// <summary>
        /// Handel the event when tester release the csa tools from the patient body. 
        /// </summary>
        /// <param name="sender"></param>
        void _autoCsaEmdUnitRepository_ReadingStopped(object sender)
        {
            if (ReadingStopped != null)
                ReadingStopped(this);
        }

        /// <summary>
        /// Handel the response received event.
        /// </summary>
        void _autoCsaEmdUnitRepository_ResponseReceived(object sender, AutoCSAResponse response, string originData)
        {

            if (ResponseReceived == null)
                return;

            switch (response)
            {
                case AutoCSAResponse.IdleMode:
                    CurrentMode = AutoCSAMode.Idle;
                    IsImprintingActive = false;
                    IsTopPlateActive = false;
                    IsAutomationStarted = false;
                    HasValidHinge = false;
                    HasValidPressure = false;
                    HasValidMoisture = false;
                    AreProbesConnected = true;
                    break;
                case AutoCSAResponse.IdleAutomationMode:
                    CurrentMode = AutoCSAMode.Automation;
                    IsAutomationStarted = false;
                    break;
                case AutoCSAResponse.ImprintingModeActivated:
                    IsImprintingActive = true;
                    break;
                case AutoCSAResponse.ValidHinge:
                    HasValidHinge = true;
                    break;
                case AutoCSAResponse.InvalidHinge:
                    HasValidHinge = false;
                    break;
                case AutoCSAResponse.ValidMoisture:
                    HasValidMoisture = true;
                    break;
                case AutoCSAResponse.InvalidMoisture:
                    HasValidMoisture = false;
                    break;
                case AutoCSAResponse.ValidPressure:
                    HasValidPressure = true;
                    break;
                case AutoCSAResponse.InvalidPressure:
                    HasValidPressure = false;
                    break;
                case AutoCSAResponse.ManualProbesDisconnected:
                    AreProbesConnected = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("response");
            }

            ResponseReceived(this, response, originData);
        }

        #endregion

        #region Event Handler Delegates

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
        /// Delegate for OnDetecting handle method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="comPortNumber"></param>
        public delegate void OnDetecting(object sender, int comPortNumber);

        /// <summary>
        /// Delegate for OnConnectionResetFinishedHandle handler method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnConnectionResetFinishedHandle(object sender);

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

        /// <summary>
        /// Delegate for response received handler method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnResponseReceived(object sender, AutoCSAResponse response, string originData);


        #endregion
    }

}
