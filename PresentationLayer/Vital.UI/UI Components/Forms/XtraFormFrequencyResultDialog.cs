using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.FrequencyTests;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormFrequencyResultDialog : XtraForm
    {
        #region Private Variables

        private List<string> _questions; 
        private int _activeQuestionIndex;
        private int _currentReading;
        private bool _isUserNavigation;

        #endregion

        #region Public Properties

        /// <summary>
        /// CurrentFrequencyTestResult
        /// </summary>
        public FrequencyTestResult CurrentFrequencyTestResult
        {
            get; set;
        }

        /// <summary>
        /// InitialFrequencyTestResult
        /// </summary>
        public FrequencyTestResult InitialFrequencyTestResult
        {
            get;
            set;
        }

        /// <summary>
        /// Questions count
        /// </summary>
        public int QuestionsCount
        {
            get
            {
                return _questions.Count;
            }
        }

        #endregion

        #region Constructors

        private XtraFormFrequencyResultDialog(bool isUserNavigation)
        {
            InitializeComponent();
            _isUserNavigation = isUserNavigation;
        }

        /// <summary>
        /// The constructor.
        /// </summary>
        public XtraFormFrequencyResultDialog(FrequencyTestResult frequencyTestResult, bool isUserNavigation) : this(isUserNavigation)
        {
            CurrentFrequencyTestResult = frequencyTestResult;

            CloneFrequencyTestResult(frequencyTestResult);

            PerformSpecificIntializationSteps();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Performs specific steps.
        /// </summary>
        private void PerformSpecificIntializationSteps()
        {
            PrepareQuestionsLists();
            _activeQuestionIndex = 0;
            barButtonItemStop.Visibility = _isUserNavigation ? BarItemVisibility.Never : BarItemVisibility.Always;

            CsaEmdUnitManager.Instance.ActivateConnection(_csa_Instance_Released, _csaManager_ReadingDone, _csaManager_MeterValueChanged);
            UiHelperClass.SetLayoutControlProperties(layoutControlFrequencyTestResult);

            InitializeGauge(StaticKeys.DefaultFrequencyTestResultAnswer);
            GetQuestion();
            FillItemName();
            CheckNextPreviousButtons();
        }

        /// <summary>
        /// Clone the frequency test result.
        /// </summary>
        private void CloneFrequencyTestResult(FrequencyTestResult frequencyTestResult)
        {
            InitialFrequencyTestResult = new FrequencyTestResult
            {
                Item = frequencyTestResult.Item,
                Notes = frequencyTestResult.Notes,
                TimesPerWeek = frequencyTestResult.TimesPerWeek,
                NumberOfWeeks = frequencyTestResult.NumberOfWeeks
            };
        }

        /// <summary>
        /// Fill the cloned frequency test result.
        /// </summary>
        private void FillClonedFrequencyTestResult(FrequencyTestResult frequencyTestResult)
        {
            frequencyTestResult.Item = InitialFrequencyTestResult.Item;
            frequencyTestResult.Notes = InitialFrequencyTestResult.Notes;
            frequencyTestResult.TimesPerWeek = InitialFrequencyTestResult.TimesPerWeek;
            frequencyTestResult.NumberOfWeeks = InitialFrequencyTestResult.NumberOfWeeks;
        }

        /// <summary>
        /// Update the values
        /// </summary>
        private void UpdateValues()
        {
            if (_activeQuestionIndex == 0)
                CurrentFrequencyTestResult.TimesPerWeek = GetCurrentGuageValue();
            if (_activeQuestionIndex == 1)
                CurrentFrequencyTestResult.NumberOfWeeks = GetCurrentGuageValue();
        }

        /// <summary>
        /// Applies the stop action rules.
        /// </summary>
        private void StopAction()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Applies the done action rules.
        /// </summary>
        private void DoneAction()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Applies the cancel action rules.
        /// </summary>
        private void CancelAction()
        {
            FillClonedFrequencyTestResult(CurrentFrequencyTestResult);

            Close();    
        }

        /// <summary>
        /// Applies the automatic rules.
        /// </summary>
        /// <param name="currentReading"></param>
        private void AutoTestEngine(int currentReading)
        {
            if (currentReading == 50)
                PerformYesAction();
            else
                PerformNoAction();

            CheckNextPreviousButtons();
        }

        /// <summary>
        /// Applies the rules of the No answer (non fifty answer).
        /// </summary>
        private void PerformNoAction()
        {
            PerformGuageOperation(GuageOperation.IncrementValue);
        }

        /// <summary>
        /// Performs an operation on the gauge.
        /// </summary>
        /// <param name="guageOperation">The operation type.</param>
        private void PerformGuageOperation(GuageOperation guageOperation)
        {
            var currentValue = digitalGaugeNumber.Text;
            var currentValueAsInteger = 0;
            var newGuageValue = 0;

            int.TryParse(currentValue, out currentValueAsInteger);

            switch (guageOperation)
            {
                case GuageOperation.IncrementValue:

                    if (currentValueAsInteger < 999)
                        newGuageValue = currentValueAsInteger + 1;
                    break;

                case GuageOperation.DecrementValue:

                    if (currentValueAsInteger > 0)
                        newGuageValue = currentValueAsInteger - 1;
                    break;
            }

            digitalGaugeNumber.Text = newGuageValue.ToString();
        }

        /// <summary>
        /// Gets the current gauge value.
        /// </summary>
        /// <returns></returns>
        private int GetCurrentGuageValue()
        {
            var currentValue = digitalGaugeNumber.Text;
            int currentValueAsInteger;

            int.TryParse(currentValue, out currentValueAsInteger);

            return currentValueAsInteger;
        }

        /// <summary>
        /// Applies the rules of a yes answer.
        /// </summary>
        private void PerformYesAction()
        {
            UpdateValues();

            if (_activeQuestionIndex < 1)
                _activeQuestionIndex += 1;
            else
                DoneAction();

            GetQuestion();
        }

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
        /// Stop the reading.
        /// </summary>
        private void StopReading()
        {
            CsaEmdUnitManager.Instance.StopReading();
            RemoveHardwareHandlers();
        }

        /// <summary>
        /// Fills the item name in the dialog title and the label.
        /// </summary>
        private void FillItemName()
        {
            Text = labelControlItemName.Text = CurrentFrequencyTestResult.Item.Name;
        }

        /// <summary>
        /// Initializes gauge value.
        /// </summary>
        /// <param name="answer"></param>
        private void InitializeGauge(string answer)
        {
            digitalGaugeNumber.Text = answer;
        }

        /// <summary>
        /// Enables/Disables the next and previous buttons.
        /// </summary>
        private void CheckNextPreviousButtons()
        {
            simpleButtonPrevious.Enabled = _activeQuestionIndex != 0;

            simpleButtonNext.Enabled = _activeQuestionIndex >= 0 && _activeQuestionIndex < QuestionsCount - 1;
        }

        /// <summary>
        /// Prepares the questions lists.
        /// </summary>
        private void PrepareQuestionsLists()
        {
            _questions = new List<string> { "Times Per Week ?", "Number Of Weeks ?"};
        }

        /// <summary>
        /// Gets the suitable question.
        /// </summary>
        /// <returns></returns>
        private void GetQuestion()
        {
            var answer = string.Empty;

            var question = _questions[_activeQuestionIndex];

            if (_isUserNavigation)
            {
                switch (_activeQuestionIndex)
                {
                    case 0:
                        answer = CurrentFrequencyTestResult.TimesPerWeek.ToString();
                        break;
                    case 1:
                        answer = CurrentFrequencyTestResult.NumberOfWeeks.ToString();
                        break;
                }
            }

            _isUserNavigation = false;

            labelControlQuestion.Text = question;

            InitializeGauge(!string.IsNullOrEmpty(answer) ? answer == "0" ? StaticKeys.DefaultFrequencyTestResultAnswer : answer : StaticKeys.DefaultFrequencyTestResultAnswer);
        }

        /// <summary>
        /// Takes the user to the next question.
        /// </summary>
        private void GoToNextQuestion()
        {
            UpdateValues();
            _isUserNavigation = true;
            _activeQuestionIndex++;
            GetQuestion();
            CheckNextPreviousButtons();
        }

        /// <summary>
        /// Takes the user to the previous question.
        /// </summary>
        private void GoToPreviousQuestion()
        {
            UpdateValues();
            _isUserNavigation = true;
            _activeQuestionIndex--;
            GetQuestion();
            CheckNextPreviousButtons();
        }

        #endregion

        #region Hardware Logic

        /// <summary>
        /// Handel the reading done event.
        /// </summary>
        private void _csaManager_MeterValueChanged(object sender, int reading, int min, int max)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.MeterValueChangedHandle(
                            _csaManager_MeterValueChanged), sender, reading, min, max);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //Update the reading Gauge in the incoming reading.
                xtraUserControlReadingGaugeY.ReadingValue = reading;

                //Set the Gauge Status.
                xtraUserControlReadingGaugeY.SetReadingStatusBarMode(TestBarStateEnum.Reading,
                string.Empty, 0);
            }
        }

        /// <summary>
        /// Handel the meter value changed.
        /// </summary>
        private void _csaManager_ReadingDone(object sender, int reading, int min, int max, int fall, int rise)
        {
            _csaManager_MeterValueChanged(sender, reading, min, max);

            if (InvokeRequired)
            {
                if (IsDisposed) return;
                Invoke(new CsaEmdUnitManager.OnReadingDone(_csaManager_ReadingDone), sender,
                                                              reading, min, max, fall, rise);
            }
            else
            {
                var csaManager = sender as CsaEmdUnitManager;

                if (csaManager != null && csaManager.IsReadingOn == false) return;

                if (!CsaEmdUnitManager.Instance.HasReading) return;

                StopReading();

                xtraUserControlReadingGaugeY.SetReadingStatusBarMode(TestBarStateEnum.WaitingToRelease, string.Empty, 0);
            }

            _currentReading = reading;
        }

        /// <summary>
        /// Handel the CSA released  event.
        /// </summary>
        /// <param name="sender">The sender as CSA manager.</param>
        private void _csa_Instance_Released(object sender)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.OnReleased(_csa_Instance_Released), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //Check if we have a reading.
                if (!CsaEmdUnitManager.Instance.HasReading) return;

                //XXX = TestBarStateEnum.TakeReading OR TestBarStateEnum.Ready
                xtraUserControlReadingGaugeY.SetReadingStatusBarMode(TestBarStateEnum.Ready, string.Empty, 0);

                //Should start another reading here.
                StartReading();

                AutoTestEngine(_currentReading);
            }
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

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the shown event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void XtraFormFrequencyResultDialog_Shown(object sender, EventArgs e)
        {
            StartReading();
        }

        /// <summary>
        /// Handles the next button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonNext_Click(object sender, EventArgs e)
        {
            GoToNextQuestion();
        }
        
        /// <summary>
        /// Handles the previous button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonPrevious_Click(object sender, EventArgs e)
        {
            GoToPreviousQuestion();
        }

        /// <summary>
        /// Handles the click on the bar manager buttons.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void barManager_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Item.Name)) return;

            if (e.Item == barButtonItemDone)
            {
                UpdateValues();
                DoneAction();
            }
            else if (e.Item == barButtonItemStop)
            {
                UpdateValues();
                StopAction();
            }
        }

        /// <summary>
        /// Handles the form closing event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void XtraFormFrequencyResultDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FormClosingEventHandler(XtraFormFrequencyResultDialog_FormClosing), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                CsaEmdUnitManager.Instance.DisposeConnection(_csa_Instance_Released, _csaManager_ReadingDone,
                                                             _csaManager_MeterValueChanged);
            }
        }

        /// <summary>
        /// Handles the click on the increment button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonIncrement_Click(object sender, EventArgs e)
        {
            PerformGuageOperation(GuageOperation.IncrementValue);
        }

        /// <summary>
        /// Handles the click on the decrement button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonDecrement_Click(object sender, EventArgs e)
        {
            PerformGuageOperation(GuageOperation.DecrementValue);
        }

        /// <summary>
        /// Handles the key up event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void XtraFormFrequencyResultDialog_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (simpleButtonNext.Enabled)
                {
                    GoToNextQuestion();
                }
            }

            if (e.KeyCode == Keys.Left)
            {
                if (simpleButtonPrevious.Enabled)
                {
                    GoToPreviousQuestion();
                }
            }

            if (e.KeyCode == Keys.Down)
            {
                if (simpleButtonDecrement.Enabled)
                {
                    PerformGuageOperation(GuageOperation.DecrementValue);
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                if (simpleButtonIncrement.Enabled)
                {
                    PerformGuageOperation(GuageOperation.IncrementValue);
                }
            }

            CheckNextPreviousButtons();
        }

        /// <summary>
        /// Handles the key down event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void XtraFormFrequencyResultDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateValues();
                DoneAction();
            }

            if(e.KeyCode == Keys.Escape)
            {
                CancelAction();
            }
        }

        #endregion
    }
}