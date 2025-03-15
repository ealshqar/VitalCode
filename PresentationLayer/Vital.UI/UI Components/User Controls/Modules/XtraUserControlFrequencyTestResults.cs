using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.FrequencyTests;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlFrequencyTestResults : XtraUserControl
    {
        #region PrivateMemebers

        private BindingList<FrequencyTestResult> _frequencyTestResults;
        private bool _isInEditMode;

        #endregion

        #region Constructors

        public XtraUserControlFrequencyTestResults()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// Event for ShowAutoTestDialog.
        /// </summary>
        public event ShowAutoTestDialogEventHandler ShowAutoTestDialog;

        /// <summary>
        /// Event for CellChanging.
        /// </summary>
        public event CellchangingEventHandler CellChanging;

        /// <summary>
        /// Event for CellChanging.
        /// </summary>
        public event FrequencyTestReseltDeletedEventHandler FrequencyTestReseltDeleted;

        /// <summary>
        /// Event for BroadcastRequest.
        /// </summary>
        public event BroadcastRequestEventHandler BroadcastRequest;

        /// <summary>
        /// Invoke OnBroadcastRequest event.
        /// </summary>
        /// <param name="result"></param>
        public void OnBroadcastRequest(List<FrequencyTestResult> result)
        {
            if (BroadcastRequest != null)
                BroadcastRequest(result);
        }

        /// <summary>
        /// Invoke FrequencyTestReseltDeleted event.
        /// </summary>
        /// <param name="result"></param>
        public void OnFrequencyTestReseltDeleted(FrequencyTestResult result)
        {
            if (FrequencyTestReseltDeleted != null)
                FrequencyTestReseltDeleted(result);
        }

        /// <summary>
        /// Invoke OnCellChanging event. 
        /// </summary>
        public void OnCellChanging()
        {
            if (CellChanging != null) 
                CellChanging();
        }

        /// <summary>
        /// Handel the selected Item changed.
        /// </summary>
        public event OnSelectedItemChanged SelectedItemChanged;

        /// <summary>
        /// Invoke for ShowAutoTestDialog event.
        /// </summary>
        public void OnShowAutoTestDialog(FrequencyTestResult result)
        {
            if (ShowAutoTestDialog == null || result == null)
                return;

            ShowAutoTestDialog(result);
            gridViewResults.Focus();
        }

        #endregion

        #region Event Invokers

        /// <summary>
        /// Invokes the selected item changed.
        /// </summary>
        /// <param name="item">The item.</param>
        public void InvokeSelectedItemChanged(Item item)
        {
            if (SelectedItemChanged == null)
                return;

            SelectedItemChanged(item);
        }

        #endregion

        #region EventsHandlersDelegates

        public delegate void CellchangingEventHandler();

        public delegate void FrequencyTestReseltDeletedEventHandler(FrequencyTestResult result);

        public delegate void BroadcastRequestEventHandler(List<FrequencyTestResult> results);

        public delegate void ShowAutoTestDialogEventHandler(FrequencyTestResult result);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the frequency test results list.
        /// </summary>
        public BindingList<FrequencyTestResult> FrequencyTestResults
        {
            get { return _frequencyTestResults; }
            set
            {
                _frequencyTestResults = value;
                SetBinding();
            }
        }

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        public System.Drawing.Font GridFont
        {
            set { gridViewResults.Appearance.Row.Font = value; }
        }

        /// <summary>
        /// Gets IsFocusedView.
        /// </summary>
        public bool IsFocusedView
        {
            get { return gridViewResults.IsFocusedView; }
        }

        public GridView View
        {
            get
            {
                return gridViewResults;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set binding.
        /// </summary>
        private void SetBinding()
        {
            if(FrequencyTestResults == null)
                return;

            gridControlResults.DataBindings.Clear();
            gridControlResults.DataSource = FrequencyTestResults;
            ClearSelection();

            UpdateButtonStatus();
            gridViewResults.MoveFirst();
        }

        /// <summary>
        /// Set the edit mode.
        /// </summary>
        /// <param name="isReadonly"></param>
        public void SetEditMode(bool isReadonly)
        {
            _isInEditMode = !isReadonly;
            UpdateButtonStatus();
            gridViewResults.OptionsBehavior.ReadOnly = isReadonly;
        }

        /// <summary>
        /// Gets the focused frequency test results.
        /// </summary>
        /// <returns></returns>
        private FrequencyTestResult GetFocusedResult()
        {
            return gridViewResults.GetFocusedRow() as FrequencyTestResult;
        }

        /// <summary>
        /// Update the status of the buttons.
        /// </summary>
        private void UpdateButtonStatus()
        {
            var isSelected = gridViewResults.SelectedRowsCount > 0;
            var isMultiSelection = gridViewResults.SelectedRowsCount > 1;

            simpleButtonDelete.Enabled = _isInEditMode && isSelected && !isMultiSelection;
            simpleButtonOpen.Enabled = _isInEditMode && isSelected && !isMultiSelection;
        }

        /// <summary>
        /// Select All.
        /// </summary>
        public void SelectAll()
        {
            gridViewResults.SelectAll();
            gridViewResults.MakeRowVisible(0);
            gridViewResults.FocusedRowHandle = 0;
        }

        /// <summary>
        /// Clean the user selection.
        /// </summary>
        public void ClearSelection()
        {
            gridControlResults.ForceInitialize();
            gridViewResults.ClearSelection();
            gridControlResults.Update();
            gridViewResults.Focus();
        }

        /// <summary>
        /// Delete the selected frequency test result.
        /// </summary>
        public void DeleteActions()
        {
            var selectedResult = GetFocusedResult();

            if(selectedResult == null)
                return;

            selectedResult.ObjectState = DomainEntityState.Deleted;

            gridViewResults.DeleteRow(gridViewResults.FocusedRowHandle);

            OnFrequencyTestReseltDeleted(selectedResult);

            gridViewResults.SelectRow(gridViewResults.FocusedRowHandle);

            UpdateButtonStatus();

            gridViewResults.Focus();
        }

        /// <summary>
        /// Post editors.
        /// </summary>
        public void PostValues()
        {
            gridViewResults.PostEditor();
            gridViewResults.ValidateEditor();
            gridViewResults.UpdateCurrentRow();
        }

        /// <summary>
        /// Add passed frequency test result to the results.
        /// </summary>
        public void AddResult(FrequencyTestResult frequencyTestResult)
        {
            FrequencyTestResults.Add(frequencyTestResult);
            gridViewResults.MoveLast();
            UpdateButtonStatus();
        }

        /// <summary>
        /// Gets the selected items.
        /// </summary>
        /// <returns>The items as binding List.</returns>
        public BindingList<FrequencyTestResult> GetSelectedItems()
        {
            var selectedResults = new BindingList<FrequencyTestResult>();

            if (gridViewResults.SelectedRowsCount > 0)
            {
                var selectedRowsIndex = gridViewResults.GetSelectedRows();

                foreach (var t in selectedRowsIndex)
                {
                    var item = gridViewResults.GetRow(t) as FrequencyTestResult;

                    if (item == null) continue;

                    selectedResults.Add(item);
                }
            }

            return selectedResults;
        }

        /// <summary>
        /// Add passed frequency test results to the results.
        /// </summary>
        public void AddResults(BindingList<FrequencyTestResult> frequencyTestResults)
        {
            foreach (var frequencyTestResult in frequencyTestResults)
            {
                AddResult(frequencyTestResult);
            }

        }

        /// <summary>
        /// Refresh the grid data source.
        /// </summary>
        public void RefreshResultsGrid()
        {
            gridViewResults.RefreshData();
            gridControlResults.RefreshDataSource();
            gridViewResults.RefreshRow(gridViewResults.FocusedRowHandle);
            gridControlResults.Focus();
            gridViewResults.Focus();
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handles the key down for the XtraUserControlFrequencyTestResults.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridControlResults_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(gridControlResults_KeyDown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (simpleButtonOpen.Enabled)
                    {
                        OnShowAutoTestDialog(GetFocusedResult());
                    }
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    if (_isInEditMode)
                    {
                        DeleteActions();
                    }
                }
            }
        }

        /// <summary>
        /// Handel the selection changed for the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new SelectionChangedEventHandler(gridViewResults_SelectionChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                UpdateButtonStatus();
                OnBroadcastRequest(GetSelectedItems().ToList());

                if (gridViewResults.SelectedRowsCount == 1)
                {
                    var testResult = gridViewResults.GetRow(gridViewResults.GetSelectedRows().FirstOrDefault()) as FrequencyTestResult;

                    if (testResult == null)
                        return;

                    InvokeSelectedItemChanged(testResult.Item);
                }
            }
        }

        /// <summary>
        /// Handle the cell value changing to change the form status.
        /// </summary>
        private void gridViewResults_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CellValueChangedEventHandler(gridViewResults_CellValueChanging), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                OnCellChanging();
            }
        }

        /// <summary>
        /// Handles the opening of the frequency test results context menu.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void contextMenuStripFrequencyTestResult_Opening(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(contextMenuStripFrequencyTestResult_Opening), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == null) return;

                e.Cancel = UiHelperClass.CancelClickAction(gridViewResults);

                var isEnabled = UiHelperClass.IsClickInRowByMouse(gridViewResults);

                contextMenuStripFrequencyTestResult.Enabled = isEnabled && _isInEditMode;
            }
        }

        /// <summary>
        /// Handles the click on the frequency test results context menu.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments</param>
        private void simpleButtonGenerateFrequencyTestResult__ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ToolStripItemClickedEventHandler(simpleButtonGenerateFrequencyTestResult__ItemClicked), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == null) return;

                ((ContextMenuStrip)sender).Hide();

                if (e.ClickedItem == toolStripMenuItemDeleteFrequencyTestResult)
                {
                    DeleteActions();
                }
            }
        }

        /// <summary>
        /// Handle focused row changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewResults_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FocusedRowChangedEventHandler(gridViewResults_FocusedRowChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (gridControlResults.Focused)
                {
                    var testResult = gridViewResults.GetRow(gridViewResults.GetSelectedRows().FirstOrDefault()) as FrequencyTestResult;

                    if (testResult == null)
                        return;

                    InvokeSelectedItemChanged(testResult.Item);
                }
            }
        }

        /// <summary>
        /// simpleButtonOpen_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonOpen_Click(object sender, System.EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonOpen_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                OnShowAutoTestDialog(GetFocusedResult());
            }
        }

        /// <summary>
        /// simpleButtonDelete_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonDelete_Click(object sender, System.EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonDelete_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                DeleteActions();
            }
        }

        #endregion

        #region Delegates

        /// <summary>
        /// Delegate for SelectedItemChanged handler method.
        /// </summary>
        /// <param name="item">The item.</param>
        public delegate void OnSelectedItemChanged(Item item);

        #endregion
    }
}
