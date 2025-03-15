using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.TestProtocols;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;
using Vital.UI.UI_Components.BaseForms;
using ErrorInfo = DevExpress.XtraEditors.DXErrorProvider.ErrorInfo;

namespace Vital.UI.UI_Components.Forms.DataManagement
{
    public partial class frmTestProtocolManagement : VitalBaseForm
    {

        #region Fields

        private LookupsManager _lookupsManager;
        private TestProtocolsManager _testProtocolsManager;
        private TestsManager _testsManager;
        private TestProtocol _testProtocol;
        private List<ProtocolStep> _deletedStepObjects;
        private List<ProtocolItem> _deletedItemObjects;

        private ProcessResult _canEditProtocolSteps;


        #endregion

        #region Constructors

        /// <summary>
        /// Construct new instance of frmTestProtocolManagement.
        /// </summary>
        /// <param name="testProtocol"></param>
        public frmTestProtocolManagement(TestProtocol testProtocol)
        {
            InitializeComponent();
            _testProtocol = testProtocol;            
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the TestProtocol.
        /// </summary>
        public TestProtocol TestProtocol
        {
            get { return _testProtocol; }
            set { _testProtocol = value; }
        }

        #endregion

        #region Methods

        #region Initialization & Binding & Helpers

        /// <summary>
        /// Initialize the object of the form if it is new object and initialize some properties
        /// </summary>
        public override void PerformSpecificIntializationSteps()
        {            
            _lookupsManager = new LookupsManager();

            _testsManager = new TestsManager();

            _testProtocolsManager = new TestProtocolsManager();

            if (_testProtocol == null) _testProtocol = new TestProtocol();

            if (_testProtocol.ObjectState == DomainEntityState.New)
            {
                _testProtocol.Name = StaticKeys.NewTestProtocol;
                _testProtocol.ProtocolSteps = new BindingList<ProtocolStep>();
                _testProtocol.ProtocolItems = new BindingList<ProtocolItem>();
                _testProtocol.CreationDateTime = DateTime.Now;
            }

            _canEditProtocolSteps = _testProtocolsManager.CanEditTestProtocolSteps(_testProtocol);

            SetFormTitle(_testProtocol.Name);

            UiHelperClass.SetLayoutControlProperties(layoutControl1);
            UiHelperClass.SetViewProperties(gridViewProtocolSteps);
            UiHelperClass.SetViewProperties(gridViewStartingItems);

            _deletedStepObjects = new List<ProtocolStep>();

            _deletedItemObjects = new List<ProtocolItem>();

            SetFormStatus(_testProtocol.ObjectState == DomainEntityState.New);
        }

        /// <summary>
        /// Set the handlers.
        /// </summary>
        public override void SetupHandllers()
        {
            TestProtocol.PropertyChanged += TestProtocol_PropertyChanged;
            TestProtocol.ProtocolSteps.RaiseListChangedEvents = true;
            TestProtocol.ProtocolSteps.ListChanged += TestProtocolLists_ListChanged;
            TestProtocol.ProtocolItems.RaiseListChangedEvents = true;
            TestProtocol.ProtocolItems.ListChanged += TestProtocolLists_ListChanged;
        }
        
        /// <summary>
        /// Clear the handlers.
        /// </summary>
        public override void ClearHandlers()
        {
            TestProtocol.PropertyChanged -= TestProtocol_PropertyChanged;
            TestProtocol.ProtocolSteps.ListChanged -= TestProtocolLists_ListChanged;
            TestProtocol.ProtocolItems.ListChanged -= TestProtocolLists_ListChanged;
        }

        /// <summary>
        /// Bind the controls of the form.
        /// </summary>
        public override void SetBinding()
        {
            FillLookUps();

            UiHelperClass.BindControl(textEditName, TestProtocol, () => TestProtocol.Name);
            UiHelperClass.BindControl(memoEditDescription, TestProtocol, () => TestProtocol.Description);
            UiHelperClass.BindControl(gridControlProtocolSteps, TestProtocol, () => TestProtocol.ProtocolSteps);
            UiHelperClass.BindControl(gridControlStartingItems, TestProtocol, () => TestProtocol.ProtocolItems);

            gridViewProtocolSteps.RefreshData();
            gridViewStartingItems.RefreshData();
        }

        /// <summary>
        /// Set up the Main Error Provider.
        /// </summary>
        public override void SetupMainErrorProvider()
        {
            dxErrorProviderMain.DataSource = TestProtocol;            
        }

        /// <summary>
        /// Clear the Binding.
        /// </summary>
        public override void ClearBinding()
        {
            textEditName.DataBindings.Clear();
            memoEditDescription.DataBindings.Clear();
            gridControlProtocolSteps.DataBindings.Clear();
            gridControlProtocolSteps.DataSource = null;
            gridControlStartingItems.DataBindings.Clear();
            gridControlStartingItems.DataSource = null;
        }

        /// <summary>
        /// Sets the edit mode of the tab
        /// </summary>
        /// <param name="isReadOnly">if true then the form will be in ready only mode</param>
        public override void SetEditMode(bool isReadOnly)
        {            
            textEditName.Properties.ReadOnly = isReadOnly;
            memoEditDescription.Properties.ReadOnly = isReadOnly;
            addStartingItemSimpleButton.Enabled = !isReadOnly;
            btnMoveUp.Enabled = !isReadOnly;
            btnMoveDown.Enabled = !isReadOnly;

            gridViewProtocolSteps.OptionsBehavior.ReadOnly = !_canEditProtocolSteps.IsSucceed || isReadOnly;

            layoutControlGroupProtocolSteps.CaptionImage = (_canEditProtocolSteps.IsSucceed || isReadOnly) ? null : Resources.locked;
            layoutControlGroupProtocolSteps.OptionsToolTip.ToolTip = (_canEditProtocolSteps.IsSucceed || isReadOnly)
                                                                ? string.Empty
                                                                : _canEditProtocolSteps.Message;

            layoutControlGroupProtocolSteps.Text = (_canEditProtocolSteps.IsSucceed || isReadOnly)
                                                                ? StaticKeys.ProtocolSteps
                                                                : StaticKeys.ProtocolStepsLocked;

            gridViewProtocolSteps.OptionsView.NewItemRowPosition = !_canEditProtocolSteps.IsSucceed || isReadOnly
                                                                       ? NewItemRowPosition.None
                                                                       : NewItemRowPosition.Bottom;
        }

        /// <summary>
        /// Rebinds the object.
        /// </summary>
        public override void Rebind()
        {
            base.Rebind();
            SetFormTitle(_testProtocol.Name);

        }

        /// <summary>
        /// Fill the lookups.
        /// </summary>
        public override void FillLookUps()
        {
            try
            {

                repositoryItemLookUpEditStep.DataSource =
                        UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.ItemType));

                repositoryItemLookUpEditStep.ForceInitialize();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }

        }

