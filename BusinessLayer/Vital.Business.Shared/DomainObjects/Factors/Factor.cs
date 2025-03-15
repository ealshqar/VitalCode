using System.Reflection;
using Vital.Business.Shared.DomainObjects.Items;

namespace Vital.Business.Shared.DomainObjects.Factors
{
    public class Factor : DomainEntity
    {
        #region Private Variables

        private Item _item;
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Get or set the Item.
        /// </summary>
        public Item Item
        {
            get { return _item; }
            set
            {
                _item = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        #endregion
    }
}
