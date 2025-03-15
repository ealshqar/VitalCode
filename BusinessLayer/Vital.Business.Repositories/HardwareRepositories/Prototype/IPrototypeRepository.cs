using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.HardwareRepositories.Prototype
{
    public interface IPrototypeRepository
    {
        #region Events

        /// <summary>
        /// Event throws when the device being disconnected.
        /// </summary>
        event OnDisconnected Disconnected;

        /// <summary>
        /// Event throws when the device being connected.
        /// </summary>
        event OnConnected Connected;

        /// <summary>
        /// Event throws when a response was received from the prototype.
        /// </summary>
        event OnResponseReceived ResponseReceived;

        /// <summary>
        /// Event throws when trying to connect some port number.
        /// </summary>
        event OnDetecting Detecting;

        #endregion

        #region Public Properties

        /// <summary>
        /// Get the is connection open value.
        /// </summary>
        bool IsConnectionOpen { get; }

        /// <summary>
        /// Gets if the prototype Connected
        /// </summary>
        bool IsProtprtpeConnected { get; set; }

        /// <summary>
        /// The timeout (In milliseconds) of checking for prototype connection status.
        /// </summary>
        int DisconnectedTimeout { get; set; }

        /// <summary>
        /// The communication port number.
        /// </summary>
        int ComPortNumber { get; set; }

        /// <summary>
        /// Gets the current point command.
        /// </summary>
        string CurrentPointCommand { get; }

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
        ProcessResult OpenConnection(int comPortNumber, int baudRate, int dataBit, int timeout, bool dtr, bool rts, bool? isAutoPortDetection);

        /// <summary>
        /// Close the connection.
        /// </summary>
        /// <returns></returns>
        ProcessResult CloseConnection();

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
    /// Delegate for response handler method.
    /// </summary>
    /// <param name="sender"></param>
    public delegate void OnResponseReceived(object sender, PrototypeResponse response, string originData);

    /// <summary>
    /// Delegate for OnDetecting handle method.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="comPortNumber"></param>
    public delegate void OnDetecting(object sender, int comPortNumber);


    #endregion
}
