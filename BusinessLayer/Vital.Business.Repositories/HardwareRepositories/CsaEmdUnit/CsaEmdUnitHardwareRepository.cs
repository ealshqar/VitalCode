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
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.Hardware.Entities;
using Vital.Hardware.Helpers;

namespace Vital.Business.Repositories.HardwareRepositories.CsaEmdUnit
{
    public class CsaEmdUnitHardwareRepository : BaseRepository, ICsaEmdUnitRepository
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

        private DateTime? _lastReadingDateTime;

        private bool _isReadingOpened;

        private bool _isReadingDoneNeedsRaise;

        private Thread _connectionCheckerThread;

        private bool _isConnectionOpen;

        private DateTime? _lastComEventDateTime;

        private bool _isCsaEmdUnitConnected;

        private bool _releasedEventNeedsRaise;

        private int _iterationCount;

        private int _toppedIterationCount;

        private double _tint;

        private double _upRate;

        private double _dnValue;

        private int _min;

        private int _max;

        private readonly object _lockReading;

        private bool _disconnectNeedToFirstRais;

        private int _disconnectedTimeout;

        #endregion

        #region Constructors

        /// <summary>
        /// CsaEmdUnitHardwareRepository Constructor.
        /// </summary>
        public CsaEmdUnitHardwareRepository()
        {
            _lockReading = new object();
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
        /// Event throws when the reading being done.
        /// </summary>
        public event OnReadingDone ReadingDone;

        /// <summary>
        /// Event throws when the reading point being relased.
        /// </summary>
        public event OnReleased Released;

        /// <summary>
        /// Event throws when trying to connect some port number.
        /// </summary>
        public event OnDetecting Detecting;

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
        public bool IsResetting
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
        public int CsaDisconnectedTimeout
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
                return _debuggingLongHWDisconnectTimeoutEnabled ? 1500 : CsaDisconnectedTimeout;
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

            StopResetting();
            CloseReading();
            _hwCommunicationHelper.Close();
            _hwCommunicationHelper.ReadingReceived -= hwCommunicationHelper_ReadingReceived;
            _hwCommunicationHelper.Alive -= hwCommunicationHelper_Alive;

            if (_isConnectionOpen && _connectionCheckerThread != null && _connectionCheckerThread.IsAlive)
            {
                _isConnectionOpen = false;
                _isCsaEmdUnitConnected = false;
                _csaEmdUnitconnectedStartTime = null;
                if (_connectionCheckerThread.IsAlive)
                    _connectionCheckerThread.Abort();
                
            }

            return new ProcessResult { IsSucceed = true };
        }

        /// <summary>
        /// Reset the connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult StartResetting()
        {
            if (_resetThread != null && _resetThread.IsAlive)
                return new ProcessResult
                {
                    IsSucceed = false,
                    Message = "Can not start resetting while another one in progress."
                };

            BroadcastMessage.Add("Resetting...");

            _currentReading = 0;

            _resetThread = new Thread(ResttingThread) { IsBackground = true };
            _resetThread.Start();

            return new ProcessResult { IsSucceed = true };
        }

