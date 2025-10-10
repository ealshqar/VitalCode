using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using Vital.Hardware.Entities;
using Vital.Hardware.Helpers;

namespace Vital.UI.UI_Components.Forms
{
    /// <summary>
    /// محاكي COM Port افتراضي للتطوير
    /// Virtual COM Port Simulator for Development
    /// </summary>
    public class VirtualComPortSimulator : IDisposable
    {
        #region Private Members

        private readonly Dictionary<string, VirtualPort> _virtualPorts;
        private readonly VirtualDeviceSimulator _deviceSimulator;
        private bool _disposed = false;

        #endregion

        #region Constructor

        public VirtualComPortSimulator()
        {
            _virtualPorts = new Dictionary<string, VirtualPort>();
            _deviceSimulator = new VirtualDeviceSimulator();
            
            // إنشاء منافذ افتراضية للاختبار
            CreateVirtualPorts();
            
            // ربط أحداث المحاكي
            _deviceSimulator.ReadingReceived += OnReadingReceived;
            _deviceSimulator.ConnectionStatusChanged += OnConnectionStatusChanged;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// بدء المحاكي
        /// </summary>
        public void Start()
        {
            _deviceSimulator.Start();
        }

        /// <summary>
        /// إيقاف المحاكي
        /// </summary>
        public void Stop()
        {
            _deviceSimulator.Stop();
        }

        /// <summary>
        /// الحصول على قائمة المنافذ الافتراضية
        /// </summary>
        /// <returns>قائمة المنافذ</returns>
        public List<ComPortInfoEntity> GetVirtualComPorts()
        {
            return _virtualPorts.Values.Select(vp => new ComPortInfoEntity
            {
                Id = vp.Id,
                Name = vp.Name
            }).ToList();
        }

        /// <summary>
        /// محاكاة فتح اتصال مع منفذ
        /// </summary>
        /// <param name="portName">اسم المنفذ</param>
        /// <param name="baudRate">سرعة البيانات</param>
        /// <returns>نجح الاتصال أم لا</returns>
        public bool OpenVirtualConnection(string portName, int baudRate = 9600)
        {
            if (_virtualPorts.ContainsKey(portName))
            {
                var port = _virtualPorts[portName];
                port.IsOpen = true;
                port.BaudRate = baudRate;
                
                // بدء المحاكي إذا لم يكن يعمل
                if (!_deviceSimulator.IsRunning)
                {
                    _deviceSimulator.Start();
                }
                
                return true;
            }
            return false;
        }

        /// <summary>
        /// محاكاة إغلاق الاتصال
        /// </summary>
        /// <param name="portName">اسم المنفذ</param>
        public void CloseVirtualConnection(string portName)
        {
            if (_virtualPorts.ContainsKey(portName))
            {
                _virtualPorts[portName].IsOpen = false;
            }
        }

        /// <summary>
        /// محاكاة إرسال أمر للجهاز
        /// </summary>
        /// <param name="portName">اسم المنفذ</param>
        /// <param name="command">الأمر</param>
        /// <returns>استجابة الجهاز</returns>
        public string SendCommand(string portName, string command)
        {
            if (_virtualPorts.ContainsKey(portName) && _virtualPorts[portName].IsOpen)
            {
                return _deviceSimulator.SimulateCommand(command);
            }
            return "ERROR: Port not open";
        }

        /// <summary>
        /// محاكاة قراءة البيانات من المنفذ
        /// </summary>
        /// <param name="portName">اسم المنفذ</param>
        /// <returns>البيانات المقروءة</returns>
        public string ReadData(string portName)
        {
            if (_virtualPorts.ContainsKey(portName) && _virtualPorts[portName].IsOpen)
            {
                return _deviceSimulator.CurrentReading.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// فحص حالة الاتصال
        /// </summary>
        /// <param name="portName">اسم المنفذ</param>
        /// <returns>حالة الاتصال</returns>
        public bool IsPortOpen(string portName)
        {
            return _virtualPorts.ContainsKey(portName) && _virtualPorts[portName].IsOpen;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// إنشاء منافذ افتراضية للاختبار
        /// </summary>
        private void CreateVirtualPorts()
        {
            // إنشاء منافذ COM افتراضية
            for (int i = 10; i <= 15; i++) // COM10 إلى COM15
            {
                var portName = $"COM{i}";
                _virtualPorts[portName] = new VirtualPort
                {
                    Id = i,
                    Name = portName,
                    IsOpen = false,
                    BaudRate = 9600,
                    Description = $"Virtual Medical Device Port {i}"
                };
            }
        }

        /// <summary>
        /// معالج حدث استقبال قراءة جديدة
        /// </summary>
        private void OnReadingReceived(int reading)
        {
            // يمكن إضافة منطق إضافي هنا
            System.Diagnostics.Debug.WriteLine($"Virtual Device Reading: {reading}");
        }

        /// <summary>
        /// معالج حدث تغيير حالة الاتصال
        /// </summary>
        private void OnConnectionStatusChanged(bool isConnected)
        {
            System.Diagnostics.Debug.WriteLine($"Virtual Device Connection: {(isConnected ? "Connected" : "Disconnected")}");
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _deviceSimulator?.Stop();
                    _deviceSimulator?.Dispose();
                }
                _disposed = true;
            }
        }

        #endregion

        #region Inner Classes

        /// <summary>
        /// منفذ افتراضي
        /// </summary>
        private class VirtualPort
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsOpen { get; set; }
            public int BaudRate { get; set; }
            public string Description { get; set; }
        }

        #endregion
    }
}