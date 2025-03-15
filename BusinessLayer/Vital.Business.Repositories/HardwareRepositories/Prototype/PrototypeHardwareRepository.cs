using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using AutoMapper;
using Vital.Business.Repositories.Helpers;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.Hardware;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;
using Vital.Hardware.Entities;
using Vital.Hardware.Helpers;
using PrototypeProtocol = Vital.Business.Repositories.Shared.PrototypeProtocol;

namespace Vital.Business.Repositories.HardwareRepositories.Prototype
{
    public class PrototypeHardwareRepository : BaseRepository, IPrototypeRepository
    {
        #region Private Members

        private readonly HwCommunicationHelper _hwCommunicationHelper;

        private readonly bool _debuggingLongHWDisconnectTimeoutEnabled;

        private Thread _connectionCheckerThread;

        private Thread _autoConnectionThread;

        private bool _isConnectionOpen;

        private DateTime? _lastComEventDateTime;

        private bool _isProtprtpeConnected;

        private readonly object _lockDataSend;

        private readonly object _lockDataReceived;

        private bool _disconnectNeedToFirstRais;

        private int _disconnectedTimeout;

        #endregion

        #region Constructors

        /// <summary>
        /// PrototypeHardwareRepository Constructor.
        /// </summary>
        public PrototypeHardwareRepository()
        {
            _lockDataSend = new object();
            _lockDataReceived = new object();
            _hwCommunicationHelper = new HwCommunicationHelper();
            _debuggingLongHWDisconnectTimeoutEnabled = Debugger.IsAttached && ConfigurationManager.AppSettings[ConfigKeys.EnableDebuggingLongHWDisconnectTimeout.ToString()].ToBoolean();

        }

        #endregion

        #region Events

        /// <summary>
        /// Event throws when the device being disconnected.
        /// </summary>
        public event OnDisconnected Disconnected;

        /// <summary>
        /// Event throws when the device being connected.
        /// </summary>
        public event OnConnected Connected;

        /// <summary>
        /// Event throws when a response was received from the prototype.
        /// </summary>
        public event OnResponseReceived ResponseReceived;

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
        /// Gets if the prototype Connected
        /// </summary>
        public bool IsProtprtpeConnected
        {
            get
            {
                return _isProtprtpeConnected;
            }
            set
            {
                _isProtprtpeConnected = value;
            }
        }

        /// <summary>
        /// The timeout (In milliseconds) of checking for prototype connection status.
        /// </summary>
        public int DisconnectedTimeout {
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
        /// Gets the current point command.
        /// </summary>
        public string CurrentPointCommand { get; private set; }

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
                return _debuggingLongHWDisconnectTimeoutEnabled ? 1500 : DisconnectedTimeout;
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
        /// <param name="isAutoPortDetection">Auto detect the port. </param
        /// <returns></returns>
        public ProcessResult OpenConnection(int comPortNumber, int baudRate, int dataBit, int timeout, bool dtr, bool rts, bool? isAutoPortDetection)
        {
            Check.Argument.IsNotNegative(comPortNumber, "comPortNumber");
            Check.Argument.IsNotNegative(baudRate, "baudRate");
            Check.Argument.IsNotNegative(dataBit, "dataBit");
            Check.Argument.IsNotNegative(timeout, "timeout");

            if (_hwCommunicationHelper.IsOpen && IsProtprtpeConnected)
            {
                if (Connected != null)
                    Connected(this);

                return new ProcessResult { IsSucceed = false, Message = "Can not open an opened connection" };
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
        /// Close the connection.
        /// </summary>
        /// <returns></returns>
        public ProcessResult CloseConnection()
        {
            if (!_isConnectionOpen && !_hwCommunicationHelper.IsOpen)
                return new ProcessResult { IsSucceed = false, Message = "Can not close an closed connection" };

            
            _hwCommunicationHelper.StringReceived -= hwCommunicationHelper_StringReceived;
            _hwCommunicationHelper.Alive -= hwCommunicationHelper_Alive;
            _hwCommunicationHelper.Close();

            if (_isConnectionOpen && _connectionCheckerThread != null && _connectionCheckerThread.IsAlive)
            {
                _isConnectionOpen = false;
                _isProtprtpeConnected = false;

                if (_connectionCheckerThread.IsAlive)
                    _connectionCheckerThread.Abort();

                if (Disconnected != null)
                    Disconnected(this);

            }

            return new ProcessResult { IsSucceed = true };
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
                Detecting(this, portSettings.PortNumber);

            ComPortNumber = portSettings.PortNumber;
            _hwCommunicationHelper.BaudRate = portSettings.BaudRate;
            _hwCommunicationHelper.DataBits = portSettings.DataBits;
            _hwCommunicationHelper.Rts = portSettings.Rts;
            _hwCommunicationHelper.Dtr = portSettings.Dtr;
            _hwCommunicationHelper.DataType = HwCommunicationHelper.HwDataTypeEnum.String;
            _hwCommunicationHelper.AliveStreamData = "E";

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

            _connectionCheckerThread = new Thread(ConnectionStatusCheckerThread) { IsBackground = true };

            _connectionCheckerThread.Start();

            return new ProcessResult { IsSucceed = true };
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

                    if (IsProtprtpeConnected)
                        return;

                }

                if (Disconnected != null)
                    Disconnected(this);
            }
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
        /// Handel the com events.
        /// </summary>
        void hwCommunicationHelper_Alive(object source)
        {
            try
            {
                if (!IsProtprtpeConnected)
                {
                    IsProtprtpeConnected = true;
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
        /// Handel the com data received events.
        /// </summary>
        void hwCommunicationHelper_StringReceived(object source, string strData)
        {
            try
            {
                lock (_lockDataReceived)
                {
                    // Check and avoid the alive stream.
                    if (strData.Equals(PrototypeProtocol.AliveStreamData))
                        return;

                    // Get the response type from the received data.
                    PrototypeResponse response;
                    if (!PrototypeProtocol.Responses.TryGetValue(strData, out response)) 
                        return;

                    if (ResponseReceived != null)
                        ResponseReceived(this, response, strData);
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
        /// Checking the connection status to keep it open.
        /// </summary>
        private void ConnectionStatusCheckerThread()
        {
            try
            {
                while (_isConnectionOpen)
                {
                    if (IsProtprtpeConnected && _lastComEventDateTime.HasValue && _lastComEventDateTime.Value.AddMilliseconds(DisconnectedTimeout) < DateTime.Now)
                    {
                        IsProtprtpeConnected = false;

                        if (Disconnected != null)
                        {
                            Disconnected(this);

                        }
                    }

                    Thread.Sleep(1100);

                    if (_disconnectNeedToFirstRais && !IsProtprtpeConnected)
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

        #endregion
    }
}