        /// <summary>
        /// Swap the steps, for moving up or down.
        /// </summary>
        /// <param name="focusedRowHandle">Focused row handle.</param>
        /// <param name="isMovingUp">Step 2 index.</param>
        private void SwapSteps(int focusedRowHandle, bool isMovingUp)
        {
            int firstHandle = focusedRowHandle;
            int firstIndex = gridViewProtocolSteps.GetDataSourceRowIndex(firstHandle);

            var view = gridViewProtocolSteps;
            
            if (view.IsNewItemRow(firstHandle) || 
                (isMovingUp && view.IsFirstRow) || 
                (!isMovingUp && view.IsLastRow)) return;

            view.GridControl.Focus();

            int secondIndex = gridViewProtocolSteps.GetDataSourceRowIndex(isMovingUp ? firstHandle - 1 : 
                                                                                       firstHandle + 1);

            var tmpOrder = TestProtocol.ProtocolSteps[firstIndex].Order;

            TestProtocol.ProtocolSteps[firstIndex].Order = TestProtocol.ProtocolSteps[secondIndex].Order;

            TestProtocol.ProtocolSteps[secondIndex].Order = tmpOrder;

            var tmp = TestProtocol.ProtocolSteps[firstIndex];

            TestProtocol.ProtocolSteps[firstIndex] = TestProtocol.ProtocolSteps[secondIndex];

            TestProtocol.ProtocolSteps[secondIndex] = tmp;

            gridViewProtocolSteps.FocusedRowHandle = isMovingUp ? firstHandle - 1 : firstHandle + 1;
        }

        #endregion

        #region Save related actions

