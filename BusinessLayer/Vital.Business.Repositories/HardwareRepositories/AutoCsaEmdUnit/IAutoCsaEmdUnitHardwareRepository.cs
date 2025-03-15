using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.Hardware;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.HardwareRepositories.AutoCsaEmdUnit
{
    public interface IAutoCsaEmdUnitHardwareRepository
    {
        #region Events

        /// <summary>
        /// Event throws when some reading ready for show.
        /// </summary>
        event MeterValueChangedHandle MeterValueChanged;

        /// <summary>
        /// Event throws when the connection reset Finished
        /// </summary>
        event OnConnectionResetFinishedHandle ConnectionResetFinished;

        /// <summary>
        /// Event throws when the device being disconnected.
        /// </summary>
        event OnDisconnected Disconnected;

        /// <summary>
        /// Event throws when the device being connected.
        /// </summary>
        event OnConnected Connected;

        /// <summary>
        /// Event throws when the reading being stabled.
        /// </summary>
        event OnReadingStabled ReadingStabled;

        /// <summary>
        /// Event throws when the reading is opened and the HW stopped sending readings.
        /// </summary>
        event OnReadingStopped ReadingStopped;

        /// <summary>
        /// Event throws when trying to connect some port number.
        /// </summary>
        event OnDetecting Detecting;

        /// <summary>
        /// Event throws when a response was received from the prototype.
        /// </summary>
        event OnResponseReceived ResponseReceived;

        #endregion

        #region Public Properties

        /// <summary>
        /// Get the is connection open value.
        /// </summary>
        bool IsConnectionOpen { get; }

        /// <summary>
        /// Get is reading on value.
        /// </summary>
        bool IsReadingOpened { get; }

        /// <summary>
        /// Get the is connection resetting value.
        /// </summary>
        bool IsConnectionResetting { get; }

        /// <summary>
        /// Gets if the Csa Emd Unit Connected
        /// </summary>
        bool IsCsaEmdUnitConnected { get; set; }

        /// <summary>
        /// Gets or sets the Minimum Reading to Register value.
        /// </summary>
        int MinimumReadingtoRegister { get; set; }

        /// <summary>
        /// The timeout (In milliseconds) of checking for CSA connection status.
        /// </summary>
        int DisconnectedTimeout { get; set; }

        /// <summary>
        /// Determines if the reading stability feature is enabled or not.
        /// </summary>
        bool ReadingStabilityEnabled { get; set; }

        /// <summary>
        /// Stability period (In milliseconds) before reading being done.
        /// </summary>
        int ReadingStabilityTimeout { get; set; }

        /// <summary>
        /// Minimum difference between readings to register a new one..
        /// </summary>
        int ReadingStabilityRange { get; set; }

        /// <summary>
        /// The communication port number.
        /// </summary>
        int ComPortNumber { get; set; }

        /// <summary>
        /// Returns true if the csa unit has a reading.
        /// </summary>
        bool HasReading { get; }

        /// <summary>
        /// Gets broadcasting status.
        /// </summary>
        bool IsBroadcasting { get; }

        /// <summary>
        /// Gets auto com port detection 
        /// </summary>
        bool AutoComPortDetection { get; set; }

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
        ProcessResult OpenConnection(int comPortNumber, int baudRate, int dataBit, int timeout, bool dtr, bool rts,
            bool? isAutoPortDetection);

        /// <summary>
        /// Cancel the auto connection detection operation.
        /// </summary>
        /// <returns></returns>
        ProcessResult CancelAutoDetection();

        /// <summary>
        /// Close the connection.
        /// </summary>
        /// <returns></returns>
        ProcessResult CloseConnection();

        /// <summary>
        /// Reset the connection.
        /// </summary>
        /// <returns></returns>
        ProcessResult StartConnectionReset();

        /// <summary>
        /// Stop the in progress resetting.
        /// </summary>
        /// <returns></returns>
        ProcessResult StopConnectionReset();

        /// <summary>
        /// Starts the reading from the device. 
        /// </summary>
        /// <returns></returns>
        ProcessResult OpenReading();

        /// <summary>
        /// Stops the reading from the device. 
        /// </summary>
        /// <returns></returns>
        ProcessResult CloseReading();

        /// <summary>
        /// Clear the Instance Caches and readings.
        /// </summary>
        void Clear();

        /// <summary>
        /// Broadcast a message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        ProcessResult Broadcast(string message);

        /// <summary>
        ///Flush the Broadcasting Buffer.
        /// </summary>
        /// <returns></returns>
        ProcessResult FlushBroadcastBuffer();

        /// <summary>
        /// Gets the com ports.
        /// </summary>
        /// <returns></returns>
        BindingList<ComPortInfo> GetComPorts();

        /// <summary>
        /// Set the testing point.
        /// </summary>
        /// <param name="pointHex">The point as hex string.</param>
        /// <param name="forceSet">Force to set the point if its the same as current.</param>
        /// <returns></returns>
        ProcessResult SetPoint(string pointHex, bool forceSet = false);

        /// <summary>
        /// Send an command for the hardware.
        /// </summary>
        /// <param name="command">The command</param>
        /// <returns></returns>
        ProcessResult SendCommand(string command);

        #endregion
    }

    #region Event Handler Delegates

    /// <summary>
    /// Delegate for OnConnectionResetFinishedHandle handler method.
    /// </summary>
    /// <param name="sender"></param>
    public delegate void OnConnectionResetFinishedHandle(object sender);

    /// <summary>
    /// Delegate for MeterValueChangedEvent handler method.
    /// </summary>
    /// <param name="sender">The Sender.</param>
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
    /// Delegate for OnReadingStabled handler method.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="reading">last reading.</param>
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
    /// Delegate for OnDetecting handle method.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="comPortNumber"></param>
    public delegate void OnDetecting(object sender, int comPortNumber);

    /// <summary>
    /// Delegate for response handler method.
    /// </summary>
    /// <param name="sender"></param>
    public delegate void OnResponseReceived(object sender, AutoCSAResponse response, string originData);


    #endregion
}