using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
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
    public partial class XtraFormRatios : XtraForm
    {
        #region Fields

        private bool _isGotRatioItem;
        private int _selectedRangeGroupIndex;

        private SettingsManager _settingsManager;

        private Item _tempRatioItem;
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
                return 1;
            }
        }

        /// <summary>
        /// Gets or set the RatioItem.
        /// </summary>
        public Item RatioItem { get; set; }

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

        public XtraFormRatios()
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

            UiHelperClass.SetLayoutControlProperties(layoutControlRatio);

            _settingsManager = new SettingsManager();

            FillLocalLookupIds();
            SetupAutoCloseTimer();

            var fontSize = _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.FontSize) });

            gridViewRatio.Appearance.Row.Font = UiHelperClass.GetFontWithSize(float.Parse(fontSize.Value.ToString()));
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
            if (RatioItem != null || TestPlayState == TestPlayStateEnum.Paused)
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
                _isGotRatioItem = false;
            }
            else
            {
                if (_rangeGroup != null || gridViewRatio.SelectedRowsCount == 1)
                {
                    _isGotRatioItem = true;
                }
                else
                {
                    _rangeGroup = gridViewRatio.GetSelectedRows().ToList();
                    _selectedRangeGroupIndex = -1;
                }
            }

        }

        /// <summary>
        /// Move up in the grid
        /// </summary>
        private void MoveUp()
        {
            var curentRowHandler = gridViewRatio.GetSelectedRows().LastOrDefault();
            var nextRowHandle = gridViewRatio.IsFirstRow ? gridViewRatio.DataRowCount - 1 : curentRowHandler - 1;
            gridViewRatio.ClearSelection();
            gridViewRatio.SelectRow(nextRowHandle);
            gridViewRatio.FocusedRowHandle = nextRowHandle;
            gridViewRatio.MakeRowVisible(nextRowHandle);
            gridViewRatio.Focus();
        }

        /// <summary>
        /// Move down in the grid
        /// </summary>
        private void MoveDown()
        {
            var curentRowHandler = gridViewRatio.GetSelectedRows().LastOrDefault();
            var nextRowHandle = gridViewRatio.IsLastRow ? 0 : curentRowHandler + 1;
            gridViewRatio.ClearSelection();
            gridViewRatio.SelectRow(nextRowHandle);
            gridViewRatio.FocusedRowHandle = nextRowHandle;
            gridViewRatio.MakeRowVisible(nextRowHandle);
            gridViewRatio.Focus();
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
            var lastSelectedRowIndex = _rangeGroup == null ? gridViewRatio.GetSelectedRows().LastOrDefault() : _rangeGroup.LastOrDefault();

            CleanGroupFlags(false);

            var firstRowIndexInNextGroup = isFirstCall ? lastSelectedRowIndex : lastSelectedRowIndex == gridViewRatio.DataRowCount - 1 ? 0 : lastSelectedRowIndex + 1;
            var lastRowIndexInNextGroup = firstRowIndexInNextGroup + ItemaPerGroup > gridViewRatio.DataRowCount - 1 ? gridViewRatio.DataRowCount - 1 : firstRowIndexInNextGroup + ItemaPerGroup - 1;
            gridViewRatio.ClearSelection();

            gridViewRatio.SelectRows(firstRowIndexInNextGroup, lastRowIndexInNextGroup);
            gridViewRatio.FocusedRowHandle = firstRowIndexInNextGroup;
            gridViewRatio.MakeRowVisible(lastRowIndexInNextGroup);
            gridViewRatio.MakeRowVisible(firstRowIndexInNextGroup);
        }

        /// <summary>
        /// Select the previous group of VF.
        /// </summary>
        private void SelectPreviousGroup()
        {
            CleanGroupFlags(false);

            var firstSelectedRowIndex = gridViewRatio.GetSelectedRows().FirstOrDefault();

            var firstRowIndexInGroup = (firstSelectedRowIndex == 0 ? gridViewRatio.DataRowCount : firstSelectedRowIndex ) - ItemaPerGroup;

            if (firstRowIndexInGroup < 0) 
                firstRowIndexInGroup = 0;

            var lastRowIndexInNextGroup = firstSelectedRowIndex > 0 ? firstSelectedRowIndex - 1 : gridViewRatio.DataRowCount - 1;

            gridViewRatio.ClearSelection();

            gridViewRatio.SelectRows(firstRowIndexInGroup, lastRowIndexInNextGroup);
            gridViewRatio.FocusedRowHandle = firstRowIndexInGroup;
            gridViewRatio.MakeRowVisible(lastRowIndexInNextGroup);
            gridViewRatio.MakeRowVisible(firstRowIndexInGroup);            
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
            gridControlRatio.DataSource = CacheHelper.SetOrGetCachableData(CachableDataEnum.Ratios);

            gridControlRatio.ForceInitialize();

            if (RatioItem != null)
            {
                for (var i = 0; i < gridViewRatio.DataRowCount; i++)
                {
                    var item = gridViewRatio.GetRow(i) as Item;

                    if (item != null && item.Id != RatioItem.Id) continue;

                    gridViewRatio.ClearSelection();
                    gridViewRatio.SelectRow(i);
                    gridViewRatio.FocusedRowHandle = i;
                    gridViewRatio.MakeRowVisible(i);

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
                    gridViewRatio.SelectRow(0);
                }

                _tempRatioItem = gridViewRatio.SelectedRowsCount > 0 ? null : gridViewRatio.GetFocusedRow() as Item;

            }

            gridControlRatio.Focus();
            gridViewRatio.Focus();

            xtraUserControlReadingGaugeRatio.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);
        }

        /// <summary>
        /// Move the force grid selection.
        /// </summary>
        private void MoveSelection()
        {
            var currentSelection = _rangeGroup != null ? _rangeGroup.IndexOf(gridViewRatio.GetSelectedRows().LastOrDefault()) : -1;

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
                gridViewRatio.ClearSelection();
                gridViewRatio.SelectRow(_rangeGroup[_selectedRangeGroupIndex]);
                gridViewRatio.FocusedRowHandle = _rangeGroup[_selectedRangeGroupIndex];
                
            }
            else
            {
                SelectNextGroup();
            }

        }

        private void CleanGroupFlags(bool isGotReading)
        {
            _rangeGroup = null;
            _isGotRatioItem = isGotReading;
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

                xtraUserControlReadingGaugeRatio.SetReadingStatusBarMode(TestBarStateEnum.WaitingToRelease, string.Empty, 0);
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
                xtraUserControlReadingGaugeRatio.ReadingValue = reading;
                xtraUserControlReadingGaugeRatio.SetReadingStatusBarMode(TestBarStateEnum.Reading, string.Empty, 0);
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
                    if (_rangeGroup != null && !_isGotRatioItem)
                    {
                        MoveSelection();
                    }
                    else if (_isGotRatioItem)
                    {
                        _tempRatioItem = gridViewRatio.GetFocusedRow() as Item;
                        xtraUserControlReadingGaugeRatio.SetReadingStatusBarMode(TestBarStateEnum.Ready,
                                                                                      string.Empty, 0);
                        CleanGroupFlags(true);
                        gridControlRatio.Refresh();

                        StartAutoCloseTimer();

                        return;
                    }
                    else
                    {
                        MoveSelection();
                    }
                }

                StartReading();

                _isGotRatioItem = false;

                xtraUserControlReadingGaugeRatio.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);
            }
        }


        #endregion

        #region UI_Events

        /// <summary>
        /// Handel the Loading event for the form.
        /// </summary>
        private void XtraFormRatio_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(XtraFormRatio_Load), sender, e);
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
        private void barManager_ItemClick(object sender, ItemClickEventArgs e)
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
        private void XtraFormRatio_Shown(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(XtraFormRatio_Shown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                StartReading();
                gridViewRatio.Focus();
            }
        }

        /// <summary>
        /// Handel the form closing to stop the reading and remove the events handlers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormRatio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FormClosingEventHandler(XtraFormRatio_FormClosing), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (_tempRatioItem == null)
                {
                    _tempRatioItem = gridViewRatio.SelectedRowsCount > 0 ?
                                    gridViewRatio.GetRow(gridViewRatio.GetSelectedRows()[0]) as Item :
                                                        gridViewRatio.GetFocusedRow() as Item;
                }

                RatioItem = _tempRatioItem;

                CsaEmdUnitManager.Instance.DisposeConnection(Csa_Instance_Released, _csaManager_ReadingDone, _csaManager_MeterValueChanged);
            }
        }

        /// <summary>
        /// Sets the value of the vital force when focused row changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewRatio_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FocusedRowChangedEventHandler(gridViewRatio_FocusedRowChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                xtraUserControlReadingGaugeRatio.Clear();

                CancelAutoCloseTimer();

                StopReading();
                StartReading();

                EnableDoneButton();

                xtraUserControlReadingGaugeRatio.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);
            }            
        }

        /// <summary>
        /// Handles the key down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormRatio_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(XtraFormRatio_KeyDown), sender, e);
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
                    DoneAction();
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
        private void XtraFormRatio_KeyUp(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(XtraFormRatio_KeyUp), sender, e);
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
                    gridControlRatio.Focus();
                    SelectPreviousGroup();
                }
                else if (e.Control && e.KeyCode == Keys.Down)
                {
                    gridControlRatio.Focus();
                    SelectNextGroup();
                }
            }
        }

        /// <summary>
        /// Handel the custom draw of the rows.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewRatio_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new RowCellCustomDrawEventHandler(gridViewRatio_CustomDrawCell), sender, e);
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
        private void gridViewRatio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new SelectionChangedEventHandler(gridViewRatio_SelectionChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                _tempRatioItem = gridViewRatio.SelectedRowsCount > 1 ? null : gridViewRatio.GetFocusedRow() as Item;

                if (gridViewRatio.SelectedRowsCount == 0 || (gridViewRatio.SelectedRowsCount == 1 && (_rangeGroup != null && _rangeGroup.IndexOf(gridViewRatio.GetSelectedRows().FirstOrDefault()) != -1)))
                    return;


                CleanGroupFlags(false);
                gridControlRatio.Refresh();
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