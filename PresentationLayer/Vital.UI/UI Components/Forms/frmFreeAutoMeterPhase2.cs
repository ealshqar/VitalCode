using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.AutoTestSource;
using Vital.Business.Shared.DomainObjects.Hardware;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;

namespace Vital.UI.UI_Components.Forms
{
    public partial class frmFreeAutoMeterPhase2 : Form
    {
        private readonly AutoTestSourceManager _autoTestSourceManager;
        private List<TestingPoint> _points;
        private List<AutoItem> _broadcastItems;

        public frmFreeAutoMeterPhase2()
        {
            _autoTestSourceManager = new AutoTestSourceManager();

            InitializeComponent();
            LoadPoints();
            LoadBroadcastItems();

            AutoCsaEmdUnitManagerPhase2.Instance.Connected += Instance_CSAConnected;
            AutoCsaEmdUnitManagerPhase2.Instance.Disconnected += Instance_CSADisconnected;
            AutoCsaEmdUnitManagerPhase2.Instance.Detecting += Instance_CSADetecting;
            AutoCsaEmdUnitManagerPhase2.Instance.ResponseReceived += Instance_ResponseReceived;
        }

      
        private void LoadPoints()
        {
            _points = _autoTestSourceManager.GetTestingPoints(new TestingPointsFilter()).ToList();
        }