        /// <summary>
        /// Uses the Tests manager to save the test        
        /// </summary>
        public override bool Save(bool isClosing)
        {
            try
            {

                if (!_testProtocol.Validate())
                    return false;

                UpdateListsWithDeletedRows();

                var result = _testProtocolsManager.SaveTestProtocol(_testProtocol);

                if (result.IsSucceed)
                {
                    _deletedStepObjects.Clear();
                }

                return result.IsSucceed;
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
                                                           
                return false;
            }
        }
        
        /// <summary>
        /// Show or Hide the errors upper control they not supported the DxErrorProvider.
        /// </summary>
        public override void ShowHideErrorIcons()
        {
            if (_canEditProtocolSteps.IsSucceed)
                ShowHideValidateGridControl(_testProtocol.ProtocolSteps.Count > 0, ExpressionHelper.GetPropertyName(() => _testProtocol.ProtocolSteps),
                             layoutControlGroupProtocolSteps);

            ShowHideValidateGridControl(_testProtocol.ValidateProtocolItems(), ExpressionHelper.GetPropertyName(() => _testProtocol.ProtocolItems),
                         layoutControlGroupStartingItems);
        }
        
        /// <summary>
        /// Setting some properties.
        /// </summary>
        public override void SetProperties()
        {
            textEditName.Properties.MaxLength = 50;
            memoEditDescription.Properties.MaxLength = int.MaxValue;
        }
        
        /// <summary>
        /// Posts the values in the controls that are not yet comitted to the datasource because the user
        /// clicked save or cancel without leaving the editor to another editor first.
        /// </summary>
        public override void PostValues()
        {
            textEditName.DoValidate();
            memoEditDescription.DoValidate();

            gridViewStartingItems.PostEditor();
            gridViewStartingItems.ValidateEditor();
            gridViewStartingItems.UpdateCurrentRow();

            gridViewProtocolSteps.PostEditor();
            gridViewProtocolSteps.ValidateEditor();
        }

        /// <summary>
        /// Actions After save.
        /// </summary>
        public override void AfterSaveAction()
        {
            TestProtocol = _testProtocolsManager.GetTestProtocolById(new SingleItemFilter { ItemId = TestProtocol.Id });
            Rebind();
        }

        /// <summary>
        /// Can the form close. With user notification.
        /// </summary>
        /// <returns></returns>
        public override bool CanClose()
        {
            if (TestProtocol.ObjectState == DomainEntityState.Unchanged || 
                TestProtocol.ObjectState == DomainEntityState.Deleted) return true;
            
            var dialogResult = UiHelperClass.ShowConfirmQuestion(StaticKeys.ChangesWillBeCanceledMessage);

            return dialogResult == DialogResult.Yes;
        }