        /// <summary>
        /// Stop the in progress resetting.
        /// </summary>
        /// <returns></returns>
        public ProcessResult StopResetting()
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
        /// <param name="flush">Flush the Buffer before add messages to broadcast.</param>
        /// <returns></returns>
        public ProcessResult Broadcast(string message)
        {
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
            catch (AutoMapperMappingException exception)
            {
                return new BindingList<ComPortInfo>();
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
            _hwCommunicationHelper.DataType = HwCommunicationHelper.HwDataTypeEnum.Reading;

            _hwCommunicationHelper.DiscardBuffers();
            _hwCommunicationHelper.Open();

            _isConnectionOpen = true;

            _disconnectNeedToFirstRais = !AutoComPortDetection;

            _hwCommunicationHelper.ReadingReceived -= hwCommunicationHelper_ReadingReceived;

            _hwCommunicationHelper.Alive -= hwCommunicationHelper_Alive;

            _hwCommunicationHelper.ReadingReceived += hwCommunicationHelper_ReadingReceived;

            _hwCommunicationHelper.Alive += hwCommunicationHelper_Alive;

            if (_connectionCheckerThread != null && _connectionCheckerThread.IsAlive)
                _connectionCheckerThread.Abort();

            _connectionCheckerThread = new Thread(ConnectionStatusCheckerThread) { IsBackground = true };

            _connectionCheckerThread.Start();

            return new ProcessResult { IsSucceed = true };
        }

        /// <summary>
        /// The Resetting thread.
        /// </summary>
        private void ResttingThread()
        {
            CloseReading();
            OpenReading();

            BroadcastMessage.Add("Ready...");

            if (ResettingFinished != null)
                ResettingFinished(this);

        }

        /// <summary>
        /// Processing after new reading.
        /// </summary>
        /// <param name="incomingReading"></param>
        private void AfterNewReading(int incomingReading)
        {
            lock (_lockReading)
            {
                if (_releasedEventNeedsRaise ||
                    (incomingReading < _currentReading && _currentReading - incomingReading > 20) ||
                    (incomingReading == _currentReading) ||
                    (incomingReading < _currentReading + ReadingStabilityRange && incomingReading > _currentReading - ReadingStabilityRange)) return;

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
        /// Handel the com events.
        /// </summary>
        void hwCommunicationHelper_ReadingReceived(object source, int reading)
        {
            try
            {
                //Avoid the startup shake reading. like 127, 31, 65
                if (IsCsaEmdUnitConnected && _csaEmdUnitconnectedStartTime.HasValue && _csaEmdUnitconnectedStartTime.Value.AddMilliseconds(1500) > DateTime.Now)
                    return;

                if (reading >= MinimumReadingtoRegister && reading <= 100)
                {
                    AfterNewReading(reading);
                }

                if (reading == 0)
                {
                    if (_releasedEventNeedsRaise)
                    {
                        _releasedEventNeedsRaise = false;
                        if (Released != null)
                            Released(this);
                    }

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
                    }
                }

                _hwCommunicationHelper.DiscardBuffers();
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
                while (_isReadingDoneNeedsRaise)
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


                            if (ReadingDone != null)
                                ReadingDone(this, _currentReading, _min, _max, (int)_dnValue, (int)_upRate);

                            _releasedEventNeedsRaise = true;
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
                    if (IsCsaEmdUnitConnected && _lastComEventDateTime.HasValue && _lastComEventDateTime.Value.AddMilliseconds(CsaDisconnectedTimeout) < DateTime.Now)
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

        #endregion


        /* - This code being never used, but we keep it as a reference for solving any problem in the new simulator code - */

        #region Fake stuff

        /// <summary>
        /// The Reading thread.
        /// </summary>
        // ToDo: Remove this method after check if we need the simulator mode anymore.
        private void ReadingThread()
        {
            //var isCurrentReadingChanged = false;

            //while (_isReadingOpened)
            //{
            //    if (UseSimulator)
            //    {
            //        if (_currentReading != 0 && _currentReading == _currentReading) return;

            //        var max = new Random(11).Next(CsaSimulatorReadingRangeMin, CsaSimulatorReadingRangeMax);
            //        var min = new Random(11).Next(CsaSimulatorReadingRangeMin, max);

            //        var incomingReading = new Random().Next(min, max);

            //        AfterNewReading(incomingReading);
            //    }

            //    if (_currentReading > _currentReading)
            //    {
            //        _currentReading = --_currentMetterReading;
            //        isCurrentReadingChanged = true;
            //    }
            //    else if (_currentReading < _currentReading)
            //    {
            //        _currentReading = ++_currentMetterReading;
            //        isCurrentReadingChanged = true;
            //    }

            //    if (isCurrentReadingChanged)
            //    {
            //        if (MeterValueChanged != null)
            //            MeterValueChanged(this, _currentReading, _min, _max);
            //        _isReadingDoneNeedsRaise = true;
            //        isCurrentReadingChanged = false;
            //        _lastReadingDateTime = DateTime.Now;
            //    }

            //    5 ms Under Performance testing ...
            //    Thread.Sleep(UseSimulator ? 100 : 3000);
            //}
        }
        //#region public Mehtods

        ///// <summary>
        ///// Open the connection Simulator. [ If the connection succeeded the process result will carry the true. Else, will throw an exception.]
        ///// </summary>
        ///// <param name="comPortNumber">The Port Number.</param>
        ///// <param name="baudRate">The BaudRate.</param>
        ///// <param name="dataBit">The DataBit</param>
        ///// <param name="timeout">The Timeout.</param>
        ///// <param name="dtr">The Dtr.</param>
        ///// <param name="rts">The Rts.</param>
        ///// <returns></returns>
        //public ProcessResult OpenConnectionSimulator(int comPortNumber, int baudRate, int dataBit, int timeout, bool dtr, bool rts)
        //{
        //    Check.Argument.IsNotNegative(comPortNumber, "comPortNumber");
        //    Check.Argument.IsNotNegative(baudRate, "baudRate");
        //    Check.Argument.IsNotNegative(dataBit, "dataBit");
        //    Check.Argument.IsNotNegative(timeout, "timeout");

        //    if (_rs232.IsOpen) return new ProcessResult() { IsSucceed = false, Message = "Can not open an opened connection" };


        //    _connectionCheckerThread = new Thread(ConnectionStatusCheckerThread) { IsBackground = true };

        //    _isConnectionOpen = true;

        //    _connectionCheckerThread.Start();

        //    return new ProcessResult() { IsSucceed = true };

        //}

        ///// <summary>
        ///// Close the connection - Simulator.
        ///// </summary>
        ///// <returns></returns>
        //public ProcessResult CloseConnectionSimulator()
        //{
        //    if (!_isConnectionOpen) return new ProcessResult() { IsSucceed = false, Message = "Can not close an closed connection" };

        //    if (_isConnectionOpen && _connectionCheckerThread != null && _connectionCheckerThread.IsAlive)
        //    {
        //        StopReading();
        //        StopResetting();
        //        _isConnectionOpen = false;
        //        if (_connectionCheckerThread.IsAlive)
        //            _connectionCheckerThread.Abort();
        //    }


        //    return new ProcessResult() { IsSucceed = true };
        //}

        ///// <summary>
        ///// Reset the connection - Simulator.
        ///// </summary>
        ///// <returns></returns>
        //public ProcessResult StartResettingSimulator()
        //{
        //    if (_resetThread != null && _resetThread.IsAlive)
        //        return new ProcessResult()
        //        {
        //            IsSucceed = false,
        //            Message = "Can not start resetting while another one in progress."
        //        };

        //    _broadcastMessage = "Resetting...";

        //    _currentReading = 0;

        //    _resetThread = new Thread(ResttingThread) { IsBackground = true };
        //    _resetThread.Start();

        //    return new ProcessResult() { IsSucceed = true };
        //}

        ///// <summary>
        ///// Stop the in progress resetting - Simulator.
        ///// </summary>
        ///// <returns></returns>
        //public ProcessResult StopResettingSimulator()
        //{
        //    if (_resetThread != null && _resetThread.IsAlive)
        //        return new ProcessResult()
        //        {
        //            IsSucceed = false,
        //            Message = "Can not stop not started resetting."
        //        };

        //    if (_resetThread == null)
        //    {
        //        return new ProcessResult()
        //        {
        //            IsSucceed = false,
        //            Message = "No resetting in progress yet."
        //        };
        //    }

        //    _broadcastMessage = "Ready...";

        //    if (_resetThread.IsAlive)
        //        _resetThread.Abort();

        //    return new ProcessResult() { IsSucceed = true };

        //}

        ///// <summary>
        ///// Starts the reading from the Simulator. 
        ///// </summary>
        ///// <returns></returns>
        //public ProcessResult StartReadingSimulator()
        //{
        //    if (_readingThread != null && _readingThread.IsAlive)
        //        return new ProcessResult()
        //        {
        //            IsSucceed = false,
        //            Message = "Reading already started."
        //        };

        //    _readingThread = new Thread(ReadingThread) { IsBackground = true };

        //    _lastReadingChekerThread = new Thread(LastReadingCheckerThread) { IsBackground = true };

        //    _isReadingOn = true;

        //    _readingThread.Start();

        //    _lastReadingChekerThread.Start();

        //    return new ProcessResult() { IsSucceed = true };
        //}

        ///// <summary>
        ///// Stops the reading from the device. 
        ///// </summary>
        ///// <returns></returns>
        //public ProcessResult StopReading()
        //{
        //    if (_readingThread != null && !_readingThread.IsAlive)
        //        return new ProcessResult()
        //        {
        //            IsSucceed = false,
        //            Message = "Can not stop not started reading."
        //        };

        //    if (_readingThread == null)
        //    {
        //        return new ProcessResult()
        //        {
        //            IsSucceed = false,
        //            Message = "No reading in progress yet."
        //        };
        //    }

        //    if (_readingThread.IsAlive)
        //        _readingThread.Abort();


        //    _isReadingOn = false;

        //    return new ProcessResult() { IsSucceed = true };
        //}

        ///// <summary>
        ///// Clear the Instance Caches and readings.
        ///// </summary>
        //public void Clear()
        //{
        //    _currentReading = 0;
        //    _currentMetterReading = 0;
        //    _currentReading = 0;
        //}

        ///// <summary>
        ///// Broadcast a message.
        ///// </summary>
        ///// <param name="message">The message.</param>
        ///// <returns></returns>
        //public ProcessResult Broadcast(string message)
        //{
        //    if (!IsConnectionOpen)
        //        return new ProcessResult()
        //                   {
        //                       IsSucceed = false,
        //                       Message = "Broadcasting failed, Connection is close."
        //                   };

        //    if (string.IsNullOrEmpty(message))
        //        return new ProcessResult()
        //                   {
        //                       IsSucceed = false,
        //                       Message = "Broadcast message is empty."
        //                   };

        //    //_rs232.Write(message);

        //    return new ProcessResult() {IsSucceed = true};
        //}

        //#endregion

        //#region Private Handlers And Threads

        ///// <summary>
        ///// The Resetting thread.
        ///// </summary>
        //private void ResttingThread()
        //{
        //    Thread.Sleep(3000);

        //    _broadcastMessage = "Ready...";

        //    if (ResettingFinished != null)
        //        ResettingFinished(this);

        //}

        ///// <summary>
        ///// The Reading thread.
        ///// </summary>
        //private void ReadingThread()
        //{
        //    while (_isReadingOn)
        //    {
        //        //Fake
        //        if (_currentReading == 0 || _currentReading != _currentReading)
        //        {
        //            var max = new Random(11).Next(30, 100);
        //            var min = new Random(11).Next(30, max);
        //            //</Fake

        //            _currentReading = new Random().Next(min, max);

        //            if (_currentReading > _currentReading)
        //            {
        //                _currentReading = --_currentMetterReading;
        //                _lastReadingDateTime = DateTime.Now; // Move this to the com events.
        //                _isReadingDoneNeedsRaise = true;
        //            }
        //            else if (_currentReading < _currentReading)
        //            {
        //                _currentReading = ++_currentMetterReading;
        //                _lastReadingDateTime = DateTime.Now; // Move this to the com events.
        //                _isReadingDoneNeedsRaise = true;
        //            }

        //            if (MeterValueChanged != null)
        //                MeterValueChanged(this, _currentReading);
        //        }

        //        Thread.Sleep(100);
        //    }
        //}

        ///// <summary>
        ///// Last reading checker.
        ///// </summary>
        //private void LastReadingCheckerThread()
        //{
        //    while (IsReadingOn)
        //    {
        //        while (_isReadingDoneNeedsRaise)
        //        {
        //            if (_lastReadingDateTime != null)
        //            {
        //                if (_lastReadingDateTime.Value.AddMilliseconds(1500) < DateTime.Now)
        //                {
        //                    ReadingDone(this, _currentMetterReading);
        //                    _lastReadingDateTime = null;
        //                    _isReadingDoneNeedsRaise = false;
        //                }
        //            }

        //            Thread.Sleep(1600);
        //        }

        //        Thread.Sleep(500);
        //    }
        //}

        ///// <summary>
        ///// Checking the connection status to keep it open.
        ///// </summary>
        //private void ConnectionStatusCheckerThread()
        //{
        //    try
        //    {
        //        while (_isConnectionOpen)
        //        {
        //            if (_lastReadingDateTime != null)
        //            {
        //                if (_lastReadingDateTime.Value.AddMilliseconds(1000) < DateTime.Now)
        //                {
        //                    //if (!_rs232.IsOpen) _rs232.Open();
        //                }
        //            }

        //            Thread.Sleep(1000);
        //        }
        //    }
        //    catch
        //    {
        //        if (Disconnected == null) return;
        //        Disconnected(this);
        //    }


        //}

        //#endregion

        #endregion

        /* ----------------------------------------------------------------------------------------------------------------*/

    }
}
