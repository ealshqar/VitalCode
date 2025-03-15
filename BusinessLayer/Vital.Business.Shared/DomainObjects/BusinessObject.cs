using System;
using System.ComponentModel;

namespace Vital.Business.Shared.DomainObjects
{

    
    [Obsolete]
    //The base class of all business objects, This class is not final. Removed all the validation related stuff.
    public abstract class BusinessObject : INotifyPropertyChanged
    {
        #region Constants

        //Error Messages
        const string ATTEMPT_TO_MODIFY_A_DELETED_OBJECT_ERROR_MESSAGE = "Attempt to modify a deleted object";
        const string ATTEMP_TO_DELETE_NEW_OBJECT_ERROR_MESSAGE = "Attemp to delete a new object that has not been saved in database";

       

        #endregion

        #region Fields

        private DomainEntityState _state;

        private string _errorsSummary = string.Empty;

        #endregion

        #region Properties

        public DomainEntityState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        public Boolean IsValid
        {
            get
            {
                return (ErrorsSummary == string.Empty);
            }
        }

        public string ErrorsSummary
        {
            get
            {
                return _errorsSummary;
            }
            set
            {
                _errorsSummary = value;
            }
        }

       

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Saved;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected void RaiseSavedEvent()
        {
            if (Saved != null)
            {
                Saved(this, new EventArgs());
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// creates an instance of the business object
        /// </summary>
        public BusinessObject()
        {
            //set new propeties
            _state = DomainEntityState.New;
        }

        #endregion
       
        #region Internal Property Setting Methods

        /// <summary>
        /// Sets the state of the internal state of the object to modified
        /// </summary>
        protected void SetModifiedState(string propertyName)
        {
            switch (_state)
            {
                case DomainEntityState.New:
                    //do nothing the object should stay new when you modify its properties
                    break;
                case DomainEntityState.Modified:
                    //do nothing, the state is already modified
                    break;
                case DomainEntityState.Unchanged:
                    _state = DomainEntityState.Modified;
                    break;

                case DomainEntityState.Deleted:
                    throw new InvalidOperationException(ATTEMPT_TO_MODIFY_A_DELETED_OBJECT_ERROR_MESSAGE);
            }
            NotifyPropertyChanged(propertyName);

        }

        #endregion

        #region Methods

        // Clone the object and set its status to new
        public virtual BusinessObject Clone()
        {
            return CloneNew();
        }

        internal virtual void CopyPropertiesFrom(BusinessObject source) { }
        internal virtual void SetParent(BusinessObject parent) { }
        internal abstract BusinessObject CloneNew();

        #endregion
    }
}
