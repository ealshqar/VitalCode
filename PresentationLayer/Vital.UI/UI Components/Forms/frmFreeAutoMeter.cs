using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraNavBar;
using DevExpress.XtraRichEdit.API.Word;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.AutoTestSource;
using Vital.Business.Shared.DomainObjects.Hardware;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;

namespace Vital.UI.UI_Components.Forms
{
    public partial class frmFreeAutoMeter : Form
    {
        private readonly AutoTestSourceManager _autoTestSourceManager;
        private List<TestingPoint> _points;
        private List<AutoItem> _broadcastItems;

        public frmFreeAutoMeter()
        {
            _autoTestSourceManager = new AutoTestSourceManager();

            InitializeComponent();
            LoadPoints();
            LoadBroadcastItems();

            AutoCsaEmdUnitManager.Instance.CSAConnected += Instance_CSAConnected;
            AutoCsaEmdUnitManager.Instance.CSADisconnected += Instance_CSADisconnected;
            AutoCsaEmdUnitManager.Instance.CSADetecting += Instance_CSADetecting;
            AutoCsaEmdUnitManager.Instance.PrototypeConnected += Instance_PrototypeConnected;
            AutoCsaEmdUnitManager.Instance.PrototypeDetecting += Instance_PrototypeDetecting;
            AutoCsaEmdUnitManager.Instance.PrototypeDisconnected += Instance_PrototypeDisconnected;
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

        void Instance_PrototypeDisconnected(object sender)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new AutoCsaEmdUnitManager.OnCSAConnected(Instance_PrototypeDisconnected), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                simpleLabelItemPrototypeConnectionStatus.Text = "Prototype Disconnected";
            }
        }

