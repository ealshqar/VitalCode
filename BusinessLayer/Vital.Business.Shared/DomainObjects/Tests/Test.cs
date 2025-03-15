using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Invoices;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Memory_Shell_Objects;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.DomainObjects.PatientSchedules;
using Vital.Business.Shared.DomainObjects.Readings;
using Vital.Business.Shared.DomainObjects.ShippingOrders;
using Vital.Business.Shared.DomainObjects.TestProtocols;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Tests
{
    public class Test : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private Patient _patient;
        private string _name;
        private string _description;
        private string _notes;
        private string _sentOrderNumber;
        private int _numberOfIssues;
        private bool _evalPeriodChecked;
        private bool _isOrderSent;

        private DateTime? _dateTime;
        private DateTime? _orderSendDate;

        private Lookup _stateLookup;
        private Lookup _typeLookup;
        private Lookup _listPointLookup;

        private TestIssue _testMainIssue;

        private BindingList<Reading> _readings;
        private BindingList<TestIssue> _testIssues;
        private BindingList<TestService> _testServices;
        private BindingList<ShippingOrder> _shippingOrders;
        private BindingList<Invoice> _invoices;
        private BindingList<TestImprintableItem> _testImprintableItems;

        private TestProtocol _testProtocol;
        private Item _item;
        private TestSchedule _testSchedule;

        private BindingList<TestImprintableItem> _deletedTestImprintableItems;
        private BindingList<ReadingPointSet> _readingPointSets;
 
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Test()
        {
            _stateLookup = new Lookup();
            _typeLookup = new Lookup();
            _listPointLookup = new Lookup();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Item.
        /// </summary>
        public Item Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                if (_item == null) return;
                _item.PropertyChanged += Item_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the Test Schedule
        /// </summary>
        public TestSchedule TestSchedule
        {
            get { return _testSchedule; }
            set
            {
                if(_testSchedule != value)
                {
                    _testSchedule = value;
                    _testSchedule.PropertyChanged += TestSchedule_PropertyChanged;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }        

        /// <summary>
        /// Gets or sets the Readings
        /// </summary>
        public BindingList<Reading> Readings
        {
            get { return _readings; }
            set
            {
                _readings = value; 
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets filtered Readings
        /// </summary>
        public BindingList<Reading> ReadingsFiltered
        {
            get
            {
                return _readings.Where(r=>r.PointSetItemId == Item.Id && r.ListPointLookupId == ListPointLookup.Id).ToBindingList();
            }
            set
            {
                _readings = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        } 

        /// <summary>
        /// Gets or sets the Invoices
        /// </summary>
        public BindingList<Invoice> Invoices
        {
            get { return _invoices; }
            set
            {
                _invoices = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the Test Main Issue
        /// </summary>
        public TestIssue TestMainIssue
        {
            get
            {
                return _testMainIssue;
            }
            set
            {
                _testMainIssue = value;
                _testMainIssue.PropertyChanged += TestMainIssue_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the test Issues
        /// </summary>
        public BindingList<TestIssue> TestIssuesForPrinting
        {
            get
            {
                var issuesForPrinting = new BindingList<TestIssue>();
                
                foreach (var issue in TestIssues)
                {
                    issuesForPrinting.Add(issue);
                }

                issuesForPrinting.Add(TestMainIssue);

                return issuesForPrinting;
            }
        }

        /// <summary>
        /// Gets or sets the test Issues
        /// </summary>
        public BindingList<TestIssue> TestIssues
        {
            get { return _testIssues; }
            set
            {
                _testIssues = value;
                _testIssues.RaiseListChangedEvents = true;
                _testIssues.ListChanged += TestIssues_ListChanged;                
            }
        }

        /// <summary>
        /// Gets or sets the test services
        /// </summary>
        public BindingList<TestService> TestServices
        {
            get { return _testServices; }
            set
            {
                _testServices = value;
                _testServices.RaiseListChangedEvents = true;
                _testServices.ListChanged += TestServices_ListChanged;                
            }
        }

        /// <summary>
        /// Gets or sets the shipping orders
        /// </summary>
        public BindingList<ShippingOrder> ShippingOrders
        {
            get { return _shippingOrders; }
            set
            {
                _shippingOrders = value;
                _shippingOrders.RaiseListChangedEvents = true;
                _shippingOrders.ListChanged += ShippingOrders_ListChanged;
            }
        }

        /// <summary>
        /// Gets or sets the test imprintable items
        /// </summary>
        public BindingList<TestImprintableItem> TestImprintableItems
        {
            get { return _testImprintableItems; }
            set
            {
                _testImprintableItems = value;
                _testImprintableItems.RaiseListChangedEvents = true;
                _testImprintableItems.ListChanged += TestImprintableItems_ListChanged;
            }
        }

        /// <summary>
        /// Gets the imprintable items ordered by level
        /// </summary>
        public BindingList<TestImprintableItem> TestImprintableItemsOrderdByLevel
        {
            get
            {
                return TestImprintableItems.OrderBy(t=>t.Level).ToBindingList();
            }      
        }

        /// <summary>
        /// Gets the imprintable items ordered by level in reverse
        /// </summary>
        public BindingList<TestImprintableItem> TestImprintableItemsOrderdByLevelReverese
        {
            get
            {
                return TestImprintableItems.OrderByDescending(t => t.Level).ToBindingList();
            }
        }

        public BindingList<TestResult> IssuesTestResults
        {
            get
            {
                var results  = new BindingList<TestResult>(TestIssues.SelectMany(r => r.TestResults).Where(rr =>
                        rr.ObjectState != DomainEntityState.Temp && rr.IsSelected).ToList());

                results = results.Concat(TestMainIssue.TestResults.Where(rr => rr.ObjectState != DomainEntityState.Temp && rr.IsSelected)).ToBindingList();

                return results;
            }
        }

        public BindingList<TestResult> IssuesTestResultsWithTempAndHidden
        {
            get
            {
                var results = new BindingList<TestResult>(TestIssues.SelectMany(r => r.TestResults).Where(rr =>
                        rr.ObjectState != DomainEntityState.Temp && rr.IsSelected).ToList());

                results = results.Concat(TestMainIssue.TestResults.Where(rr => rr.ObjectState != DomainEntityState.Temp && rr.IsSelected)).ToBindingList();

                return results;
            }
        }

        /// <summary>
        /// Gets the next result ID
        /// </summary>
        /// <returns></returns>
        public int GetNextTestResultId()
        {
            return IssuesTestResults.Count == 0 ? 1 : IssuesTestResults.Max(r => r.TempImprintingId) + 1;
        }

        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        public Patient Patient
        {
            get { return _patient; }
            set
            {
                _patient = value; 
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if(_name != value)
                {
                    _name = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                if(_description != value)
                {
                    _description = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        public string Notes
        {
            get { return _notes; }
            set
            {
                if(_notes != value)
                {
                    _notes = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of issues.
        /// </summary>
        public int NumberOfIssues
        {
            get { return _numberOfIssues; }
            set
            {
                if (_numberOfIssues != value)
                {
                    _numberOfIssues = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the evaluation period check
        /// </summary>
        public bool EvalPeriodChecked
        {
            get { return _evalPeriodChecked; }
            set
            {
                if (_evalPeriodChecked != value)
                {
                    _evalPeriodChecked = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or Sets the order sent property
        /// </summary>
        public bool IsOrderSent
        {
            get { return _isOrderSent; }
            set
            {
                if (_isOrderSent != value)
                {
                    _isOrderSent = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or Sets the sent order number.
        /// </summary>
        public string SentOrderNumber
        {
            get { return GetLastOrderNumber();}
        }

        /// <summary>
        /// Gets or Sets the order sent date property.
        /// </summary>
        public DateTime? OrderSendDate
        {
            get { return GetLastOrderSendDate();}
        }

        /// <summary>
        /// Last Order.
        /// </summary>
        public ShippingOrder LastOrder
        {
            get { return ShippingOrders == null? null : ShippingOrders.LastOrDefault(); }
        }

        /// <summary>
        /// Gets the last order send date.
        /// </summary>
        /// <returns></returns>
        private DateTime? GetLastOrderSendDate()
        {
            if (IsOrderSent)
            {
                var sentDate = LastOrder != null ? LastOrder.SentDate : null;
                    
                if (sentDate != null)
                    return sentDate.Value;
            }

            return null;
        }

        /// <summary>
        /// Gets the last order number.
        /// </summary>
        /// <returns></returns>
        private string GetLastOrderNumber()
        {
            return LastOrder != null ? LastOrder.Number : string.Empty;
        }
        
        /// <summary>
        /// Gets or sets the test date and time.
        /// </summary>
        public DateTime? DateTime
        {
            get { return _dateTime; }
            set
            {
                if(_dateTime != value)
                {
                    _dateTime = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the state lookup.
        /// </summary>
        public Lookup StateLookup
        {
            get { return _stateLookup; }
            set
            {
                _stateLookup = value;
                if (_stateLookup == null) return;
                _stateLookup.PropertyChanged += StateLookup_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the type lookup.
        /// </summary>
        public Lookup TypeLookup
        {
            get { return _typeLookup; }
            set
            {
                _typeLookup = value;
                if (_typeLookup == null) return;
                _typeLookup.PropertyChanged += TypeLookup_PropertyChanged;
                
            }
        }

        /// <summary>
        /// Gets or sets the listPointLookup, [Left, Right or Both]
        /// </summary>
        public Lookup ListPointLookup
        {
            get { return _listPointLookup; }
            set
            {
                _listPointLookup = value;
                if (_listPointLookup == null) return;
                _listPointLookup.PropertyChanged += _listPointLookup_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the test protocol
        /// </summary>
        public TestProtocol TestProtocol
        {
            get { return _testProtocol; }
            set
            {
                _testProtocol = value;
                if (_testProtocol == null) return;
                _testProtocol.PropertyChanged += TestProtocol_PropertyChanged;                
            }
        }

        /// <summary>
        /// Gets test major issues.
        /// Major issue is a result result that has not empty 4 factors.
        /// </summary>
        public BindingList<TestResult> MajorIssues
        {
            get
            {
                return TestIssues == null
                ? null
                : TestIssues.SelectMany(i => i.TestResults).GetResultsHaveFourFactors().ToBindingList();
            }
        }

        /// <summary>
        /// Memory only collection of reading point sets and their reading records
        /// </summary>
        public BindingList<ReadingPointSet> ReadingPointSets
        {
            get
            {
                return _readingPointSets;
            }
            set
            {
                _readingPointSets = value;
            }
        }

        /// <summary>
        /// Returns a filtered list of ReadingPointSets with only point sets that has readings that has been tested, this is to make sure
        /// empty pointsets do not show up in report.
        /// </summary>
        public BindingList<ReadingPointSet> ReadingPointSetsFiltered
        {
            get
            {
                return ReadingPointSets == null ? null : ReadingPointSets.Where(p=> !p.AllReadingsEmpty).ToBindingList();
            }
        }

        #region Imprintable Items

        /// <summary>
        /// Gets or sets the deleted test imprintable items
        /// </summary>
        public BindingList<TestImprintableItem> DeletedTestImprintableItems
        {
            get
            {
                return _deletedTestImprintableItems ??
                       (_deletedTestImprintableItems = new BindingList<TestImprintableItem>());
            }
            set
            {
                _deletedTestImprintableItems = value;
            }
        }

        /// <summary>
        /// Gets or sets the deleted test imprintable items orderd by level in reverse
        /// </summary>
        public BindingList<TestImprintableItem> DeletedTestImprintableItemsOrderdByLevelReverse
        {
            get
            {
                return DeletedTestImprintableItems.OrderByDescending(t => t.LevelAfterDelete).ToBindingList();
            }
        }

        /// <summary>
        /// Gets a temporary ID for imprintable item to support tree logic
        /// </summary>
        public int ImprintingMaxId
        {
            get
            {
                return (TestImprintableItems == null || TestImprintableItems.Count == 0
                    ? 0
                    : TestImprintableItems.Max(t => t.TempId)) + 1;
            }
        }

        /// <summary>
        /// Gets the order to assign to a new item added to the list
        /// </summary>
        public int ImprintingRootNextOrder
        {
            get
            {
                return (TestImprintableItems == null || TestImprintableItems.Count == 0
                    ? 0
                    : TestImprintableItems.Where(tt => tt.Parent == null).Max(t => t.Order)) + 1;
            }
        }

        /// <summary>
        /// Gets the order of the last item in the root
        /// </summary>
        public int ImprintingRootLastOrder
        {
            get
            {
                return (TestImprintableItems == null || TestImprintableItems.Count == 0
                    ? 0
                    : TestImprintableItems.Where(tt => tt.Parent == null).Max(t => t.Order));
            }
        }

        /// <summary>
        /// Get parent sub items
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public List<TestImprintableItem> GetParentSubItems(TestImprintableItem parent, bool forDeletedItems = false)
        {
            if (parent == null) return null;

            if (forDeletedItems)
            {
                return DeletedTestImprintableItems.Where(t => t.TempParentAfterDelete != null && t.DeletedParentId == parent.TempId).OrderBy(t => t.Order).ToList();
            }
            else
            {
                return TestImprintableItems.Where(t => t.Parent != null && t.ParentId == parent.TempId).OrderBy(t => t.Order).ToList();
            }
        }

        /// <summary>
        /// Get root sub items
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="forDeletedItems"></param>
        /// <returns></returns>
        public List<TestImprintableItem> GetRootSubItems(bool forDeletedItems = false)
        {
            if (forDeletedItems)
            {
                return DeletedTestImprintableItems.Where(t => t.TempParentAfterDelete == null ||
                                                              (t.TempParentAfterDelete != null && 
                                                              DeletedTestImprintableItems.Count(tt => tt.TempId == t.TempParentAfterDelete.TempId) == 0))
                                                              .OrderBy(t => t.Order).ToList();
            }
            else
            {
                return TestImprintableItems.Where(t => t.Parent == null).OrderBy(t => t.Order).ToList();
            }
        }

        /// <summary>
        /// Gets the last order in a parent
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public int GetParentLastOrder(TestImprintableItem parent)
        {
            if (parent == null) return 0;
            var parentItems = GetParentSubItems(parent);
            return (parentItems == null || parentItems.Count == 0) ? 0 :parentItems.Max(t => t.Order);
        }

        /// <summary>
        /// Gets if an item has sub items
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool HasSubItems(TestImprintableItem parent, bool forDeletedItems = false)
        {
            if (parent == null) return false;

            if (forDeletedItems)
            {
                return DeletedTestImprintableItems.Any(t => t.TempParentAfterDelete != null && t.DeletedParentId == parent.TempId);
            }
            else
            {
                return TestImprintableItems.Any(t => t.Parent != null && t.ParentId == parent.TempId);
            }
        }

        /// <summary>
        /// Resets the root items order
        /// </summary>
        public void ResetRootOrder()
        {
            var order = 1;
            foreach (var rootItem in TestImprintableItems.Where(t => t.Parent == null).OrderBy(t => t.Order))
            {
                rootItem.Order = order;
                order += 1;
            }
        }

        /// <summary>
        /// Resets a parent sub items order
        /// </summary>
        public void ResetParentSubItemsOrder(TestImprintableItem parent)
        {
            if (parent == null) return;
            var order = 1;
            foreach (var rootItem in TestImprintableItems.Where(t => t.Parent != null && t.ParentId == parent.TempId).OrderBy(t=>t.Order))
            {
                rootItem.Order = order;
                order += 1;
            }
        }

        /// <summary>
        /// Moves an item relatively to another item
        /// </summary>
        public void MoveItemRelativeToOtherItem(TestImprintableItem movedItem,TestImprintableItem targetItem, bool movedAfterTarget)
        {
            if (movedItem == null || targetItem == null) return;

            var movedItemParent = movedItem.Parent;
            var targetOrder = targetItem.Order;

            if (movedAfterTarget)
            {
                foreach (var subItem in (targetItem.IsRootItem() ? GetRootSubItems().Where(t => t.Order > targetItem.Order) :
                                                                   GetParentSubItems(targetItem.Parent).Where(t => t.Order > targetItem.Order)))
                {
                    subItem.Order = subItem.Order + 1;
                }
                movedItem.Order = targetOrder + 1;
            }
            else
            {
                foreach (var subItem in (targetItem.IsRootItem() ? GetRootSubItems().Where(t => t.Order >= targetItem.Order) :
                                                                   GetParentSubItems(targetItem.Parent).Where(t => t.Order >= targetItem.Order)))
                {
                    subItem.Order = subItem.Order + 1;
                }
                movedItem.Order = targetOrder;
            }

            movedItem.Parent = targetItem.Parent;

            if (targetItem.IsRootItem() || movedItemParent == null)
            {
                ResetRootOrder();
            }
            
            if (movedItemParent != null)
            {
                ResetParentSubItemsOrder(movedItemParent);
            }

            if (targetItem.Parent != null)
            {
                ResetParentSubItemsOrder(targetItem.Parent);
            }
        }

        /// <summary>
        /// Moves sub items to the root
        /// </summary>
        /// <param name="parent"></param>
        public void MoveSubItemsToRoot(TestImprintableItem parent)
        {
            if (parent == null) return;
            foreach (var subItem in GetParentSubItems(parent))
            {
                MoveItemToRoot(subItem);
            }
        }

        /// <summary>
        /// Moves an item to the root
        /// </summary>
        /// <param name="imprintableItem"></param>
        public void MoveItemToRoot(TestImprintableItem imprintableItem)
        {
            if (imprintableItem == null) return;
            if (imprintableItem != null && imprintableItem.Parent != null)
            {
                imprintableItem.Parent = null;
                imprintableItem.Order = ImprintingRootNextOrder;
            }
            ResetRootOrder();
        }

        /// <summary>
        /// Updates sub items check state
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="isChecked"></param>
        public void UpdateSubItemsCheck(TestImprintableItem parent,bool isChecked)
        {
            if (parent == null) return;
            foreach (var subItem in GetParentSubItems(parent))
            {
                subItem.IsChecked = isChecked;
            }
        }

        /// <summary>
        /// Gets if sub items options should be enabled for an item
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool SubItemsOptionsEnabled(TestImprintableItem parent)
        {
            return  parent != null && HasSubItems(parent);
        }

        /// <summary>
        /// Delete an imprintable item
        /// </summary>
        /// <param name="imprintableItem"></param>
        public void DeleteImprintableItem(TestImprintableItem imprintableItem)
        {
            if (imprintableItem == null) return;
            imprintableItem.ObjectState = DomainEntityState.Deleted;

            if (imprintableItem.TestResult != null)
            {
                var result = IssuesTestResults.FirstOrDefault(r => r.TempImprintingId == imprintableItem.TestResult.TempImprintingId);

                if (result != null)
                {
                    result.IsImprinted = false;
                }
            }

            if (imprintableItem.IsRootItem())
            {
                ResetRootOrder();
            }
            else
            {
                ResetParentSubItemsOrder(imprintableItem.Parent);
            }

            //THIS NEEDS TO BE CALLED BEFORE REMOVING THE ITEM FROM THE LIST, OTHERWISE SUB ITEMS WON'T BE FOUND
            MoveSubItemsToRoot(imprintableItem);
            //Backup item parent for delete related code
            imprintableItem.Parent = null;
            DeletedTestImprintableItems.Add(imprintableItem);
            TestImprintableItems.Remove(imprintableItem);
        }

        /// <summary>
        /// Adds a new imprintable item to the list
        /// </summary>
        /// <param name="item"></param>
        /// <param name="result"></param>
        public void AddNewImprintableItem(Item item, TestResult result)
        {
            var newImprintable = new TestImprintableItem()
            {
                Test = this,
                Item = item,
                TestResult = result,
                Order = ImprintingRootNextOrder,
                TempId = ImprintingMaxId
            };

            TestImprintableItems.Add(newImprintable);
        }

        /// <summary>
        /// Restores an imprintable item to the list of items
        /// </summary>
        /// <param name="deletedImprintableItem"></param>
        public void RestoreDeletedImprintableItem(TestImprintableItem deletedImprintableItem, TestResult testResult)
        {
            if (deletedImprintableItem == null) return;
            deletedImprintableItem.ObjectState = deletedImprintableItem.Id == 0
                     ? DomainEntityState.New
                     : DomainEntityState.Modified;
            deletedImprintableItem.Order = ImprintingRootNextOrder;
            deletedImprintableItem.TempId = ImprintingMaxId;
            deletedImprintableItem.TestResult = testResult;
            deletedImprintableItem.Item = testResult.Item;
            TestImprintableItems.Add(deletedImprintableItem);
            DeletedTestImprintableItems.Remove(deletedImprintableItem);
        }

        /// <summary>
        /// Gets if an item is first item in its range
        /// </summary>
        /// <param name="imprintableItem"></param>
        /// <returns></returns>
        public bool IsFirstItem(TestImprintableItem imprintableItem)
        {
            if (imprintableItem == null) return false;

            TestImprintableItem firstItem = null;

            firstItem = imprintableItem.IsRootItem() ? TestImprintableItems.FirstOrDefault() : GetParentSubItems(imprintableItem.Parent).FirstOrDefault();

            return firstItem != null && firstItem.TempId == imprintableItem.TempId && firstItem.Order == 1;
        }

        /// <summary>
        /// Gets if an item is last item in its range
        /// </summary>
        /// <param name="imprintableItem"></param>
        /// <returns></returns>
        public bool IsLastItem(TestImprintableItem imprintableItem)
        {
            if (imprintableItem == null) return false;

            TestImprintableItem lastItem = null;
            
            lastItem = imprintableItem.IsRootItem() ? TestImprintableItems.OrderBy(t=>t.Order).LastOrDefault() : GetParentSubItems(imprintableItem.Parent).LastOrDefault();
            var order = imprintableItem.IsRootItem() ? ImprintingRootLastOrder: GetParentLastOrder(imprintableItem.Parent);

            return lastItem != null && lastItem.TempId == imprintableItem.TempId && lastItem.Order == order;
        }

        /// <summary>
        /// Swaps order for certain item in a specific direction
        /// </summary>
        /// <param name="currentItem"></param>
        /// <param name="isMovingUp"></param>
        public void SwitchOrder(TestImprintableItem currentItem,bool isMovingUp)
        {
            TestImprintableItem otherItem = null;

            if (currentItem != null)
            {
                if (currentItem.IsRootItem())
                {
                    otherItem = isMovingUp ? TestImprintableItems.OrderBy(t => t.Order).
                                             LastOrDefault(tt => tt.IsRootItem() && tt.Order < currentItem.Order) :
                                             TestImprintableItems.OrderBy(t => t.Order).
                                             FirstOrDefault(tt => tt.IsRootItem() && tt.Order > currentItem.Order);
                }
                else
                {
                    otherItem = isMovingUp ? TestImprintableItems.OrderBy(t => t.Order).
                                             LastOrDefault(tt => !tt.IsRootItem() && tt.ParentId == currentItem.ParentId && tt.Order < currentItem.Order) :
                                             TestImprintableItems.OrderBy(t => t.Order).
                                             FirstOrDefault(tt => !tt.IsRootItem() && tt.ParentId == currentItem.ParentId && tt.Order > currentItem.Order);
                }
            }

            if (otherItem == null) return;

            var otherOrder = otherItem.Order;
            otherItem.Order = currentItem.Order;
            currentItem.Order = otherOrder;
        }

        /// <summary>
        /// Sets the level for all imprintable items in memory
        /// </summary>
        public void UpdateImprintableItemsLevel(bool forDeletedItems = false)
        {
            int index = 0;
            foreach (var item in GetRootSubItems(forDeletedItems))
            {
                if (forDeletedItems)
                {
                    item.LevelAfterDelete = 0;
                }
                else
                {
                    item.Level = 0;
                    item.ImprintingIndex = index;
                }
                
                if (HasSubItems(item,forDeletedItems))
                {
                    UpdateSubItemsLevel(item, forDeletedItems);
                }
            }
        }

        /// <summary>
        /// Sets the level for an item sub items
        /// </summary>
        /// <param name="currentItem"></param>
        private void UpdateSubItemsLevel(TestImprintableItem currentItem,bool forDeletedItems = false)
        {
            foreach (var item in GetParentSubItems(currentItem, forDeletedItems))
            {
                if (forDeletedItems)
                {
                    item.LevelAfterDelete = currentItem.LevelAfterDelete + 1;   
                }
                else
                {
                    item.Level = currentItem.Level + 1;
                }
                
                if (HasSubItems(item, forDeletedItems))
                {
                    UpdateSubItemsLevel(item, forDeletedItems);
                }  
            }
        }
        
        /// <summary>
        /// Sets the index for all imprintable items in memory
        /// </summary>
        public void UpdateImprintableItemsIndex()
        {
            int index = 0;
            foreach (var item in GetRootSubItems())
            {
                item.ImprintingIndex = index;
                if (HasSubItems(item))
                {
                    index = UpdateSubItemsIndex(item, index);
                }
                index += 1;
            }
        }

        /// <summary>
        /// Sets the index for an item sub items
        /// </summary>
        /// <param name="currentItem"></param>
        private int UpdateSubItemsIndex(TestImprintableItem currentItem, int parentIndex)
        {
            foreach (var item in GetParentSubItems(currentItem))
            {
                parentIndex += 1; 
                item.ImprintingIndex = parentIndex;

                if (HasSubItems(item))
                {
                    parentIndex = UpdateSubItemsIndex(item, item.ImprintingIndex);
                }
            }
            return parentIndex;
        }

        #endregion

        #region Calculation Properties (Memory)

        public bool IsAdjustmentOnProducts
        {
            get
            {
                return (TestSchedule != null && 
                        TestSchedule.DiscountApply != null)
                        && TestSchedule.DiscountApply.Value == AdjustmentApplyEnum.Products.ToString();
            }
        }

        #region Products

        public decimal ProductsSubtotal
        {
            get
            {
                return (TestSchedule == null || TestSchedule.ScheduleLines == null)? 0:
                    (decimal)TestSchedule.ScheduleLines.Where(s => !s.IsDeleted).Sum(ss => ss.Price * (int.Parse(string.IsNullOrEmpty(ss.NoOfBottle) ? "0" : ss.NoOfBottle)));
            }
        }

        public decimal ProductsAdjustmentValue
        {
            get
            {
                return (TestSchedule == null || !IsAdjustmentOnProducts ) ? 0 : 
                    TestSchedule.DiscountAsPercentage ? TestSchedule.Discount * ProductsSubtotal : 
                                                        TestSchedule.Discount;
            }
        }

        public decimal ProductsWithAdjustment
        {
            get
            {
                return ProductsSubtotal == 0 || ProductsSubtotal + ProductsAdjustmentValue <= 0 ? 0 : ProductsSubtotal + ProductsAdjustmentValue;
            }
        }

        public decimal ProductsWithTax
        {
            get
            {
                return (TestSchedule == null) ? 0 : ProductsWithAdjustment * TestSchedule.Tax;
            }
        }

        public decimal ProductsWithAdjustmentAndTax
        {
            get
            {
                return ProductsWithAdjustment + ProductsWithTax;
            }
        }

        #endregion

        #region Services

        public decimal ServicesSubtotal
        {
            get
            {
                return (TestServices == null) ? 0 : TestServices.Sum(ss => ss.Price);
            }
        }

        public decimal ServicesAdjustmentValue
        {
            get
            {
                return (TestSchedule == null || IsAdjustmentOnProducts) ? 0 : 
                        TestSchedule.DiscountAsPercentage ? TestSchedule.Discount * ServicesSubtotal : 
                                                            TestSchedule.Discount;
            }
        }

        public decimal ServicesWithAdjustment
        {
            get
            {
                return ServicesSubtotal == 0 || ServicesSubtotal + ServicesAdjustmentValue <= 0 ? 0 : ServicesSubtotal + ServicesAdjustmentValue;
            }
        }        

        #endregion        

        public decimal TestTotal
        {
            get
            {
                return ProductsWithAdjustmentAndTax + ServicesWithAdjustment;
            }
        }

        #endregion

        #endregion

        #region Handlers

        /// <summary>
        /// Notifies the change of the test issues list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TestSchedule_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => TestSchedule), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => TestSchedule));
        }

        /// <summary>
        /// Notifies the change of the state for the list Point Lookup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _listPointLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ListPointLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => ListPointLookup, () => ListPointLookup.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the type lookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TypeLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => TypeLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => TypeLookup, () => TypeLookup.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the state lookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StateLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => StateLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => StateLookup, () => StateLookup.Id));
        }

        /// <summary>
        /// Notifies the change of the item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => Item), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => Item, () => Item.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the protocol lookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TestProtocol_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => TestProtocol), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => TestProtocol, () => TestProtocol.Id));
        }

        /// <summary>
        /// Notifies the change of the test issues list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TestIssues_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => TestIssues), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => TestIssues));
        }

        /// <summary>
        /// Notifies the change of the test imprintable items list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TestImprintableItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => TestImprintableItems), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => TestImprintableItems));
        }

        /// <summary>
        /// Notifies the change of the test services list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TestServices_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => TestServices), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => TestServices));
        }

        /// <summary>
        /// Notifies about the change of shipping order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ShippingOrders_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ShippingOrders), new ErrorInfo());
            //SetModifiedState(ExpressionHelper.GetPropertyName(() => ShippingOrders));
        }

        /// <summary>
        /// Handles property changes in TestMainIssue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestMainIssue_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => TestMainIssue), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => TestMainIssue));
        }
        #endregion

        #region Validation

        /// <summary>
        /// Gets the validation errors according to the below cases.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="info">The error info.</param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            if (propertyName == ExpressionHelper.GetPropertyName(() => Name))
            {
                IsRequiredStringPropertyValid(propertyName, Name, info);
            }

            else if ((propertyName == ExpressionHelper.GetPropertyName(() => TypeLookup) && !TypeLookup.Validate() ))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            else if((propertyName == ExpressionHelper.GetPropertyName(() => StateLookup) && !StateLookup.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            else if (propertyName == ExpressionHelper.GetPropertyName(() => TestIssues) && !ValidateTestIssues())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            else if (propertyName == ExpressionHelper.GetPropertyName(() => TestMainIssue) && !TestMainIssue.IsValid)
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            UpdateErrorsSummary(info);
        }

        /// <summary>
        /// Implements the IDXErrorInfo.
        /// </summary>
        /// <param name="info"></param>
        public virtual void GetError(ErrorInfo info)
        {
            
        }

        /// <summary>
        /// Checks the properties by calling the Get Property error.
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (!IsChanged) return true;

            ValidationErrors.Clear();

            foreach (PropertyInfo property in GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }

            return IsValid;
        }

        /// <summary>
        /// Validates the Test Issues.
        /// </summary>
        /// <returns></returns>
        public bool ValidateTestIssues()
        {
            return TestIssues.All(testIssues => testIssues.Validate());
        }

        #endregion
    }
}