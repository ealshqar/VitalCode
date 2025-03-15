using System;
using System.Windows.Forms;
using Vital.Business.Managers;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormMeterCounterDialog : DevExpress.XtraEditors.XtraForm
    {
        #region Fields

        private bool _isWaitingCsaRealsedToTakeNewReading;
        private bool _isEditable;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the issues count.
        /// </summary>
        public int Count
        {
            get
            {
                int count;
                int.TryParse(digitalGaugeMeterCounter.Text, out count);
                return count;
            }
            set
            {
                digitalGaugeMeterCounter.Text = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the Test Play State
        /// </summary>
        public TestPlayStateEnum TestPlayState { get; set; }

        /// <summary>
        /// Get or set question Text.
        /// </summary>
        public string QuestionText
        {
            get { return simpleLabelItemQuestion.Text; }
            set { simpleLabelItemQuestion.Text = value; }
        }

        /// <summary>
        /// Get or set tip text.
        /// </summary>
        public string Tip
        {
            get { return labelControlTip.Text; }
            set { labelControlTip.Text = value; }
        }

        /// <summary>
        /// Get or set form title.
        /// </summary>
        public string Title
        {
            get { return Text; }
            set { Text = value; }
        }

        #endregion

        #region Constructors
        
        public XtraFormMeterCounterDialog()
        {
            InitializeComponent();
            PerformSpecificIntializationSteps();
            SetEditMode(false);
            TestPlayState = TestPlayStateEnum.Paused;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Perform the initialization of the dialog
        /// </summary>
        private void PerformSpecificIntializationSteps()
        {
            CsaEmdUnitManager.Instance.ActivateConnection(Csa_Instance_Released, _csaManager_ReadingDone, _csaManager_MeterValueChanged);
            UiHelperClass.SetLayoutControlProperties(layoutControlCounter);
        }

        /// <summary>
        /// Enables the Done button.
        /// </summary>
        private void EnableDoneButton()
        {
            barButtonItemDone.Enabled = true;
        }

        /// <summary>
        /// Sets the edit mode of the form
        /// </summary>
        /// <param name="isReadOnly"></param>
        private void SetEditMode(bool isReadOnly)
        {
            simpleButtonDescrease.Enabled = !isReadOnly;
            simpleButtonIncrease.Enabled = !isReadOnly;
            _isEditable = !isReadOnly;
        }
       
        /// <summary>
        /// Checks the current reading.
        /// </summary>
        /// <param name="value"></param>
        private void CheckReading(int value)
        {
            if (!CrossLayersSharedLogic.IsAcceptableReading(value))
            {
                StopReading();
                _isWaitingCsaRealsedToTakeNewReading = true;
            }
            else
            {
                //This order of calling is to avoid the reading overlapping.
                _isWaitingCsaRealsedToTakeNewReading = false;
                StopReading();
            }

        }

        /// <summary>
        /// Performs increase count, and its related actions.
        /// </summary>
        private void PerformIncreasesCount()
        {
            if (CsaEmdUnitManager.Instance.IsReadingOn) return;

            Count++;
            EnableDoneButton();
            StartReading();
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
        /// Increase the count.
        /// </summary>
        private void IncreaseCount()
        {
            xtraUserControlReadingGaugeMeterCounter.Clear();

            _isWaitingCsaRealsedToTakeNewReading = false;
            StopReading();
            Count++;
            StartReading();
            EnableDoneButton();

            gaugeControlMeterCounter.Focus();

            xtraUserControlReadingGaugeMeterCounter.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);
        }

        /// <summary>
        /// Decrease the count of issues
        /// </summary>
        private void DecreaseCount()
        {
            if (Count <= 0) return;

            xtraUserControlReadingGaugeMeterCounter.Clear();

            _isWaitingCsaRealsedToTakeNewReading = false;
            StopReading();
            Count--;
            StartReading();
            EnableDoneButton();

            gaugeControlMeterCounter.Focus();

            xtraUserControlReadingGaugeMeterCounter.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);
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

        #endregion

        #region Events

        #region HW_Events

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
                SetEditMode(false);

                if (!CsaEmdUnitManager.Instance.HasReading) return;

                if (TestPlayState == TestPlayStateEnum.Playing)
                {
                    if (!_isWaitingCsaRealsedToTakeNewReading)
                    {
                        xtraUserControlReadingGaugeMeterCounter.SetReadingStatusBarMode(TestBarStateEnum.Ready,
                                                                                       string.Empty, 0);
                        return;
                    }

                    _isWaitingCsaRealsedToTakeNewReading = false;

                    PerformIncreasesCount();
                }
                else
                {
                    StartReading();
                }

                xtraUserControlReadingGaugeMeterCounter.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);

            }
        }

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
                SetEditMode(true);

                if (TestPlayState == TestPlayStateEnum.Playing)
                {
                    CheckReading(reading);
                }
                else
                {
                    // This case for avoid reading overlaping, we have to stop the reading inside the CheckReading method, have alook on the CheckReading method.
                    StopReading();
                }

                xtraUserControlReadingGaugeMeterCounter.SetReadingStatusBarMode(TestBarStateEnum.WaitingToRelease, string.Empty, 0);
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
                xtraUserControlReadingGaugeMeterCounter.ReadingValue = reading;
                xtraUserControlReadingGaugeMeterCounter.SetReadingStatusBarMode(TestBarStateEnum.Reading, string.Empty, 0);
            }

        }

        #endregion

        #region UI_Events

        /// <summary>
        /// Handel the load event for the form.
        /// </summary>
        private void XtraFormMajorIssuesNumber_Load(object sender, EventArgs e)
        {
            xtraUserControlReadingGaugeMeterCounter.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);
        }

        /// <summary>
        /// Close the form by done or cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barManager_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        /// <summary>
        /// Increase the count.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonIncrease_Click(object sender, EventArgs e)
        {
            IncreaseCount();
        }       

        /// <summary>
        /// Decrease the count.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonDescrease_Click(object sender, EventArgs e)
        {
            DecreaseCount();
        }

        /// <summary>
        /// Handel the shown of the form (Starts the reading).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormMeterCounterDialog_Shown(object sender, EventArgs e)
        {
            StartReading();
        }

        /// <summary>
        /// Handel the form closing to stop the reading and remove the events handlers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormMeterCounterDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            CsaEmdUnitManager.Instance.DisposeConnection(Csa_Instance_Released, _csaManager_ReadingDone, _csaManager_MeterValueChanged);
        }

        /// <summary>
        /// Handles the key down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormMeterCounterDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && barButtonItemDone.Enabled)
            {
                DoneAction();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                CancelAction();
            }
        }

        /// <summary>
        /// Handles the Up and down keys which only work in the KeyUp event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormMeterCounterDialog_KeyUp(object sender, KeyEventArgs e)
        {
            if(!_isEditable) return;

            if (e.KeyCode == Keys.Up)
            {
                IncreaseCount();
            }
            else if (e.KeyCode == Keys.Down)
            {
                DecreaseCount();
            }
        }

        #endregion
        
        #endregion        
    }
}