        void Instance_PrototypeDetecting(object sender, int comPortNumber)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new AutoCsaEmdUnitManager.OnCSADetecting(Instance_PrototypeDetecting), sender, comPortNumber);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                simpleLabelItemPrototypeConnectionStatus.Text = "PT: Searching COM Port " + comPortNumber;
            }
        }

        void Instance_PrototypeConnected(object sender)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new AutoCsaEmdUnitManager.OnCSAConnected(Instance_PrototypeConnected), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                simpleLabelItemPrototypeConnectionStatus.Text = "Prototype Connected";
            }
        }

        void Instance_CSADetecting(object sender, int comPortNumber)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new AutoCsaEmdUnitManager.OnCSADetecting(Instance_CSADetecting), sender, comPortNumber);
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
                    Invoke(new AutoCsaEmdUnitManager.OnCSADisconnected(Instance_CSADisconnected), sender);
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
                    Invoke(new AutoCsaEmdUnitManager.OnCSAConnected(Instance_CSAConnected), sender);
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
            var availableComPorts = AutoCsaEmdUnitManager.Instance.GetComPorts().Select(p => p.Name).ToList();
            comboBoxEditPrototypeComPort.Properties.Items.AddRange(availableComPorts);
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
            AutoCsaEmdUnitManager.Instance.SetReadingMode(mode);
        }

        /// <summary>
        /// Start the reading.
        /// </summary>
        private void StartReading(TestingPoint point)
        {
            AutoCsaEmdUnitManager.Instance.Broadcast(_broadcastItems);
            AutoCsaEmdUnitManager.Instance.StartReading(point);
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
            AutoCsaEmdUnitManager.Instance.MeterValueChanged -= _csaManager_MeterValueChanged;
            AutoCsaEmdUnitManager.Instance.ReadingStabled -= _csaManager_ReadingStabled;
        }

        /// <summary>
        /// Add the hardware handlers.
        /// </summary>
        private void AddHardwareHandlers()
        {
            AutoCsaEmdUnitManager.Instance.MeterValueChanged += _csaManager_MeterValueChanged;
            AutoCsaEmdUnitManager.Instance.ReadingStabled += _csaManager_ReadingStabled;
        }

        /// <summary>
        /// Stop the reading.
        /// </summary>
        private void StopReading()
        {
            AutoCsaEmdUnitManager.Instance.StopReading();
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
                Invoke(new AutoCsaEmdUnitManager.OnReadingStabled(_csaManager_ReadingStabled), sender, reading, min, max, fall, rise);
            }
            else
            {
                if (!AutoCsaEmdUnitManager.Instance.HasReading) return;

                LogCurrentReading();

                if (AutoCsaEmdUnitManager.Instance.ReadingMode != AutoCSAReadingMode.Mixed)
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
                    Invoke(new AutoCsaEmdUnitManager.MeterValueChangedHandle(_csaManager_MeterValueChanged), sender,
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
                    AutoCsaEmdUnitManager.Instance.CurrentReading.DateTime.ToString("hh:mm:ss.fff"),
                    AutoCsaEmdUnitManager.Instance.CurrentReading.Value,
                    AutoCsaEmdUnitManager.Instance.CurrentReading.Min,
                    AutoCsaEmdUnitManager.Instance.CurrentReading.Max,
                    AutoCsaEmdUnitManager.Instance.CurrentReading.Stabilized ? "Stabilized" : "Received",
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
                    Invoke(new AutoCsaEmdUnitManager.OnReadingStopped(Csa_Instance_ReadingStopped), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //This check for prevent the released [@UI level only] action get executing twice.
                if (!AutoCsaEmdUnitManager.Instance.HasReading) return;

                xtraUserControlReadingGaugeScheduleLine.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);

                xtraUserControlReadingGaugeScheduleLine.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);

                //StartReading();

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
            AutoCsaEmdUnitManager.Instance.DisposeCSAConnection(_csaManager_MeterValueChanged, Csa_Instance_ReadingStopped, _csaManager_ReadingStabled);
        }

        /// <summary>
        /// Handel the form load to start reading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFreeMeter_Load(object sender, EventArgs e)
        {
            SetBinding();
            AutoCsaEmdUnitManager.Instance.ActivateCSAConnection(_csaManager_MeterValueChanged, Csa_Instance_ReadingStopped, _csaManager_ReadingStabled);
        }

        #endregion

        #endregion


        private void simpleButtonTogglePrototypeConnection_Click(object sender, EventArgs e)
        {
            if (AutoCsaEmdUnitManager.Instance.IsPrototypeConnectionOpen)
            {
                var result = AutoCsaEmdUnitManager.Instance.ClosePrototypeConnection();

                if (result.IsSucceed)
                {
                    simpleButtonTogglePrototypeConnection.Text = "Connect";
                    comboBoxEditPrototypeComPort.Enabled = true;
                    comboBoxEditPoints.Enabled = false;
                    comboBoxEditReadingMode.Enabled = false;
                }
                else
                {
                    MessageBox.Show(result.Message, "Error");
                }
                
            }
            else
            {
//                int comPort;
//                if (!int.TryParse(comboBoxEditPrototypeComPort.SelectedText.Replace("COM", ""), out comPort))
//                {
//                    MessageBox.Show("Please select a COM Port.", "Validation Error");
//                    return;
//                }

                var result = AutoCsaEmdUnitManager.Instance.OpenPrototypeConnection();

                if (result.IsSucceed)
                {
                    simpleButtonTogglePrototypeConnection.Text = "Disconnect";
                    comboBoxEditPrototypeComPort.Enabled = false;
                    comboBoxEditPoints.Enabled = true;
                    comboBoxEditReadingMode.Enabled = true;
                }
                else
                {
                    MessageBox.Show(result.Message, "Error");
                }
            }
        }

        private void simpleButtonToggleReading_Click(object sender, EventArgs e)
        {
            if (AutoCsaEmdUnitManager.Instance.IsReadingOn)
            {
                StopReading();
                simpleButtonToggleReading.Text = "Start Reading";
                comboBoxEditPoints.Enabled = true;
            }
            else
            {
                if (!AutoCsaEmdUnitManager.Instance.IsPrototypeConnectionOpen)
                {
                    MessageBox.Show("Cannot start reading! Please connect the Prototype HW.", "Cannot Start Reading");
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
                comboBoxEditPoints.Enabled = AutoCsaEmdUnitManager.Instance.ReadingMode != AutoCSAReadingMode.StableReading;
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
            if(AutoCsaEmdUnitManager.Instance.ReadingMode == AutoCSAReadingMode.StableReading)
                return;

            var pointName = comboBoxEditPoints.SelectedText;
            var point = _points.FirstOrDefault(p => p.Name.Equals(pointName));
            if (point == null)
            {
                MessageBox.Show("Selected point is not supported.", "Cannot Start Reading");
                return;
            }

            AutoCsaEmdUnitManager.Instance.Broadcast(_broadcastItems);
            AutoCsaEmdUnitManager.Instance.SetPoint(point);
        }

        private void simpleButtonPerformMoistureCehck_Click(object sender, EventArgs e)
        {
            AutoCsaEmdUnitManager.Instance.PerformMoistureCehck();
        }

        private void simpleButtonPerformPressureCheck_Click(object sender, EventArgs e)
        {
            AutoCsaEmdUnitManager.Instance.PerformPressureCheck();
        }


    }
}
