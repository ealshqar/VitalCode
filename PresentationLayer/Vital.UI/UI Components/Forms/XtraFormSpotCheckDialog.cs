using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.SpotChecks;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormSpotCheckDialog : XtraForm
    {
        #region Private Variables

        private LookupsManager _lookupsManager;
        
        private Lookup _ivSheetLookup;
        private Lookup _capsolTLookup;
        private Lookup _mineralsLookup;
        private Lookup _dmpsLookup;

        private List<string> _mteQuestions; 
        private List<string> _mineralsQuestions; 
        private List<string> _capsolTQuestions;
        private List<string> _dmpsQuestions;

        private List<string> _spotCheckQuestions; 

        private int _activeQuestionIndex;
        private int _currentReading;
        private bool _isUserNavigation;

        private SpotCheckDialogType _currentSpotCheckDialogType;
        
        #endregion

        #region Public Properties

        public SpotCheckResult CurrentSpotCheckResult
        {
            get; set;
        }

        public SpotCheckResult InitialSpotCheckResult
        {
            get;
            set;
        }

        public SpotCheck CurrentSpotCheck { get; set; }

        #endregion

        #region Constructors

        private XtraFormSpotCheckDialog(bool isUserNavigation)
        {
            InitializeComponent();
            _isUserNavigation = isUserNavigation;
        }

        /// <summary>
        /// The constructor.
        /// </summary>
        public XtraFormSpotCheckDialog(SpotCheckResult spotCheckResult, bool isUserNavigation) : this(isUserNavigation)
        {
            _currentSpotCheckDialogType = SpotCheckDialogType.SpotCheckResult;

            CurrentSpotCheckResult = spotCheckResult;
            
            CloneSpotCheckResult(spotCheckResult);

            PerformSpecificIntializationSteps();
        }

        /// <summary>
        /// The constructor.
        /// </summary>
        public XtraFormSpotCheckDialog(SpotCheck spotCheck, bool isUserNavigation) : this(isUserNavigation)
        {
            _currentSpotCheckDialogType = SpotCheckDialogType.SpotCheck;

            CurrentSpotCheck = spotCheck;
            
            PerformSpecificIntializationSteps();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Performs specific steps.
        /// </summary>
        private void PerformSpecificIntializationSteps()
        {
            _lookupsManager = new LookupsManager();

            _activeQuestionIndex = 0;
            barButtonItemStop.Visibility = _isUserNavigation ? BarItemVisibility.Never : BarItemVisibility.Always;

            CsaEmdUnitManager.Instance.ActivateConnection(_csa_Instance_Released, _csaManager_ReadingDone, _csaManager_MeterValueChanged);
            UiHelperClass.SetLayoutControlProperties(layoutControlSpotCheck);

            FillLookUps();
            PrepareQuestionsLists();
            InitializeGauge(StaticKeys.DefaultSpotCheckAnswer);
            GetQuestion();
            FillItemName();
            CheckNextPreviousButtons();
        }

        /// <summary>
        /// Clone the spot check result.
        /// </summary>
        private void CloneSpotCheckResult(SpotCheckResult spotCheckResult)
        {
            InitialSpotCheckResult = new SpotCheckResult();

            InitialSpotCheckResult.Dosage = spotCheckResult.Dosage;
            InitialSpotCheckResult.Notes = spotCheckResult.Notes;
            InitialSpotCheckResult.NumberOfBags = spotCheckResult.NumberOfBags;
            InitialSpotCheckResult.NumberOfWeeks = spotCheckResult.NumberOfWeeks;
            InitialSpotCheckResult.YesNo = spotCheckResult.YesNo;
        }

        /// <summary>
        /// Fill the cloned spot check result.
        /// </summary>
        private void FillClonedSpotCheckResult(SpotCheckResult spotCheckResult)
        {
            spotCheckResult.Dosage = InitialSpotCheckResult.Dosage;
            spotCheckResult.Notes = InitialSpotCheckResult.Notes;
            spotCheckResult.NumberOfBags = InitialSpotCheckResult.NumberOfBags;
            spotCheckResult.NumberOfWeeks = InitialSpotCheckResult.NumberOfWeeks;
            spotCheckResult.YesNo = InitialSpotCheckResult.YesNo;
        }

        /// <summary>
        /// Update the values based on current dialog type.
        /// </summary>
        private void UpdateValues()
        {
            switch (_currentSpotCheckDialogType)
            {
                case SpotCheckDialogType.SpotCheck:
                    UpdateSpotCheckValues();
                    break;
                case SpotCheckDialogType.SpotCheckResult:
                    UpdateSpotCheckResultValues();
                    break;
            }
        }

        /// <summary>
        /// Update the values for spot check.
        /// </summary>
        private void UpdateSpotCheckValues()
        {
            switch (_activeQuestionIndex)
            {
                case 0:
                    CurrentSpotCheck.IngredientsNumberOfBags = GetCurrentGuageValue();
                    break;
                case 1:
                    CurrentSpotCheck.IngredientsNumberPerWeek = GetCurrentGuageValue();
                    break;
                case 2:
                    if (_currentReading > 0)
                        CurrentSpotCheck.MineralsThree = _currentReading == 50;
                    break;
                case 3:
                    CurrentSpotCheck.MineralsNormalSalineCc = GetCurrentGuageValue();
                    break;
                case 4:
                    CurrentSpotCheck.MineralsNormalSalineCcpriority = GetCurrentGuageValue();
                    break;
                case 5:
                    if (_currentReading > 0)
                        CurrentSpotCheck.MineralsOne = _currentReading == 50;
                    break;
                case 6:
                    if (_currentReading > 0)
                        CurrentSpotCheck.MineralsIvPush = _currentReading == 50;
                    break;
                case 7:
                    CurrentSpotCheck.MineralsSterlieWaterCc = GetCurrentGuageValue();
                    break;
                case 8:
                    CurrentSpotCheck.MineralsSterlieWaterCcpriority = GetCurrentGuageValue();
                    break;
                case 9:
                    CurrentSpotCheck.MineralsDextroseCc = GetCurrentGuageValue();
                    break;
                case 10:
                    CurrentSpotCheck.MineralsDextroseCcpriority = GetCurrentGuageValue();
                    break;
                case 11:
                    CurrentSpotCheck.MineralsIvperMin = GetCurrentGuageValue();
                    break;
                case 12:
                    CurrentSpotCheck.MineralsPerWeek = GetCurrentGuageValue();
                    break;
                case 13:
                    CurrentSpotCheck.MineralsEdta = GetCurrentGuageValue();
                    break;
            }
        }

        /// <summary>
        /// Updates the values of the spot check result.
        /// </summary>
        private void UpdateSpotCheckResultValues()
        {
            if (CurrentSpotCheckResult.ResultType.Id == _ivSheetLookup.Id)
            {
                if (_activeQuestionIndex == 0)
                    CurrentSpotCheckResult.NumberOfBags = GetCurrentGuageValue();
                if (_activeQuestionIndex == 1)
                    CurrentSpotCheckResult.NumberOfWeeks = GetCurrentGuageValue();
                if (_activeQuestionIndex == 2)
                    CurrentSpotCheckResult.Dosage = GetCurrentGuageValue();
            }

            if (CurrentSpotCheckResult.ResultType.Id == _mineralsLookup.Id)
            {
                if (_activeQuestionIndex == 0)
                    CurrentSpotCheckResult.Dosage = GetCurrentGuageValue();
            }

            if (CurrentSpotCheckResult.ResultType.Id == _capsolTLookup.Id)
            {
                if (_activeQuestionIndex == 0)
                    CurrentSpotCheckResult.NumberOfBags = GetCurrentGuageValue();
                if (_activeQuestionIndex == 1)
                    CurrentSpotCheckResult.NumberOfWeeks = GetCurrentGuageValue();
                if (_activeQuestionIndex == 2)
                    CurrentSpotCheckResult.Dosage = GetCurrentGuageValue();
            }

            if (CurrentSpotCheckResult.ResultType.Id == _dmpsLookup.Id)
            {
                if (_activeQuestionIndex == 0)
                    CurrentSpotCheckResult.NumberOfBags = GetCurrentGuageValue();
                if (_activeQuestionIndex == 1)
                    CurrentSpotCheckResult.NumberOfWeeks = GetCurrentGuageValue();
                if (_activeQuestionIndex == 2)
                    CurrentSpotCheckResult.Dosage = GetCurrentGuageValue();
            }
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
            if (_currentSpotCheckDialogType == SpotCheckDialogType.SpotCheckResult)
                FillClonedSpotCheckResult(CurrentSpotCheckResult);

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
        /// Check if current question is bool.
        /// </summary>
        /// <returns></returns>
        private bool IsBooleanQuestion()
        {
            return _currentSpotCheckDialogType == SpotCheckDialogType.SpotCheck && (_activeQuestionIndex == 2 || _activeQuestionIndex == 5 || _activeQuestionIndex == 6);
        }

        /// <summary>
        /// Applies the rules of the No answer (non fifty answer).
        /// </summary>
        private void PerformNoAction()
        {

            if(_currentSpotCheckDialogType == SpotCheckDialogType.SpotCheck)
            {
                if (IsBooleanQuestion())
                {
                    UpdateValues();
                    _activeQuestionIndex++;
                    GetQuestion();
                }
                else
                {
                    PerformGuageOperation(GuageOperation.IncrementValue);
                }

                return;
            }

            if (CurrentSpotCheckResult.ResultType.Id == _ivSheetLookup.Id && _activeQuestionIndex <= 2)
            {
                PerformGuageOperation(GuageOperation.IncrementValue);
            }

            if (CurrentSpotCheckResult.ResultType.Id == _mineralsLookup.Id)
            {
                if (_activeQuestionIndex == 0)
                    PerformGuageOperation(GuageOperation.IncrementValue);
            }

            if (CurrentSpotCheckResult.ResultType.Id == _capsolTLookup.Id && _activeQuestionIndex <= 2)
            {
                PerformGuageOperation(GuageOperation.IncrementValue);
            }

            if (CurrentSpotCheckResult.ResultType.Id == _dmpsLookup.Id && _activeQuestionIndex <= 2)
            {
                PerformGuageOperation(GuageOperation.IncrementValue);
            }

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
            var currentValueAsInteger = 0;

            int.TryParse(currentValue, out currentValueAsInteger);

            return currentValueAsInteger;
        }

        /// <summary>
        /// Applies the rules of a yes answer.
        /// </summary>
        private void PerformYesAction()
        {
            UpdateValues();

            if (_currentSpotCheckDialogType == SpotCheckDialogType.SpotCheck)
            {
                if (_activeQuestionIndex < 13)
                {
                    _activeQuestionIndex++;
                }
                else
                {
                    DoneAction();
                    return;
                }

                GetQuestion();
                return;
            }

            if (CurrentSpotCheckResult.ResultType.Id == _ivSheetLookup.Id)
            {
                if (_activeQuestionIndex < 2)
                    _activeQuestionIndex += 1;
                else
                    DoneAction();
            }
            else if (CurrentSpotCheckResult.ResultType.Id == _mineralsLookup.Id)
            {
                DoneAction();
            }
            else if (CurrentSpotCheckResult.ResultType.Id == _capsolTLookup.Id)
            {
                if (_activeQuestionIndex < 2)
                    _activeQuestionIndex += 1;
                else
                    DoneAction();
            }
            else if (CurrentSpotCheckResult.ResultType.Id == _dmpsLookup.Id)
            {
                if (_activeQuestionIndex < 2)
                    _activeQuestionIndex += 1;
                else
                    DoneAction();
            }

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
            Text =  labelControlItemName.Text =  _currentSpotCheckDialogType == SpotCheckDialogType.SpotCheckResult ? CurrentSpotCheckResult.Item.Name : "Details";
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

            switch (_currentSpotCheckDialogType)
            {
                case SpotCheckDialogType.SpotCheck:
                    simpleButtonNext.Enabled = _activeQuestionIndex >= 0 && _activeQuestionIndex < 13;
                    break;
                case SpotCheckDialogType.SpotCheckResult:
                    if (CurrentSpotCheckResult.ResultType.Id == _ivSheetLookup.Id ||
                        CurrentSpotCheckResult.ResultType.Id == _capsolTLookup.Id
                        || CurrentSpotCheckResult.ResultType.Id == _dmpsLookup.Id)
                    {
                        simpleButtonNext.Enabled = _activeQuestionIndex >= 0 && _activeQuestionIndex < 2;
                    }

                    if (CurrentSpotCheckResult.ResultType.Id == _mineralsLookup.Id)
                    {
                        simpleButtonNext.Enabled = _activeQuestionIndex != 0;
                    }
                    break;
            }

        }

        /// <summary>
        /// Fills the lookups ids.
        /// </summary>
        private void FillLookUps()
        {
            _ivSheetLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SpotCheckResultType, SpotCheckResultType.IVSheet));

            _capsolTLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SpotCheckResultType, SpotCheckResultType.CapsolT));

            _mineralsLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SpotCheckResultType, SpotCheckResultType.Minerals));

            _dmpsLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SpotCheckResultType, SpotCheckResultType.Dmps));
        }

        /// <summary>
        /// Prepares the questions lists.
        /// </summary>
        private void PrepareQuestionsLists()
        {
            switch (_currentSpotCheckDialogType)
            {
                case SpotCheckDialogType.SpotCheck:
                    _spotCheckQuestions = new List<string>
                                              {
                                                  "Number of bags ?",
                                                  "Number PER week ?",
                                                  "3-3 1/2 hr ?",                                                  
                                                  "How many CCs for 0.9% Normal Saline ?",
                                                  "What's the priority for 0.9% Normal Saline ?",
                                                  "1-1 1/2 hr ?",
                                                  "IV Push ?",
                                                  "How many CCs for Sterile Water ?",
                                                  "What's the priority for Sterile Water ?",
                                                  "How many CCs for Dextrose 5% in H2O ?",
                                                  "What's the priority for Dextrose 5% in H2O ?",
                                                  "IV/Min ?",
                                                  "PER week ?",
                                                  "EDTA ?"
                                              };
                    break;
                case SpotCheckDialogType.SpotCheckResult:
                    _mteQuestions = new List<string> {"Number of bags ?", "Number PER week ?", "Dosage ?"};
                    _mineralsQuestions = new List<string> {"How many CCs ?"};
                    _capsolTQuestions = new List<string> { "Number of bags ?", "Number PER week ?", "Dosage ?" };
                    _dmpsQuestions = new List<string> { "Number of bags ?", "Number PER week ?", "Dosage ?" };
                    break;
            }
            
            
        }

        /// <summary>
        /// Gets the suitable question.
        /// </summary>
        /// <returns></returns>
        private void GetQuestion()
        {
            switch (_currentSpotCheckDialogType)
            {
                case SpotCheckDialogType.SpotCheck:
                    GetQuestionSoptCheck();
                    break;
                case SpotCheckDialogType.SpotCheckResult:
                    GetQuestionSpotCheckResult();
                    break;
            }
        }

        /// <summary>
        /// Gets the question for spot check.
        /// </summary>
        private void GetQuestionSoptCheck()
        {
            string question = _spotCheckQuestions[_activeQuestionIndex];
            var answer = string.Empty;

            if (IsBooleanQuestion())
            {
                InitializeGauge("Y/N");
                answer = "Y/N";
                simpleButtonDecrement.Enabled = false;
                simpleButtonIncrement.Enabled = false;
            }
            else
            {
                simpleButtonDecrement.Enabled = true;
                simpleButtonIncrement.Enabled = true;
            }

            if(_isUserNavigation)
            {
                switch (_activeQuestionIndex)
                {
                    case 0:
                        answer = CurrentSpotCheck.IngredientsNumberOfBags.ToString();
                        break;
                    case 1:
                        answer = CurrentSpotCheck.IngredientsNumberPerWeek.ToString();
                        break;
                    case 2:
                        answer = CurrentSpotCheck.MineralsThree ? "Yes" : "No";
                        break;
                    case 3:
                        answer = CurrentSpotCheck.MineralsNormalSalineCc.ToString();
                        break;
                    case 4:
                        answer = CurrentSpotCheck.MineralsNormalSalineCcpriority.ToString();
                        break;
                    case 5:
                        answer = CurrentSpotCheck.MineralsOne ? "Yes" : "No";
                        break;
                    case 6:
                        answer = CurrentSpotCheck.MineralsIvPush ? "Yes" : "No";
                        break;
                    case 7:
                        answer = CurrentSpotCheck.MineralsSterlieWaterCc.ToString();
                        break;
                    case 8:
                        answer = CurrentSpotCheck.MineralsSterlieWaterCcpriority.ToString();
                        break;
                    case 9:
                        answer = CurrentSpotCheck.MineralsDextroseCc.ToString();
                        break;
                    case 10:
                        answer = CurrentSpotCheck.MineralsDextroseCcpriority.ToString();
                        break;
                    case 11:
                        answer = CurrentSpotCheck.MineralsIvperMin.ToString();
                        break;
                    case 12:
                        answer = CurrentSpotCheck.MineralsPerWeek.ToString();
                        break;
                    case 13:
                        answer = CurrentSpotCheck.MineralsEdta.ToString();
                        break;
                }
            }

            _isUserNavigation = false;

            labelControlQuestion.Text = question;

            if (_activeQuestionIndex == 3)
            {
                InitializeGauge(string.IsNullOrEmpty(answer) || answer.Equals("0") ?  "500" : answer );
            }
            else if (_activeQuestionIndex == 2)
            {
                InitializeGauge(string.IsNullOrEmpty(answer) || answer.Equals("Y/N") ? "Yes" : answer);
            }
            else if(_activeQuestionIndex == 0 || _activeQuestionIndex == 1)
            {
                InitializeGauge(string.IsNullOrEmpty(answer) || answer.Equals("0") ?  "1" : answer );
            }
            else
            {
                InitializeGauge(!string.IsNullOrEmpty(answer) ? answer : "0");
            }
        }

        /// <summary>
        /// Gets the question for spot check result.
        /// </summary>
        private void GetQuestionSpotCheckResult()
        {
            var question = string.Empty;
            var answer = string.Empty;  

            if (CurrentSpotCheckResult.ResultType.Id == _ivSheetLookup.Id)
            {
                question = _mteQuestions[_activeQuestionIndex];

                if (_isUserNavigation)
                {
                    switch (_activeQuestionIndex)
                    {
                        case 0:
                            answer = CurrentSpotCheckResult.NumberOfBags.ToString();
                            break;
                        case 1:
                            answer = CurrentSpotCheckResult.NumberOfWeeks.ToString();
                            break;
                        case 2:
                            answer = CurrentSpotCheckResult.Dosage.ToString();
                            break;
                    }
                }
            }

            if (CurrentSpotCheckResult.ResultType.Id == _mineralsLookup.Id)
            {
                question = _mineralsQuestions[_activeQuestionIndex];

                if (_isUserNavigation)
                {
                    answer = CurrentSpotCheckResult.Dosage.ToString();
                }

            }

            if (CurrentSpotCheckResult.ResultType.Id == _capsolTLookup.Id)
            {
                question = _capsolTQuestions[_activeQuestionIndex];

                if (_isUserNavigation)
                {
                    switch (_activeQuestionIndex)
                    {
                        case 0:
                            answer = CurrentSpotCheckResult.NumberOfBags.ToString();
                            break;
                        case 1:
                            answer = CurrentSpotCheckResult.NumberOfWeeks.ToString();
                            break;
                        case 2:
                            answer = CurrentSpotCheckResult.Dosage.ToString();
                            break;
                    }
                }
            }

            if (CurrentSpotCheckResult.ResultType.Id == _dmpsLookup.Id)
            {
                question = _dmpsQuestions[_activeQuestionIndex];

                if (_isUserNavigation)
                {
                    switch (_activeQuestionIndex)
                    {
                        case 0:
                            answer = CurrentSpotCheckResult.NumberOfBags.ToString();
                            break;
                        case 1:
                            answer = CurrentSpotCheckResult.NumberOfWeeks.ToString();
                            break;
                        case 2:
                            answer = CurrentSpotCheckResult.Dosage.ToString();
                            break;
                    }
                }
            }

            _isUserNavigation = false;

            labelControlQuestion.Text = question;

            if (_activeQuestionIndex == 2 || (CurrentSpotCheckResult.ResultType.Id == _mineralsLookup.Id && _activeQuestionIndex == 0))
            {
                var minimumDosage = CurrentSpotCheckResult.Item.DosageMinimum;

                InitializeGauge(!string.IsNullOrEmpty(answer) ? answer == "0" ?
                                                                minimumDosage ?? StaticKeys.DefaultSpotCheckAnswer :
                                                                answer :
                                                                minimumDosage ?? StaticKeys.DefaultSpotCheckAnswer);
            }
            else
            {
                InitializeGauge(!string.IsNullOrEmpty(answer) ? answer == "0" ? StaticKeys.DefaultSpotCheckAnswer : answer : StaticKeys.DefaultSpotCheckAnswer);
            }
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
        private void XtraFormSpotCheckDialog_Shown(object sender, EventArgs e)
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
        private void XtraFormSpotCheckDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FormClosingEventHandler(XtraFormSpotCheckDialog_FormClosing), sender, e);
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
        private void XtraFormSpotCheckDialog_KeyUp(object sender, KeyEventArgs e)
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
        private void XtraFormSpotCheckDialog_KeyDown(object sender, KeyEventArgs e)
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
