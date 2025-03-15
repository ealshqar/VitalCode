using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq.Expressions;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;
using Vital.UI.UI_Components.Forms;


#region Enumerations

/// <summary>
/// Possible types for a a tab
/// </summary>
public enum TabTypes
{
    Patient    
}

/// <summary>
/// Possible states for a a tab
/// </summary>
public enum EntityTabState
{
    New = 0,        //New tab is opened (No ID is locked)
    Unchanged = 1,  //Edit button wasn't clicked and no field value was changed (No ID is locked)
    Modified = 2,   //Edit button was clicked and a fields or property was changed (ID is locked and object changed)
    Unlocked = 3    //Edit button was clicked without changing any field value (ID is locked only)
}

public enum EditModeChangeMode
{
    ByUser,
    ByCode
}

public enum BarInfoTypes
{
    EditInfo,
    LockInfo,
    DeleteInfo,
    None
}

public enum CurrentTabAction
{
    None,
    IsSaving,
    IsReverting,
    IsDeleting,
    IsLoading
}

public enum FieldsAction
{
    Bind,
    SetEditMode,
    Validate    
}

#endregion

public struct EntityControl
{
    public Control Field { get; set; }
    public Expression<Func<object>> Exp { get; set; }
    public Expression<Func<object>> SubExp { get; set; }
    public bool CustomBind { get; set; }
    public bool CustomEdit { get; set; }
}

namespace Vital.UI
{
    public partial class XtraTabPageEntity : XtraUserControl
    {
        #region Fields

        private CurrentTabAction _currentAction = CurrentTabAction.None;
        private EntityTabState _tabState = EntityTabState.New;
        private object _fieldsList;

        #endregion

        #region Properties

        /// <summary>
        /// Determine the type of the tab opened, Project or Projects View for instance
        /// </summary>
        public TabTypes TabType { get; set; }

        /// <summary>
        /// Determines the state of the tab like New, unchanged, modified, unlocked
        /// </summary>
        public EntityTabState TabState
        {
            get { return _tabState; }
            set
            {
                _tabState = value;
                switch (_tabState)
                {
                    case EntityTabState.Modified:
                        break;
                    case EntityTabState.Unchanged:
                        break;
                }
            }
        }

        /// <summary>
        /// Sets and gets the text of the tab
        /// </summary>
        public string TabText
        {
            get { return (ParentTab == null) ? string.Empty : ParentTab.Text; }
            set { ParentTab.Text = value; }
        }

        /// <summary>
        /// This is a pointer to the XtraTabPage containing the entity user control
        /// </summary>
        public XtraTabPage ParentTab
        {
            get { return Parent as XtraTabPage; }
            set { Parent = value; }
        }

        /// <summary>
        /// This will set or get the business object of the tab
        /// </summary>
        public DomainEntity TabObject { get; set; }

        /// <summary>
        /// This will set and get the icon of the tab depending on the tab's type
        /// </summary>
        public Image TabTypeImage { get; set; }

        /// <summary>
        /// Determine the current action done by the UI
        /// </summary>
        public CurrentTabAction CurrentAction
        {
            get { return _currentAction; }
            set { _currentAction = value; }
        }

        /// <summary>
        /// List of fields in the form
        /// </summary>
        public object FieldsList
        {
            get
            {
                return _fieldsList;
            }
            set
            {
                _fieldsList = value;
            }
        }

        #endregion

        #region Constructors

        public XtraTabPageEntity()
        {
            InitializeComponent();
            InitiateFieldsList();
        }

        #endregion

        #region Methods

        #region Virtual

        #region Initialization & Binding

        /// <summary>
        /// Set the business object by getting the current business object when open or create a new
        /// one when new tab is created and this will occur specificly for each business object type
        /// </summary>
        /// <param name="isNew">Determine if a new tab is created or if a tab is opened</param>
        /// <param name="parentObject"></param>
        public virtual void PerformSpecificIntializationSteps(bool isNew, DomainEntity parentObject)
        {
        }

        /// <summary>
        /// Handles the filling of any lookups in form
        /// </summary>
        public virtual void FillLookups()
        {
        }

        /// <summary>
        /// Handles the properties of fields that can change periodically line Min, Max, Mask ...etc
        /// </summary>
        public virtual void SetFieldsSettings()
        {
        }

        /// <summary>
        /// Adds the controls needed for binding into the fields list
        /// </summary>
        public virtual void RegisterFields()
        {
        }