        private void LoadBroadcastItems()
        {
            // 1. Get the default protocol from the DB. We need to change this when we ask the user to select the protocol in future.
            var defaultProtocol = _autoTestSourceManager.GetAutoProtocols(new AutoProtocolsFilter { IsDefaultProtocol = true }).FirstOrDefault();

            if (defaultProtocol == null)
                return;

            // 2. Get the last protocol revision that belong to the selected protocol.
            var latestProtocolRevision =
                _autoTestSourceManager.GetAutoProtocolRevisions(new AutoProtocolRevisionsFilter
                {
                    AutoProtocolsId = defaultProtocol.Id
                }).OrderBy(r => r.CreationDateTime).LastOrDefault();

            var testingStageRevision = latestProtocolRevision.AutoProtocolStageRevisions.FirstOrDefault(r => r.AutoTestStage.Key.Equals(EnumNameResolver.Resolve(StageKey.Testing)));
            
            if(testingStageRevision == null)
                return;

            _broadcastItems = testingStageRevision.StageAutoItemsForStages.Select(s => s.StageAutoItems).First().Select(s => s.AutoItem).ToList();
        }

        
        void Instance_CSADetecting(object sender, int comPortNumber)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new AutoCsaEmdUnitManagerPhase2.OnDetecting(Instance_CSADetecting), sender, comPortNumber);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                simpleLabelItemCSAConnectionStatus.Text = "CSA: Searching COM Port " + comPortNumber;
            }
            
        }

        void Instance_CSADisconnected(object sender)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new AutoCsaEmdUnitManagerPhase2.OnDisconnected(Instance_CSADisconnected), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                simpleLabelItemCSAConnectionStatus.Text = "CSA Disconnected";
            }
        }

        void Instance_CSAConnected(object sender)
        {
            if (InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new AutoCsaEmdUnitManagerPhase2.OnConnected(Instance_CSAConnected), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                simpleLabelItemCSAConnectionStatus.Text = "CSA Connected";
            }
        }

        private void SetBinding()
        {
            BindComPortsComboBoxs();
            BindPointsComboBox();
            BindReadingModesComboBox();
        }

        private void BindComPortsComboBoxs()
        {
            var availableComPorts = AutoCsaEmdUnitManagerPhase2.Instance.GetComPorts().Select(p => p.Name).ToList();
            //comboBoxEditComPort.Properties.Items.AddRange(availableComPorts);
        }

        private void BindPointsComboBox()
        {
            comboBoxEditPoints.Properties.Items.AddRange(_points.Select(p => p.Name).ToList());
        }

        private void BindReadingModesComboBox()
        {
            comboBoxEditReadingMode.Properties.Items.AddRange(new []
            {
                EnumNameResolver.Resolve(AutoCSAReadingMode.Continuous),
                EnumNameResolver.Resolve(AutoCSAReadingMode.StableReading),
                EnumNameResolver.Resolve(AutoCSAReadingMode.Mixed)
            });
        }         
                                                  
        #region HW_Logic                                   

        /// <summary>
        /// Set the reading mode.
        /// </summary>
        /// <param name="mode">The reading mode.</param>
        private void SetReadingMode(AutoCSAReadingMode mode)
        {
            AutoCsaEmdUnitManagerPhase2.Instance.SetReadingMode(mode);
        }

        /// <summary>
        /// Start the reading.
        /// </summary>
        private void StartReading(TestingPoint point)
        {
            AutoCsaEmdUnitManagerPhase2.Instance.Broadcast(_broadcastItems);
            AutoCsaEmdUnitManagerPhase2.Instance.StartReading(point);
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
            AutoCsaEmdUnitManagerPhase2.Instance.MeterValueChanged -= _csaManager_MeterValueChanged;
            AutoCsaEmdUnitManagerPhase2.Instance.ReadingStabled -= _csaManager_ReadingStabled;
        }

        /// <summary>
        /// Add the hardware handlers.
        /// </summary>
        private void AddHardwareHandlers()
        {
            AutoCsaEmdUnitManagerPhase2.Instance.MeterValueChanged += _csaManager_MeterValueChanged;
            AutoCsaEmdUnitManagerPhase2.Instance.ReadingStabled += _csaManager_ReadingStabled;
        }

        /// <summary>
        /// Stop the reading.
        /// </summary>
        private void StopReading()
        {
            AutoCsaEmdUnitManagerPhase2.Instance.StopReading();
        }

        #endregion

        #region Events

        #region HW_Events

        /// <summary>
        /// Handel the meter value changed.
        /// </summary>
        void _csaManager_ReadingStabled(object sender, int reading, int min, int max, int fall, int rise)
        {
            _csaManager_MeterValueChanged(sender, reading, min, max);

            var csaManager = sender as AutoCsaEmdUnitManager;

            if (csaManager != null && csaManager.IsReadingOn == false) return;

            if (InvokeRequired)
            {
                Invoke(new AutoCsaEmdUnitManagerPhase2.OnReadingStabled(_csaManager_ReadingStabled), sender, reading, min, max, fall, rise);
            }
            else
            {
                if (!AutoCsaEmdUnitManagerPhase2.Instance.HasReading) return;

                LogCurrentReading();

                if (AutoCsaEmdUnitManagerPhase2.Instance.ReadingMode != AutoCSAReadingMode.Mixed)
                {
                    // Reading was automatically stopped by the manager because the reading mode is StableReading.
                    simpleButtonToggleReading.Text = "Start Reading";
                    comboBoxEditPoints.Enabled = true;
                }
                
                xtraUserControlReadingGaugeScheduleLine.SetReadingStatusBarMode(TestBarStateEnum.WaitingToRelease, string.Empty, 0);
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
                    Invoke(new AutoCsaEmdUnitManagerPhase2.MeterValueChangedHandle(_csaManager_MeterValueChanged), sender,
                           reading, min, max);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {

                LogCurrentReading();

                xtraUserControlReadingGaugeScheduleLine.ReadingValue = reading;

                xtraUserControlReadingGaugeScheduleLine.SetReadingStatusBarMode(TestBarStateEnum.Reading, string.Empty, 0);

            }

        }

        private void LogCurrentReading()
        {
            memoEditEventsLog.Text += string.Format("{0} >> CurrentReading({1}, {2}, {3}, {4}){5}",
                    AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.DateTime.ToString("hh:mm:ss.fff"),
                    AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Value,
                    AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Min,
                    AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Max,
                    AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Stabilized ? "Stabilized" : "Received",
                    Environment.NewLine);
        }

        private void LogResponse(AutoCSAResponse response)
        {
            memoEditEventsLog.Text += string.Format("{0} >> Response({1}){2}",
                   DateTime.Now.ToString("hh:mm:ss.fff"),
                   response,
                   Environment.NewLine);
        }

        /// <summary>
        /// Handel the CSA reading stopped event.
        /// </summary>
        /// <param name="sender">The sender as CSA manager.</param>
        void Csa_Instance_ReadingStopped(object sender)
        {
            if (InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new AutoCsaEmdUnitManagerPhase2.OnReadingStopped(Csa_Instance_ReadingStopped), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //This check for prevent the released [@UI level only] action get executing twice.
                if (!AutoCsaEmdUnitManagerPhase2.Instance.HasReading) return;

                xtraUserControlReadingGaugeScheduleLine.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);

                xtraUserControlReadingGaugeScheduleLine.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);

                //StartReading();

            }
        }

        void Instance_ResponseReceived(object sender, AutoCSAResponse response, string originData)
        {
            if (InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new AutoCsaEmdUnitManagerPhase2.OnResponseReceived(Instance_ResponseReceived), sender, response, originData);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                LogResponse(response);
            }
            
        }

        #endregion

        #region UI_Events

        /// <summary>
        /// Handel the form closing to stop the reading and remove the events handlers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormProductDosages_FormClosing(object sender, FormClosingEventArgs e)
        {
            AutoCsaEmdUnitManagerPhase2.Instance.DisposeCSAConnection(_csaManager_MeterValueChanged, Csa_Instance_ReadingStopped, _csaManager_ReadingStabled);
        }

        /// <summary>
        /// Handel the form load to start reading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFreeMeter_Load(object sender, EventArgs e)
        {
            SetBinding();
            AutoCsaEmdUnitManagerPhase2.Instance.ActivateCSAConnection(_csaManager_MeterValueChanged, Csa_Instance_ReadingStopped, _csaManager_ReadingStabled);
        }

        #endregion

        #endregion


