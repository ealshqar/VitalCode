using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using System.Linq;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormFourFactors : DevExpress.XtraEditors.XtraForm
    {
        #region Fields

        private TestResultFactor _focusFactor;
        private SettingsManager _settingsManager;
        private LookupsManager _lookupsManager;

        private bool _isWaitingCsaRealsedToTakeNewReading;
        private bool _isWaitingCsaRealsedToMoveNextPotency;

        private int _yesLookupId;
        private int _autoCloseAfter;
        private int _autoCloseAfterPassedTime;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the item name.
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
            }
        }

        /// <summary>
        /// Gets or sets the result factors list.
        /// </summary>
        public BindingList<TestResultFactor> ResultFactors { get; set; }

        /// <summary>
        /// Gets or sets the test result.
        /// </summary>
        public TestResult TestResult { get; set; }

        /// <summary>
        /// Gets or sets the Test Play State
        /// </summary>
        public TestPlayStateEnum TestPlayState { get; set; }

        #endregion

        #region Constructors

        public XtraFormFourFactors()
        {
            InitializeComponent();
            TestPlayState = TestPlayStateEnum.Paused;
            CustomeInitializeComponent();
        }

        #endregion

        #region Logic

        /// <summary>
        /// Customize the Initialize Component, for adding remove or change some components.
        /// </summary>
        private void CustomeInitializeComponent()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingCustomComponents);

            //Add Images and Combobox Items for handling four factor images, we are doing it like this because the ImageList control
            //doesn't allow adding images from Project Resources and we don't want to store images the in the resource file of the form itself
            //Using the code below, we are adding images from project resources and then creating combobox items based on them.

            //Add Images and Combobox Items for handling four factor images, we are doing it like this because the ImageList control
            //doesn't allow adding images from Project Resources and we don't want to store images the in the resource file of the form itself
            //Using the code below, we are adding images from project resources and then creating combobox items based on them.
            imageListFourFactors.Images.Add("CircleBalanced", Vital.UI.Properties.Resources.CircleBalanced);
            imageListFourFactors.Images.Add("CircleUnbalanced", Vital.UI.Properties.Resources.CircleUnbalanced);
            imageListFourFactors.Images.Add("CircleClear", Vital.UI.Properties.Resources.CircleClear);

            repositoryItemImageComboBoxFourFactors.Items.AddRange(new ImageComboBoxItem[] {
            new ImageComboBoxItem("Balanced", FourFactorState.Balanced, 0),
            new ImageComboBoxItem("Unbalanced", FourFactorState.UnBalanced, 1),
            new ImageComboBoxItem("Clear", FourFactorState.Clear, 2)});
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Perform the initialization of the test issue
        /// </summary>
        private void PerformSpecificIntializationSteps()
        {
            CsaEmdUnitManager.Instance.ActivateConnection(Csa_Instance_Released, _csaManager_ReadingDone, _csaManager_MeterValueChanged);

            UiHelperClass.SetLayoutControlProperties(layoutControlVitalForce);

            _settingsManager = new SettingsManager();
            _lookupsManager = new LookupsManager();

            if (ResultFactors.Count == 0)
            {
                var fourFactors = (BindingList<Item>)CacheHelper.SetOrGetCachableData(CachableDataEnum.FourFactors);

                foreach (var factorPoint in fourFactors)
                {
                    ResultFactors.Add(new TestResultFactor { TestResult = TestResult, Factor = factorPoint, Reading = 0 });
                }
            }

            FillLocalLookupIds();
            SetupAutoCloseTimer();

            var fontSize =
                    _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.FontSize) });
            gridViewFourFactors.Appearance.Row.Font = UiHelperClass.GetFontWithSize(float.Parse(fontSize.Value.ToString()));
            
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
            if (ResultFactors == null || ResultFactors.Any(f => f.Reading != 0) || TestPlayState == TestPlayStateEnum.Paused)
                return;

            var isAutoCloseOnSetting = ((BindingList<Setting>)CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings)).FirstOrDefault(s => s.Key == EnumNameResolver.Resolve(SettingKeys.AutoCloseDialogs));

            if (isAutoCloseOnSetting == null || Convert.ToInt32(isAutoCloseOnSetting.Value) != _yesLookupId)
                return;

            var autoCloseAfterSetting = ((BindingList<Setting>)CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings)).FirstOrDefault(s => s.Key == EnumNameResolver.Resolve(SettingKeys.AutoCloseDialogsTimeout));

            if (autoCloseAfterSetting == null)
                return;

            _autoCloseAfter = Convert.ToInt32(autoCloseAfterSetting.Value);
        }

        /// <summary>
        /// Check if the setting for the FF is done and start the auto close.
        /// </summary>
        private void CheckForAutoClose()
        {
            if (_autoCloseAfter != 0 && ResultFactors.All(f => f.Reading != 0))
                StartAutoCloseTimer();
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
        /// Bind the form controls.
        /// </summary>
        private void SetBinding()
        {
            gridControlFourFactors.DataSource = ResultFactors;

            SelectFirstFactor();

            xtraUserControlReadingGaugeVitalForce.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);
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
        /// Perform the factor reading..
        /// </summary>
        /// <param name="value"></param>
        private void PerformFactorsReading(int value)
        {
            StopReading();

            _isWaitingCsaRealsedToTakeNewReading = true;

            if (_focusFactor == null) return;

            _focusFactor.Reading = value;

            EnableDoneButton();
        }

        /// <summary>
        /// Set up the Display of the factor name.
        /// </summary>
        public void SetupFactorsDisplay()
        {
            bool _showFactorFullName;

            var factorFullName = ((BindingList<Setting>)CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings))
                                    .FirstOrDefault(s => s.Key == EnumNameResolver.Resolve(SettingKeys.FourFactorsFullName));

            var yesLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.Yes));

            var _yesLookupId = yesLookup != null ? yesLookup.Id : 0;

            if (factorFullName == null || yesLookup == null) return;
            
            _showFactorFullName = Convert.ToInt32(factorFullName.Value) == _yesLookupId;

            var tempItem = new Item();
            gridColumnFactorName.FieldName = "Factor." + (_showFactorFullName
                                                          ? ExpressionHelper.GetPropertyName(() => tempItem.FullName)
                                                          : ExpressionHelper.GetPropertyName(() => tempItem.Name));            
        }

        /// <summary>
        /// Move selection of the potency grid.
        /// </summary>
        private void MoveSelection()
        {

            if (gridViewFourFactors.IsLastRow)
            {
                StopReading();
                xtraUserControlReadingGaugeVitalForce.SetReadingStatusBarMode(TestBarStateEnum.Ready, string.Empty, 0);
            }
            else
            {
                MoveDown();
            }

            CheckForAutoClose();

        }

        /// <summary>
        /// Select first factor.
        /// </summary>
        private void SelectFirstFactor()
        {
            gridViewFourFactors.SelectRow(0);
            gridViewFourFactors.FocusedRowHandle = 0;
            gridViewFourFactors.MakeRowVisible(0);
            gridViewFourFactors.MoveFirst();
        }

        /// <summary>
        /// Move down in the grid
        /// </summary>
        private void MoveDown()
        {
            var nextRowHandleFactors = gridViewFourFactors.FocusedRowHandle + 1;
            gridViewFourFactors.SelectRow(nextRowHandleFactors);
            gridViewFourFactors.FocusedRowHandle = nextRowHandleFactors;
            gridViewFourFactors.MakeRowVisible(nextRowHandleFactors);
            gridViewFourFactors.Focus();

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
            CsaEmdUnitManager.Instance.StopReading();
            RemoveHardwareHandlers();
        }

        #endregion

        #region Events

        #region HW_Events

        /// <summary>
        /// Handel the meter value changed.
        /// </summary>
        void _csaManager_ReadingDone(object sender, int reading, int min, int max, int fall, int rise)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.OnReadingDone(_csaManager_ReadingDone), sender, reading, min, max, fall, rise);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                _csaManager_MeterValueChanged(sender, reading, min, max);

                var csaManager = sender as CsaEmdUnitManager;

                if (csaManager != null && csaManager.IsReadingOn == false) return;

                if (InvokeRequired)
                {
                    Invoke(new CsaEmdUnitManager.OnReadingDone(_csaManager_ReadingDone), sender, reading, min, max, fall,
                           rise);
                }
                else
                {
                    if (!CsaEmdUnitManager.Instance.HasReading) return;

                    PerformFactorsReading(reading);

                    //when all the factors has readings, show ready status.
                    xtraUserControlReadingGaugeVitalForce.SetReadingStatusBarMode(
                        ResultFactors.Any(c => c.Reading == 0)
                            ? TestBarStateEnum.WaitingToRelease
                            : TestBarStateEnum.Ready,
                        string.Empty, 0);
                }
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

                if (_focusFactor != null)
                {
                    _focusFactor.Reading = reading;
                }

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
                //This check for prevent the released [@UI level only] action get executing twice.
                if (!CsaEmdUnitManager.Instance.HasReading) return;

                xtraUserControlReadingGaugeVitalForce.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);

                if (TestPlayState == TestPlayStateEnum.Playing)
                {
                    if (_isWaitingCsaRealsedToTakeNewReading)
                    {
                        _isWaitingCsaRealsedToTakeNewReading = false;
                    }
                    else if (_isWaitingCsaRealsedToMoveNextPotency)
                    {
                        _isWaitingCsaRealsedToMoveNextPotency = false;
                    }

                    MoveSelection();
                }

                xtraUserControlReadingGaugeVitalForce.SetReadingStatusBarMode(
                    ResultFactors.Any(c => c.Reading == 0) ? TestBarStateEnum.TakeReading : TestBarStateEnum.Ready,
                    string.Empty, 0);


                StartReading();

            }
        }

        #endregion

        #region UI_Events

        /// <summary>
        ///  Handel the form load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormFourFactors_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(XtraFormFourFactors_Load), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                PerformSpecificIntializationSteps();
                SetupFactorsDisplay();
                SetBinding();                
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
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new DevExpress.XtraBars.ItemClickEventHandler(XtraFormFourFactors_Load), sender, e);
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
        /// Handel the form closing to stop the reading and remove the events handlers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormFourFactors_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FormClosingEventHandler(XtraFormFourFactors_FormClosing), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                CsaEmdUnitManager.Instance.DisposeConnection(Csa_Instance_Released, _csaManager_ReadingDone,
                                                             _csaManager_MeterValueChanged);
            }
        }

        /// <summary>
        /// Handel the focused row changed to start the reading on the factors.
        /// </summary>
        private void gridViewFourFactors_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FocusedRowChangedEventHandler(gridViewFourFactors_FocusedRowChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                CancelAutoCloseTimer();

                _focusFactor = gridViewFourFactors.GetFocusedRow() as TestResultFactor;
                xtraUserControlReadingGaugeVitalForce.Clear();
                StartReading();

                xtraUserControlReadingGaugeVitalForce.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty,
                                                                              0);
            }
        }

        /// <summary>
        /// Handel the row click event to start the reading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewFourFactors_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gridViewFourFactors_RowClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                StartReading();
            }
        }

        /// <summary>
        /// Handles the key down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormFourFactors_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(XtraFormFourFactors_KeyDown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if ((e.KeyCode == Keys.Enter && barButtonItemDone.Enabled) || e.KeyCode == Keys.Escape)
                {
                    DoneAction();
                }
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

                var closeAfter = (_autoCloseAfter - _autoCloseAfterPassedTime) / 1000f;

                labelControlTip.Text = string.Format(StaticKeys.FormWillColseAfterMessage, closeAfter.ToString("0.0"));

                if (_autoCloseAfterPassedTime >= _autoCloseAfter)
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
