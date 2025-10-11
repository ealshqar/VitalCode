# تفاصيل الاتصال بالجهاز الحديث - Auto CSA Phase 2

## 🎯 نظرة شاملة

**الجهاز**: Auto CSA Phase 2  
**Manager**: `AutoCsaEmdUnitManagerPhase2`  
**Repository**: `AutoCsaEmdUnitHardwareRepository`  
**الموقع**: `BusinessLayer/Vital.Business/Managers/AutoCsaEmdUnitManagerPhase2.cs`

---

## 📊 مواصفات الجهاز

### الجهاز الحديث (Auto CSA Phase 2)

```
┌─────────────────────────────────────────┐
│      Auto CSA Device (Phase 2)          │
├─────────────────────────────────────────┤
│                                         │
│  🔌 اتصال واحد موحّد                    │
│  📡 COM Port واحد فقط                   │
│  🎛️ تحكم كامل متكامل                  │
│                                         │
│  المكونات الداخلية:                     │
│  ├─ CSA Component (القراءات)           │
│  └─ Prototype Component (التحكم)       │
│     (موحدة في جهاز واحد)                │
│                                         │
└─────────────────────────────────────────┘
```

**الميزة الرئيسية**: جمع CSA و Prototype في جهاز واحد! ✨

---

## 🔌 إعدادات الاتصال

### Serial Port Configuration

```csharp
// الإعدادات التلقائية عند الاتصال
var filter = new SerialPortConnectionFilter(HardwareType.CSA)
{
    BaudRate = 1200,           // سرعة النقل
    DataBit = 8,               // بت البيانات
    Timeout = 2000,            // وقت الانتظار (2 ثانية)
    Dtr = false,               // Data Terminal Ready
    Rts = true,                // Request To Send
    Parity = None,             // لا يوجد Parity
    StopBits = 1               // بت التوقف
};
```

### الإعدادات من قاعدة البيانات

```sql
-- في جدول Setting
Key: CommunicationsPort
Value: 3  -- رقم المنفذ (COM3)

-- إذا كانت القيمة 0 → Auto Detection
```

### Hardware Profile Settings

```csharp
// من HwProfile Table
MinimumReadingtoRegister: 46      // أقل قراءة للتسجيل
ReadingStabilityRange: 5          // نطاق الاستقرار
ReadingStabilityTimeout: 1000     // وقت الاستقرار (ms)
DisconnectedTimeout: 5000         // وقت انتظار الفصل (ms)
```

---

## 🚀 عملية الاتصال الكاملة

### خطوة 1: التهيئة

```csharp
// عند إنشاء الـ Manager
public AutoCsaEmdUnitManagerPhase2()
{
    // 1. إنشاء Repository
    _autoCsaEmdUnitRepository = new AutoCsaEmdUnitHardwareRepository();
    
    // 2. إنشاء Setting Repository
    _settingRepository = new SettingDatabaseRepository();
    
    // 3. تعبئة Lookup IDs
    FillLookupIds();
    
    // 4. تحميل الإعدادات من قاعدة البيانات
    RefreshSettings();
    
    // 5. تهيئة CurrentReading
    CurrentReading = new AutoCSAReadingModel(0, 0, 0);
}
```

### خطوة 2: تحميل الإعدادات

```csharp
public void RefreshSettings()
{
    // إغلاق الاتصال الحالي أولاً
    CloseCSAConnection();
    
    // 1. تحميل Hardware Profile
    RefreshHwProfileSettings();
    //    ├─→ MinimumReadingtoRegister
    //    ├─→ ReadingStabilityRange
    //    ├─→ ReadingStabilityTimeout
    //    └─→ DisconnectedTimeout
    
    // 2. تحميل COM Port Number
    RefreshPortNumberSettings();
    //    ├─→ CommunicationsPort → ComPortNumber
    //    └─→ إذا = 0 → AutoComPortDetection = true
    
    // 3. إعدادات أخرى
    EdsAutoPlayWaitingTime = Load(SettingKeys.EdsNextPointNavigationTime);
    BeepDuration = Load(SettingKeys.Duration);
    BeepFrequency = Load(SettingKeys.Frequency);
    IsBroadcastingOn = Load(SettingKeys.BroadcastingStatus);
    BroadcastMethodology = Load(SettingKeys.BroadcastMethodology);
}
```

### خطوة 3: فتح الاتصال

```csharp
// الطريقة الأولى: اتصال بسيط
public ProcessResult OpenCSAConnection()
{
    // 1. إنشاء Filter بالإعدادات المحملة
    var filter = new SerialPortConnectionFilter(HardwareType.CSA)
    {
        Timeout = 2000,
        ComPortNumber = ComPortNumber,              // من Settings
        AutoComPortDetection = AutoComPortDetection // من Settings
    };
    
    // 2. فتح الاتصال
    return OpenCSAConnection(filter);
}

// الطريقة الثانية: اتصال بإعدادات مخصصة
public ProcessResult OpenCSAConnection(SerialPortConnectionFilter filter)
{
    try
    {
        // 1. تهيئة Event Handlers
        InitCSAHardwareHandlers();
        
        // 2. فتح الاتصال عبر Repository
        var result = _autoCsaEmdUnitRepository.OpenConnection(
            filter.ComPortNumber,
            filter.BaudRate,      // 1200
            filter.DataBit,       // 8
            filter.Timeout,       // 2000
            filter.Dtr,           // false
            filter.Rts,           // true
            filter.AutoComPortDetection
        );
        
        return result;
    }
    catch (Exception ex)
    {
        throw new VitalHardwareException(ex);
    }
}
```

### خطوة 4: تهيئة Event Handlers

```csharp
private void InitCSAHardwareHandlers()
{
    // 1. إزالة أي handlers قديمة
    _autoCsaEmdUnitRepository.Disconnected -= _autoCsaEmdUnitRepository_Disconnected;
    _autoCsaEmdUnitRepository.Connected -= _autoCsaEmdUnitRepository_Connected;
    _autoCsaEmdUnitRepository.ReadingStopped -= _autoCsaEmdUnitRepository_ReadingStopped;
    _autoCsaEmdUnitRepository.Detecting -= _autoCsaEmdUnitRepository_Detecting;
    _autoCsaEmdUnitRepository.ResponseReceived -= _autoCsaEmdUnitRepository_ResponseReceived;
    
    // 2. إضافة handlers جديدة
    _autoCsaEmdUnitRepository.Disconnected += _autoCsaEmdUnitRepository_Disconnected;
    _autoCsaEmdUnitRepository.Connected += _autoCsaEmdUnitRepository_Connected;
    _autoCsaEmdUnitRepository.ReadingStopped += _autoCsaEmdUnitRepository_ReadingStopped;
    _autoCsaEmdUnitRepository.Detecting += _autoCsaEmdUnitRepository_Detecting;
    _autoCsaEmdUnitRepository.ResponseReceived += _autoCsaEmdUnitRepository_ResponseReceived;
}
```

