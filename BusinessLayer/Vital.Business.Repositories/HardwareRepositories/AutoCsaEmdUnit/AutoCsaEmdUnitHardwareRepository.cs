using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using AutoMapper;
using Vital.Business.Repositories.Helpers;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.Hardware;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;
using Vital.Hardware.Entities;
using Vital.Hardware.Helpers;

namespace Vital.Business.Repositories.HardwareRepositories.AutoCsaEmdUnit
{
    public class AutoCsaEmdUnitHardwareRepository : BaseRepository, IAutoCsaEmdUnitHardwareRepository
    {

        #region Private Members

        private readonly HwCommunicationHelper _hwCommunicationHelper;

        private readonly bool _debuggingLongHWDisconnectTimeoutEnabled;

        private int _currentReading;

        private DateTime? _csaEmdUnitconnectedStartTime;

        private List<string> BroadcastMessage
        {
            get; set;
        }

        private Thread _resetThread;

        private Thread _lastReadingChekerThread;

        private Thread _autoConnectionThread;

        private Thread _broadcastThread;

        private DateTime? _lastReadingDateTime;

        private bool _isReadingOpened;

        private bool _isReadingDoneNeedsRaise;

        private Thread _connectionCheckerThread;

        private bool _isConnectionOpen;

        private DateTime? _lastComEventDateTime;

        private bool _isCsaEmdUnitConnected;

        private bool _readingStoppedEventRaised;

        private int _iterationCount;

        private int _toppedIterationCount;

        private double _tint;

        private double _upRate;

        private double _dnValue;

        private int _min;

        private int _max;

        private readonly object _lockReading;

        private readonly object _lockDataReceived;

        private readonly object _lockDataSend;

        private bool _disconnectNeedToFirstRais;

        private int _disconnectedTimeout;

        private AutoCSAResponse? _lastReceivedResponse;

        #endregion

        #region Constructors

