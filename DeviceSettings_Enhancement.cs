// تحسينات إضافية لإعدادات الجهاز الحديث

// 1. إضافة إعدادات خاصة للجهاز الحديث في SerialPortConnectionFilter
public class ModernDeviceConnectionFilter : SerialPortConnectionFilter
{
    public ModernDeviceConnectionFilter() : base(HardwareType.CSA)
    {
        // إعدادات محسنة للجهاز الحديث
        BaudRate = 9600;      // سرعة أعلى
        DataBit = 8;
        Timeout = 5000;       // مهلة أطول
        Dtr = false;
        Rts = true;
        
        // إعدادات إضافية للجهاز الحديث
        ReadTimeout = 3000;
        WriteTimeout = 3000;
    }
    
    public int ReadTimeout { get; set; }
    public int WriteTimeout { get; set; }
}

// 2. تحسين آلية كشف نوع الجهاز
public static class DeviceDetectionHelper
{
    public static DeviceType DetectDeviceType(IAutoCsaEmdUnitHardwareRepository repository)
    {
        try
        {
            // إرسال أمر تحقق للجهاز الحديث
            var resetResult = repository.SendCommand(AutoCSAProtocol.Commands[AutoCSACommand.Reset]);
            
            if (resetResult.IsSucceed)
            {
                // انتظار الاستجابة
                Thread.Sleep(1000);
                
                // التحقق من الاستجابة
                // إذا كان الجهاز يرد على أوامر AutoCSA فهو جهاز حديث
                return DeviceType.ModernAutoCSA;
            }
        }
        catch
        {
            // إذا فشل، قد يكون جهاز قديم
        }
        
        return DeviceType.LegacyCSA;
    }
}

public enum DeviceType
{
    LegacyCSA,
    ModernAutoCSA,
    Unknown
}

// 3. تحسين معالجة البيانات المستقبلة
public class EnhancedDataProcessor
{
    private DateTime _lastDataReceived = DateTime.MinValue;
    private readonly object _lockObject = new object();
    
    public bool ProcessIncomingData(string data)
    {
        lock (_lockObject)
        {
            _lastDataReceived = DateTime.Now;
            
            // معالجة خاصة للجهاز الحديث
            if (IsModernDeviceData(data))
            {
                return ProcessModernDeviceData(data);
            }
            
            // معالجة الجهاز القديم
            return ProcessLegacyDeviceData(data);
        }
    }
    
    private bool IsModernDeviceData(string data)
    {
        // التحقق من أنماط البيانات الخاصة بالجهاز الحديث
        return AutoCSAProtocol.Responses.ContainsKey(data) ||
               data.All(char.IsDigit) ||
               data.StartsWith("E") || // إشارة النشاط
               data.Length > 0;
    }
    
    private bool ProcessModernDeviceData(string data)
    {
        // معالجة بيانات الجهاز الحديث
        if (AutoCSAProtocol.Responses.ContainsKey(data))
        {
            // استجابة صحيحة من الجهاز الحديث
            return true;
        }
        
        if (int.TryParse(data, out int reading))
        {
            // قراءة رقمية صحيحة
            return reading >= 0 && reading <= 100;
        }
        
        return false;
    }
    
    private bool ProcessLegacyDeviceData(string data)
    {
        // معالجة بيانات الجهاز القديم
        return !string.IsNullOrWhiteSpace(data);
    }
    
    public bool IsDeviceResponsive(TimeSpan timeout)
    {
        return DateTime.Now - _lastDataReceived <= timeout;
    }
}

// 4. إعدادات محسنة للاتصال
public static class ConnectionSettings
{
    public static class ModernDevice
    {
        public const int BaudRate = 9600;
        public const int DataBits = 8;
        public const int Timeout = 5000;
        public const int DisconnectedTimeout = 8000;
        public const int ReadingStabilityTimeout = 2000;
        public const string AliveStreamData = null; // قبول أي بيانات
        
        public static SerialPortConnectionFilter GetConnectionFilter()
        {
            return new SerialPortConnectionFilter(HardwareType.CSA)
            {
                BaudRate = BaudRate,
                DataBit = DataBits,
                Timeout = Timeout,
                Dtr = false,
                Rts = true,
                AutoComPortDetection = true
            };
        }
    }
    
    public static class LegacyDevice
    {
        public const int BaudRate = 1200;
        public const int DataBits = 8;
        public const int Timeout = 1500;
        public const int DisconnectedTimeout = 3000;
        
        public static SerialPortConnectionFilter GetConnectionFilter()
        {
            return new SerialPortConnectionFilter(HardwareType.CSA)
            {
                BaudRate = BaudRate,
                DataBit = DataBits,
                Timeout = Timeout,
                Dtr = false,
                Rts = true,
                AutoComPortDetection = true
            };
        }
    }
}