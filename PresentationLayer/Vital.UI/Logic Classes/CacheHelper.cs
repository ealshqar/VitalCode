using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Vital.Business.Managers;
using Vital.Business.Shared.Caching;
using Vital.Business.Shared.DomainObjects.AppInfos;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Services;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.DomainObjects.TestProtocols;
using Vital.Business.Shared.DomainObjects.VitalForceSheet;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.UI.Logic_Classes
{
	public class CacheHelper
	{
	    /// <summary>
		/// Sets or gets the data into/from cache.
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <returns></returns>
		public static object SetOrGetCachableData(CachingFilter filter)
		{
			var data = new object();

            switch (filter.InformationType)
            {
                case CachableDataEnum.ItemsGroup:

                    return SetOrGetCachedItemsGroups();

                case CachableDataEnum.VisibleSettings:

                    return SetOrGetCachedSettings(CachableDataEnum.VisibleSettings);

                case CachableDataEnum.Settings:

                    return SetOrGetCachedSettings(CachableDataEnum.Settings);

                case CachableDataEnum.HwProfileSettings:

                    return SetOrGetCachedSettings(CachableDataEnum.HwProfileSettings);

                case CachableDataEnum.HwProfiles:

                    return SetOrGetCachedHwProfiles();

                case CachableDataEnum.PrintingSettings:
                    return SetOrGetCachedSettings(CachableDataEnum.PrintingSettings);

                case CachableDataEnum.ShippingOrderSettings:
                    return SetOrGetCachedSettings(CachableDataEnum.ShippingOrderSettings);

                case CachableDataEnum.BackupAndRestore:
                    return SetOrGetCachedSettings(CachableDataEnum.BackupAndRestore);

                case CachableDataEnum.ShippingOrdersTimeInfo:
                    return SetOrGetCachedShippingOrdersTimeInfo(); 

                case CachableDataEnum.PointsList:

                    return SetOrGetCachedPointsList();

                case CachableDataEnum.TestProtocols:

                    return SetOrGetCachedTestProtocols();

                case CachableDataEnum.TestStates:

                    return SetOrGetCachedTestStates();

                case CachableDataEnum.EvalPeriodTypes:

                    return SetOrGetCachedEvalPeriodTypes();

                case CachableDataEnum.TestTypes:

                    return SetOrGetCachedTestTypes();

                case CachableDataEnum.Lookups:

                    return SetOrGetCachedLookups();
                    
                case CachableDataEnum.DataManagementItems:

                    return SetOrGetCachedItems(filter);

                case CachableDataEnum.SingleValues:

                    return SetOrGetCachedSingleValue(filter);

                case CachableDataEnum.DBVersion:

                    return SetOrGetCachedDBVersion();
                case CachableDataEnum.Logo:

                    return SetOrGetCachedLogo();

                case CachableDataEnum.FourFactors:

                    return SetOrGetCachedFourFactors();
                
                case CachableDataEnum.FourCauses:

                    return SetOrGetCachedFourCauses();

                case CachableDataEnum.Ratios:

                    return SetOrGetCachedRatios();

                case CachableDataEnum.Products:

                    return SetOrGetCachedProducts();

                case CachableDataEnum.AllProducts:

                    return SetOrGetCachedAllProducts();

                case CachableDataEnum.CustomDilutions:

                    return SetOrGetCachedCustomDilutions();

                case CachableDataEnum.VitalForce:

                    return SetOrGetCachedVitalForce();

                case CachableDataEnum.Top10:

                    return SetOrGetCachedTop10();

                case CachableDataEnum.GenericList:

                    return SetOrGetCachedGenericList();
                case CachableDataEnum.FrequencyList:
                    
                    return SetOrGetCachedFrequencyList();

                case CachableDataEnum.Services:
                    
                    return SetOrGetCachedServicesList();

                case CachableDataEnum.ShippingMethod:

                    return SetOrGetCachedShippingMethod();

                case CachableDataEnum.VFSItemsSource:

                    return SetOrGetCachedVFSItemsSourceList();

                case CachableDataEnum.VFSSecondaryItemsSource:

                    return SetOrGetCachedVFSSecondaryItemSourceList();

                case CachableDataEnum.AllItems:

                    return SetOrGetCachedSubstanceItems();

                case CachableDataEnum.AllSubstance:
                    return SetOrGetCachedItemsByType(filter.InformationType, ItemTypeEnum.Substance);
                case CachableDataEnum.AllPotency:
                    return SetOrGetCachedItemsByType(filter.InformationType, ItemTypeEnum.Potency);
                case CachableDataEnum.AllBacteria:
                    return SetOrGetCachedItemsByType(filter.InformationType, ItemTypeEnum.Bacteria);
                case CachableDataEnum.VitalEmail:
                    return SetOrGetCachedVitalEmail();
            }

		    return data;
		}

        /// <summary>
        /// Sets or gets the cached shipping email.
        /// </summary>
        /// <returns></returns>
        private static object SetOrGetCachedVitalEmail()
        {
            var appInfoManager = new AppInfoManager();

            var key = CachableDataEnum.VitalEmail.ToString();

            VitalEmail vitalEmail;

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                vitalEmail = (VitalEmail)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                vitalEmail = new VitalEmail
                {
                    ShippingTargetEmail = UiHelperClass.Decrypt(appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.ShippingTargetEmail)),
                    FeedbackTargetEmail = UiHelperClass.Decrypt(appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.FeedbackTargetEmail)),
                    ShippingSenderEmail = UiHelperClass.Decrypt(appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.ShippingSenderEmail)),
                    ShippingSenderPass = UiHelperClass.Decrypt(appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.ShippingSenderPass)),
                    FeedbackSenderEmail = UiHelperClass.Decrypt(appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.FeedbackSenderEmail)),
                    FeedbackSenderPass = UiHelperClass.Decrypt(appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.FeedbackSenderPass)),
                    ExceptionSenderEmail = UiHelperClass.Decrypt(appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.ExceptionSenderEmail)),
                    ExceptionSenderPass = UiHelperClass.Decrypt(appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.ExceptionSenderPass))
                };

                CachingManager.PutInCache(vitalEmail, CachableDataEnum.VitalEmail.ToString());
            }

            return vitalEmail;
        }

	    /// <summary>
		/// Sets or gets the value into/from cache.
		/// </summary>
		/// <param name="filter">The filter</param>
		/// <returns></returns>
		private static object SetOrGetCachedSingleValue(CachingFilter filter)
		{

			var settingsManager = new SettingsManager();
			var key = CachableDataEnum.SingleValues.ToString();

			switch (filter.SingleValueType)
			{
			   case SingleValueTypeEnum.Settings:
					
					Setting setting;

					if (CachingManager.IsNotEmpty(key).IsSucceed)
					{
						setting = (Setting)CachingManager.GetFromCache(key).Data;
					}
					else
					{
						setting = settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.NewSubstanceWaiting) });
						CachingManager.PutInCache(setting, key);
					}

					return setting;
				case SingleValueTypeEnum.Item:
					break;
				case SingleValueTypeEnum.AppInfo:
					break;
			}
			
			return new object();
		}

		/// <summary>
		/// Sets or gets the data into/from cache.
		/// </summary>
		/// <param name="dataEnum">The filter.</param>
		/// <returns></returns>
		public static object SetOrGetCachableData(CachableDataEnum dataEnum)
		{
			return SetOrGetCachableData(new CachingFilter() {InformationType = dataEnum});
		}

		/// <summary>
		/// Sets or gets the items into/from cache.
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <returns></returns>
		private static object SetOrGetCachedItems(CachingFilter filter)
		{
			var itemsManager = new ItemsManager();
			BindingList<Item> items;

			string itemsCacheKey = string.Format("{0}{1}",filter.CacheKey, filter.DataId);

			if (CachingManager.IsNotEmpty(itemsCacheKey).IsSucceed)
			{
				items = (BindingList<Item>)CachingManager.GetFromCache(itemsCacheKey).Data;
			}
			else
			{
				items = itemsManager.GetItems(new ItemsFilter
				{
					TypeLookupId = filter.DataId
				});

				CachingManager.PutInCache(items, itemsCacheKey);
			}

			return items;
		}

        /// <summary>
        /// Sets or gets the substance items into/from cache.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        private static object SetOrGetCachedItemsByType(CachableDataEnum cacheKey, ItemTypeEnum itemType)
        {
            BindingList<Item> items;
            var key = cacheKey.ToString();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                items = (BindingList<Item>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                var itemsManager = new ItemsManager();

                var itemsTemp = itemsManager.GetItems(new ItemsFilter
                {
                    TypeLookupId = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemType, itemType)).Id
                }).ToList();

                items = itemsTemp.ToBindingList();

                CachingManager.PutInCache(items, key);
            }

            return items;
        }

        /// <summary>
        /// Sets or gets the substance items into/from cache.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        private static object SetOrGetCachedSubstanceItems()
        {
            BindingList<Item> items;

            var key = CachableDataEnum.AllItems.ToString();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                items = (BindingList<Item>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                var itemsTemp = ((BindingList<Item>)SetOrGetCachableData(CachableDataEnum.AllSubstance)).ToList();

                itemsTemp.AddRange((BindingList<Item>)SetOrGetCachableData(CachableDataEnum.AllBacteria));

                itemsTemp.AddRange((BindingList<Item>)SetOrGetCachableData(CachableDataEnum.AllPotency));

                itemsTemp.AddRange((BindingList<Item>)SetOrGetCachedAllProducts());

                items = itemsTemp.ToBindingList();

                CachingManager.PutInCache(items, key);
            }

            return items;
        }

		/// <summary>
		/// Sets or gets the points list into/from cache.
		/// </summary>
		/// <returns></returns>
		private static object SetOrGetCachedPointsList()
		{
			BindingList<Lookup> testPoints;
			var lookupsManager = new LookupsManager();
			var key = CachableDataEnum.PointsList.ToString();
			if (CachingManager.IsNotEmpty(key).IsSucceed)
			{
				testPoints = (BindingList<Lookup>)CachingManager.GetFromCache(key).Data;
			}
			else
			{
                testPoints = UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.ListPoints));

				CachingManager.PutInCache(testPoints, CachableDataEnum.PointsList.ToString());
			}

			return testPoints;
		}

		/// <summary>
		/// Sets or gets test states into/from cache.
		/// </summary>
		/// <returns></returns>
		private static object SetOrGetCachedTestStates()
		{
			BindingList<Lookup> testStates;
			var lookupsManager = new LookupsManager();
			var key = CachableDataEnum.TestStates.ToString();

			if (CachingManager.IsNotEmpty(key).IsSucceed)
			{
				testStates = (BindingList<Lookup>)CachingManager.GetFromCache(key).Data;
			}
			else
			{
                testStates = UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.TestState));

				CachingManager.PutInCache(testStates, CachableDataEnum.TestStates.ToString());
			}

			return testStates;
		}

        /// <summary>
        /// Sets or gets eval period types into/from cache.
        /// </summary>
        /// <returns></returns>
        private static object SetOrGetCachedEvalPeriodTypes()
        {
            BindingList<Lookup> evalPeriodTypes;
            var lookupsManager = new LookupsManager();
            var key = CachableDataEnum.EvalPeriodTypes.ToString();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                evalPeriodTypes = (BindingList<Lookup>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                evalPeriodTypes = UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.EvalPeriodType));

                CachingManager.PutInCache(evalPeriodTypes, CachableDataEnum.EvalPeriodTypes.ToString());
            }

            return evalPeriodTypes;
        }

        /// <summary>
        /// Sets or gets shipping method from cache into/from cache.
        /// </summary>
        /// <returns></returns>
        private static object SetOrGetCachedShippingMethod()
        {
            BindingList<Lookup> shippingMethods;
            var lookupsManager = new LookupsManager();
            var key = CachableDataEnum.ShippingMethod.ToString();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                shippingMethods = (BindingList<Lookup>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                shippingMethods = UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.ShippingMethod));

                CachingManager.PutInCache(shippingMethods, CachableDataEnum.ShippingMethod.ToString());
            }

            return shippingMethods;
        }

		/// <summary>
		/// Sets or gets test types into/from cache.
		/// </summary>
		/// <returns></returns>
		private static object SetOrGetCachedTestTypes()
		{
			BindingList<Lookup> testTypes;
			var lookupsManager = new LookupsManager();
			var key = CachableDataEnum.TestTypes.ToString();

			if (CachingManager.IsNotEmpty(key).IsSucceed)
			{
				testTypes = (BindingList<Lookup>)CachingManager.GetFromCache(key).Data;
			}
			else
			{
                testTypes = UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.TestType));

				CachingManager.PutInCache(testTypes, key);
			}

			return testTypes;
		}

        /// <summary>
        /// Sets or gets yes/no lookup into/from cache.
        /// </summary>
        /// <returns></returns>
        private static object SetOrGetCachedLookups()
        {
            BindingList<Lookup> lookups;
            var lookupsManager = new LookupsManager();
            var key = CachableDataEnum.Lookups.ToString();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                lookups = (BindingList<Lookup>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                lookups = lookupsManager.GetLookups(new LookupsFilter());

                CachingManager.PutInCache(lookups, key);
            }

            return lookups;
        }

		/// <summary>
		/// Sets or gets test protocols into/from cache.
		/// </summary>
		/// <returns></returns>
		private static object SetOrGetCachedTestProtocols()
		{
			var testProtocolsManager = new TestProtocolsManager();
			BindingList<TestProtocol> testProtocols;

			if (CachingManager.IsNotEmpty(CachableDataEnum.TestProtocols.ToString()).IsSucceed)
			{
				testProtocols = (BindingList<TestProtocol>)CachingManager.GetFromCache(CachableDataEnum.TestProtocols.ToString()).Data;
			}
			else
			{
				testProtocols = testProtocolsManager.GetTestProtocols(new TestProtocolsFilter());

				CachingManager.PutInCache(testProtocols, CachableDataEnum.TestProtocols.ToString());
			}

			return testProtocols;
		}

		/// <summary>
		/// Sets or gets database version into/from cache.
		/// </summary>
		/// <returns></returns>
		private static object SetOrGetCachedDBVersion()
		{
			var appInfoManager = new AppInfoManager();
			AppInfo dbVersionAppInfo;
			var key = CachableDataEnum.DBVersion.ToString();

			if (CachingManager.IsNotEmpty(key).IsSucceed)
			{
				dbVersionAppInfo = (AppInfo)CachingManager.GetFromCache(key).Data;
			}
			else
			{
				dbVersionAppInfo = appInfoManager.GetAppInfoByProperty(new AppInfoFilter 
				{ Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.DBVersion) });

				CachingManager.PutInCache(dbVersionAppInfo, key);
			}

			return dbVersionAppInfo;
		}

        /// <summary>
		/// Sets or gets logo into/from cache.
		/// </summary>
		/// <returns></returns>
		private static object SetOrGetCachedLogo()
		{
			var _appImagesManager = new AppImagesManager();

			Image appImage = null;
			var key = CachableDataEnum.Logo.ToString();

			if (CachingManager.IsNotEmpty(key).IsSucceed)
			{
				appImage = (Image) CachingManager.GetFromCache(key).Data;
			}
			else
			{
				var data = _appImagesManager.GetAppImageByProperty(new AppImageFilter { Property = StaticKeys.AppImagesLogo });

				if (data != null && data.Value != null && data.Value.Length > 0)
				{
					appImage = VitalHelper.ToImage(data.Value);
				}

				CachingManager.PutInCache(appImage, key);
			}

			return appImage;
		}

        /// <summary>
        /// Sets or gets settings into/from cache.
        /// </summary>
        /// <returns></returns>
        private static object SetOrGetCachedSettings(CachableDataEnum cachableDataEnum)
        {
            BindingList<Setting> settingsList = null;
            var settingsManager = new SettingsManager();
            var key = cachableDataEnum.ToString();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                settingsList = (BindingList<Setting>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                switch (cachableDataEnum)
                {
                        case CachableDataEnum.Settings:
                        settingsList = settingsManager.GetSettings(new SettingsFilter());
                        break;

                        case CachableDataEnum.VisibleSettings:
                        settingsList = settingsManager.GetSettings(new SettingsFilter { IsVisible = true });
                        break;

                        case CachableDataEnum.PrintingSettings:

                        var printingGroupLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SettingGroup, SettingGroups.ReportOptions, false, true));
                        
                        if(printingGroupLookup == null)
                            return null;

                        settingsList = settingsManager.GetSettings(new SettingsFilter { SettingGroupLookupId = printingGroupLookup.Id});
                        
                        break;

                        case CachableDataEnum.ShippingOrderSettings:
                        
                        var shippingOrdersGroupLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SettingGroup, SettingGroups.ShippingOrders, false, true));

                        if (shippingOrdersGroupLookup == null)
                            return null;

                        settingsList = settingsManager.GetSettings(new SettingsFilter { SettingGroupLookupId = shippingOrdersGroupLookup.Id });
                        
                        break;

                        case CachableDataEnum.BackupAndRestore:

                        var backupAndRestoreGroupLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SettingGroup, SettingGroups.BackupAndRestore, false, true));

                        if (backupAndRestoreGroupLookup == null)
                            return null;

                        settingsList = settingsManager.GetSettings(new SettingsFilter { SettingGroupLookupId = backupAndRestoreGroupLookup.Id });

                        break;

                        case CachableDataEnum.HwProfileSettings:

                        var hwProfileSettingsGroupLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SettingGroup, SettingGroups.HardwareProfileSettings, false, true));

                        if (hwProfileSettingsGroupLookup == null)
                            return null;

                        settingsList = settingsManager.GetSettings(new SettingsFilter { SettingGroupLookupId = hwProfileSettingsGroupLookup.Id });

                        break;
                }
                
                CachingManager.PutInCache(settingsList, key);
            }

            return settingsList;
        }

		/// <summary>
		/// Sets or gets four factors into/from cache.
		/// </summary>
		/// <returns></returns>
		private static object SetOrGetCachedFourFactors()
		{
			var itemsManager = new ItemsManager();
			var key = CachableDataEnum.FourFactors.ToString();

			var fourFactors = new BindingList<Item>();

			if (CachingManager.IsNotEmpty(key).IsSucceed)
			{
				fourFactors = (BindingList<Item>)CachingManager.GetFromCache(key).Data;
			}
			else
			{
                var targetTypeLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.TargetType, TargetType.FourFactors));

				if (targetTypeLookup != null)
				{
                    fourFactors = itemsManager.GetItems(new ItemsFilter
                    {
                        TargetTypeLookupId = targetTypeLookup.Id,
                        OrderById = true
                    });
                    CachingManager.PutInCache(fourFactors, key);
				}
			}

			return fourFactors;
		}

        /// <summary>
        /// Sets or gets ratios into/from cache.
        /// </summary>
        /// <returns></returns>
        private static object SetOrGetCachedRatios()
        {
            var itemsManager = new ItemsManager();
            var key = CachableDataEnum.Ratios.ToString();

            var ratios = new BindingList<Item>();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                ratios = (BindingList<Item>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                var targetTypeLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.TargetType, TargetType.Ratios));

                if (targetTypeLookup != null)
                {
                    ratios = itemsManager.GetItems(new ItemsFilter
                    {
                        TargetTypeLookupId = targetTypeLookup.Id,
                        OrderById = true
                    });
                    CachingManager.PutInCache(ratios, key);
                }
            }

            return ratios;
        }

        /// <summary>
        /// Sets or gets all products into/from cache.
        /// </summary>
        /// <returns></returns>
        private static object SetOrGetCachedAllProducts()
        {
            var lookupsManager = new LookupsManager();
            var itemsManager = new ItemsManager();
            var key = CachableDataEnum.AllProducts.ToString();
            var allProducts = new BindingList<Item>();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                allProducts = (BindingList<Item>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                var lookupProducts = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Product));

                allProducts = new BindingList<Item>(itemsManager.GetItems(new ItemsFilter()
                {
                    TypeLookupId = lookupProducts.Id
                })).OrderBy(c => c.Name).ToBindingList();

                CachingManager.PutInCache(allProducts, key);
            }

            return allProducts;
        }

		/// <summary>
		/// Sets or gets products into/from cache.
		/// </summary>
		/// <returns></returns>
		private static object SetOrGetCachedProducts()
		{
			var lookupsManager = new LookupsManager();
			var itemsManager = new ItemsManager();
			var key = CachableDataEnum.Products.ToString();
			var products = new BindingList<Item>();

			if (CachingManager.IsNotEmpty(key).IsSucceed)
			{
				products = (BindingList<Item>)CachingManager.GetFromCache(key).Data;
			}
			else
			{
                var lookupMySubstanceList = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.TargetType, TargetType.MySubstancesList));

				products = new BindingList<Item>(itemsManager.GetItems(new ItemsFilter()
				{                    
					TargetTypeLookupId = lookupMySubstanceList.Id,
				})).OrderBy(c => c.Name).ToBindingList();

				CachingManager.PutInCache(products, key);
			}

			return products;
		}

		/// <summary>
		/// Sets or gets Custom Dilutions into/from cache.
		/// </summary>
		/// <returns></returns>
		private static object SetOrGetCachedCustomDilutions()
		{
			var lookupsManager = new LookupsManager();
			var itemsManager = new ItemsManager();
			var key = CachableDataEnum.CustomDilutions.ToString();
			var customDilutions = new BindingList<Item>();

			if (CachingManager.IsNotEmpty(key).IsSucceed)
			{
				customDilutions = (BindingList<Item>)CachingManager.GetFromCache(key).Data;
			}
			else
			{
				var customDilutionItem = itemsManager.GetItems(new ItemsFilter {Name = StaticKeys.CustomDilutions}).FirstOrDefault();

				if (customDilutionItem != null)
				{
					customDilutions = itemsManager.GetItemChildren(new SingleItemFilter {ItemId = customDilutionItem.Id});
					CachingManager.PutInCache(customDilutions, key);
				}
			}

			return customDilutions;
		}

        /// <summary>
        /// Sets or gets generic list items into/from cache.
        /// </summary>
        /// <returns></returns>
        private static object SetOrGetCachedGenericList()
        {
            var lookupsManager = new LookupsManager();
            var itemsManager = new ItemsManager();
            var key = CachableDataEnum.GenericList.ToString();
            var genericListItems = new BindingList<Item>();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                genericListItems = (BindingList<Item>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                var genericItem = itemsManager.GetItems(new ItemsFilter { Name = StaticKeys.Generic }).FirstOrDefault();

                if (genericItem != null)
                {
                    genericListItems = itemsManager.GetItemChildren(new SingleItemFilter { ItemId = genericItem.Id });
                    CachingManager.PutInCache(genericListItems, key);
                }
            }

            return genericListItems;            
        }

        /// <summary>
        /// Sets or gets four causes into/from cache.
        /// </summary>
        /// <returns></returns>
        private static object SetOrGetCachedFourCauses()
        {
            var itemsManager = new ItemsManager();
            var key = CachableDataEnum.FourCauses.ToString();

            var fourCauses = new BindingList<Item>();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                fourCauses = (BindingList<Item>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                var fourCausesItem = itemsManager.GetItems(new ItemsFilter { Name = StaticKeys.FourCauses }).FirstOrDefault();

                if (fourCausesItem != null)
                {
                    fourCauses = itemsManager.GetItemChildren(new SingleItemFilter { ItemId = fourCausesItem.Id });
                    CachingManager.PutInCache(fourCauses, key);
                }
            }

            return fourCauses;
        }

        /// <summary>
        /// Sets or gets fequency list items into/from cache.
        /// </summary>
        /// <returns></returns>
        private static object SetOrGetCachedFrequencyList()
        {
            var itemsManager = new ItemsManager();
            var key = CachableDataEnum.FrequencyList.ToString();
            var frequencyListItems = new BindingList<Item>();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                frequencyListItems = (BindingList<Item>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                var frequencyParentItem = itemsManager.GetItems(new ItemsFilter { Key = StaticKeys.EnergyFrequencyGroupsParentItemKey }).FirstOrDefault();

                if (frequencyParentItem != null)
                {
                    frequencyListItems = itemsManager.GetItemChildren(new SingleItemFilter { ItemId = frequencyParentItem.Id });
                    CachingManager.PutInCache(frequencyListItems, key);
                }
            }

            return frequencyListItems;
        }

        /// <summary>
        /// Sets or gets generic list items into/from cache.
        /// </summary>
        /// <returns></returns>
        private static object SetOrGetCachedServicesList()
        {
            BindingList<Service> servicesList = null;
            var servicessManager = new ServicesManager();
            var key = CachableDataEnum.Services.ToString();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                servicesList = (BindingList<Service>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                servicesList = servicessManager.GetServices(new ServicesFilter());

                CachingManager.PutInCache(servicesList, key);
            }

            return servicesList;
        }

        /// <summary>
        /// Sets or gets generic list items into/from cache.
        /// </summary>
        /// <returns></returns>
        private static object SetOrGetCachedVFSItemsSourceList()
        {
            BindingList<VFSItemSource> vfsItemsSourceList = null;
            var vfsManager = new VitalForceSheetManager();
            var key = CachableDataEnum.VFSItemsSource.ToString();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                vfsItemsSourceList = (BindingList<VFSItemSource>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                vfsItemsSourceList = vfsManager.GetVFSItemsSource(new VFSItemSourceFilter());

                CachingManager.PutInCache(vfsItemsSourceList, key);
            }

            return vfsItemsSourceList;
        }

        /// <summary>
        /// Sets or gets generic list items into/from cache.
        /// </summary>
        /// <returns></returns>
        private static object SetOrGetCachedVFSSecondaryItemSourceList()
        {
            BindingList<VFSSecondaryItemSource> vfsSecondaryItemsSourceList = null;
            var vfsManager = new VitalForceSheetManager();
            var key = CachableDataEnum.VFSSecondaryItemsSource.ToString();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                vfsSecondaryItemsSourceList = (BindingList<VFSSecondaryItemSource>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                vfsSecondaryItemsSourceList = vfsManager.GetVFSSecondaryItemsSource(new VFSSecondaryItemSourceFilter());

                CachingManager.PutInCache(vfsSecondaryItemsSourceList, key);
            }

            return vfsSecondaryItemsSourceList;
        }

		/// <summary>
		/// Sets or gets Custom Dilutions into/from cache.
		/// </summary>
		/// <returns></returns>
		private static object SetOrGetCachedVitalForce()
		{
			var lookupsManager = new LookupsManager();
			var itemsManager = new ItemsManager();
			var key = CachableDataEnum.VitalForce.ToString();
			var vitalForces = new BindingList<Item>();

			if (CachingManager.IsNotEmpty(key).IsSucceed)
			{
				vitalForces = (BindingList<Item>)CachingManager.GetFromCache(key).Data;
			}
			else
			{
				var vitalForceItem = itemsManager.GetItems(new ItemsFilter { Name = StaticKeys.VitalForce }).FirstOrDefault();

				if (vitalForceItem != null)
				{
					vitalForces = itemsManager.GetItemChildren(new SingleItemFilter { ItemId = vitalForceItem.Id });
					CachingManager.PutInCache(vitalForces, key);
				}                
			}

			return vitalForces;
		}

		/// <summary>
		/// Sets or gets Top 10 into/from cache.
		/// </summary>
		/// <returns></returns>
		private static object SetOrGetCachedTop10()
		{
			var lookupsManager = new LookupsManager();
			var itemsManager = new ItemsManager();
			var key = CachableDataEnum.Top10.ToString();
			var top10Items = new BindingList<Item>();

			if (CachingManager.IsNotEmpty(key).IsSucceed)
			{
				top10Items = (BindingList<Item>)CachingManager.GetFromCache(key).Data;
			}
			else
			{
				var topTenItem = itemsManager.GetItems(new ItemsFilter { Name = StaticKeys.TopTen }).FirstOrDefault();

				if (topTenItem != null)
				{
					top10Items = itemsManager.GetItemChildren(new SingleItemFilter { ItemId = topTenItem.Id });
					CachingManager.PutInCache(top10Items, key);
				}
			}

			return top10Items;
		}

		/// <summary>
		/// Sets or gets item groups into/from cache.
		/// </summary>
		/// <returns></returns>
		private static object SetOrGetCachedItemsGroups()
		{
			var lookupsManager = new LookupsManager();
			var itemsManager = new ItemsManager();
			var itemsGroups = new BindingList<Item>();

			var key = CachableDataEnum.ItemsGroup.ToString();

			if (CachingManager.IsNotEmpty(key).IsSucceed)
			{
				itemsGroups = (BindingList<Item>)CachingManager.GetFromCache(key).Data;
			}
			else
			{
                var listTypeLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ListType, ListTypeEnum.UserList));
                var targetTypeLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.TargetType, TargetType.MyPointsList));
                var itemTypeLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Point));

				if (listTypeLookup != null && itemTypeLookup != null && targetTypeLookup != null)
				{
                    itemsGroups = itemsManager.GetItems(new ItemsFilter
                    {
                        TargetTypeLookupId = targetTypeLookup.Id,
                        TypeLookupId = itemTypeLookup.Id,
                        ListTypeLookupId = listTypeLookup.Id,
                    });

                    CachingManager.PutInCache(itemsGroups, key);
				}
			}

			return itemsGroups;

		}

        /// <summary>
        /// Sets or gets database version into/from cache.
        /// </summary>
        /// <returns></returns>
        private static object SetOrGetCachedShippingOrdersTimeInfo()
        {
            var appInfoManager = new AppInfoManager();

            BindingList<AppInfo> shippingTimeInfo;

            var key = CachableDataEnum.ShippingOrdersTimeInfo.ToString();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                shippingTimeInfo = (BindingList<AppInfo>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                shippingTimeInfo = new BindingList<AppInfo> { 
                    appInfoManager.GetAppInfoByProperty(new AppInfoFilter { Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.ShippingTimeZoneId) }),
                    appInfoManager.GetAppInfoByProperty(new AppInfoFilter { Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.ShippingTimeZoneDisplayName) }),
                    appInfoManager.GetAppInfoByProperty(new AppInfoFilter { Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.ShippingOpenAt) }),
                    appInfoManager.GetAppInfoByProperty(new AppInfoFilter { Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.ShippingCloseAt) }),
                    appInfoManager.GetAppInfoByProperty(new AppInfoFilter { Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.ShippingWeekEndDays) })
                };

                CachingManager.PutInCache(shippingTimeInfo, key);
            }

            return shippingTimeInfo;
        }

        /// <summary>
        /// Sets or gets cached hw profiles.
        /// </summary>
        /// <returns></returns>
        private static object SetOrGetCachedHwProfiles()
        {
            BindingList<HwProfile> hwProfilesList;
            var settingsManager = new SettingsManager();
            var key = CachableDataEnum.HwProfiles.ToString();

            if (CachingManager.IsNotEmpty(key).IsSucceed)
            {
                hwProfilesList = (BindingList<HwProfile>)CachingManager.GetFromCache(key).Data;
            }
            else
            {
                hwProfilesList = settingsManager.GetHwProfiles(new HwProfilesFilter());
                CachingManager.PutInCache(hwProfilesList, key);
            }

            return hwProfilesList;
        }
	}
}
