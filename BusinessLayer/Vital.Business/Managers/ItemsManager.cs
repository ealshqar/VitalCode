using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Vital.Business.Repositories.DatabaseRepositories.Items;
using Vital.Business.Repositories.DatabaseRepositories.Lookups;
using Vital.Business.Repositories.DatabaseRepositories.Properties;
using Vital.Business.Repositories.DatabaseRepositories.Readings;
using Vital.Business.Repositories.DatabaseRepositories.Settings;
using Vital.Business.Repositories.DatabaseRepositories.TestProtocols;
using Vital.Business.Repositories.DatabaseRepositories.Tests;
using Vital.Business.Repositories.DatabaseRepositories.VitalForceSheet;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Readings;
using Vital.Business.Shared.DomainObjects.TestProtocols;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.DomainObjects.VitalForceSheet;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
	public class ItemsManager : BaseManager
	{
		#region Private Variables

		private readonly IItemRepository _itemsRepository;
		private readonly ITestRepository _testRepository;
		private readonly IReadingRepository _readingRepository;
		private readonly ISettingRepository _settingsRepository;
		private readonly IPropertyRepository _propertyRepository;
		private readonly ITestProtocolRepository _testProtocolRepository;
		private readonly ILookupRepository _lookupManager;
	    private readonly IVitalForceSheetRepository _vfsrepositroy;

		#endregion

		#region Constructors

		/// <summary>
		/// FactorsManager Constructor.
		/// </summary>
		public ItemsManager()
		{
			_itemsRepository = new ItemDatabaseRepository();
			_testRepository = new TestDatabaseRepository();
            _vfsrepositroy = new VitalForceSheetDatabaseRepository();
			_readingRepository = new ReadingDatabaseRepository();
			_settingsRepository = new SettingDatabaseRepository();
			_propertyRepository = new PropertyDatabaseRepository();
			_testProtocolRepository = new TestProtocolDatabaseRepository();
			_lookupManager = new LookupDatabaseRepository();
		}

		#endregion

		#region Public Methods

		#region Items Stuff

		/// <summary>
		/// Gets single Item By Id.
		/// </summary>
		/// <param name="filter">The Filter.</param>
		/// <returns>The Item.</returns>
		public Item GetItemById(SingleItemFilter filter)
		{
			Check.Argument.IsNotNull(() => filter);

			try
			{
				return _itemsRepository.LoadItemById(filter.ItemId);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}

		}

		/// <summary>
		/// Gets single Item By Id.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>The Item.</returns>
		public Item GetItemByKey(ItemKeys key)
		{
			try
			{
				return _itemsRepository.LoadItemByKey(key.ToString());
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}

		}

		/// <summary>
		/// Gets Items depends on the passed filter.
		/// </summary>
		/// <param name="filter">The Filter.</param>
		/// <returns>List of Items.</returns>
		public BindingList<Item> GetItems(ItemsFilter filter)
		{
			Check.Argument.IsNotNull(() => filter);

			try
			{
				return _itemsRepository.LoadItems(filter.Name,filter.Key, filter.FullName, filter.TypeLookupId, filter.ListTypeLookupId, filter.TargetTypeLookupId, filter.ItemSourceLookupId, filter.ImageId, filter.X, filter.Y,
                                           filter.UserId, filter.CreationDateTime, filter.UpdatedDateTime, filter.OrderById, filter.SearchKey, filter.LoadHiddenItems, filter.IncludeHiddenChilds);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Gets Items for the GoToProperty
		/// </summary>        
		/// <returns>List of Items.</returns>
		public BindingList<Item> GetGoToPropertyItems()
		{
			var lookupsManager = new LookupsManager();            
			var targetTypeLookup = lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.TargetType, TargetType.GoToPropertyList));
			var targetTypeFirstOrDefault = targetTypeLookup.FirstOrDefault();

			try
			{
				return targetTypeFirstOrDefault == null ? null : _itemsRepository.LoadItems(targetTypeFirstOrDefault.Id,false);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Saves the passed Item.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public ProcessResult SaveItem(Item item)
		{
			Check.Argument.IsNotNull(() => item);

			if (!item.IsChanged) return ProcessResult.Succeed;
			
			try
			{
				item.SetUserAndDates();

				var processResult =  _itemsRepository.Save(item);

				//save the relations.
				var processResultItems = item.Parents != null ? SaveItemRelations(item.Parents) : new ProcessResult() { IsSucceed = true };
				var processResultChildren = item.Children != null ? SaveItemRelations(item.Children) : new ProcessResult() { IsSucceed = true };

				//save the targets.
				var processResultTargets = item.ItemTargets != null ? SaveItemTargets(item.ItemTargets) : new ProcessResult() { IsSucceed = true };

				var processResultProperties = item.Properties != null ? SaveItemProperties(item.Properties) : new ProcessResult() { IsSucceed = true };

				if (processResult.IsSucceed && processResultChildren.IsSucceed && processResultItems.IsSucceed && processResultTargets.IsSucceed && processResultProperties.IsSucceed)
				{
					item.ObjectState = DomainEntityState.Unchanged;
				}

				return processResult;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Saves the passed items list.
		/// </summary>
		/// <param name="itemsList">The items.</param>
		/// <returns></returns>
		public ProcessResult Save(BindingList<Item> itemsList)
		{
			Check.Argument.IsNotNull(() => itemsList);

			try
			{
				var result = new ProcessResult { IsSucceed = false };

				foreach (var item in itemsList)
				{
					if (item.ObjectState == DomainEntityState.Deleted)
					{
						result = DeleteItem(item);                      
					}
					else
					{
						result = SaveItem(item);
					}

					if (result.IsSucceed == false)
						return result;
				}

				return result;

			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Deletes the passed Item.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		public ProcessResult DeleteItem(Item item)
		{
			Check.Argument.IsNotNull(() => item);

			try
			{
				var processResultProperties = item.Properties != null ? DeleteItemProperties(item.Properties) : new ProcessResult() { IsSucceed = true};
				var processResultTargets = item.ItemTargets != null ? DeleteItemTargets(item.ItemTargets) : new ProcessResult() { IsSucceed = true };
				var processResultRelations =  DeleteItemFromRelations(item);

				var processResult = _itemsRepository.Delete(item);

				//delete item relations.
				var processResultItems = item.Parents != null ? DeleteItemRelations(item.Parents) : new ProcessResult() { IsSucceed = true };

				if (processResultRelations.IsSucceed && processResult.IsSucceed && processResultItems.IsSucceed && processResultProperties.IsSucceed && processResultTargets.IsSucceed) { item.ObjectState = DomainEntityState.Deleted; }

				return processResult;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Check if can save the passed item.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		public ProcessResult CanSaveItem(Item item)
		{
			Check.Argument.IsNotNull(() => item);

			try
			{
				var testsFilter = new TestsFilter() { ItemId = item.Id };

				if(item.ObjectState != DomainEntityState.New)
				{
					var tests = GetItemTests(testsFilter);

					if(tests.Count > 0)
					{
						if ((from itemTarget in item.ItemTargets
							 let targetTypeLookup = _lookupManager.LoadLockupById(itemTarget.TargetTypeLookup.Id)
							 where (targetTypeLookup.Value == EnumNameResolver.Resolve(TargetType.MyPointsList)) && itemTarget.ObjectState != DomainEntityState.Deleted
							 select itemTarget).Any())
						{
							return new ProcessResult() { IsSucceed = true };
						}

						return new ProcessResult() {IsSucceed = false, Message = StaticKeys.CannotSaveItem};
					}
				}

				return new ProcessResult() { IsSucceed = true };
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Check the passed item if can be deleted or not.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns>True : Can be delete. False: Cannot be deleted.</returns>
		public ProcessResult CanDeleteItem(Item item)
		{
			Check.Argument.IsNotNull(() => item);

			try
			{
                var issuesFilter = new TestIssuesFilter() { ItemId = item.Id, IssuesLoadingType = TestIssuesLoadingType.Both };
                var readingsPointSetFilter = new ReadingsFilter() { PointSetItemId = item.Id };
                var readingsFilter = new ReadingsFilter() {ItemId = item.Id};
				var testResultsFilter = new TestResultsFilter() {ItemId = item.Id , VitalForceId = item.Id };
				var testsFilter = new TestsFilter() {ItemId = item.Id };
				var settingsFilter = new SettingsFilter() { Key = "ListPoints" };
				var testResultsFactorsFilter = new TestResultFactorsFilter() {  FactorId = item.Id , PotencyId = item.Id , TestResultId = item.Id };
				var protocolItemsFilter = new ProtocolItemsFilter() { ItemId = item.Id };
			    var testImprintableItemsFilter = new TestImprintableItemsFilter() {ItemId = item.Id};
			    var vfsItemsFilter = new VFSItemsFilter() {ItemId = item.Id};
                var vfsSecondaryItemsFilter = new VFSSecondaryItemsFilter() {ItemId = item.Id};
				
				if (GetItemReadings(readingsFilter).Count > 0 ||
                    GetItemReadings(readingsPointSetFilter).Count > 0 ||
					GetItemTests(testsFilter).Count > 0 ||
					GetItemRelatedTestResults(testResultsFilter).Count > 0 ||
					GetItemRelatedFactors(testResultsFactorsFilter).Count > 0 ||
					GetItemRelatedProtocolItems(protocolItemsFilter).Count > 0 ||
                    GetItemImprintableItems(testImprintableItemsFilter).Count > 0 ||
                    GetItemVFSItems(vfsItemsFilter).Count > 0 ||
                    GetItemVFSSecondaryItems(vfsSecondaryItemsFilter).Count > 0 ||
					!CanDeleteItemFromSettings(settingsFilter, item) ||
                    _testRepository.LoadTestIssues(issuesFilter.Name, issuesFilter.TestId, issuesFilter.ProtocolStepId, issuesFilter.ItemId, issuesFilter.IssuesLoadingType).Count > 0)
				{
					return new ProcessResult { IsSucceed = false, Message = StaticKeys.ItemInUseMessageTitle };
				}

				return new ProcessResult { IsSucceed = true, Message = string.Empty};
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}

		}

		/// <summary>
		/// Check the passed item if can be deleted or not.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns>True : Can be delete. False: Cannot be deleted.</returns>
		public ProcessResult ItemHasRelations(Item item)
		{
			Check.Argument.IsNotNull(() => item);

			try
			{
				var itemChildren = _itemsRepository.LoadItemRelations(item.Id, 0, 0, null, null,true);

				var lookupMySubstanceAdditions = _lookupManager.LoadLookups(LookupTypes.TargetType.ToString(), TargetType.MySubstancesAdditionsList.ToString()).FirstOrDefault();
											   
				int targetOccurances = lookupMySubstanceAdditions != null ? _itemsRepository.LoadItems(lookupMySubstanceAdditions.Id,true).Count(c => c.Id == item.Id) : 0;
			   
				if (itemChildren.Count + targetOccurances > 1)
				{
					return new ProcessResult { IsSucceed = false, Message = StaticKeys.ItemInUseMessageTitle };
				}

				return new ProcessResult { IsSucceed = true, Message = string.Empty };
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}

        }

		/// <summary>
		/// Gets the protocol items based on passed filter.
		/// </summary>
		/// <param name="protocolItemsFilter">The filter.</param>
		/// <returns></returns>
		private BindingList<ProtocolItem> GetItemRelatedProtocolItems(ProtocolItemsFilter protocolItemsFilter)
		{
			Check.Argument.IsNotNull(() => protocolItemsFilter);

			try
			{
				var factors = _testProtocolRepository.LoadProtocolItems(protocolItemsFilter.TestProtocolId , protocolItemsFilter.ItemId);

				return factors;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Gets TestResultFactor based on the passed filter.
		/// </summary>
		/// <param name="testResultsFactorFilter">The filter.</param>
		/// <returns></returns>
		private BindingList<TestResultFactor> GetItemRelatedFactors(TestResultFactorsFilter testResultsFactorFilter)
		{
			Check.Argument.IsNotNull(() => testResultsFactorFilter);

			try
			{
				var factors = _testRepository.LoadTestResultFactors(testResultsFactorFilter.FactorId , testResultsFactorFilter.PotencyId , testResultsFactorFilter.TestResultId);

				return factors;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Check if can delete the passed item based in if it related to some setting object..
		/// </summary>
		/// <param name="settingsFilter">The setting filter.</param>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		private bool CanDeleteItemFromSettings(SettingsFilter settingsFilter , Item item)
		{
			var setting = _settingsRepository.LoadSettingByKey(settingsFilter.Key);

			return Convert.ToInt32(setting.Value) != item.Id;
		}

		/// <summary>
		/// Gets a tests contains they items.  
		/// </summary>
		/// <param name="testsFilter">The filter.</param>
		/// <returns></returns>
		private BindingList<Test> GetItemTests(TestsFilter testsFilter)
		{
			Check.Argument.IsNotNull(() => testsFilter);

			try
			{
				var tests = _testRepository.LoadTests(testsFilter.Name,testsFilter.PatientId,testsFilter.ItemId , testsFilter.TestProtocolId,null, LoadingTypeEnum.None);

				return tests;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Gets a test results contains they items.
		/// </summary>
		/// <param name="testResultsFilter"></param>
		/// <returns></returns>
		private BindingList<TestResult> GetItemRelatedTestResults(TestResultsFilter testResultsFilter)
		{
			Check.Argument.IsNotNull(() => testResultsFilter);

			try
			{
				var testResults = _testRepository.LoadTestResults(testResultsFilter.TestIssueId, testResultsFilter.ItemId,
															   testResultsFilter.ParentId,
															   testResultsFilter.VitalForceId , testResultsFilter.IsSelected );

				return testResults;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		#endregion

		#region Item Details Stuff

		/// <summary>
		/// Gets item details depends on the passed filter.
		/// </summary>
		/// <param name="filter">The Filter.</param>
		/// <returns>List of Items.</returns>
		public BindingList<ItemDetails> GetItemDetails(ItemsDetailsFilter filter)
		{
			Check.Argument.IsNotNull(() => filter);

			try
			{
				return _itemsRepository.LoadItemDetails(filter.ImageId, filter.X, filter.Y,
										   filter.UserId, filter.CreationDateTime, filter.UpdatedDateTime);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}
		#endregion

		#region ItemGroupStuff

		/// <summary>
		/// Gets readings depends on the passed filter.
		/// </summary>
		/// <param name="filter">The Filter.</param>
		/// <returns>List of Readings.</returns>
		public BindingList<Reading> GetItemsAsReadings(SingleItemFilter filter)
		{
			Check.Argument.IsNotNull(() => filter);

			try
			{
			    var pointSetItem = GetItemById(filter);

				var relations = GetItemRelations(new ItemRelationsFilter() { ParentId = filter.ItemId,LoadHiddenItems = true});

				var children = relations.Select(i => i.Child).ToList();

                var readings = children.Select(item => new Reading() { Item = item, PointSetItemId = pointSetItem.Id }).ToList();

				return new BindingList<Reading>(readings); 
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		#endregion

		#region ItemRlation Stuff

		/// <summary>
		/// Saves the list of item relations
		/// </summary>
		/// <param name="itemRelations">The list of item relations.</param>
		/// <returns></returns>
		private ProcessResult SaveItemRelations(BindingList<ItemRelation> itemRelations)
		{
			Check.Argument.IsNotNull(() => itemRelations);

			var itemsList = itemRelations.ToList();

			try
			{
				var processResult = ProcessResult.Succeed;

				foreach (var itemRelation in itemsList)
				{
					if (itemRelation.ObjectState == DomainEntityState.Deleted)
					{
						processResult = DeleteItemRelation(itemRelation);
					}
					else if (itemRelation.IsChanged)
					{
						itemRelation.SetUserAndDates();

						processResult = _itemsRepository.Save(itemRelation);
						if (processResult.IsSucceed)
						{
							itemRelation.ObjectState = DomainEntityState.Unchanged;
						}
					}

					if (!processResult.IsSucceed)
					{
						processResult.IsSucceed = false;
						break;
					}
				}

				return processResult;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Saves the list of item targets
		/// </summary>
		/// <param name="itemTargets">The list of item targets.</param>
		/// <returns></returns>
		private ProcessResult SaveItemTargets(BindingList<ItemTarget> itemTargets)
		{
			Check.Argument.IsNotNull(() => itemTargets);

			var itemsList = itemTargets.ToList();

			try
			{
				var processResult = ProcessResult.Succeed;

				foreach (var itemTarget in itemsList)
				{
					if (itemTarget.ObjectState == DomainEntityState.Deleted)
					{
						processResult = DeleteItemTarget(itemTarget);
					}
					else
					{
						processResult = Save(itemTarget);
					}

					if (processResult.IsSucceed)
					{
						if (itemTarget.ObjectState != DomainEntityState.Deleted)
							itemTarget.ObjectState = DomainEntityState.Unchanged;
					}
					else
					{
						processResult.IsSucceed = false;
						break;
					}
				}

				return processResult;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Deletes a list of item relations.
		/// </summary>
		/// <param name="itemRelations">The list of item relations.</param>
		/// <returns></returns>
		public ProcessResult DeleteItemRelations(BindingList<ItemRelation> itemRelations)
		{
			Check.Argument.IsNotNull(() => itemRelations);

			var result = ProcessResult.Succeed;

			try
			{
				foreach (var itemRelation in itemRelations)
				{
					if (itemRelation.Id > 0)
					{
						result = _itemsRepository.Delete(itemRelation);

						itemRelation.ObjectState = DomainEntityState.Deleted;

						if (!result.IsSucceed) return result;
					}
				}

				return result;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Deletes an item relation.
		/// </summary>
		/// <param name="itemRelation">The item relation.</param>
		/// <returns></returns>
		public ProcessResult DeleteItemRelation(ItemRelation itemRelation)
		{
			Check.Argument.IsNotNull(() => itemRelation);

			try
			{
				var processResult = _itemsRepository.Delete(itemRelation);

				if (processResult.IsSucceed) { itemRelation.ObjectState = DomainEntityState.Deleted; }

				return processResult;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Gets single Item relation By Id.
		/// </summary>
		/// <param name="filter">The Filter.</param>
		/// <returns>The Item.</returns>
		public ItemRelation GetItemRelationById(SingleItemFilter filter)
		{
			Check.Argument.IsNotNull(() => filter);

			try
			{
				return _itemsRepository.LoadItemRelationById(filter.ItemId);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}

		}

		/// <summary>
		/// Gets Item Children. 
		/// </summary>
		/// <param name="itemIdFilter">The Item Id Filter.</param>
	    /// <param name="loadHiddenItems"></param>
		/// <returns>List of Items.</returns>
	    public BindingList<Item> GetItemChildren(SingleItemFilter itemIdFilter, bool loadHiddenItems = false)
		{
			Check.Argument.IsNotNull(()=> itemIdFilter);

			try
			{
			    var cachedItems = ItemsCacheHelper.GetChildItemsFromCacheIfExisting(itemIdFilter.ItemId, loadHiddenItems);

			    if (cachedItems != null)
			    {
			        return cachedItems;
			    }
			    else
			    {
                    var relations = GetItemRelations(new ItemRelationsFilter() { ParentId = itemIdFilter.ItemId, LoadHiddenItems = loadHiddenItems });

                    var children = relations.GetChildren();

                    ItemsCacheHelper.AddChildItemsToCachedList(itemIdFilter.ItemId, loadHiddenItems, children);

                    return children;
			    }
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Gets Item Readings. 
		/// </summary>
		/// <param name="readingsFilter">The readings Filter.</param>
		/// <returns>List of readings.</returns>
		public BindingList<Reading> GetItemReadings(ReadingsFilter readingsFilter)
		{
			Check.Argument.IsNotNull(() => readingsFilter);

			try
			{
                var readings = _readingRepository.LoadReadings(readingsFilter.TestId, readingsFilter.DateTime,
															   readingsFilter.ItemId, readingsFilter.PointSetItemId,
                                                               readingsFilter.ListPointLookupId,
															   readingsFilter.Max, readingsFilter.Min,
															   readingsFilter.Fall, readingsFilter.Rise,
															   readingsFilter.Value);

				return readings;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

        /// <summary>
        /// Gets Item Imprintable Items. 
        /// </summary>
        /// <returns>List of readings.</returns>
        public BindingList<TestImprintableItem> GetItemImprintableItems(TestImprintableItemsFilter testImprintableItemsFilter)
        {
            Check.Argument.IsNotNull(() => testImprintableItemsFilter);

            try
            {


                var testImprintableItems = _testRepository.LoadTestImprintableItems(testImprintableItemsFilter.TestId,testImprintableItemsFilter.ItemId);

                return testImprintableItems;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

		/// <summary>
        /// Gets Item VFS Items. 
        /// </summary>
        /// <returns>List of readings.</returns>
        public BindingList<VFSItem> GetItemVFSItems(VFSItemsFilter vfsItemsFilter)
        {
            Check.Argument.IsNotNull(() => vfsItemsFilter);

            try
            {


                var vfsItems = _vfsrepositroy.LoadVFSItems(vfsItemsFilter.VfsId, 
                                                                       vfsItemsFilter.ItemSourceId, 
                                                                       vfsItemsFilter.ItemId, 
                                                                       vfsItemsFilter.PreviousV1, 
                                                                       vfsItemsFilter.PreviousV2, 
                                                                       vfsItemsFilter.CurrentV1, 
                                                                       vfsItemsFilter.CurrentV2, 
                                                                       vfsItemsFilter.IsSkipped, 
                                                                       vfsItemsFilter.Comments);

                return vfsItems;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets Item VFS Secondary Items. 
        /// </summary>
        public BindingList<VFSSecondaryItem> GetItemVFSSecondaryItems(VFSSecondaryItemsFilter vfsSecondaryItemsFilter)
        {
            Check.Argument.IsNotNull(() => vfsSecondaryItemsFilter);

            try
            {


                var vfsItems = _vfsrepositroy.LoadVFSSecondaryItems(vfsSecondaryItemsFilter.VFSId, vfsSecondaryItemsFilter.ItemId, vfsSecondaryItemsFilter.SectionLookupId);

                return vfsItems;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }
        
		/// <summary>
		/// Gets Item Readings. 
		/// </summary>
		/// <param name="itemTargetsFilter">The targets Filter.</param>
		/// <returns>List of readings.</returns>
		public BindingList<ItemTarget> GetItemTargets(ItemTargetsFilter itemTargetsFilter)
		{
			Check.Argument.IsNotNull(() => itemTargetsFilter);

			try
			{
				var targets = _itemsRepository.LoadItemTargets(itemTargetsFilter.ItemId);

				return targets;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Gets Item Parents.
		/// </summary>
		/// <param name="itemIdFilter">The Item Id Filter.</param>
		/// <returns></returns>
		public List<Item> GetItemParents(SingleItemFilter itemIdFilter)
		{
			Check.Argument.IsNotNull(()=> itemIdFilter);

			try
			{
				var relations = GetItemRelations(new ItemRelationsFilter() { ChildId = itemIdFilter.ItemId });

				var children = relations.Select(i => i.Parent).ToList();

				return children;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Gets the ItemRelations depends on passed filter.
		/// </summary>
		/// <param name="filter">The Filter.</param>
		/// <returns>List of ItemRelations.</returns>
		public BindingList<ItemRelation> GetItemRelations(ItemRelationsFilter filter)
		{
			Check.Argument.IsNotNull(() => filter);

			try
			{
				return _itemsRepository.LoadItemRelations(filter.ChildId, filter.ParentId, filter.UserId, filter.CreationDateTime, filter.UpdatedDateTime, filter.LoadHiddenItems);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}


		#endregion

		#region ItemTargetStuff

		/// <summary>
		/// Loads ItemTarget by Id.
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <returns></returns>
		public ItemTarget GetItemTargetById(SingleItemFilter filter)
		{
			Check.Argument.IsNotNull(() => filter);

			try
			{
				return _itemsRepository.LoadItemTargetById(1);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Saves the passed ItemTarget.
		/// </summary>
		/// <param name="itemTarget">The ItemTarget.</param>
		/// <returns></returns>
		public ProcessResult Save(ItemTarget itemTarget)
		{
			Check.Argument.IsNotNull(()=>itemTarget);

			if (!itemTarget.IsChanged) return ProcessResult.Succeed;

			try
			{
				itemTarget.SetUserAndDates();
				return _itemsRepository.Save(itemTarget);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="itemRelation"></param>
		/// <returns></returns>
		public ProcessResult Save(ItemRelation itemRelation)
		{
			Check.Argument.IsNotNull(() => itemRelation);

			if (!itemRelation.IsChanged) return ProcessResult.Succeed;

			try
			{
				itemRelation.SetUserAndDates();
				return _itemsRepository.Save(itemRelation);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Deletes the passed ItemTarget.
		/// </summary>
		/// <param name="itemTarget">The ItemTarget.</param>
		/// <returns></returns>
		public ProcessResult DeleteItemTarget(ItemTarget itemTarget)
		{
			Check.Argument.IsNotNull(() => itemTarget);

			try
			{
				var processResult = _itemsRepository.Delete(itemTarget);

				if (processResult.IsSucceed) { itemTarget.ObjectState = DomainEntityState.Deleted; }

				return processResult;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		#endregion

		#region ItemProperty
		
		/// <summary>
		/// Gets ItemProperty by id.
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
		public ItemProperty GetItemPropertyById(SingleItemFilter filter)
		{
			Check.Argument.IsNotNull(() => filter);

			try
			{
				return _itemsRepository.LoadItemPropertyById(filter.ItemId);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Loads the Item properties depends on passed filter.
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
		public BindingList<ItemProperty> GetItemProperties(RelationalPropertiesFilter filter)
		{
			Check.Argument.IsNotNull(() => filter);

			try
			{
				return _itemsRepository.LoadItemProperties(filter.RelatedEntityId, filter.Key);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Saves the list of item properties.
		/// </summary>
		/// <param name="itemProperties">The list of item properties.</param>
		/// <returns></returns>
		public ProcessResult SaveItemProperties(BindingList<ItemProperty> itemProperties)
		{
			Check.Argument.IsNotNull(() => itemProperties);

			var propertiesList = itemProperties.ToList();

			try
			{
				var processResult = ProcessResult.Succeed;

				foreach (var itemProperty in propertiesList)
				{
					if (itemProperty.ObjectState == DomainEntityState.Deleted)
					{
						processResult = DeleteItemProperty(itemProperty);
					}
					else if (itemProperty.IsChanged)
					{
						SaveItemProperty(itemProperty);
					}

					if (processResult.IsSucceed)
					{
						if (itemProperty.ObjectState != DomainEntityState.Deleted)
							itemProperty.ObjectState = DomainEntityState.Unchanged;
					}
					else
					{
						processResult.IsSucceed = false;
						break;
					}
				}

				return processResult;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Save the passed ItemProperty.
		/// </summary>
		/// <param name="itemProperty"></param>
		/// <returns></returns>
		public ProcessResult SaveItemProperty(ItemProperty itemProperty)
		{
			Check.Argument.IsNotNull(() => itemProperty);

			try
			{
				return _itemsRepository.Save(itemProperty);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Deletes a list of item properties.
		/// </summary>
		/// <param name="itemProperties">The list of item properties.</param>
		/// <returns></returns>
		public ProcessResult DeleteItemProperties(BindingList<ItemProperty> itemProperties)
		{
			Check.Argument.IsNotNull(() => itemProperties);

			var result = ProcessResult.Succeed;

			try
			{
				foreach (var itemProperty in itemProperties)
				{
					if (itemProperty.Id <= 0) continue;

					result = _itemsRepository.Delete(itemProperty);

					itemProperty.ObjectState = DomainEntityState.Deleted;

					if (!result.IsSucceed) return result;
				}

				return result;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Deletes a list of item targets.
		/// </summary>
		/// <param name="itemTargets">The list of item targets.</param>
		/// <returns></returns>
		public ProcessResult DeleteItemTargets(BindingList<ItemTarget> itemTargets)
		{
			Check.Argument.IsNotNull(() => itemTargets);

			var result = ProcessResult.Succeed;

			try
			{
				foreach (var itemTarget in itemTargets)
				{
					if (itemTarget.Id <= 0) continue;

					result = _itemsRepository.Delete(itemTarget);

					itemTarget.ObjectState = DomainEntityState.Deleted;

					if (!result.IsSucceed) return result;
				}

				return result;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Delete the passed ItemProperty.
		/// </summary>
		/// <param name="itemProperty"></param>
		/// <returns></returns>
		public ProcessResult DeleteItemProperty(ItemProperty itemProperty)
		{
			Check.Argument.IsNotNull(() => itemProperty);

			try
			{
				return _itemsRepository.Delete(itemProperty);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Deletes the item relations (childs)
		/// </summary>
		/// <param name="item">The item .</param>
		/// <returns></returns>
		private ProcessResult DeleteItemFromRelations(Item item)
		{
			Check.Argument.IsNotNull(() => item);

			var itemRelations = _itemsRepository.LoadItemRelations(item.Id, 0, 0, null, null,true);

			var result = ProcessResult.Succeed;

			try
			{
				foreach (var itemRelation in itemRelations)
				{
					if (itemRelation.Id <= 0) continue;

					result = _itemsRepository.Delete(itemRelation);

					itemRelation.ObjectState = DomainEntityState.Deleted;

					if (!result.IsSucceed) return result;
				}

				return result;
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		#endregion

		#region ItemRelationProperty

		/// <summary>
		/// Gets ItemRelationProperty by id.
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
		public ItemRelationProperty GetItemRelationPropertyById(SingleItemFilter filter)
		{
			Check.Argument.IsNotNull(() => filter);

			try
			{
				return _itemsRepository.LoadItemRelationPropertyById(filter.ItemId);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Loads the Item Relation properties depends on passed filter.
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
		public BindingList<ItemRelationProperty> GetItemRelationProperties(RelationalPropertiesFilter filter)
		{
			Check.Argument.IsNotNull(() => filter);

			try
			{
				return _itemsRepository.LoadItemRelationProperties(filter.RelatedEntityId, filter.Key);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Save the passed ItemRelationProperty.
		/// </summary>
		/// <param name="itemRelationProperty"></param>
		/// <returns></returns>
		public ProcessResult SaveItemRelationProperty(ItemRelationProperty itemRelationProperty)
		{
			Check.Argument.IsNotNull(() => itemRelationProperty);

			try
			{
				if (itemRelationProperty.Property != null && itemRelationProperty.Property.IsChanged)
				{
					var result = _propertyRepository.Save(itemRelationProperty.Property);

					if (!result.IsSucceed)
						return result;
				}

				return _itemsRepository.Save(itemRelationProperty);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		/// <summary>
		/// Delete the passed ItemRelationProperty.
		/// </summary>
		/// <param name="itemRelationProperty"></param>
		/// <returns></returns>
		public ProcessResult DeleteItemRelationProperty(ItemRelationProperty itemRelationProperty)
		{
			Check.Argument.IsNotNull(() => itemRelationProperty);

			try
			{
				return _itemsRepository.Delete(itemRelationProperty);
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				if (exception is VitalDatabaseException) throw;
				throw new VitalLogicalException(exception);
			}
		}

		#endregion

		#endregion
	}
}

