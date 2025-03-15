using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.AutoTestDestination;
using Vital.Business.Shared.DomainObjects.FrequencyTests;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.DomainObjects.ShippingOrders;
using Vital.Business.Shared.DomainObjects.SpotChecks;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.DomainObjects.VitalForceSheet;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;
using Vital.UI.UI_Components.Forms;
using Vital.UI.UI_Components.UI_Classes;
using Vital.UI.UI_Components.User_Controls.Modules;

namespace Vital.UI.UI_Components.User_Controls.Entity_Tabs
{
    public partial class XtraTabPagePatient : XtraTabPageEntity
    {
        #region Fields

        #region Managers

        private TestsManager _testManager;
        private SpotCheckManager _spotCheckManager;
        private LookupsManager _lookupsManager;
        private PatientsManager _patientManager;
        private SettingsManager _settingsManager;
        private ShippingOrdersManager _shippingOrdersManager;
        private VitalForceSheetManager _vfsManager;
        private FrequencyTestsManager _frequencyTestsManager;
        private AutoTestSourceManager _autoTestSourceManager;
        private AutoTestDestinationManager _autoTestDestinationManager;

        #endregion

        #region Others

        private XtraUserControlVitalRichEdit _xtraUserControlVitalRichEditMain;
        readonly List<PatientHistory> _deletedObjects;
        private GridCheckMarksSelection _selectionTests;
        private int _itemTypeProductId;
        private static int _nonelistTypeLookupId;
        private static int _potencyTypeLookupId;
        private static int _yesLookupId;
        private bool _isReadOnly;
        private bool _automatedTestScreenEnabled;

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Return the tab object as a building after casting its value
        /// </summary>
        public Patient TabPatient
        {
            get { return TabObject as Patient; }
        }

        /// <summary>
        /// Gets selected tests in the tests grid
        /// </summary>
        internal GridCheckMarksSelection SelectionTests
        {
            get
            {
                return _selectionTests;
            }
        }
        
        /// <summary>
        /// Gets selected spot checks in the spot checks grid
        /// </summary>
        internal GridCheckMarksSelection SelectionSpotChecks
        {
            get
            {
                return _selectionTests;
            }
        }
        
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public XtraTabPagePatient()
        {
            _deletedObjects = new List<PatientHistory>();
            
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Overriding Methods

        #region Initialization & Binding

        /// <summary>
        /// Initialize the object of the tab if it is new tab and initialize some properties
        /// </summary>
        /// <param name="isNew">Determine if the tab is new or opened tab</param>
        /// <param name="parentObject"></param>
        public override void PerformSpecificIntializationSteps(bool isNew, DomainEntity parentObject)
        {            
            TabTypeImage = Resources.Patient_Tab;

            _testManager = new TestsManager();
            _spotCheckManager = new SpotCheckManager();
            _frequencyTestsManager = new FrequencyTestsManager();
            _lookupsManager = new LookupsManager();
            _patientManager = new PatientsManager();
            _settingsManager = new SettingsManager();
            _shippingOrdersManager = new ShippingOrdersManager();
            _vfsManager = new VitalForceSheetManager();
            _autoTestSourceManager = new AutoTestSourceManager();
            _autoTestDestinationManager = new AutoTestDestinationManager();

            _automatedTestScreenEnabled = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.AutomatedTestScreenEnabled);
            xtraTabPageAutoTests.PageVisible = _automatedTestScreenEnabled;

            TabObject = TabObject ?? 
                new Patient { Number = _patientManager.GetNewPatientNumber(),
                              GenderLookup =  new Lookup() , 
                              PatientHistory = new BindingList<PatientHistory>(),
                              Tests = new BindingList<Test>(),
                              VFSRecords = new BindingList<VFS>(),
                              ShippingOrders = new BindingList<ShippingOrder>(),
                              SpotChecks = new BindingList<SpotCheck>(),
                              FrequencyTests = new BindingList<FrequencyTest>(),
                              AutoTests = new BindingList<AutoTest>()
                              };

            var itemTypeLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Product));            

            var firstOrDefault = itemTypeLookup;
            if (firstOrDefault != null)
            {
                _itemTypeProductId = firstOrDefault.Id;
            }

            textEditFirstName.Focus();