---

## 🔄 التدفق التفصيلي للاتصال

```
المستخدم/التطبيق
    │ يستدعي
    ▼
AutoCsaEmdUnitManagerPhase2.OpenCSAConnection()
    │
    ├─→ InitCSAHardwareHandlers()
    │   └─ تهيئة جميع Event Handlers
    │
    └─→ Repository.OpenConnection()
        │
        ├─→ HwCommunicationHelper.Open()
        │   │
        │   ├─→ _serialPort = new SerialPort()
        │   ├─→ _serialPort.PortName = "COM3"
        │   ├─→ _serialPort.BaudRate = 1200
        │   ├─→ _serialPort.DataBits = 8
        │   ├─→ _serialPort.Parity = None
        │   ├─→ _serialPort.StopBits = 1
        │   ├─→ _serialPort.DtrEnable = false
        │   ├─→ _serialPort.RtsEnable = true
        │   ├─→ _serialPort.ReadTimeout = 2000
        │   │
        │   ├─→ _serialPort.DataReceived += OnDataReceived
        │   │
        │   └─→ _serialPort.Open()
        │       │
        │       └─→ [نجح؟]
        │           ├─ نعم → _isConnectionOpen = true
        │           └─ لا → Exception
        │
        ├─→ بدء Thread المراقبة
        │   │
        │   └─→ _connectionCheckerThread = new Thread()
        │       └─→ مراقبة استمرار الاتصال
        │
        └─→ Connected Event ✓
            │
            └─→ Manager.Connected Handler
                │
                └─→ _autoCsaEmdUnitRepository.OpenReading()
                    └─→ بدء استقبال القراءات تلقائياً
```

---

## 📡 استقبال البيانات

### آلية استقبال البيانات من الجهاز

```
Serial Port DataReceived Event
    │
    ▼
HwCommunicationHelper.OnDataReceived()
    │
    ├─→ قراءة Bytes من Port
    │   └─→ _serialPort.BytesToRead
    │
    ├─→ تحويل إلى String
    │   └─→ Encoding.ASCII.GetString(buffer)
    │
    └─→ تحديد نوع البيانات:
        │
        ├─→ [قراءة رقمية؟] (Hex: 00xx)
        │   │
        │   └─→ ProcessReading()
        │       ├─→ تحويل من Hex → Decimal
        │       ├─→ MeterValueChanged Event
        │       └─→ التحقق من الاستقرار
        │           └─→ ReadingStabled Event
        │
        └─→ [استجابة أمر؟] (حرف واحد: H, L, P, إلخ)
            │
            └─→ ProcessResponse()
                ├─→ البحث في AutoCSAProtocol.Responses
                └─→ ResponseReceived Event
```

### معالجة القراءات

```csharp
void OnDataReceived(string data)
{
    lock (_lockDataReceived)
    {
        // 1. تحديث وقت آخر بيانات
        _lastComEventDateTime = DateTime.Now;
        
        // 2. هل هي قراءة؟
        if (IsReadingData(data))
        {
            // تحويل من Hex
            var reading = ConvertHexToReading(data);
            
            // التحقق من الحد الأدنى
            if (reading >= MinimumReadingtoRegister)
            {
                _currentReading = reading;
                
                // حساب Min/Max
                UpdateMinMax(reading);
                
                // إطلاق MeterValueChanged
                MeterValueChanged?.Invoke(this, reading, _min, _max);
                
                // التحقق من الاستقرار
                if (ReadingStabilityEnabled)
                {
                    CheckStability(reading);
                }
            }
        }
        // 3. أم استجابة؟
        else if (IsResponseData(data))
        {
            // البحث في قاموس الاستجابات
            if (AutoCSAProtocol.Responses.ContainsKey(data))
            {
                var response = AutoCSAProtocol.Responses[data];
                
                // إطلاق ResponseReceived
                ResponseReceived?.Invoke(this, response, data);
            }
        }
    }
}
```

---

## 🎛️ الأوضاع والحالات (Modes & States)

### AutoCSAMode - أوضاع الجهاز

```csharp
public enum AutoCSAMode
{
    Disconnected,    // غير متصل
    Idle,            // خامل (متصل لكن غير نشط)
    Manual,          // وضع يدوي
    Automation       // وضع آلي
}

// الوضع الحالي
public AutoCSAMode CurrentMode { get; private set; }
```

### الحالات التفصيلية (Detailed States)

```csharp
// حالات الأتمتة
public bool? IsAutomationStarted { get; private set; }       // بدأت الأتمتة؟
public bool? IsImprintingActive { get; private set; }        // Imprinting نشط؟
public bool? IsTopPlateActive { get; private set; }          // اللوح العلوي نشط؟

// حالات الفحوصات
public bool? HasValidHinge { get; private set; }             // المفصل سليم؟
public bool? HasValidMoisture { get; private set; }          // الرطوبة جيدة؟
public bool? HasValidPressure { get; private set; }          // الضغط جيد؟
public bool AreProbesConnected { get; private set; }         // المجسات متصلة؟

// حالات القراءة
public bool IsReadingOn { get; private set; }                // القراءة نشطة؟
public bool HasReading { get; }                              // توجد قراءة؟
```

---

## 📨 الأوامر المتاحة (Commands)

### جميع الأوامر

```csharp
// 1. إعادة التعيين
PerformReset()
    └─→ Send: "R"

// 2. الأوضاع
ActivateManualMode()
    └─→ Send: "N"

ActivateAutomationMode()
    └─→ Send: "A"

// 3. الأتمتة
StartAutomation()
    └─→ Send: "S"

StopAutomation()
    └─→ Send: "O"

// 4. الفحوصات
PerformHingeCheck()
    └─→ Send: "H"

PerformMoistureCehck()  // لاحظ الخطأ الإملائي في الكود
    └─→ Send: "M"

PerformPressureCheck()
    └─→ Send: "P"

// 5. الطباعة الطاقية
ActivateImprintingMode()
    └─→ Send: "I"

ActivateTopPlate()
    └─→ Send: "T"

// 6. تحديد النقطة
SetPoint(TestingPoint point)
    └─→ Send: "Pxx"  // xx = HWIdentifier بـ Hex
```

