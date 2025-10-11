# تحليل Repository Layer والبروتوكولات

## 📑 فهرس المحتويات
1. [Repository Layer Architecture](#1-repository-layer-architecture)
2. [البروتوكولات والأوامر](#2-البروتوكولات-والأوامر)
3. [تفصيل Interfaces](#3-تفصيل-interfaces)
4. [تفصيل الأوامر والاستجابات](#4-تفصيل-الأوامر-والاستجابات)
5. [آلية الاتصال التفصيلية](#5-آلية-الاتصال-التفصيلية)

---

## 1. Repository Layer Architecture

### 1.1 الهيكل العام

```
Repository Layer (طبقة المستودعات)
│
├── Hardware Repositories (مستودعات الأجهزة)
│   ├── CsaEmdUnit/
│   │   ├── ICsaEmdUnitRepository ............... Interface
│   │   └── CsaEmdUnitHardwareRepository ....... Implementation
│   │
│   ├── AutoCsaEmdUnit/
│   │   ├── IAutoCsaEmdUnitHardwareRepository .. Interface
│   │   └── AutoCsaEmdUnitHardwareRepository ... Implementation
│   │
│   └── Prototype/
│       ├── IPrototypeRepository ................ Interface
│       └── PrototypeHardwareRepository ......... Implementation
│
└── Database Repositories (مستودعات قاعدة البيانات)
    ├── AutoTestSource/
    ├── AutoTestDestination/
    ├── Settings/
    └── ... (أخرى)
```

---

## 2. البروتوكولات والأوامر

### 2.1 AutoCSA Protocol

**الموقع**: `BusinessLayer/Vital.Business.Shared/Shared/AutoCSAProtocol.cs`

#### الأوامر (Commands)

```csharp
public enum AutoCSACommand
{
    Reset,                      // R - إعادة تعيين الجهاز
    ActivateManualMode,         // N - تفعيل الوضع اليدوي
    ActivateTopPlate,           // T - تفعيل اللوح العلوي
    ActivateImprinting,         // I - تفعيل وضع Imprinting
    ActivateAutomationMode,     // A - تفعيل الوضع الآلي
    StartAutomation,            // S - بدء الأتمتة
    StopAutomation,             // O - إيقاف الأتمتة
    HingeCheck,                 // H - فحص المفصل
    MoistureCheck,              // M - فحص الرطوبة
    PressureCheck               // P - فحص الضغط
}
```

#### تعيين الأوامر (Command Mapping)

```csharp
public static readonly Dictionary<AutoCSACommand, string> Commands = 
{
    {AutoCSACommand.Reset, "R"},
    {AutoCSACommand.ActivateManualMode, "N"},
    {AutoCSACommand.ActivateTopPlate, "T"},
    {AutoCSACommand.ActivateImprinting, "I"},
    {AutoCSACommand.ActivateAutomationMode, "A"},
    {AutoCSACommand.StartAutomation, "S"},
    {AutoCSACommand.StopAutomation, "O"},
    {AutoCSACommand.HingeCheck, "H"},
    {AutoCSACommand.MoistureCheck, "M"},
    {AutoCSACommand.PressureCheck, "P"}
};
```

#### الاستجابات (Responses)

```csharp
public enum AutoCSAResponse
{
    IdleMode,                   // E - وضع الخمول
    IdleAutomationMode,         // O - وضع الأتمتة الخامل
    ImprintingModeActivated,    // I - تم تفعيل وضع Imprinting
    ValidHinge,                 // H - المفصل سليم
    InvalidHinge,               // Z - المفصل معطوب
    ValidMoisture,              // L - الرطوبة مناسبة
    InvalidMoisture,            // X - الرطوبة غير مناسبة
    ValidPressure,              // P - الضغط مناسب
    InvalidPressure,            // Y - الضغط غير مناسب
    ManualProbesDisconnected    // W - المجسات اليدوية مفصولة
}
```

#### تعيين الاستجابات (Response Mapping)

```csharp
public static readonly Dictionary<string, AutoCSAResponse> Responses =
{
    {"E", AutoCSAResponse.IdleMode},
    {"O", AutoCSAResponse.IdleAutomationMode},
    {"I", AutoCSAResponse.ImprintingModeActivated},
    {"H", AutoCSAResponse.ValidHinge},
    {"Z", AutoCSAResponse.InvalidHinge},
    {"L", AutoCSAResponse.ValidMoisture},
    {"X", AutoCSAResponse.InvalidMoisture},
    {"P", AutoCSAResponse.ValidPressure},
    {"Y", AutoCSAResponse.InvalidPressure},
    {"W", AutoCSAResponse.ManualProbesDisconnected}
};
```

### 2.2 Prototype Protocol

**الموقع**: `BusinessLayer/Vital.Business.Shared/Shared/PrototypeProtocol.cs`

#### بيانات الحياة (Alive Stream)

```csharp
public const string AliveStreamData = "E";
// البيانات التي يرسلها الجهاز باستمرار ليؤكد اتصاله
```

#### الأوامر (Commands)

```csharp
public enum PrototypeCommand
{
    Reset,                      // 0 - إعادة تعيين
    SetScanningPoint,           // تحديد نقطة المسح
    MoistureCheck,              // M - فحص الرطوبة
    PressureCheck               // P - فحص الضغط
}
```

#### تعيين الأوامر

```csharp
public static readonly Dictionary<PrototypeCommand, string> Commands =
{
    {PrototypeCommand.Reset, "0"},
    {PrototypeCommand.MoistureCheck, "M"},
    {PrototypeCommand.PressureCheck, "P"}
};
```

#### الاستجابات (Responses)

```csharp
public enum PrototypeResponse
{
    ValidMoisture,              // M - الرطوبة صالحة
    InvalidMoisture,            // N - الرطوبة غير صالحة
    ValidPressure,              // P - الضغط صالح
    InvalidPressure             // Q - الضغط غير صالح
}
```

#### تعيين الاستجابات

```csharp
public static readonly Dictionary<string, PrototypeResponse> Responses =
{
    {"M", PrototypeResponse.ValidMoisture},
    {"N", PrototypeResponse.InvalidMoisture},
    {"P", PrototypeResponse.ValidPressure},
    {"Q", PrototypeResponse.InvalidPressure}
};
```

---

## 3. تفصيل Interfaces

### 3.1 ICsaEmdUnitRepository

**الموقع**: `HardwareRepositories/CsaEmdUnit/ICsaEmdUnitRepository.cs`

#### Events (الأحداث)

```csharp
event MeterValueChangedHandle MeterValueChanged;     // تغيير قيمة القراءة
event OnResettingFinishedHandle ResettingFinished;   // انتهاء إعادة التعيين
event OnDisconnected Disconnected;                   // فصل الجهاز
event OnConnected Connected;                         // اتصال الجهاز
event OnReadingDone ReadingDone;                     // اكتمال القراءة
event OnReleased Released;                           // تحرير الأدوات
event OnDetecting Detecting;                         // البحث عن الجهاز
```

#### Properties (الخصائص)

```csharp
bool IsConnectionOpen { get; }              // حالة الاتصال
bool IsReadingOpened { get; }               // حالة القراءة
bool IsResetting { get; }                   // حالة إعادة التعيين
bool IsCsaEmdUnitConnected { get; }         // هل الجهاز متصل
int MinimumReadingtoRegister { get; set; }  // أقل قراءة للتسجيل
int CsaDisconnectedTimeout { get; set; }    // وقت انتظار الفصل
int ReadingStabilityTimeout { get; set; }   // وقت استقرار القراءة
int ReadingStabilityRange { get; set; }     // نطاق استقرار القراءة
int ComPortNumber { get; set; }             // رقم المنفذ
bool HasReading { get; }                    // وجود قراءة
bool IsBroadcasting { get; }                // حالة البث
bool AutoComPortDetection { get; set; }     // الكشف التلقائي
```

#### Methods (الطرق)

```csharp
ProcessResult OpenConnection(int comPortNumber, int baudRate, 
    int dataBit, int timeout, bool dtr, bool rts, bool? isAutoPortDetection);
ProcessResult CloseConnection();
ProcessResult CancelAutoDetection();
ProcessResult StartResetting();
ProcessResult StopResetting();
ProcessResult OpenReading();
ProcessResult CloseReading();
void Clear();
ProcessResult Broadcast(string message);
ProcessResult FlushBroadcastBuffer();
BindingList<ComPortInfo> GetComPorts();
```

### 3.2 IAutoCsaEmdUnitHardwareRepository

**الموقع**: `HardwareRepositories/AutoCsaEmdUnit/IAutoCsaEmdUnitHardwareRepository.cs`

#### Events (الأحداث الإضافية)

```csharp
event MeterValueChangedHandle MeterValueChanged;
event OnConnectionResetFinishedHandle ConnectionResetFinished;
event OnDisconnected Disconnected;
event OnConnected Connected;
event OnReadingStabled ReadingStabled;          // قراءة مستقرة (جديد)
event OnReadingStopped ReadingStopped;          // توقف القراءة (جديد)
event OnDetecting Detecting;
event OnResponseReceived ResponseReceived;       // استجابة مستلمة (جديد)
```

#### Properties الإضافية

```csharp
bool ReadingStabilityEnabled { get; set; }   // تفعيل استقرار القراءة (جديد)
```

#### Methods الإضافية

```csharp
ProcessResult SetPoint(string pointHex, bool forceSet = false);  // تحديد نقطة (جديد)
ProcessResult SendCommand(string command);                        // إرسال أمر (جديد)
```

### 3.3 IPrototypeRepository

**الموقع**: `HardwareRepositories/Prototype/IPrototypeRepository.cs`

#### Events

```csharp
event OnDisconnected Disconnected;
event OnConnected Connected;
event OnResponseReceived ResponseReceived;
event OnDetecting Detecting;
```

#### Properties

```csharp
bool IsConnectionOpen { get; }
bool IsProtprtpeConnected { get; set; }
int DisconnectedTimeout { get; set; }
int ComPortNumber { get; set; }
string CurrentPointCommand { get; }          // الأمر الحالي للنقطة
bool AutoComPortDetection { get; set; }
```

#### Methods

```csharp
ProcessResult OpenConnection(int comPortNumber, int baudRate, 
    int dataBit, int timeout, bool dtr, bool rts, bool? isAutoPortDetection);
ProcessResult CloseConnection();
ProcessResult SetPoint(string pointHex, bool forceSet = false);
ProcessResult SendCommand(string command);
```

---

## 4. تفصيل الأوامر والاستجابات

### 4.1 جدول الأوامر الكامل

#### AutoCSA Commands

| الأمر | الحرف | الوصف | متى يُستخدم |
|-------|-------|-------|-------------|
| Reset | R | إعادة تعيين كاملة | عند بدء الفحص أو حدوث خطأ |
| ActivateManualMode | N | التبديل للوضع اليدوي | للفحص اليدوي التقليدي |
| ActivateTopPlate | T | تفعيل اللوح العلوي | قبل بدء الأتمتة |
| ActivateImprinting | I | تفعيل الطباعة الطاقية | لطباعة العلاجات |
| ActivateAutomationMode | A | التبديل للوضع الآلي | للفحص الآلي |
| StartAutomation | S | بدء عملية الأتمتة | بعد تجهيز الجهاز |
| StopAutomation | O | إيقاف الأتمتة | عند الانتهاء أو الإلغاء |
| HingeCheck | H | فحص المفصل | قبل بدء الفحص |
| MoistureCheck | M | فحص الرطوبة | قبل بدء الفحص |
| PressureCheck | P | فحص الضغط | قبل بدء الفحص |

#### Prototype Commands

| الأمر | الحرف | الوصف | متى يُستخدم |
|-------|-------|-------|-------------|
| Reset | 0 | إعادة تعيين | عند حدوث خطأ |
| MoistureCheck | M | فحص الرطوبة | فحص أولي |
| PressureCheck | P | فحص الضغط | فحص أولي |
| SetScanningPoint | Pxx | تحديد نقطة | xx = رقم النقطة بـ Hex |

### 4.2 جدول الاستجابات الكامل

#### AutoCSA Responses

| الاستجابة | الحرف | المعنى | الإجراء المطلوب |
|-----------|-------|--------|-----------------|
| IdleMode | E | وضع الخمول | جاهز للأوامر |
| IdleAutomationMode | O | أتمتة خاملة | جاهز لبدء الأتمتة |
| ImprintingModeActivated | I | Imprinting مفعّل | يمكن البدء بالطباعة |
| ValidHinge | H | مفصل سليم ✓ | متابعة الفحص |
| InvalidHinge | Z | مفصل معطوب ✗ | إصلاح المفصل |
| ValidMoisture | L | رطوبة مناسبة ✓ | متابعة الفحص |
| InvalidMoisture | X | رطوبة غير مناسبة ✗ | ضبط الرطوبة |
| ValidPressure | P | ضغط مناسب ✓ | متابعة الفحص |
| InvalidPressure | Y | ضغط غير مناسب ✗ | ضبط الضغط |
| ManualProbesDisconnected | W | مجسات مفصولة | توصيل المجسات |

#### Prototype Responses

| الاستجابة | الحرف | المعنى | الإجراء المطلوب |
|-----------|-------|--------|-----------------|
| ValidMoisture | M | رطوبة صالحة ✓ | متابعة |
| InvalidMoisture | N | رطوبة غير صالحة ✗ | إعادة الفحص |
| ValidPressure | P | ضغط صالح ✓ | متابعة |
| InvalidPressure | Q | ضغط غير صالح ✗ | إعادة الفحص |
| Alive Stream | E | الجهاز حي | (مستمر) |

---

## 5. آلية الاتصال التفصيلية

### 5.1 سير الاتصال بـ CSA التقليدي

```
1. فتح Serial Port
   ├─→ PortName: COM{X}
   ├─→ BaudRate: 1200
   ├─→ DataBits: 8
   ├─→ Parity: None
   ├─→ StopBits: 1
   └─→ Open()

2. بدء الاستماع
   └─→ DataReceived Event

3. استقبال البيانات
   ├─→ قراءة Bytes
   ├─→ تحويل إلى Hex
   └─→ تحليل القيمة

4. معالجة القراءة
   ├─→ التحقق من الحد الأدنى
   ├─→ التحقق من الاستقرار
   └─→ إطلاق Event

5. Broadcasting (اختياري)
   ├─→ تحضير الرسالة
   ├─→ WriteLine(message)
   └─→ مراقبة Buffer
```

### 5.2 سير الاتصال بـ Auto Device

```
┌─────────────────────────────────┐
│  فتح اتصال CSA                  │
│  (للقراءات)                     │
│  - BaudRate: 1200               │
└───────────┬─────────────────────┘
            │
            ├─→ Connected Event
            │
            ▼
┌─────────────────────────────────┐
│  فتح اتصال Prototype            │
│  (للتحكم)                       │
│  - BaudRate: 9600               │
└───────────┬─────────────────────┘
            │
            ├─→ Connected Event
            ├─→ استقبال "E" (Alive)
            │
            ▼
┌─────────────────────────────────┐
│  إرسال أوامر التهيئة            │
│  1. Reset (R)                   │
│  2. ActivateAutomationMode (A)  │
│  3. ActivateTopPlate (T)        │
└───────────┬─────────────────────┘
            │
            ├─→ انتظار استجابات
            │
            ▼
┌─────────────────────────────────┐
│  الفحوصات الأولية               │
│  1. HingeCheck (H) → H/Z        │
│  2. MoistureCheck (M) → L/X     │
│  3. PressureCheck (P) → P/Y     │
└───────────┬─────────────────────┘
            │
            ├─→ التحقق من النتائج
            │
            ▼
┌─────────────────────────────────┐
│  بدء الأتمتة                    │
│  StartAutomation (S)            │
└───────────┬─────────────────────┘
            │
            ▼
┌─────────────────────────────────┐
│  حلقة الفحص                     │
│  ┌───────────────────────────┐  │
│  │ 1. SetPoint(Pxx)          │  │
│  │ 2. انتظار قراءة CSA       │  │
│  │ 3. حفظ النتيجة            │  │
│  │ 4. الانتقال للنقطة التالية│  │
│  └──────────┬────────────────┘  │
│             │ (تكرار)           │
│             └─→ حتى الانتهاء    │
└─────────────────────────────────┘
```

### 5.3 تسلسل الأحداث (Event Sequence)

#### للقراءة البسيطة

```
1. StartReading()
   ↓
2. OpenReading()
   ↓
3. DataReceived من Serial Port
   ↓
4. ReadingReceivedHandler()
   ↓
5. ProcessReceivedData()
   ↓
6. MeterValueChanged Event ← (مستمر)
   ↓
7. [استقرار القراءة؟]
   ├─ نعم → ReadingDone Event
   └─ لا → العودة للخطوة 6
```

#### للأتمتة الكاملة

```
1. ActivateAutomationMode()
   ↓
2. ResponseReceived(IdleAutomationMode)
   ↓
3. ActivateTopPlate()
   ↓
4. ResponseReceived(TopPlateActive)
   ↓
5. PerformChecks()
   ├→ HingeCheck
   ├→ MoistureCheck
   └→ PressureCheck
   ↓
6. [جميع الفحوصات OK؟]
   ├─ نعم → متابعة
   └─ لا → إيقاف + إخطار
   ↓
7. StartAutomation()
   ↓
8. [لكل نقطة في القائمة]
   ├→ SetPoint(point)
   ├→ StartReading()
   ├→ ReadingStabled Event
   ├→ SaveResult()
   └→ النقطة التالية
   ↓
9. StopAutomation()
   ↓
10. تقرير النتائج
```

---

## 6. معالجة البيانات

### 6.1 تحليل البيانات الواردة

#### من CSA (قراءات)

```csharp
// البيانات الواردة: Hexadecimal
// مثال: "00FF" = 127 في Decimal

private void ProcessReceivedData(string hexData)
{
    // 1. التحقق من Alive Stream
    if (!_hwConnected && hexData.Equals(AliveStreamData))
    {
        _hwConnected = true;
        InvokeAlive();
        return;
    }
    
    // 2. تحويل من Hex إلى قراءة
    var readingIndex = Array.IndexOf(ValueTranslate, hexData);
    
    if (readingIndex != -1)
    {
        var reading = readingIndex / 2;  // تحويل إلى قيمة فعلية
        
        // 3. التحقق من الحد الأدنى
        if (reading >= MinimumReadingtoRegister)
        {
            // 4. إطلاق حدث القراءة
            InvokeReadingReceivedEvent(reading);
        }
    }
}
```

#### من Prototype (أوامر واستجابات)

```csharp
// البيانات الواردة: String
// مثال: "M" = ValidMoisture

private void ProcessPrototypeResponse(string data)
{
    // 1. التحقق من Alive Stream
    if (data == PrototypeProtocol.AliveStreamData)
    {
        UpdateConnectionStatus();
        return;
    }
    
    // 2. البحث في قاموس الاستجابات
    if (PrototypeProtocol.Responses.ContainsKey(data))
    {
        var response = PrototypeProtocol.Responses[data];
        
        // 3. إطلاق حدث الاستجابة
        InvokeResponseReceived(response, data);
    }
}
```

### 6.2 استقرار القراءة (Reading Stability)

```csharp
// آلية تحديد استقرار القراءة

private int _lastReading = 0;
private DateTime _lastReadingTime = DateTime.MinValue;
private int _stableReadingCount = 0;

private bool IsReadingStable(int currentReading)
{
    // 1. حساب الفرق
    var difference = Math.Abs(currentReading - _lastReading);
    
    // 2. ضمن النطاق المسموح؟
    if (difference <= ReadingStabilityRange)
    {
        _stableReadingCount++;
        
        // 3. الوقت الكافي؟
        var elapsedTime = (DateTime.Now - _lastReadingTime).TotalMilliseconds;
        
        if (elapsedTime >= ReadingStabilityTimeout && 
            _stableReadingCount >= 3)  // 3 قراءات متتالية
        {
            return true;  // القراءة مستقرة
        }
    }
    else
    {
        // إعادة العداد
        _stableReadingCount = 0;
        _lastReadingTime = DateTime.Now;
    }
    
    _lastReading = currentReading;
    return false;
}
```

---

## 7. نماذج البيانات المتبادلة

### 7.1 نموذج القراءة

```csharp
public class AutoCSAReadingModel
{
    public int Reading { get; set; }      // القراءة الرئيسية
    public int Min { get; set; }          // الحد الأدنى
    public int Max { get; set; }          // الحد الأقصى
    public bool IsStable { get; set; }    // هل القراءة مستقرة
    public DateTime Timestamp { get; set; } // وقت القراءة
    
    public AutoCSAReadingModel(int reading, int min, int max, bool isStable = false)
    {
        Reading = reading;
        Min = min;
        Max = max;
        IsStable = isStable;
        Timestamp = DateTime.Now;
    }
}
```

### 7.2 نموذج نقطة الاختبار

```csharp
public class TestingPoint
{
    public int Id { get; set; }              // المعرّف
    public string Name { get; set; }         // الاسم (مثل: LU-1)
    public string HWIdentifier { get; set; } // معرّف الجهاز (مثل: P05)
    public string Description { get; set; }  // الوصف
    public int Order { get; set; }           // الترتيب
}
```

---

## 8. الملخص التنفيذي للـ Repository Layer

### مسؤوليات Repository Layer

1. **إدارة الاتصال المباشر** مع الأجهزة عبر Serial Port
2. **تنفيذ البروتوكولات** وتحويل الأوامر/الاستجابات
3. **معالجة البيانات** الواردة وتحليلها
4. **إطلاق الأحداث** لإخطار الطبقات العليا
5. **إدارة الحالة** (Connected, Disconnected, Reading, etc.)

### التسلسل الهرمي

```
Manager Layer (Business Logic)
    ↓ يستدعي
Repository Layer (Data Access)
    ↓ يستخدم
Hardware Helper (Communication)
    ↓ يتعامل مع
Serial Port (Physical Connection)
    ↓ متصل بـ
Medical Device (الجهاز الطبي)
```

### الفروقات الرئيسية

| الميزة | CSA Repository | Auto Repository | Prototype Repository |
|--------|---------------|----------------|---------------------|
| **الهدف** | قراءات فقط | قراءات + تحكم | تحكم فقط |
| **البروتوكول** | بسيط | AutoCSA | Prototype |
| **الأوامر** | لا يوجد | 10 أوامر | 3 أوامر |
| **الاستجابات** | قراءات رقمية | 10 استجابات | 4 استجابات + Alive |
| **SetPoint** | لا يدعم | يدعم | يدعم |
| **Stability** | أساسي | متقدم | لا ينطبق |

---

**تاريخ الإنشاء**: 2025-10-11  
**الجزء**: 5 من سلسلة التحليل الشامل