        /// <summary>
        /// Revert the Test object.      
        /// </summary>
        public override bool Revert()
        {
            try
            {
                IsLoaded = false;

                TestProtocol = TestProtocol.ObjectState == DomainEntityState.New
                                   ? new TestProtocol
                                   {
                                       Name = StaticKeys.NewTestProtocol,
                                       ProtocolSteps = new BindingList<ProtocolStep>(),
                                       ProtocolItems = new BindingList<ProtocolItem>(),
                                       CreationDateTime = DateTime.Now

                                   }
                                   : _testProtocolsManager.GetTestProtocolById(new SingleItemFilter { ItemId = TestProtocol.Id });

                Rebind();

                IsLoaded = true;

                return true;
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);

                return false;
            }
        }

        /// <summary>
        /// Delete a Test Protocol.
        /// </summary>
        /// <returns></returns>
        public override bool Delete()
        {
            try
            {

                var canDeleteTestProtocol = _testProtocolsManager.CanDeleteTestProtocol(_testProtocol);

                if (!canDeleteTestProtocol.IsSucceed)
                {
                    UiHelperClass.ShowInformation(canDeleteTestProtocol.Message);

                    return false;
                }

                var result = _testProtocolsManager.DeleteTestProtocol(_testProtocol);

                if (result.IsSucceed) Close();

                return result.IsSucceed;
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);

                return false;
            }

        }
        
        /// <summary>
        /// Before delete actions.
        /// </summary>
        public override void BeforeDeleteAction()
        {
            PostValues();
            Revert();
        }

        /// <summary>
        /// Delete a protocol step.
        /// </summary>
        /// <param name="rowHandler">The row handler.</param>
        /// <returns></returns>
        public bool DeleteProtocolStep(int rowHandler)
        {
            var currentRow = gridViewProtocolSteps.GetRow(rowHandler) as ProtocolStep;

            if (currentRow == null) return false;

            currentRow.ObjectState = DomainEntityState.Deleted;

            var currentOrder = currentRow.Order;

            _deletedStepObjects.Add(currentRow);

            gridViewProtocolSteps.DeleteRow(gridViewProtocolSteps.FocusedRowHandle);

            for (var i = rowHandler; i < gridViewProtocolSteps.RowCount; i++)
            {
                var step = gridViewProtocolSteps.GetRow(i) as ProtocolStep;

                if (step != null)
                {
                    step.Order = currentOrder++;
                }
            }

            return true;
        }

        /// <summary>
        /// Delete a protocol starting item.
        /// </summary>
        /// <param name="rowHandler">The row handler</param>
        /// <returns></returns>
        public bool DeleteProtocolStartingItem(int rowHandler)
        {
            var currentRow = gridViewStartingItems.GetRow(rowHandler) as ProtocolItem;

            if (currentRow == null) return false;

            currentRow.ObjectState = DomainEntityState.Deleted;

            _deletedItemObjects.Add(currentRow);

            gridViewStartingItems.DeleteRow(gridViewStartingItems.FocusedRowHandle);

            return true;
        }

        /// <summary>
        /// Add the deleted objects to the actual list again.
        /// </summary>
        private void UpdateListsWithDeletedRows()
        {
            foreach (var step in _deletedStepObjects)
            {
                TestProtocol.ProtocolSteps.Add(step);
            }
            foreach (var item in _deletedItemObjects)
            {
                TestProtocol.ProtocolItems.Add(item);
            }
        }       
        
        /// <summary>
        /// Validate a grid. (List)
        /// </summary>
        /// <param name="isValid">The is valid boolean.</param>
        /// <param name="propertyName">Property Name.</param>
        /// <param name="layoutControlGroup">Layout Control Group object.</param>
        private void ShowHideValidateGridControl(bool isValid, string propertyName, LayoutControlGroup layoutControlGroup)
        {
            var errorInfo = new ErrorInfo();

            _testProtocol.GetPropertyError(propertyName, errorInfo);

            layoutControlGroup.CaptionImage = (isValid) ? null : Resources.Error;
            layoutControlGroup.OptionsToolTip.ToolTip = (isValid)
                                                            ? string.Empty
                                                            : errorInfo.ErrorText;
        }
        
        #endregion

        #endregion

        #region Handlers

        /// <summary>
        /// Handel the form closing event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !CanClose();
        }

        /// <summary>
        /// Handles the clicking on the add new starting items button
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void addStartingItemSimpleButton_Click(object sender, EventArgs e)
        {
            var items = TestProtocol.ProtocolItems.Select(pi => pi.Item).ToList();

            var frmStartingItems = new frmNewTestProtocolStartingItems(new BindingList<Item>(items));
            var result = frmStartingItems.ShowDialog();
            if (result == DialogResult.OK)
            {
                var selectedItems = frmStartingItems.SelectedItems;

                foreach (var selectedItem in selectedItems)
                {
                    var exists = TestProtocol.ProtocolItems.Any(pi => pi.Item.Id == selectedItem.Id);

                    if (!exists)
                    {
                        TestProtocol.ProtocolItems.Add(new ProtocolItem
                        {
                            Item = selectedItem,
                            TestProtocol = _testProtocol
                        });
                    }
                }

                for (var i = 0; i < TestProtocol.ProtocolItems.Count; i++)
                {
                    var protocolItem = TestProtocol.ProtocolItems.ElementAtOrDefault(i);

                    if (protocolItem != null)
                    {
                        var exists = selectedItems.Any(pi => pi.Id == protocolItem.Item.Id);

                        if (!exists)
                        {
                            TestProtocol.ProtocolItems.Remove(protocolItem);
                            protocolItem.ObjectState = DomainEntityState.Deleted;
                            _deletedItemObjects.Add(protocolItem);
                            i--;
                        }    
                    }
                }
            }
        }

        /// <summary>
        /// Handles the opening of the context menu for a grid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ContextMenuStripOpening(object sender, CancelEventArgs e)
        {
            if (sender == contextMenuStripStartingItems)
            {
                e.Cancel = UiHelperClass.CancelClickAction(gridViewStartingItems);

                var isEnabled = UiHelperClass.IsClickInRow(gridViewStartingItems);

                toolStripMenuItemDeleteStartingItem.Enabled = isEnabled && IsInEditMode;
            }
            else if (sender == contextMenuStripSteps)
            {
                e.Cancel = UiHelperClass.CancelClickAction(gridViewProtocolSteps);

                var isEnabled = UiHelperClass.IsClickInRow(gridViewProtocolSteps);

                toolStripMenuItemDeleteStep.Enabled = isEnabled && IsInEditMode && _canEditProtocolSteps.IsSucceed;
            }
        }

        /// <summary>
        /// handles the click on the context menu for a grid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ContextMenuStripItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (sender != null)
            {
                ((ContextMenuStrip)sender).Hide();

                if (e.ClickedItem == toolStripMenuItemDeleteStep)
                {
                    DeleteProtocolStep(gridViewProtocolSteps.FocusedRowHandle);
                }
                else if (e.ClickedItem == toolStripMenuItemDeleteStartingItem)
                {
                    DeleteProtocolStartingItem(gridViewStartingItems.FocusedRowHandle);
                }
            }
        }

        /// <summary>
        /// Handel moving step up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            SwapSteps(gridViewProtocolSteps.FocusedRowHandle, true);
        }

        /// <summary>
        /// Handel the Moving step down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnMoveDown_Click(object sender, System.EventArgs e)
        {
            SwapSteps(gridViewProtocolSteps.FocusedRowHandle, false);
        }

        /// <summary>
        /// Init new step row.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridViewProtocolSteps_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                var view = sender as GridView;

                if (view == null) return;

                var step = view.GetRow(e.RowHandle) as ProtocolStep;

                if (step != null)
                {
                    step.TestProtocol = TestProtocol;
                    step.Order = TestProtocol.ProtocolSteps.Max(s => s.Order) + 1;
                }
            }
            catch (Exception ex)
            {
                UiHelperClass.ShowError(string.Empty , ex);
            }
        }

        /// <summary>
        /// Validate the editor for duplication steps 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridViewProtocolSteps_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                if (!IsInEditMode) return;

                var view = sender as GridView;

                if (view == null || e.Value == null) return;

                var foucsedStep = gridViewProtocolSteps.GetFocusedRow() as ProtocolStep;

                if (foucsedStep == null) return;

                if (TestProtocol.ProtocolSteps.Where(s => s.Type.Id == (int)e.Value).ToList().Count > 0)
                {
                    if (foucsedStep.Type != null && foucsedStep.Type.Id == (int)e.Value) return;

                    e.Valid = false;
                    e.ErrorText = StaticKeys.StepAlreadyExists;
                }
            }
            catch (Exception exception)
            {
                UiHelperClass.ShowError(exception.Message, exception);
            }

        }

        /// <summary>
        /// Handel the Hide for the editor on the steps grid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridViewProtocolSteps_HiddenEditor(object sender, EventArgs e)
        {
            try
            {
                var view = sender as GridView;

                if(view != null)
                {
                    var step = view.GetFocusedRow() as ProtocolStep;

                    if (step == null || step.Type == null) return;

                    if (step.Type.Id == 0)
                    {
                        view.CancelUpdateCurrentRow();
                    }
                }
            }
            catch (Exception exception)
            {
                UiHelperClass.ShowError(exception.Message, exception);
            }

        }

        /// <summary>
        /// Handel the lists changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        void TestProtocolLists_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (FormStatus != FormStatusEnum.New)
                FormStatus = FormStatusEnum.Modified;

            ValidateForm();
        }

        /// <summary>
        /// Handel the object changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        void TestProtocol_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (TestProtocol.ObjectState)
            {
                case DomainEntityState.Modified:
                    FormStatus = FormStatusEnum.Modified;
                    break;
                case DomainEntityState.Deleted:
                    FormStatus = FormStatusEnum.Locked;
                    break;
                case DomainEntityState.Unchanged:
                    FormStatus = FormStatusEnum.Unchanged;
                    break;
            }
        }

        #endregion
    }
}
