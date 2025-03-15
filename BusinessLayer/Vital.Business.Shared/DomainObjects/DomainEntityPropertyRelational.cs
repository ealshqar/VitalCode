using System.Reflection;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Properties;


namespace Vital.Business.Shared.DomainObjects
{
    public class DomainEntityPropertyRelational : DomainEntityRefluctionBase
    {
        #region Private Variables

        private Property _property;

        private object _value;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Value value.
        /// </summary>
        public override object Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Property.
        /// </summary>
        public Property Property
        {
            get { return _property; }
            set
            {
                _property = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the ListTypeLookup
        /// </summary>
        public override Lookup ValueTypeLookup
        {
            get { return Property == null ? null : Property.ValueTypeLookup; }
            set
            {
                Property.ValueTypeLookup = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }


        /// <summary>
        /// Get or set the Key.
        /// </summary>
        public override string Key
        {
            get { return Property == null ? null : Property.Key; }
            set
            {
                if (Property == null)
                    return;

                Property.Key = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the SourceConfig, that deals with the data source of the shown control by reflections.
        /// </summary>
        public override string SourceConfig
        {
            get { return Property == null ? null : Property.SourceConfig; }
            set
            {
                if (Property == null)
                    return;

                Property.SourceConfig = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the MembersConfig, that sets the control members by reflections.
        /// </summary>
        public override string MembersConfig
        {
            get { return Property == null ? null : Property.MembersConfig; }
            set
            {
                if (Property == null)
                    return;

                Property.MembersConfig = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the Caption, that sets the control caption depends by reflections.
        /// </summary>
        public override string Caption
        {
            get { return Property == null ? null : Property.Caption; }
            set
            {
                if (Property == null)
                    return;

                Property.Caption = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }
    
        #endregion

        #region Validation

        #endregion

        #region Event Handlers

        #endregion
    }
}