---

## 📥 الاستجابات من الجهاز (Responses)

### معالجة الاستجابات

```csharp
void _autoCsaEmdUnitRepository_ResponseReceived(
    object sender, 
    AutoCSAResponse response, 
    string originData)
{
    switch (response)
    {
        case AutoCSAResponse.IdleMode:  // "E"
            CurrentMode = AutoCSAMode.Idle;
            IsImprintingActive = false;
            IsTopPlateActive = false;
            IsAutomationStarted = false;
            HasValidHinge = false;
            HasValidPressure = false;
            HasValidMoisture = false;
            AreProbesConnected = true;
            break;
            
        case AutoCSAResponse.IdleAutomationMode:  // "O"
            CurrentMode = AutoCSAMode.Automation;
            IsAutomationStarted = false;
            break;
            
        case AutoCSAResponse.ImprintingModeActivated:  // "I"
            IsImprintingActive = true;
            break;
            
        case AutoCSAResponse.ValidHinge:  // "H"
            HasValidHinge = true;
            break;
            
        case AutoCSAResponse.InvalidHinge:  // "Z"
            HasValidHinge = false;
            break;
            
        case AutoCSAResponse.ValidMoisture:  // "L"
            HasValidMoisture = true;
            break;
            
        case AutoCSAResponse.InvalidMoisture:  // "X"
            HasValidMoisture = false;
            break;
            
        case AutoCSAResponse.ValidPressure:  // "P"
            HasValidPressure = true;
            break;
            
        case AutoCSAResponse.InvalidPressure:  // "Y"
            HasValidPressure = false;
            break;
            
        case AutoCSAResponse.ManualProbesDisconnected:  // "W"
            AreProbesConnected = false;
            break;
    }
    
    // إعادة إطلاق الحدث للطبقات العليا
    ResponseReceived?.Invoke(this, response, originData);
}
```

### جدول الاستجابات الكامل

| الاستجابة | الحرف | الخاصية المُحدثة | القيمة |
|-----------|-------|------------------|--------|
| IdleMode | E | CurrentMode | Idle |
| IdleAutomationMode | O | CurrentMode | Automation |
| ImprintingModeActivated | I | IsImprintingActive | true |
| ValidHinge | H | HasValidHinge | true ✓ |
| InvalidHinge | Z | HasValidHinge | false ✗ |
| ValidMoisture | L | HasValidMoisture | true ✓ |
| InvalidMoisture | X | HasValidMoisture | false ✗ |
| ValidPressure | P | HasValidPressure | true ✓ |
| InvalidPressure | Y | HasValidPressure | false ✗ |
| ManualProbesDisconnected | W | AreProbesConnected | false ✗ |

---

## 🎯 Events (الأحداث)

### جميع الأحداث المتاحة

```csharp
// 1. أحداث الاتصال
public event OnConnected Connected;
public event OnDisconnected Disconnected;
public event OnDetecting Detecting;

// 2. أحداث القراءة
public event MeterValueChangedHandle MeterValueChanged;
public event OnReadingStabled ReadingStabled;
public event OnReadingStopped ReadingStopped;

// 3. أحداث أخرى
public event OnConnectionResetFinishedHandle ConnectionResetFinished;
public event OnResponseReceived ResponseReceived;  // ⭐ الأهم
```

### Event Delegates (التوقيعات)

```csharp
// الاتصال
public delegate void OnConnected(object sender);
public delegate void OnDisconnected(object sender);
public delegate void OnDetecting(object sender, int comPortNumber);

// القراءة
public delegate void MeterValueChangedHandle(
    object sender, 
    int reading, 
    int min, 
    int max
);

public delegate void OnReadingStabled(
    object sender, 
    int reading, 
    int min, 
    int max, 
    int fall, 
    int rise
);

public delegate void OnReadingStopped(object sender);

// الاستجابات ⭐
public delegate void OnResponseReceived(
    object sender, 
    AutoCSAResponse response,  // نوع الاستجابة
    string originData          // البيانات الأصلية
);
```

---

## 🔄 سير العمل الكامل

### سيناريو: فحص آلي كامل

```
الخطوة 1: الاتصال
──────────────────
Code:
    var manager = AutoCsaEmdUnitManagerPhase2.Instance;
    manager.RefreshSettings();
    var result = manager.OpenCSAConnection();

Flow:
    ├─→ فتح Serial Port
    ├─→ بدء Threads المراقبة
    └─→ Connected Event
        └─→ فتح القراءة تلقائياً


الخطوة 2: تفعيل الوضع الآلي
──────────────────────────────
Code:
    manager.ActivateAutomationMode();

Flow:
    ├─→ Send: "A"
    ├─→ انتظار...
    └─→ ResponseReceived("O") → IdleAutomationMode
        └─→ CurrentMode = Automation


الخطوة 3: الفحوصات الأولية
────────────────────────────
Code:
    manager.PerformHingeCheck();
    manager.PerformMoistureCheck();
    manager.PerformPressureCheck();

Flow:
    ├─→ HingeCheck
    │   ├─→ Send: "H"
    │   └─→ Receive: "H" → HasValidHinge = true ✓
    │
    ├─→ MoistureCheck
    │   ├─→ Send: "M"
    │   └─→ Receive: "L" → HasValidMoisture = true ✓
    │
    └─→ PressureCheck
        ├─→ Send: "P"
        └─→ Receive: "P" → HasValidPressure = true ✓


الخطوة 4: تفعيل اللوح العلوي
──────────────────────────────
Code:
    manager.ActivateTopPlate();

Flow:
    ├─→ Send: "T"
    ├─→ _waitingActivateTopPlate = true
    ├─→ انتظار قراءة...
    └─→ MeterValueChanged
        └─→ IsTopPlateActive = true


الخطوة 5: بدء الأتمتة
───────────────────────
Code:
    manager.StartAutomation();

Flow:
    ├─→ Send: "S"
    ├─→ _waitingStartAutomation = true
    ├─→ انتظار قراءة...
    └─→ MeterValueChanged
        └─→ IsAutomationStarted = true


الخطوة 6: المسح (للكل نقطة)
────────────────────────────
Code:
    foreach (var point in points)
    {
        manager.SetPoint(point);
        manager.StartReading();
        // انتظار ReadingStabled
        // حفظ النتيجة
    }

Flow (لكل نقطة):
    │
    ├─→ SetPoint(point)
    │   ├─→ Send: "P05"  // مثال
    │   └─→ CurrentTestingPoint = point
    │
    ├─→ StartReading()
    │   ├─→ OpenReading()
    │   ├─→ Subscribe to Events
    │   └─→ IsReadingOn = true
    │
    ├─→ [الجهاز يرسل قراءات...]
    │   ├─→ DataReceived: "00A0" → 80
    │   ├─→ MeterValueChanged(80, 75, 85)
    │   ├─→ DataReceived: "00A0" → 80
    │   ├─→ MeterValueChanged(80, 75, 85)
    │   └─→ (مستمر...)
    │
    ├─→ [التحقق من الاستقرار]
    │   ├─→ الفرق < ReadingStabilityRange (5)؟
    │   └─→ الوقت > ReadingStabilityTimeout (1000ms)؟
    │       └─→ نعم → ReadingStabled Event
    │           └─→ reading=80, min=75, max=85
    │
    ├─→ StopReading()
    │   └─→ IsReadingOn = false
    │
    └─→ حفظ النتيجة في DB


الخطوة 7: إنهاء الأتمتة
────────────────────────
Code:
    manager.StopAutomation();

Flow:
    └─→ Send: "O"
```

