# 🎯 دليل محاكي الجهاز الطبي - Device Simulator Guide

## 📋 نظرة عامة

هذا الدليل يوضح كيفية استخدام محاكي الجهاز الطبي في نظام Vital Expert للتطوير والاختبار بدون الحاجة للجهاز الفعلي.

## 🚀 الطرق المتاحة للمحاكاة

### 1. **المحاكاة المدمجة (الطريقة الأسرع)**

النظام يحتوي على آلية محاكاة مدمجة يمكن تفعيلها بسهولة:

#### التفعيل:
```xml
<!-- في ملف App.config -->
<add key="UseSimulatedReadings" value="true"/>
```

#### المميزات:
- ✅ تعمل فوراً بدون إعداد إضافي
- ✅ تحاكي جميع وظائف الجهاز
- ✅ تدعم التحكم بلوحة المفاتيح:
  - **Left Shift**: قراءات عالية (Yes)
  - **Left Ctrl**: قراءات منخفضة (No)
  - **بدون مفاتيح**: قراءات عشوائية

### 2. **المحاكي المتقدم (للتطوير المتقدم)**

محاكي شامل مع واجهة تحكم كاملة:

#### الملفات المضافة:
- `VirtualDeviceSimulator.cs` - المحاكي الأساسي
- `VirtualComPortSimulator.cs` - محاكي COM Port
- `DeviceSimulatorControlForm.cs` - واجهة التحكم

## 🛠️ إعداد المحاكاة

### الخطوة 1: تفعيل المحاكاة المدمجة

```xml
<!-- App.config -->
<appSettings>
    <!-- تفعيل المحاكاة -->
    <add key="UseSimulatedReadings" value="true"/>
    
    <!-- إعدادات المحاكاة -->
    <add key="UseStableReadingsOnly" value="true"/>
    <add key="ReadingStabilityDelay" value="10"/>
    <add key="HardwareCheckDelay" value="3500"/>
</appSettings>
```

### الخطوة 2: إعدادات إضافية للتطوير

```xml
<!-- إعدادات مفيدة للتطوير -->
<add key="EnableDebuggingLongHWDisconnectTimeout" value="true"/>
<add key="DebuggingLongHWDisconnectTimeout" value="900000"/>
<add key="EnableAutomationPostFailureAutoResume" value="true"/>
```

## 🎮 كيفية الاستخدام

### 1. **بدء الاختبار مع المحاكاة**

1. افتح شاشة `XtraFormAutoTest`
2. تأكد من تفعيل `UseSimulatedReadings = true`
3. اضغط **Start** لبدء الاختبار
4. النظام سيعمل تلقائياً بدون الحاجة لجهاز فعلي

### 2. **التحكم في القراءات**

#### أثناء الاختبار:
- **Left Shift**: فرض قراءات عالية (نتيجة إيجابية)
- **Left Ctrl**: فرض قراءات منخفضة (نتيجة سلبية)
- **بدون مفاتيح**: قراءات عشوائية تلقائية

### 3. **استخدام واجهة التحكم المتقدمة**

```csharp
// فتح واجهة التحكم
var simulatorForm = new DeviceSimulatorControlForm();
simulatorForm.Show();
```

#### مميزات واجهة التحكم:
- 📊 مراقبة القراءات الحية
- 🎛️ تحكم يدوي في القيم
- 📈 أنواع محاكاة مختلفة:
  - **Random**: قراءات عشوائية
  - **Incremental**: قراءات متزايدة
  - **Sine**: موجة جيبية
  - **Stable**: قراءات ثابتة مع تذبذب طفيف

## 🔧 إعداد بيئة التطوير

### 1. **للمطور (أنت)**

```xml
<!-- App.config للتطوير -->
<add key="UseSimulatedReadings" value="true"/>
<add key="UseStableReadingsOnly" value="false"/> <!-- للاختبار السريع -->
<add key="ReadingStabilityDelay" value="1"/> <!-- سرعة عالية -->
```

### 2. **للمستخدم النهائي**

