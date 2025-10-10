@echo off
REM ========================================
REM   تشغيل سريع - وضع الإنتاج مع الجهاز الحقيقي
REM   Quick Start - Production Mode with Real Device
REM ========================================

echo.
echo ========================================
echo   Vital Expert - Production Mode  
echo   وضع الإنتاج مع الجهاز الحقيقي
echo ========================================
echo.

REM التحقق من وجود الملفات المطلوبة
if not exist "PresentationLayer\Vital.UI\bin\Debug\Vital.UI.exe" (
    echo ERROR: التطبيق غير موجود! يرجى تجميع المشروع أولاً
    echo ERROR: Application not found! Please build the project first.
    pause
    exit /b 1
)

REM نسخ إعدادات الإنتاج
echo تحديث إعدادات الإنتاج...
echo Updating production settings...

REM إنشاء نسخة احتياطية من App.config
if exist "PresentationLayer\Vital.UI\bin\Debug\Vital.UI.exe.config" (
    copy "PresentationLayer\Vital.UI\bin\Debug\Vital.UI.exe.config" "PresentationLayer\Vital.UI\bin\Debug\Vital.UI.exe.config.backup" >nul
)

REM تحديث إعدادات الإنتاج في App.config
powershell -Command "(gc 'PresentationLayer\Vital.UI\bin\Debug\Vital.UI.exe.config') -replace '<add key=\"UseSimulatedReadings\" value=\"true\"/>', '<add key=\"UseSimulatedReadings\" value=\"false\"/>' | Out-File -encoding UTF8 'PresentationLayer\Vital.UI\bin\Debug\Vital.UI.exe.config'"

echo.
echo إعدادات الإنتاج تم تفعيلها بنجاح!
echo Production settings activated successfully!
echo.

REM عرض الإعدادات المهمة
echo الإعدادات الحالية:
echo Current Settings:
echo - UseSimulatedReadings: FALSE
echo - سيتم البحث عن الجهاز الحقيقي
echo - Will search for real device
echo.

REM فحص منافذ COM المتاحة
echo فحص منافذ COM المتاحة...
echo Checking available COM ports...

powershell -Command "[System.IO.Ports.SerialPort]::getportnames() | ForEach-Object { Write-Host 'Found COM Port:' $_ }"

echo.

REM تحذير مهم
echo تحذير مهم:
echo IMPORTANT WARNING:
echo تأكد من:
echo Make sure:
echo 1. الجهاز الطبي متصل عبر USB
echo 1. Medical device is connected via USB
echo 2. تعريف الجهاز مثبت بشكل صحيح
echo 2. Device driver is properly installed  
echo 3. الجهاز يظهر في Device Manager كـ COM Port
echo 3. Device appears in Device Manager as COM Port
echo.

REM بدء التطبيق
echo بدء التطبيق...
echo Starting application...
echo.

cd "PresentationLayer\Vital.UI\bin\Debug"
start Vital.UI.exe

echo.
echo التطبيق يعمل الآن في وضع الإنتاج!
echo Application is now running in production mode!
echo.
echo في حالة عدم التعرف على الجهاز:
echo If device is not recognized:
echo 1. تحقق من Device Manager
echo 1. Check Device Manager
echo 2. أعد تثبيت تعريف الجهاز
echo 2. Reinstall device driver
echo 3. جرب منفذ USB آخر
echo 3. Try different USB port
echo 4. استخدم وضع المحاكاة للاختبار
echo 4. Use simulation mode for testing
echo.

pause