using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraLayout.Utils;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using System.Linq;
using Vital.UI.Enums;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections.Generic;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormVitalForce : XtraForm
    {
        #region Fields

        private bool _isGotVitalForce;
        private int _selectedRangeGroupIndex;

        private SettingsManager _settingsManager;
        private LookupsManager _lookupsManager;

        private Item _tempVitalForce;
        private List<int> _rangeGroup;

        private int _yesLookupId;
        private int _autoCloseAfter;
        private int _autoCloseAfterPassedTime;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the item per group value.
        /// </summary>
        private int ItemaPerGroup
        {
            get
            {
                return int.Parse(spinEditItemaPerGroup.EditValue.ToString());
            }
        }

        /// <summary>
        /// Gets or set the Vital Force Item.
        /// </summary>
        public Item VitalForce { get; set; }

        /// <summary>
        /// Get or set the Item Name.
        /// </summary>
        public string ItemName
        {
            get
            {
                return simpleLabelItemItemName.Text;
            }
            set
            {
                simpleLabelItemItemName.Text = value;
                simpleLabelItemItemName.OptionsToolTip.ToolTip = value;
            }
        }

        /// <summary>
        /// Gets or sets the Test Play State
        /// </summary>
        public TestPlayStateEnum TestPlayState { get; set; }

        #endregion

        #region Constructors

        public XtraFormVitalForce()
        {
            InitializeComponent();
            TestPlayState = TestPlayStateEnum.Paused;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Perform the initialization of the test issue
        /// </summary>
        private void PerformSpecificIntializationSteps()
        {
            CsaEmdUnitManager.Instance.ActivateConnection(Csa_Instance_Released, _csaManager_ReadingDone, _csaManager_MeterValueChanged);

            UiHelperClass.SetLayoutControlProperties(layoutControlVitalForce);

            _settingsManager = new SettingsManager();
            _lookupsManager = new LookupsManager();

            FillLocalLookupIds();
            SetupAutoCloseTimer();

            var fontSize = _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.FontSize) });

            gridViewVitalForce.Appearance.Row.Font = UiHelperClass.GetFontWithSize(float.Parse(fontSize.Value.ToString()));
        }

        /// <summary>
        /// Fill the local lookups ids.
        /// </summary>
        private void FillLocalLookupIds()
        {
            /*This code will cause an issue in the designer mode of the general test form. we need to check if the form is loaded
            or not before executing this */
            var yesLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.Yes));

            _yesLookupId = yesLookup != null ? yesLookup.Id : 0;
        }

        /// <summary>
        /// Setup the auto close timer and settings.
        /// </summary>
        private void SetupAutoCloseTimer()
        {
            if(VitalForce != null || TestPlayState == TestPlayStateEnum.Paused)
                return;

            var isAutoCloseOnSetting = ((BindingList<Setting>)CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings)).FirstOrDefault(s => s.Key == EnumNameResolver.Resolve(SettingKeys.AutoCloseDialogs));

            if (isAutoCloseOnSetting == null || Convert.ToInt32(isAutoCloseOnSetting.Value) != _yesLookupId)
                return;

            var autoCloseAfterSetting = ((BindingList<Setting>)CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings)).FirstOrDefault(s => s.Key == EnumNameResolver.Resolve(SettingKeys.AutoCloseDialogsTimeout));

            if(autoCloseAfterSetting == null)
                return;

            _autoCloseAfter = Convert.ToInt32(autoCloseAfterSetting.Value);
        }

        /// <summary>
        /// Start the auto close operation.
        /// </summary>
        private void StartAutoCloseTimer()
        {
            if (_autoCloseAfter == 0 || timerAutoClose.Enabled)
                return;

            _autoCloseAfterPassedTime = 0;
            timerAutoClose.Start();
        }

        /// <summary>
        /// Cancel auto close operation.
        /// </summary>
        private void CancelAutoCloseTimer()
        {
            if (_autoCloseAfter == 0 || !timerAutoClose.Enabled)
                return;

            labelControlTip.Text = StaticKeys.PressEnterToCloseForm;
            timerAutoClose.Stop();
            _autoCloseAfterPassedTime = 0;
        }

        /// <summary>
        /// Enables the Done button.
        /// </summary>
        private void EnableDoneButton()
        {
            labelControlTip.Visible = true;
            barButtonItemDone.Enabled = true;
        }

        /// <summary>
        /// Checks the current reading.
        /// </summary>
        /// <param name="value"></param>
        private void CheckReading(int value)
        {
            if (!CrossLayersSharedLogic.IsAcceptableReading(value))
            {
                _isGotVitalForce = false;
            }
            else
            {
                if (_rangeGroup != null || gridViewVitalForce.SelectedRowsCount == 1)
                {
                    _isGotVitalForce = true;
                }
                else
                {
                    _rangeGroup = gridViewVitalForce.GetSelectedRows().ToList();
                    _selectedRangeGroupIndex = -1;
                }
            }

        }

        /// <summary>
        /// Move up in the grid
        /// </summary>
        private void MoveUp()
        {
            var curentRowHandler = gridViewVitalForce.GetSelectedRows().LastOrDefault();
            var nextRowHandle = gridViewVitalForce.IsFirstRow ? gridViewVitalForce.DataRowCount - 1 : curentRowHandler - 1;
            gridViewVitalForce.ClearSelection();
            gridViewVitalForce.SelectRow(nextRowHandle);
            gridViewVitalForce.FocusedRowHandle = nextRowHandle;
            gridViewVitalForce.MakeRowVisible(nextRowHandle);
            gridViewVitalForce.Focus();
        }

        /// <summary>
        /// Move down in the grid
        /// </summary>
        private void MoveDown()
        {
            var curentRowHandler = gridViewVitalForce.GetSelectedRows().LastOrDefault();
            var nextRowHandle = gridViewVitalForce.IsLastRow ? 0 : curentRowHandler + 1;
            gridViewVitalForce.ClearSelection();
            gridViewVitalForce.SelectRow(nextRowHandle);
            gridViewVitalForce.FocusedRowHandle = nextRowHandle;
            gridViewVitalForce.MakeRowVisible(nextRowHandle);
            gridViewVitalForce.Focus();
        }

        /// <summary>
        /// Performs logic of the done action
        /// </summary>
        private void DoneAction()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Performs the cancel action
        /// </summary>
        private void CancelAction()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Select the next group of VF.
        /// </summary>
        private void SelectNextGroup(bool isFirstCall = false)
        {
            var lastSelectedRowIndex = _rangeGroup == null ? gridViewVitalForce.GetSelectedRows().LastOrDefault() : _rangeGroup.LastOrDefault();

            CleanGroupFlags(false);

            var firstRowIndexInNextGroup = isFirstCall ? lastSelectedRowIndex : lastSelectedRowIndex == gridViewVitalForce.DataRowCount - 1 ? 0 : lastSelectedRowIndex + 1;
            var lastRowIndexInNextGroup = firstRowIndexInNextGroup + ItemaPerGroup > gridViewVitalForce.DataRowCount - 1 ? gridViewVitalForce.DataRowCount - 1 : firstRowIndexInNextGroup + ItemaPerGroup - 1;
            gridViewVitalForce.ClearSelection();

            gridViewVitalForce.SelectRows(firstRowIndexInNextGroup, lastRowIndexInNextGroup);
            gridViewVitalForce.FocusedRowHandle = firstRowIndexInNextGroup;
            gridViewVitalForce.MakeRowVisible(lastRowIndexInNextGroup);
            gridViewVitalForce.MakeRowVisible(firstRowIndexInNextGroup);
        }

        /// <summary>
        /// Update the selected group of VF.
        /// </summary>
        private void UpdateSelectedGroup()
        {
            CleanGroupFlags(false);

            var selectedRows = gridViewVitalForce.GetSelectedRows().ToList();

            //Check if can update the selection or not.
            //If the selection is not in series, application cannot update the selection.  
            var lastCheckedIndex = selectedRows.FirstOrDefault();
            foreach (var selectedRow in selectedRows)
            {
                var defrence = lastCheckedIndex - selectedRow;
                lastCheckedIndex = selectedRow;
                if (defrence < -1 || defrence > 1)
                {
                    return;
                }

            }

            if (selectedRows.Count == 0) return;

            if (selectedRows.Count > ItemaPerGroup)
            {
                gridViewVitalForce.ClearSelection();
                gridViewVitalForce.SelectRows(selectedRows[0], selectedRows[ItemaPerGroup == 1 ? 0 : ItemaPerGroup - 1]);
            }
            else
            {
                var lastRowToSelect = selectedRows.LastOrDefault() + (ItemaPerGroup - selectedRows.Count);
                gridViewVitalForce.SelectRows(selectedRows[0], lastRowToSelect > gridViewVitalForce.RowCount - 1 ? gridViewVitalForce.RowCount - 1 : lastRowToSelect);
            }

        }

        /// <summary>
        /// Select the previous group of VF.
        /// </summary>
        private void SelectPreviousGroup()
        {
            CleanGroupFlags(false);

            var firstSelectedRowIndex = gridViewVitalForce.GetSelectedRows().FirstOrDefault();

            var firstRowIndexInGroup = (firstSelectedRowIndex == 0 ? gridViewVitalForce.DataRowCount : firstSelectedRowIndex ) - ItemaPerGroup;

            if (firstRowIndexInGroup < 0) 
                firstRowIndexInGroup = 0;

            var lastRowIndexInNextGroup = firstSelectedRowIndex > 0 ? firstSelectedRowIndex - 1 : gridViewVitalForce.DataRowCount - 1;

            gridViewVitalForce.ClearSelection();

            gridViewVitalForce.SelectRows(firstRowIndexInGroup, lastRowIndexInNextGroup);
            gridViewVitalForce.FocusedRowHandle = firstRowIndexInGroup;
            gridViewVitalForce.MakeRowVisible(lastRowIndexInNextGroup);
            gridViewVitalForce.MakeRowVisible(firstRowIndexInGroup);            
        }

        #endregion

        #region HW_Logic

        /// <summary>
        /// Start the reading.
        /// </summary>
        private void StartReading()
        {
            CsaEmdUnitManager.Instance.Clear();

            ResetHardwareHandlers();

            CsaEmdUnitManager.Instance.StartReading();
        }

        /// <summary>
        /// Reset the hardware handlers.
        /// </summary>
        private void ResetHardwareHandlers()
        {
            RemoveHardwareHandlers();
            AddHardwareHandlers();
        }

        /// <summary>
        /// Remove the hardware handlers.
        /// </summary>
        private void RemoveHardwareHandlers()
        {
            CsaEmdUnitManager.Instance.MeterValueChanged -= _csaManager_MeterValueChanged;
            CsaEmdUnitManager.Instance.ReadingDone -= _csaManager_ReadingDone;
        }

        /// <summary>
        /// Add the hardware handlers.
        /// </summary>
        private void AddHardwareHandlers()
        {
            CsaEmdUnitManager.Instance.MeterValueChanged += _csaManager_MeterValueChanged;
            CsaEmdUnitManager.Instance.ReadingDone += _csaManager_ReadingDone;
        }

        /// <summary>
        /// Stop the reading.
        /// </summary>
        private void StopReading()
        {
            RemoveHardwareHandlers();

            CsaEmdUnitManager.Instance.StopReading();
        }

        /// <summary>
        /// Set Binging for the controls.
        /// </summary>
        private void SetBinding()
        {
            gridControlVitalForce.DataSource = CacheHelper.SetOrGetCachableData(CachableDataEnum.VitalForce);

            gridControlVitalForce.ForceInitialize();

            if(TestPlayState == TestPlayStateEnum.Playing)
            {
                layoutControlItemItemsPerGroup.Visibility = LayoutVisibility.Always;

                spinEditItemaPerGroup.Properties.MinValue = 1;

                spinEditItemaPerGroup.Properties.MaxValue = gridViewVitalForce.RowCount - 1;

                spinEditItemaPerGroup.EditValue = CsaEmdUnitManager.Instance.NumberOfVitalForceItemsToJump;

                spinEditItemaPerGroup.Refresh();

                spinEditItemaPerGroup.EditValueChanged += spinEditItemaPerGroup_EditValueChanged;
            }
            else
            {
                layoutControlItemItemsPerGroup.Visibility = LayoutVisibility.Never;
            }

            if (VitalForce != null)
            {
                for (var i = 0; i < gridViewVitalForce.DataRowCount; i++)
                {
                    var item = gridViewVitalForce.GetRow(i) as Item;

                    if (item != null && item.Id != VitalForce.Id) continue;

                    gridViewVitalForce.ClearSelection();
                    gridViewVitalForce.SelectRow(i);
                    gridViewVitalForce.FocusedRowHandle = i;
                    gridViewVitalForce.MakeRowVisible(i);

                    break;
                }
            }
            else
            {
                if (TestPlayState == TestPlayStateEnum.Playing)
                {
                    SelectNextGroup(true);
                }
                else
                {
                    gridViewVitalForce.SelectRow(0);
                }

                _tempVitalForce = gridViewVitalForce.SelectedRowsCount > 0 ? null : gridViewVitalForce.GetFocusedRow() as Item;

            }

            gridControlVitalForce.Focus();
            gridViewVitalForce.Focus();

            xtraUserControlReadingGaugeVitalForce.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);
        }

        /// <summary>
        /// Move the force grid selection.
        /// </summary>
        private void MoveSelection()
        {
            var currentSelection = _rangeGroup != null ? _rangeGroup.IndexOf(gridViewVitalForce.GetSelectedRows().LastOrDefault()) : -1;

            if (currentSelection != -1)
            {
                if (_rangeGroup != null && (_selectedRangeGroupIndex == -1 && currentSelection == _rangeGroup.Count - 1))
                {
                    _selectedRangeGroupIndex = currentSelection;
                }
                else
                {
                    _selectedRangeGroupIndex = currentSelection - 1;
                }
            }

            if (_rangeGroup != null && (currentSelection != -1 && _selectedRangeGroupIndex >= 0 && _selectedRangeGroupIndex < _rangeGroup.Count()))
            {
                gridViewVitalForce.ClearSelection();
                gridViewVitalForce.SelectRow(_rangeGroup[_selectedRangeGroupIndex]);
                gridViewVitalForce.FocusedRowHandle = _rangeGroup[_selectedRangeGroupIndex];
                
            }
            else
            {
                SelectNextGroup();
            }

        }

        private void CleanGroupFlags(bool isGotReading)
        {
            _rangeGroup = null;
            _isGotVitalForce = isGotReading;
            _selectedRangeGroupIndex = -1;
        }

        #endregion

        #region Events

        #region HW_Events

        /// <summary>
        /// Handel the meter value changed.
        /// </summary>
        void _csaManager_ReadingDone(object sender, int reading, int min, int max, int fall, int rise)
        {
            _csaManager_MeterValueChanged(sender, reading, min, max);

            if (InvokeRequired)
            {
                Invoke(new CsaEmdUnitManager.OnReadingDone(_csaManager_ReadingDone), sender, reading, min, max, fall, rise);
            }
            else
            {
                if (!CsaEmdUnitManager.Instance.HasReading) return;

                StopReading();

                if (TestPlayState == TestPlayStateEnum.Playing)
                    CheckReading(reading);

                xtraUserControlReadingGaugeVitalForce.SetReadingStatusBarMode(TestBarStateEnum.WaitingToRelease, string.Empty, 0);
            }

        }

        /// <summary>
        /// Handel the reading done event.
        /// </summary>
        void _csaManager_MeterValueChanged(object sender, int reading, int min, int max)
        {
            if (InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.MeterValueChangedHandle(_csaManager_MeterValueChanged), sender,
                           reading, min, max);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                CancelAutoCloseTimer();
                xtraUserControlReadingGaugeVitalForce.ReadingValue = reading;
                xtraUserControlReadingGaugeVitalForce.SetReadingStatusBarMode(TestBarStateEnum.Reading, string.Empty, 0);
            }

        }

        /// <summary>
        /// Handel the CSA released  event.
        /// </summary>
        /// <param name="sender">The sender as CSA manager.</param>
        void Csa_Instance_Released(object sender)
        {
            if (InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.OnReleased(Csa_Instance_Released), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {

                if (!CsaEmdUnitManager.Instance.HasReading) return;

                if (TestPlayState == TestPlayStateEnum.Playing)
                {
                    if (_rangeGroup != null && !_isGotVitalForce)
                    {
                        MoveSelection();
                    }
                    else if (_isGotVitalForce)
                    {
                        _tempVitalForce = gridViewVitalForce.GetFocusedRow() as Item;
                        xtraUserControlReadingGaugeVitalForce.SetReadingStatusBarMode(TestBarStateEnum.Ready,
                                                                                      string.Empty, 0);
                        CleanGroupFlags(true);
                        gridControlVitalForce.Refresh();

                        StartAutoCloseTimer();

                        return;
                    }
                    else
                    {
                        MoveSelection();
                    }
                }

                StartReading();

                _isGotVitalForce = false;

                xtraUserControlReadingGaugeVitalForce.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);
            }
        }


        #endregion

        #region UI_Events

        /// <summary>
        /// Handel the Loading event for the form.
        /// </summary>
        private void XtraFormVitalForce_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(XtraFormVitalForce_Load), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingData);
                PerformSpecificIntializationSteps();
                SetBinding();
                UiHelperClass.HideSplash();
            }
        }

        /// <summary>
        /// Close the form by done or cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barManager_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ItemClickEventHandler(barManager_ItemClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (string.IsNullOrEmpty(e.Item.Name)) return;

                if (e.Item == barButtonItemDone)
                {
                    DoneAction();
                }
                else if (e.Item == barButtonItemCancel)
                {
                    CancelAction();
                }
            }
        }

        /// <summary>
        /// Move Up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPreviousGroup_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonPreviousGroup_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (TestPlayState == TestPlayStateEnum.Playing)
                {
                    SelectPreviousGroup();
                }
                else
                {
                    MoveUp();
                }
            }
        }

        /// <summary>
        /// Move down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonNextGroup_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonNextGroup_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (TestPlayState == TestPlayStateEnum.Playing)
                {
                    SelectNextGroup();
                }
                else
                {
                    MoveDown();
                }
            }            
        }

        /// <summary>
        /// Handel the shown of the form (Starts the reading).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormVitalForce_Shown(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(XtraFormVitalForce_Shown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                StartReading();
                gridViewVitalForce.Focus();
            }
        }

        /// <summary>
        /// Handel the form closing to stop the reading and remove the events handlers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormVitalForce_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FormClosingEventHandler(XtraFormVitalForce_FormClosing), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (_tempVitalForce == null)
                {
                    _tempVitalForce = gridViewVitalForce.SelectedRowsCount > 0 ?
                                    gridViewVitalForce.GetRow(gridViewVitalForce.GetSelectedRows()[0]) as Item :
                                                        gridViewVitalForce.GetFocusedRow() as Item;
                }

                VitalForce = _tempVitalForce;

                CsaEmdUnitManager.Instance.DisposeConnection(Csa_Instance_Released, _csaManager_ReadingDone, _csaManager_MeterValueChanged);
            }
        }

        /// <summary>
        /// Sets the value of the vital force when focused row changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewVitalForce_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FocusedRowChangedEventHandler(gridViewVitalForce_FocusedRowChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                xtraUserControlReadingGaugeVitalForce.Clear();

                CancelAutoCloseTimer();

                StopReading();
                StartReading();

                EnableDoneButton();

                xtraUserControlReadingGaugeVitalForce.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);
            }            
        }

        /// <summary>
        /// Handles the key down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormVitalForce_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(XtraFormVitalForce_KeyDown), sender, e);
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
                    if (!spinEditItemaPerGroup.IsEditorActive)
                    {
                        DoneAction();
                    }
                    else
                    {
                        gridViewVitalForce.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    CancelAction();
                }
            }
        }

        /// <summary>
        /// Handles the Up and down keys which only work in the KeyUp event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormVitalForce_KeyUp(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(XtraFormVitalForce_KeyUp), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.Control && e.KeyCode == Keys.Up)
                {
                    gridControlVitalForce.Focus();
                    SelectPreviousGroup();
                }
                else if (e.Control && e.KeyCode == Keys.Down)
                {
                    gridControlVitalForce.Focus();
                    SelectNextGroup();
                }
            }
        }

        /// <summary>
        /// Handel the custom draw of the rows.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewVitalForce_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new RowCellCustomDrawEventHandler(gridViewVitalForce_CustomDrawCell), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                var gridView = sender as GridView;

                if (gridView == null) return;

                if (gridView.SelectedRowsCount == 1 && _rangeGroup != null && _rangeGroup.Count(r => r == e.RowHandle) > 0)
                {
                    e.Appearance.BackColor = gridView.FocusedRowHandle == e.RowHandle ? Color.Orange : Color.YellowGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
                else
                {
                    e.Appearance.BackColor = e.Appearance.ForeColor = Color.Empty;
                }
            }
        }

        /// <summary>
        /// Handel the selection changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewVitalForce_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new SelectionChangedEventHandler(gridViewVitalForce_SelectionChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                _tempVitalForce = gridViewVitalForce.SelectedRowsCount > 1 ? null : gridViewVitalForce.GetFocusedRow() as Item;

                if (gridViewVitalForce.SelectedRowsCount == 0 || (gridViewVitalForce.SelectedRowsCount == 1 && (_rangeGroup != null && _rangeGroup.IndexOf(gridViewVitalForce.GetSelectedRows().FirstOrDefault()) != -1)))
                    return;


                CleanGroupFlags(false);
                gridControlVitalForce.Refresh();
            }
        }

        /// <summary>
        /// Handel the edit value changed for the number of items inside each group on click in the spin arrows.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spinEditItemaPerGroup_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new SpinEventHandler(spinEditItemaPerGroup_Spin), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                CancelAutoCloseTimer();

                if (!e.IsSpinUp) return;

                if (spinEditItemaPerGroup.Value == spinEditItemaPerGroup.Properties.MinValue)
                {
                    spinEditItemaPerGroup.EditValue = 0;
                }

            }
        }

        /// <summary>
        /// Handel the edit value changed for the number of items inside each group 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spinEditItemaPerGroup_EditValueChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(spinEditItemaPerGroup_EditValueChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                UpdateSelectedGroup();
            }
        }

        /// <summary>
        /// Handle the timer tick for auto close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerAutoClose_Tick(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(timerAutoClose_Tick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                _autoCloseAfterPassedTime += timerAutoClose.Interval;

                var closeAfter = (_autoCloseAfter - _autoCloseAfterPassedTime)/1000f;

                labelControlTip.Text = string.Format(StaticKeys.FormWillColseAfterMessage, closeAfter.ToString("0.0"));

                if(_autoCloseAfterPassedTime >= _autoCloseAfter)
                {
                    timerAutoClose.Stop();
                    DoneAction();
                }
            }
        }

        #endregion        

        #endregion        
    }
}