
using System.Reflection;
using Vital.Business.Shared.DomainObjects.Lookups;

namespace Vital.Business.Shared.DomainObjects.Settings
{
    public class Setting : DomainEntityRefluctionBase
    {
        #region Private Variables

        private string _name;
        private string _description;
        private Lookup _settingGroupLookup;
        private bool? _isVisible;

        #endregion

        #region Public Properties

        /// <summary>
        /// Get or set the Name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the Description.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the TypeLookup.
        /// </summary>
        public Lookup SettingGroupLookup
        {
            get { return _settingGroupLookup; }
            set
            {
                _settingGroupLookup = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets setting visibility.
        /// </summary>
        public bool? IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        #endregion
    }
}
