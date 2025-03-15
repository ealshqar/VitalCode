using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.Data.Filtering.Helpers;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Memory_Shell_Objects;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.UI.Logic_Classes
{
    public class TestHelper
    {
        #region Fields

        private int _yesLookupId;
        private int _noLookupId;
        private readonly LookupsManager _lookupsManager;
        private readonly PropertiesManager _testPropertiesManager;
        private readonly ItemsManager _testItemsManager;
        #endregion

        #region Constructor

        /// <summary>
        /// The constructor.
        /// </summary>
        public TestHelper()
        {
            _lookupsManager = new LookupsManager();
            _testPropertiesManager = new PropertiesManager();
            _testItemsManager = new ItemsManager();
            FillLocalLookupIds();    
        }

        #endregion

        #region General Methods

        /// <summary>
        /// Fill the local lookups ids.
        /// </summary>
        private void FillLocalLookupIds()
        {
            var yesLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.Yes));
            var noLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.No));

            _yesLookupId = yesLookup != null ? yesLookup.Id : 0;
            _noLookupId = noLookup != null ? noLookup.Id : 0;
        }

        /// <summary>
        /// Generates a structured list of Point Sets and their respective readings
        /// </summary>
        public static BindingList<ReadingPointSet> GenerateReadingPointSets(Test test)
        {
            if (test.ReadingPointSets == null)
            {
                test.ReadingPointSets = new BindingList<ReadingPointSet>();    
            }

            test.ReadingPointSets.Clear();

            //Get the point sets collection from cache
            var pointSets = (BindingList<Item>)CacheHelper.SetOrGetCachableData(CachableDataEnum.ItemsGroup);

            //Generate a distinct list of used PointSetIds that are used in the generated reading records, this is used to show only point set groups that are used
            var createdPointSetsIds = test.Readings.Select(r => r.PointSetItemId).Distinct().ToList();

            foreach (var createdPointSetsId in createdPointSetsIds)
            {
                var id = createdPointSetsId;

                var readingPointSet = new ReadingPointSet()
                {
                    PointSetItem = pointSets.FirstOrDefault(i => i.Id == createdPointSetsId),
                    Readings = test.Readings.Where(r => r.PointSetItemId == id).ToBindingList()
                };

                test.ReadingPointSets.Add(readingPointSet);
            }

            return test.ReadingPointSets;
        }

        #endregion

        #region Imprinting Helpers

        /// <summary>
        /// Apply imprinting action to single test result
        /// </summary>
        /// <param name="CurrentTest"></param>
        /// <param name="testResult"></param>
        /// <param name="action"></param>
        /// <param name="showWaitingPanel"></param>
        /// <param name="refresh"></param>
        public void ApplyImprintActionSingleResult(Test CurrentTest,TestResult testResult, ImprintingAction action, bool showWaitingPanel = false)
        {
            switch (action)
            {
                case ImprintingAction.Imprint:
                    CreateTestResultImprintableItem(CurrentTest, testResult, showWaitingPanel);
                    break;
                case ImprintingAction.MarkNotImprintable:
                    UpdateItemImprintableProperty(testResult.Item, false, showWaitingPanel);
                    break;
                case ImprintingAction.RemoveFromImprintList:
                    RemoveTestResultImprintableItem(CurrentTest, testResult, showWaitingPanel);
                    break;
            }
        }

        /// <summary>
        /// Applies an imprinting related action on one or multiple test results.
        /// </summary>
        /// <param name="currentTest"></param>
        /// <param name="testresults"></param>
        /// <param name="action"></param>
        public void ApplyImprintAction(Test currentTest, IEnumerable<TestResult> testresults, ImprintingAction action)
        {
            if (testresults == null)
                return;

            switch (action)
            {
                case ImprintingAction.Imprint:
                    UiHelperClass.ShowWaitingPanel(StaticKeys.AddToImprintingList);
                    break;
                case ImprintingAction.MarkNotImprintable:
                    UiHelperClass.ShowWaitingPanel(StaticKeys.UpdateImprintingState);
                    break;
                case ImprintingAction.RemoveFromImprintList:
                    UiHelperClass.ShowWaitingPanel(StaticKeys.RemoveFromImprintingList);
                    break;
            }

            foreach (var testresult in testresults)
            {
                ApplyImprintActionSingleResult(currentTest, testresult, action);
            }

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Sets the Imprintable property for an item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="markAsImprintable"></param>
        public void UpdateItemImprintableProperty(Item item, bool markAsImprintable, bool showWaitingPanel = false)
        {
            if (item == null || item.Properties == null) return;

            if (showWaitingPanel)
            {
                UiHelperClass.ShowWaitingPanel(StaticKeys.UpdateImprintingState);
            }

            //Update the property for the current item
            UpdateSingleItemImprintableProperty(item, markAsImprintable);

            //Update the item imprintable property in memory in other caches to make sure the item works the same way regardless of where it exists in memory
            //This needs to be changed in the future to make sure that the items are all coming from one cache
            UpdateImprintablePropertyInCache(item, CachableDataEnum.AllItems, markAsImprintable);
            UpdateImprintablePropertyInCache(item, CachableDataEnum.Top10, markAsImprintable);
            UpdateImprintablePropertyInCache(item, CachableDataEnum.Products, markAsImprintable);
            UpdateImprintablePropertyInCache(item, CachableDataEnum.AllProducts, markAsImprintable);
           
            if (showWaitingPanel)
            {
                UiHelperClass.HideSplash();
            }
        }

        /// <summary>
        /// Update imprintable item property in cache for all items with the same name
        /// </summary>
        /// <param name="mastrItem"></param>
        /// <param name="cacheKey"></param>
        /// <param name="markAsImprintable"></param>
        private void UpdateImprintablePropertyInCache(Item mastrItem, CachableDataEnum cacheKey, bool markAsImprintable)
        {
            //Get duplicated items and update their property too
            var itemsCache = CacheHelper.SetOrGetCachableData(cacheKey) as BindingList<Item>;

            if (itemsCache != null)
            {
                var duplictedItems = itemsCache.Where(i => i.Name.Trim() == mastrItem.Name.Trim());

                foreach (var duplictedItem in duplictedItems)
                {
                    //This check is included to make sure that the Imprintable property doesn't get duplicated In DB, if we don't include the check,
                    //the system will create another property for the same item
                    if (duplictedItem.Id == mastrItem.Id)
                    {
                        duplictedItem.Properties = mastrItem.Properties;
                    }
                    else
                    {
                        UpdateSingleItemImprintableProperty(duplictedItem, markAsImprintable);    
                    }
                }
            }
        }

        /// <summary>
        /// Sets the Imprintable property for a single item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="markAsImprintable"></param>
        public void UpdateSingleItemImprintableProperty(Item item, bool markAsImprintable)
        {
            if (item == null || item.Properties == null) return;

            //Determine if the item is alreday imprintable
            var isImprintableCurrently = item.Properties.HasProperty(PropertiesEnum.IsImprintable, _yesLookupId.ToString());

            if (markAsImprintable && !isImprintableCurrently)
            {
                var hasImprintableProperty = item.Properties.HasProperty(PropertiesEnum.IsImprintable);
                var property = _testPropertiesManager.GetPropertyByKey(PropertiesEnum.IsImprintable);

                if (hasImprintableProperty)
                {
                    var itemProperty = item.Properties.FirstOrDefault(
                            ip => ip.Property != null && ip.Property.Id == property.Id);

                    if (itemProperty != null)
                    {
                        itemProperty.Value = _yesLookupId;
                    }
                    _testItemsManager.SaveItemProperty(itemProperty);
                }
                else
                {
                    var itemProperty = new ItemProperty
                    {
                        Item = item,
                        Property = property,
                        Value = _yesLookupId
                    };
                    _testItemsManager.SaveItemProperty(itemProperty);
                    item.Properties.Add(itemProperty);
                }
            }
            else
            {
                if (!markAsImprintable && isImprintableCurrently && item.Properties.HasProperty(PropertiesEnum.IsImprintable))
                {
                    var property = _testPropertiesManager.GetPropertyByKey(PropertiesEnum.IsImprintable);

                    var itemProperty =
                        item.Properties.FirstOrDefault(
                            ip => ip.Property != null && ip.Property.Id == property.Id);

                    if (itemProperty != null)
                    {
                        itemProperty.Value = _noLookupId;
                        _testItemsManager.SaveItemProperty(itemProperty);
                    }
                }
            }
        }

        /// <summary>
        /// Removes test result's imprintable item if any
        /// </summary>
        /// <param name="currentTest"></param>
        /// <param name="testResult"></param>
        public void RemoveTestResultImprintableItem(Test currentTest, TestResult testResult, bool showWaitingPanel = false)
        {
            if (testResult == null ||
                !testResult.IsImprinted ||
                currentTest == null ||
                currentTest.TestImprintableItems == null) return;

            if (showWaitingPanel)
            {
                UiHelperClass.ShowWaitingPanel(StaticKeys.RemoveFromImprintingList);
            }
            //Get the imprintable item for the test result
            var imprintableItem = currentTest.TestImprintableItems.FirstOrDefault(t => t.TestResult != null && t.TestResult.TempImprintingId == testResult.TempImprintingId);

            if (imprintableItem == null)
            {
                testResult.IsImprinted = false;
            }
            else
            {
                currentTest.DeleteImprintableItem(imprintableItem);
            }
            
            if (showWaitingPanel)
            {
                UiHelperClass.HideSplash();
            }
        }

        /// <summary>
        /// Creates an imprintable item for single test result
        /// </summary>
        /// <param name="currentTest"></param>
        /// <param name="testResult"></param>
        /// <param name="showWaitingPanel"></param>
        public void CreateTestResultImprintableItem(Test currentTest, TestResult testResult, bool showWaitingPanel = false)
        {
            if (currentTest == null || currentTest.TestImprintableItems == null || testResult == null ||
                testResult.Item == null || testResult.Item.Properties == null || testResult.IsImprinted) return;

            var result = currentTest.TestImprintableItems.FirstOrDefault(t => t.TestResult != null && t.TestResult.TempImprintingId == testResult.TempImprintingId);

            if (result != null) return;

            if (showWaitingPanel)
            {
                UiHelperClass.ShowWaitingPanel(StaticKeys.AddToImprintingList);
            }

            var deletedImprintableItem =
                currentTest.DeletedTestImprintableItems.FirstOrDefault(t => t.TestResult != null && t.TestResult.TempImprintingId == testResult.TempImprintingId);

            if (deletedImprintableItem == null && testResult.Item != null)
            {
                deletedImprintableItem = currentTest.DeletedTestImprintableItems.FirstOrDefault(t => t.TestResult != null &&
                                                                                                     t.Item != null &&
                                                                                                     t.Item.Id == testResult.Item.Id);
            }

            if (deletedImprintableItem != null)
            {
                currentTest.RestoreDeletedImprintableItem(deletedImprintableItem, testResult);
            }
            else
            {
                currentTest.AddNewImprintableItem(testResult.Item, testResult);
            }
            testResult.IsImprinted = true;

            UpdateItemImprintableProperty(testResult.Item, true, showWaitingPanel);

            if (showWaitingPanel)
            {
                UiHelperClass.HideSplash();
            }
        }

        #endregion
    }
}
