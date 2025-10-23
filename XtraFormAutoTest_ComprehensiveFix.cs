// حل شامل لمشكلة الاتصال مع الجهاز الحديث في XtraFormAutoTest
// التعديلات المطلوبة:

// 1. إضافة متغيرات جديدة في بداية الكلاس
private bool _isReconnecting = false;
private Timer _connectionCheckTimer;
private int _reconnectionAttempts = 0;
private const int MAX_RECONNECTION_ATTEMPTS = 5;

// 2. تعديل دالة PerformCsaInitialization
private void PerformCsaInitialization()
{
    //Only perform real hardware initializatin logic if the simulated reading mode is not enabled.
    if (AutoTest.UseSimulatedReadings) return;

    if (_isReconnecting) return; // منع المحاولات المتعددة المتزامنة

    UpdateCsaConnectionStatus(CSAState.Detecting);

    //Set the default reading mode
    AutoCsaEmdUnitManagerPhase2.Instance.SetReadingMode(AutoCSAReadingMode.StableReading);

    // محاولة الاتصال مع إعدادات محسنة للجهاز الحديث
    bool connectionSuccessful = TryConnectWithDifferentSettings();

    if (connectionSuccessful)
    {
        //Activate CSA connection and specifying related events.
        AutoCsaEmdUnitManagerPhase2.Instance.ActivateCSAConnection(CsaMeterValueChanged);
        
        // بدء مراقبة الاتصال
        StartConnectionMonitoring();
        
        _reconnectionAttempts = 0; // إعادة تعيين عداد المحاولات
    }
    else
    {
        UpdateCsaConnectionStatus(CSAState.Disconnected);
        
        // جدولة إعادة المحاولة إذا لم نتجاوز الحد الأقصى
        if (_reconnectionAttempts < MAX_RECONNECTION_ATTEMPTS)
        {
            ScheduleReconnection();
        }
    }
}

