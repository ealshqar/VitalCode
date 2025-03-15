using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraLayout.Utils;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.SpotChecks;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlSpotCheckResults : XtraUserControl
    {
        #region PrivateMemebers

        private BindingList<SpotCheckResult> _spotCheckResults;
        private bool _isInEditMode;

        #endregion

        #region Constructors

        public XtraUserControlSpotCheckResults()
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
        public event SpotCheckReseltDeletedEventHandler SpotCheckReseltDeleted;

        /// <summary>
        /// Event for BroadcastRequest.
        /// </summary>
        public event BroadcastRequestEventHandler BroadcastRequest;

        /// <summary>
        /// Invoke OnBroadcastRequest event.
        /// </summary>
        /// <param name="result"></param>
        public void OnBroadcastRequest(List<SpotCheckResult> result)
        {
            if (BroadcastRequest != null)
                BroadcastRequest(result);
        }

        /// <summary>
        /// Invoke SpotCheckReseltDeleted event.
        /// </summary>
        /// <param name="result"></param>
        public void OnSpotCheckReseltDeleted(SpotCheckResult result)
        {
            if (SpotCheckReseltDeleted != null)
                SpotCheckReseltDeleted(result);
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
        /// Invoke for ShowAutoTestDialog event.
        /// </summary>
        public void OnShowAutoTestDialog(SpotCheckResult result)
        {
            if (ShowAutoTestDialog == null || result == null)
                return;

            ShowAutoTestDialog(result);
            gridViewResults.Focus();
        }

        #endregion

        #region EventsHandlersDelegates

        public delegate void ShowAutoTestDialogEventHandler(SpotCheckResult result);

        public delegate void CellchangingEventHandler();

        public delegate void SpotCheckReseltDeletedEventHandler(SpotCheckResult result);

        public delegate void BroadcastRequestEventHandler(List<SpotCheckResult> results);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the spot check results list.
        /// </summary>
        public BindingList<SpotCheckResult> SpotCheckResults
        {
            get { return _spotCheckResults; }
            set
            {
                _spotCheckResults = value;
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
        /// Show or hide the ItemNameCoulmn.
        /// </summary>
        public bool ShowStarCoulmn
        {
            get { return gridColumnIsStarred.Visible; }
            set { gridColumnIsStarred.Visible = value; }
        }

        /// <summary>
        /// Show or hide the ItemNameCoulmn.
        /// </summary>
        public bool ShowItemNameCoulmn
        {
            get { return gridColumnIvType.Visible; }
            set { gridColumnIvType.Visible = value; }
        }

        /// <summary>
        /// Show or hide the NoOfBagsCoulmn.
        /// </summary>
        public bool ShowNoOfBagsCoulmn
        {
            get { return gridColumnNoOfBags.Visible; }
            set { gridColumnNoOfBags.Visible = value; }
        }

        /// <summary>
        /// Show or hide the NoPerWeekCoulmn.
        /// </summary>
        public bool ShowNoPerWeekCoulmn
        {
            get { return gridColumnNoPerWeek.Visible; }
            set { gridColumnNoPerWeek.Visible = value; }
        }

        /// <summary>
        /// Show or hide the DosagesCoulmn.
        /// </summary>
        public bool ShowDosagesCoulmn
        {
            get { return gridColumnDosages.Visible; }
            set { gridColumnDosages.Visible = value; }
        }

        /// <summary>
        /// Show or hide the NotesCoulmn.
        /// </summary>
        public bool ShowNotesCoulmn
        {
            get { return gridColumnNotes.Visible; }
            set { gridColumnNotes.Visible = value; }
        }

        /// <summary>
        /// Show or hide the MgPerMlCoulmn.
        /// </summary>
        public bool ShowMgPerMlCoulmn
        {
            get { return gridColumnMgPerMl.Visible; }
            set { gridColumnMgPerMl.Visible = value; }
        }

        /// <summary>
        /// Show or hide the DosageRange.
        /// </summary>
        public bool ShowDosageReangeCoulmn
        {
            get { return gridColumnDosageRange.Visible; }
            set { gridColumnDosageRange.Visible = value; }
        }

        /// <summary>
        /// Show or hide the NameCoulmnCaption.
        /// </summary>
        public string ItemNameCoulmnCaption
        {
            get { return gridColumnIvType.Caption; }
            set { gridColumnIvType.Caption = value; }
        }

        /// <summary>
        /// Gets or sets the NoOfBagsCoulmn Caption.
        /// </summary>
        public string NoOfBagsCoulmnCaption
        {
            get { return gridColumnNoOfBags.Caption; }
            set { gridColumnNoOfBags.Caption = value; }
        }

        /// <summary>
        /// Gets or sets the NoPerWeekCoulmn Caption.
        /// </summary>
        public string NoPerWeekCoulmnCaption
        {
            get { return gridColumnNoPerWeek.Caption; }
            set { gridColumnNoPerWeek.Caption = value; }
        }

        /// <summary>
        /// Gets or sets the DosagesCoulmn Caption.
        /// </summary>
        public string DosagesCoulmnCaption
        {
            get { return gridColumnDosages.Caption; }
            set { gridColumnDosages.Caption = value; }
        }

        /// <summary>
        /// Gets or sets the NotesCoulmn Caption.
        /// </summary>
        public string NotesCoulmnCaption
        {
            get { return gridColumnNotes.Caption; }
            set { gridColumnNotes.Caption = value; }
        }

        /// <summary>
        /// Gets or sets the MgPerMlCoulmn Caption.
        /// </summary>
        public string MgPerMlCoulmnCaption
        {
            get { return gridColumnMgPerMl.Caption; }
            set { gridColumnMgPerMl.Caption = value; }
        }

        /// <summary>
        ///  Gets or sets the DosageRange Caption.
        /// </summary>
        public string DosageReangeCoulmnCaption
        {
            get { return gridColumnDosageRange.Caption; }
            set { gridColumnDosageRange.Caption = value; }
        }

        /// <summary>
        /// Gets IsFocusedView.
        /// </summary>
        public bool IsFocusedView
        {
            get { return gridViewResults.IsFocusedView; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set binding.
        /// </summary>
        private void SetBinding()
        {
            if(SpotCheckResults == null)
                return;

            gridControlResults.DataBindings.Clear();
            gridControlResults.DataSource = SpotCheckResults;
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
        /// Gets the focused spot check results.
        /// </summary>
        /// <returns></returns>
        private SpotCheckResult GetFocusedResult()
        {
            return gridViewResults.GetFocusedRow() as SpotCheckResult;
        }

        /// <summary>
        /// Update the status of the buttons.
        /// </summary>
        private void UpdateButtonStatus()
        {
            var isSelected = gridViewResults.SelectedRowsCount > 0;
            var isMultiSelection = gridViewResults.SelectedRowsCount > 1;

            simpleButtonClearSelection.Enabled = _isInEditMode && isSelected;
            simpleButtonDelete.Enabled =  _isInEditMode && isSelected && !isMultiSelection;
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
        /// Delete the selected spot check result.
        /// </summary>
        public void DeleteActions()
        {
            var selectedResult = GetFocusedResult();

            if(selectedResult == null)
                return;

            selectedResult.ObjectState = DomainEntityState.Deleted;

            gridViewResults.DeleteRow(gridViewResults.FocusedRowHandle);

            OnSpotCheckReseltDeleted(selectedResult);

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
        /// Add passed spot check result to the results.
        /// </summary>
        public void AddResult(SpotCheckResult spotCheckResult)
        {
            SpotCheckResults.Add(spotCheckResult);
            gridViewResults.MoveLast();
            UpdateButtonStatus();
        }

        /// <summary>
        /// Gets the selected items.
        /// </summary>
        /// <returns>The items as binding List.</returns>
        public BindingList<SpotCheckResult> GetSelectedItems()
        {
            var selectedResults = new BindingList<SpotCheckResult>();

            if (gridViewResults.SelectedRowsCount > 0)
            {
                var selectedRowsIndex = gridViewResults.GetSelectedRows();

                foreach (var t in selectedRowsIndex)
                {
                    var item = gridViewResults.GetRow(t) as SpotCheckResult;

                    if (item == null) continue;

                    selectedResults.Add(item);
                }
            }

            return selectedResults;
        }

        /// <summary>
        /// Add passed spot check results to the results.
        /// </summary>
        public void AddResults(BindingList<SpotCheckResult> spotCheckResults)
        {
            foreach (var spotCheckResult in spotCheckResults)
            {
                AddResult(spotCheckResult);
            }

        }

        #endregion

        #region Handlers

        /// <summary>
        /// Clear user selection.
        /// </summary>
        private void simpleButtonClearSelection_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonClearSelection_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                gridViewResults.ClearSelection();
            }
        }

        /// <summary>
        /// Open Selected Result in the dosages dialog.
        /// </summary>
        private void simpleButtonOpen_Click(object sender, EventArgs e)
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
        /// Delete user selection.
        /// </summary>
        private void simpleButtonDelete_Click(object sender, EventArgs e)
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

        /// <summary>
        /// Handles the key down for the XtraUserControlSpotCheckResults.
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
                    if (simpleButtonDelete.Enabled)
                    {
                        DeleteActions();
                    }
                }
                else if (e.KeyCode == Keys.F8)
                {
                    if (simpleButtonClearSelection.Enabled)
                    {
                        ClearSelection();
                    }
                }
                else if(e.KeyCode == Keys.F4)
                {
                    gridViewResults.SelectAll();
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
                    Invoke(new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridViewResults_CellValueChanging), sender, e);
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
        /// Handles the opening of the spot check results context menu.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void contextMenuStripSpotCheckResult_Opening(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(contextMenuStripSpotCheckResult_Opening), sender, e);
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

                contextMenuStripSpotCheckResult.Enabled = isEnabled && _isInEditMode;
            }
        }

        /// <summary>
        /// Handles the click on the spot check results context menu.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments</param>
        private void simpleButtonGenerateSpotCheckResult__ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ToolStripItemClickedEventHandler(simpleButtonGenerateSpotCheckResult__ItemClicked), sender, e);
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

                if (e.ClickedItem == toolStripMenuItemDeleteSpotCheckResult)
                {
                    DeleteActions();
                }
                else if (e.ClickedItem == openToolStripMenuItemOpenSpotCheckResult)
                {
                    OnShowAutoTestDialog(GetFocusedResult());
                }
            }
        }

        #endregion

    }
}
