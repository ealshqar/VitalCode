using System.Reflection;

namespace Vital.Business.Shared.DomainObjects.AppInfos
{
    public class AppInfo : DomainEntity
    {
        #region Private Variables

        private string _property;
        private string _value;

        #endregion

        #region Public Properties

        /// <summary>
        /// Get or set the property.
        /// </summary>
        public string Property
        {
            get { return _property; }
            set
            {
                _property = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the value.
        /// </summary>
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        #endregion        
    }
}