// 3. دالة جديدة لتجربة إعدادات مختلفة
private bool TryConnectWithDifferentSettings()
{
    // قائمة بالإعدادات المختلفة للتجربة
    var settingsToTry = new[]
    {
        // إعدادات للجهاز الحديث
        new { BaudRate = 9600, Timeout = 5000, Description = "Modern Device Settings" },
        new { BaudRate = 19200, Timeout = 4000, Description = "High Speed Settings" },
        
        // الإعدادات الأصلية
        new { BaudRate = 1200, Timeout = 1500, Description = "Original CSA Settings" },
        new { BaudRate = 2400, Timeout = 2000, Description = "Alternative Settings" }
    };

    foreach (var settings in settingsToTry)
    {
        Logger.LogInfo($"محاولة الاتصال باستخدام: {settings.Description}");
        
        var connectionFilter = new SerialPortConnectionFilter(HardwareType.CSA)
        {
            ComPortNumber = 0, // كشف تلقائي أولاً
            BaudRate = settings.BaudRate,
            DataBit = 8,
            Timeout = settings.Timeout,
            Dtr = false,
            Rts = true,
            AutoComPortDetection = true
        };

        try
        {
            var result = AutoCsaEmdUnitManagerPhase2.Instance.OpenCSAConnection(connectionFilter);
            
            if (result.IsSucceed)
            {
                // انتظار قصير للتأكد من الاتصال
                Thread.Sleep(2000);
                
                if (AutoCsaEmdUnitManagerPhase2.Instance.IsCsaEmdUnitConnected)
                {
                    Logger.LogInfo($"نجح الاتصال باستخدام: {settings.Description}");
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.LogError($"فشل في الاتصال باستخدام {settings.Description}: {ex.Message}");
        }

        // إغلاق الاتصال قبل المحاولة التالية
        try
        {
            AutoCsaEmdUnitManagerPhase2.Instance.CloseCSAConnection();
        }
        catch { }

        Thread.Sleep(1000); // انتظار بين المحاولات
    }

    // إذا فشل الكشف التلقائي، جرب منافذ محددة
    return TrySpecificPorts();
}

// 4. دالة لتجربة منافذ محددة
private bool TrySpecificPorts()
{
    Logger.LogInfo("تجربة منافذ محددة...");
    
    for (int port = 1; port <= 20; port++)
    {
        var connectionFilter = new SerialPortConnectionFilter(HardwareType.CSA)
        {
            ComPortNumber = port,
            BaudRate = 9600, // بدء بالسرعة العالية للجهاز الحديث
            DataBit = 8,
            Timeout = 3000,
            Dtr = false,
            Rts = true,
            AutoComPortDetection = false
        };

        try
        {
            Logger.LogInfo($"تجربة المنفذ COM{port}...");
            
            var result = AutoCsaEmdUnitManagerPhase2.Instance.OpenCSAConnection(connectionFilter);
            
            if (result.IsSucceed)
            {
                Thread.Sleep(2000); // انتظار للتأكد من الاتصال
                
                if (AutoCsaEmdUnitManagerPhase2.Instance.IsCsaEmdUnitConnected)
                {
                    Logger.LogInfo($"نجح الاتصال على المنفذ COM{port}");
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.LogDebug($"فشل على المنفذ COM{port}: {ex.Message}");
        }

        // إغلاق الاتصال قبل المحاولة التالية
        try
        {
            AutoCsaEmdUnitManagerPhase2.Instance.CloseCSAConnection();
        }
        catch { }

        Thread.Sleep(500); // انتظار قصير بين المحاولات
    }

    return false;
}

// 5. دالة لبدء مراقبة الاتصال
private void StartConnectionMonitoring()
{
    if (_connectionCheckTimer != null)
    {
        _connectionCheckTimer.Stop();
        _connectionCheckTimer.Dispose();
    }

    _connectionCheckTimer = new Timer(5000); // فحص كل 5 ثوانٍ
    _connectionCheckTimer.Elapsed += (sender, e) =>
    {
        if (!AutoTest.UseSimulatedReadings)
        {
            CheckConnectionStatus();
        }
    };
    _connectionCheckTimer.Start();
}

// 6. تحسين دالة فحص حالة الاتصال
private void CheckConnectionStatus()
{
    try
    {
        bool isConnected = AutoCsaEmdUnitManagerPhase2.Instance.IsConnectionOpen && 
                          AutoCsaEmdUnitManagerPhase2.Instance.IsCsaEmdUnitConnected;

        if (!isConnected && AutoTest.CurrentCsaState == CSAState.Connected)
        {
            Logger.LogWarning("تم اكتشاف انقطاع في الاتصال");
            UpdateCsaConnectionStatus(CSAState.Disconnected);
            
            // محاولة إعادة الاتصال إذا لم يكن الاختبار قيد التشغيل
            if (AutoTest.CurrentTestStatus != AutoTestStatus.InProgress)
            {
                ScheduleReconnection();
            }
        }
        else if (isConnected && AutoTest.CurrentCsaState != CSAState.Connected)
        {
            Logger.LogInfo("تم استعادة الاتصال");
            UpdateCsaConnectionStatus(CSAState.Connected);
        }
    }
    catch (Exception ex)
    {
        Logger.LogError($"خطأ في فحص حالة الاتصال: {ex.Message}");
    }
}

// 7. دالة لجدولة إعادة الاتصال
private void ScheduleReconnection()
{
    if (_isReconnecting) return;

    _reconnectionAttempts++;
    
    if (_reconnectionAttempts > MAX_RECONNECTION_ATTEMPTS)
    {
        Logger.LogError("تجاوز الحد الأقصى لمحاولات إعادة الاتصال");
        return;
    }

    _isReconnecting = true;
    
    // انتظار متزايد بين المحاولات
    int delay = _reconnectionAttempts * 2000; // 2, 4, 6, 8, 10 ثوانٍ
    
    Timer reconnectTimer = new Timer(delay);
    reconnectTimer.Elapsed += (sender, e) =>
    {
        reconnectTimer.Stop();
        reconnectTimer.Dispose();
        
        _isReconnecting = false;
        
        Logger.LogInfo($"محاولة إعادة الاتصال رقم {_reconnectionAttempts}");
        PerformCsaInitialization();
    };
    reconnectTimer.Start();
}

// 8. تحسين دالة إغلاق الاتصال
private void DisconnectCSAHandlers()
{
    // إيقاف مراقبة الاتصال
    if (_connectionCheckTimer != null)
    {
        _connectionCheckTimer.Stop();
        _connectionCheckTimer.Dispose();
        _connectionCheckTimer = null;
    }

    //Close the CSA connection
    AutoCsaEmdUnitManagerPhase2.Instance.CancelCSAAutoDetection();

    //CSA Handlers
    AutoCsaEmdUnitManagerPhase2.Instance.Detecting -= DetectingEvent;
    AutoCsaEmdUnitManagerPhase2.Instance.Connected -= ConnectedEvent;
    AutoCsaEmdUnitManagerPhase2.Instance.Disconnected -= DisconnectedEvent;
    AutoCsaEmdUnitManagerPhase2.Instance.ResponseReceived -= ResponseReceived;

    //Dispose CSA connection after removing handlers
    AutoCsaEmdUnitManagerPhase2.Instance.DisposeCSAConnection(CsaMeterValueChanged);

    //Close CSA connections
    AutoCsaEmdUnitManagerPhase2.Instance.CloseCSAConnection();
    
    _isReconnecting = false;
    _reconnectionAttempts = 0;
}

// 9. إضافة زر إعادة الاتصال اليدوي
private void simpleButtonReconnectHW_Click(object sender, EventArgs e)
{
    if (AutoTest.UseSimulatedReadings) return;

    Logger.LogInfo("بدء إعادة الاتصال اليدوي...");
    
    // إعادة تعيين المتغيرات
    _isReconnecting = false;
    _reconnectionAttempts = 0;
    
    // إغلاق الاتصال الحالي
    try
    {
        AutoCsaEmdUnitManagerPhase2.Instance.CloseCSAConnection();
    }
    catch { }

    // محاولة إعادة الاتصال
    PerformCsaInitialization();
}