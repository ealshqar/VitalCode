using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.AutoTestDestination;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestSource
{
    public class StageAutoItem : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private AutoProtocolStage _autoProtocolStage;
        private StageAutoItem _stageAutoItemsParent;
        private AutoItem _autoItem;
        private TestingPoint _testingPoint;
        private Lookup _scanningMethod;
        private Lookup _childsOrderType;
        private Lookup _childsScanningType;
        private int _order;
        private int _scansNumber;
        private int _matchesNumber;
        private bool _finishAllScanRounds;
        private string _directAccessChecks;

        public BindingList<DirectAccessScanCheck> _directAccessScanChecks;

        private BindingList<StageAutoItem> _stageAutoItems;

        #region MemoryOnly Variables

        private bool _childesLoaded;
        private bool _childesChecked;
        private bool _addAllChildesAsResultsChecked;
        private bool _dilutionsChecked;
        private AutoItemScanState _scanState;
        private BindingList<StageAutoItem> _testingPathItems;
        private AutoTest _currentAutoTest;

        #endregion

        #endregion

        #region LookupEnums

        /// <summary>
        /// AutoItemScanningMethod Enum
        /// </summary>
        public AutoItemScanningMethod ScanningMethodEnum
        {
            get
            {
                return EnumNameResolver.LookupAsEnum<AutoItemScanningMethod>(ScanningMethod);
            }
        }

        /// <summary>
        /// AutoItemChildsOrderType Enum
        /// </summary>
        public ChildsOrderType ChildsOrderTypeEnum
        {
            get
            {
                return EnumNameResolver.LookupAsEnum<ChildsOrderType>(ChildsOrderType);
            }
        }

        /// <summary>
        /// AutoItemChildsScanningType Enum
        /// </summary>
        public ChildsScanningType ChildsScanningTypeEnum
        {
            get
            {
                return EnumNameResolver.LookupAsEnum<ChildsScanningType>(ChildsScanningType);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the AutoProtocolStage.
        /// </summary>
        public AutoProtocolStage AutoProtocolStage
        {
            get { return _autoProtocolStage; }
            set
            {
                if (_autoProtocolStage == null || !_autoProtocolStage.Equals(value))
                {
                    _autoProtocolStage = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the StageAutoItemsParent.
        /// </summary>
        public StageAutoItem Parent
        {
            get { return _stageAutoItemsParent; }
            set
            {
                if (_stageAutoItemsParent == null || !_stageAutoItemsParent.Equals(value))
                {
                    _stageAutoItemsParent = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the AutoItem.
        /// </summary>
        public AutoItem AutoItem
        {
            get { return _autoItem; }
            set
            {
                if (_autoItem == null || !_autoItem.Equals(value))
                {
                    _autoItem = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the TestingPoint.
        /// </summary>
        public TestingPoint TestingPoint
        {
            get { return _testingPoint; }
            set
            {
                if (_testingPoint == null || !_testingPoint.Equals(value))
                {
                    _testingPoint = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_testingPoint == null) return;
                _testingPoint.PropertyChanged += TestingPoint_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the ScanningMethod.
        /// </summary>
        public Lookup ScanningMethod
        {
            get { return _scanningMethod; }
            set
            {
                if (_scanningMethod == null || !_scanningMethod.Equals(value))
                {
                    _scanningMethod = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_scanningMethod == null) return;
                _scanningMethod.PropertyChanged += ScanningMethod_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the ChildsOrderType.
        /// </summary>
        public Lookup ChildsOrderType
        {
            get { return _childsOrderType; }
            set
            {
                if (_childsOrderType == null || !_childsOrderType.Equals(value))
                {
                    _childsOrderType = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_childsOrderType == null) return;
                _childsOrderType.PropertyChanged += ChildsOrderType_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the ChildsScanningType.
        /// </summary>
        public Lookup ChildsScanningType
        {
            get { return _childsScanningType; }
            set
            {
                if (_childsScanningType == null || !_childsScanningType.Equals(value))
                {
                    _childsScanningType = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_childsScanningType == null) return;
                _childsScanningType.PropertyChanged += ChildsScanningType_PropertyChanged;
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
                if (!_order.Equals(value))
                {
                    _order = value;
                    //SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the ScansNumber.
        /// </summary>
        public int ScansNumber
        {
            get { return _scansNumber; }
            set
            {
                if (!_scansNumber.Equals(value))
                {
                    _scansNumber = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the MatchesNumber.
        /// </summary>
        public int MatchesNumber
        {
            get { return _matchesNumber; }
            set
            {
                if (!_matchesNumber.Equals(value))
                {
                    _matchesNumber = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the FinishAllScanRounds.
        /// </summary>
        public bool FinishAllScanRounds
        {
            get { return _finishAllScanRounds; }
            set
            {
                if (!_finishAllScanRounds.Equals(value))
                {
                    _finishAllScanRounds = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the DirectAccessChecks.
        /// </summary>
        public string DirectAccessChecks
        {
            get { return _directAccessChecks; }
            set
            {
                if (_directAccessChecks == null || !_directAccessChecks.Equals(value))
                {
                    _directAccessChecks = value;
                    //SetModifiedState(MethodBase.GetCurrentMethod().Name);//Disabled to avoid cross-threading exceptions when updating collection during scanning
                }
            }
        }

        /// <summary>
        /// Gets or sets the StageAutoItems.
        /// </summary>
        public BindingList<StageAutoItem> StageAutoItems
        {
            get { return _stageAutoItems; }
            set
            {
                if (_stageAutoItems != null && _stageAutoItems == value) return;

                _stageAutoItems = value;

                //IMPORTANT: Commented out because it was causing threading exceptions when the collection was adjusted during scanning
                //_stageAutoItems.RaiseListChangedEvents = true;
                //_stageAutoItems.ListChanged += StageAutoItems_ListChanged;
            }
        }

        #endregion

        #region Scanning Memory Only Properties

        #region Process

        #region General

        /// <summary>
        /// List of ScanBookmarks for the StageAutoItem, these will allow for dynamic navigation across multiple levels of scanning
        /// </summary>
        public List<ScanBookmark> ScanBookmarks { get; set; }

        /// <summary>
        /// A list of Sub StageAutoItems used for scanning, this includes split/switch operations
        /// </summary>
        public BindingList<StageAutoItem> ScanItems { get; set; }

        /// <summary>
        /// Indicates if StageAutoItems is not null which means it was loaded
        /// </summary>
        public bool ChildesLoaded
        {
            get
            {
                return (StageAutoItems != null && StageAutoItems.Any()) || _childesLoaded;
            }
            set
            {
                _childesLoaded = value;
            }
        }

        /// <summary>
        /// Indicates if the StageAutoItems collection is not empty
        /// </summary>
        public bool HasStageAutoItems
        {
            get
            {
                return ChildesLoaded && StageAutoItems.Any();
            }
        }

        /// <summary>
        /// Indicates if the ScanItems collection is not empty
        /// </summary>
        public bool HasScanItems
        {
            get
            {
                return ScanItems != null && ScanItems.Any();
            }
        }

        /// <summary>
        /// Indicates if the current ScanBookmark was counted in progress calculation
        /// </summary>
        public bool ProgressRecorded { get; set; }

        #endregion

        #region Split/Switch

        /// <summary>
        /// The number of the items in the first half of the ScanItems collection
        /// </summary>
        public int FirstHalfCount { get; set; }

        /// <summary>
        /// The number of the items in the second half of the ScanItems collection
        /// </summary>
        public int SecondHalfCount
        {
            get
            {
                return !HasScanItems ? 0 : ScanItems.Count - FirstHalfCount;
            }
        }

        #endregion

        #region Multi-Level Scanning

        /// <summary>
        /// Collection of testing path items
        /// </summary>
        public BindingList<StageAutoItem> TestingPathItems
        {
            get
            {
                //If the collection was null then initialize it
                if (_testingPathItems == null)
                {
                    _testingPathItems = new BindingList<StageAutoItem>();

                    //If this StageAutoItem is a Root one then add it as the first item in the testing path items
                    if (IsRoot)
                    {
                        _testingPathItems.Add(this);
                    }
                }

                return _testingPathItems;
            }
        }

        /// <summary>
        /// Gets the current testing path parent
        /// </summary>
        public StageAutoItem TestingPathParent
        {
            get
            {
                return TestingPathItems == null || !TestingPathItems.Any()?  null :
                       IsTestingOnRoot? this : TestingPathItems.LastOrDefault();
            }
        }

        /// <summary>
        /// Gets the current testing path parent
        /// </summary>
        public StageAutoItem TestingPathParentWithResult
        {
            get
            {
                return TestingPathItems == null ||
                      !TestingPathItems.Any() ? null :
                       TestingPathItems.LastOrDefault(item => (item.IsRoot || item.AddResultOnMatch) && item.HasResult);
            }
        }

        /// <summary>
        /// Indicates if we are currently testing on root
        /// </summary>
        public bool IsTestingOnRoot
        {
            get
            {
                return TestingPathItems.Count() <= 1;
            }
        }

        /// <summary>
        /// Indicates if the current StageAutoItem is a root item listed directly under the Stage
        /// </summary>
        public bool IsRoot
        {
            get
            {
                return AutoProtocolStage != null;
            }
        }

        /// <summary>
        /// Indicates if the current item has a result
        /// </summary>
        public bool HasResult
        {
            get
            {
                return AddResultOnMatch && AutoTestResult != null;
            }
        }

        /// <summary>
        /// The referenced AutoTestResult
        /// </summary>
        public AutoTestResult AutoTestResult { get; set; }

        /// <summary>
        /// The current auto tests
        /// </summary>
        public AutoTest CurrentAutoTest
        {
            get
            {
                return _currentAutoTest;
            }
            set
            {
                if (_currentAutoTest == null)
                {
                    _currentAutoTest = value;
                }
            }
        }

        /// <summary>
        /// Indicates if the current StageAutoItem belongs to an item that is accessed using direct access method and not drill down
        /// </summary>
        public bool IsDirectAcessScanCheck { get; set; }

        #endregion

        #region Scanning Parameters

        /// <summary>
        /// Gets or sets IsMatchesNumberPushedBack
        /// </summary>
        public bool IsMatchesNumberPushedBack { get; set; }

        /// <summary>
        /// Gets or sets FoundMatchesNumber
        /// </summary>
        public int FoundMatchesNumber { get; set; }

        /// <summary>
        /// Indicates if the matches number needed has been achived
        /// </summary>
        public bool MatchesNumberAchieved
        {
            get
            {
                return FoundMatchesNumber >= MatchesNumber;
            }
        }

        /// <summary>
        /// Indicates if scanning can be ended based on matches number
        /// </summary>
        public bool CanEndScanningByMatchesNumber
        {
            get
            {
                //Important:
                //We are using HasScanItems and not HasStageAutoItems because HasScanItems depends on ScanItems
                //list which excludes added results which is important specially if all items were added as results
                //and there are no more items to scan
                return !HasScanItems || (MatchesNumberAchieved && FinishAllScanRounds == false);
            }
        }

        /// <summary>
        /// Indicates if scanning rounds expired
        /// </summary>
        public bool ScanningRoundsExpired
        {
            get
            {
                var scanningRoundsBookmark = GetStepScanBookmark(ScanBookmarkType.ScanningRounds);

                return scanningRoundsBookmark != null && scanningRoundsBookmark.IsLast;
            }
        }

        /// <summary>
        /// Indicates if MultiLevelScanning expired
        /// </summary>
        public bool MultiLevelScanningExpired
        {
            get
            {
                var multiLevelScanningBookmark = GetStepScanBookmark(ScanBookmarkType.MultiLevelScanning);

                return multiLevelScanningBookmark != null && multiLevelScanningBookmark.IsLast;
            }
        }

        /// <summary>
        /// Gets state of childes check
        /// </summary>
        public ScanCheckState ChildesCheck
        {
            get
            {
                if (CheckChildes)
                {
                    return _childesChecked ? ScanCheckState.Checked : ScanCheckState.Pending;
                }

                return ScanCheckState.NotNeeded;
            }
            set
            {
                _childesChecked = CheckChildes && value == ScanCheckState.Checked;
            }
        }

        /// <summary>
        /// Gets state of AddAllChildesAsResults check
        /// </summary>
        public ScanCheckState AddAllChildesAsResultsCheck
        {
            get
            {
                if (CheckAddAllChildesOnMatch)
                {
                    return _addAllChildesAsResultsChecked ? ScanCheckState.Checked : ScanCheckState.Pending;
                }

                return ScanCheckState.NotNeeded;
            }
            set
            {
                _addAllChildesAsResultsChecked = CheckAddAllChildesOnMatch && value == ScanCheckState.Checked;
            }
        }

        /// <summary>
        /// Indicates if the current StageAutoItem has any drill down or special scanning checks
        /// </summary>
        public bool HasDrillDownChecks
        {
            get
            {
                return CheckChildes ||
                       CheckAddAllChildesOnMatch ||
                       (DirectAccessScanChecks != null && DirectAccessScanChecks.Any());
            }
        }

        /// <summary>
        /// Indicates if scanning on current item is finished
        /// </summary>
        public bool IsScanningFinished
        {
            get
            {
                //Important:
                //We are using HasScanItems and not HasStageAutoItems because HasScanItems depends on ScanItems
                //list which excludes added results which is important specially if all items were added as results
                //and there are no more items to scan
                return !HasScanItems || 
                    //For the case of Sequential Scanning we have different definition of scanning finished state than in the case of eliminiation, in elimination we considered the scanning
                    //finised if the matches number is found or if the scanning rounds are expired. However in the sequential case we consider the scanning finished if the number of matches
                    //is reached already but it if is not reached then we finish scanning only if the scanning rounds are expired and also the MultiLevelScanning steps are also expired to make
                    //sure the scanning continues checking the list after the first match is found and doesn't just end the whole list scanning. Notice that as soon as we start scanning the list
                    //then the ScanningRoundsExpired flag already becomes true so without relying on MultiLevelScanningExpired, the scanning round ends and doesn't give a chance for further testing
                    //to find multiple matches within the same round in case multiple matches were required and the MatchesNumber required was more than 1.
                    (ChildsScanningTypeEnum == Shared.ChildsScanningType.ChildsScanningTypeSequential? (CanEndScanningByMatchesNumber || (ScanningRoundsExpired && MultiLevelScanningExpired)):
                                                                                                       (CanEndScanningByMatchesNumber || ScanningRoundsExpired));
            }
        }

        /// <summary>
        /// Scanning State
        /// </summary>
        public AutoItemScanState ScanState
        {
            get
            {
                return IsScanningFinished ? AutoItemScanState.Finished : _scanState;
            }
            set
            {
                _scanState = value;
            }
        }

        #endregion

        #endregion

        #region Data

        /// <summary>
        /// List of dosage options from all ProductForms related to the product of current stage auto item
        /// </summary>
        public BindingList<DosageOption> DosageOptions { get; set; }

        /// <summary>
        /// List of direct acces items generated based on the comma separated string DirectAccessChecks
        /// </summary>
        public BindingList<DirectAccessScanCheck> DirectAccessScanChecks
        {
            get
            {
                //If the list is null then set its value
                if (_directAccessScanChecks == null)
                {
                    if (!string.IsNullOrEmpty(DirectAccessChecks))
                    {
                        //If the comma separated string isn't empty then convert to a list of string
                        //This approach makes it relatively easy to add/edit the list of keys instead of storing them in their own table.
                        _directAccessScanChecks = new BindingList<DirectAccessScanCheck>();

                        foreach (var checkKey in DirectAccessChecks.Split(',').ToList())
                        {
                            _directAccessScanChecks.Add(new DirectAccessScanCheck { Key = checkKey, IsChecked = false});
                        }

                        _directAccessScanChecks.RaiseListChangedEvents = true;
                        _directAccessScanChecks.ListChanged += directAccessScanChecks_ListChanged;//IMPORTANT: Handling of updating the DirectAccessItemsString based on changes in the list
                    }
                }

                return _directAccessScanChecks;
            }
            set
            {
                _directAccessScanChecks = value;

                SetDirectAccessCheckStringBasedOnChecksList();
            }
        }

        /// <summary>
        /// The number of childes for a StageAutoItem (Loaded from database)
        /// </summary>
        public int ChildesCount { get; set; }

        /// <summary>
        /// Indicates if the StageAutoItem has child items
        /// </summary>
        public bool HasChildes
        {
            get
            {
                return ChildesCount > 0;
            }
        }

        #endregion

        #region AutoItem Based

        /// <summary>
        /// Gets value of AddResultOnMatch 
        /// </summary>
        public bool AddResultOnMatch { get { return AutoItem != null && AutoItem.AddResultOnMatch; } }

        /// <summary>
        /// Gets value of ExcludeOnMatch 
        /// </summary>
        public bool ExcludeOnMatch { get { return AutoItem != null && AutoItem.ExcludeOnMatch; } }

        /// <summary>
        /// Gets value of AddAllChildesOnMatch 
        /// </summary>
        public bool AddAllChildesOnMatch { get { return AutoItem != null && AutoItem.AddAllChildesOnMatch; } }

        /// <summary>
        /// Indicates if childes should be checked
        /// </summary>
        public bool CheckChildes { get { return HasStageAutoItems && !AddAllChildesOnMatch; } }

        /// <summary>
        /// Indicates if all childes should be added as results
        /// </summary>
        public bool CheckAddAllChildesOnMatch { get { return HasStageAutoItems && AddAllChildesOnMatch; } }

        #endregion

        #endregion

        #region Logic

        #region Scan Bookmarks

        /// <summary>
        /// Find the scan bookmark by type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public ScanBookmark GetStepScanBookmark(ScanBookmarkType type, ScanBookmarkSource source = ScanBookmarkSource.Undefined)
        {
            //Validate that the StageAutoItem has ScanBookmarks in it
            if (ScanBookmarks == null || !ScanBookmarks.Any()) return null;

            //Find bookmark by type and source
            //IMPORTANT:
            //We needed to add source information to StepBookmark because we had to support user navigation for adding
            //results and for delete. Because of this it is very important to get the right bookmark type & source based on 
            //case. Below we return the User bookmark only if the test wasn't in progress or if User bookmark was explicitly requested through
            //the source parameter which by default has an "Undefined" value. In all other cases we return the automation bookmark by default.
            return (CurrentAutoTest != null && CurrentAutoTest.CurrentTestStatus != AutoTestStatus.InProgress) ||
                   source == ScanBookmarkSource.User? ScanBookmarks.FirstOrDefault(bookmark => bookmark.Type == type && bookmark.Source == ScanBookmarkSource.User):
                    ScanBookmarks.FirstOrDefault(bookmark => bookmark.Type == type && bookmark.Source == ScanBookmarkSource.Automation);
        }

        /// <summary>
        /// Initializes the ScanBookmarks collection based on the stage, this action is performed once on each StageAutoItem for multi-level stage items
        /// </summary>
        /// <param name="stage"></param>
        public void InitializeScanBookmarks(StageTabKey stage)
        {
            //Only perform initialization if the scan bookmarks collection wasn't initialized already
            if(ScanBookmarks != null) return;

            //Perform initialization only for stages that have multiple levels
            if(stage != StageTabKey.Testing && stage != StageTabKey.Dosage) return;

            //Create the scanbookmarks based on the stage, notice that we use StageTabKey and not StageKey, the TabKey is more generic
            //and can cover more cases and can help this functionality work in future even when adding new stuff if it works the same way
            if (stage == StageTabKey.Testing)
            {
                ScanBookmarks = new List<ScanBookmark>
                {
                    new ScanBookmark { Type = ScanBookmarkType.ScanningRounds,Source = ScanBookmarkSource.Automation },
                    new ScanBookmark { Type = ScanBookmarkType.ScanningRounds,Source = ScanBookmarkSource.User },
                    new ScanBookmark { Type = ScanBookmarkType.MultiLevelScanning, Source = ScanBookmarkSource.Automation },
                    new ScanBookmark { Type = ScanBookmarkType.MultiLevelScanning, Source = ScanBookmarkSource.User }
                };
            }
            else if (stage == StageTabKey.Dosage)
            {
                ScanBookmarks = new List<ScanBookmark>
                {
                    new ScanBookmark { Type = ScanBookmarkType.DosageOptions, Source = ScanBookmarkSource.Automation },
                    new ScanBookmark { Type = ScanBookmarkType.DosageOptions, Source = ScanBookmarkSource.User },
                };
            }
        }

        /// <summary>
        /// Get the bookmark limit based on the requested bookmark type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetItemBookmarkLimit(ScanBookmarkType type)
        {
            switch (type)
            {
                case ScanBookmarkType.ScanningRounds:
                    return ScansNumber;
                case ScanBookmarkType.MultiLevelScanning:
                    if (ChildsScanningTypeEnum == Shared.ChildsScanningType.ChildsScanningTypeElimination)
                    {
                        //Extract the elimination rounds limit value from the test based on config keys
                        //If the test or the limit isn't available then use 3 as a default, this is just a precaution
                        return CurrentAutoTest == null || CurrentAutoTest.MutliLevelScanningEliminationRounds <= 0 ? 3 : CurrentAutoTest.MutliLevelScanningEliminationRounds;
                    }
                    return ScanItems.Count;

                case ScanBookmarkType.DosageOptions:
                    return DosageOptions.Count;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Moves the index of the bookmark to next index
        /// </summary>
        public void MoveBookmarkNext(ScanBookmarkType type, AutoTestStatus lastTestStatus)
        {
            //Find bookmark by type
            var scanBookmark = GetStepScanBookmark(type);

            //Return if the bookmark wasn't found
            if (scanBookmark == null) return;

            //Flag used to determine if the current step bookmark will be updated or not based on checks
            var setCurrentBookmakrDetails = false;

            //If the current index is empty, then get the first index
            if (scanBookmark.IsEmpty)
            {
                //Set starting index
                scanBookmark.Index = 0;

                //Since the index was updated, we set the flag to update the bookmark to true
                setCurrentBookmakrDetails = true;
            }

            //If the last status was in progress then get next index or finish if there are no steps left
            else if (lastTestStatus == AutoTestStatus.InProgress)
            {
                //We only increase the index if there are more steps, notice at this point the index is still referring to the last value, it hasn't increased yet.
                if (scanBookmark.NextOrCurrentIndexValue < GetItemBookmarkLimit(type))
                {
                    scanBookmark.Index += 1;

                    //Since the index was updated, we set the flag to update the bookmark to true
                    setCurrentBookmakrDetails = true;
                }
                //IMPORTANT: If there are no more items to scan, the scanning should be stopped, this will be handled in the scanning logic itself because this method won't be called
            }

            //Always update user step bookmark to match the current automation step bookmark
            if (scanBookmark.Source == ScanBookmarkSource.Automation)
            {
                //Get user bookmark by type
                var userStepBookmark = GetStepScanBookmark(type, ScanBookmarkSource.User);

                if (userStepBookmark != null)
                {
                    userStepBookmark.Index = scanBookmark.Index;
                }
            }

            //If the flag is true then update the bookmark fields
            if (setCurrentBookmakrDetails)
            {
                UpdateBookmarkMarkerByIndex(type);
            }
        }

        /// <summary>
        /// Updates the properties of the marker to match the details of current bookmark step
        /// </summary>
        public void UpdateBookmarkMarkerByIndex(ScanBookmarkType type)
        {
            //Find bookmark by type
            var scanBookmark = GetStepScanBookmark(type);

            //Return if the bookmark wasn't found
            if (scanBookmark == null) return;

            //Below we set the name of the ScanBookmark based on its type, each type takes its data from a different place and can depend
            //on multiple conditions to determine the right value
            switch (type)
            {
                case ScanBookmarkType.ScanningRounds:
                    scanBookmark.Name = " Scanning Round" + " (" + (scanBookmark.IndexValue + 1) + ")";
                    break;
                case ScanBookmarkType.MultiLevelScanning:

                    //Notice here that the name differs based on the child scanning type
                    if (ChildsScanningTypeEnum == Shared.ChildsScanningType.ChildsScanningTypeSequential)
                    {
                        scanBookmark.Name = GetCurrentScanItem(type).AutoItem.Name;
                    }
                    else
                    {
                        scanBookmark.Name = " Eliminiation" + " (" + (scanBookmark.IndexValue + 1) + ")";
                    }

                    break;
                case ScanBookmarkType.DosageOptions:
                    scanBookmark.Name = " Scanning Round" + " (" + (scanBookmark.IndexValue + 1) + ")" + " - " + CurrentDosageOption.Name;
                    break;
            }

            //scanBookmark.Name = AutoItem.Name;
            //scanBookmark.Key = AutoItem.Key;
            //scanBookmark.UIKey = AutoItem.Key;
            //scanBookmark.IsMultiLevel = StageAutoItems != null && StageAutoItems.Any();
            scanBookmark.IsLast = scanBookmark.NextOrCurrentIndexValue >= GetItemBookmarkLimit(type);//Here index is current

            //Always update user step bookmark to match the current automation step bookmark
            if (scanBookmark.Source == ScanBookmarkSource.Automation)
            {
                //Get user bookmark by type
                var userStepBookmark = GetStepScanBookmark(type, ScanBookmarkSource.User);

                if (userStepBookmark != null)
                {
                    userStepBookmark.Name = scanBookmark.Name;
                    userStepBookmark.Key = scanBookmark.Key;
                    userStepBookmark.UIKey = scanBookmark.Key;
                    userStepBookmark.IsMultiLevel = scanBookmark.IsMultiLevel;
                    userStepBookmark.IsLast = scanBookmark.IsLast;
                }
            }
        }

        /// <summary>
        /// Sets the step bookmark index based on a specific value
        /// </summary>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="skipUpdatingBookmarkInfo"></param>
        public void SetBookmarkIndex(int index, ScanBookmarkType type, bool skipUpdatingBookmarkInfo = false)
        {
            //Find bookmark by type
            var scanBookmark = GetStepScanBookmark(type);

            //Return if the bookmark wasn't found
            if (scanBookmark == null) return;

            scanBookmark.Index = index;

            if (!skipUpdatingBookmarkInfo)
            {
                UpdateBookmarkMarkerByIndex(type);
            }
        }

        /// <summary>
        /// Resets the details of a ScanBookmark based on its type
        /// </summary>
        public void ResetBookmarkMarker(ScanBookmarkType type)
        {
            //Find bookmark by type
            var scanBookmark = GetStepScanBookmark(type);

            //Return if the bookmark wasn't found
            if (scanBookmark == null) return;

            scanBookmark.Index = null;
            scanBookmark.Name = string.Empty;
            scanBookmark.Key = string.Empty;
            scanBookmark.UIKey = string.Empty;
            scanBookmark.IsMultiLevel = false;
            scanBookmark.IsLast = false;

            //Always update user step bookmark to match the current automation step bookmark
            if (scanBookmark.Source == ScanBookmarkSource.Automation)
            {
                //Get user bookmark by type
                var userStepBookmark = GetStepScanBookmark(type, ScanBookmarkSource.User);

                if (userStepBookmark != null)
                {
                    userStepBookmark.Index = null;
                    userStepBookmark.Name = string.Empty;
                    userStepBookmark.Key = string.Empty;
                    userStepBookmark.UIKey = string.Empty;
                    userStepBookmark.IsMultiLevel = false;
                    userStepBookmark.IsLast = false;
                }
            }
        }

        /// <summary>
        /// Resets the details of a ScanBookmark based on its index within collection
        /// </summary>
        public void ResetBookmarkMarker(int indexInCollection, bool resetFollowingBookmarks = false)
        {
            //Validate that the StageAutoItem has ScanBookmarks in it
            if (ScanBookmarks == null || !ScanBookmarks.Any()) return;

            //Find bookmark by index in collection
            var scanBookmark = ScanBookmarks.Count > indexInCollection? ScanBookmarks[indexInCollection] : null;

            //Return if the bookmark wasn't found
            if (scanBookmark == null) return;

            scanBookmark.Index = null;
            scanBookmark.Name = string.Empty;
            scanBookmark.Key = string.Empty;
            scanBookmark.UIKey = string.Empty;
            scanBookmark.IsMultiLevel = false;
            scanBookmark.IsLast = false;

            //Always update user step bookmark to match the current automation step bookmark
            if (scanBookmark.Source == ScanBookmarkSource.Automation)
            {
                //Get user bookmark by type
                var userStepBookmark = GetStepScanBookmark(scanBookmark.Type, ScanBookmarkSource.User);

                if (userStepBookmark != null)
                {
                    userStepBookmark.Index = null;
                    userStepBookmark.Name = string.Empty;
                    userStepBookmark.Key = string.Empty;
                    userStepBookmark.UIKey = string.Empty;
                    userStepBookmark.IsMultiLevel = false;
                    userStepBookmark.IsLast = false;
                }
            }

            //If the check is true then we call the logic again on following ScanBookmarks because they are considered decendants or childs of the current ScanBookmarks
            //and their scan activities are applied multiple times within the current ScanBookmark and so when the current ScanBookmark index is reset then we most probably need to
            //reset the child bookmarks too.
            //IMPORTANT: This of course assumes that ScanBookmarks in collection are childs of each other, so the first is the first step, the second is a child and if there is a third for
            //IMPORTANT: example then it is also a child, for example in Testing stage, first Scanbookmakr is ScanningRounds, its child is Testing Items Scanning regerdless 1x1 or Split/Switch
            //IMPORTANT: Now if we don't want this assumption, then we need to add a level to the ScanBookmark and explicitly define that to determine level for each ScanBookmark, this will also
            //IMPORTANT: allow us to define multiple ScanBookmarks at the same level using the same level #, this can be useful if we have multiple steps running at same level.
            if (resetFollowingBookmarks)
            {
                ResetBookmarkMarker(indexInCollection + 1, true);
            }
        }

        /// <summary>
        /// This logic moves a ScanBookmark to its last index to prevent further scanning activities on it, the bookmark is determined by type
        /// which is sent by the caller
        /// </summary>
        /// <param name="type"></param>
        public void MoveScanBookmarkToLast(ScanBookmarkType type)
        {
            //Validate that the StageAutoItem has ScanBookmarks in it
            if (ScanBookmarks == null || !ScanBookmarks.Any()) return;

            //Find bookmark by type
            var scanBookmark = GetStepScanBookmark(type);

            //Return if the bookmark wasn't found
            if (scanBookmark == null) return;

            SetBookmarkIndex(GetItemBookmarkLimit(type) - 1, type);
        }

        /// <summary>
        /// This logic moves a ScanBookmark to its previous index to allow further scanning activities on it since its parent matched again, the bookmark is determined by type
        /// which is sent by the caller
        /// </summary>
        /// <param name="type"></param>
        public void MoveScanBookmarkToPrevious(ScanBookmarkType type)
        {
            //Validate that the StageAutoItem has ScanBookmarks in it
            if (ScanBookmarks == null || !ScanBookmarks.Any()) return;

            //Find bookmark by type
            var scanBookmark = GetStepScanBookmark(type);

            //Return if the bookmark wasn't found
            if (scanBookmark == null) return;

            var beforeLastIndex = GetItemBookmarkLimit(type) - 1;

            SetBookmarkIndex(beforeLastIndex - 1, type);
        }

        #endregion

        #region General

        /// <summary>
        /// Reference to the current scan item based on bookmark index
        /// </summary>
        public StageAutoItem GetCurrentScanItem(ScanBookmarkType type)
        {
            //Find bookmark by type
            var scanBookmark = GetStepScanBookmark(type);

            //Return null if the bookmark wasn't found
            if (scanBookmark == null) return null;

            //Return null if there are no ScanItems
            if (!HasScanItems) return null;

            //Return null if the ScanBookmark type doesn't support returning StageAutoItems
            switch (type)
            {
                case ScanBookmarkType.MultiLevelScanning:
                    //Here it is very important to Order the ScanItems by order property because we can't depend on their original order
                    //inside the collection, the UI uses an order column to order the rows visually and so the logic will have to worry about
                    //ordering the items each time it needs to perform an action that requires a specific order
                    return ChildsScanningTypeEnum == Shared.ChildsScanningType.ChildsScanningTypeElimination ?
                                                     ScanItems.OrderBy(si => si.Order).FirstOrDefault() :
                                                     ScanItems.OrderBy(si => si.Order).ToList()[scanBookmark.IndexValue];
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets current dosage option based on the ScanBookmark index
        /// </summary>
        public DosageOption CurrentDosageOption
        {
            get
            {
                var dosageOptionBookmark = GetStepScanBookmark(ScanBookmarkType.DosageOptions);

                if (dosageOptionBookmark != null && !dosageOptionBookmark.IsEmpty)
                {
                    return DosageOptions[dosageOptionBookmark.IndexValue];
                }

                return null;
            }
        }

        /// <summary>
        /// Set the ScanItems collection by creating a clone from the origianl ScanItemsCollection and set other flags needed for scanning
        /// </summary>
        public void SetUpdateScanItems()
        {
            //Only set the ScanItems collection if it was null, this is important to avoid resetting this collection if this logic was called again when pausing the
            //scan for example
            if (ScanItems == null)
            {
                //If the original StageAutoItems collection is empty then just set the ScanItem to an empty collection, if it is not empty then clone the items
                //in it in a new collection
                ScanItems = !HasStageAutoItems ? new BindingList<StageAutoItem>() : new BindingList<StageAutoItem>(StageAutoItems);

                //Notice that we are initializing the collection like this "new BindingList<StageAutoItem>(StageAutoItems)" and this is important because it creates
                //a clone of the original collection without keeping a reference to it, this is important because any changes we do to the ScanItems like split/switch
                //will not affect the original StageAutoItems collection which is important because we need it after the scanning is done to set the items again based on it.

                //IMPORTANT: Reset MultiLevelScanning bookmark each time we reset the ScanItems
                //When the ScanItems list is scanned for the first time then the logic below won't do much because the bookmark should already be empty however it
                //becomes important when scanning a list multiple times in Elimination scanning mode and that is because if we do the split/switch multiple times
                //without getting a match then the list should be reset to start a new scanning round, in this case the MultiLevelScanning bookmark should be reset
                //too to start from the begining, otherwise it would start from the last index which is incorrect.
                ResetBookmarkMarker(ScanBookmarkType.MultiLevelScanning);
            }

            //Handle filtering out matched items that should be excluded from the list.
            FilterResultsAndUpdateLimitAndIndex();

            //Set the number of items in the first half
            SetFirstHalfCountByCurrentList();
        }

        /// <summary>
        /// Update the ScanItems list to filter out any items that also exist in results and should be excluded
        /// </summary>
        public void FilterResultsAndUpdateLimitAndIndex()
        {
            //IMPORTANT:
            //In the check below we are using HasStageAutoItems and HasScanItems because if the ScanItems list is empty then the logic below won't be executed even though
            //there should be a case where it should which is when setting the ScanItems after deleting a test result that was just marked as a result without being saved, in such case
            //the item should be included in the ScanItems list but if the ScanItems list was empty (For example if user added all items as results) then the check would prevent the logic
            //below from execution, because of this we are using HasStageAutoItems instead
            if(!HasStageAutoItems || CurrentAutoTest == null || !CurrentAutoTest.TestingResults.Any()) return;

            //IMPORTANT:
            /* In Phase 2 of automation we are required to exclude items from the scanning list if those items are configured to be excluded after they are matched.
             * This can be done by comparing the ScanItems list to the AutoTest results list and remove the common items that have the right configuration.
             * 
             * However the removal of the items is not so simple because it can happen in a variety of conditions and each affect the process in its own way:
             * 1- The matched items may need to be exlcuded while scanning in elimination or 1x1 modes and each require different handling.
             *   - Elimination:
             *      - In this case the round ends when finding a match or when reaching the limit but the important point is that if a match is found then the round doesn't continue
             *      and it needs to be reset, this means that the matches could just be excluded but this needs to happen before or during the next scanning round.
             *   - 1x1:
             *      - In this case, things are much different because a scanning round can allow multiple matches and the behavior of the scanning depends on the number of matches
             *      and the requirement to finish all scanning rounds. This means that after finding a match, not only the list needs to be filtered, but also the limit of the list
             *      needs to be adjusted and also the index of the scanning needs to be adjusted based on the filtration. In addition to that we can see that the filtration will be required
             *      to happen during the multi-level scanning which means it can happen multiple times within a single scanning round, this means that it might be called in different locations
             *      compared to the elimination case.
             * 2- The exclusion could be a result of an item that was just matched in the current list or it could be because the item was matched at a deeper level after drilling down
             * and when coming back up to the top this item was already added to results and so we should exclude it to avoid testing it again. This means that when resuming/starting scanning
             * activities on only list it has to be updated before the scanning start exclude an items in that may have been scanned and matched at deeper levels. In the case of elimination this
             * might not be a problem because after finding a match the round simply ends and we start a new round, in such case we only exclude any matches but in the case of the 1x1 this could 
             * be problematic because the previous index location becomes invalid and also the limit of the list itself may have shrunk and so both values need to be adjusted, the index here has
             * a special case because it needs to offset itself only by the number of matches that were excluded before it, any matches that are yet to be scanned and are excluded shouldn't affect
             * the index.
             * 3- The exclusion of the items need to be smart to avoid adding the same item as a result twice, this scenario can happen if ExcludeOnMatch = False, in such case if an item was tested
             * and matched then by default it won't be excluded from the list, because of this an item should also be excluded if it was tested and matched even if it has ExcludeOnMatch = False but
             * the difference is that it should be excluded only for the current parent, meaning that if we visit a list where this item exists again then we keep it in list as long as it doesn't exist
             * as a result under the current parent. So in general an item should be excluded from the current scanning list if it has ExcludeOnMatch = True and it exists anywhere within the results or
             * if it has ExcludeOnMatch = False and it exists as a result under the current result parent.
             */

            //Select items to exclude from ScanItems
            //var itemsToExclude = StageAutoItems.Where(
            //    stageAutoItem =>
            //        //Case 1: ExcludeOnMatch = True and the item has a match anywhere within the results list.
            //        (stageAutoItem.ExcludeOnMatch && CurrentAutoTest.TestingResults.Any(result => result.AutoItem != null && result.AutoItem.Id == stageAutoItem.AutoItem.Id && !result.IsDeleted) ||

            //        //Case 2: ExcludeOnMatch = False and the item has a match within the current parent if the current parent has result
            //        (!stageAutoItem.ExcludeOnMatch && 
            //        CurrentAutoTest.CurrentTestingPathParentWithResult != null && 
            //        CurrentAutoTest.CurrentTestingPathParentWithResult.HasResult &&
            //        CurrentAutoTest.TestingResults.Where(siblingResult => siblingResult.StructureParentId == CurrentAutoTest.CurrentTestingPathParentWithResult.AutoTestResult.StructureId)
            //        .Any(childResult => childResult.AutoItem != null && childResult.AutoItem.Id == stageAutoItem.AutoItem.Id && !childResult.IsDeleted)))).ToBindingList();

            var itemsToExclude = StageAutoItems.Where(
                stageAutoItem =>
                    //Case 1: ExcludeOnMatch = True and the item has a match anywhere within the results list.
                    (stageAutoItem.ExcludeOnMatch && CurrentAutoTest.TestingResults.Any(result => result.AutoItem != null && result.AutoItem.Id == stageAutoItem.AutoItem.Id && !result.IsDeleted) ||

                    //Case 2: ExcludeOnMatch = False and the item has a match within the current parent if the current parent has result
                    (!stageAutoItem.ExcludeOnMatch &&
                    !stageAutoItem.HasScanItems &&
                    CurrentAutoTest.CurrentTestingPathParentWithResult != null &&
                    CurrentAutoTest.CurrentTestingPathParentWithResult.HasResult &&
                    CurrentAutoTest.TestingResults.Where(siblingResult => siblingResult.StructureParentId == CurrentAutoTest.CurrentTestingPathParentWithResult.AutoTestResult.StructureId)
                    .Any(childResult => childResult.AutoItem != null && childResult.AutoItem.Id == stageAutoItem.AutoItem.Id && !childResult.IsDeleted)))).ToBindingList();

            //If there are no items to exclude then just apply the match exclusion logic to restore any items that user may have deleted
            //but nothing else should be done
            if (itemsToExclude.Count == 0)
            {
                ExcludeMatchedItems(itemsToExclude);//IMPORTANT: THIS LOGIC IS ONLY NEEDED TO RESTORE ANY RESULTS THAT USER DELETED SO THEY SHOW UP AGAIN IN TESTING LIST, IF THIS CAUSES ISSUES THEN DISABLE IT AND INVESTIGATE .. IT IS NOT A VERY IMPORTANT OPTION TO KEEP
                return;
            }

            //The case of sequential scanning requires special handling for filtration of results because a single scanning round allows multiple matches unlike the case
            //of elimination scanning and this means that after finding a match in the list and possibly drilling down, when drilling back up again the list may be required
            //to exclud multiple items and not just the one that matched and these items could exist anywhere in the list compared to the last index value before the drill down.
            //This means that the index needs to be adjusted to match the new scenario. The handling also depends on how the current StageAutoItem is configured and whether it is
            //required to finish all rouds or not.
            if (ChildsScanningTypeEnum == Shared.ChildsScanningType.ChildsScanningTypeSequential)
            {
                if (FinishAllScanRounds || !MatchesNumberAchieved)
                {
                    //Here we need to adjust both the limit and the index, the limit will be automatically adjusted when we remove the matched items, the index needs special handling
                    var multiLevelScanBookmark = GetStepScanBookmark(ScanBookmarkType.MultiLevelScanning);

                    //The condition "multiLevelScanBookmark.IndexValue != 0" prevented correct index handling when first item in list matches
                    //if (multiLevelScanBookmark != null && !multiLevelScanBookmark.IsEmpty && multiLevelScanBookmark.IndexValue != 0)
                    if (multiLevelScanBookmark != null && !multiLevelScanBookmark.IsEmpty)
                    {
                        var currentIndex = multiLevelScanBookmark.Index.HasValue? multiLevelScanBookmark.Index.Value : -1;

                        //IMPORTANT: We only adjust the index if this is an Automation step bookmark not a User step bookmark
                        //If the current bookmark is an Automation, then move the index handle offest caused by excluded items while scanning.
                        //If the bookmark is a User bookmark however, then no index ofsetting is required and we only need to exclude matched items.
                        if (multiLevelScanBookmark.Source == ScanBookmarkSource.Automation)
                        {
                            /*The index should be adjusted to only account for items that were scanned previously on the same list and didn't match but then later after drilling down to
                             * a result where scanned again and matched and so when drilling back up and going back to the list to continue scanning on it those same items are supposed to
                             * be excluded from the list, doing so will push the index forward because the whole list will become shorter and so the index needs to move back by the same
                             * number of items that matched to make sure the index remains correct and that the scanning doesn't miss any item that still needs to be scanned. This handling
                             * only covers items that their order comes before the current index (They were scanned already), any items that are yet to be scanned should not affect the index.
                            */
                            /* Notice below we offset the index by items that are to be excluded and their order was before the current index.
                                * We also deduct 1 from the offset value to account for the starting index of 0, for example when the index is 0, we have 1 item and so since the index doesn't
                                * start at 1, then we have to account for that in our offset, it is like using i < items.count() - 1 in for loop.
                            */
                            var offset = ScanItems.Count(scanItem => itemsToExclude.Any(itemToExclude => itemToExclude == scanItem) && ScanItems.IndexOf(scanItem) <= currentIndex);
                            offset = offset - 1; //Here we deduct 1 to account the starting index of 0

                            /*
                                * In addition to the number of items to account for, the index needs to deduct an additional 1 to make sure that the item currently highlighted gets scanned because
                                * shortly after this logic the index gets pushed to the next number and so if that happens without deducting 1 then the logic will skip the item that it was supposed to scan
                                * next and so we dedeuct 1 to avoid this case. This means that temporarily the information in the ScanBookmark will belong to an item that was already scanned but
                                * it should refresh as soon as the code reaches the segment where it increases the bookmark index and updates the UI.
                                */
                            currentIndex = currentIndex - offset - 1;

                            //IMPORTANT: Handling Correct Index After Match
                            /* Regardless of the number of items that we need to exclude, the index of the first item in the list will always be 0 and that is where we need to scan next
                            * however the logic here is designed to apply offset on the index by the amount of items excluded and not push the index next simply because there is specific
                            * logic just for that purpose and it will be called afterwards, if we push the index from now then that logic will push the index too and this causes a jump and
                            * an item will be excluded, to avoid this, we need to accept the possibility of having a negative number in the index for now but it should be adjusted when moving
                            * to the next index but we only accept an offset of 1 but not more than that, so -1 is accepted because the logic will move to index 0 but -2 for example is not ok
                            * because the index will be moved to -1 and there is no such index in the list, the check below shouldn't be required but we will keep it here just in case.
                            */
                            if (currentIndex < -1)
                            {
                                currentIndex = -1;
                            }
                        }

                        //IMPORTANT: Items should be excluded before updating the index or otherwise the ScanBookmark info will still be linked to the item that previously matched
                        ExcludeMatchedItems(itemsToExclude);

                        //IMPORTANT: This should be done after the matches are excluded
                        //While no index offsetting is needed in case of user selection we still need to validate that index is still within range
                        //because the user might add the last item in list as a result and so this causes the current index to become invalid
                        //since it becomes out of range, to handle this we need to move the index to the last item in list.
                        if (multiLevelScanBookmark.Source == ScanBookmarkSource.User)
                        {
                            //If the index is out of range then adjust the index accordingly
                            if (ScanItems.Count <= currentIndex)
                            {
                                //If there are items still then we move the index to the last item in list
                                if (ScanItems.Any())
                                {
                                    currentIndex = ScanItems.Count - 1;
                                    SetBookmarkIndex(currentIndex, ScanBookmarkType.MultiLevelScanning);
                                }
                                else
                                {
                                    //If there are no more items to scan then we reset the index
                                    ResetBookmarkMarker(ScanBookmarkType.MultiLevelScanning);
                                }
                            }
                        }
                        else
                        {
                            //IMPORTANT: Update the index to move it back based on the offset adjustments move above
                            //IMPORTANT: Notice that we set the parameter "" to true to avoid updating the bookmark based on the current index value which is invalid
                            //The current index value is set to a previous value to allow the logic moving forward to push the index correctly and avoid skipping items and so there is no
                            //value behind updating the bookmark based on the index value, in best case it would set it to incorrect info, in worst cause it causes exception if the index
                            //is -1 for example.
                            SetBookmarkIndex(currentIndex, ScanBookmarkType.MultiLevelScanning, true);
                        }
                    }
                    else
                    {
                        //We added the logic to exclude matched items here too because the logic reaches this current case when adding results on a test that was already ended and
                        //got loaded from the DB, in this case there are no ScanBookmark so no index needs to be adjusted but we still need to excluded added results.
                        ExcludeMatchedItems(itemsToExclude);
                    }
                }
                else
                {
                    //The index shouldn't be adjusted in this case and in fact it should be set to 0 because we found all the matches we need and we are not required to finish all rounds.
                }
            }
            else
            {
                //In the case of eliminiation we just exlcude the items because there are no updates on the index
                ExcludeMatchedItems(itemsToExclude);
            }
        }

        /// <summary>
        /// Remove matched items from the StageAutoItems list and from ScanItems
        /// </summary>
        /// <param name="itemsToExclude"></param>
        private void ExcludeMatchedItems(BindingList<StageAutoItem> itemsToExclude)
        {
            //IMPORTANT:
            //Instead of removing excluded items from teh ScanItems list we are overriding the whole list to only keep the items we need, this is very important because
            //of an issue with shared references. Previously we used to remove the items to exclude from ScanItems list and this was working however this approach caused those
            //same items to be also removed from the StageAutoItems list, this is happening because the object itself is the same in the two collections and even thought we tried
            //to use a specific syntax to create the ScanItems list as a separate copy from StageAutoItems the objects in both collections are still the same. A true clonse can be
            //done by creating a complete new instance of the object and copy each property separately which is too much work and maintenance. We figured we could avoid this by setting
            //the ScanItems list each time from scratch instead of removing matches from it however the downside here is that each time we set this list the linked gridcontrol has
            //its FocusedRowHandle change, we fixed this by resetting the FocusedRowHandle based on the bookmark index and the issue was solved. We made this whole change because we faced
            //an issue when deleting a result from the results list and this result was just added manually by the user using "Mark As Result" option and without saving the user went
            //back to the result they added and deleted it, in such case the result should show up in the testing list (Based on ScanItems) however this didn't work because the system couldn't
            //restore the result from StageAutoItem because it was also deleted from this list when the system removed the item from ScanItems list when it matched.
            var filteredScanItems = new BindingList<StageAutoItem>(StageAutoItems.Where(item => itemsToExclude.All(itemToExclude => itemToExclude != item)).ToBindingList());

            ScanItems = new BindingList<StageAutoItem>(filteredScanItems);

            //IMPORTANT: OLD BUT KEPT FOR REFERENCE
            //Remove the items to exclude
            //We use here for and foreach to avoid cross reference issue
            //Even though the object we are removing is loaded from the itemsToExclude collection and not from ScanItems, the logic below can cause a cross reference
            //error if we used foreach instead of "for", it is as if the for each is applied on the ScanItems collection deep down and so using "for" helps avoid this issue.
            //for (var i = 0; i < itemsToExclude.Count(); i++)
            //{
            //    var itemToExclude = itemsToExclude[i];

            //    //We are using lock to avoid using the list by multiple threads
            //    lock (ScanItems)
            //    {
            //        ScanItems.Remove(itemToExclude);
            //    }
            //}
        }

        /// <summary>
        /// Set the number of items in the first half
        /// </summary>
        private void SetFirstHalfCountByCurrentList()
        {
            FirstHalfCount = HasScanItems? ScanItems.Count / 2 : 0;
        }

        #endregion

        #region Scanning

        #region General

        /// <summary>
        /// Analyze current reading value and parameters and determine what action to take and return the selection action to the caller
        /// </summary>
        /// <param name="receivedReadingIsYes">Indicates if the received reading is Yes</param>
        /// <param name="lastTestStatus"></param>
        /// <returns></returns>
        public MultiLevelScanAction PerformActionByReading(bool receivedReadingIsYes, AutoTestStatus lastTestStatus)
        {
            //Determine action process based on the scanning type
            if (ChildsScanningTypeEnum == Shared.ChildsScanningType.ChildsScanningTypeElimination)
            {
                //If the reading received is Yes, then we have a choice between Splitting or Marking a result
                if (receivedReadingIsYes)
                {
                    //Reset the bookmark since we received a Yes reading
                    ResetBookmarkMarker(ScanBookmarkType.MultiLevelScanning);

                    if (FirstHalfCount == 1)
                    {
                        //Case 1: In the current case we found a match so we perform the actions for MarkResult

                        //Reset ScanItems collection because we are leaving the current scanning round
                        ScanItems = null;

                        //Call logic to set ScanItems collection again, this will set it to the original StageAutoItems list and restore ScanItems to its starting state
                        SetUpdateScanItems();

                        return MultiLevelScanAction.MarkResult;
                    }
                    else
                    {
                        //Case 2: The list doesn't have a single item in its first half so we can split it
                        SplitScanItems();

                        return MultiLevelScanAction.Split;
                    }
                }
                else
                {
                    //If the reading received is No, then we have a choice between Switching or Skipping

                    //We increase the number of the sequential No readings we received using the bookmark index
                    MoveBookmarkNext(ScanBookmarkType.MultiLevelScanning,lastTestStatus);

                    //Get the bookmark using type
                    var scanBookmark = GetStepScanBookmark(ScanBookmarkType.MultiLevelScanning);

                    if (!scanBookmark.IsLast)
                    {
                        //Case 3: If we didn't reach the limit of 3 allowed Switch operations, then we perform a Switch action.

                        SwitchScanItems();

                        return MultiLevelScanAction.Switch;
                    }
                    else
                    {
                        //Case 4: If we reached the switch limit, then we skip the current scanning round

                        //Reset ScanItems collection because we are leaving the current scanning round
                        ScanItems = null;

                        //Call logic to set ScanItems collection again, this will set it to the original StageAutoItems list and restore ScanItems to its starting state
                        SetUpdateScanItems();

                        return MultiLevelScanAction.Skip;
                    }
                }
            }
            else
            {
                //If the reading received is Yes, then we should mark a result, otherwise we just move next
                return receivedReadingIsYes ? MultiLevelScanAction.MarkResult : MultiLevelScanAction.MoveNext;
            }
        }

        #endregion

        #region Split/Switch

        /// <summary>
        /// Perform a Split action on the current ScanItems collection
        /// </summary>
        public void SplitScanItems()
        {
            //If the ScanItems collection has one item in it then we should do nothing, there is nothing to split here
            if (!HasScanItems || ScanItems.Count == 1) return;

            //Set the ScanItems value to take the first half of what it currently has
            ScanItems = ScanItems.OrderBy(si => si.Order).Take(FirstHalfCount).ToBindingList();

            //Set the number of items in the first half
            SetFirstHalfCountByCurrentList();
        }

        /// <summary>
        /// Perform a Switch action on the current ScanItems collection
        /// </summary>
        public void SwitchScanItems()
        {
            //If the ScanItems collection has one item in it then we should do nothing, there is nothing to switch here
            if (!HasScanItems || ScanItems.Count == 1) return;

            //Store a copy of the SecondHalfCount before switching (Even though it doesn't matter because the switching just updates the order property inside the
            //StageAutoItems but doesn't update the order of the items in the collection itself
            var preSwitchSecondHalfCount = SecondHalfCount;

            //Store an instance of the last index to avoid having to recalculate it each time in the loop
            var lastIndex = ScanItems.Count - 1;

            var orderIndex = 0;

            //Loop over the ScanItems and reverese their Order value to make the Switch, notice here that we are using foreach and not for, this is important because the
            //normal for loops over the collection using the order of the items inside however we want the order using the Order property and because of this notice
            //that below we are ordering the items by Order property before looping and we are using the index to determine the new order. Luckily the loop is not affected
            //by changing the Order property value because the list we are looping is already ordered by the Order property and this order is not affected because it represents
            //the list order and not the Order property itself inside the object, also the object has reference to the original record inside the ScanItems and so when updating
            //its Order property, it gets updated inside the ScanItems collection.
            foreach (var scanItem in ScanItems.OrderBy(si => si.Order))
            {
                scanItem.Order = lastIndex - orderIndex;
                orderIndex += 1;
            }

            //Finalize the switch by switching the FirstHalfCount with the SecondHalfCount value
            FirstHalfCount = preSwitchSecondHalfCount;

            //The SecondHalfCount will be updated dynamically
        }

        #endregion

        #region Multi-Level Scanning

        /// <summary>
        /// Performs a drill down action by adding new StageAutoItem to the end of the collection
        /// </summary>
        /// <param name="newParent"></param>
        public void DrillDown(StageAutoItem newParent)
        {
            //Make sure that the last item in testing path doesn't match the new parent added
            if (TestingPathItems.LastOrDefault() == null || TestingPathItems.LastOrDefault() != newParent)
            {
                newParent.SetAutoTest(CurrentAutoTest);
                TestingPathItems.Add(newParent);
            }
        }

        /// <summary>
        /// Drills back up by removing the last item in the testing path items collection
        /// </summary>
        public void DrillBack()
        {
            if (!IsTestingOnRoot && TestingPathItems.LastOrDefault() != null)
            {
                TestingPathItems.Remove(TestingPathItems.LastOrDefault());
            }
        }

        /// <summary>
        /// Sets the AutoTest reference inside StageAutoItem in case it wasn't set
        /// </summary>
        /// <param name="autoTest"></param>
        public void SetAutoTest(AutoTest autoTest)
        {
            if (CurrentAutoTest == null)
            {
                CurrentAutoTest = autoTest;

                //IMPORTANT:
                //When loading a saved test from DB, we use the logic below to set the result (If any) for the root inside the root StageAutoItem, this can help
                //any manually added results to show up under the current result for the current root instead of showing at the end of the list without a parent.
                if (IsRoot &&
                    AutoItem != null &&
                    CurrentAutoTest.CurrentTestStatus == AutoTestStatus.Ended &&
                    CurrentAutoTest.AutoTestResults != null && CurrentAutoTest.AutoTestResults.Any())
                {
                    //Find the first matching result and link with it if found
                    var matchingResult = CurrentAutoTest.AutoTestResults.FirstOrDefault(result => result.AutoItem != null && result.AutoItem.Id == AutoItem.Id);

                    if (matchingResult != null)
                    {
                        AutoTestResult = matchingResult;
                        matchingResult.StageAutoItem = this;
                    }
                }
            }
        }

        /// <summary>
        /// Indicates if a special scanning check can be skipped
        /// </summary>
        /// <param name="scanCheckKey"></param>
        /// <returns></returns>
        public bool CanSkipCheck(string scanCheckKey)
        {
            //Get the scan check key value as enum
            var scanCheck = EnumNameResolver.StringAsEnumWithUndefined<ScanCheck>(scanCheckKey);

            switch (scanCheck)
            {
                case ScanCheck.Undefined:
                    var directItemScanCheck = GetDirectAccessItemScanState(scanCheckKey);
                    return directItemScanCheck == ScanCheckState.Checked || directItemScanCheck == ScanCheckState.NotNeeded;
                case ScanCheck.Childes:
                    return ChildesCheck == ScanCheckState.Checked || ChildesCheck == ScanCheckState.NotNeeded;
                case ScanCheck.AddAllChildesAsResults:
                    return AddAllChildesAsResultsCheck == ScanCheckState.Checked || AddAllChildesAsResultsCheck == ScanCheckState.NotNeeded;
                default:
                    return true;
            }
        }

        /// <summary>
        /// Sets a certain special scanning check as finished
        /// </summary>
        /// <param name="scanCheckKey"></param>
        public void FinishCheck(string scanCheckKey)
        {
            //Get the scan check key value as enum
            var scanCheck = EnumNameResolver.StringAsEnumWithUndefined<ScanCheck>(scanCheckKey);

            switch (scanCheck)
            {
                case ScanCheck.Undefined:
                    var directAccessCheck = GetDirectAccessScanCheck(scanCheckKey);

                    if (directAccessCheck != null)
                    {
                        directAccessCheck.IsChecked = true;
                    }

                    break;
                case ScanCheck.Childes:
                    ChildesCheck = ScanCheckState.Checked;
                    break;
                case ScanCheck.AddAllChildesAsResults:
                    AddAllChildesAsResultsCheck = ScanCheckState.Checked;
                    break;
            }
        }

        /// <summary>
        /// Gets the scan state of a direct access item within the list of direct access items inside StageAutoItem
        /// </summary>
        /// <param name="directAccessItemKey"></param>
        /// <returns></returns>
        public ScanCheckState GetDirectAccessItemScanState(string directAccessItemKey)
        {
            //Get the directAccessCheck using the DirectAccessItem key
            var directAccessCheck = GetDirectAccessScanCheck(directAccessItemKey);

            //If the direct access scan check is found then return the scan state based on the check value
            if (directAccessCheck != null)
            {
                return directAccessCheck.IsChecked ? ScanCheckState.Checked : ScanCheckState.Pending;
            }

            return ScanCheckState.NotNeeded;
        }

        /// <summary>
        /// Get the matching direct access scan check using key
        /// </summary>
        /// <param name="directAccessItemKey"></param>
        /// <returns></returns>
        public DirectAccessScanCheck GetDirectAccessScanCheck(string directAccessItemKey)
        {
            return DirectAccessScanChecks == null? null : DirectAccessScanChecks.FirstOrDefault(check => check.Key == directAccessItemKey);
        }

        /// <summary>
        /// Sets the DirectAccessChecks string as comma separated list of keys generated from the DirectAccessScanCheck list
        /// </summary>
        private void SetDirectAccessCheckStringBasedOnChecksList()
        {
            if (_directAccessScanChecks != null)
            {
                DirectAccessChecks = string.Join(",", _directAccessScanChecks.Select(directAccessScanCheck => directAccessScanCheck.Key).ToList());
            }
            else
            {
                DirectAccessChecks = null;
            }
        }

        #endregion

        #endregion

        #endregion

        #region Public Events

        /// <summary>
        /// Notifies the change of the state for the TestingPoint.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestingPoint_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => TestingPoint), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => TestingPoint, () => TestingPoint.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the ScanningMethod.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanningMethod_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ScanningMethod), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => ScanningMethod, () => ScanningMethod.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the ChildsOrderType.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildsOrderType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ChildsOrderType), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => ChildsOrderType, () => ChildsOrderType.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the ChildsScanningType.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildsScanningType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ChildsScanningType), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => ChildsScanningType, () => ChildsScanningType.Id));
        }

        /// <summary>
        /// Handle DirectAccessItems list changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void directAccessScanChecks_ListChanged(object sender, ListChangedEventArgs e)
        {
            //The logic here allows keeping the DirectAccessChecks updated based on the items inside the DirectAccessScanChecks without having to manually
            //update the list when adding/removing items because the event here will take care of this on its own.
            SetDirectAccessCheckStringBasedOnChecksList();
        }

        #endregion

        #region Validation

        /// <summary>
        /// Gets the validation errors according to the below cases.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="info">The error info</param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            if (propertyName == ExpressionHelper.GetPropertyName(() => TestingPoint))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => ScanningMethod) && !(ScanningMethod == null || ScanningMethod.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => ChildsOrderType) && !(ChildsOrderType == null || ChildsOrderType.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => ChildsScanningType) && !(ChildsScanningType == null || ChildsScanningType.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Order))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => ScansNumber))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => MatchesNumber))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => FinishAllScanRounds))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => DirectAccessChecks))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => StageAutoItems) && !ValidateStageAutoItems())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            UpdateErrorsSummary(info);
        }

        /// <summary>
        /// Validates the StageAutoItem.
        /// </summary>
        /// <returns></returns>
        public bool ValidateStageAutoItems()
        {
            return StageAutoItems.All(stageAutoItems => stageAutoItems.Validate());
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
            ValidationErrors.Clear();

            foreach (PropertyInfo property in GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }

            return IsValid;
        }
        #endregion
    }
}