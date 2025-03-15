using System.Reflection;
using Vital.Business.Shared.DomainObjects.Lookups;

namespace Vital.Business.Shared.DomainObjects.Items
{
    public class ItemTarget : DomainEntity
    {
        #region Private Variables

        private Lookup _targetTypeLookup;
        private Item _item;
        private int _order;

        #endregion

        #region Public Properties

        /// <summary>
        /// Get Or set the TargetTypeLookup value.
        /// </summary>
        public Lookup TargetTypeLookup
        {
            get { return _targetTypeLookup; }
            set
            {
                _targetTypeLookup = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the Item value.
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

        /// <summary>
        /// Gets or sets the Order.
        /// </summary>
        public int Order
        {
            get { return _order; }
            set
            {
                _order = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        #endregion
    }
}
