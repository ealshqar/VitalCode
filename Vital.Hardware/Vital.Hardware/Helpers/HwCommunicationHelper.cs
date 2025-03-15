using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using Vital.Hardware.Entities;

namespace Vital.Hardware.Helpers
{
    public class HwCommunicationHelper
    {
        #region Enums

        public enum HwDataTypeEnum
        {
            Reading,
            Hexadecimal,
            String
        }

        #endregion

        #region Events

        public event ReadingReceivedHandler ReadingReceived;

        public void InvokeReadingReceivedEvent(int reading)
        {
            if (ReadingReceived != null)
                ReadingReceived(this, reading);
        }

        public event HexadecimalReceivedHandler HexadecimalReceived;

        public void InvokeHexadecimalReceivedEvent(string hex)
        {
            if (HexadecimalReceived != null)
                HexadecimalReceived(this, hex);
        }

        public event StringReceivedHandler StringReceived;

        public void InvokeStringReceivedEvent(string str)
        {
            if (StringReceived != null)
                StringReceived(this, str);
        }

        public event AliveHandler Alive;

        public void InvokeAlive()
        {
            var handler = Alive;
            if (handler != null) handler(this);
        }

        #endregion

        #region Private Members

        private readonly SerialPort _serialPort;
        private readonly object _lockCommPort;
        private bool _hwConnected;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or set the port number.
        /// </summary>
        public int PortNumber
        {
            get
            {
                return _serialPort == null ? 0 : int.Parse(_serialPort.PortName.Replace("COM", ""));
            }
            set
            {
                _serialPort.PortName = string.Format("COM{0}", value);
            }
        }

        /// <summary>
        /// Gets or set the BaudRate.
        /// </summary>
        public int BaudRate
        {
            get
            {
                return _serialPort == null ? 0 : _serialPort.BaudRate;
            }
            set
            {
                _serialPort.BaudRate = value;
            }
        }

        /// <summary>
        /// Gets or set the DataBits.
        /// </summary>
        public int DataBits
        {
            get
            {
                return _serialPort == null ? 0 : _serialPort.DataBits;
            }
            set
            {
                _serialPort.DataBits = value;
            }
        }

        /// <summary>
        /// Gets or set the Timeout.
        /// </summary>
        public int Timeout
        {
            get
            {
                return _serialPort == null ? 0 : _serialPort.ReadTimeout;
            }
            set
            {
                _serialPort.ReadTimeout = value;
            }
        }

        /// <summary>
        /// Gets or set the Parity.
        /// </summary>
        public Parity Parity
        {
            get
            {
                return _serialPort == null ? Parity.None : _serialPort.Parity;
            }
            set
            {
                _serialPort.Parity = value;
            }
        }

        /// <summary>
        /// Gets or set the StopBits.
        /// </summary>
        public StopBits StopBits
        {
            get
            {
                return _serialPort == null ? StopBits.None : _serialPort.StopBits;
            }
            set
            {
                _serialPort.StopBits = value;
            }
        }

        /// <summary>
        /// Gets or set Dtr Enabled value.
        /// One of DTR or RTS should be set to true only.
        /// </summary>
        public bool Dtr
        {
            get
            {
                return _serialPort != null && _serialPort.DtrEnable;
            }
            set
            {
                _serialPort.DtrEnable = value;
            }
        }

        /// <summary>
        /// Gets or set Rts Enabled value.
        /// One of DTR or RTS should be set to true only.
        /// </summary>
        public bool Rts
        {
            get
            {
                return _serialPort != null && _serialPort.RtsEnable;
            }
            set
            {
                _serialPort.RtsEnable = value;
            }
        }

