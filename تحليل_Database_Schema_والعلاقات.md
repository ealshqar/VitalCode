# تحليل Database Schema والعلاقات

## 📑 فهرس المحتويات
1. [نظرة عامة على قاعدة البيانات](#1-نظرة-عامة-على-قاعدة-البيانات)
2. [جداول Auto Test](#2-جداول-auto-test)
3. [العلاقات بين الجداول](#3-العلاقات-بين-الجداول)
4. [جداول الإعدادات](#4-جداول-الإعدادات)
5. [مخطط Entity Relationship](#5-مخطط-entity-relationship)

---

## 1. نظرة عامة على قاعدة البيانات

### 1.1 قاعدة البيانات

```
اسم قاعدة البيانات: VitalExpert
النوع: SQL Server / SQL Server Express
ORM Framework: LLBLGen Pro 3.5
```

### 1.2 المجموعات الرئيسية للجداول

```
VitalExpert Database
│
├── Auto Test (الفحص الآلي)
│   ├── AutoTest ...................... الفحوصات الآلية
│   ├── AutoTestResult ............... نتائج الفحص
│   ├── AutoTestResultProduct ........ منتجات النتائج
│   ├── AutoTestStage ................ مراحل الفحص
│   └── ... (أخرى)
│
├── Auto Protocol (البروتوكول)
│   ├── AutoProtocol ................. البروتوكولات
│   ├── AutoProtocolRevision ......... نسخ البروتوكول
│   ├── AutoProtocolStage ............ مراحل البروتوكول
│   ├── AutoProtocolStageRevision .... نسخ مراحل البروتوكول
│   └── ... (أخرى)
│
├── Auto Items (العناصر)
│   ├── AutoItem ..................... العناصر القابلة للفحص
│   ├── AutoItemRelation ............. علاقات العناصر
│   ├── StageAutoItem ................ عناصر المرحلة
│   └── ... (أخرى)
│
├── Testing Points (نقاط الاختبار)
│   ├── TestingPoint ................. نقاط الفحص
│   └── ... (أخرى)
│
├── Settings & Configuration
│   ├── Setting ...................... الإعدادات
│   ├── HwProfile .................... ملفات الأجهزة
│   └── Lookup ....................... القيم المرجعية
│
├── Patients & Tests
│   ├── Patient ...................... المرضى
│   ├── Test ......................... الفحوصات التقليدية
│   ├── TestResult ................... نتائج الفحوصات
│   └── ... (أخرى)
│
└── Products & Services
    ├── Product ...................... المنتجات
    ├── ProductSize .................. أحجام المنتجات
    ├── ProductForm .................. أشكال المنتجات
    └── ... (أخرى)
```

---

## 2. جداول Auto Test

### 2.1 AutoTest (الفحص الآلي)

```sql
CREATE TABLE AutoTest
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    PatientId INT NOT NULL,                -- المريض
    UserId INT NOT NULL,                   -- المستخدم/الفاحص
    AutoProtocolRevisionId INT NOT NULL,   -- نسخة البروتوكول المستخدمة
    TestDate DATETIME NOT NULL,            -- تاريخ الفحص
    Notes NVARCHAR(MAX),                   -- ملاحظات
    IsDeleted BIT DEFAULT 0,               -- محذوف؟
    CreationDateTime DATETIME,             -- تاريخ الإنشاء
    ModificationDateTime DATETIME,         -- تاريخ التعديل
    
    -- Foreign Keys
    CONSTRAINT FK_AutoTest_Patient 
        FOREIGN KEY (PatientId) REFERENCES Patient(Id),
    CONSTRAINT FK_AutoTest_User 
        FOREIGN KEY (UserId) REFERENCES User(Id),
    CONSTRAINT FK_AutoTest_AutoProtocolRevision 
        FOREIGN KEY (AutoProtocolRevisionId) REFERENCES AutoProtocolRevision(Id)
);
```

**الحقول الرئيسية**:
- `Id`: المعرّف الفريد
- `PatientId`: ربط بالمريض
- `AutoProtocolRevisionId`: البروتوكول المستخدم
- `TestDate`: تاريخ ووقت الفحص

**العلاقات**:
- `1:N` مع `AutoTestResult` (فحص واحد → نتائج متعددة)
- `N:1` مع `Patient` (عدة فحوصات → مريض واحد)
- `N:1` مع `AutoProtocolRevision` (عدة فحوصات → بروتوكول واحد)

### 2.2 AutoTestResult (نتائج الفحص)

```sql
CREATE TABLE AutoTestResult
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    AutoTestId INT NOT NULL,               -- الفحص
    AutoItemId INT NOT NULL,               -- العنصر المفحوص
    Reading INT,                           -- القراءة
    MinReading INT,                        -- أقل قراءة
    MaxReading INT,                        -- أعلى قراءة
    TestingPointId INT,                    -- نقطة الفحص
    IsResult BIT DEFAULT 0,                -- هل هي نتيجة نهائية
    ResultOrder INT,                       -- ترتيب النتيجة
    StageId INT,                           -- المرحلة
    Notes NVARCHAR(MAX),                   -- ملاحظات
    
    CONSTRAINT FK_AutoTestResult_AutoTest 
        FOREIGN KEY (AutoTestId) REFERENCES AutoTest(Id),
    CONSTRAINT FK_AutoTestResult_AutoItem 
        FOREIGN KEY (AutoItemId) REFERENCES AutoItem(Id),
    CONSTRAINT FK_AutoTestResult_TestingPoint 
        FOREIGN KEY (TestingPointId) REFERENCES TestingPoint(Id)
);
```

**الحقول الرئيسية**:
- `Reading`: القراءة الفعلية من CSA
- `MinReading`, `MaxReading`: نطاق القراءة
- `TestingPointId`: النقطة التي تم الفحص عندها
- `IsResult`: تحديد النتائج النهائية المهمة

### 2.3 AutoTestResultProduct (منتجات النتائج)

```sql
CREATE TABLE AutoTestResultProduct
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    AutoTestResultId INT NOT NULL,         -- النتيجة
    ProductId INT NOT NULL,                -- المنتج
    Quantity INT,                          -- الكمية
    Duration INT,                          -- المدة (أيام/أسابيع)
    DosageNotes NVARCHAR(MAX),            -- ملاحظات الجرعة
    Price DECIMAL(18,2),                   -- السعر
    
    CONSTRAINT FK_AutoTestResultProduct_AutoTestResult 
        FOREIGN KEY (AutoTestResultId) REFERENCES AutoTestResult(Id),
    CONSTRAINT FK_AutoTestResultProduct_Product 
        FOREIGN KEY (ProductId) REFERENCES Product(Id)
);
```

---

## 3. جداول Auto Protocol

### 3.1 AutoProtocol (البروتوكول)

```sql
CREATE TABLE AutoProtocol
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,           -- اسم البروتوكول
    Description NVARCHAR(MAX),             -- الوصف
    IsDefaultProtocol BIT DEFAULT 0,       -- البروتوكول الافتراضي
    IsActive BIT DEFAULT 1,                -- نشط؟
    UserId INT,                            -- المنشئ
    CreationDateTime DATETIME,
    ModificationDateTime DATETIME,
    
    CONSTRAINT FK_AutoProtocol_User 
        FOREIGN KEY (UserId) REFERENCES User(Id)
);
```

**ملاحظات**:
- `IsDefaultProtocol`: يحدد البروتوكول الذي يُستخدم افتراضياً للفحوصات الجديدة
- البروتوكول يحتوي على عدة نسخ (Revisions)

### 3.2 AutoProtocolRevision (نسخ البروتوكول)

```sql
CREATE TABLE AutoProtocolRevision
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    AutoProtocolId INT NOT NULL,           -- البروتوكول الأساسي
    RevisionNumber INT NOT NULL,           -- رقم النسخة
    Description NVARCHAR(MAX),
    IsActive BIT DEFAULT 1,
    CreationDateTime DATETIME,
    
    CONSTRAINT FK_AutoProtocolRevision_AutoProtocol 
        FOREIGN KEY (AutoProtocolId) REFERENCES AutoProtocol(Id)
);
```

**ملاحظات**:
- يسمح بتطوير البروتوكول مع الحفاظ على النسخ القديمة
- الفحوصات القديمة تبقى مربوطة بنسختها الأصلية

### 3.3 AutoProtocolStage (مراحل البروتوكول)

```sql
CREATE TABLE AutoProtocolStage
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    AutoProtocolId INT NOT NULL,
    Name NVARCHAR(255),                    -- اسم المرحلة
    Key NVARCHAR(100),                     -- مفتاح فريد (Preliminary, Testing, etc.)
    Order INT,                             -- الترتيب
    Description NVARCHAR(MAX),
    
    CONSTRAINT FK_AutoProtocolStage_AutoProtocol 
        FOREIGN KEY (AutoProtocolId) REFERENCES AutoProtocol(Id)
);
```

**المراحل القياسية**:
- `Preliminary`: مرحلة أولية
- `MajorIssues`: القضايا الرئيسية
- `Testing`: الفحص الفعلي
- `Dosage`: الجرعات
- `Summary`: الملخص
- `Results`: النتائج

### 3.4 AutoProtocolStageRevision (نسخ مراحل البروتوكول)

```sql
CREATE TABLE AutoProtocolStageRevision
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    AutoProtocolRevisionId INT NOT NULL,
    AutoProtocolStageId INT NOT NULL,
    StageOrder INT,                        -- ترتيب ظهور المرحلة
    IsActive BIT DEFAULT 1,
    
    CONSTRAINT FK_AutoProtocolStageRevision_Revision 
        FOREIGN KEY (AutoProtocolRevisionId) REFERENCES AutoProtocolRevision(Id),
    CONSTRAINT FK_AutoProtocolStageRevision_Stage 
        FOREIGN KEY (AutoProtocolStageId) REFERENCES AutoProtocolStage(Id)
);
```

---

## 4. جداول Auto Items

### 4.1 AutoItem (العناصر)

```sql
CREATE TABLE AutoItem
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,           -- الاسم
    Description NVARCHAR(MAX),             -- الوصف
    AutoItemTypeId INT,                    -- النوع (Point, Organ, Product, etc.)
    AutoItemStructureTypeId INT,           -- نوع الهيكل (Single, Category, Custom)
    AutoItemStatusId INT,                  -- الحالة (Active, Hidden, etc.)
    ParentAutoItemId INT,                  -- العنصر الأب (للهيكل الشجري)
    Order INT,                             -- الترتيب
    IsImprintable BIT DEFAULT 0,           -- قابل للطباعة الطاقية
    ChildsOrderTypeId INT,                 -- طريقة ترتيب الأبناء
    ChildsScanningTypeId INT,              -- طريقة مسح الأبناء (Sequential/Elimination)
    AutoItemScanningMethodId INT,          -- طريقة المسح (Normal/MatchRequired)
    
    CONSTRAINT FK_AutoItem_Parent 
        FOREIGN KEY (ParentAutoItemId) REFERENCES AutoItem(Id)
);
```

**أنواع العناصر** (AutoItemType):
- `Point`: نقطة فحص
- `Organ`: عضو
- `Gland`: غدة
- `Product`: منتج
- `ProductCategory`: فئة منتجات
- `General`: عام

**أنواع الهيكل** (AutoItemStructureType):
- `SingleItem`: عنصر واحد
- `Category`: فئة (حاوية)
- `Custom`: مخصص

**أنواع المسح** (ChildsScanningType):
- `None`: لا يوجد
- `Sequential`: تسلسلي (فحص كل الأبناء)
- `Elimination`: بالحذف (حذف السليم والتركيز على المشاكل)

### 4.2 StageAutoItem (عناصر المرحلة)

```sql
CREATE TABLE StageAutoItem
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    AutoProtocolStageRevisionId INT NOT NULL,  -- نسخة مرحلة البروتوكول
    AutoItemId INT NOT NULL,                   -- العنصر
    TestingPointId INT,                        -- نقطة الفحص (اختياري)
    ItemOrder INT,                             -- ترتيب العنصر
    IsActive BIT DEFAULT 1,
    
    CONSTRAINT FK_StageAutoItem_StageRevision 
        FOREIGN KEY (AutoProtocolStageRevisionId) 
        REFERENCES AutoProtocolStageRevision(Id),
    CONSTRAINT FK_StageAutoItem_AutoItem 
        FOREIGN KEY (AutoItemId) REFERENCES AutoItem(Id),
    CONSTRAINT FK_StageAutoItem_TestingPoint 
        FOREIGN KEY (TestingPointId) REFERENCES TestingPoint(Id)
);
```

**الربط**:
- يربط العناصر بالمراحل
- يحدد أي عناصر تُفحص في أي مرحلة
- يحدد نقطة الفحص لكل عنصر

### 4.3 AutoItemRelation (علاقات العناصر)

```sql
CREATE TABLE AutoItemRelation
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    ParentAutoItemId INT NOT NULL,         -- العنصر الأب
    ChildAutoItemId INT NOT NULL,          -- العنصر الابن
    RelationOrder INT,                     -- ترتيب العلاقة
    
    CONSTRAINT FK_AutoItemRelation_Parent 
        FOREIGN KEY (ParentAutoItemId) REFERENCES AutoItem(Id),
    CONSTRAINT FK_AutoItemRelation_Child 
        FOREIGN KEY (ChildAutoItemId) REFERENCES AutoItem(Id)
);
```

---

## 5. جداول Testing Points

### 5.1 TestingPoint (نقاط الاختبار)

```sql
CREATE TABLE TestingPoint
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,           -- الاسم (مثل: LU-1)
    FullName NVARCHAR(255),                -- الاسم الكامل
    ShortName NVARCHAR(100),               -- الاسم المختصر
    HWIdentifier NVARCHAR(50),             -- معرّف الجهاز (Pxx في Hex)
    Description NVARCHAR(MAX),             -- الوصف
    MeridianName NVARCHAR(100),            -- اسم الخط الطاقي (Meridian)
    Side NVARCHAR(10),                     -- الجانب (Left/Right)
    Order INT,                             -- الترتيب
    UserId INT,                            -- المنشئ
    IsActive BIT DEFAULT 1,
    
    CONSTRAINT FK_TestingPoint_User 
        FOREIGN KEY (UserId) REFERENCES User(Id)
);
```

**مثال على البيانات**:
```
Id | Name  | FullName      | HWIdentifier | MeridianName | Side
---|-------|---------------|--------------|--------------|------
1  | LU-1  | Lung 1        | P01          | Lung         | Left
2  | LU-2  | Lung 2        | P02          | Lung         | Left
3  | LI-4  | Large Int 4   | P11          | Large Int    | Right
```

**HWIdentifier**:
- يُستخدم للتواصل مع جهاز Prototype
- تنسيق: `Pxx` حيث `xx` رقم بنظام Hexadecimal
- مثال: `P05` = Point 5

---

## 6. جداول الإعدادات

### 6.1 Setting (الإعدادات)

```sql
CREATE TABLE Setting
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Key NVARCHAR(255) NOT NULL UNIQUE,     -- مفتاح الإعداد
    Value NVARCHAR(MAX),                   -- القيمة
    ValueTypeId INT,                       -- نوع القيمة (Numerical, Text, List, etc.)
    SettingGroupId INT,                    -- المجموعة
    Description NVARCHAR(MAX),
    IsVisible BIT DEFAULT 1,               -- ظاهر في الإعدادات
    DisplayOrder INT,                      -- ترتيب العرض
);
```

**الإعدادات المتعلقة بالأجهزة**:

| Key | Value | الوصف |
|-----|-------|-------|
| CommunicationsPort | 3 | COM Port لـ CSA |
| AutoTestDeviceComPort | 5 | COM Port لـ Prototype |
| MinimumReadingtoRegister | 46 | أقل قراءة للتسجيل |
| ReadingStabilityRange | 5 | نطاق استقرار القراءة |
| ReadingStabilityTimeout | 1000 | وقت استقرار القراءة (ms) |
| CsaDisconnectedTimeout | 5000 | وقت انتظار الفصل (ms) |
| BroadcastingStatus | 1 | حالة البث (ON/OFF) |
| BroadcastMethodology | 2 | طريقة البث |

### 6.2 HwProfile (ملفات الأجهزة)

```sql
CREATE TABLE HwProfile
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,           -- اسم الملف
    MinReading INT DEFAULT 46,             -- أقل قراءة
    StabilityRange INT DEFAULT 5,          -- نطاق الاستقرار
    StabilityTimeout INT DEFAULT 1000,     -- وقت الاستقرار
    DisconnectedTimeout INT DEFAULT 5000,  -- وقت الفصل
    IsDefault BIT DEFAULT 0,               -- الملف الافتراضي
    Description NVARCHAR(MAX),
);
```

**الاستخدام**:
- ملفات تعريف مختلفة لظروف فحص مختلفة
- يمكن التبديل بين الملفات حسب الحاجة

### 6.3 Lookup (القيم المرجعية)

```sql
CREATE TABLE Lookup
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Type NVARCHAR(100) NOT NULL,           -- النوع
    Key NVARCHAR(100) NOT NULL,            -- المفتاح
    Value NVARCHAR(255) NOT NULL,          -- القيمة
    Order INT,
    
    CONSTRAINT UQ_Lookup_Type_Key UNIQUE (Type, Key)
);
```

**أمثلة**:
```
Type                    | Key                   | Value
------------------------|------------------------|------------------------
YesNo                   | Yes                   | Yes
YesNo                   | No                    | No
OnOff                   | On                    | On
OnOff                   | Off                   | Off
BroadcastMethodologies  | VitalMessage          | VitalMessage
AutoItemType            | Point                 | Point
AutoItemType            | Organ                 | Organ
```

---

## 7. العلاقات بين الجداول

### 7.1 مخطط العلاقات الرئيسي

```
Patient (المريض)
    │
    ├─→ AutoTest (1:N)
    │      │
    │      ├─→ AutoTestResult (1:N)
    │      │      │
    │      │      ├─→ AutoItem (N:1)
    │      │      │
    │      │      ├─→ TestingPoint (N:1)
    │      │      │
    │      │      └─→ AutoTestResultProduct (1:N)
    │      │             │
    │      │             └─→ Product (N:1)
    │      │
    │      └─→ AutoProtocolRevision (N:1)
    │             │
    │             ├─→ AutoProtocol (N:1)
    │             │
    │             └─→ AutoProtocolStageRevision (1:N)
    │                    │
    │                    ├─→ AutoProtocolStage (N:1)
    │                    │
    │                    └─→ StageAutoItem (1:N)
    │                           │
    │                           ├─→ AutoItem (N:1)
    │                           │
    │                           └─→ TestingPoint (N:1)
    │
    └─→ Test (الفحوصات التقليدية) (1:N)
```

### 7.2 شرح العلاقات

#### علاقة Patient → AutoTest
```
مريض واحد ← عدة فحوصات آلية
- يمكن للمريض إجراء عدة فحوصات عبر الزمن
- كل فحص مرتبط بمريض واحد فقط
```

#### علاقة AutoTest → AutoTestResult
```
فحص واحد ← عدة نتائج
- الفحص الواحد ينتج عنه عدة نتائج (عنصر لكل نقطة)
- كل نتيجة مرتبطة بفحص واحد
```

#### علاقة AutoProtocol → AutoProtocolRevision
```
بروتوكول واحد ← عدة نسخ
- النسخة 1: الإصدار الأصلي
- النسخة 2: تحديث أول
- النسخة 3: تحديث ثاني
- الفحوصات القديمة تبقى مرتبطة بنسختها
```

#### علاقة StageAutoItem → AutoItem + TestingPoint
```
عنصر المرحلة ← يربط:
- AutoItem: ماذا نفحص (Organ, Product, etc.)
- TestingPoint: أين نفحص (LU-1, LI-4, etc.)
- يحدد أي عنصر يُفحص عند أي نقطة
```

---

## 8. مخطط ERD مبسط

```
┌──────────────┐
│   Patient    │
└──────┬───────┘
       │ 1:N
       ▼
┌──────────────┐       ┌──────────────────────┐
│   AutoTest   │ N:1   │ AutoProtocolRevision │
└──────┬───────┘──────▶└──────┬───────────────┘
       │ 1:N                   │ N:1
       │                       ▼
       │              ┌─────────────────┐
       │              │  AutoProtocol   │
       │              └─────────────────┘
       │                       │ 1:N
       │                       ▼
       │              ┌──────────────────────────┐
       │              │ AutoProtocolStageRevision│
       │              └──────┬───────────────────┘
       │                     │ 1:N
       │                     ▼
       │              ┌──────────────────┐
       │              │  StageAutoItem   │
       │              └──────┬───────────┘
       │                     │ N:1
       │                     ├─→ AutoItem
       │                     └─→ TestingPoint
       │
       ▼ 1:N
┌──────────────────┐
│ AutoTestResult   │
└──────┬───────────┘
       │ N:1
       ├─→ AutoItem
       ├─→ TestingPoint
       │
       │ 1:N
       ▼
┌──────────────────────────┐
│ AutoTestResultProduct    │
└──────┬───────────────────┘
       │ N:1
       ▼
┌──────────────┐
│   Product    │
└──────────────┘
```

---

## 9. استعلامات SQL مفيدة

### 9.1 الحصول على الفحوصات الأخيرة

```sql
-- آخر 10 فحوصات آلية
SELECT TOP 10
    at.Id,
    at.TestDate,
    p.FirstName + ' ' + p.LastName AS PatientName,
    u.UserName AS PerformedBy,
    apr.RevisionNumber,
    COUNT(atr.Id) AS ResultsCount
FROM AutoTest at
INNER JOIN Patient p ON at.PatientId = p.Id
INNER JOIN [User] u ON at.UserId = u.Id
INNER JOIN AutoProtocolRevision apr ON at.AutoProtocolRevisionId = apr.Id
LEFT JOIN AutoTestResult atr ON at.Id = atr.AutoTestId
WHERE at.IsDeleted = 0
GROUP BY at.Id, at.TestDate, p.FirstName, p.LastName, u.UserName, apr.RevisionNumber
ORDER BY at.TestDate DESC;
```

### 9.2 نتائج فحص معين

```sql
-- نتائج الفحص رقم 1
SELECT 
    atr.Id,
    ai.Name AS ItemName,
    tp.Name AS PointName,
    atr.Reading,
    atr.MinReading,
    atr.MaxReading,
    atr.IsResult AS IsFinalResult
FROM AutoTestResult atr
INNER JOIN AutoItem ai ON atr.AutoItemId = ai.Id
LEFT JOIN TestingPoint tp ON atr.TestingPointId = tp.Id
WHERE atr.AutoTestId = 1
ORDER BY atr.ResultOrder;
```

### 9.3 البروتوكول الافتراضي

```sql
-- الحصول على البروتوكول الافتراضي مع آخر نسخة
SELECT 
    ap.Id AS ProtocolId,
    ap.Name AS ProtocolName,
    apr.Id AS RevisionId,
    apr.RevisionNumber,
    COUNT(apsr.Id) AS StagesCount
FROM AutoProtocol ap
INNER JOIN AutoProtocolRevision apr ON ap.Id = apr.AutoProtocolId
LEFT JOIN AutoProtocolStageRevision apsr ON apr.Id = apsr.AutoProtocolRevisionId
WHERE ap.IsDefaultProtocol = 1 
  AND apr.IsActive = 1
GROUP BY ap.Id, ap.Name, apr.Id, apr.RevisionNumber
ORDER BY apr.RevisionNumber DESC;
```

### 9.4 إعدادات الأجهزة

```sql
-- الحصول على جميع إعدادات الأجهزة
SELECT 
    s.Key,
    s.Value,
    vt.Value AS ValueType,
    s.Description
FROM Setting s
LEFT JOIN Lookup vt ON s.ValueTypeId = vt.Id
WHERE s.Key IN (
    'CommunicationsPort',
    'AutoTestDeviceComPort',
    'MinimumReadingtoRegister',
    'ReadingStabilityRange',
    'ReadingStabilityTimeout',
    'CsaDisconnectedTimeout',
    'BroadcastingStatus',
    'BroadcastMethodology'
)
ORDER BY s.Key;
```

---

## 10. مخطط تدفق البيانات في قاعدة البيانات

### 10.1 عند إنشاء فحص جديد

```
1. إنشاء AutoTest
   ├─→ PatientId
   ├─→ UserId
   └─→ AutoProtocolRevisionId (من البروتوكول الافتراضي)

2. تحميل البروتوكول
   ├─→ AutoProtocolRevision
   │   └─→ AutoProtocolStageRevisions
   │       └─→ StageAutoItems
   │           ├─→ AutoItems
   │           └─→ TestingPoints

3. بدء الفحص
   ├─→ للكل StageAutoItem:
   │   ├─→ SetPoint(TestingPoint.HWIdentifier)
   │   ├─→ قراءة من CSA
   │   └─→ إنشاء AutoTestResult
   │       ├─→ AutoTestId
   │       ├─→ AutoItemId
   │       ├─→ TestingPointId
   │       ├─→ Reading, Min, Max
   │       └─→ حفظ في قاعدة البيانات

4. تحديد المنتجات (Dosage Stage)
   └─→ للكل نتيجة نهائية:
       └─→ إنشاء AutoTestResultProduct
           ├─→ AutoTestResultId
           ├─→ ProductId
           ├─→ Quantity, Duration
           └─→ حفظ

5. إنهاء الفحص
   └─→ تحديث AutoTest.ModificationDateTime
```

### 10.2 عند تحديث البروتوكول

```
1. البروتوكول الموجود
   └─→ AutoProtocol (لا يُعدل)

2. إنشاء نسخة جديدة
   └─→ AutoProtocolRevision
       ├─→ RevisionNumber++
       └─→ AutoProtocolId

3. نسخ المراحل
   └─→ AutoProtocolStageRevision
       ├─→ AutoProtocolRevisionId (الجديدة)
       └─→ نسخ/تعديل StageAutoItems

4. الفحوصات الجديدة
   └─→ تستخدم النسخة الجديدة

5. الفحوصات القديمة
   └─→ تبقى على النسخة القديمة
```

---

## 11. الفهرسة والأداء

### 11.1 Indexes المقترحة

```sql
-- لتحسين البحث عن الفحوصات
CREATE INDEX IX_AutoTest_PatientId_TestDate 
ON AutoTest(PatientId, TestDate DESC);

-- لتحسين تحميل النتائج
CREATE INDEX IX_AutoTestResult_AutoTestId 
ON AutoTestResult(AutoTestId, ResultOrder);

-- لتحسين البحث عن الإعدادات
CREATE UNIQUE INDEX IX_Setting_Key 
ON Setting(Key);

-- لتحسين تحميل عناصر المرحلة
CREATE INDEX IX_StageAutoItem_StageRevisionId 
ON StageAutoItem(AutoProtocolStageRevisionId, ItemOrder);
```

### 11.2 نصائح الأداء

```sql
-- ✅ استخدم Eager Loading لتجنب N+1 queries
-- مثال: تحميل الفحص مع جميع النتائج دفعة واحدة

-- ✅ استخدم Pagination للقوائم الكبيرة
SELECT TOP 50 * FROM AutoTest
WHERE PatientId = @PatientId
ORDER BY TestDate DESC;

-- ✅ استخدم Caching للبيانات الثابتة
-- مثل: Lookups, Settings, Protocols
```

---

## 12. الملخص التنفيذي للقاعدة

### الجداول الرئيسية (حسب الأهمية)

```
1. AutoTest ........................ الفحص الآلي الرئيسي
2. AutoTestResult .................. النتائج الفعلية
3. AutoProtocol + Revision ......... تعريف الفحص
4. AutoItem ........................ ما يتم فحصه
5. TestingPoint .................... أين يتم الفحص
6. Setting ......................... الإعدادات
7. AutoTestResultProduct ........... المنتجات الموصوفة
```

### العلاقات الحرجة

```
AutoTest → AutoProtocolRevision
  ↓ يحدد البروتوكول المستخدم

AutoTestResult → AutoItem + TestingPoint
  ↓ يربط العنصر بالنقطة بالقراءة

StageAutoItem → AutoItem + TestingPoint
  ↓ يحدد ماذا نفحص وأين
```

### نقاط مهمة

✅ **الإعدادات في جدول Settings** - يمكن تعديلها بدون تعديل الكود  
✅ **البروتوكولات بنظام Revisions** - يحافظ على سلامة البيانات التاريخية  
✅ **العناصر هيكل شجري** - AutoItem.ParentAutoItemId  
✅ **الربط بين Source و Destination** - StageAutoItem يربط البروتوكول بالنتائج  

---

**تاريخ الإنشاء**: 2025-10-11  
**الجزء**: 8 من سلسلة التحليل الشامل  
**ORM**: LLBLGen Pro 3.5
