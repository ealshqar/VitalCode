using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Users;
using Vital.Business.Shared.Shared;


namespace Vital.Business.Shared.DomainObjects
{
    #region Enums

    public enum DomainEntityState
    {
        New,
        Modified,
        Unchanged,
        Deleted,
        Temp
    }

    #endregion

    public class DomainEntity : INotifyPropertyChanged
    {
        #region Private Variables

        private int _id;
        private DomainEntityState _objectState;
        private string _errorSummary;
        private List<VitalValidationError> _validationErrors;
        private User _user;
        private DateTime _creationDateTime;
        private DateTime _updatedDateTime;
        private bool _isDeleted;

        #endregion

        #region Constructors
        
        /// <summary>
        /// The constructor.
        /// </summary>
        public DomainEntity()
        {
            _objectState = DomainEntityState.New;
            _errorSummary = string.Empty;
            ValidationErrors = new List<VitalValidationError>();
        }

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set 
            { 
                _id = value;
                NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Indicates if the record is new
        /// </summary>
        public bool IsNew
        {
            get
            {
                return Id == 0;
            }
        }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        /// <summary>
        /// Gets or sets the creation date time of an object.
        /// </summary>
        public DateTime CreationDateTime
        {
            get { return _creationDateTime; }
            set { _creationDateTime = value; }
        }

        /// <summary>
        /// Gets or sets the updated date time of an object.
        /// </summary>
        public DateTime UpdatedDateTime
        {
            get { return _updatedDateTime; }
            set { _updatedDateTime = value; }
        }

        /// <summary>
        /// Gets or sets the IsDeleted.
        /// </summary>
        public bool IsDeletedMemory
        {
            get { return _objectState == DomainEntityState.Deleted; }
        }

        /// <summary>
        /// Gets or sets the list of validation errors.
        /// </summary>
        public List<VitalValidationError> ValidationErrors
        {
            get { return _validationErrors; }
            set { _validationErrors = value; }
        }

        /// <summary>
        /// Gets or sets the error summary.
        /// </summary>
        public string ErrorSummary
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the is valid flag for the object.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return ValidationErrors.Count > 0 ? false : true;
            }
        }
        
        /// <summary>
        /// Gets or sets the object state.
        /// </summary>
        public DomainEntityState ObjectState
        {
            get
            {
                return _objectState;
            }
            set
            {
                _objectState = value;
            }
        }

        /// <summary>
        /// Gets if the current object has been changed or not.
        /// </summary>
        public bool IsChanged
        {
            get { return _objectState == DomainEntityState.New || _objectState == DomainEntityState.Modified; }
        }

