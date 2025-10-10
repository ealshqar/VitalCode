using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Vital.UI.UI_Components.Forms
{
    /// <summary>
    /// نافذة التحكم في محاكي الجهاز الطبي
    /// Device Simulator Control Form
    /// </summary>
    public partial class DeviceSimulatorControlForm : XtraForm
    {
        #region Private Members

        private VirtualDeviceSimulator _simulator;
        private Timer _updateTimer;

        #endregion

        #region Constructor

        public DeviceSimulatorControlForm()
        {
            InitializeComponent();
            InitializeSimulator();
            SetupTimer();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// تهيئة المحاكي
        /// </summary>
        private void InitializeSimulator()
        {
            _simulator = new VirtualDeviceSimulator();
            _simulator.ReadingReceived += OnReadingReceived;
            _simulator.ConnectionStatusChanged += OnConnectionStatusChanged;
        }

        /// <summary>
        /// إعداد مؤقت التحديث
        /// </summary>
        private void SetupTimer()
        {
            _updateTimer = new Timer();
            _updateTimer.Interval = 100; // تحديث كل 100ms
            _updateTimer.Tick += UpdateTimer_Tick;
            _updateTimer.Start();
        }

        /// <summary>
        /// معالج حدث المؤقت
        /// </summary>
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (_simulator != null)
            {
                // تحديث القراءة الحالية
                labelCurrentReading.Text = $"Current Reading: {_simulator.CurrentReading}";
                
                // تحديث حالة الاتصال
                labelConnectionStatus.Text = $"Status: {(_simulator.IsRunning ? "Connected" : "Disconnected")}";
                labelConnectionStatus.ForeColor = _simulator.IsRunning ? Color.Green : Color.Red;
                
                // تحديث حالة الأزرار
                buttonStart.Enabled = !_simulator.IsRunning;
                buttonStop.Enabled = _simulator.IsRunning;
            }
        }

        /// <summary>
        /// معالج حدث استقبال قراءة جديدة
        /// </summary>
        private void OnReadingReceived(int reading)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(OnReadingReceived), reading);
                return;
            }
            
            // إضافة القراءة لقائمة السجل
            var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            listBoxReadings.Items.Insert(0, $"[{timestamp}] Reading: {reading}");
            
            // الاحتفاظ بآخر 50 قراءة فقط
            if (listBoxReadings.Items.Count > 50)
            {
                listBoxReadings.Items.RemoveAt(listBoxReadings.Items.Count - 1);
            }
        }

        /// <summary>
        /// معالج حدث تغيير حالة الاتصال
        /// </summary>
        private void OnConnectionStatusChanged(bool isConnected)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(OnConnectionStatusChanged), isConnected);
                return;
            }
            
            var message = isConnected ? "Device Connected" : "Device Disconnected";
            listBoxReadings.Items.Insert(0, $"[{DateTime.Now:HH:mm:ss}] {message}");
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// بدء المحاكي
        /// </summary>
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            _simulator?.Start();
        }

        /// <summary>
        /// إيقاف المحاكي
        /// </summary>
        private void ButtonStop_Click(object sender, EventArgs e)
        {
            _simulator?.Stop();
        }

        /// <summary>
        /// تغيير نوع المحاكاة
        /// </summary>
        private void ComboBoxSimulationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_simulator != null && comboBoxSimulationType.SelectedItem != null)
            {
                var selectedType = (SimulationType)comboBoxSimulationType.SelectedItem;
                _simulator.SimulationType = selectedType;
            }
        }

        /// <summary>
        /// تغيير القراءة يدوياً
        /// </summary>
        private void TrackBarReading_ValueChanged(object sender, EventArgs e)
        {
            if (_simulator != null)
            {
                _simulator.SetReading(trackBarReading.Value);
                labelManualReading.Text = $"Manual Reading: {trackBarReading.Value}";
            }
        }

        /// <summary>
        /// مسح سجل القراءات
        /// </summary>
        private void ButtonClearLog_Click(object sender, EventArgs e)
        {
            listBoxReadings.Items.Clear();
        }

        /// <summary>
        /// إغلاق النافذة
        /// </summary>
        private void DeviceSimulatorControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _updateTimer?.Stop();
            _simulator?.Stop();
            _simulator?.Dispose();
        }

        #endregion

        #region Designer Code

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private SimpleButton buttonStart;
        private SimpleButton buttonStop;
        private SimpleButton buttonClearLog;
        private LabelControl labelCurrentReading;
        private LabelControl labelConnectionStatus;
        private LabelControl labelManualReading;
        private ComboBoxEdit comboBoxSimulationType;
        private TrackBarControl trackBarReading;
        private ListBoxControl listBoxReadings;
        private GroupControl groupControlStatus;
        private GroupControl groupControlControls;
        private GroupControl groupControlLog;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStart = new SimpleButton();
            this.buttonStop = new SimpleButton();
            this.buttonClearLog = new SimpleButton();
            this.labelCurrentReading = new LabelControl();
            this.labelConnectionStatus = new LabelControl();
            this.labelManualReading = new LabelControl();
            this.comboBoxSimulationType = new ComboBoxEdit();
            this.trackBarReading = new TrackBarControl();
            this.listBoxReadings = new ListBoxControl();
            this.groupControlStatus = new GroupControl();
            this.groupControlControls = new GroupControl();
            this.groupControlLog = new GroupControl();
            
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSimulationType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarReading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarReading.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxReadings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlStatus)).BeginInit();
            this.groupControlStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlControls)).BeginInit();
            this.groupControlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlLog)).BeginInit();
            this.groupControlLog.SuspendLayout();
            this.SuspendLayout();

            // Form
            this.Text = "Device Simulator Control - محاكي الجهاز الطبي";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += DeviceSimulatorControlForm_FormClosing;

            // Group Controls
            this.groupControlStatus.Text = "Device Status - حالة الجهاز";
            this.groupControlStatus.Location = new Point(12, 12);
            this.groupControlStatus.Size = new Size(560, 80);

            this.groupControlControls.Text = "Controls - التحكم";
            this.groupControlControls.Location = new Point(12, 98);
            this.groupControlControls.Size = new Size(560, 120);

            this.groupControlLog.Text = "Readings Log - سجل القراءات";
            this.groupControlLog.Location = new Point(12, 224);
            this.groupControlLog.Size = new Size(560, 230);

            // Status Labels
            this.labelCurrentReading.Location = new Point(20, 30);
            this.labelCurrentReading.Text = "Current Reading: 0";
            this.labelCurrentReading.AutoSizeMode = LabelAutoSizeMode.None;
            this.labelCurrentReading.Size = new Size(200, 20);

            this.labelConnectionStatus.Location = new Point(20, 50);
            this.labelConnectionStatus.Text = "Status: Disconnected";
            this.labelConnectionStatus.ForeColor = Color.Red;
            this.labelConnectionStatus.AutoSizeMode = LabelAutoSizeMode.None;
            this.labelConnectionStatus.Size = new Size(200, 20);

            // Control Buttons
            this.buttonStart.Location = new Point(20, 30);
            this.buttonStart.Size = new Size(80, 30);
            this.buttonStart.Text = "Start";
            this.buttonStart.Click += ButtonStart_Click;

            this.buttonStop.Location = new Point(110, 30);
            this.buttonStop.Size = new Size(80, 30);
            this.buttonStop.Text = "Stop";
            this.buttonStop.Click += ButtonStop_Click;

            // Simulation Type
            this.comboBoxSimulationType.Location = new Point(200, 30);
            this.comboBoxSimulationType.Size = new Size(120, 20);
            this.comboBoxSimulationType.Properties.Items.AddRange(Enum.GetValues(typeof(SimulationType)));
            this.comboBoxSimulationType.SelectedIndex = 0;
            this.comboBoxSimulationType.SelectedIndexChanged += ComboBoxSimulationType_SelectedIndexChanged;

            // Manual Reading Control
            this.labelManualReading.Location = new Point(20, 70);
            this.labelManualReading.Text = "Manual Reading: 50";
            this.labelManualReading.AutoSizeMode = LabelAutoSizeMode.None;
            this.labelManualReading.Size = new Size(150, 20);

            this.trackBarReading.Location = new Point(180, 65);
            this.trackBarReading.Size = new Size(200, 45);
            this.trackBarReading.Properties.Minimum = 0;
            this.trackBarReading.Properties.Maximum = 100;
            this.trackBarReading.Value = 50;
            this.trackBarReading.ValueChanged += TrackBarReading_ValueChanged;

            // Readings Log
            this.listBoxReadings.Location = new Point(20, 30);
            this.listBoxReadings.Size = new Size(520, 170);

            this.buttonClearLog.Location = new Point(460, 205);
            this.buttonClearLog.Size = new Size(80, 25);
            this.buttonClearLog.Text = "Clear Log";
            this.buttonClearLog.Click += ButtonClearLog_Click;

            // Add controls to groups
            this.groupControlStatus.Controls.Add(this.labelCurrentReading);
            this.groupControlStatus.Controls.Add(this.labelConnectionStatus);

            this.groupControlControls.Controls.Add(this.buttonStart);
            this.groupControlControls.Controls.Add(this.buttonStop);
            this.groupControlControls.Controls.Add(this.comboBoxSimulationType);
            this.groupControlControls.Controls.Add(this.labelManualReading);
            this.groupControlControls.Controls.Add(this.trackBarReading);

            this.groupControlLog.Controls.Add(this.listBoxReadings);
            this.groupControlLog.Controls.Add(this.buttonClearLog);

            // Add groups to form
            this.Controls.Add(this.groupControlStatus);
            this.Controls.Add(this.groupControlControls);
            this.Controls.Add(this.groupControlLog);

            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSimulationType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarReading.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarReading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxReadings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlStatus)).EndInit();
            this.groupControlStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlControls)).EndInit();
            this.groupControlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlLog)).EndInit();
            this.groupControlLog.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}