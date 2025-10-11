# تحليل UI Layer وسير العمل الكامل

## 📑 فهرس المحتويات
1. [واجهات المستخدم الرئيسية](#1-واجهات-المستخدم-الرئيسية)
2. [XtraFormAutoTest - شاشة الفحص الآلي](#2-xtraformautotest---شاشة-الفحص-الآلي)
3. [frmFreeAutoMeter - أداة الاختبار](#3-frmfreeautometer---أداة-الاختبار)
4. [سير العمل الكامل](#4-سير-العمل-الكامل)
5. [مراحل الفحص الآلي](#5-مراحل-الفحص-الآلي)
6. [التكامل بين الطبقات](#6-التكامل-بين-الطبقات)

---

## 1. واجهات المستخدم الرئيسية

### 1.1 نظرة عامة

```
UI Layer (طبقة الواجهات)
│
├── Forms (النوافذ الرئيسية)
│   ├── XtraFormAutoTest.cs ................. شاشة الفحص الآلي الكاملة
│   ├── frmFreeAutoMeter.cs ................. أداة اختبار Auto (النسخة 1)
│   ├── frmFreeAutoMeterPhase2.cs ........... أداة اختبار Auto (Phase 2)
│   ├── frmGeneralTest.cs ................... شاشة الفحص التقليدي
│   ├── frmSettings.cs ...................... شاشة الإعدادات
│   └── ribbonFrmMain.cs .................... الشاشة الرئيسية
│
├── User Controls (عناصر التحكم)
│   ├── XtraUserControlAutoTestResults ..... عرض النتائج
│   ├── XtraUserControlMeter ............... عداد القراءة
│   └── ... (أخرى)
│
└── Reports (التقارير)
    ├── XtraReportAutoTest ................. تقرير الفحص الآلي
    └── ... (أخرى)
```

---

## 2. XtraFormAutoTest - شاشة الفحص الآلي

### 2.1 المسؤوليات الرئيسية

**الموقع**: `PresentationLayer/Vital.UI/UI Components/Forms/XtraFormAutoTest.cs`

#### الوظائف

```
┌────────────────────────────────────┐
│   XtraFormAutoTest                 │
├────────────────────────────────────┤
│ ✓ إدارة دورة حياة الفحص الآلي     │
│ ✓ التحكم في الأجهزة (CSA + Proto) │
│ ✓ عرض المراحل والنتائج            │
│ ✓ إدارة Threads (المعالجة المتعددة)│
│ ✓ التفاعل مع المستخدم             │
│ ✓ حفظ وتحميل البيانات             │
└────────────────────────────────────┘
```

### 2.2 المتغيرات الرئيسية

#### متغيرات UI

```csharp
// منع التحديثات المتعددة
private bool _isUpdatingUserStageSelection;

// تتبع طريقة تغيير الصف
private FocusedRowChangeMethod _lastStageRowChangeMethod;

// مجموعات Lookup للعناصر
private IDictionary<int, RepositoryItemLookUpEdit> productFormLookupEdits;
private IDictionary<int, RepositoryItemLookUpEdit> productSizeLookupEdits;
```

#### متغيرات التأخير والتوقيت

```csharp
private int _hardwareCheckDelay;           // تأخير فحص الجهاز
private int _stageAutoItemPostDelay;       // تأخير بعد عنصر المرحلة
private int _multiLevelStageItemDelay;     // تأخير عنصر متعدد المستويات
private int _productDosageDelay;           // تأخير جرعة المنتج
private int _cSAGeneralCommandDelay;       // تأخير أمر CSA العام
private int _readingStabilityDelay;        // تأخير استقرار القراءة
private int _mutliLevelStageItemPostDelay; // تأخير ما بعد متعدد المستويات
```

#### متغيرات العمليات والـ Threads

```csharp
private Thread _scanningThread;            // Thread المسح الرئيسي
private Thread _readingThread;             // Thread القراءة المحاكي
private Thread _hardwareValidationThread;  // Thread التحقق من الجهاز
private Thread _errorThread;               // Thread الأخطاء
private bool _dataOperationActive;         // علم عملية البيانات نشطة
```

#### متغيرات البيانات

```csharp
private AutoTest _autoTest;                        // الفحص الآلي الحالي
private AutoTestSourceManager _autoTestSourceManager;     // مدير المصدر
private AutoTestDestinationManager _autoTestDestinationManager; // مدير الوجهة
private int _sessionSturctureId;                   // معرّف هيكل الجلسة
```

#### متغيرات الأجهزة

```csharp
private AutoCSACommand _lastSentCommand;           // آخر أمر مُرسل
private bool _lastCommandInProgress;               // الأمر قيد التنفيذ
private AutoCSAResponse _lastCommandResponse;      // آخر استجابة
private bool _hardwareValidationPerformed;         // تم التحقق من الجهاز

// خيارات التشغيل
private bool _enableAutomationPostFailureAutoResume;  // إعادة تشغيل تلقائي بعد فشل
private bool _pausedByCSAFailureNotification;         // متوقف بسبب إخطار فشل
private string _lastCSANotificationFailure;           // رسالة آخر فشل

// أعلام الفحوصات
private bool _performPressureCheck;     // إجراء فحص الضغط
private bool _performMoistureCheck;     // إجراء فحص الرطوبة
private bool _performHingeCheck;        // إجراء فحص المفصل

// كائنات القفل (للتزامن)
private object _responseLock;
private object _startStopLock;
private object _hardwareValidationThreadLock;
private object _scanningThreadLock;
```

### 2.3 الخصائص العامة

```csharp
public bool IsNew { get; }                 // هل الفحص جديد
public bool IsLoaded { get; set; }         // هل النافذة محملة
public bool IsClosing { get; set; }        // هل النافذة تُغلق
public AutoTest AutoTest { get; set; }     // الفحص الآلي الحالي
public EditorButton StartStopButton { get; } // زر البدء/الإيقاف
public TreeList TestingTreeList { get; }   // شجرة نتائج الاختبار
public TreeList AllResultsTreeList { get; } // شجرة جميع النتائج
```

---

## 3. frmFreeAutoMeter - أداة الاختبار

### 3.1 الغرض

**أداة اختبار بسيطة** للتحقق من عمل الأجهزة بدون فحص كامل.

**الموقع**: `PresentationLayer/Vital.UI/UI Components/Forms/frmFreeAutoMeter.cs`

### 3.2 المتغيرات

```csharp
private readonly AutoTestSourceManager _autoTestSourceManager;
private List<TestingPoint> _points;          // قائمة النقاط
private List<AutoItem> _broadcastItems;      // قائمة عناصر البث
```

### 3.3 الأحداث المُشترك فيها

```csharp
// أحداث CSA
AutoCsaEmdUnitManager.Instance.CSAConnected += Instance_CSAConnected;
AutoCsaEmdUnitManager.Instance.CSADisconnected += Instance_CSADisconnected;
AutoCsaEmdUnitManager.Instance.CSADetecting += Instance_CSADetecting;

// أحداث Prototype
AutoCsaEmdUnitManager.Instance.PrototypeConnected += Instance_PrototypeConnected;
AutoCsaEmdUnitManager.Instance.PrototypeDetecting += Instance_PrototypeDetecting;
AutoCsaEmdUnitManager.Instance.PrototypeDisconnected += Instance_PrototypeDisconnected;
```

### 3.4 معالجات الأحداث

```csharp
void Instance_CSAConnected(object sender)
{
    if (InvokeRequired)
    {
        Invoke(new AutoCsaEmdUnitManager.OnCSAConnected(Instance_CSAConnected), sender);
    }
    else
    {
        simpleLabelItemCSAConnectionStatus.Text = "CSA Connected";
    }
}

void Instance_CSADetecting(object sender, int comPortNumber)
{
    UpdateUIText($"CSA: Searching COM Port {comPortNumber}");
}

// ... معالجات مشابهة للأحداث الأخرى
```

### 3.5 تحميل البيانات

```csharp
private void LoadPoints()
{
    // تحميل نقاط الاختبار من قاعدة البيانات
    _points = _autoTestSourceManager
        .GetTestingPoints(new TestingPointsFilter())
        .ToList();
}

private void LoadBroadcastItems()
{
    // 1. الحصول على البروتوكول الافتراضي
    var defaultProtocol = _autoTestSourceManager
        .GetAutoProtocols(new AutoProtocolsFilter { IsDefaultProtocol = true })
        .FirstOrDefault();
    
    // 2. الحصول على آخر نسخة من البروتوكول
    var latestRevision = _autoTestSourceManager
        .GetAutoProtocolRevisions(new AutoProtocolRevisionsFilter { 
            AutoProtocolsId = defaultProtocol.Id 
        })
        .OrderBy(r => r.CreationDateTime)
        .LastOrDefault();
    
    // 3. استخراج عناصر مرحلة الاختبار
    var testingStageRevision = latestRevision.AutoProtocolStageRevisions
        .FirstOrDefault(r => r.AutoTestStage.Key == "Testing");
    
    _broadcastItems = testingStageRevision.StageAutoItemsForStages
        .Select(s => s.StageAutoItems)
        .First()
        .Select(s => s.AutoItem)
        .ToList();
}
```

---

## 4. سير العمل الكامل

### 4.1 دورة حياة الفحص الآلي

```
┌──────────────────────────────────────┐
│  1. إنشاء/فتح الفحص                  │
│  - إنشاء AutoTest جديد               │
│  - أو تحميل موجود                    │
└────────────┬─────────────────────────┘
             │
             ▼
┌──────────────────────────────────────┐
│  2. تهيئة الواجهة                    │
│  - تحميل البروتوكول                  │
│  - تحميل المراحل                     │
│  - ربط البيانات بالواجهة             │
└────────────┬─────────────────────────┘
             │
             ▼
┌──────────────────────────────────────┐
│  3. الاتصال بالأجهزة                 │
│  - فتح اتصال CSA                     │
│  - فتح اتصال Prototype               │
│  - التحقق من الاتصال                 │
└────────────┬─────────────────────────┘
             │
             ▼
┌──────────────────────────────────────┐
│  4. التهيئة والتحضير                 │
│  - ActivateAutomationMode             │
│  - ActivateTopPlate                   │
│  - إجراء الفحوصات الأولية            │
│    ├→ HingeCheck                      │
│    ├→ MoistureCheck                   │
│    └→ PressureCheck                   │
└────────────┬─────────────────────────┘
             │
             ├─ [الفحوصات ناجحة؟]
             │
          نعم│                    لا
             │                     │
             ▼                     ▼
┌─────────────────────┐   ┌────────────────┐
│  5. بدء الفحص       │   │  إيقاف + إخطار│
│  StartAutomation    │   │  بالمشكلة      │
└──────────┬──────────┘   └────────────────┘
           │
           ▼
┌──────────────────────────────────────┐
│  6. حلقة المسح (Scanning Loop)        │
│  ┌────────────────────────────────┐  │
│  │ للكل مرحلة (Stage):            │  │
│  │   ┌──────────────────────────┐ │  │
│  │   │ للكل عنصر (Item):        │ │  │
│  │   │   1. SetPoint()          │ │  │
│  │   │   2. StartReading()      │ │  │
│  │   │   3. انتظار استقرار      │ │  │
│  │   │   4. حفظ النتيجة         │ │  │
│  │   │   5. عرض في UI           │ │  │
│  │   └──────────────────────────┘ │  │
│  └────────────────────────────────┘  │
└────────────┬─────────────────────────┘
             │
             ▼
┌──────────────────────────────────────┐
│  7. إنهاء الفحص                      │
│  - StopAutomation                     │
│  - حفظ البيانات                      │
│  - توليد التقرير                     │
└────────────┬─────────────────────────┘
             │
             ▼
┌──────────────────────────────────────┐
│  8. المراجعة والنتائج                │
│  - عرض النتائج                       │
│  - طباعة التقرير                     │
│  - تصدير البيانات (اختياري)          │
└──────────────────────────────────────┘
```

### 4.2 Thread Management (إدارة المعالجة المتعددة)

```
Main UI Thread (Thread الواجهة الرئيسي)
│
├─→ Scanning Thread (Thread المسح)
│   │
│   ├─→ يتحكم في حلقة المسح
│   ├─→ يستدعي SetPoint
│   ├─→ يستدعي StartReading
│   ├─→ ينتظر النتائج
│   └─→ يحفظ البيانات
│
├─→ Hardware Validation Thread (Thread التحقق)
│   │
│   ├─→ يراقب حالة الأجهزة
│   ├─→ يجري الفحوصات الدورية
│   └─→ يوقف المسح عند الحاجة
│
├─→ Reading Thread (Thread القراءة - محاكي)
│   │
│   ├─→ يولد قراءات محاكية (للاختبار)
│   └─→ يطلق أحداث ReadingStabled
│
└─→ Error Thread (Thread الأخطاء)
    │
    ├─→ يعالج الأخطاء بشكل منفصل
    └─→ يسجل الأخطاء
```

### 4.3 تزامن البيانات (Data Synchronization)

```csharp
// استخدام Lock Objects لمنع تضارب الـ Threads

// مثال 1: حماية الاستجابات
lock (_responseLock)
{
    _lastCommandResponse = response;
    _lastCommandInProgress = false;
}

// مثال 2: حماية عمليات البدء/الإيقاف
lock (_startStopLock)
{
    if (_scanningThread != null && _scanningThread.IsAlive)
    {
        _scanningThread.Abort();
    }
}

// مثال 3: حماية عمليات البيانات
_dataOperationActive = true;
try
{
    // عملية قاعدة البيانات
    _autoTestDestinationManager.SaveAutoTest(_autoTest);
}
finally
{
    _dataOperationActive = false;
}
```

---

## 5. مراحل الفحص الآلي

### 5.1 المراحل (Stages)

```
┌─────────────────────────────────────────┐
│  Auto Test Stages (مراحل الفحص الآلي)   │
├─────────────────────────────────────────┤
│                                         │
│  1. Preliminary ................. أولي  │
│     - استبيان أولي                      │
│     - معلومات عامة                      │
│                                         │
│  2. Major Issues ......... قضايا رئيسية│
│     - تحديد المشاكل الكبرى              │
│     - الأولويات                         │
│                                         │
│  3. Testing .................... الفحص │
│     - المسح الفعلي                      │
│     - قراءة النقاط                      │
│     - جمع البيانات                      │
│                                         │
│  4. Dosage .................... الجرعات│
│     - تحديد المنتجات                    │
│     - حساب الجرعات                      │
│     - جدول الأخذ                        │
│                                         │
│  5. Summary .................... الملخص│
│     - مراجعة النتائج                    │
│     - توصيات                            │
│                                         │
│  6. Results ................... النتائج│
│     - النتائج النهائية                  │
│     - التقرير                           │
│                                         │
└─────────────────────────────────────────┘
```

### 5.2 مرحلة الفحص (Testing Stage) - التفصيل

```
Testing Stage
│
├── Scanning Process (عملية المسح)
│   │
│   ├─→ Sequential Scanning (مسح تسلسلي)
│   │   └─ فحص كل عنصر بالترتيب
│   │
│   └─→ Elimination Scanning (مسح بالحذف)
│       └─ حذف العناصر السليمة والتركيز على المشاكل
│
├── Multi-Level Scanning (مسح متعدد المستويات)
│   │
│   ├─→ Split (تقسيم)
│   │   └─ تقسيم المجموعة إلى مجموعتين
│   │
│   ├─→ Switch (تبديل)
│   │   └─ التبديل بين المجموعات
│   │
│   ├─→ Move Next (الانتقال للتالي)
│   │   └─ الانتقال للعنصر التالي في الوضع 1x1
│   │
│   └─→ Mark Result (تحديد كنتيجة)
│       └─ تحديد العنصر المميز كنتيجة
│
├── Reading Modes (أوضاع القراءة)
│   │
│   ├─→ Continuous (مستمر)
│   ├─→ Stable (مستقر)
│   └─→ Mixed (مختلط)
│
└── Result Handling (معالجة النتائج)
    │
    ├─→ Auto Add (إضافة تلقائية)
    ├─→ Manual Confirm (تأكيد يدوي)
    └─→ Skip (تخطي)
```

### 5.3 Bookmarking System (نظام الإشارات المرجعية)

```csharp
// نظام لحفظ الموقع الحالي والعودة إليه

public class ScanBookmark
{
    public StageKey StageKey { get; set; }        // المرحلة
    public string UIKey { get; set; }             // مفتاح الواجهة
    public int StageIndex { get; set; }           // فهرس المرحلة
    public int ItemIndex { get; set; }            // فهرس العنصر
    public bool IsEmpty { get; }                  // هل فارغ
}

// استخدام:
AutoTest.StageBookmark.StageKey = StageKey.Testing;
AutoTest.StageBookmark.ItemIndex = 5;  // نحن عند العنصر رقم 5
```

---

## 6. التكامل بين الطبقات

### 6.1 مخطط التكامل

```
┌─────────────────────────────────────────┐
│         UI Layer (طبقة الواجهات)        │
│  ┌───────────────────────────────────┐  │
│  │  XtraFormAutoTest                 │  │
│  │  - عرض الواجهة                    │  │
│  │  - التفاعل مع المستخدم             │  │
│  │  - إدارة Threads                  │  │
│  └───────────────┬───────────────────┘  │
└──────────────────┼──────────────────────┘
                   │ يستدعي
                   ▼
┌─────────────────────────────────────────┐
│      Business Layer (طبقة الأعمال)      │
│  ┌───────────────────────────────────┐  │
│  │  AutoCsaEmdUnitManager            │  │
│  │  - منطق الأعمال                   │  │
│  │  - التحقق من الصحة                │  │
│  │  - معالجة الأحداث                 │  │
│  └───────────────┬───────────────────┘  │
└──────────────────┼──────────────────────┘
                   │ يستدعي
                   ▼
┌─────────────────────────────────────────┐
│    Repository Layer (طبقة المستودعات)   │
│  ┌───────────────────────────────────┐  │
│  │  AutoCsaEmdUnitHardwareRepository │  │
│  │  - الوصول للبيانات                │  │
│  │  - تنفيذ البروتوكول               │  │
│  │  - إدارة الاتصال                  │  │
│  └───────────────┬───────────────────┘  │
└──────────────────┼──────────────────────┘
                   │ يستخدم
                   ▼
┌─────────────────────────────────────────┐
│     Hardware Layer (طبقة الأجهزة)       │
│  ┌───────────────────────────────────┐  │
│  │  HwCommunicationHelper            │  │
│  │  - Serial Port Management         │  │
│  │  - قراءة/كتابة البيانات           │  │
│  └───────────────┬───────────────────┘  │
└──────────────────┼──────────────────────┘
                   │ يتصل بـ
                   ▼
         ┌──────────────────┐
         │  Medical Device  │
         │  الجهاز الطبي    │
         └──────────────────┘
```

### 6.2 تدفق الأحداث (Event Flow)

```
Medical Device (الجهاز)
    │ يرسل بيانات
    ▼
HwCommunicationHelper
    │ DataReceived Event
    ▼
Repository Layer
    │ MeterValueChanged/ResponseReceived
    ▼
Manager (Business Layer)
    │ معالجة + منطق
    ▼
UI Layer (XtraFormAutoTest)
    │ Invoke للـ UI Thread
    ▼
تحديث الواجهة
```

### 6.3 مثال: تدفق قراءة واحدة

```
1. المستخدم ينقر "Start" في UI
   ↓
2. XtraFormAutoTest.StartScanning()
   ↓
3. يُنشئ Scanning Thread
   ↓
4. Thread يستدعي: manager.SetPoint(point)
   ↓
5. Manager → Repository → SendCommand("Pxx")
   ↓
6. HwHelper يرسل عبر Serial Port
   ↓
7. Prototype ينشط النقطة
   ↓
8. Thread يستدعي: manager.StartReading()
   ↓
9. CSA يرسل قراءات
   ↓
10. HwHelper.DataReceived يستقبل
    ↓
11. Repository.ProcessData()
    ↓
12. Repository → MeterValueChanged Event
    ↓
13. Manager يعالج القراءة
    ↓
14. Manager → ReadingStabled Event (عند الاستقرار)
    ↓
15. UI Handler يستقبل (في Thread منفصل)
    ↓
16. UI يستخدم Invoke للـ Main Thread
    ↓
17. تحديث الواجهة بالقراءة
    ↓
18. حفظ النتيجة في قاعدة البيانات
    ↓
19. الانتقال للنقطة التالية
```

---

## 7. معالجة الحالات الخاصة

### 7.1 إيقاف مؤقت وإعادة تشغيل

```csharp
// الإيقاف المؤقت
public void PauseScanning()
{
    if (_scanningThread != null && _scanningThread.IsAlive)
    {
        // حفظ الموقع الحالي
        AutoTest.StageBookmark = GetCurrentPosition();
        
        // إيقاف القراءة
        _autoManager.StopReading();
        
        // وقف Thread بشكل آمن
        _scanningThread.Suspend();  // أو علم للتوقف
    }
}

// إعادة التشغيل
public void ResumeScanning()
{
    if (_scanningThread != null)
    {
        // العودة للموقع المحفوظ
        RestorePosition(AutoTest.StageBookmark);
        
        // إعادة تشغيل Thread
        _scanningThread.Resume();
        
        // إعادة بدء القراءة
        _autoManager.StartReading();
    }
}
```

### 7.2 معالجة فشل الجهاز

```csharp
// معالج لاستجابة فشل
void OnHardwareFailureResponse(AutoCSAResponse response)
{
    switch (response)
    {
        case AutoCSAResponse.InvalidHinge:
            PauseScanning();
            ShowUserMessage("المفصل غير سليم، يرجى إصلاحه");
            _pausedByCSAFailureNotification = true;
            _lastCSANotificationFailure = "Invalid Hinge";
            break;
            
        case AutoCSAResponse.InvalidMoisture:
            PauseScanning();
            ShowUserMessage("الرطوبة غير مناسبة");
            break;
            
        case AutoCSAResponse.InvalidPressure:
            PauseScanning();
            ShowUserMessage("الضغط غير مناسب");
            break;
    }
}

// إعادة تشغيل تلقائية بعد الإصلاح
void OnHardwareFixedResponse(AutoCSAResponse response)
{
    if (_enableAutomationPostFailureAutoResume && 
        _pausedByCSAFailureNotification)
    {
        switch (response)
        {
            case AutoCSAResponse.ValidHinge:
            case AutoCSAResponse.ValidMoisture:
            case AutoCSAResponse.ValidPressure:
                ShowUserMessage("تم إصلاح المشكلة، إعادة التشغيل...");
                ResumeScanning();
                _pausedByCSAFailureNotification = false;
                break;
        }
    }
}
```

### 7.3 حفظ تلقائي

```csharp
// حفظ دوري أثناء الفحص
private Timer _autoSaveTimer;

private void InitializeAutoSave()
{
    _autoSaveTimer = new Timer();
    _autoSaveTimer.Interval = 30000; // كل 30 ثانية
    _autoSaveTimer.Tick += AutoSaveTimer_Tick;
    _autoSaveTimer.Start();
}

private void AutoSaveTimer_Tick(object sender, EventArgs e)
{
    if (_dataOperationActive)
        return;  // لا تقاطع عمليات البيانات
    
    try
    {
        _dataOperationActive = true;
        
        // حفظ البيانات الحالية
        _autoTestDestinationManager.SaveAutoTest(_autoTest);
        
        // تحديث آخر وقت حفظ
        UpdateLastSaveTime();
    }
    finally
    {
        _dataOperationActive = false;
    }
}
```

---

## 8. الملخص التنفيذي للـ UI Layer

### المسؤوليات الرئيسية

1. **عرض الواجهة** وتفاعل المستخدم
2. **إدارة Threads** للمعالجة المتعددة
3. **التنسيق بين المكونات** (Managers + Repositories)
4. **معالجة الأحداث** من الأجهزة
5. **حفظ وتحميل البيانات**
6. **التعامل مع الحالات الخاصة** (أخطاء، إيقاف، استئناف)

### نقاط مهمة للمطورين

✅ **استخدم Invoke** دائمًا عند تحديث UI من Thread منفصل  
✅ **استخدم Lock Objects** لحماية البيانات المشتركة  
✅ **تحقق من _dataOperationActive** قبل إيقاف Threads  
✅ **احفظ Bookmarks** للعودة للموقع الصحيح  
✅ **امسح Handlers** عند إغلاق النافذة لتجنب Memory Leaks  

---

**تاريخ الإنشاء**: 2025-10-11  
**الجزء**: 6 من سلسلة التحليل الشامل