        /// <summary>
        /// Performs custom binding action
        /// </summary>
        public virtual void PerformCustomBinding()
        {
        }

        /// <summary>
        /// Performs custom set edit mode action
        /// </summary>
        public virtual void PerformCustomSetEditMode(bool isReadOnly)
        {
        }
        
        /// <summary>
        /// Sets the lookups error providers datasources
        /// </summary>
        public virtual void SetLookupErrorProviderDataSource()
        {
        }

        /// <summary>
        /// Updates the lookup error providers
        /// </summary>
        public virtual void UpdateLookupErrorProvider()
        {
        }        

        /// <summary>
        /// Used to refresh the contents of the project, building and component lists
        /// </summary>
        public virtual void CustomRefresh()
        {
        }        

        #endregion

        #region Parent related actions

        /// <summary>
        /// Adds the ID of the parent for the current object in the Lock dictionary for locking purposes
        /// </summary>
        public virtual void AddParentIDToEditDictionary()
        {
        }
        
        #endregion

        #region General Actions

        /// <summary>
        /// Performs the logic of a custom bar button item
        /// </summary>
        /// <param name="barButtonItemName"></param>
        public virtual void PerformCustomActionLogic(string barButtonItemName)
        {

        }

        /// <summary>
        /// Performs logic based on specific hot key
        /// </summary>
        /// <param name="e"></param>
        public virtual void PerformHotKeyAction(KeyEventArgs e) {}

        #endregion

        #region Save related actions

        /// <summary>
        /// Perform save action based on entity type
        /// </summary>
        /// <returns>True if saved successfully</returns>
        public virtual bool Save()
        {
            return true;
        }

        /// <summary>
        /// Perform delete action
        /// </summary>
        /// <returns>True if deleted successfully</returns>
        public virtual bool Delete()
        {
            return true;
        }

        /// <summary>
        /// Perform a checking and user notification before delete action
        /// </summary>
        /// <returns>True if object ready for deletion.</returns>
        public virtual bool CanDelete()
        {
            return true;
        }

        /// <summary>
        /// Perfroms any specific actions needed for the tab after the deleting
        /// </summary>
        public virtual void AfterDeleteActions()
        {
        }

        /// <summary>
        /// Perfroms any specific actions needed for the tab before the deleting
        /// </summary>
        public virtual void BeforeDeleteActions()
        {
        }

        /// <summary>
        /// Perform revert changes action based on entity type
        /// </summary>        
        public virtual void Revert() {}

        /// <summary>
        /// Preform custom save actions for tabs that doesn't present EntityBusinessObject
        /// </summary>
        /// <returns>True if saved successfully</returns>
        public virtual bool CustomSave()
        {
            return true;
        }

        /// <summary>
        /// Preform custom cancel action for tabs that doesn't present EntityBusinessObject
        /// </summary>
        public virtual void CustomCancel()
        {
        }        

        /// <summary>
        /// Perfroms any specific actions needed for the tab after the saving
        /// </summary>
        public virtual void AfterSaveActions()
        {
        }

        /// <summary>
        /// Perfroms any specific actions needed for the tab before the saving
        /// </summary>
        public virtual void BeforeSaveActions()
        {
        }

        /// <summary>
        /// Revert the current business object changes
        /// </summary>
        public virtual void RestoreBusinessObjectValue()
        {
        }

        /// <summary>
        /// Performs specific actions afrer canceling chnages.
        /// </summary>
        public virtual void AfterCancelActions()
        {
        }

        #endregion

        #region Validation

        /// <summary>
        /// Determines if a change in one of the business object's properties should notify the parent
        /// tab or not, by default this will return true unless the inherting tab instance specifiy something
        /// else.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual bool ShouldPropertyNotifyTab(string propertyName)
        {
            return true;
        }

        /// <summary>
        /// Performs any custome validation steps
        /// </summary>
        public virtual void PerformCustomValidation()
        {
        }

        /// <summary>
        /// Performs validation logic after custom validation
        /// </summary>
        public virtual void PerformPostCustomValidation()
        {
        }

        /// <summary>
        /// Calls a sequence to validate the tab and show or hide error icons in addition to 
        /// error summary
        /// </summary>
        /// <returns></returns>
        public bool ValidateTab()
        {
            PerformCustomValidation();
            TabObject.Validate();
            PerformPostCustomValidation();
            dxErrorProviderMain.UpdateBinding();
            UpdateLookupErrorProvider();
            SetTabIcon();
            ShowHideErrorIcons();
            return TabObject.IsValid;
        }

