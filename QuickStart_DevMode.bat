@echo off
REM ========================================
REM   تشغيل سريع - وضع التطوير مع المحاكاة
REM   Quick Start - Development Mode with Simulation
REM ========================================

echo.
echo ========================================
echo   Vital Expert - Development Mode
echo   وضع التطوير مع محاكي الجهاز الطبي
echo ========================================
echo.

REM التحقق من وجود الملفات المطلوبة
if not exist "PresentationLayer\Vital.UI\bin\Debug\Vital.UI.exe" (
    echo ERROR: التطبيق غير موجود! يرجى تجميع المشروع أولاً
    echo ERROR: Application not found! Please build the project first.
    pause
    exit /b 1
)

REM نسخ إعدادات المحاكاة
echo تحديث إعدادات المحاكاة...
echo Updating simulation settings...

REM إنشاء نسخة احتياطية من App.config
if exist "PresentationLayer\Vital.UI\bin\Debug\Vital.UI.exe.config" (
    copy "PresentationLayer\Vital.UI\bin\Debug\Vital.UI.exe.config" "PresentationLayer\Vital.UI\bin\Debug\Vital.UI.exe.config.backup" >nul
)

REM تحديث إعدادات المحاكاة في App.config
powershell -Command "(gc 'PresentationLayer\Vital.UI\bin\Debug\Vital.UI.exe.config') -replace '<add key=\"UseSimulatedReadings\" value=\"false\"/>', '<add key=\"UseSimulatedReadings\" value=\"true\"/>' | Out-File -encoding UTF8 'PresentationLayer\Vital.UI\bin\Debug\Vital.UI.exe.config'"

echo.
echo إعدادات المحاكاة تم تفعيلها بنجاح!
echo Simulation settings activated successfully!
echo.

REM عرض الإعدادات المهمة
echo الإعدادات الحالية:
echo Current Settings:
echo - UseSimulatedReadings: TRUE
echo - التحكم بلوحة المفاتيح: Left Shift (عالي) / Left Ctrl (منخفض)
echo - Keyboard Control: Left Shift (High) / Left Ctrl (Low)
echo.

REM بدء التطبيق
echo بدء التطبيق...
echo Starting application...
echo.

cd "PresentationLayer\Vital.UI\bin\Debug"
start Vital.UI.exe

echo.
echo التطبيق يعمل الآن في وضع المحاكاة!
echo Application is now running in simulation mode!
echo.
echo نصائح الاستخدام:
echo Usage Tips:
echo - اضغط Left Shift أثناء الاختبار للحصول على قراءات عالية
echo - اضغط Left Ctrl أثناء الاختبار للحصول على قراءات منخفضة  
echo - اتركهما للحصول على قراءات عشوائية
echo.
echo - Press Left Shift during test for high readings
echo - Press Left Ctrl during test for low readings
echo - Leave them for random readings
echo.

pause