---

## 🔍 Auto Detection (الكشف التلقائي)

### كيف يعمل Auto Detection

```csharp
// عند ComPortNumber = 0
public ProcessResult OpenConnection(
    int comPortNumber, 
    ..., 
    bool? isAutoPortDetection)
{
    if (isAutoPortDetection == true || comPortNumber == 0)
    {
        // بدء Auto Detection
        _autoConnectionThread = new Thread(AutoDetectionThreadWorker);
        _autoConnectionThread.Start();
    }
    else
    {
        // اتصال مباشر
        return DirectConnect(comPortNumber);
    }
}

private void AutoDetectionThreadWorker()
{
    // 1. الحصول على جميع المنافذ المتاحة
    var availablePorts = _hwCommunicationHelper.GetComPorts();
    
    // 2. محاولة الاتصال بكل منفذ
    foreach (var port in availablePorts)
    {
        // إطلاق حدث البحث
        Detecting?.Invoke(this, port.Id);
        
        try
        {
            // محاولة الاتصال
            var result = _hwCommunicationHelper.Open(
                port.Id, 
                1200,  // BaudRate
                8,     // DataBits
                ...
            );
            
            if (result)
            {
                // انتظار للتحقق من وجود بيانات
                Thread.Sleep(1000);
                
                // هل استُقبلت بيانات؟
                if (_lastComEventDateTime != null)
                {
                    // نجح الاتصال! ✓
                    ComPortNumber = port.Id;
                    Connected?.Invoke(this);
                    return;
                }
                else
                {
                    // لا توجد بيانات، جرب المنفذ التالي
                    _hwCommunicationHelper.Close();
                }
            }
        }
        catch
        {
            // فشل، جرب المنفذ التالي
            continue;
        }
    }
    
    // فشل الاتصال بجميع المنافذ
    throw new Exception("لم يتم العثور على الجهاز");
}
```

---

## 📊 مراقبة الاتصال (Connection Monitoring)

### Thread المراقبة

```csharp
// Thread يعمل باستمرار للتحقق من الاتصال
private void ConnectionCheckerThreadWorker()
{
    while (_isConnectionOpen)
    {
        try
        {
            // التحقق من آخر بيانات مُستقبلة
            var timeSinceLastData = DateTime.Now - _lastComEventDateTime;
            
            if (timeSinceLastData.TotalMilliseconds > DisconnectedTimeout)
            {
                // الجهاز انقطع!
                _isCsaEmdUnitConnected = false;
                
                if (_disconnectNeedToFirstRais)
                {
                    _disconnectNeedToFirstRais = false;
                    Disconnected?.Invoke(this);
                }
            }
            else
            {
                // الجهاز متصل
                if (!_isCsaEmdUnitConnected)
                {
                    _isCsaEmdUnitConnected = true;
                    _disconnectNeedToFirstRais = true;
                }
            }
            
            // انتظار قبل الفحص التالي
            Thread.Sleep(100);
        }
        catch
        {
            // تجاهل الأخطاء
        }
    }
}
```

---

## 🎨 مثال كامل: اتصال + فحص + قراءة

