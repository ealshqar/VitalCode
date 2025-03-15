using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.AppInfos;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.UI.Logic_Classes
{
    public class ProductSeatsHelper
    {
        #region Fields

        private static AppInfoManager _appInfoManager;
        private static ItemsManager _itemsManager;

        private static Lookup _productTypeLookup;
        private static Lookup _userNewItemSourceLookup;
        private static Lookup _userHiddenItemStateLookup;
        private static Lookup _systemHiddenItemStateLookup;
        private static Lookup _activeItemStateLookup;
        private static AppInfo _seatsNumberAppInfo;
        private static BindingList<Item> _products;
        private static bool _isInitialized;

        #endregion

        #region Properties

        /// <summary>
        /// SeatsNumberAppInfo
        /// </summary>
        public static AppInfo SeatsNumberAppInfo
        {
            get
            {
                ChecksInitialization();
                return _seatsNumberAppInfo;
            }
        }

        /// <summary>
        /// UserHiddenItemStateLookup
        /// </summary>
        public static Lookup UserHiddenItemStateLookup
        {
            get
            {
                ChecksInitialization();
                return _userHiddenItemStateLookup;
            }
        }

        /// <summary>
        /// SystemHiddenItemStateLookup
        /// </summary>
        public static Lookup SystemHiddenItemStateLookup
        {
            get
            {
                ChecksInitialization();
                return _systemHiddenItemStateLookup;
            }
        }

        /// <summary>
        /// ActiveItemStateLookup
        /// </summary>
        public static Lookup ActiveItemStateLookup
        {
            get
            {
                ChecksInitialization();
                return _activeItemStateLookup;
            }
        }

        /// <summary>
        /// Products
        /// </summary>
        public static BindingList<Item> Products
        {
            get
            {
                ChecksInitialization();
                return _products;
            }
        }

        /// <summary>
        /// Seats Number in DB
        /// </summary>
        public static int SeatsNumber
        {
            get
            {
                ChecksInitialization();
                int seatsNumber;

                if (SeatsNumberAppInfo == null || !int.TryParse(SeatsNumberAppInfo.Value, out seatsNumber))
                    return 0;

                return seatsNumber;
            }
        }

        /// <summary>
        /// Total number of Products
        /// </summary>
        public static int ProductsCount
        {
            get
            {
                ChecksInitialization();
                return Products == null ? 0 : Products.Count;
            }
        }

        /// <summary>
        /// Gets the active products count
        /// </summary>
        public static int ActiveProductsCount
        {
            get
            {
                ChecksInitialization();
                return Products == null || UserHiddenItemStateLookup == null ? 0 :
                                           Products.Count(i => i.IsItemActive(UserHiddenItemStateLookup.Id, SystemHiddenItemStateLookup.Id));
            }
        }

        /// <summary>
        /// Gets the remaining seats count
        /// </summary>
        public static int RemainingSeatsCount
        {
            get
            {
                ChecksInitialization();
                return SeatsNumber - ActiveProductsCount;
            }
        }

        /// <summary>
        /// Hidden Products Count
        /// </summary>
        public static int HiddenProductsCount
        {
            get
            {
                ChecksInitialization();
                return ProductsCount - ActiveProductsCount;
            }
        }

        /// <summary>
        /// AllowActiveOption
        /// </summary>
        public static bool AllowActiveOption
        {
            get
            {
                ChecksInitialization();
                return SeatsNumber - ActiveProductsCount > 0;
            }
        }

        /// <summary>
        /// NewProductItemState
        /// </summary>
        public static int NewProductItemState
        {
            get
            {
                ChecksInitialization();
                return RemainingSeatsCount > 0 ? ActiveItemStateLookup.Id : UserHiddenItemStateLookup.Id;
            }
        }

        #endregion

        #region Logic

        /// <summary>
        /// Returns if an item is active
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool IsItemActive(Item item)
        {
            ChecksInitialization();
            return item.IsItemActive(UserHiddenItemStateLookup.Id, SystemHiddenItemStateLookup.Id);
        }

        /// <summary>
        /// Gets if an item has child items with the option of filtering hidden items so they are not counted or to include them
        /// The option to filterHiddenItems is important and must be set to false when checking if item has childs before deleting it
        /// for example so we don't delete the item and get an exception while the item has childs in DB but all of them are hidden
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="filterHiddenChilds"></param>
        /// <returns></returns>
        public static bool ItemHasChilds(Item parent,bool filterHiddenChilds)
        {
            if (filterHiddenChilds)
            {
                return parent.Parents != null && parent.Parents.Count(i => i.Child != null && IsItemActive(i.Child)) > 0;
            }
            else
            {
                return parent.Parents != null && parent.Parents.Count > 0;
            }
        }

        /// <summary>
        /// Checks Initialization
        /// </summary>
        private static void ChecksInitialization()
        {
            if (!_isInitialized)
            {
                InitializeFields();
            }
        }

        /// <summary>
        /// Initialize Fields
        /// </summary>
        public static void InitializeFields()
        {
            _appInfoManager = new AppInfoManager();
            _itemsManager = new ItemsManager();

            _productTypeLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Product));
            _userNewItemSourceLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SourceType, SourceTypeEnum.UserNewItem));
            _userHiddenItemStateLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemState, ItemStateEnum.UserHidden));
            _systemHiddenItemStateLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemState, ItemStateEnum.SystemHidden));
            _activeItemStateLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemState, ItemStateEnum.Active));
            
            _seatsNumberAppInfo = _appInfoManager.GetAppInfoByProperty(new AppInfoFilter
            {
                Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.ProductsEntryCapacity)
            });
            _isInitialized = true;
        }

        /// <summary>
        /// Load User Products
        /// </summary>
        /// <returns></returns>
        public static BindingList<Item> LoadUserProducts()
        {
            _products = _itemsManager.GetItems(new ItemsFilter() { TypeLookupId = _productTypeLookup.Id, ItemSourceLookupId = _userNewItemSourceLookup.Id, LoadHiddenItems = true});

            return Products;
        }

        #endregion
    }
}