        /// <summary>
        /// Shows or hides the error icons in the tabs for the whole tab and for detail tabs
        /// </summary>
        public virtual void ShowHideErrorIcons()
        {
        }
        
        #endregion

        #region Closing

        /// <summary>
        /// Clears the handlers of the current tab
        /// </summary>
        public virtual void ClearHandlers()
        {
        }

        /// <summary>
        /// Perform logic that should occur before closing action
        /// </summary>
        public virtual void PerformBeforeCloseActions()
        {
        }

        #endregion

        #region UI

        /// <summary>
        /// Performs any actions needed after a tab is selected and became the active tab
        /// </summary>
        public virtual void PerfromAfterSelectionAction()
        {
        }

        #endregion

        #endregion

        #region ButtonsActions

        /// <summary>
        /// Performs the action of the Ok button
        /// </summary>
        public void SaveAndCloseAction()
        {
            SaveOrSaveAndClose(true);
        }

        /// <summary>
        /// Performs the action of the Apply button
        /// </summary>
        public void SaveAction()
        {
            SaveOrSaveAndClose(false);
        }

        /// <summary>
        /// Performs a custom action
        /// </summary>
        public void PerformCustomAction(string barButtonItemName)
        {
            PerformCustomActionLogic(barButtonItemName);
        }

        /// <summary>
        /// Activates the tab edit mode
        /// </summary>
        public void EditAction()
        {
            UnlockTab();
        }

        /// <summary>
        /// Locks the tab and prevents editing
        /// </summary>
        public void LockAction()
        {
            SetEditMode(true, true);
            TabState = EntityTabState.Unchanged;
            UpdateActionButtons();
        }

        /// <summary>
        /// Delete the tab object
        /// </summary>
        public void DeleteAction()
        {
            CurrentAction = CurrentTabAction.IsDeleting;
            if (TabObject != null)
            {
                try
                {
                    if (UiHelperClass.ShowConfirmQuestion(StaticKeys.DeleteConfirmQuestion) == DialogResult.Yes)
                        if (CanDelete())
                        {
                            UiHelperClass.ShowWaitingPanel("Deleting ...");
                            BeforeDeleteActions();
                            PostValues();
                            Delete();
                            UibllInteraction.Instance.CloseTabPage(ParentTab);
                            AfterDeleteActions();
                            UpdateActionButtons();

                            UiHelperClass.HideSplash();
                        }
                }
                catch (NullReferenceException nullReferenceException)
                {
                    throw new VitalBaseException(nullReferenceException.Message);
                }
            }           
        }        

        #endregion

        #region UI Changes

        /// <summary>
        /// Create an XtraTabPage and put the user control in it
        /// </summary>
        private void CreateTab()
        {
            //Creates a tab instance
            var parentTab = new XtraTabPage {PageVisible = true};
            //Page should be set to visible or it won't appear by default
            //Set the dock to fill so it will fill all the available area
            Dock = DockStyle.Fill;
            
            //Add the current entity user control in it
            parentTab.Controls.Add(this);
            //Add the tab to the tab pages and give it focus
            UibllInteraction.Instance.FormTabControl.TabPages.Add(parentTab);
            UibllInteraction.Instance.FormTabControl.SelectedTabPage = parentTab;
        }

        /// <summary>
        /// Sets the icon for the tab, this also includes error icon if there are any errors
        /// </summary>
        public void SetTabIcon()
        {
            //Set the icon depending on the tab type and if there are any errors then add error icon
            ParentTab.Image = (TabObject.IsValid) ? MergeImages(false) : MergeImages(true);
            ParentTab.Tooltip = (TabObject.IsValid) ? null : TabObject.ErrorSummary;
        }