```xml
<!-- App.config للإنتاج -->
<add key="UseSimulatedReadings" value="false"/>
<add key="UseStableReadingsOnly" value="true"/>
<add key="ReadingStabilityDelay" value="10"/>
```

## 📦 نشر النظام للمستخدم

### الخيار 1: نسخة تطوير (مع محاكاة)

```bash
# إنشاء نسخة للاختبار
1. اضبط UseSimulatedReadings = true
2. اكمبايل المشروع
3. انسخ المجلد كاملاً للمستخدم
4. أرفق دليل الاستخدام
```

### الخيار 2: نسخة إنتاج (جهاز حقيقي)

```bash
# إنشاء نسخة للإنتاج
1. اضبط UseSimulatedReadings = false
2. تأكد من إعدادات الجهاز الصحيحة
3. اكمبايل المشروع
4. انسخ للمستخدم مع تعريفات الجهاز
```

### الخيار 3: نسخة مرنة (تدعم الاثنين)

إنشاء ملف إعدادات منفصل:

```xml
<!-- DeviceSettings.config -->
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <!-- وضع المحاكاة: true للاختبار، false للإنتاج -->
    <add key="UseSimulatedReadings" value="true"/>
    
    <!-- إعدادات الجهاز الحقيقي -->
    <add key="DefaultComPort" value="3"/>
    <add key="BaudRate" value="9600"/>
    <add key="DataBits" value="8"/>
    
    <!-- إعدادات المحاكاة -->
    <add key="SimulationSpeed" value="normal"/> <!-- fast, normal, slow -->
    <add key="SimulationType" value="random"/> <!-- random, stable, incremental -->
  </appSettings>
</configuration>
```

## 🎯 سيناريوهات الاختبار

### 1. **اختبار التدفق الكامل**
```
1. بدء الاختبار
2. المرور عبر جميع المراحل
3. توليد نتائج متنوعة
4. حفظ وطباعة التقرير
```

### 2. **اختبار حالات الخطأ**
```
1. محاكاة انقطاع الاتصال
2. اختبار إعادة الاتصال
3. اختبار التعافي من الأخطاء
```

### 3. **اختبار الأداء**
```
1. اختبارات سرعة عالية
2. اختبارات طويلة المدى
3. اختبار الذاكرة والموارد
```

## 🚨 نصائح مهمة

### للمطور:
- ✅ استخدم المحاكاة دائماً أثناء التطوير
- ✅ اختبر جميع السيناريوهات قبل الإرسال
- ✅ وثق أي تغييرات في الإعدادات
- ✅ احتفظ بنسخة من إعدادات العمل

### للمستخدم النهائي:
- 📋 اتبع دليل التشغيل بدقة
- 🔌 تأكد من تعريف الجهاز قبل التشغيل
- 💾 احتفظ بنسخة احتياطية من الإعدادات
- 📞 تواصل مع الدعم عند الحاجة

## 🔍 استكشاف الأخطاء

### المشكلة: المحاكاة لا تعمل
```
الحل:
1. تأكد من UseSimulatedReadings = true
2. أعد تشغيل التطبيق
3. تحقق من ملف Log للأخطاء
```

### المشكلة: القراءات لا تتغير
```
الحل:
1. تأكد من عدم تفعيل UseStableReadingsOnly
2. جرب الضغط على مفاتيح التحكم
3. أعد تشغيل الاختبار
```

### المشكلة: النظام بطيء
```
الحل:
1. قلل ReadingStabilityDelay
2. قلل HardwareCheckDelay  
3. تأكد من موارد النظام
```

## 📞 الدعم الفني

عند مواجهة مشاكل، يرجى إرفاق:
- ملف `App.config`
- ملف `Vital.log`
- وصف المشكلة بالتفصيل
- خطوات إعادة إنتاج المشكلة

---

**ملاحظة**: هذا المحاكي مصمم للتطوير والاختبار فقط. للاستخدام الطبي الفعلي، يجب استخدام الجهاز الحقيقي المعتمد.