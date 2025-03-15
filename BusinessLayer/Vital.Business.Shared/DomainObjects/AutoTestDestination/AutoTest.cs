using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.AutoTestSource;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestDestination
{
    public class AutoTest : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private Patient _patient;
        private AutoProtocolRevision _autoProtocolRevision;
        private string _name;
        private string _description;
        private string _notes;
        private DateTime _testDate;
        private BindingList<AutoTestResult> _autoTestResults;
        private BindingList<AutoTestResult> _readings;
        private BindingList<AutoTestResult> _testingResults;
        private BindingList<AutoTestResultProduct> _products;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Patient.
        /// </summary>
        public Patient Patient
        {
            get { return _patient; }
            set
            {
                if (_patient == null || !_patient.Equals(value))
                {
                    _patient = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the AutoProtocolRevision.
        /// </summary>
        public AutoProtocolRevision AutoProtocolRevision
        {
            get { return _autoProtocolRevision; }
            set
            {
                if (_autoProtocolRevision == null || !_autoProtocolRevision.Equals(value))
                {
                    _autoProtocolRevision = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == null || !_name.Equals(value))
                {
                    _name = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == null || !_description.Equals(value))
                {
                    _description = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Notes.
        /// </summary>
        public string Notes
        {
            get { return _notes; }
            set
            {
                if (_notes == null || !_notes.Equals(value))
                {
                    _notes = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the TestDate.
        /// </summary>
        public DateTime TestDate
        {
            get { return _testDate; }
            set
            {
                if (!_testDate.Equals(value))
                {
                    _testDate = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the AutoTestResults.
        /// </summary>
        public BindingList<AutoTestResult> AutoTestResults
        {
            get { return _autoTestResults; }
            set
            {
                if (_autoTestResults != null && _autoTestResults == value) return;

                _autoTestResults = value;
                _autoTestResults.RaiseListChangedEvents = true;
                _autoTestResults.ListChanged += AutoTestResults_ListChanged;
            }
        }

        #region Memory Only

        /// <summary>
        /// List of auto test results that are readings
        /// </summary>
        public BindingList<AutoTestResult> Readings
        {
            get
            {
                if (_readings == null)
                {
                    _readings = AutoTestResults == null ? null :
                               AutoTestResults.Where(result => result.AutoItem != null &&
                                                               result.AutoItem.Type != null &&
                                                               result.AutoItem.TypeEnum == AutoItemType.AutoItemTypePoint).ToBindingList();
                }

                return _readings;
            }
        }

        /// <summary>
        /// List of auto test results that are products
        /// </summary>
        public BindingList<AutoTestResultProduct> Products
        {
            get
            {
                if (_products == null)
                {
                    UpdateProducts();
                }

                return _products;
            }
        }

        /// <summary>
        /// List of products that are checked
        /// </summary>
        public BindingList<AutoTestResultProduct> CheckedProducts
        {
            get
            {
                return Products.Where(product => product.IsChecked).ToBindingList();
            }
        }

        /// <summary>
        /// Returns a list of results excluding the readings
        /// </summary>
        public BindingList<AutoTestResult> TestingResults
        {
            get
            {
                if (_testingResults == null)
                {
                    _testingResults = _testingResults = AutoTestResults == null ? null :
                                        AutoTestResults.Where(result => result.AutoItem != null &&
                                              result.AutoItem.Type != null &&
                                              result.AutoItem.TypeEnum != AutoItemType.AutoItemTypePoint).ToBindingList();
                }

                return _testingResults;
            }
        }

        #endregion

        #endregion

        #region Binding Properties

        /// <summary>
        /// Gets the auto protocol revision name.
        /// </summary>
        public string AutoProtocolRevisionName
        {
            get
            {
                return AutoProtocolRevision == null ? null : AutoProtocolRevision.Name;
            }
        }

        #endregion

        #region Scanning Memory Only Properties

        #region Scanning Variables

        private ScanBookmark _stageBookmark;
        private ScanBookmark _userStageBookmark;

        private ScanBookmark _stageItemBookmark;
        private ScanBookmark _userStageItemBookmark;

        /// <summary>
        /// The elimination rounds limit value
        /// </summary>
        public int MutliLevelScanningEliminationRounds { get; set; }

        #endregion

        #region Scanning Flags

        /// <summary>
        /// AutoTest Current Status
        /// </summary>
        public AutoTestStatus CurrentTestStatus { get; set; }

        /// <summary>
        /// AutoTest Last Status
        /// </summary>
        public AutoTestStatus LastTestStatus { get; set; }

        /// <summary>
        /// The number of items to scan
        /// </summary>
        public int ScanItemsCount { get; set; }

        /// <summary>
        /// The number of items scanned already
        /// </summary>
        public int ScannedItemsCount { get; set; }

        /// <summary>
        /// Percentage of scanned items
        /// </summary>
        public int ScanProgressPercentage
        {
            get
            {
                return ScanItemsCount == 0 ? 0 : (int)Math.Round((double)(100 * ScannedItemsCount) / ScanItemsCount);
            }
        }

        /// <summary>
        /// The number of the current scan action on a certain "StageAutoItem" under current stage
        /// </summary>
        public int CurrentScanTrialNumber { get; set; }

        /// <summary>
        /// List of the last recevied reading
        /// </summary>
        public List<ReadingRecord> ReadingRecords { get; set; }

        /// <summary>
        /// Indicates if the hardware is valid for scanning
        /// </summary>
        public bool HardwareValidForScanning
        {
            get
            {
                return CurrentCsaState == CSAState.Connected;
            }
        }

        /// <summary>
        /// Indicates if at least one hardware is disconnected
        /// </summary>
        public bool AHardwareIsDisconnected
        {
            get
            {
                return CurrentCsaState == CSAState.Disconnected;
            }
        }

        /// <summary>
        /// The current connection state of the CSA
        /// </summary>
        public CSAState CurrentCsaState { get; set; }

        /// <summary>
        /// The last connection state of the CSA
        /// </summary>
        public CSAState LastCsaState { get; set; }

        /// <summary>
        /// The current reason of connection issue in CSA
        /// </summary>
        public string CurrentCsaReason { get; set; }

        /// <summary>
        /// Bookmark to identify current stage
        /// </summary>
        public ScanBookmark StageBookmark
        {
            get
            {
                return CurrentTestStatus == AutoTestStatus.InProgress ? _stageBookmark : UserStageBookmark;
            }
            set
            {
                if (CurrentTestStatus == AutoTestStatus.InProgress)
                {
                    _stageBookmark = value;
                }
                else
                {
                    UserStageBookmark = value;
                }
            }
        }

        /// <summary>
        /// Bookmark to identify current stage based on user selection
        /// </summary>
        public ScanBookmark UserStageBookmark
        {
            get
            {
                return _userStageBookmark;
            }
            set
            {
                _userStageBookmark = value;
            }
        }

        /// <summary>
        /// Bookmark to identify current stage auto item
        /// </summary>
        public ScanBookmark StageItemBookmark
        {
            get
            {
                return CurrentTestStatus == AutoTestStatus.InProgress ? _stageItemBookmark : UserStageItemBookmark;
            }
            set
            {
                if (CurrentTestStatus == AutoTestStatus.InProgress)
                {
                    _stageItemBookmark = value;
                }
                else
                {
                    UserStageItemBookmark = value;
                }
            }
        }

        /// <summary>
        /// Bookmark to identify current stage auto item based on user selection
        /// </summary>
        public ScanBookmark UserStageItemBookmark
        {
            get
            {
                return _userStageItemBookmark;
            }
            set
            {
                _userStageItemBookmark = value;
            }
        }

        #endregion

        #region Scanning Data

        /// <summary>
        /// List of stage revisions ordered
        /// </summary>
        public List<AutoProtocolStageRevision> StageRevisions
        {
            get
            {
                return AutoProtocolRevision.AutoProtocolStageRevisions.OrderBy(sr => sr.Order).ToList();
            }
        }

        /// <summary>
        /// Reference to the current stage revision based on stage bookmark index
        /// </summary>
        public AutoProtocolStageRevision CurrentStageRevision
        {
            get
            {
                return StageBookmark == null || StageBookmark.IsEmpty ? null : StageRevisions[StageBookmark.IndexValue];
            }
        }

        /// <summary>
        /// Reference to the current stage based on stage bookmark index
        /// </summary>
        public AutoTestStage CurrentStage
        {
            get
            {
                return CurrentStageRevision == null? null : CurrentStageRevision.AutoTestStage;
            }
        }

        /// <summary>
        /// Reference to the current stage auto item based on stage auto item bookmark index
        /// </summary>
        public StageAutoItem CurrentRootStageAutoItem
        {
            get
            {
                return  StageItemBookmark == null ||
                        StageItemBookmark.IsEmpty ||
                        CurrentStageRevision == null ||
                        CurrentStageRevision.AutoProtocolStage == null ||
                        !CurrentStageRevision.AutoProtocolStage.StageAutoItems.Any()? null :
                        CurrentStageRevision.AutoProtocolStage.StageAutoItems.OrderBy(sr => sr.Order).ToList()[StageItemBookmark.IndexValue];
            }
        }

        /// <summary>
        /// Gets the TestingPathParent of the CurrentRootStageAutoItem
        /// </summary>
        public StageAutoItem CurrentTestingPathParent
        {
            get
            {
                return CurrentRootStageAutoItem == null ? null : CurrentRootStageAutoItem.TestingPathParent;
            }
        }

        /// <summary>
        /// Gets the TestingPathParentWithResult of the CurrentRootStageAutoItem
        /// </summary>
        public StageAutoItem CurrentTestingPathParentWithResult
        {
            get
            {
                return CurrentRootStageAutoItem == null ? null : CurrentRootStageAutoItem.TestingPathParentWithResult;
            }
        }

        /// <summary>
        /// Return the current test result selected based on index for stages that depen on results
        /// </summary>
        public AutoTestResult CurrentTestResult
        {
            get
            {
                if (StageBookmark.IsEmpty || StageItemBookmark.IsEmpty) return null;

                switch (StageKeyEnum)
                {
                    case StageKey.Preliminary:
                    case StageKey.Summary:
                        return Readings[StageItemBookmark.IndexValue];
                    case StageKey.Dosage:
                        var resultProduct =  Products[StageItemBookmark.IndexValue];
                        return resultProduct == null ? null : resultProduct.AutoTestResult;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the number of stages available
        /// </summary>
        public int StageBookmarkLimit
        {
            get
            {
                return StageRevisions.Count();
            }
        }

        /// <summary>
        /// Gets the number of current stage items available
        /// </summary>
        public int StageItemBookmarkLimit
        {
            get
            {
                return CurrentStageRevision.AutoProtocolStage.StageAutoItems.Count();
            }
        }

        /// <summary>
        /// Gets the current item sub item bookmark limit
        /// </summary>
        public int ItemSubItemBookmarkLimit
        {
            get
            {
                if (StageBookmark.IsEmpty || StageItemBookmark.IsEmpty) return 0;

                switch (StageKeyEnum)
                {
                    case StageKey.MajorIssues:
                    case StageKey.Testing:
                        return CurrentRootStageAutoItem.ScansNumber;
                    case StageKey.Dosage:
                        return CurrentRootStageAutoItem.DosageOptions.Count;
                }

                return 0;
            }
        }

        /// <summary>
        /// Enum of the current stage tab key based on current stage
        /// </summary>
        public StageTabKey StageTabKeyEnum
        {
            get
            {
                return StageBookmark.IsEmpty? StageTabKey.Undefined : EnumNameResolver.StringAsEnum<StageTabKey>(CurrentStage.StageTabKey);
            }
        }

        /// <summary>
        /// Enum of the current stage key based on current stage
        /// </summary>
        public StageKey StageKeyEnum
        {
            get
            {
                return StageBookmark.IsEmpty ? StageKey.Undefined : EnumNameResolver.StringAsEnum<StageKey>(CurrentStage.Key);
            }
        }

        #endregion

        #region Simulated Readings

        /// <summary>
        /// Indicates if the current reading value is representing a Yes reading
        /// </summary>
        public bool IsCurrentReadingYes
        {
            get
            {
                return CurrentReadingValue >= 48 && CurrentReadingValue <= 52;
            }
        }

        /// <summary>
        /// Current temporary simulated reading value 
        /// </summary>
        public int CurrentReadingValue { get; set; }

        /// <summary>
        /// Indicates if the current simulated reading value is stable to be considered
        /// </summary>
        public bool IsReadingStable { get; set; }

        /// <summary>
        /// Indicates if the AutoTest uses readings only when they are stable
        /// </summary>
        public bool UseStableReadingsOnly { get; set; }

        /// <summary>
        /// Indicates if the AutoTest uses simulated readings instead of real readings
        /// </summary>
        public bool UseSimulatedReadings { get; set; }

        /// <summary>
        /// Simulates balanced readings only during summary phase
        /// </summary>
        public bool SimulateBalancedSummary { get; set; }

        /// <summary>
        /// The maximum reading value within current reading session
        /// </summary>
        public int CurrentSessionMax { get; set; }

        /// <summary>
        /// The minimum reading value within current reading session
        /// </summary>
        public int CurrentSessionMin { get; set; }

        /// <summary>
        /// The target reading value within current reading session
        /// </summary>
        public int CurrentSessionTarget { get; set; }

        /// <summary>
        /// Indicates if a reading session is in progress
        /// </summary>
        public bool SessionInProgress { get; set; }

        /// <summary>
        /// Indicates if a reading stabilization is in progress
        /// </summary>
        public bool IsStabilizing { get; set; }

        /// <summary>
        /// Counter for stabilized readings 
        /// </summary>
        public int StabilizationCounter { get; set; }

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AutoTest()
        {
            //Initialize the AutoTest status before setting it
            CurrentTestStatus = AutoTestStatus.Undefined;
            LastTestStatus = AutoTestStatus.Undefined;

            //List of readings received from the CSA
            ReadingRecords = new List<ReadingRecord>();

            //CSA state flags
            CurrentCsaState = CSAState.Disconnected;
            LastCsaState = CSAState.Disconnected;

            //Scanning bookmarks for stage, stage item and item sub-item
            _stageBookmark = new ScanBookmark {Type = ScanBookmarkType.Stage, Source = ScanBookmarkSource.Automation};
            _userStageBookmark = new ScanBookmark { Type = ScanBookmarkType.Stage, Source = ScanBookmarkSource.User };

            _stageItemBookmark = new ScanBookmark { Type = ScanBookmarkType.StageItem, Source = ScanBookmarkSource.Automation };
            _userStageItemBookmark = new ScanBookmark { Type = ScanBookmarkType.StageItem, Source = ScanBookmarkSource.User };
        }

        #endregion

        #region Logic

        #region General

        /// <summary>
        /// Set the initial TestStatus based on type of record new or loaded from DB
        /// </summary>
        public void SetTestStatus()
        {
            if (IsNew)
            {
                CurrentTestStatus = AutoTestStatus.Pending;
                LastTestStatus = CurrentTestStatus;
            }
            else
            {
                CurrentTestStatus = AutoTestStatus.Ended;
                LastTestStatus = CurrentTestStatus;
            }
        }

        /// <summary>
        /// Gets stage revision by key
        /// </summary>
        /// <param name="stageKey"></param>
        /// <returns></returns>
        public AutoProtocolStageRevision GetStageRevisionByKey(StageKey stageKey)
        {
            return StageRevisions.FirstOrDefault(protocolStageRevision => protocolStageRevision.AutoTestStage.StageKeyEnum == stageKey);
        }

        /// <summary>
        /// Checks if the test contains a certain stage by key
        /// </summary>
        /// <param name="stageKey"></param>
        /// <returns></returns>
        public bool TestHasStage(StageKey stageKey)
        {
            return StageRevisions.Any(protocolStageRevision => protocolStageRevision.AutoTestStage.StageKeyEnum == stageKey);
        }

        /// <summary>
        /// Indicates if the current stage has items to scan
        /// </summary>
        public bool CurrentStageHasItems
        {
            get
            {
                return StageItemBookmarkLimit > 0;
            }
        }

        /// <summary>
        /// This logic moves a ScanBookmark to its last index to prevent further scanning activities on it, the bookmark is determined by type
        /// which is sent by the caller
        /// </summary>
        /// <param name="type"></param>
        public void MoveScanBookmarkToLast(ScanBookmarkType type)
        {
            switch (type)
            {
                case ScanBookmarkType.Stage:
                    SetStageBookmarkIndex(StageBookmarkLimit - 1);
                    break;
                case ScanBookmarkType.StageItem:
                    SetStageItemBookmarkIndex(StageItemBookmarkLimit - 1);
                    break;
                default:
                    //Move a ScanBookmark inside collection to its last possible index based on its type
                    CurrentRootStageAutoItem.TestingPathParent.MoveScanBookmarkToLast(type);
                    break;
            }
        }

        /// <summary>
        /// Gets the current scan info as a combination of Stage + StageItem + TestingPathItems + Step
        /// </summary>
        /// <param name="isForBreadcrumb"></param>
        /// <returns></returns>
        public string GetCurrentScanInfo(bool isForBreadcrumb)
        {
            //Determine the separator to use based on usage case
            var separator = isForBreadcrumb ? " > " : " - ";

            var currentScanInfo = string.Empty;

            if (StageBookmark.IsEmpty)
            {
                currentScanInfo = string.Empty;
            }
            else
            {
                //Start with stage name, it should already be set inside the bookmark
                currentScanInfo += StageBookmark.Name;

                if (!StageItemBookmark.IsEmpty)
                {
                    //currentScanInfo += separator + StageItemBookmark.Name;

                    //Add any further terms only if the test is in progress
                    if (CurrentTestStatus == AutoTestStatus.InProgress)
                    {
                        //Specify the exact details to add based on stage type
                        switch (StageKeyEnum)
                        {
                            case StageKey.MajorIssues:
                            case StageKey.Testing:
                                //Include CurrentRootStageAutoItem info only in case of multi-level scanning since it causes an error
                                //if called in case of dosage for example where things are handled in different way
                                if (CurrentRootStageAutoItem != null)
                                {
                                    //Add the names of all the items in the current testing path, starting with the root and ending with the current parent
                                    foreach (var testingPathItem in CurrentRootStageAutoItem.TestingPathItems)
                                    {
                                        currentScanInfo += separator + testingPathItem.AutoItem.Name;
                                    }
                                }

                                //First get the scanning round
                                var scanningRoundBookmark = GetStepScanBookmark(ScanBookmarkType.ScanningRounds);

                                if (scanningRoundBookmark != null && !scanningRoundBookmark.IsEmpty)
                                {
                                    currentScanInfo += separator + scanningRoundBookmark.Name;

                                    //Second, get the step getting performed, this will return scan item name in sequential mode or static term in elimination mode
                                    var multiLevelScanningBookmark = GetStepScanBookmark(ScanBookmarkType.MultiLevelScanning);

                                    if (multiLevelScanningBookmark != null && !multiLevelScanningBookmark.IsEmpty)
                                    {
                                        currentScanInfo += separator + multiLevelScanningBookmark.Name;
                                    }
                                }

                                break;
                            case StageKey.Dosage:
                                //In the case of dosage we use the bookmark name which should include the dosage option title
                                var dosageOptionBookmark = GetStepScanBookmark(ScanBookmarkType.DosageOptions);

                                if (dosageOptionBookmark != null && !dosageOptionBookmark.IsEmpty)
                                {
                                    currentScanInfo += separator + dosageOptionBookmark.Name;
                                }
                                break;
                        }
                    }
                }
            }

            return currentScanInfo;
        }

        #endregion

        #region Stage Bookmark

        /// <summary>
        /// Checks the testing state & index to determine the next stage to be in progress
        /// </summary>
        public void MoveStageBookmarkNext()
        {
            //Flag used to determine if the current stage bookmark will be updated or not based on checks
            var setCurrentBookmakrDetails = false;

            //If the last status was pending, then get the first stage key by default
            if (LastTestStatus == AutoTestStatus.Pending)
            {
                //Set stage starting index, we always start at zero.
                StageBookmark.Index = 0;

                //Since the index was updated, we set the flag to update the stage to true
                setCurrentBookmakrDetails = true;
            }
            //IMPORTANT:
            //If the last status was paused, then if the stage bookmark is empty, then we set it to 0, if not then we don't adjust it and we just continue
            //without updating the stage bookmark details so the testing continues from where it stopped.
            //The last status can be "Paused" in the special case where the start/stop button is sequentially and quickly clicked to resume/pause testing 
            //where the last status doesn't get a chance to be set as InProgress even though the scanning resumed and this is because we set it to InProgress
            //only when getting to the level of scanning StageAutoItem, we don't do this before because it would screw up this logic here and so we need
            //to keep the last status as it was and only change its value when getting to StageAutoItem level however this will cause us issues unless
            //we handle the Paused state, handling it is fairly simple, if the test was previously pending and didn't start yet, then we just set the index to 0
            //but if the test has already progressed beyond that, then we leave the index where it was and we don't change it.
            else if (LastTestStatus == AutoTestStatus.Paused)
            {
                //Only do this if the bookmark is empty
                if (StageBookmark.IsEmpty)
                {
                    //Set stage starting index, we always start at zero.
                    StageBookmark.Index = 0;

                    //Since the index was updated, we set the flag to update the stage to true
                    setCurrentBookmakrDetails = true;
                }
                //Important: if the bookmark wasn't empty then don't edit it, let the test resume from where it stopped.
            }
            //If the last status was in progress then get next stage key if we are moving to next stage or finish if there are no stages left to scan.
            else if (LastTestStatus == AutoTestStatus.InProgress && !StageBookmark.IsEmpty)
            {
                //We only increase the index if the list has more items to scan, notice at this point the index is still referring to the last value, it hasn't increased yet.
                if (StageBookmark.NextOrCurrentIndexValue < StageBookmarkLimit)
                {
                    StageBookmark.Index += 1;

                    //Since the index was updated, we set the flag to update the stage to true
                    setCurrentBookmakrDetails = true;
                }
                //IMPORTANT: If there are no more items to scan, the scanning should be stopped, this will be handled in the scanning logic itself because this method won't be called
            }

            //Always update stage bookmark to match the current automation bookmark
            if (StageBookmark.Source == ScanBookmarkSource.Automation)
            {
                UserStageBookmark.Index = StageBookmark.Index;
            }

            //If the flag is true then update the bookmark fields
            if (setCurrentBookmakrDetails)
            {
                UpdateStageMarkerByIndex();

                //IMPORTANT: Reset stage item bookmark whenever the stage index is updated to make sure the stage item marker starts from the beggining of the stage
                ResetStageItemMarker();
            }
        }

        /// <summary>
        /// Updates the properties of the stage marker to match the details of current stage
        /// </summary>
        private void UpdateStageMarkerByIndex()
        {
            StageBookmark.Name = CurrentStage.Name;
            StageBookmark.Key = CurrentStage.Key;
            StageBookmark.UIKey = CurrentStage.StageTabKey;
            StageBookmark.IsMultiLevel = CurrentStage.IsMultiLevel;
            StageBookmark.IsLast = StageBookmark.NextOrCurrentIndexValue >= StageBookmarkLimit; //Here index is current

            //Always update user stage bookmark to match the current automation bookmark
            if (StageBookmark.Source == ScanBookmarkSource.Automation)
            {
                UserStageBookmark.Name = CurrentStage.Name;
                UserStageBookmark.Key = CurrentStage.Key;
                UserStageBookmark.UIKey = CurrentStage.StageTabKey;
                UserStageBookmark.IsMultiLevel = CurrentStage.IsMultiLevel;
                UserStageBookmark.IsLast = StageBookmark.NextOrCurrentIndexValue >= StageBookmarkLimit;//Here index is current
            }
        }

        /// <summary>
        /// Sets the stage bookmark index based on a specific value, this is mainly for user selection in UI
        /// </summary>
        /// <param name="stageIndex"></param>
        public void SetStageBookmarkIndex(int stageIndex)
        {
            StageBookmark.Index = stageIndex;
            UpdateStageMarkerByIndex();

            //IMPORTANT: Reset stage item bookmark whenever the stage index is updated to make sure the stage item marker starts from the beggining of the stage
            ResetStageItemMarker();
        }

        #endregion

        #region Stage Item Bookmark

        /// <summary>
        /// Moves the index of the stage item bookmark
        /// </summary>
        public void MoveStageItemBookmarkNext()
        {
            //Flag used to determine if the current stage bookmark will be updated or not based on checks
            var setCurrentBookmakrDetails = false;

            //If the current index is empty, then get the first stage item key
            if (StageItemBookmark.IsEmpty)
            {
                //Set stage item starting index
                StageItemBookmark.Index = 0;

                //Since the index was updated, we set the flag to update the stage to true
                setCurrentBookmakrDetails = true;
            }
            //If the last status was in progress then get next stage key or finish if there are no stages left
            else if (LastTestStatus == AutoTestStatus.InProgress)
            {
                //We only increase the index if the list has more items to scan, notice at this point the index is still referring to the last value, it hasn't increased yet.
                if (StageItemBookmark.NextOrCurrentIndexValue < StageItemBookmarkLimit)
                {
                    StageItemBookmark.Index += 1;

                    //Since the index was updated, we set the flag to update the stage to true
                    setCurrentBookmakrDetails = true;
                }

                //IMPORTANT: If there are no more items to scan, the scanning should be stopped, this will be handled in the scanning logic itself because this method won't be called
            }

            //Always update stage item bookmark to match the current automation bookmark
            if (StageItemBookmark.Source == ScanBookmarkSource.Automation)
            {
                UserStageItemBookmark.Index = StageItemBookmark.Index;
            }

            //If the flag is true then update the stage bookmark fields
            if (setCurrentBookmakrDetails)
            {
                UpdateStageItemMarkerByIndex();
            }
        }

        /// <summary>
        /// Updates the properties of the stage item marker to match the details of current stage item
        /// </summary>
        private void ResetStageItemMarker()
        {
            StageItemBookmark.Index = null;
            StageItemBookmark.Name = string.Empty;
            StageItemBookmark.Key = string.Empty;
            StageItemBookmark.UIKey = string.Empty;
            StageItemBookmark.IsMultiLevel = false;
            StageItemBookmark.IsLast = false;

            //Always update user stage item bookmark to match the current automation bookmark
            if (StageItemBookmark.Source == ScanBookmarkSource.Automation)
            {
                UserStageItemBookmark.Index = null;
                UserStageItemBookmark.Name = string.Empty;
                UserStageItemBookmark.Key = string.Empty;
                UserStageItemBookmark.UIKey = string.Empty;
                UserStageItemBookmark.IsMultiLevel = false;
                UserStageItemBookmark.IsLast = false;
            }
        }

        /// <summary>
        /// Updates the properties of the stage item marker to match the details of current stage item
        /// </summary>
        private void UpdateStageItemMarkerByIndex()
        {
            StageItemBookmark.Name = CurrentRootStageAutoItem == null? string.Empty : CurrentRootStageAutoItem.AutoItem.Name;
            StageItemBookmark.Key = CurrentRootStageAutoItem == null ? string.Empty : CurrentRootStageAutoItem.AutoItem.Key;
            StageItemBookmark.UIKey = CurrentRootStageAutoItem == null ? string.Empty : CurrentRootStageAutoItem.AutoItem.Key;
            StageItemBookmark.IsMultiLevel = CurrentRootStageAutoItem != null && CurrentRootStageAutoItem.StageAutoItems != null && CurrentRootStageAutoItem.StageAutoItems.Any();
            StageItemBookmark.IsLast = StageItemBookmark.NextOrCurrentIndexValue >= StageItemBookmarkLimit;//Here index is current

            //Always update user stage item bookmark to match the current automation bookmark
            if (StageItemBookmark.Source == ScanBookmarkSource.Automation)
            {
                UserStageItemBookmark.Name = CurrentRootStageAutoItem == null ? string.Empty : CurrentRootStageAutoItem.AutoItem.Name;
                UserStageItemBookmark.Key = CurrentRootStageAutoItem == null ? string.Empty : CurrentRootStageAutoItem.AutoItem.Key;
                UserStageItemBookmark.UIKey = CurrentRootStageAutoItem == null ? string.Empty : CurrentRootStageAutoItem.AutoItem.Key;
                UserStageItemBookmark.IsMultiLevel = CurrentRootStageAutoItem != null && CurrentRootStageAutoItem.StageAutoItems != null && CurrentRootStageAutoItem.StageAutoItems.Any();
                UserStageItemBookmark.IsLast = StageItemBookmark.NextOrCurrentIndexValue >= StageItemBookmarkLimit;//Here index is current
            }
        }

        /// <summary>
        /// Sets the stage item bookmark index based on a specific value, this is mainly for user selection in UI
        /// </summary>
        /// <param name="stageItemIndex"></param>
        public void SetStageItemBookmarkIndex(int stageItemIndex)
        {
            StageItemBookmark.Index = stageItemIndex;
            UpdateStageItemMarkerByIndex();
        }

        #endregion

        #region Step Bookmarks

        /// <summary>
        /// Find the scan bookmark by type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ScanBookmark GetStepScanBookmark(ScanBookmarkType type)
        {
            return CurrentRootStageAutoItem.TestingPathParent.GetStepScanBookmark(type);
        }

        /// <summary>
        /// Reference to the current scan item based on bookmark index
        /// </summary>
        public StageAutoItem GetCurrentScanItem(ScanBookmarkType type)
        {
            return CurrentRootStageAutoItem.TestingPathParent.GetCurrentScanItem(type);
        }

        /// <summary>
        /// Moves the index of the bookmark
        /// </summary>
        public void MoveStepBookmarkNext(ScanBookmarkType type)
        {
            CurrentRootStageAutoItem.TestingPathParent.MoveBookmarkNext(type, LastTestStatus);
        }

        /// <summary>
        /// Resets the details of a ScanBookmark based on its type
        /// </summary>
        public void ResetStepBookmarkMarker(ScanBookmarkType type)
        {
            CurrentRootStageAutoItem.TestingPathParent.ResetBookmarkMarker(type);
        }

        /// <summary>
        /// Resets the details of a ScanBookmark based on its index within collection
        /// </summary>
        public void ResetStepBookmarkMarker(int indexInCollection, bool resetFollowingBookmarks = false)
        {
            CurrentRootStageAutoItem.ResetBookmarkMarker(indexInCollection, resetFollowingBookmarks);
        }

        /// <summary>
        /// Updates the properties of the marker to match the details of current bookmark step
        /// </summary>
        public void UpdateStepBookmarkMarkerByIndex(ScanBookmarkType type)
        {
            CurrentRootStageAutoItem.UpdateBookmarkMarkerByIndex(type);
        }

        /// <summary>
        /// Sets the step bookmark index based on a specific value
        /// </summary>
        /// <param name="index"></param>
        /// <param name="type"></param>
        public void SetStepBookmarkIndex(int index, ScanBookmarkType type)
        {
            CurrentRootStageAutoItem.SetBookmarkIndex(index, type);
        }

        #endregion

        #region Products

        /// <summary>
        /// Updates the product list based on added results
        /// </summary>
        public void UpdateProducts()
        {
            _products = AutoTestResults == null ? null :
                       AutoTestResults.Where(result => result.AutoItem != null &&
                                                       result.AutoItem.Type != null &&
                                                       result.AutoItem.TypeEnum == AutoItemType.AutoItemTypeProduct)
                                                       .SelectMany(result => result.AutoTestResultProducts).ToBindingList();

            if (_products != null)
            {
                _products.ListChanged += Products_ListChanged;
            }
        }

        #endregion

        #endregion

        #region Public Events

        /// <summary>
        /// Notifies the change of the AutoTestResults.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoTestResults_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => AutoTestResults), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => AutoTestResults));
        }

        /// <summary>
        /// Notifies the change of the Products.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Products_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => Products), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => Products));
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => Name))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Description))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Notes))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => AutoTestResults) && !ValidateAutoTestResults())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            UpdateErrorsSummary(info);
        }

        /// <summary>
        /// Validates the AutoTestResult.
        /// </summary>
        /// <returns></returns>
        public bool ValidateAutoTestResults()
        {
            return AutoTestResults.All(autoTestResults => autoTestResults.Validate());
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

    public class DirectAccessScanCheck
    {
        /// <summary>
        /// Key of the direct access item
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Indicates if the direct access item was checked (Scanned)
        /// </summary>
        public bool IsChecked { get; set; }
    }

    public class ReadingRecord
    {
        /// <summary>
        /// The value of the reading
        /// </summary>
        public int ReadingValue { get; set; }

        /// <summary>
        /// The datetime at which the reading was received
        /// </summary>
        public DateTime ReadingDateTime { get; set; }
    }

    public class ScanBookmark
    {
        /// <summary>
        /// The type of the scan bookmark
        /// </summary>
        public ScanBookmarkType Type { get; set; }

        /// <summary>
        /// The source of the scan bookmark
        /// </summary>
        public ScanBookmarkSource Source { get; set; }

        /// <summary>
        /// The name of the scan bookmark item (Stage or AutoItem)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The key of the scan bookmark item (Stage or AutoItem)
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The key of the scan bookmark which matches a key of a UI component to allow matching it dynamically
        /// </summary>
        public string UIKey { get; set; }

        /// <summary>
        /// Indicates if the current bookmark is multilevel of not, currently this applies only to Stages but moving forward it can be used for other bookmarks
        /// </summary>
        public bool IsMultiLevel { get; set; }

        /// <summary>
        /// The index of the scan bookmark item within collection of items/steps to scan
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// The index value of the scan bookmark item within collection of items/steps to scan
        /// </summary>
        public int IndexValue
        {
            get
            {
                return Index.HasValue ? Index.Value : -1;
            }
        }

        /// <summary>
        /// The value of the next or current index depending on where it is called
        /// </summary>
        public int NextOrCurrentIndexValue
        {
            get
            {
                return Index.HasValue ? Index.Value + 1 : -1;
            }
        }

        /// <summary>
        /// Indicates if this bookmark is pointing at last item in list
        /// </summary>
        public bool IsLast { get; set; }

        /// <summary>
        /// Indicates if the bookmark is not referring to an item or index yet
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return !Index.HasValue;
            }

        }
    }
}