        /// <summary>
        /// Gets the Is Comm Port Open.
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return _serialPort != null && _serialPort.IsOpen;
            }
        }

        /// <summary>
        /// Gets or sets the data type.
        /// </summary>
        public HwDataTypeEnum DataType { get; set; }

        /// <summary>
        /// Gets or sets the alive stream data.
        /// </summary>
        public string AliveStreamData { get; set; }

        #endregion

        #region Constroctor

        /// <summary>
        /// Construct new HW connection helper
        /// </summary>
        public HwCommunicationHelper()
        {
            _serialPort = new SerialPort();
            _lockCommPort = new object();
            _serialPort.DataReceived += serialPort_DataReceived;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Open the connection base on this object properties.
        /// </summary>
        public bool Open()
        {
            try
            {
                lock (_lockCommPort)
                {
                    _serialPort.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Open the connection based on the passed parameters.
        /// </summary>
        public bool Open(int portNumber, int baudRate, int dataBits, Parity parity, StopBits stopBits)
        {
            try
            {
                lock (_lockCommPort)
                {
                    PortNumber = portNumber;
                    BaudRate = baudRate;
                    DataBits = dataBits;
                    Parity = parity;
                    StopBits = stopBits;

                    _serialPort.Open();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Close the current connection.
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                lock (_lockCommPort)
                {
                    if (_serialPort.IsOpen)
                    {
                        _serialPort.Close();
                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Write a string data to the hardware.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Write(string data)
        {
            try
            {

                switch (DataType)
                {
                    case HwDataTypeEnum.Hexadecimal:
                        return WriteHex(data);
                    case HwDataTypeEnum.Reading:
                    case HwDataTypeEnum.String:
                        return WriteString(data);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Write a string data to the hardware.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool WriteString(string data)
        {
            try
            {

                _serialPort.WriteLine(data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Write an string as a Hex to the hardware.
        /// </summary>
        /// <param name="data"></param>
        public bool WriteHex(string data)
        {
            try
            {
                var dataHexBytes = new[] {Convert.ToByte(data, 16)};

                _serialPort.Write(dataHexBytes, 0, dataHexBytes.Length);

                return true;
            }
            catch
            {
                return false;
            }



        }

        /// <summary>
        /// Discard the buffers data of reading and writing.
        /// </summary>
        /// <returns></returns>
        public bool DiscardBuffers()
        {
            try
            {
                _serialPort.DiscardOutBuffer();
                _serialPort.DiscardInBuffer();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a list of port names.
        /// </summary>
        /// <returns></returns>
        public List<ComPortInfoEntity> GetComPorts()
        {
            try
            {
                // Get a list of serial port names.
                var ports = SerialPort.GetPortNames();

                var portsList = new List<ComPortInfoEntity>();

                portsList.AddRange(ports.Select(p => new ComPortInfoEntity()
                {
                    Id = int.Parse(p.ToLower().Replace("com", "")),
                    Name = p
                }));

                return portsList;

            }
            catch (Exception)
            {
                return new List<ComPortInfoEntity>();
            }

        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handel the data received from the comm event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                switch (DataType)
                {
                    case HwDataTypeEnum.Reading:
                    case HwDataTypeEnum.Hexadecimal:

                        var stringBuffer = new StringBuilder();

                        for (int i = 0; i < _serialPort.BytesToRead; i++)
                        {
                            var byt = _serialPort.ReadByte();

                            stringBuffer.Append(byt.ToString("X2"));

                            ProcessReceivedData(stringBuffer.ToString());
                        }

                        break;
                    case HwDataTypeEnum.String:
                        var dataStr = _serialPort.ReadExisting();
                        var dataArray = dataStr.Trim('\n').Trim('\r').Split('\n').SelectMany(d => d.Split('\r')).Where(d => !string.IsNullOrEmpty(d));
                        foreach (var data in dataArray)
                        {
                            ProcessReceivedData(data);
                        }
                        
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            }
            catch 
            {

            }

        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Process the incoming data.
        /// </summary>
        /// <param name="data">the data.</param>
        private void ProcessReceivedData(string data)
        {
            // Check for alive stream: if the HW is not connected yet and the AliveStreamData was set, check if the received data matches the AliveStreamData.
            if (!_hwConnected && !string.IsNullOrEmpty(AliveStreamData) && !data.Equals(AliveStreamData))
                return;

            switch (DataType)
            {
                case HwDataTypeEnum.Reading:
                    var commingValueIndex = Array.IndexOf(DataParserHelpers.ValueTranslate, data);
                    int tempCommingValue;
                    if (commingValueIndex != -1 || (int.TryParse(data, out tempCommingValue) && tempCommingValue == 0))
                    {
                        var incomingReading = commingValueIndex/2;
                        InvokeReadingReceivedEvent(incomingReading);
                    }
                    break;
                case HwDataTypeEnum.Hexadecimal:
                    InvokeHexadecimalReceivedEvent(data);
                    break;
                case HwDataTypeEnum.String:
                    if (!string.IsNullOrWhiteSpace(data))
                        InvokeStringReceivedEvent(data);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _hwConnected = true;
            InvokeAlive();
        }

    #endregion
    }

    #region Event Handler delegates

    public delegate void ReadingReceivedHandler(object sender, int reading);
    public delegate void HexadecimalReceivedHandler(object sender, string hex);
    public delegate void StringReceivedHandler(object sender, string str);
    public delegate void AliveHandler(object sender);

    #endregion

    
}