        /// <summary>
        /// CsaEmdUnitHardwareRepository Constructor.
        /// </summary>
        public AutoCsaEmdUnitHardwareRepository()
        {
            _lockReading = new object();
            _lockDataSend = new object();
            _lockDataReceived = new object();
            _hwCommunicationHelper = new HwCommunicationHelper();
            _lastReadingDateTime = null;
            _debuggingLongHWDisconnectTimeoutEnabled = Debugger.IsAttached && ConfigurationManager.AppSettings[ConfigKeys.EnableDebuggingLongHWDisconnectTimeout.ToString()].ToBoolean();
            BroadcastMessage = new List<string>();
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
        public event OnConnectionResetFinishedHandle ConnectionResetFinished;

        /// <summary>
        /// Event throws when the device being disconnected.
        /// </summary>
        public event OnDisconnected Disconnected;

        /// <summary>
        /// Event throws when the device being connected.
        /// </summary>
        public event OnConnected Connected;

        /// <summary>
        /// Event throws when the reading being stabled.
        /// </summary>
        public event OnReadingStabled ReadingStabled;

        /// <summary>
        /// Event throws when the reading is opened and the HW stopped sending readings.
        /// </summary>
        public event OnReadingStopped ReadingStopped;

        /// <summary>
        /// Event throws when trying to connect some port number.
        /// </summary>
        public event OnDetecting Detecting;


        public event OnResponseReceived ResponseReceived;

        #endregion

        #region Public Properties

        /// <summary>
        /// Get the is connection open value.
        /// </summary>
        public bool IsConnectionOpen
        {
            get
            {
                return _isConnectionOpen && _hwCommunicationHelper.IsOpen;
            }
        }

        /// <summary>
        /// Get is reading on value.
        /// </summary>
        public bool IsReadingOpened
        {
            get
            {
                return _isReadingOpened;
            }
        }

        /// <summary>
        /// Get the is resetting value.
        /// </summary>
        public bool IsConnectionResetting
        {
            get
            {
                return _resetThread.IsAlive;
            }
        }

        /// <summary>
        /// Gets if the Csa Emd Unit Connected
        /// </summary>
        public bool IsCsaEmdUnitConnected
        {
            get
            {
                return _isCsaEmdUnitConnected;
            }
            set
            {
                _isCsaEmdUnitConnected = value;
            }
        }

        /// <summary>
        /// Gets or sets the Minimum Reading to Register value.
        /// </summary>
        public int MinimumReadingtoRegister { get; set; }

        /// <summary>
        /// The timeout (In milliseconds) of checking for CSA connection status.
        /// </summary>
        public int DisconnectedTimeout
        {
            get
            {
                // if _debuggingLongHWDisconnectTimeoutEnabled => Prevent HW From Disconnection While Debugging - Increase the connection timeout.
                return _debuggingLongHWDisconnectTimeoutEnabled
                    ? ConfigurationManager.AppSettings[ConfigKeys.DebuggingLongHWDisconnectTimeout.ToString()].ToInteger() 
                    : _disconnectedTimeout;
            }
            set
            {
                _disconnectedTimeout = value;
            }
        }

        /// <summary>
        /// Determines if the reading stability feature is enabled or not.
        /// </summary>
        public bool ReadingStabilityEnabled { get; set; }

        /// <summary>
        /// Stability period (In milliseconds) before reading being done.
        /// </summary>
        public int ReadingStabilityTimeout { get; set; }

        /// <summary>
        /// Minimum difference between readings to register a new one..
        /// </summary>
        public int ReadingStabilityRange { get; set; }

        /// <summary>
        /// The communication port number.
        /// </summary>
        public int ComPortNumber
        {
            get { return _hwCommunicationHelper.PortNumber; }
            set
            {
                if (_hwCommunicationHelper.PortNumber == value)
                    return;
                    
                if (_hwCommunicationHelper.IsOpen)
                    CloseConnection();

                _hwCommunicationHelper.PortNumber = value;
            }
        }

        /// <summary>
        /// Returns true if the csa unit has a reading.
        /// </summary>
        public bool HasReading
        {
            get
            {
                return _currentReading >= MinimumReadingtoRegister;
            }
        }

        /// <summary>
        /// Gets broadcasting status.
        /// </summary>
        public bool IsBroadcasting
        {
            get
            {
                lock (BroadcastMessage)
                {
                    if (BroadcastMessage.Count > 0)
                    {
                    }
                    return BroadcastMessage.Count > 0;
                    
                }
            }
        }

        /// <summary>
        /// Gets auto com port detection 
        /// </summary>
        public bool AutoComPortDetection { get; set; }

        /// <summary>
        /// Gets the current point command.
        /// </summary>
        public string CurrentPointCommand { get; private set; }

        #endregion

        #region PrivateProperties

        /// <summary>
        /// Gets the auto connection thread interval in ms.
        /// </summary>
        private int AutoConnectionThreadInterval
        {
            get
            {
                // if _debuggingLongHWDisconnectTimeoutEnabled => Prevent HW From Disconnection While Debugging - Avoid the dependence on DisconnectedTimeout to keep the auto connection thread running every short period of time.
                return _debuggingLongHWDisconnectTimeoutEnabled ? 2500 : DisconnectedTimeout;
            }
        }

        #endregion

        #region public Mehtods

        /// <summary>
        /// Open the connection with the device. [ If the connection succeeded the process result will carry the true. Else, will throw an exception.]
        /// </summary>
        /// <param name="comPortNumber">The Port Number.</param>
        /// <param name="baudRate">The BaudRate.</param>
        /// <param name="dataBit">The DataBit</param>
        /// <param name="timeout">The Timeout.</param>
        /// <param name="dtr">The Dtr.</param>
        /// <param name="rts">The Rts.</param>
        /// <param name="isAutoPortDetection">Auto detect the port. </param>
        /// <param name="readingStabilityEnabled">Reading Stability enabled.</param>
        /// <returns></returns>
        public ProcessResult OpenConnection(int comPortNumber, int baudRate, int dataBit, int timeout, bool dtr, bool rts, bool? isAutoPortDetection)
        {
            Check.Argument.IsNotNegative(comPortNumber, "comPortNumber");
            Check.Argument.IsNotNegative(baudRate, "baudRate");
            Check.Argument.IsNotNegative(dataBit, "dataBit");
            Check.Argument.IsNotNegative(timeout, "timeout");

            if (_hwCommunicationHelper.IsOpen && IsCsaEmdUnitConnected)
            {
                if (Connected != null)
                    Connected(this);

                return new ProcessResult {IsSucceed = false, Message = "Can not open an opened connection"};
            }

            if (_hwCommunicationHelper.IsOpen)
            {
                CloseConnection();
            }

            var isComPortManualDetectionMode = isAutoPortDetection.HasValue ? !isAutoPortDetection.Value : !AutoComPortDetection;

            ProcessResult result;

            if (isComPortManualDetectionMode)
            {
                result = OpenConnection(new PortSettingsHelper
                                            {
                                                BaudRate = baudRate,
                                                DataBits = dataBit,
                                                Dtr = dtr,
                                                Rts = rts,
                                                PortNumber = comPortNumber
                                            });
            }
            else
            {
                AutoComPortDetection = true;

                if (_autoConnectionThread != null && _autoConnectionThread.IsAlive)
                {
                    CloseConnection();
                    _autoConnectionThread.Abort();
                }

                _autoConnectionThread = new Thread(AutoConnectionThread) { IsBackground = true };

                _autoConnectionThread.Start(new PortSettingsHelper
                {
                    BaudRate = baudRate,
                    DataBits = dataBit,
                    Dtr = dtr,
                    Rts = rts,
                });

                result = ProcessResult.Succeed;
            }

            return result;
        }

        /// <summary>
        /// Cancel the auto connection detection operation.
        /// </summary>
        /// <returns></returns>
        public ProcessResult CancelAutoDetection()
        {
            try
            {
                if (_autoConnectionThread != null && _autoConnectionThread.IsAlive)
                {
                    CloseConnection();
                    _autoConnectionThread.Abort();
                }

                return ProcessResult.Succeed;
            }
            catch
            {
                return ProcessResult.Succeed;
            }
            
        }

        /// <summary>
        /// Close the connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult CloseConnection()
        {
            if (!_isConnectionOpen && !_hwCommunicationHelper.IsOpen)
                return new ProcessResult { IsSucceed = false, Message = "Can not close an closed connection" };

            StopConnectionReset();
            CloseReading();
            _hwCommunicationHelper.Close();
            _hwCommunicationHelper.StringReceived -= hwCommunicationHelper_StringReceived;
            _hwCommunicationHelper.Alive -= hwCommunicationHelper_Alive;

            if (_isConnectionOpen && _connectionCheckerThread != null && _connectionCheckerThread.IsAlive)
            {
                _isConnectionOpen = false;
                _isCsaEmdUnitConnected = false;
                _csaEmdUnitconnectedStartTime = null;
                if (_connectionCheckerThread.IsAlive)
                    _connectionCheckerThread.Abort();
                if(_broadcastThread != null && _broadcastThread.IsAlive)
                    _broadcastThread.Abort();
                
            }

            return new ProcessResult { IsSucceed = true };
        }

        /// <summary>
        /// Reset the connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult StartConnectionReset()
        {
            if (_resetThread != null && _resetThread.IsAlive)
                return new ProcessResult
                {
                    IsSucceed = false,
                    Message = "Can not start resetting while another one in progress."
                };

            BroadcastMessage.Add("Resetting...");

            _currentReading = 0;

            _resetThread = new Thread(ConnectionResttingThread) { IsBackground = true };
            _resetThread.Start();

            return new ProcessResult { IsSucceed = true };
        }

        /// <summary>
        /// Stop the in progress resetting.
        /// </summary>
        /// <returns></returns>
        public ProcessResult StopConnectionReset()
        {
            if (_resetThread != null && _resetThread.IsAlive)
                return new ProcessResult
                {
                    IsSucceed = false,
                    Message = "Can not stop not started resetting."
                };

            if (_resetThread == null)
            {
                return new ProcessResult
                {
                    IsSucceed = false,
                    Message = "No resetting in progress yet."
                };
            }

            BroadcastMessage.Add("Ready...");

            if (_resetThread.IsAlive)
                _resetThread.Abort();

            return new ProcessResult { IsSucceed = true };

        }

        /// <summary>
        /// Starts the reading from the device. 
        /// </summary>
        /// <returns></returns>
        public ProcessResult OpenReading()
        {
            
            if (_isReadingOpened)
                return new ProcessResult
                           {
                               IsSucceed = false,
                               Message = "Reading already started."
                           };

            _lastReadingChekerThread = new Thread(LastReadingCheckerThread) {IsBackground = true};

            _isReadingOpened = true;

            _lastReadingChekerThread.Start();

            _currentReading = 0;

            return new ProcessResult {IsSucceed = true};
        }

        /// <summary>
        /// Stops the reading from the device. 
        /// </summary>
        /// <returns></returns>
        public ProcessResult CloseReading()
        {
            if (!_isReadingOpened)
                return new ProcessResult
                {
                    IsSucceed = false,
                    Message = "Can not stop not started reading."
                };

            Clear();

            _isReadingOpened = false;

            return new ProcessResult { IsSucceed = true };
        }

        /// <summary>
        /// Clear the Instance Caches and readings.
        /// </summary>
        public void Clear()
        {
            _currentReading = 0;
            _min = 0;
            _max = 0;
            _iterationCount = 0;
            _toppedIterationCount = 0;
            _tint = 0;
            _upRate = 0;
            _dnValue = 0;
        }

        /// <summary>
        /// Broadcast a message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public ProcessResult Broadcast(string message)
        {
            return ProcessResult.Succeed;
            if (!IsConnectionOpen || !_hwCommunicationHelper.IsOpen)
                return new ProcessResult
                           {
                               IsSucceed = false,
                               Message = "Broadcasting failed, Connection is close."
                           };

            if (string.IsNullOrEmpty(message))
                return new ProcessResult
                           {
                               IsSucceed = false,
                               Message = "Broadcast message is empty."
                           };


            lock (BroadcastMessage)
            {
                if (BroadcastMessage.FirstOrDefault(m => m.Equals(message)) == null)
                    BroadcastMessage.Add(message);
            }

            if (_broadcastThread == null || !_broadcastThread.IsAlive)
            {
                _broadcastThread = new Thread(BroadcastThread) {IsBackground = true};
                _broadcastThread.Start();
            }

            return ProcessResult.Succeed;
        }

        /// <summary>
        ///Flush the Broadcasting Buffer.
        /// </summary>
        /// <returns></returns>
        public ProcessResult FlushBroadcastBuffer()
        {
            if (!IsConnectionOpen ||!_hwCommunicationHelper.IsOpen)
                return new ProcessResult
                           {
                               IsSucceed = false,
                               Message = "Broadcasting failed, Connection is close."
                           };
            
                lock (BroadcastMessage)
                {
                    BroadcastMessage.Clear();
                }
            

            return ProcessResult.Succeed;
        }

        /// <summary>
        /// Gets the com ports.
        /// </summary>
        /// <returns></returns>
        public BindingList<ComPortInfo> GetComPorts()
        {
            var portInfoEntities = _hwCommunicationHelper.GetComPorts();

            if (portInfoEntities == null)
            {
                return new BindingList<ComPortInfo>();
            }

            try
            {
                var portInfo = Mapper.Map<IList<ComPortInfoEntity>, IList<ComPortInfo>>(portInfoEntities);

                return portInfo.ToBindingList();
            }
            catch (AutoMapperMappingException)
            {
                return new BindingList<ComPortInfo>();
            }
        }

        /// <summary>
        /// Set the testing point.
        /// </summary>
        /// <param name="pointCommand">The command string.</param>
        /// <param name="forceSet">Force to set the point if its the same as current.</param>
        /// <returns></returns>
        public ProcessResult SetPoint(string pointCommand, bool forceSet)
        {
            Check.Argument.IsNotEmpty(pointCommand, "pointCommand");

            if (!forceSet && string.Equals(CurrentPointCommand, pointCommand))
                return ProcessResult.Succeed;

            lock (_lockDataSend)
            {
                if (!IsConnectionOpen || !_hwCommunicationHelper.IsOpen)
                    return new ProcessResult
                    {
                        IsSucceed = false,
                        Message = "Set point failed, Connection is close."
                    };

                var isScucess = _hwCommunicationHelper.Write(pointCommand);

                if (!isScucess)
                    return ProcessResult.Failed;

                CurrentPointCommand = pointCommand;

                return ProcessResult.Succeed;
            }
        }

        /// <summary>
        /// Send an command for the hardware.
        /// </summary>
        /// <param name="command">The command</param>
        /// <returns></returns>
        public ProcessResult SendCommand(string command)
        {
            Check.Argument.IsNotEmpty(command, "command");

            lock (_lockDataSend)
            {
                if (!IsConnectionOpen || !_hwCommunicationHelper.IsOpen)
                    return new ProcessResult
                    {
                        IsSucceed = false,
                        Message = "Send command failed, Connection is close."
                    };

                var isScucess = _hwCommunicationHelper.Write(command);

                return !isScucess ? ProcessResult.Failed : ProcessResult.Succeed;
            }
        }

        #endregion

        #region Private Handlers And Threads

        /// <summary>
        /// Open the connection.
        /// </summary>
        /// <returns></returns>
        private ProcessResult OpenConnection(PortSettingsHelper portSettings)
        {
            if (IsConnectionOpen)
                CloseConnection();

            if (Detecting != null)
                Detecting(this, ComPortNumber);

            ComPortNumber = portSettings.PortNumber;
            _hwCommunicationHelper.BaudRate = portSettings.BaudRate;
            _hwCommunicationHelper.DataBits = portSettings.DataBits;
            _hwCommunicationHelper.Rts = portSettings.Rts;
            _hwCommunicationHelper.Dtr = portSettings.Dtr;
            _hwCommunicationHelper.DataType = HwCommunicationHelper.HwDataTypeEnum.String;
            _hwCommunicationHelper.AliveStreamData = null; // Accept any data as active stream, we need to handle the device type determination by sending command to it (Reset command for example).

            _hwCommunicationHelper.DiscardBuffers();
            _hwCommunicationHelper.Open();

            _isConnectionOpen = true;

            _disconnectNeedToFirstRais = !AutoComPortDetection;

            _hwCommunicationHelper.StringReceived -= hwCommunicationHelper_StringReceived;

            _hwCommunicationHelper.Alive -= hwCommunicationHelper_Alive;

            _hwCommunicationHelper.StringReceived += hwCommunicationHelper_StringReceived;

            _hwCommunicationHelper.Alive += hwCommunicationHelper_Alive;

            if (_connectionCheckerThread != null && _connectionCheckerThread.IsAlive)
                _connectionCheckerThread.Abort();

            if (_broadcastThread != null && _broadcastThread.IsAlive)
                _broadcastThread.Abort();

            _connectionCheckerThread = new Thread(ConnectionStatusCheckerThread) { IsBackground = true };

            _connectionCheckerThread.Start();

            return new ProcessResult { IsSucceed = true };
        }

        /// <summary>
        /// The Resetting thread.
        /// </summary>
        private void ConnectionResttingThread()
        {
            CloseReading();
            OpenReading();

            BroadcastMessage.Add("Ready...");

            if (ConnectionResetFinished != null)
                ConnectionResetFinished(this);

        }

        /// <summary>
        /// Processing after new reading.
        /// </summary>
        /// <param name="incomingReading"></param>
        private void AfterNewReading(int incomingReading)
        {
            lock (_lockReading)
            {
                if(!IsReadingOpened)
                    return;

                if( incomingReading == _currentReading)
                    return;

                if (ReadingStabilityEnabled && 
                    ((incomingReading < _currentReading && _currentReading - incomingReading > 0) 
                    || (incomingReading < _currentReading + ReadingStabilityRange && incomingReading > _currentReading - ReadingStabilityRange)))
                    return;
                    

                _iterationCount++;

                if (_currentReading != incomingReading)
                    _min = _currentReading;

                if (incomingReading > _max)
                {
                    _max = incomingReading;
                    _toppedIterationCount++;
                }

                _tint = _iterationCount - _toppedIterationCount;

                //if ((incomingReading < _currentReading && _currentReading - incomingReading > 20) || (incomingReading == _currentReading)) return;
                _currentReading = incomingReading;

                if (MeterValueChanged != null)
                    MeterValueChanged(this, _currentReading, _min, _max);
                _isReadingDoneNeedsRaise = true;
                _lastReadingDateTime = DateTime.Now;
            }

        }

        /// <summary>
        /// Handel the com events.
        /// </summary>
        void hwCommunicationHelper_Alive(object source)
        {
            try
            {
                if (!IsCsaEmdUnitConnected)
                {
                    _csaEmdUnitconnectedStartTime = DateTime.Now;
                    IsCsaEmdUnitConnected = true;
                    if (Connected != null)
                    {
                        Connected(this);
                    }
                }

                _lastComEventDateTime = DateTime.Now;
            }
            catch
            {

            }

        }

        /// <summary>
        /// Last reading checker.
        /// </summary>
        private void LastReadingCheckerThread()
        {
            //var test = new TimeSpan(0, 0, 0, 0, 800);
            //DateTime.Now.Subtract(_lastReadingDateTime.Value) >= test

            while (IsReadingOpened)
            {
                while (ReadingStabilityEnabled && _isReadingDoneNeedsRaise)
                {
                    lock (_lockReading)
                    {
                        if (_lastReadingDateTime.HasValue && _lastReadingDateTime.Value.AddMilliseconds(ReadingStabilityTimeout) < DateTime.Now)
                        {
                            _tint = _toppedIterationCount * 0.5;

                            if (_tint < 0.01)
                                _tint = _max / 99;

                            if (_tint < 0.01)
                                _tint = 1;

                            _upRate = _max / _tint; // Rais

                            if (_upRate > 99) _upRate = 99;

                            _dnValue = _max - _min; // Fall

                            _lastReadingDateTime = null;
                            _isReadingDoneNeedsRaise = false;

                            if (ReadingStabled != null)
                                ReadingStabled(this, _currentReading, _min, _max, (int)_dnValue, (int)_upRate);

                            Clear();
                        }

                        Thread.Sleep(5);
                    }
                }

                Thread.Sleep(5);
            }
        }

        /// <summary>
        /// Checking the connection status to keep it open.
        /// </summary>
        private void ConnectionStatusCheckerThread()
        {
            try
            {
                while (_isConnectionOpen)
                {
                    if (IsCsaEmdUnitConnected && _lastComEventDateTime.HasValue && _lastComEventDateTime.Value.AddMilliseconds(DisconnectedTimeout) < DateTime.Now)
                    {
                        IsCsaEmdUnitConnected = false;

                        if (Disconnected != null)
                        {
                            Disconnected(this);

                        }
                    }

                    Thread.Sleep(1100);

                    if (_disconnectNeedToFirstRais && !IsCsaEmdUnitConnected)
                    {
                        _disconnectNeedToFirstRais = false;

                        if (Disconnected != null)
                        {
                            Disconnected(this);

                        }
                    }
                }
            }
            catch
            {

            }
        }

       
        /// <summary>
        /// Auto connection thread method.
        /// </summary>
        private void AutoConnectionThread(object portSettings)
        {
            lock (_autoConnectionThread)
            {
                var portSettingsHelper = portSettings as PortSettingsHelper;

                if (portSettingsHelper == null)
                    return;

                var comPortsInfo = GetComPorts();

                foreach (var comPortInfo in comPortsInfo)
                {

                    if (IsConnectionOpen)
                        CloseConnection();

                    portSettingsHelper.PortNumber = comPortInfo.Id;

                    ComPortNumber = comPortInfo.Id;

                    OpenConnection(portSettingsHelper);

                    Thread.Sleep(AutoConnectionThreadInterval);

                    if (IsCsaEmdUnitConnected)
                        return;

                }

                if (Disconnected != null)
                    Disconnected(this);
            }
        }

        private void BroadcastThread()
        {
            while (_isConnectionOpen)
            {
                if (BroadcastMessage.Count > 0)
                {
                    try
                    {
                        try
                        {
                            lock (BroadcastMessage)
                            {
                                var toBroadcast = BroadcastMessage.FirstOrDefault();

                                if (toBroadcast == null)
                                    return;

                                _hwCommunicationHelper.DiscardBuffers();
                                _hwCommunicationHelper.Write(toBroadcast);
                                //Console.WriteLine(toBroadcast);
                                BroadcastMessage.Remove(toBroadcast);

                            }

                        }
                        catch
                        {

                        }
                    }
                    catch
                    {

                    }

                    Thread.Sleep(500);
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Handel the com data received events.
        /// </summary>
        void hwCommunicationHelper_StringReceived(object source, string strData)
        {
            try
            {
                lock (_lockDataReceived)
                {
                    // Get the response type from the received data.
                    AutoCSAResponse response;
                    if (AutoCSAProtocol.Responses.TryGetValue(strData, out response) && ResponseReceived != null)
                    {
                        // Check and avoid re-raising the event multiple times.
                        // The hardware may keep sanding the same last response.
                        if (!_lastReceivedResponse.HasValue || response != _lastReceivedResponse)
                        {
                            _lastReceivedResponse = response;
                            ResponseReceived(this, response, strData);
                        }
                    }
                    else
                    {
                        int readingValue;
                        if (int.TryParse(strData, out readingValue))
                            OnReadingReceived(readingValue);
                    }

                }
            }
            catch
            {

            }
            finally
            {
                _hwCommunicationHelper.DiscardBuffers();
            }
        }

        /// <summary>
        /// Handel the com events.
        /// </summary>
        private void OnReadingReceived(int reading)
        {
            try
            {
                //Avoid the startup shake reading. like 127, 31, 65
                if (IsCsaEmdUnitConnected && _csaEmdUnitconnectedStartTime.HasValue && _csaEmdUnitconnectedStartTime.Value.AddMilliseconds(1500) > DateTime.Now)
                    return;

                // Check if the reading is 0 and the raise the ReadingStopped if it was not raised.
                if (reading == 0)
                {
                    if (!_readingStoppedEventRaised)
                    {
                        if (ReadingStopped != null)
                            ReadingStopped(this);

                        _readingStoppedEventRaised = true;
                    }
                }
                else
                {
                    // Reset the _readingStoppedEventRaised flag when receive reading greater than 0.
                    _readingStoppedEventRaised = false;
                }

                if (reading >= MinimumReadingtoRegister && reading <= 100)
                {
                    AfterNewReading(reading);
                }

                _hwCommunicationHelper.DiscardBuffers();
            }
            catch
            {

            }
        }

        #endregion

    }
}