        /// <summary>
        /// Gets or sets the IsDeleted.
        /// </summary>
        public bool IsDeleted
        {
            get
            {
                return _isDeleted;
            }
            set
            {
                if (!_isDeleted.Equals(value))
                {
                    _isDeleted = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies a property change.
        /// </summary>
        /// <param name="propertyName"></param>
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Internal Property Setting Methods

        /// <summary>
        /// Reset the object status.
        /// </summary>
        public void ResetStatus()
        {
            ObjectState = DomainEntityState.Unchanged;
        }

        /// <summary>
        /// Sets the state of the domain entity for lookups
        /// </summary>
        /// <param name="propertyName"></param>
        protected void SetModifiedStateLookup(string propertyName)
        {
            //This method is created to handle case for lookups or complex properties where the property name
            //comes as we send it in the method parameters ex. "StateLookup.Id", however in normal properties case
            //the property comes like this ex. "set_Name" because it gets generated using the method
            //"MethodBase.GetCurrentMethod().Name" in the set part of the property.

            //This method call with the parameter below makes sure the property name gets to the UI property changed
            //even like this "StateLookup.Id"
            SetModifiedState("set_" + propertyName);
        }

        /// <summary>
        /// Sets the state of the domain entity for lookups
        /// </summary>
        /// <param name="propertyName"></param>
        protected void SetModifiedEntity(string propertyName)
        {
            //This method is created to handle case for lookups or complex properties where the property name
            //comes as we send it in the method parameters ex. "StateLookup.Id", however in normal properties case
            //the property comes like this ex. "set_Name" because it gets generated using the method
            //"MethodBase.GetCurrentMethod().Name" in the set part of the property.

            //This method call with the parameter below makes sure the property name gets to the UI property changed
            //even like this "StateLookup.Id"
            SetModifiedState("set_" + propertyName);
        }

        /// <summary>
        /// Sets the state of the domain entity.
        /// </summary>
        /// <param name="propertyName"></param>
        public void SetModifiedState(string propertyName)
        {
            propertyName = propertyName.Substring(4);

            switch (_objectState)
            {
                case DomainEntityState.New:
                    break;
                case DomainEntityState.Modified:
                    break;
                case DomainEntityState.Unchanged:
                    ObjectState = DomainEntityState.Modified;
                    break;
                case DomainEntityState.Deleted:
                    break;//throw new InvalidOperationException(StaticKeys.AttemptToModifyADeletedObjectErrorMessage);
            }

            NotifyPropertyChanged(propertyName);
        }

        /// <summary>
        /// Checks if the property value was changed.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="parameter">The expression.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        protected bool CheckPropertyChanged<T>(Expression<Func<T>> parameter, ref T oldValue, ref T newValue)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler != null)
            {
                var parameterName = ExpressionHelper.GetParameterName(parameter);
                return CheckPropertyChanged(parameterName, ref oldValue, ref  newValue);
            }
            else
            {
                if (oldValue == null && newValue == null)
                {
                    return false;
                }

                if ((oldValue == null && newValue != null) || !oldValue.Equals((T)newValue))
                {
                    oldValue = newValue;
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Checks if the property value was changed.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="propertyName">The property name.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        protected bool CheckPropertyChanged<T>(string propertyName, ref T oldValue, ref T newValue)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler != null)
            {
                Check.Argument.IsNotEmpty(() => propertyName);

                if (oldValue == null && newValue == null)
                {
                    return false;
                }

                if ((oldValue == null && newValue != null) || !oldValue.Equals((T)newValue))
                {
                    oldValue = newValue;
                    NotifyPropertyChanged(propertyName);

                    DataStateChanged(DomainEntityState.Modified);
                    return true;
                }
            }
            else
            {
                if (oldValue == null && newValue == null)
                {
                    return false;
                }

                if ((oldValue == null && newValue != null) || !oldValue.Equals((T)newValue))
                {
                    oldValue = newValue;
                    DataStateChanged(DomainEntityState.Modified);
                    return true;
                }

            }

            return false;
        }

        protected bool CheckPropertyChanged<T>(ref T oldValue, ref T newValue)
        {
            if (!oldValue.Equals(newValue))
            {
                oldValue = newValue;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks the state of the domain entity.
        /// </summary>
        /// <param name="dataState"></param>
        private void DataStateChanged(DomainEntityState dataState)
        {
            if (ObjectState == DomainEntityState.Deleted && dataState != DomainEntityState.Deleted)
            {
                throw new InvalidOperationException(StaticKeys.AttemptToModifyADeletedObjectErrorMessage);
            }

            ObjectState = dataState;
        }

        #endregion

        #region Validation

        /// <summary>
        /// Checks if the date is valid.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="info">The error info.</param>
        /// <returns></returns>
        public  bool IsDatePropertyValid(string propertyName, DateTime propertyValue, ErrorInfo info)
        {
            if (propertyValue > new DateTime(1900, 1, 1) && (propertyValue < DateTime.MaxValue))
            {
                return true;
            }

            info.ErrorText = StaticKeys.ValidationMessageDate;
            info.ErrorType = ErrorType.Critical;

            return false;
        }

        /// <summary>
        /// Checks if the required value is provided.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="info">The error info.</param>
        /// <returns></returns>
        protected bool IsRequiredStringPropertyValid(string propertyName, string propertyValue, ErrorInfo info)
        {
            if (string.IsNullOrEmpty(propertyValue))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
                return false;
            }
            
            if (propertyValue.Trim() == string.Empty)
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField ;
                info.ErrorType = ErrorType.Critical;
                return false;
            }
            
            return true;
        }

        protected bool IsRequiredStringOrZeroPropertyValid(string propertyName, string propertyValue, ErrorInfo info)
        {
            if (string.IsNullOrEmpty(propertyValue))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
                return false;
            }
            else if (propertyValue.Trim() == string.Empty)
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
                return false;
            }
            else if (propertyValue.Trim() == "0")
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the value length meets the required length.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="propertyMaxLength">The maximum allowed length.</param>
        /// <param name="info">The error info.</param>
        /// <returns></returns>
        protected bool IsLimitedLengthStringPropertyValid(string propertyName, string propertyValue, int propertyMaxLength, ErrorInfo info)
        {
            bool isValid = true;

            if (!string.IsNullOrEmpty(propertyValue) && propertyValue.Trim() != string.Empty)
            {
                if (propertyValue.Length > propertyMaxLength)
                {
                    info.ErrorText = StaticKeys.ValidationMessageValueLength;
                    info.ErrorType = ErrorType.Critical;
                    isValid = false;
                }
            }

            return isValid;
        }
        
        /// <summary>
        /// Updates the list of the validation errors.
        /// </summary>
        /// <param name="info">The error info.</param>
        public void UpdateErrorsSummary(ErrorInfo info)
        {
            if(!string.IsNullOrEmpty(info.ErrorText))
                if (ValidationErrors != null)
                {
                    ValidationErrors.Add(new VitalValidationError() {Message = info.ErrorText, Type = info.ErrorType});
                }
        }

        /// <summary>
        /// Updates the list of the validation errors.
        /// </summary>
        /// <param name="info">The error info.</param>
        /// <param name="propertyName"></param>
        public void UpdateErrorsSummary(ErrorInfo info, string propertyName)
        {
            if (!string.IsNullOrEmpty(info.ErrorText))
                if (ValidationErrors != null)
                {
                    ValidationErrors.Add(new VitalValidationError() { PropertyName = propertyName, Message = info.ErrorText, Type = info.ErrorType });
                }
        }

        /// <summary>
        /// Validate on the base class level.
        /// </summary>
        /// <returns>If the object is valid or invalid.</returns>
        public virtual bool Validate()
        {
            return !IsChanged || IsValid;
        }

        #endregion
    }
}
