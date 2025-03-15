using System.Reflection;

namespace Vital.Business.Shared.DomainObjects.Users
{
    public class User : DomainEntity
    {
        #region Private Variables

        private string _name;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        #endregion
    }
}