//        private void simpleButtonToggleConnection_Click(object sender, EventArgs e)
//        {
//            if (AutoCsaEmdUnitManagerPhase2.Instance.IsConnectionOpen)
//            {
//                var result = AutoCsaEmdUnitManagerPhase2.Instance.CloseCSAConnection();
//
//                if (result.IsSucceed)
//                {
//                    simpleButtonToggleConnection.Text = "Connect";
//                    comboBoxEditComPort.Enabled = true;
//                    comboBoxEditPoints.Enabled = false;
//                    comboBoxEditReadingMode.Enabled = false;
//                }
//                else
//                {
//                    MessageBox.Show(result.Message, "Error");
//                }
//                
//            }
//            else
//            {
//
//                var result = AutoCsaEmdUnitManagerPhase2.Instance.OpenCSAConnection();
//
//                if (result.IsSucceed)
//                {
//                    simpleButtonToggleConnection.Text = "Disconnect";
//                    comboBoxEditComPort.Enabled = false;
//                    comboBoxEditPoints.Enabled = true;
//                    comboBoxEditReadingMode.Enabled = true;
//                }
//                else
//                {
//                    MessageBox.Show(result.Message, "Error");
//                }
//            }
//        }

        private void simpleButtonToggleReading_Click(object sender, EventArgs e)
        {
            if (AutoCsaEmdUnitManagerPhase2.Instance.IsReadingOn)
            {
                StopReading();
                simpleButtonToggleReading.Text = "Start Reading";
                comboBoxEditPoints.Enabled = true;
            }
            else
            {
                if (!AutoCsaEmdUnitManagerPhase2.Instance.IsConnectionOpen)
                {
                    MessageBox.Show("Cannot start reading! Please connect the CSA.", "Cannot Start Reading");
                    return;
                }

                var pointName = comboBoxEditPoints.SelectedText;
                
                if (string.IsNullOrEmpty(pointName))
                {
                    MessageBox.Show("Please select a point.", "Validation Error");
                    return;
                }
                
                var point = AutoTestingPoint.ToArray().FirstOrDefault(p => p.Name.Equals(pointName));
                if (point == null)
                {
                    MessageBox.Show("Selected point is not supported.", "Cannot Start Reading");
                    return;
                }

                
                StartReading(_points.First(p => p.Name.Equals(pointName)));

                simpleButtonToggleReading.Text = "Stop Reading";
                comboBoxEditPoints.Enabled = AutoCsaEmdUnitManagerPhase2.Instance.ReadingMode != AutoCSAReadingMode.StableReading;
            }
            
        }

        private void simpleButtonSetReadingMode_Click(object sender, EventArgs e)
        {
            var readingModeStr = comboBoxEditReadingMode.SelectedText;

            if (string.IsNullOrEmpty(readingModeStr))
            {
                MessageBox.Show("Please select the reading mode.", "Validation Error");
                return;
            }

            SetReadingMode(EnumNameResolver.LookupAsEnum<AutoCSAReadingMode>(readingModeStr));

        }

        private void comboBoxEditPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AutoCsaEmdUnitManagerPhase2.Instance.ReadingMode == AutoCSAReadingMode.StableReading)
                return;

            var pointName = comboBoxEditPoints.SelectedText;
            var point = _points.FirstOrDefault(p => p.Name.Equals(pointName));
            if (point == null)
            {
                MessageBox.Show("Selected point is not supported.", "Cannot Start Reading");
                return;
            }

            AutoCsaEmdUnitManagerPhase2.Instance.Broadcast(_broadcastItems);
            AutoCsaEmdUnitManagerPhase2.Instance.SetPoint(point);
        }

        private void simpleButtonPerformMoistureCehck_Click(object sender, EventArgs e)
        {
            AutoCsaEmdUnitManagerPhase2.Instance.PerformMoistureCehck();
        }

        private void simpleButtonPerformPressureCheck_Click(object sender, EventArgs e)
        {
            AutoCsaEmdUnitManagerPhase2.Instance.PerformPressureCheck();
        }

        private void simpleButtonPerformHingeCheck_Click(object sender, EventArgs e)
        {
            AutoCsaEmdUnitManagerPhase2.Instance.PerformHingeCheck();
        }

        private void simpleButtonRest_Click(object sender, EventArgs e)
        {
            AutoCsaEmdUnitManagerPhase2.Instance.PerformReset();
        }

        private void simpleButtonManualMode_Click(object sender, EventArgs e)
        {
            AutoCsaEmdUnitManagerPhase2.Instance.ActivateManualMode();
        }

        private void simpleButtonAutomationMode_Click(object sender, EventArgs e)
        {
            AutoCsaEmdUnitManagerPhase2.Instance.ActivateAutomationMode();
        }

        private void simpleButtonStartAutomation_Click(object sender, EventArgs e)
        {
            AutoCsaEmdUnitManagerPhase2.Instance.StartAutomation();
        }

        private void simpleButtonStopAutomation_Click(object sender, EventArgs e)
        {
            AutoCsaEmdUnitManagerPhase2.Instance.StopAutomation();
        }

        private void simpleButtonActivateImprinting_Click(object sender, EventArgs e)
        {
            AutoCsaEmdUnitManagerPhase2.Instance.ActivateImprintingMode();
        }

        private void simpleButtonActivateTopPlate_Click(object sender, EventArgs e)
        {
            AutoCsaEmdUnitManagerPhase2.Instance.ActivateTopPlate();
        }

        
    }
}