```csharp
public class AutoTestExample
{
    private AutoCsaEmdUnitManagerPhase2 _manager;
    
    public void PerformCompleteTest()
    {
        try
        {
            // ════════════════════════════════════
            // المرحلة 1: التهيئة والاتصال
            // ════════════════════════════════════
            
            _manager = AutoCsaEmdUnitManagerPhase2.Instance;
            
            // تحميل الإعدادات
            _manager.RefreshSettings();
            
            // الاشتراك في الأحداث
            SubscribeToEvents();
            
            // فتح الاتصال
            var connectResult = _manager.OpenCSAConnection();
            
            if (!connectResult.IsSucceed)
            {
                Console.WriteLine("❌ فشل الاتصال!");
                return;
            }
            
            Console.WriteLine("✓ تم الاتصال بنجاح");
            Thread.Sleep(2000);  // انتظار استقرار الاتصال
            
            
            // ════════════════════════════════════
            // المرحلة 2: تفعيل الوضع الآلي
            // ════════════════════════════════════
            
            Console.WriteLine("\n🔄 تفعيل الوضع الآلي...");
            _manager.ActivateAutomationMode();
            Thread.Sleep(1000);
            
            if (_manager.CurrentMode == AutoCSAMode.Automation)
            {
                Console.WriteLine("✓ الوضع الآلي مفعّل");
            }
            
            
            // ════════════════════════════════════
            // المرحلة 3: الفحوصات الأولية
            // ════════════════════════════════════
            
            Console.WriteLine("\n🔍 إجراء الفحوصات الأولية...");
            
            // فحص المفصل
            _manager.PerformHingeCheck();
            Thread.Sleep(500);
            
            if (_manager.HasValidHinge == true)
                Console.WriteLine("  ✓ المفصل سليم");
            else
                Console.WriteLine("  ✗ المفصل غير سليم!");
            
            // فحص الرطوبة
            _manager.PerformMoistureCehck();
            Thread.Sleep(500);
            
            if (_manager.HasValidMoisture == true)
                Console.WriteLine("  ✓ الرطوبة مناسبة");
            else
                Console.WriteLine("  ✗ الرطوبة غير مناسبة!");
            
            // فحص الضغط
            _manager.PerformPressureCheck();
            Thread.Sleep(500);
            
            if (_manager.HasValidPressure == true)
                Console.WriteLine("  ✓ الضغط مناسب");
            else
                Console.WriteLine("  ✗ الضغط غير مناسب!");
            
            // التحقق من جميع الفحوصات
            if (!_manager.HasValidHinge.GetValueOrDefault() ||
                !_manager.HasValidMoisture.GetValueOrDefault() ||
                !_manager.HasValidPressure.GetValueOrDefault())
            {
                Console.WriteLine("\n❌ الفحوصات فشلت! إيقاف العملية.");
                return;
            }
            
            Console.WriteLine("\n✓ جميع الفحوصات نجحت!");
            
            
            // ════════════════════════════════════
            // المرحلة 4: تفعيل اللوح العلوي
            // ════════════════════════════════════
            
            Console.WriteLine("\n🔧 تفعيل اللوح العلوي...");
            _manager.ActivateTopPlate();
            Thread.Sleep(2000);
            
            if (_manager.IsTopPlateActive == true)
                Console.WriteLine("✓ اللوح العلوي مفعّل");
            
            
            // ════════════════════════════════════
            // المرحلة 5: بدء الأتمتة
            // ════════════════════════════════════
            
            Console.WriteLine("\n▶️  بدء الأتمتة...");
            _manager.StartAutomation();
            Thread.Sleep(2000);
            
            if (_manager.IsAutomationStarted == true)
                Console.WriteLine("✓ الأتمتة بدأت");
            
            
            // ════════════════════════════════════
            // المرحلة 6: المسح
            // ════════════════════════════════════
            
            Console.WriteLine("\n📊 بدء المسح...\n");
            
            // قائمة نقاط الاختبار
            var points = new List<TestingPoint>
            {
                new TestingPoint { Name = "LU-1", HWIdentifier = "P01" },
                new TestingPoint { Name = "LU-2", HWIdentifier = "P02" },
                new TestingPoint { Name = "LI-4", HWIdentifier = "P11" }
            };
            
            // وضع القراءة المستقرة
            _manager.SetReadingMode(AutoCSAReadingMode.StableReading);
            
            // المسح
            foreach (var point in points)
            {
                Console.WriteLine($"  🎯 فحص نقطة: {point.Name}");
                
                // تحديد النقطة
                _manager.SetPoint(point);
                Thread.Sleep(500);
                
                // بدء القراءة
                _manager.StartReading();
                
                // انتظار ReadingStabled Event
                // (سيتم التعامل معه في Event Handler)
                
                Thread.Sleep(3000);  // انتظار الاستقرار
                
                Console.WriteLine($"     القراءة: {_manager.CurrentReading.Value}");
                Console.WriteLine($"     النطاق: {_manager.CurrentReading.Min}-{_manager.CurrentReading.Max}");
                Console.WriteLine();
            }
            
            
            // ════════════════════════════════════
            // المرحلة 7: إيقاف الأتمتة
            // ════════════════════════════════════
            
            Console.WriteLine("\n⏹️  إيقاف الأتمتة...");
            _manager.StopAutomation();
            Thread.Sleep(1000);
            
            
            // ════════════════════════════════════
            // المرحلة 8: الإغلاق
            // ════════════════════════════════════
            
            Console.WriteLine("\n🔌 إغلاق الاتصال...");
            UnsubscribeFromEvents();
            _manager.CloseCSAConnection();
            
            Console.WriteLine("✓ تم بنجاح!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ خطأ: {ex.Message}");
        }
    }
    
    
    // ════════════════════════════════════
    // Event Handlers
    // ════════════════════════════════════
    
    private void SubscribeToEvents()
    {
        _manager.Connected += OnConnected;
        _manager.Disconnected += OnDisconnected;
        _manager.MeterValueChanged += OnMeterValueChanged;
        _manager.ReadingStabled += OnReadingStabled;
        _manager.ResponseReceived += OnResponseReceived;
    }
    
    private void UnsubscribeFromEvents()
    {
        _manager.Connected -= OnConnected;
        _manager.Disconnected -= OnDisconnected;
        _manager.MeterValueChanged -= OnMeterValueChanged;
        _manager.ReadingStabled -= OnReadingStabled;
        _manager.ResponseReceived -= OnResponseReceived;
    }
    
    void OnConnected(object sender)
    {
        Console.WriteLine("🔗 Event: Connected");
    }
    
    void OnDisconnected(object sender)
    {
        Console.WriteLine("❌ Event: Disconnected");
    }
    
    void OnMeterValueChanged(object sender, int reading, int min, int max)
    {
        Console.WriteLine($"📈 القراءة الحالية: {reading} ({min}-{max})");
    }
    
    void OnReadingStabled(object sender, int reading, int min, int max, int fall, int rise)
    {
        Console.WriteLine($"✓ القراءة استقرت: {reading}");
        Console.WriteLine($"   Min: {min}, Max: {max}");
        Console.WriteLine($"   Fall: {fall}, Rise: {rise}");
    }
    
    void OnResponseReceived(object sender, AutoCSAResponse response, string data)
    {
        Console.WriteLine($"📨 استجابة من الجهاز: {response} ('{data}')");
        
        switch (response)
        {
            case AutoCSAResponse.ValidHinge:
                Console.WriteLine("   ✓ المفصل سليم");
                break;
            case AutoCSAResponse.InvalidHinge:
                Console.WriteLine("   ✗ المفصل معطوب");
                break;
            case AutoCSAResponse.ValidMoisture:
                Console.WriteLine("   ✓ الرطوبة جيدة");
                break;
            case AutoCSAResponse.InvalidMoisture:
                Console.WriteLine("   ✗ الرطوبة سيئة");
                break;
            // ... إلخ
        }
    }
}
```

---

## ⚙️ الإعدادات المتقدمة