        /// <summary>
        /// This will update the state of the action buttons in the ribbon when the state of the tab changes
        /// </summary>
        public void UpdateActionButtons()
        {
            // Check first if the tab is selected or not because only selected tab should change the state
            // of the action buttons in the ribbon
            if (UibllInteraction.Instance.IsThisTabSelected(ParentTab))
            {
                //if the tab is list view tab, then all buttons will be disabled except close button
                // Save, Save & Close and Cancel will be enabled only for opened modified tabs or "Create New" tabs
                // that was edited by the user in the UI so by default they will be disabled.
                UibllInteraction.Instance.IsSaveAndCloseButtonEnabled(false);
                UibllInteraction.Instance.IsSaveButtonEnabled(false);
                UibllInteraction.Instance.IsCancelButtonEnabled(false);
                UibllInteraction.Instance.IsDeleteButtonEnabled(false);
                UibllInteraction.Instance.IsPreliminaryTestButtonEnabled(false);
                UibllInteraction.Instance.IsAutoTestButtonEnabled(false);
                UibllInteraction.Instance.IsVFSButtonEnabled(false);
                UibllInteraction.Instance.IsSpotCheckButtonEnabled(false);
                UibllInteraction.Instance.IsFrequencyTestButtonEnabled(false);
                // Disable the Edit button by default and then enable it if all the terms are valid
                UibllInteraction.Instance.IsEditButtonEnabled(false);
                UibllInteraction.Instance.IsLockButtonEnabled(false);                

                if (TabObject != null)
                {
                    UibllInteraction.Instance.IsPreliminaryTestButtonEnabled(true);
                    UibllInteraction.Instance.IsAutoTestButtonEnabled(true);
                    UibllInteraction.Instance.IsVFSButtonEnabled(true);
                    UibllInteraction.Instance.IsSpotCheckButtonEnabled(true);
                    UibllInteraction.Instance.IsFrequencyTestButtonEnabled(true);

                    if (TabObject.ObjectState == DomainEntityState.Modified)
                    {
                        UibllInteraction.Instance.IsSaveAndCloseButtonEnabled(true);
                        UibllInteraction.Instance.IsSaveButtonEnabled(true);
                        UibllInteraction.Instance.IsCancelButtonEnabled(true);

                        if (TabObject.ObjectState != DomainEntityState.New)
                        {
                            UibllInteraction.Instance.IsDeleteButtonEnabled(true);
                        }                        
                    }
                    else if (TabObject.ObjectState == DomainEntityState.New)
                    {
                        if (TabState == EntityTabState.Modified || TabState == EntityTabState.New)
                        {
                            UibllInteraction.Instance.IsSaveAndCloseButtonEnabled(true);
                            UibllInteraction.Instance.IsSaveButtonEnabled(true);
                            UibllInteraction.Instance.IsCancelButtonEnabled(false);
                        }
                    }

                    /*Terms are valid only if the tab status is unchanged so "Create New" tabs wouldn't apply and ...
                     If opened tabs are not unlocked meaning that their controls are all ready only and they are not editable and ...
                     If their parent ID is not locked so they are free then thay can be edited.*/

                    else if (TabObject.ObjectState == DomainEntityState.Unchanged)
                    {
                        UibllInteraction.Instance.IsDeleteButtonEnabled(true);
                        if (TabState != EntityTabState.Unlocked)
                        {
                            UibllInteraction.Instance.IsEditButtonEnabled(true);
                        }
                        else
                        {
                            UibllInteraction.Instance.IsLockButtonEnabled(true);                            
                        }
                    }
                }
            }
            else
            {
                if (TabObject != null && (TabObject.ObjectState == DomainEntityState.Deleted || TabObject.ObjectState == DomainEntityState.Unchanged)) return;
                UibllInteraction.Instance.IsPreliminaryTestButtonEnabled(false);
                UibllInteraction.Instance.IsAutoTestButtonEnabled(false);
                UibllInteraction.Instance.IsVFSButtonEnabled(false);
                UibllInteraction.Instance.IsSpotCheckButtonEnabled(false);
                UibllInteraction.Instance.IsFrequencyTestButtonEnabled(false);
                UibllInteraction.Instance.IsSaveAndCloseButtonEnabled(false);
                UibllInteraction.Instance.IsSaveButtonEnabled(false);
                UibllInteraction.Instance.IsCancelButtonEnabled(false);
                UibllInteraction.Instance.IsDeleteButtonEnabled(false);
                UibllInteraction.Instance.IsEditButtonEnabled(false);
                UibllInteraction.Instance.IsLockButtonEnabled(false);
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Call common actions when creating new object or when opening an existing one
        /// </summary>
        private void StartCommonInitializationActions(bool isNew, DomainEntity parentObject)
        {
            //Create a tab and put the current entity user control in the tab page created
            CreateTab();
            InitializeTabObject(isNew, parentObject);
            /*Initilize the binding of the controls for each entity type as specified, this will be
            overriden in the subclasses for the XtraTabPageEntity class*/
            FillLookups();
            SetFieldsSettings();
            RegisterFields();
            SetBinding();
            SetLookupErrorProviderDataSource();
        }

        /// <summary>
        /// This will initialize the tab object by setting its subclass type and tying it to an event handler
        /// that will monitor property changes in the business object
        /// </summary>
        /// <param name="isNew"></param>
        /// <param name="parentObject"></param>
        private void InitializeTabObject(bool isNew, DomainEntity parentObject)
        {
            //Create the business object
            PerformSpecificIntializationSteps(isNew, parentObject);
            //Set the data source for the Error Providor control and initialize it
            dxErrorProviderMain.DataSource = TabObject;
            dxErrorProviderMain.ClearErrors();
            TabObject.PropertyChanged += TabObject_PropertyChanged;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Performs an action for the fields in the fields list one by one
        /// </summary>
        private void SetEditModeSpecific(bool isReadOnly)
        {
            foreach (var field in (List<EntityControl>)FieldsList)
            {
                if (!field.CustomBind)
                {
                    if (field.Field is BaseEdit)
                    {
                        ((BaseEdit)field.Field).Properties.ReadOnly = isReadOnly;
                    }
                    else if (field.Field.GetType() == typeof(GridControl))
                    {
                        var mainView = (GridView)((GridControl)field.Field).MainView;
                        if (mainView != null)
                        {
                            mainView.OptionsBehavior.ReadOnly = isReadOnly;
                        }
                    }
                }
            }
            //Custom Binding for all controls that should be binded ina custom way
            if (((List<EntityControl>)FieldsList).Any(f => f.CustomEdit))
            {
                PerformCustomSetEditMode(isReadOnly);
            }
        }

        /// <summary>
        /// Opens the tab of the business object
        /// </summary>
        /// <param name="openedObject">The passed object</param>
        /// <param name="tabType">The tab type.</param>
        public void Open(DomainEntity openedObject, TabTypes tabType)
        {
            CurrentAction = CurrentTabAction.IsLoading;

            if (openedObject == null)
            {                
                CreateTab();                
                SetEditMode(true, false);
                FillLookups();
                SetFieldsSettings();
                RegisterFields();
                SetBinding();
                SetLookupErrorProviderDataSource();
                UpdateActionButtons();
            }
            else
            {
                UiHelperClass.ShowWaitingPanel("Opening ...");
                
                //By default, state of opened tab should be Unchanged
                TabState = EntityTabState.Unchanged;

                //Set the initialized object of the tab to the passed object
                TabObject = openedObject;

                //Call some methods that are common when opening a new tab or an existing object
                StartCommonInitializationActions(false, null);

                SetEditMode(true, false);
                SetTabIcon();
                UpdateActionButtons();

                CheckAutoEdit();

                UiHelperClass.HideSplash();
            }

            CurrentAction = CurrentTabAction.None;
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Checks for auto editing settings
        /// </summary>
        private void CheckAutoEdit()
        {
            var autoEditSetting =
                ((BindingList<Setting>) CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings)).FirstOrDefault(
                    s => s.Key == EnumNameResolver.Resolve(SettingKeys.AutoEnableEdit));

            if (autoEditSetting != null &&
                UiHelperClass.GetYesLookupId() != null &&
                Convert.ToInt32(autoEditSetting.Value) == UiHelperClass.GetYesLookupId())
            {
                UnlockTab();
            }
        }

        /// <summary>
        /// Initialize a new tab for the business object
        /// </summary>
        public void New(DomainEntity parentObject)
        {
            //By default state of Create New tabs is New
            CurrentAction = CurrentTabAction.IsLoading;
            TabState = EntityTabState.New;
            StartCommonInitializationActions(true, parentObject);
            SetEditMode(false, false);
            SetTabIcon();
            dxErrorProviderMain.ClearErrors();
            UpdateActionButtons();
            CurrentAction = CurrentTabAction.None;
        }

        /// <summary>
        /// Performs the action of the Cancel or cancel and close buttons
        /// </summary>
        public void CancelOrCancelCloseAction(bool isClosing)
        {
            CurrentAction = CurrentTabAction.IsReverting;
            if (TabObject != null)
            {
                try
                {
                    /* New tabs will be closed without revert if the user didn't modify the tab or modified it since
                     it will not make a difference to revert since this will cause no beneifit to the user since 
                     the tab will close anyway */
                    if (TabObject.ObjectState == DomainEntityState.New)
                    {
                        if (isClosing)
                        {
                            if (TabState == EntityTabState.Modified)
                            {

                                if (UiHelperClass.ShowConfirmQuestion(StaticKeys.ChangesWillBeCanceledMessage) == DialogResult.Yes)
                                {
                                    UibllInteraction.Instance.CloseTabPage(ParentTab);
                                }
                            }
                            else
                            {
                                UibllInteraction.Instance.CloseTabPage(ParentTab);
                            }
                        }
                        else //we revert changes only if the tab was canceling without closing
                        {
                            if (UiHelperClass.ShowConfirmQuestion(StaticKeys.ChangesWillBeCanceledMessage) == DialogResult.Yes)
                            {
                                UiHelperClass.ShowWaitingPanel("Undo ...");
                                
                                PostValues();
                                RevertChanges();
                                //Status of the tab should be restored to new
                                TabState = EntityTabState.New;
                                UiHelperClass.HideSplash();
                            }

                            UpdateActionButtons();
                        }
                    }
                    else
                    {
                        switch (TabState)
                        {
                            /* Unchanged case occurs only when clicking Close button since if the tab state is unchanged,
                             The Cancel button will be disabled and this code can be called only from Close, in this case
                             There are no changes to revert and we should only close the tab without releasing its project ID also.*/
                            case EntityTabState.Unchanged:

                                UibllInteraction.Instance.CloseTabPage(ParentTab);
                                break;
                            /* Unlocked case occurs when a tab is opened but and its edit button was clicked but without editing
                            any of its properties, in this case we don't need to revert changes since no changes occured but
                            we still need to release the ID from the dictionary since the tab will be closed and we shouldn't 
                            leave the ID since it will prevent other tabs from being editable, also this case will be called
                            only from the Close button since the Cancel button will be disabled.*/

                            case EntityTabState.Unlocked:

                                UibllInteraction.Instance.CloseTabPage(ParentTab);
                                break;
                            /* This case occures if the user clicked the edit button and then edited the properties of the object,
                            for this case we need to revert changes, and if the user wants to close, then we should release he 
                            the parent's ID and also to restore flags status if the tab will not be closed since this code can
                            be called using Cancel or Close buttons. */

                            case EntityTabState.Modified:

                                if (UiHelperClass.ShowConfirmQuestion(StaticKeys.ChangesWillBeCanceledMessage) == DialogResult.Yes)
                                {
                                    UiHelperClass.ShowWaitingPanel("Undo ...");

                                    PostValues();
                                    RevertChanges();
                                    if (isClosing)
                                    {
                                        UibllInteraction.Instance.CloseTabPage(ParentTab);
                                    }
                                    else
                                    {
                                        /* The status should be restored to unlocked since the user didn't close the tab by calling
                                         cancel button and so the ID wasn't removed and in this case we should restore the state
                                         of the tab to the unlocked status. */
                                        TabState = EntityTabState.Unlocked;
                                    }

                                    UiHelperClass.HideSplash();
                                }

                                break;
                        }

                        UpdateActionButtons();
                    }
                }
                catch (NullReferenceException nullReferenceException)
                {
                    throw new VitalBaseException(nullReferenceException.InnerException.Message);
                }
            }
            CurrentAction = CurrentTabAction.None;
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Save the tab object if it is valid and close it if isClosing is true
        /// </summary>
        /// <returns></returns>
        private void SaveOrSaveAndClose(bool isClosing)
        {
            CurrentAction = CurrentTabAction.IsSaving;
            dxErrorProviderMain.DataSource = TabObject;
            dxErrorProviderMain.UpdateBinding();
            PostValues();
            try
            {
                if (ValidateTab())
                {
                    if (TabState == EntityTabState.Modified || TabObject.ObjectState == DomainEntityState.Modified)
                    {
                        UiHelperClass.ShowWaitingPanel("Saving ...");
                        
                        BeforeSaveActions();

                        Save();
                        TabObject.ErrorSummary = string.Empty;
                        SetEditMode(true, false);                        
                        AfterSaveActions();
                        if (isClosing)
                        {
                            UibllInteraction.Instance.CloseTabPage(ParentTab);
                        }
                        TabState = EntityTabState.Unchanged;
                        UpdateActionButtons();
                        UiHelperClass.HideSplash();
                    }
                }
                else
                {
                    UiHelperClass.ShowError(StaticKeys.ValidationMessageTitle, StaticKeys.ValidationMessageGeneral);
                }
            }
            catch (NullReferenceException nullReferenceException)
            {

                throw new VitalBaseException(nullReferenceException.InnerException.Message);
            }
            CurrentAction = CurrentTabAction.None;
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Reverts the changes in the object for the current tab
        /// </summary>
        public void RevertChanges()
        {
            CurrentAction = CurrentTabAction.IsReverting;
            dxErrorProviderMain.DataSource = TabObject;
            dxErrorProviderMain.UpdateBinding();
            PostValues();
            Revert();
            
            TabState = EntityTabState.Unchanged;
            InitializeTabObject(false, TabObject);            
            SetBinding();
            SetLookupErrorProviderDataSource();
            SetEditMode(false, false);
            SetTabIcon();            
            
            AfterCancelActions();            
            TabObject.ErrorSummary = string.Empty;
            CurrentAction = CurrentTabAction.None;
        }

        /// <summary>
        /// Release the locked tab by removing its ID from the dictionary so any other object in 
        /// the hirarchy can be edited
        /// </summary>
        public void UnlockTab()
        {
            if (TabObject != null)
            {                
                SetEditMode(false, true);
                TabState = EntityTabState.Unlocked;
                UpdateActionButtons();
            }
        }

        /// <summary>
        /// Binds the controls for each entity user control specificly depending on its business object type
        /// </summary>
        public void SetBinding()
        {
            PerformFieldsAction(FieldsAction.Bind);
        }

        /// <summary>
        /// Set the edit mode of the tab and its controls
        /// </summary>
        /// <param name="isReadOnly"></param>
        /// <param name="isChangedByUser">Indicate that the changed happend by a user click on edit button and not initialization</param>
        public void SetEditMode(bool isReadOnly, bool isChangedByUser)
        {
            SetEditModeSpecific(isReadOnly);
        }

        /// <summary>
        /// Posts the values in the controls that are not yet comitted to the datasource because the user
        /// clicked save or cancel without leaving the editor to another editor first.
        /// </summary>
        public void PostValues()
        {
            PerformFieldsAction(FieldsAction.Validate);
        }

        /// <summary>
        /// Performs an action for the fields in the fields list one by one
        /// </summary>
        private void PerformFieldsAction(FieldsAction action)
        {
            foreach (var field in (List<EntityControl>)FieldsList)
            {
                switch (action)
                {
                    case FieldsAction.Bind:
                        if (!field.CustomBind)
                        {
                            if (field.SubExp != null)
                            {
                                UiHelperClass.BindControl(field.Field, TabObject, field.Exp, field.SubExp);
                            }
                            else
                            {
                                UiHelperClass.BindControl(field.Field, TabObject, field.Exp);
                            }
                        }                                       
                        break;
                    case FieldsAction.Validate:
                        if (field.Field.GetType() == typeof(BaseEdit))
                        {
                            ((BaseEdit)field.Field).DoValidate();
                        }
                        else if (field.Field.GetType() == typeof(LookUpEditBase))
                        {
                            ((LookUpEditBase)field.Field).DoValidate();
                        }
                        else if (field.Field.GetType() == typeof(GridView))
                        {
                            var mainView = (GridView)((GridControl)field.Field).MainView;
                            if (mainView != null)
                            {
                                UiHelperClass.GridViewPostValues(mainView);
                            }
                        }
                        break;
                }
            }
            //Custom Binding for all controls that should be binded ina custom way
            if(((List<EntityControl>)FieldsList).Any(f => f.CustomBind))
            {
                PerformCustomBinding();
            }
            
        }

        #endregion

        #region General Common Functionalities

        /// <summary>
        /// Initializes the field list collection
        /// </summary>
        public void InitiateFieldsList()
        {
            _fieldsList = new List<EntityControl>();
        }

        /// <summary>
        /// Add a control to the list of controls controlable by form
        /// </summary>
        /// <param name="control"></param>
        /// <param name="expression"></param>        
        public void AddToFieldsList(Control control, Expression<Func<object>> expression)
        {
            if (((List<EntityControl>)FieldsList).Any(e => e.Field == control)) return;
            ((List<EntityControl>)FieldsList).Add(new EntityControl { Field = control, Exp = expression });
        }

        /// <summary>
        /// Add a control to the list of controls controlable by form
        /// </summary>
        /// <param name="control"></param>
        /// <param name="expression"></param>
        /// <param name="subExpression"></param>
        public void AddToFieldsList(Control control, 
                                    Expression<Func<object>> expression, 
                                    Expression<Func<object>> subExpression)
        {
            if (((List<EntityControl>)FieldsList).Any(e => e.Field == control)) return;
            ((List<EntityControl>)FieldsList).Add(new EntityControl { Field = control, Exp = expression, SubExp = subExpression });
        }

        /// <summary>
        /// Add a control to the list of controls controlable by form
        /// </summary>
        /// <param name="control"></param>
        /// <param name="expression"></param>
        /// <param name="customBind"></param>
        /// <param name="customEdit"></param>        
        public void AddToFieldsList(Control control, Expression<Func<object>> expression, bool customBind, bool customEdit)
        {
            if (((List<EntityControl>)FieldsList).Any(e => e.Field == control)) return;
            ((List<EntityControl>)FieldsList).Add(new EntityControl
            {
                Field = control,
                Exp = expression,
                CustomBind = customBind,
                CustomEdit = customEdit
            });
        }

        /// <summary>
        /// Add a control to the list of controls controlable by form
        /// </summary>
        /// <param name="control"></param>
        /// <param name="expression"></param>
        /// <param name="subExpression"></param>
        /// <param name="customBind"></param>
        /// <param name="customEdit"></param>
        public void AddToFieldsList(Control control, 
                                    Expression<Func<object>> expression, 
                                    Expression<Func<object>> subExpression,
                                    bool customBind,
                                    bool customEdit)
        {
            if (((List<EntityControl>) FieldsList).Any(e => e.Field == control)) return;
            ((List<EntityControl>) FieldsList).Add(new EntityControl
                                                                       {
                                                                           Field = control,
                                                                           Exp = expression,
                                                                           SubExp = subExpression,
                                                                           CustomBind = customBind,
                                                                           CustomEdit = customEdit
                                                                       });
        }
        
        /// <summary>
        /// Merge the images of error icon and the icon for the tab
        /// </summary>
        /// <param name="isErrorIncluded">Determine if the error icon should be included or not</param>
        /// <returns></returns>
        public Image MergeImages(bool isErrorIncluded)
        {
            Image tempImage = Resources.TabEmptyIcon;
            //Put the image in a graphics object for editing
            Graphics graphics = Graphics.FromImage(tempImage);
            if (TabTypeImage != null)
            {
                graphics.DrawImage(GetThumbnailImage(TabTypeImage), 1, 1, 16, 16);
                graphics.DrawImage(GetThumbnailImage(TabTypeImage), 1, 1, 16, 16);
            }

            if (isErrorIncluded)
            {
                graphics.DrawImage(Resources.Error, 18, 1, 16, 16);
                graphics.Dispose();
                return tempImage;
            }
            //Dispose the graphics object
            graphics.Dispose();
            return GetThumbnailImage(TabTypeImage);
        }

        /// <summary>
        /// Return the passed image but with a smaller size
        /// </summary>
        /// <param name="largeImage"></param>
        /// <returns></returns>
        public Image GetThumbnailImage(Image largeImage)
        {
            return largeImage.GetThumbnailImage(16, 16, ThumbnailTargetMethod, new IntPtr());
        }

        /// <summary>
        /// This method is used only as a parameter for the GetThumbnailImageAbort method of Image type
        /// </summary>
        /// <returns></returns>
        public bool ThumbnailTargetMethod()
        {
            return false;
        }      

        /// <summary>
        /// Determines if gridview click action should be canceled
        /// </summary>
        /// <param name="view"></param>
        public bool CancelClickAction(GridView view)
        {
            return view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position)).InGroupPanel ||
                   view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position)).InFilterPanel ||
                   view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position)).InColumnPanel ||
                   view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position)).InGroupColumn;
        }
     
        #endregion

        #endregion

        #region Handlers

        /// <summary>
        /// Notify the current tab that a property has changed in its business object
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void TabObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ShouldPropertyNotifyTab(e.PropertyName) && TabState != EntityTabState.Unchanged)
            {
                TabState = EntityTabState.Modified;
                UpdateActionButtons();
            }            
        }       

        #endregion
    }
}