            var nonelistTypeLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ListType, ListTypeEnum.None));

            _nonelistTypeLookupId = nonelistTypeLookup != null ? nonelistTypeLookup.Id : 0;

            var yesLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.Yes));
            
            _yesLookupId = yesLookup != null ? yesLookup.Id : 0;

            var potencyTypeLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Potency));
            _potencyTypeLookupId = potencyTypeLookup != null ? potencyTypeLookup.Id : 0;

            BindShippingOrders();

            TabObject.PropertyChanged += TabObject_PropertyChanged;
        }

        /// <summary>
        /// Fill the lookup controls with the collections of objects from the cache
        /// </summary>
        public override void FillLookups()
        {
            try
            {
                UiHelperClass.FillLookup(lookUpEditGender, UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.Gender)));
                UiHelperClass.FillLookup(repositoryItemLookUpEditTestType, UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.TestType)));
                UiHelperClass.FillLookup(repositoryItemLookUpEditTestState, UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.TestState)));
                UiHelperClass.FillLookup(repositoryItemLookUpEditHistoryType, UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.HistoryType)));
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Handles the properties of fields that can change periodically line Min, Max, Mask ...etc
        /// </summary>
        public override void SetFieldsSettings()
        {
            spinEditPatientNumber.Properties.MaxValue = int.MaxValue;
            spinEditPatientNumber.Properties.MinValue = 0;
            spinEditPatientNumber.AutoSizeInLayoutControl = true;
            dateEditDateOfBirth.Properties.MaxValue = DateTime.Now;
            textEditFirstName.Properties.MaxLength = 50;
            textEditLastName.Properties.MaxLength = 50;
            textEditCity.Properties.MaxLength = 50;
            textEditState.Properties.MaxLength = 2;
            textEditZip.Properties.MaxLength = 10;
            textEditHomePhone.Properties.MaxLength = 50;
            textEditWorkPhone.Properties.MaxLength = 50;
            textEditCellPhone.Properties.MaxLength = 50;
            textEditFax.Properties.MaxLength = 50;
            lookUpEditGender.AutoSizeInLayoutControl = true;
            UiHelperClass.SetLayoutControlProperties(layoutControlPatientInfo);
            UiHelperClass.SetTabControlProperties(xtraTabControlMain);
            UiHelperClass.SetViewProperties(gridViewTests);
            UiHelperClass.SetViewProperties(gridViewHistory);

            _selectionTests = new GridCheckMarksSelection(gridViewTests, gridColumnPrint, repositoryItemCheckEditPrint);

            var fontSize =
                    _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.FontSize) });

            gridViewHistory.Appearance.Row.Font = UiHelperClass.GetFontWithSize(float.Parse(fontSize.Value.ToString()));
            gridViewTests.Appearance.Row.Font = UiHelperClass.GetFontWithSize(float.Parse(fontSize.Value.ToString()));
        }

        /// <summary>
        /// Adds the controls needed for binding into the fields list
        /// </summary>
        public override void RegisterFields()
        {
            AddToFieldsList(lookUpEditGender, () => TabPatient.GenderLookup, () => TabPatient.GenderLookup.Id);
            AddToFieldsList(ParentTab, () => TabPatient.FirstName);
            AddToFieldsList(spinEditPatientNumber, () => TabPatient.Number);
            AddToFieldsList(textEditFirstName, () => TabPatient.FirstName);
            AddToFieldsList(textEditLastName, () => TabPatient.LastName);
            AddToFieldsList(textEditAddress1, () => TabPatient.Address1);
            AddToFieldsList(textEditAddress2, () => TabPatient.Address2);
            AddToFieldsList(textEditCity, () => TabPatient.City);
            AddToFieldsList(textEditState, () => TabPatient.State);
            AddToFieldsList(textEditZip, () => TabPatient.Zip);
            AddToFieldsList(dateEditDateOfBirth, () => TabPatient.DateOfBirth);
            AddToFieldsList(textEditHomePhone, () => TabPatient.HomePhone);
            AddToFieldsList(textEditWorkPhone, () => TabPatient.WorkPhone);
            AddToFieldsList(textEditCellPhone, () => TabPatient.CellPhone);
            AddToFieldsList(textEditFax, () => TabPatient.Fax);
            AddToFieldsList(textEditEmail, () => TabPatient.Email);
            AddToFieldsList(gridControlTests, () => TabPatient.Tests,false,true);
            AddToFieldsList(gridControlAutoTests, () => TabPatient.AutoTests, false, true);
            AddToFieldsList(gridControlSpotChecks, () => TabPatient.SpotChecks, false, true);
            AddToFieldsList(gridControlFrequencyTests, () => TabPatient.FrequencyTests, false, true);
            AddToFieldsList(gridControlHistory, () => TabPatient.PatientHistory);
            AddToFieldsList(gridControlVFS, () => TabPatient.VFSRecords, false, true);
        }

        /// <summary>
        /// Performs custom set edit mode action
        /// </summary>
        /// <param name="isReadOnly"></param>
        public override void PerformCustomSetEditMode(bool isReadOnly)
        {
            _isReadOnly = isReadOnly;
            SetEditModeOpenMailClient();
            gridViewTests.OptionsBehavior.ReadOnly = false;

            if (_xtraUserControlVitalRichEditMain != null)
                _xtraUserControlVitalRichEditMain.ReadOnly = isReadOnly;
        }

        /// <summary>
        /// handle set edit mode for mail client
        /// </summary>
        private void SetEditModeOpenMailClient()
        {
            simpleButtonSendEmail.Enabled = !string.IsNullOrEmpty(TabPatient.Email);
        }

        /// <summary>
        /// Sets the lookups error providers datasources
        /// </summary>
        public override void SetLookupErrorProviderDataSource()
        {
            dxErrorProviderLookup.DataSource = TabObject;
            dxErrorProviderLookup.ClearErrors();
        }        

        /// <summary>
        /// Binds the history of the patient.
        /// </summary>
        private void BindHistory()
        {            
            if(TabPatient.PatientHistory == null)
                TabPatient.PatientHistory = new BindingList<PatientHistory>();

            gridControlHistory.DataBindings.Clear();
            UiHelperClass.BindControl(gridControlHistory, TabObject, () => TabPatient.PatientHistory);          
        }

        /// <summary>
        /// Binds the shipping orders
        /// </summary>
        private void BindShippingOrders()
        {
            TabPatient.ShippingOrders = TabObject.ObjectState == DomainEntityState.New ? new BindingList<ShippingOrder>() :
                                                    _shippingOrdersManager.GetShippingOrders(
                                                    new ShippingOrdersFilter()
                                                    {
                                                        PatientId = TabPatient.Id,
                                                        LoadingType = LoadingTypeEnum.All
                                                    });

            gridControlShippingOrders.DataBindings.Clear();

            UiHelperClass.BindControl(gridControlShippingOrders, TabObject, () => TabPatient.ShippingOrders);
        }
        
        /// <summary>
        /// Bind the VitalRichEditControl.
        /// </summary>
        private void BindVitalRichEditControl()
        {
            if (_xtraUserControlVitalRichEditMain != null)
                _xtraUserControlVitalRichEditMain.Bind(TabPatient, () => TabPatient.Notes);
        }
                
        #endregion

        #region Child related actions

        /// <summary>
        /// Checks if a property change should notify the tab
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public override bool ShouldPropertyNotifyTab(string propertyName)
        {
            if (propertyName == ExpressionHelper.GetPropertyName(() => TabPatient.Tests) | 
                propertyName == ExpressionHelper.GetPropertyName(() => TabPatient.PatientHistory))
            {
                return false;
            }            
            return true;
        }

        #endregion

        #region Save related actions
        
        #region Save

        /// <summary>
        /// Performs any specific actions needed for the tab before the saving
        /// </summary>
        public override void BeforeSaveActions()
        {
            if (_xtraUserControlVitalRichEditMain != null)
                _xtraUserControlVitalRichEditMain.PostValue();

            lookUpEditGender.PostLookupValues(TabPatient.GenderLookup);
        }

        /// <summary>
        /// Uses the Patient manager to save the patient        
        /// </summary>
        public override bool Save()
        {
            try
            {
                //appends the marked as deleted object to the patient history list.
                UpdateHistoryListWithDeletedRows();

                if (_patientManager.SavePatient(TabPatient).IsSucceed)
                {
                    //clear the temporary objects.
                    _deletedObjects.Clear();
                    TabPatient.PatientHistory = _patientManager.GetPatientsHistory(new PatientHistoryFilter { PatientId = TabPatient.Id });
                    BindHistory();
                    BindVitalRichEditControl();
                    return true;
                }

                return false;

            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);

                return false;
            }
        }        

        /// <summary>
        /// Apply object's specific actions after saving
        /// </summary>
        public override void AfterSaveActions()
        {
            UibllInteraction.Instance.RefreshPatients();
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Appends the marked as deleted object to the patient history list.
        /// </summary>
        private void UpdateHistoryListWithDeletedRows()
        {
            foreach (var history in _deletedObjects)
            {
                TabPatient.PatientHistory.Add(history);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Cancel delete.
        /// </summary>
        /// <returns></returns>
        public override bool CanDelete()
        {
            if (TabPatient.ObjectState == DomainEntityState.New) return true;

            if (TabPatient.Tests != null && 
                TabPatient.Tests.Count > 0 || 
                TabPatient.SpotChecks != null && 
                TabPatient.SpotChecks.Count > 0 &&
                TabPatient.FrequencyTests != null &&
                TabPatient.FrequencyTests.Count > 0)
            {
                var result = UiHelperClass.ShowConfirmQuestion(StaticKeys.DeletePatientHasTestsOrSpotChecksQuestion);

                return result == DialogResult.Yes;
            }

            return true;
        }

        /// <summary>
        /// Uses the Patient manager to delete the patient        
        /// </summary>
        public override bool Delete()
        {
            try
            {
                var resultHistory = _patientManager.DeletePatientHistory(TabPatient.PatientHistory);

                if (resultHistory.IsSucceed)
                {
                    TabPatient.PatientHistory.Clear();

                    var result = _patientManager.DeletePatient(TabPatient);

                    return result.IsSucceed && resultHistory.IsSucceed;
                }

                return false;
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);

                return false;
            }
        }

        /// <summary>
        /// Perfroms any specific actions needed for the tab before the deleting
        /// </summary>
        public override void BeforeDeleteActions()
        {
            Revert();
        }

        /// <summary>
        /// Apply object's specific actions after deleting
        /// </summary>
        public override void AfterDeleteActions()
        {
            UibllInteraction.Instance.RefreshPatients();
        }

        #endregion

        #region Revert

        /// <summary>
        /// Uses the Patient manager to save the patient        
        /// </summary>
        public override void Revert()
        {
            try
            {
                TabObject = TabObject.ObjectState == DomainEntityState.New ?
                new Patient
                {
                    GenderLookup = new Lookup(),
                    PatientHistory = new BindingList<PatientHistory>()
                } :
                _patientManager.GetPatientById(new SingleItemFilter { ItemId = TabPatient.Id });
                
                //clear the temporary list.
                _deletedObjects.Clear();                
                SetBinding();
                BindVitalRichEditControl();
                SetLookupErrorProviderDataSource();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Apply object's specific actions after cancel actions.
        /// </summary>
        public override void AfterCancelActions()
        {
            UibllInteraction.Instance.RefreshPatients();
        }

        #endregion

        #region Validation

        /// <summary>
        /// Updates the lookup error providers
        /// </summary>
        public override void UpdateLookupErrorProvider()
        {
            dxErrorProviderLookup.UpdateBinding();
        }

        #endregion
        
        #endregion

        #region General Actions

        private void AutoSavePatient()
        {
            if (TabPatient.ObjectState == DomainEntityState.New ||
                TabPatient.ObjectState == DomainEntityState.Modified)
            {
                SaveAction();
            }
        }

        /// <summary>
        /// Performs logic of a custom button in the ribbon bar
        /// </summary>
        /// <param name="barButtonItemName"></param>
        public override void PerformCustomActionLogic(string barButtonItemName)
        {
            if (barButtonItemName == "barButtonItemStartPreliminary" ||
                barButtonItemName == "barButtonItemAutoTest" || 
                barButtonItemName == "barButtonItemStartSpotCheck" ||
                barButtonItemName == "barButtonItemStartFrequencyTest" ||
                barButtonItemName == "barButtonItemVFS")
            {
                if (barButtonItemName == "barButtonItemStartPreliminary")
                {
                    OpenTest(true);
                }
                else if (barButtonItemName == "barButtonItemAutoTest")
                {
                    OpenAutoTest(true);
                }
                else if (barButtonItemName == "barButtonItemStartSpotCheck")
                {
                    OpenSpotCheck(true);
                }
                else if (barButtonItemName == "barButtonItemStartFrequencyTest")
                {
                    OpenFrequencyTest(true);
                }
                else if (barButtonItemName == "barButtonItemVFS")
                {
                    OpenVFS(true);
                }
            }
        }

        /// <summary>
        /// Handles a hot key click
        /// </summary>
        /// <param name="e"></param>
        public override void PerformHotKeyAction(KeyEventArgs e)
        {            
            if (e.Alt && e.KeyCode == Keys.I)
            {
                xtraTabControlMain.SelectedTabPage = xtraTabPagePatientInfo;
                textEditFirstName.Focus();
            }
            else if (e.Alt && e.KeyCode == Keys.T)
            {
                xtraTabControlMain.SelectedTabPage = xtraTabPageTests;
                gridControlTests.Focus();
                gridViewTests.Focus();
            }
            else if (e.Alt && e.KeyCode == Keys.M)
            {
                xtraTabControlMain.SelectedTabPage = xtraTabPageHistory;
                gridControlHistory.Focus();
                gridViewHistory.Focus();
            }
            else if (e.Alt && e.KeyCode == Keys.N)
            {
                xtraTabControlMain.SelectedTabPage = xtraTabPageNotes;
            }
            else if (e.Alt && e.KeyCode == Keys.C)
            {
                xtraTabControlMain.SelectedTabPage = xtraTabPageSpotCheck;
            }
            else if (e.Alt && e.KeyCode == Keys.U)
            {
                if (_automatedTestScreenEnabled)
                {
                    xtraTabControlMain.SelectedTabPage = xtraTabPageAutoTests;
                }
            } 
        }

        #endregion

        #endregion

        #region AutoTests

        /// <summary>
        /// Get focused auto test.
        /// </summary>
        /// <returns></returns>
        private AutoTest GetFocusedAutoTest()
        {
            if (gridViewAutoTests.GetFocusedRow() != null)
            {
                if (gridViewAutoTests != null)
                    if (_autoTestDestinationManager != null)
                        return _autoTestDestinationManager.GetAutoTestById(new AutoTestsFilter { AutoTestId = ((AutoTest)gridViewAutoTests.GetFocusedRow()).Id , LoadingType = LoadingTypeEnum.All});
            }
            return null;
        }

        /// <summary>
        /// Opens an auto test.
        /// </summary>
        /// <param name="isNew"></param>
        private void OpenAutoTest(bool isNew)
        {
            try
            {
                //Proceed only if automation is enabled
                if (!_automatedTestScreenEnabled)
                {
                    return;
                }

                UiHelperClass.ShowWaitingPanel(isNew ? "New Auto Test ..." : "Loading Auto Test ...");

                AutoSavePatient();

                //This condition added here because we have to open a new test before we make sure that the new patient had been saved.
                if (TabPatient.ObjectState != DomainEntityState.Unchanged)
                    return;

                var autoTest = isNew ? new AutoTest
                {
                    Patient = TabPatient,
                    Name = string.Format(StaticKeys.NewAutoTest, TabPatient.AutoTests.Count + 1)
                } : GetFocusedAutoTest();

                var autoTestFrm = new XtraFormAutoTest(autoTest);
                var currentFrequencyTestHandle = gridViewAutoTests.FocusedRowHandle;
                var dataSourceIndex = gridViewAutoTests.GetDataSourceRowIndex(currentFrequencyTestHandle);

                UiHelperClass.HideSplash();
                autoTestFrm.ShowDialog();
                var autoTestUpdated = autoTestFrm.AutoTest;

                // Update the autoTests grid with the changes for the closed Auto test.
                if (autoTestUpdated.ObjectState == DomainEntityState.Modified)
                {
                    // Auto test was modified: Reload the auto test.
                    UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingData);
                    autoTestUpdated = _autoTestDestinationManager.GetAutoTestById(new AutoTestsFilter { AutoTestId = autoTestUpdated.Id });
                    UiHelperClass.HideSplash();
                }

                if (isNew && autoTestUpdated.ObjectState == DomainEntityState.Unchanged)
                {
                    // Auto test is new and was saved successfully: Add the new auto test to the grid.
                    TabPatient.AutoTests.Add(autoTestUpdated);
                }
                else if (!isNew)
                {
                    if (autoTestUpdated.ObjectState == DomainEntityState.Deleted)
                    {
                        // Auto test was deleted: Remove the auto test form the grid.
                        TabPatient.AutoTests.RemoveAt(dataSourceIndex);
                    }
                    else
                    {
                        // Auto test was not changed: Update the grid with the auto test object that came form the from.
                        TabPatient.AutoTests[dataSourceIndex] = autoTestUpdated;
                    }
                }

                UiHelperClass.HideSplash();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Delete the selected auto test.
        /// </summary>
        private void DeleteAutoTest()
        {
            try
            {
                if (UiHelperClass.ShowConfirmQuestion(StaticKeys.DeletePatientConfirmQuestion) == DialogResult.Yes)
                {
                    UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));
                    var currentAutoTest = (AutoTest)gridViewAutoTests.GetFocusedRow();
                    TabPatient.AutoTests.Remove(currentAutoTest);
                    _autoTestDestinationManager.Delete(currentAutoTest);
                    UiHelperClass.HideSplash();
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// The action of checking for opening a test
        /// </summary>
        private void OpenAutoTestAction(bool openByEnter)
        {
            if (gridViewAutoTests.GetFocusedRow() != null && (UiHelperClass.IsClickInRowByMouse(gridViewAutoTests) || openByEnter))
            {
                OpenAutoTest(false);
            }
        }

        #endregion

        #region Test

        /// <summary>
        /// Opens a test
        /// </summary>
        private void OpenTest(bool isNew)
        {
            try
            {
                UiHelperClass.ShowWaitingPanel(isNew? "New Test ..." : "Loading Test ...");

                AutoSavePatient();

                //This condition added here because we have to open a new test before we make sure that the new patient had been saved.
                if (TabPatient.ObjectState != DomainEntityState.Unchanged)
                  return;

                var currentTest = isNew ? new Test
                                            {
                                                Patient = TabPatient,
                                                Name = string.Format(StaticKeys.NewTest, TabPatient.Tests.Count + 1)
                                            }
                                      : GetFocusedTest();

                var testFrm = new frmGeneralTest(currentTest);
                var currentTestId = currentTest.Id;
                var currentTestHandle = gridViewTests.FocusedRowHandle;
                var dataSourceIndex = gridViewTests.GetDataSourceRowIndex(currentTestHandle);

                UiHelperClass.HideSplash();
                testFrm.ShowDialog();
                xtraUserControlPrintingOptionsMain.UpdateOptions();
                var testUpdated = testFrm.TestObject;

                if (testUpdated.ObjectState == DomainEntityState.Modified)
                {
                    UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingData);
                    testUpdated = _testManager.GetTestById(new SingleItemFilter {ItemId = testUpdated.Id});
                    UiHelperClass.HideSplash();
                }

                if (isNew && testUpdated.ObjectState == DomainEntityState.Unchanged)
                {
                    TabPatient.Tests.Add(testUpdated);
                    TabPatient.SpotChecks =
                        _spotCheckManager.GetSpotChecks(new SpotChecksFilter() {PatientId = TabPatient.Id});
                    gridControlSpotChecks.DataSource = TabPatient.SpotChecks;
                    TabPatient.VFSRecords = _vfsManager.GetVFSs(new VFSsFilter { PatientId = TabPatient.Id });
                    gridControlVFS.DataSource = TabPatient.VFSRecords;
                    BindShippingOrders();
                }
                else if (!isNew)
                {
                    if (testUpdated.ObjectState == DomainEntityState.Deleted)
                    {                        
                        TabPatient.Tests.RemoveAt(dataSourceIndex);
                    }
                    else
                    {
                        TabPatient.Tests[dataSourceIndex] = testUpdated;
                        TabPatient.SpotChecks = _spotCheckManager.GetSpotChecks(new SpotChecksFilter() { PatientId = TabPatient.Id });
                        gridControlSpotChecks.DataSource = TabPatient.SpotChecks;
                        TabPatient.VFSRecords = _vfsManager.GetVFSs(new VFSsFilter { PatientId = TabPatient.Id });
                        gridControlVFS.DataSource = TabPatient.VFSRecords;
                        BindShippingOrders();
                    }                    
                }
                
                if (TabPatient.Tests.Count(t => t.Id == currentTestId) == 0)
                {
                    _selectionTests.SelectRow(currentTestHandle, false);                    
                }
                
                UibllInteraction.Instance.UpdateShippingOrdersCount();
                UibllInteraction.Instance.MainForm.UpdateTimingBar();
                UiHelperClass.CheckForAutoBackup(_settingsManager);
                if (testFrm.PatientDetailsChanged)
                {
                    UpdatePatientAddressInfo();
                }
                UiHelperClass.HideSplash();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Open a spot check.
        /// </summary>
        private void OpenSpotCheck(bool isNew)
        {
            try
            {
                UiHelperClass.ShowWaitingPanel(isNew ? "New Spot Check ..." : "Loading Spot Check ...");

                AutoSavePatient();

                //This condition added here because we have to open a new test before we make sure that the new patient had been saved.
                if (TabPatient.ObjectState != DomainEntityState.Unchanged)
                    return;

                var currentSpotCheck = isNew ? new SpotCheck
                {
                    Patient = TabPatient,
                    Name = string.Format(StaticKeys.NewSpotCheck, TabPatient.SpotChecks.Count + 1)
                } : GetFocusedSpotCheck();

                var spotCheckFrm = new frmSpotCheck(currentSpotCheck);
                var currentSpotCheckHandle = gridViewSpotChecks.FocusedRowHandle;
                var dataSourceIndex = gridViewSpotChecks.GetDataSourceRowIndex(currentSpotCheckHandle);

                UiHelperClass.HideSplash();
                spotCheckFrm.ShowDialog();
                xtraUserControlPrintingOptionsMain.UpdateOptions();
                var spotcheckUpdated = spotCheckFrm.SpotCheckObject;

                if (spotcheckUpdated.ObjectState == DomainEntityState.Modified)
                {
                    UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingData);
                    spotcheckUpdated = _spotCheckManager.GetSpotCheckById(new SingleItemFilter { ItemId = spotcheckUpdated.Id });
                    UiHelperClass.HideSplash();
                }

                if (isNew && spotcheckUpdated.ObjectState == DomainEntityState.Unchanged)
                {
                    TabPatient.SpotChecks.Add(spotcheckUpdated);                    
                }
                else if (!isNew)
                {
                    if (spotcheckUpdated.ObjectState == DomainEntityState.Deleted)
                    {
                        TabPatient.SpotChecks.RemoveAt(dataSourceIndex);
                    }
                    else
                    {
                        TabPatient.SpotChecks[dataSourceIndex] = spotcheckUpdated;
                    }
                }

                UiHelperClass.HideSplash();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Open a FrequencyTest.
        /// </summary>
        private void OpenFrequencyTest(bool isNew)
        {
            try
            {
                UiHelperClass.ShowWaitingPanel(isNew ? "New Frequency Test ..." : "Loading Frequency Test ...");

                AutoSavePatient();

                //This condition added here because we have to open a new test before we make sure that the new patient had been saved.
                if (TabPatient.ObjectState != DomainEntityState.Unchanged)
                    return;

                var currentFrequencyTest = isNew ? new FrequencyTest
                {
                    Patient = TabPatient,
                    Name = string.Format(StaticKeys.NewFrequencyTest, TabPatient.FrequencyTests.Count + 1)
                } : GetFocusedFrequencyTest();

                var frequencyTestFrm = new frmFrequencyTest(currentFrequencyTest);
                var currentFrequencyTestHandle = gridViewFrequencyTests.FocusedRowHandle;
                var dataSourceIndex = gridViewFrequencyTests.GetDataSourceRowIndex(currentFrequencyTestHandle);

                UiHelperClass.HideSplash();
                frequencyTestFrm.ShowDialog();
                xtraUserControlPrintingOptionsMain.UpdateOptions();
                var frequencyTestUpdated = frequencyTestFrm.FrequencyTestObject;

                if (frequencyTestUpdated.ObjectState == DomainEntityState.Modified)
                {
                    UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingData);
                    frequencyTestUpdated = _frequencyTestsManager.GetFrequencyTestById(new SingleItemFilter { ItemId = frequencyTestUpdated.Id });
                    UiHelperClass.HideSplash();
                }

                if (isNew && frequencyTestUpdated.ObjectState == DomainEntityState.Unchanged)
                {
                    TabPatient.FrequencyTests.Add(frequencyTestUpdated);
                }
                else if (!isNew)
                {
                    if (frequencyTestUpdated.ObjectState == DomainEntityState.Deleted)
                    {
                        TabPatient.FrequencyTests.RemoveAt(dataSourceIndex);
                    }
                    else
                    {
                        TabPatient.FrequencyTests[dataSourceIndex] = frequencyTestUpdated;
                    }
                }

                UiHelperClass.HideSplash();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Open a VFS
        /// </summary>
        private void OpenVFS(bool isNew)
        {
            try
            {
                UiHelperClass.ShowWaitingPanel(isNew ? "New Vital Force Sheet ..." : "Loading Vital Force Sheet ...");

                AutoSavePatient();

                //This condition added here because we have to open a new test before we make sure that the new patient had been saved.
                if (TabPatient.ObjectState != DomainEntityState.Unchanged)
                    return;

                var currentvfs = isNew ? new VFS
                {
                    Patient = TabPatient,
                    DateTime = DateTime.Now,
                    Name = string.Format(StaticKeys.NewVFS, TabPatient.VFSRecords.Count + 1)
                } : GetFocusedVFS();

                var frmVFS = new frmVFS(currentvfs);
                var currentVFSHandle = gridViewVFS.FocusedRowHandle;
                var dataSourceIndex = gridViewVFS.GetDataSourceRowIndex(currentVFSHandle);

                UiHelperClass.HideSplash();
                frmVFS.ShowDialog();
                xtraUserControlPrintingOptionsMain.UpdateOptions();
                var vfsUpdated = frmVFS.VFSObject;

                if (vfsUpdated.ObjectState == DomainEntityState.Modified)
                {
                    UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingData);
                    vfsUpdated = _vfsManager.GetVFSById(new SingleItemFilter { ItemId = vfsUpdated.Id });
                    UiHelperClass.HideSplash();
                }

                if (isNew && vfsUpdated.ObjectState == DomainEntityState.Unchanged)
                {
                    TabPatient.VFSRecords.Add(vfsUpdated);
                }
                else if (!isNew)
                {
                    if (vfsUpdated.ObjectState == DomainEntityState.Deleted)
                    {
                        TabPatient.VFSRecords.RemoveAt(dataSourceIndex);
                    }
                    else
                    {
                        TabPatient.VFSRecords[dataSourceIndex] = vfsUpdated;
                    }
                }

                UiHelperClass.HideSplash();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Deletes a test
        /// </summary>
        private void DeleteTest()
        {
            try
            {
                if(UiHelperClass.ShowConfirmQuestion(StaticKeys.DeletePatientConfirmQuestion) == DialogResult.Yes)
                {
                    UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));
                    _selectionTests.SelectRow(gridViewTests.FocusedRowHandle,false);
                    var currentTest = (Test)gridViewTests.GetFocusedRow();
                    TabPatient.Tests.Remove(currentTest);
                    _testManager.DeleteTest(currentTest);
                    UibllInteraction.Instance.MainForm.UpdateShippingOrdersCount();
                    UiHelperClass.HideSplash();
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Deletes a SpotCheck.
        /// </summary>
        private void DeleteSpotCheck()
        {
            try
            {
                if (UiHelperClass.ShowConfirmQuestion(StaticKeys.DeletePatientConfirmQuestion) == DialogResult.Yes)
                {
                    UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));
                    var currentSpotChecks = (SpotCheck)gridViewSpotChecks.GetFocusedRow();
                    TabPatient.SpotChecks.Remove(currentSpotChecks);
                    _spotCheckManager.DeleteSpotCheck(currentSpotChecks);
                    UiHelperClass.HideSplash();
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Deletes a FrequencyTest.
        /// </summary>
        private void DeleteFrequencyTest()
        {
            try
            {
                if (UiHelperClass.ShowConfirmQuestion(StaticKeys.DeletePatientConfirmQuestion) == DialogResult.Yes)
                {
                    UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));
                    var currentFrequencyTest = (FrequencyTest)gridViewFrequencyTests.GetFocusedRow();
                    TabPatient.FrequencyTests.Remove(currentFrequencyTest);
                    _frequencyTestsManager.DeleteFrequencyTest(currentFrequencyTest);
                    UiHelperClass.HideSplash();
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Deletes a VFS.
        /// </summary>
        private void DeleteVFS()
        {
            try
            {
                if (UiHelperClass.ShowConfirmQuestion(StaticKeys.DeletePatientConfirmQuestion) == DialogResult.Yes)
                {
                    UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));
                    var currentVFS = (VFS)gridViewVFS.GetFocusedRow();
                    TabPatient.VFSRecords.Remove(currentVFS);
                    _vfsManager.DeleteVFS(currentVFS);
                    UiHelperClass.HideSplash();
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Get focused test.
        /// </summary>
        /// <returns></returns>
        private Test GetFocusedTest()
        {
            if (gridViewTests.GetFocusedRow() != null)
            {
                if (gridViewTests != null)
                    if (_testManager != null)
                        return _testManager.GetTestById(new SingleItemFilter { ItemId = ((Test) gridViewTests.GetFocusedRow()).Id });
            }           
            return null;
        }

        /// <summary>
        /// Gets focused spot check.
        /// </summary>
        /// <returns></returns>
        private SpotCheck GetFocusedSpotCheck()
        {
            if (gridViewSpotChecks.GetFocusedRow() != null)
            {
                if (gridViewSpotChecks != null)
                    if (_spotCheckManager != null)
                        return _spotCheckManager.GetSpotCheckById(new SingleItemFilter { ItemId = ((SpotCheck)gridViewSpotChecks.GetFocusedRow()).Id });
            }

            return null;
        }

        /// <summary>
        /// Gets focused FrequencyTest.
        /// </summary>
        /// <returns></returns>
        private FrequencyTest GetFocusedFrequencyTest()
        {
            if (gridViewFrequencyTests.GetFocusedRow() != null)
            {
                if (gridViewFrequencyTests != null)
                    if (_frequencyTestsManager != null)
                        return _frequencyTestsManager.GetFrequencyTestById(new SingleItemFilter { ItemId = ((FrequencyTest)gridViewFrequencyTests.GetFocusedRow()).Id });
            }

            return null;
        }

        /// <summary>
        /// Gets focused VFS.
        /// </summary>
        /// <returns></returns>
        private VFS GetFocusedVFS()
        {
            if (gridViewVFS.GetFocusedRow() != null)
            {
                if (gridViewVFS != null)
                    if (_vfsManager != null)
                        return _vfsManager.GetVFSById(new SingleItemFilter { ItemId = ((VFS)gridViewVFS.GetFocusedRow()).Id });
            }

            return null;
        }

        /// <summary>
        /// The action of checking for opening a test
        /// </summary>
        private void OpenTestAction(bool openByEnter)
        {
            if (gridViewTests.GetFocusedRow() != null && (UiHelperClass.IsClickInRowByMouse(gridViewTests) || openByEnter))
            {
                _selectionTests.SelectRow(gridViewTests.FocusedRowHandle, true);
                OpenTest(false);
            }
        }

        /// <summary>
        /// The action of checking for opening a test
        /// </summary>
        private void OpenSpotCheckAction(bool openByEnter)
        {
            if (gridViewSpotChecks.GetFocusedRow() != null && (UiHelperClass.IsClickInRowByMouse(gridViewSpotChecks) || openByEnter))
            {
                OpenSpotCheck(false);
            }
        }

        /// <summary>
        /// The action of checking for opening a FrequencyTest(
        /// </summary>
        private void OpenFrequencyTestAction(bool openByEnter)
        {
            if (gridViewFrequencyTests.GetFocusedRow() != null && (UiHelperClass.IsClickInRowByMouse(gridViewFrequencyTests) || openByEnter))
            {
                OpenFrequencyTest(false);
            }
        }

        /// <summary>
        /// Logic for printing
        /// </summary>
        /// <param name="isPreview"></param>
        private void Print(bool isPreview)
        {
            if (_selectionTests.SelectedCount == 0)
            {
                UiHelperClass.ShowInformation("At least one test should be selected.", "No tests to print");
                return;
            }

            if (!UiHelperClass.HasPrintableInfo(xtraUserControlPrintingOptionsMain)) return;

            var selectedTests = new List<Test>();
            UiHelperClass.ShowWaitingPanel("Generating Report ...");
            
            for (int i = 0; i < _selectionTests.SelectedCount; i++)
            {
                var test = gridViewTests.GetRow(_selectionTests.GetSelectedRowHandle(i)) as Test;
                if (test != null)
                {
                    selectedTests.Add(_testManager.GetTestById(new SingleItemFilter { ItemId = test.Id }));    
                }                
            }            

            UiHelperClass.HideSplash();
           
            UiHelperClass.PrintTestReport(isPreview, 
                                           _settingsManager,
                                            TabPatient, 
                                            selectedTests, 
                                            _itemTypeProductId, 
                                            xtraUserControlPrintingOptionsMain,
                                            _nonelistTypeLookupId,
                                            _yesLookupId,
                                            _potencyTypeLookupId);
        }

        /// <summary>
        /// Handles printing client invoice
        /// </summary>
        private void PrintClientInvoice()
        {
            UiHelperClass.PrintClientInvoice(GetFocusedTest(), xtraUserControlPrintingOptionsMain);            
        }
        
        /// <summary>
        /// Handles printing client schedule
        /// </summary>
        private void PrintClientSchedule()
        {
            UiHelperClass.PrintPatientScheduleReport(GetFocusedTest(), _lookupsManager, _settingsManager, xtraUserControlPrintingOptionsMain);
        }

        /// <summary>
        /// Update patient profile info after updating it through shipping order area
        /// </summary>
        private void UpdatePatientAddressInfo()
        {
            try
            {
                RevertChanges();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        #endregion

        #region History

        /// <summary>
        /// Delete patient history
        /// </summary>
        private void DeleteHistory()
        {
            try
            {
                if(UiHelperClass.ShowConfirmQuestion(StaticKeys.DeletePatientConfirmQuestion) == DialogResult.Yes)
                {
                    var focusedRow = gridViewHistory.GetFocusedRow() as PatientHistory;

                    if (focusedRow != null && focusedRow.Id > 0)
                    {
                        //mark the object as deleted.
                        focusedRow.ObjectState = DomainEntityState.Deleted;
                        //add the deleted objects to a temporary list.
                        _deletedObjects.Add(focusedRow);
                    }

                    //delete the row 
                    gridViewHistory.DeleteRow(gridViewHistory.FocusedRowHandle);
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        #endregion

        #endregion

        #region Handlers

        #region General

        /// <summary>
        /// Handles the opening of the context menu for a grid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ContextMenuStripOpening(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(ContextMenuStripOpening), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == contextMenuStripCrud)
                {
                    var isEnabled = false;
                    toolStripMenuItemPrintInvoice.Visible = false;
                    toolStripMenuItemPrintPatientScedule.Visible = false;

                    if (xtraTabControlMain.SelectedTabPage == xtraTabPageTests)
                    {
                        e.Cancel = CancelClickAction(gridViewTests);
                        toolStripMenuItemPrintInvoice.Visible = true;
                        toolStripMenuItemPrintPatientScedule.Visible = true;

                        isEnabled = UiHelperClass.IsClickInRowByMouse(gridViewTests);

                    }
                    else if (xtraTabControlMain.SelectedTabPage == xtraTabPageSpotCheck)
                    {
                        e.Cancel = CancelClickAction(gridViewSpotChecks);

                        isEnabled = UiHelperClass.IsClickInRowByMouse(gridViewSpotChecks);
                    }
                    else if (xtraTabControlMain.SelectedTabPage == xtraTabPageFrequencyTests)
                    {
                        e.Cancel = CancelClickAction(gridViewFrequencyTests);

                        isEnabled = UiHelperClass.IsClickInRowByMouse(gridViewFrequencyTests);
                    }
                    else if (xtraTabControlMain.SelectedTabPage == xtraTabPageVitalForceSheet)
                    {
                        e.Cancel = CancelClickAction(gridViewVFS);

                        isEnabled = UiHelperClass.IsClickInRowByMouse(gridViewVFS);
                    }
                    else if (xtraTabControlMain.SelectedTabPage == xtraTabPageAutoTests)
                    {
                        e.Cancel = CancelClickAction(gridViewAutoTests);

                        isEnabled = UiHelperClass.IsClickInRowByMouse(gridViewAutoTests);

                    }

                    ToolStripMenuItemNew.Enabled = TabObject.ObjectState != DomainEntityState.New;
                    ToolStripMenuItemOpen.Enabled = isEnabled;
                    toolStripMenuItemDelete.Enabled = isEnabled;
                    toolStripMenuItemPrintInvoice.Enabled = isEnabled;
                    toolStripMenuItemPrintPatientScedule.Enabled = isEnabled;
                }
                else if (sender == contextMenuStripHistory)
                {
                    e.Cancel = CancelClickAction(gridViewHistory);
                    toolStripMenuItemDeleteHistory.Enabled = UiHelperClass.IsClickInRowByMouse(gridViewHistory) && TabState != EntityTabState.Unchanged;
                }
            }
        }

        /// <summary>
        /// handles the click on the context menu for a grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ToolStripItemClickedEventHandler(ContextMenuStripItemClicked), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender != null)
                {
                    ((ContextMenuStrip)sender).Hide();

                    if (sender == contextMenuStripCrud)
                    {
                        if (xtraTabControlMain.SelectedTabPage == xtraTabPageTests)
                        {
                            if (e.ClickedItem == ToolStripMenuItemNew)
                            {
                                OpenTest(true);
                            }
                            else if (e.ClickedItem == ToolStripMenuItemOpen)
                            {
                                OpenTest(false);
                            }
                            else if (e.ClickedItem == toolStripMenuItemDelete)
                            {
                                DeleteTest();
                            }
                            else if (e.ClickedItem == toolStripMenuItemPrintInvoice)
                            {
                                PrintClientInvoice();
                            }
                            else if (e.ClickedItem == toolStripMenuItemPrintPatientScedule)
                            {
                                PrintClientSchedule();
                            }
                        }
                        else if (xtraTabControlMain.SelectedTabPage == xtraTabPageSpotCheck)
                        {
                            if (e.ClickedItem == ToolStripMenuItemNew)
                            {
                                OpenSpotCheck(true);
                            }
                            else if (e.ClickedItem == ToolStripMenuItemOpen)
                            {
                                OpenSpotCheck(false);
                            }
                            else if (e.ClickedItem == toolStripMenuItemDelete)
                            {
                                DeleteSpotCheck();
                            }
                        }
                        else if (xtraTabControlMain.SelectedTabPage == xtraTabPageFrequencyTests)
                        {
                            if (e.ClickedItem == ToolStripMenuItemNew)
                            {
                                OpenFrequencyTest(true);
                            }
                            else if (e.ClickedItem == ToolStripMenuItemOpen)
                            {
                                OpenFrequencyTest(false);
                            }
                            else if (e.ClickedItem == toolStripMenuItemDelete)
                            {
                                DeleteFrequencyTest();
                            }
                        }
                        else if (xtraTabControlMain.SelectedTabPage == xtraTabPageVitalForceSheet)
                        {
                            if (e.ClickedItem == ToolStripMenuItemNew)
                            {
                                OpenVFS(true);
                            }
                            else if (e.ClickedItem == ToolStripMenuItemOpen)
                            {
                                OpenVFS(false);
                            }
                            else if (e.ClickedItem == toolStripMenuItemDelete)
                            {
                                DeleteVFS();
                            }
                        }
                        else if (xtraTabControlMain.SelectedTabPage == xtraTabPageAutoTests)
                        {
                            if (e.ClickedItem == ToolStripMenuItemNew)
                            {
                                OpenAutoTest(true);
                            }
                            else if (e.ClickedItem == ToolStripMenuItemOpen)
                            {
                                OpenAutoTest(false);
                            }
                            else if (e.ClickedItem == toolStripMenuItemDelete)
                            {
                                DeleteAutoTest();
                            }
                        }
                    }
                    else if (sender == contextMenuStripHistory)
                    {
                        if (e.ClickedItem == toolStripMenuItemDeleteHistory)
                        {
                            DeleteHistory();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handel the tab page changing to bind the notes.
        /// </summary>
        private void xtraTabControlMain_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new TabPageChangingEventHandler(xtraTabControlMain_SelectedPageChanging), sender, e);

                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.Page == xtraTabPageNotes && _xtraUserControlVitalRichEditMain == null)
                {
                    UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingNotes);

                    _xtraUserControlVitalRichEditMain = new XtraUserControlVitalRichEdit()
                                                            {
                                                                Dock = DockStyle.Fill,
                                                                ReadOnly = _isReadOnly
                                                            };

                    xtraTabPageNotes.Controls.Add(_xtraUserControlVitalRichEditMain);

                    BindVitalRichEditControl();

                    UiHelperClass.HideSplash();
                }
            }
        }

        /// <summary>
        /// Handle open mail client logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                PostValues();
                if (!string.IsNullOrEmpty(TabPatient.Email))
                {
                    var proc = new System.Diagnostics.Process
                    {
                        StartInfo = { FileName = "mailto:" + TabPatient.Email + "?subject=&body=" }
                    };
                    proc.Start();
                }
            }
            catch (Exception)
            {
                UiHelperClass.ShowInformation(StaticKeys.DatabaseBackupErrorOccured);
            }
        }

        /// <summary>
        /// Handle property changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TabObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetEditModeOpenMailClient();
        }

        /// <summary>
        /// Handle updating the gender lookup Name/Key as soon as it is updated by user to prevent issues with gender dependent entities like AutoTest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditGender_EditValueChanged(object sender, EventArgs e)
        {
            lookUpEditGender.PostLookupValues(TabPatient.GenderLookup);
        }

        #endregion
        
        #region Test

        /// <summary>
        /// Handles the doube click event on the  grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewDoubleClick(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(GridViewDoubleClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == gridViewTests)
                {
                    OpenTestAction(false);
                }
                else if(sender == gridViewSpotChecks)
                {
                    OpenSpotCheckAction(false);
                }
                else if (sender == gridViewFrequencyTests)
                {
                    OpenFrequencyTestAction(false);
                }
                else if (sender == gridViewVFS)
                {
                    OpenVFS(false);
                }
                else if (sender == gridViewAutoTests)
                {
                    OpenAutoTestAction(false);
                }
            }
        }

        /// <summary>
        /// Handles the key down event for the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(GridViewKeyDown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.KeyCode == Keys.Return)
                {
                    if (sender == gridViewTests)
                    {
                        OpenTestAction(true);
                    }
                    else if (sender == gridViewSpotChecks)
                    {
                        OpenSpotCheckAction(true);
                    }
                    else if (sender == gridViewFrequencyTests)
                    {
                        OpenFrequencyTestAction(true);
                    }
                    else if (sender == gridViewAutoTests)
                    {
                        OpenAutoTestAction(true);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the print preview action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrintPreview_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonPrintPreview_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == simpleButtonPrintPreview)
                {
                    Print(true);
                }
            }
        }

        /// <summary>
        /// Handles the printing action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrint_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonPrint_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == simpleButtonPrint)
                {
                    Print(false);
                }
            }
        }


        #endregion

        #region History

        /// <summary>
        /// Handles the i
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewHistoryInitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new InitNewRowEventHandler(GridViewHistoryInitNewRow), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                var newHistoryRow = gridViewHistory.GetFocusedRow() as PatientHistory;

                var historyTypeLookups = UiHelperClass.GetLookupByTypeFromCache(new LookupsFilter { Type = Enum.GetName(typeof(LookupTypes), LookupTypes.HistoryType) });

                if (newHistoryRow != null)
                {
                    newHistoryRow.Patient = TabPatient;
                    newHistoryRow.TypeLookup = new Lookup { Id = historyTypeLookups[0].Id };
                }
            }
        }      

        #endregion                        

        #endregion
    }
}