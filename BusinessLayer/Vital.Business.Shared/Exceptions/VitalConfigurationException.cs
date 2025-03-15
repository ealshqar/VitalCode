using System;
using System.Runtime.Serialization;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.Exceptions
{
    /// <summary>
    /// This exception is thrown when a configuration setting is not found or is incorrect
    /// </summary>
    /// <remarks>
    /// This exception is used instead of the built in ConfigurationException because that one is not thrown when settings have
    /// wrong values or are not present (this is a known issue with .Net configuration classes). Also the built in exception has
    /// many properties that are not needed
    /// </remarks>
    [Serializable]
    public class VitalConfigurationException : Exception, ISerializable
    {
        #region Fields

        private readonly ConfigurationExceptionErrorCode _errorCode;
        private readonly string _settingName;
        private readonly string _settingValue;

        #endregion

        #region Properties
        
        /// <summary>
        /// Gets the configuration error code that identifies the exception
        /// </summary>
        public ConfigurationExceptionErrorCode ErrorCode
        {
            get
            {
                return _errorCode;
            }
        }

        /// <summary>
        /// Gets the name of the settings that caused the exception
        /// </summary>
        public string SettingName
        {
            get
            {
                return _settingName;
            }

        }

        /// <summary>
        /// Gets the value of the settings that caused the exception
        /// </summary>
        public string SettingValue
        {
            get
            {
                return _settingValue;
            }
        }


        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new Configuration exception
        /// </summary>
        /// <param name="configurationExceptionErrorCode">Error code that identifies the exception</param>
        /// <param name="settingName">Name of setting that caused the exception</param>
        /// <param name="settingValue">Value of setting that caused the exception</param>
        /// <param name="innerException">Original exception that occured during reading the configuration setting</param>
        public VitalConfigurationException(ConfigurationExceptionErrorCode configurationExceptionErrorCode, string settingName, string settingValue, Exception innerException)
            : base(GetErrorMessage(configurationExceptionErrorCode,settingName,settingValue), innerException)
        {
            _errorCode = configurationExceptionErrorCode;
            _settingName = settingName;
            _settingValue = settingValue;
        }

        /// <summary>
        /// Creates a new Configuration exception
        /// </summary>
        /// <param name="configurationExceptionErrorCode">Error code that identifies the exception</param>
        /// <param name="settingName">Name of setting that caused the exception</param>
        /// <param name="settingValue">Value of setting that caused the exception</param>
        public VitalConfigurationException(ConfigurationExceptionErrorCode configurationExceptionErrorCode, string settingName, string settingValue)
            : this(configurationExceptionErrorCode, settingName, settingValue, null)
        {
            
        }


        /// <summary>
        /// Creates a new Configuration exception
        /// </summary>
        /// <param name="configurationExceptionErrorCode">Error code that identifies the exception</param>
        /// <param name="settingName">Name of setting that caused the exception</param>
        /// <param name="innerException">Original exception that occured during reading the configuration setting</param>
        public VitalConfigurationException(ConfigurationExceptionErrorCode configurationExceptionErrorCode, string settingName, Exception innerException)
            : this(configurationExceptionErrorCode, settingName, null, innerException)
        {
            
        }

        /// <summary>
        /// Creates a new Configuration exception
        /// </summary>
        /// <param name="configurationExceptionErrorCode">Error code that identifies the exception</param>
        /// <param name="settingName">Name of setting that caused the exception</param>
        public VitalConfigurationException(ConfigurationExceptionErrorCode configurationExceptionErrorCode, string settingName)
            : this(configurationExceptionErrorCode, settingName, null, null)
        {

        }

        
        /// <summary>
        ///  Deserialization constructor
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected VitalConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context) 
        {
            _errorCode = (ConfigurationExceptionErrorCode)info.GetInt32(StaticKeys.ErrorCodePropertyName);
            _settingName = info.GetString(StaticKeys.SettingNamePropertyName);
            _settingValue = info.GetString(StaticKeys.SettingValuePropertyName);
        }

        #endregion

        #region ISerializable Members

        /// <summary>
        /// Serializes the exception state
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //serialize fields
            info.AddValue(StaticKeys.SettingValuePropertyName, _settingValue);
            info.AddValue(StaticKeys.SettingNamePropertyName, _settingName);
            info.AddValue(StaticKeys.ErrorCodePropertyName, _errorCode);

            //let the base serialize itself
            base.GetObjectData(info, context);

        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// helper methods to generate the exception message based on the error code
        /// </summary>
        /// <param name="configurationExceptionErrorCode">The configuration exception error code.</param>
        /// <param name="settingName">The setting name.</param>
        /// <param name="settingValue">The setting value.</param>
        /// <returns></returns>
        private static string GetErrorMessage(ConfigurationExceptionErrorCode configurationExceptionErrorCode, string settingName, string settingValue)
        {
            switch (configurationExceptionErrorCode)
            {
                case ConfigurationExceptionErrorCode.SettingNotFound:
                    return string.Format(StaticKeys.ConfigSettingNotFoundErrorMessage, settingName);
                case ConfigurationExceptionErrorCode.SettingValueIsInvalid:
                    return string.Format(StaticKeys.ConfigSettingValueIsInvalidErrorMessage, settingName, settingValue);
                case ConfigurationExceptionErrorCode.ConnectionStringSettingIsInvalid:
                    return string.Format(StaticKeys.ConnectionStringSettingIsInvalidErrorMessage, settingName);
                case ConfigurationExceptionErrorCode.ConnectionStringSettingHasInvalidDatabaseName:
                    return string.Format(StaticKeys.ConnectionStringSettingHasInvalidDatabaseNameErrorMessage, settingName);
                case ConfigurationExceptionErrorCode.ConnectionStringTimeout:
                    return string.Format(StaticKeys.ConnectionStringTimeoutSqlErrorMessage, settingName);
                default:
                    return string.Format(StaticKeys.GeneralConfigurationErrorMessage, settingName);
            }
        }

        #endregion
    }
}