### ReadingStabilityEnabled

```csharp
// التحكم في استقرار القراءة
public void SetReadingMode(AutoCSAReadingMode mode)
{
    switch (mode)
    {
        case AutoCSAReadingMode.Continuous:
            // قراءة مستمرة بدون انتظار استقرار
            _autoCsaEmdUnitRepository.ReadingStabilityEnabled = false;
            break;
            
        case AutoCSAReadingMode.StableReading:
        case AutoCSAReadingMode.Mixed:
            // انتظار استقرار القراءة
            _autoCsaEmdUnitRepository.ReadingStabilityEnabled = true;
            break;
    }
    
    ReadingMode = mode;
}
```

### أوضاع القراءة الثلاثة

```
1. Continuous (مستمر)
   ├─ ReadingStabilityEnabled = false
   ├─ MeterValueChanged يُطلق باستمرار
   ├─ ReadingStabled لا يُطلق
   └─ الاستخدام: للمراقبة المباشرة

2. StableReading (مستقر)
   ├─ ReadingStabilityEnabled = true
   ├─ MeterValueChanged يُطلق باستمرار
   ├─ ReadingStabled يُطلق عند الاستقرار
   ├─ StopReading() تلقائياً بعد الاستقرار
   └─ الاستخدام: للفحص الدقيق

3. Mixed (مختلط)
   ├─ ReadingStabilityEnabled = true
   ├─ MeterValueChanged يُطلق باستمرار
   ├─ ReadingStabled يُطلق عند الاستقرار
   ├─ القراءة تستمر بعد الاستقرار
   └─ الاستخدام: المراقبة + الحفظ عند الاستقرار
```

---

## 🔐 Thread Safety (التزامن الآمن)

### Lock Objects المستخدمة

```csharp
// في Repository
private readonly object _lockReading;         // حماية القراءات
private readonly object _lockDataReceived;    // حماية استقبال البيانات
private readonly object _lockDataSend;        // حماية إرسال البيانات

// الاستخدام
void OnDataReceived(string data)
{
    lock (_lockDataReceived)
    {
        // معالجة البيانات بأمان
        ProcessData(data);
    }
}

public ProcessResult SendCommand(string command)
{
    lock (_lockDataSend)
    {
        // إرسال الأمر بأمان
        _hwCommunicationHelper.Write(command);
    }
}
```

---

## 📈 مخطط الحالات (State Machine)

```
                    [بدء]
                      │
                      ▼
            ┌──────────────────┐
            │  Disconnected    │
            └────────┬─────────┘
                     │ OpenConnection()
                     ▼
            ┌──────────────────┐
            │   Connected      │
            │   (Idle Mode)    │
            └────────┬─────────┘
                     │
        ┌────────────┼────────────┐
        │            │            │
        ▼            ▼            ▼
   ┌─────────┐  ┌─────────┐  ┌──────────┐
   │ Manual  │  │ Automation│  │Imprinting│
   │  Mode   │  │   Mode    │  │   Mode   │
   └────┬────┘  └────┬─────┘  └────┬─────┘
        │            │              │
        │            ▼              │
        │     ┌────────────┐       │
        │     │ Automation │       │
        │     │  Started   │       │
        │     └────┬───────┘       │
        │          │               │
        └──────────┴───────────────┘
                   │
                   ▼
            CloseConnection()
                   │
                   ▼
            [Disconnected]
```

---

## 🎯 الخصائص الذكية (Smart Properties)

### خصائص تُحدّث تلقائياً

```csharp
// هذه الخصائص تُحدث بناءً على الاستجابات

// 1. الوضع الحالي
CurrentMode  
    ← يُحدث عند: IdleMode, IdleAutomationMode, Manual

// 2. حالات الأتمتة
IsAutomationStarted
    ← true: عند استقبال قراءة بعد StartAutomation
    ← false: عند IdleMode

IsTopPlateActive
    ← true: عند استقبال قراءة بعد ActivateTopPlate
    ← false: عند IdleMode

IsImprintingActive
    ← true: عند ImprintingModeActivated
    ← false: عند IdleMode

// 3. حالات الفحوصات
HasValidHinge
    ← true: عند ValidHinge ("H")
    ← false: عند InvalidHinge ("Z") أو IdleMode

HasValidMoisture
    ← true: عند ValidMoisture ("L")
    ← false: عند InvalidMoisture ("X") أو IdleMode

HasValidPressure
    ← true: عند ValidPressure ("P")
    ← false: عند InvalidPressure ("Y") أو IdleMode

// 4. حالة المجسات
AreProbesConnected
    ← true: عند IdleMode
    ← false: عند ManualProbesDisconnected ("W")
```

---

## 🎬 سيناريو واقعي كامل

### الكود الكامل مع Output

