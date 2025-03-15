using System.Reflection;
using Vital.Business.Shared.DomainObjects.Lookups;


namespace Vital.Business.Shared.DomainObjects
{

    public class DomainEntityRefluctionBase : DomainEntity
    {
        #region Private Variables

        private object _value;
        private object _defaultValue;
        private string _key;
        private Lookup _valueTypeLookup;
        private string _sourceConfig;
        private string _membersConfig;
        private string _caption;

        #endregion

        #region Constructors
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Get or set the Value.
        /// </summary>
        public virtual object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the Default Value.
        /// </summary>
        public virtual object DefaultValue
        {
            get { return _defaultValue; }
            set
            {
                _defaultValue = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the Key.
        /// </summary>
        public virtual string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the ListTypeLookup
        /// </summary>
        public virtual Lookup ValueTypeLookup
        {
            get { return _valueTypeLookup; }
            set
            {
                _valueTypeLookup = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the SourceConfig, that deals with the data source of the shown control by reflections.
        /// </summary>
        public virtual string SourceConfig
        {
            get { return _sourceConfig; }
            set
            {
                _sourceConfig = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the MembersConfig, that sets the control members by reflections.
        /// </summary>
        public virtual string MembersConfig
        {
            get { return _membersConfig; }
            set
            {
                _membersConfig = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the Caption, that sets the control caption depends by reflections.
        /// </summary>
        public virtual string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the UI Item Repository.
        /// </summary>
        public object UiItemRepository
        {
            get;
            set;
        }

        #endregion

        #region Events

        #endregion
    }
}
