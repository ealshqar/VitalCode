using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using Vital.Hardware.Helpers;

namespace Vital.UI.UI_Components.Forms
{
    /// <summary>
    /// محاكي الجهاز الطبي للتطوير والاختبار
    /// Virtual Medical Device Simulator for Development and Testing
    /// </summary>
    public class VirtualDeviceSimulator
    {
        #region Private Members

        private Thread _simulatorThread;
        private bool _isRunning;
        private readonly Random _random = new Random();
        private int _currentReading = 50; // قراءة افتراضية
        private readonly object _lockObject = new object();

        #endregion

        #region Events

        /// <summary>
        /// حدث عند استقبال قراءة جديدة
        /// </summary>
        public event Action<int> ReadingReceived;

        /// <summary>
        /// حدث عند تغيير حالة الاتصال
        /// </summary>
        public event Action<bool> ConnectionStatusChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// هل المحاكي يعمل حالياً
        /// </summary>
        public bool IsRunning => _isRunning;

        /// <summary>
        /// القراءة الحالية
        /// </summary>
        public int CurrentReading
        {
            get
            {
                lock (_lockObject)
                {
                    return _currentReading;
                }
            }
            private set
            {
                lock (_lockObject)
                {
                    _currentReading = value;
                }
            }
        }

        /// <summary>
        /// نوع المحاكاة
        /// </summary>
        public SimulationType SimulationType { get; set; } = SimulationType.Random;

        #endregion

        #region Public Methods

        /// <summary>
        /// بدء المحاكي
        /// </summary>
        public void Start()
        {
            if (_isRunning) return;

            _isRunning = true;
            _simulatorThread = new Thread(SimulatorLoop)
            {
                IsBackground = true,
                Name = "VirtualDeviceSimulator"
            };
            _simulatorThread.Start();

            ConnectionStatusChanged?.Invoke(true);
        }

        /// <summary>
        /// إيقاف المحاكي
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
            _simulatorThread?.Join(1000); // انتظار ثانية واحدة
            ConnectionStatusChanged?.Invoke(false);
        }

        /// <summary>
        /// تعيين قراءة محددة
        /// </summary>
        /// <param name="reading">القراءة المطلوبة</param>
        public void SetReading(int reading)
        {
            CurrentReading = Math.Max(0, Math.Min(100, reading)); // تحديد النطاق 0-100
        }

        /// <summary>
        /// محاكاة استجابة للأمر
        /// </summary>
        /// <param name="command">الأمر المرسل</param>
        /// <returns>الاستجابة</returns>
        public string SimulateCommand(string command)
        {
            switch (command?.ToUpper())
            {
                case "*IDN?":
                    return "VitalExpert,CSA-Simulator,v1.0,SN123456";
                
                case "READ?":
                    return CurrentReading.ToString();
                
                case "STATUS?":
                    return _isRunning ? "CONNECTED" : "DISCONNECTED";
                
                case "RESET":
                    CurrentReading = 50;
                    return "OK";
                
                default:
                    return "ERROR: Unknown Command";
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// حلقة المحاكي الرئيسية
        /// </summary>
        private void SimulatorLoop()
        {
            while (_isRunning)
            {
                try
                {
                    // توليد قراءة جديدة حسب نوع المحاكاة
                    int newReading = GenerateReading();
                    CurrentReading = newReading;

                    // إرسال القراءة للمستمعين
                    ReadingReceived?.Invoke(newReading);

                    // انتظار قبل القراءة التالية (محاكاة سرعة الجهاز الحقيقي)
                    Thread.Sleep(500); // نصف ثانية بين القراءات
                }
                catch (ThreadAbortException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    // تسجيل الخطأ (يمكن استخدام Logger هنا)
                    System.Diagnostics.Debug.WriteLine($"Simulator Error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// توليد قراءة حسب نوع المحاكاة
        /// </summary>
        /// <returns>القراءة المولدة</returns>
        private int GenerateReading()
        {
            switch (SimulationType)
            {
                case SimulationType.Random:
                    return _random.Next(0, 101); // قراءة عشوائية 0-100

                case SimulationType.Incremental:
                    CurrentReading = (CurrentReading + 1) % 101;
                    return CurrentReading;

                case SimulationType.Sine:
                    // محاكاة موجة جيبية
                    double time = DateTime.Now.Millisecond / 1000.0;
                    return (int)(50 + 30 * Math.Sin(time * Math.PI));

                case SimulationType.Stable:
                    // قراءة ثابتة مع تذبذب طفيف
                    return CurrentReading + _random.Next(-2, 3);

                default:
                    return CurrentReading;
            }
        }

        #endregion

        #region Keyboard Control Support

        /// <summary>
        /// دعم التحكم بلوحة المفاتيح (مثل النظام الأصلي)
        /// </summary>
        /// <returns>قراءة محاكاة حسب المفاتيح المضغوطة</returns>
        public int GetKeyboardControlledReading()
        {
            // محاكاة نفس منطق النظام الأصلي
            if (IsKeyPressed(Keys.LShiftKey))
            {
                return _random.Next(70, 101); // قراءة عالية (Yes)
            }
            else if (IsKeyPressed(Keys.LControlKey))
            {
                return _random.Next(0, 31); // قراءة منخفضة (No)
            }
            else
            {
                return _random.Next(0, 101); // قراءة عشوائية
            }
        }

        /// <summary>
        /// فحص ضغط المفتاح
        /// </summary>
        private bool IsKeyPressed(Keys key)
        {
            return (Control.ModifierKeys & key) == key;
        }

        #endregion
    }

    #region Enums

    /// <summary>
    /// أنواع المحاكاة المتاحة
    /// </summary>
    public enum SimulationType
    {
        /// <summary>
        /// قراءات عشوائية
        /// </summary>
        Random,

        /// <summary>
        /// قراءات متزايدة
        /// </summary>
        Incremental,

        /// <summary>
        /// قراءات على شكل موجة جيبية
        /// </summary>
        Sine,

        /// <summary>
        /// قراءات ثابتة مع تذبذب طفيف
        /// </summary>
        Stable
    }

    #endregion
}