```csharp
using System;
using System.Threading;
using Vital.Business.Managers;

class Program
{
    static void Main()
    {
        Console.WriteLine("════════════════════════════════════");
        Console.WriteLine("   Auto CSA Phase 2 - Full Test    ");
        Console.WriteLine("════════════════════════════════════\n");
        
        var manager = AutoCsaEmdUnitManagerPhase2.Instance;
        
        // الاشتراك في الأحداث
        manager.Connected += (s) => Console.WriteLine("✓ Connected Event");
        manager.Disconnected += (s) => Console.WriteLine("✗ Disconnected Event");
        
        manager.ResponseReceived += (s, response, data) => 
        {
            Console.WriteLine($"📨 Response: {response} ('{data}')");
        };
        
        manager.MeterValueChanged += (s, r, min, max) => 
        {
            Console.WriteLine($"📊 Reading: {r} ({min}-{max})");
        };
        
        manager.ReadingStabled += (s, r, min, max, f, ri) => 
        {
            Console.WriteLine($"✓ STABLE: {r}");
            Console.WriteLine("════════════════════════════════════\n");
        };
        
        try
        {
            // ═══ 1. الاتصال ═══
            Console.WriteLine("1️⃣  الاتصال بالجهاز...");
            manager.RefreshSettings();
            var result = manager.OpenCSAConnection();
            
            if (!result.IsSucceed)
            {
                Console.WriteLine("❌ فشل الاتصال");
                return;
            }
            
            Thread.Sleep(2000);
            
            // ═══ 2. الفحوصات ═══
            Console.WriteLine("\n2️⃣  الفحوصات الأولية...");
            
            manager.PerformHingeCheck();
            Thread.Sleep(1000);
            Console.WriteLine($"   Hinge: {(manager.HasValidHinge == true ? "✓" : "✗")}");
            
            manager.PerformMoistureCehck();
            Thread.Sleep(1000);
            Console.WriteLine($"   Moisture: {(manager.HasValidMoisture == true ? "✓" : "✗")}");
            
            manager.PerformPressureCheck();
            Thread.Sleep(1000);
            Console.WriteLine($"   Pressure: {(manager.HasValidPressure == true ? "✓" : "✗")}");
            
            // ═══ 3. التحضير ═══
            Console.WriteLine("\n3️⃣  تحضير الجهاز...");
            
            manager.ActivateAutomationMode();
            Thread.Sleep(1000);
            
            manager.ActivateTopPlate();
            Thread.Sleep(2000);
            
            // ═══ 4. بدء الأتمتة ═══
            Console.WriteLine("\n4️⃣  بدء الأتمتة...");
            manager.StartAutomation();
            Thread.Sleep(2000);
            
            // ═══ 5. المسح ═══
            Console.WriteLine("\n5️⃣  المسح...");
            manager.SetReadingMode(AutoCSAReadingMode.StableReading);
            
            var point = new TestingPoint { Name = "LU-1", HWIdentifier = "P01" };
            manager.SetPoint(point);
            Thread.Sleep(500);
            
            manager.StartReading();
            Thread.Sleep(4000);  // انتظار الاستقرار
            
            Console.WriteLine($"   النتيجة النهائية: {manager.CurrentReading.Value}");
            
            // ═══ 6. الإيقاف ═══
            Console.WriteLine("\n6️⃣  إيقاف الأتمتة...");
            manager.StopAutomation();
            Thread.Sleep(1000);
            
            // ═══ 7. الإغلاق ═══
            Console.WriteLine("\n7️⃣  إغلاق الاتصال...");
            manager.CloseCSAConnection();
            
            Console.WriteLine("\n════════════════════════════════════");
            Console.WriteLine("         ✓ اكتمل بنجاح!            ");
            Console.WriteLine("════════════════════════════════════");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ خطأ: {ex.Message}");
        }
        
        Console.ReadLine();
    }
}
```

### Output المتوقع

```
════════════════════════════════════
   Auto CSA Phase 2 - Full Test    
════════════════════════════════════

1️⃣  الاتصال بالجهاز...
✓ Connected Event
📨 Response: IdleMode ('E')

2️⃣  الفحوصات الأولية...
📨 Response: ValidHinge ('H')
   Hinge: ✓
📨 Response: ValidMoisture ('L')
   Moisture: ✓
📨 Response: ValidPressure ('P')
   Pressure: ✓

3️⃣  تحضير الجهاز...
📨 Response: IdleAutomationMode ('O')
📊 Reading: 0 (0-0)

4️⃣  بدء الأتمتة...
📊 Reading: 0 (0-0)

5️⃣  المسح...
📊 Reading: 75 (70-80)
📊 Reading: 76 (70-80)
📊 Reading: 76 (70-80)
📊 Reading: 76 (70-80)
✓ STABLE: 76
════════════════════════════════════

   النتيجة النهائية: 76

6️⃣  إيقاف الأتمتة...

7️⃣  إغلاق الاتصال...

════════════════════════════════════
         ✓ اكتمل بنجاح!            
════════════════════════════════════
```

---

## 🔧 وظائف خاصة بـ Phase 2

### 1. SetPoint (تحديد نقطة)

```csharp
public ProcessResult SetPoint(TestingPoint point)
{
    // إرسال أمر SetPoint للجهاز
    var result = _autoCsaEmdUnitRepository.SetPoint(
        point.HWIdentifier,  // مثل: "P05"
        forceSet: true       // إجبار التحديد حتى لو نفس النقطة
    );
    
    if (result.IsSucceed)
    {
        CurrentTestingPoint = point;  // حفظ النقطة الحالية
    }
    
    return result;
}

// مثال
var point = new TestingPoint 
{ 
    Name = "LU-1", 
    HWIdentifier = "P01"  // يُرسل "PP01" للجهاز
};
manager.SetPoint(point);
```

### 2. Broadcasting (البث الطاقي)

```csharp
public ProcessResult Broadcast(
    List<AutoItem> items, 
    bool isImprinting = false, 
    bool flush = true)
{
    // 1. التحقق من الاتصال
    if (!IsCsaEmdUnitConnected)
        return ProcessResult.Failed;
    
    // 2. التحقق من تفعيل Broadcasting
    if (!isImprinting && !IsBroadcastingOn)
        return ProcessResult.Failed;
    
    // 3. حفظ حالة Imprinting
    _isImprinting = isImprinting;
    
    // 4. فلترة العناصر (إذا Imprinting)
    if (_isImprinting)
    {
        items = items.Where(i => i.IsImprintable).ToList();
    }
    
    // 5. مسح البث القديم
    if (flush)
    {
        FlushBroadcastBuffer();
    }
    
    // 6. توليد الرسائل حسب Methodology
    switch (BroadcastMethodology)
    {
        case BroadcastMethodologies.VitalMessage:
            items.ForEach(i => 
                _autoCsaEmdUnitRepository.Broadcast(i.Id.ToString())
            );
            break;
            
        case BroadcastMethodologies.BlankString:
            items.ForEach(i => 
                _autoCsaEmdUnitRepository.Broadcast(i.Name)
            );
            break;
            
        case BroadcastMethodologies.VitalMessageIngredients:
            var messages = GenerateBroadcastMessage(items, ...);
            messages.ForEach(msg => 
                _autoCsaEmdUnitRepository.Broadcast(msg)
            );
            break;
            
        // ... طرق أخرى
    }
    
    return ProcessResult.Succeed;
}
```

### 3. الفحوصات الثلاثة

```csharp
// فحص المفصل
public ProcessResult PerformHingeCheck()
{
    return _autoCsaEmdUnitRepository.SendCommand(
        AutoCSAProtocol.Commands[AutoCSACommand.HingeCheck]
    );
    // Send: "H"
    // Receive: "H" (Valid) أو "Z" (Invalid)
}

// فحص الرطوبة
public ProcessResult PerformMoistureCehck()
{
    return _autoCsaEmdUnitRepository.SendCommand(
        AutoCSAProtocol.Commands[AutoCSACommand.MoistureCheck]
    );
    // Send: "M"
    // Receive: "L" (Valid) أو "X" (Invalid)
}

// فحص الضغط
public ProcessResult PerformPressureCheck()
{
    return _autoCsaEmdUnitRepository.SendCommand(
        AutoCSAProtocol.Commands[AutoCSACommand.PressureCheck]
    );
    // Send: "P"
    // Receive: "P" (Valid) أو "Y" (Invalid)
}
```

---

## ⚡ الميزات الذكية

### 1. Beep التلقائي

```csharp
void _autoCsaEmdUnitRepository_ReadingStabled(...)
{
    // عند استقرار القراءة
    if (BeepDuration > 0)
    {
        // صوت تنبيه
        Console.Beep(BeepFrequency, BeepDuration);
    }
    
    // إطلاق الحدث
    ReadingStabled?.Invoke(...);
    
    // إيقاف القراءة التلقائي (إذا StableReading)
    if (ReadingMode != AutoCSAReadingMode.Mixed)
    {
        StopReading();
    }
}
```

### 2. القراءات "المقبولة"

```csharp
// القراءات بين 46-54 تُعتبر 50 (مقبولة)
if (CrossLayersSharedLogic.IsAcceptableReading(reading))
{
    reading = 50;
}

CurrentReading = new AutoCSAReadingModel(reading, min, max, true);
```

### 3. Waiting Flags (أعلام الانتظار)

```csharp
// أعلام داخلية لتتبع الأوامر المُرسلة
private bool _waitingActivateTopPlate;
private bool _waitingStartAutomation;
private bool _waitingActivateManaualMode;

// مثال
public ProcessResult ActivateTopPlate()
{
    var result = SendCommand("T");
    
    if (result.IsSucceed)
    {
        _waitingActivateTopPlate = true;  // في انتظار التأكيد
    }
    
    return result;
}

// عند استقبال قراءة
void OnMeterValueChanged(...)
{
    if (_waitingActivateTopPlate)
    {
        _waitingActivateTopPlate = false;  // تم التأكيد
        IsTopPlateActive = true;           // تحديث الحالة
    }
}
```

---

## 🛡️ معالجة الأخطاء

### أنواع الأخطاء

```csharp
// 1. خطأ الاتصال
try
{
    manager.OpenCSAConnection();
}
catch (VitalHardwareException ex)
{
    if (ex.Message.Contains("Port"))
    {
        Console.WriteLine("خطأ في المنفذ - تحقق من الاتصال");
    }
}

// 2. خطأ Timeout
try
{
    manager.PerformHingeCheck();
    // انتظار الاستجابة...
}
catch (TimeoutException)
{
    Console.WriteLine("انتهى وقت الانتظار - الجهاز لا يستجيب");
}

// 3. خطأ الانقطاع
manager.Disconnected += (sender) =>
{
    Console.WriteLine("⚠️ الجهاز انقطع!");
    
    // محاولة إعادة الاتصال
    Thread.Sleep(2000);
    var reconnectResult = manager.RefreshCSAConnection();
    
    if (reconnectResult.IsSucceed)
    {
        Console.WriteLine("✓ تم إعادة الاتصال");
    }
};
```

---

## 📊 الملخص التقني

### المميزات الرئيسية

```
✅ اتصال واحد موحد (بدلاً من اثنين)
✅ ResponseReceived Event (جديد!)
✅ خصائص ذكية تُحدث تلقائياً
✅ Waiting Flags للتتبع الدقيق
✅ دعم كامل للأتمتة
✅ 3 فحوصات متقدمة (Hinge + Moisture + Pressure)
✅ Imprinting Mode
✅ Top Plate Control
✅ Beep تلقائي
✅ Thread Safe تماماً
```

### الفرق عن النسخة القديمة

```
النسخة القديمة:
├─ 2 اتصال (CSA + Prototype)
├─ 2 Manager منفصلين
├─ تعقيد أكثر
└─ وظائف محدودة

Phase 2:
├─ 1 اتصال (Auto CSA موحد)
├─ 1 Manager
├─ أبسط وأوضح
└─ وظائف أكثر وأقوى
```

---

## 🎯 نصائح للاستخدام

### ✅ Do's

```csharp
// ✓ دائماً استخدم Singleton
var manager = AutoCsaEmdUnitManagerPhase2.Instance;

// ✓ حمّل الإعدادات قبل الاتصال
manager.RefreshSettings();

// ✓ تحقق من النتائج
var result = manager.OpenCSAConnection();
if (result.IsSucceed) { ... }

// ✓ اشترك في ResponseReceived
manager.ResponseReceived += HandleResponse;

// ✓ تحقق من الفحوصات قبل البدء
if (manager.HasValidHinge == true && 
    manager.HasValidMoisture == true && 
    manager.HasValidPressure == true)
{
    // آمن للبدء
}

// ✓ ألغِ الاشتراك عند الإغلاق
manager.ResponseReceived -= HandleResponse;
```

### ❌ Don'ts

```csharp
// ✗ لا تنشئ instance جديد
var manager = new AutoCsaEmdUnitManagerPhase2();  // خطأ!

// ✗ لا تنسَ RefreshSettings
manager.OpenCSAConnection();  // بدون refresh

// ✗ لا تتجاهل Responses
// اشترك دائماً في ResponseReceived

// ✗ لا تبدأ الأتمتة بدون فحوصات
manager.StartAutomation();  // بدون تحقق من Hinge/Moisture/Pressure

// ✗ لا تنسَ إلغاء الاشتراك
// يسبب Memory Leak
```

---

## 📚 الخلاصة

**Auto CSA Phase 2** هو **الجهاز الحديث** مع:

```
🎯 اتصال واحد بسيط
🎯 وظائف متقدمة شاملة
🎯 ResponseReceived للتفاعل الذكي
🎯 خصائص ذكية تُحدث تلقائياً
🎯 Thread Safe تماماً
🎯 أسهل في الاستخدام
🎯 أقوى وأكثر موثوقية
```

**الاستخدام الموصى به**: 
- جميع التطبيقات الجديدة
- XtraFormAutoTest
- frmFreeAutoMeterPhase2

---

**تاريخ الإنشاء**: 2025-10-11  
**النوع**: دليل تفصيلي للجهاز الحديث  
**الحالة**: شامل وجاهز للاستخدام ✅
