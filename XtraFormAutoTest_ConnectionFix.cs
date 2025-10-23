// حل مؤقت لمشكلة الاتصال في XtraFormAutoTest
// يجب إضافة هذا الكود في دالة PerformCsaInitialization

private void PerformCsaInitialization()
{
    //Only perform real hardware initializatin logic if the simulated reading mode is not enabled.
    if (AutoTest.UseSimulatedReadings) return;

    UpdateCsaConnectionStatus(CSAState.Detecting);

    //Set the default reading mode
    AutoCsaEmdUnitManagerPhase2.Instance.SetReadingMode(AutoCSAReadingMode.StableReading);

    // ===== الحل المقترح =====
    
    // 1. تجربة إعدادات مختلفة للجهاز الحديث
    var connectionFilter = new SerialPortConnectionFilter(HardwareType.CSA)
    {
        ComPortNumber = 0, // استخدام الكشف التلقائي أولاً
        BaudRate = 9600,   // تجربة سرعة أعلى للجهاز الحديث
        DataBit = 8,
        Timeout = 5000,    // زيادة المهلة الزمنية
        Dtr = false,
        Rts = true,
        AutoComPortDetection = true
    };

    try
    {
        // محاولة الاتصال مع الإعدادات الجديدة
        var result = AutoCsaEmdUnitManagerPhase2.Instance.OpenCSAConnection(connectionFilter);
        
        if (!result.IsSucceed)
        {
            // إذا فشل، جرب إعدادات أخرى
            connectionFilter.BaudRate = 1200; // الإعداد الأصلي
            connectionFilter.Timeout = 3000;
            result = AutoCsaEmdUnitManagerPhase2.Instance.OpenCSAConnection(connectionFilter);
        }
        
        if (!result.IsSucceed)
        {
            // إذا فشل مرة أخرى، جرب منفذ محدد
            for (int port = 1; port <= 20; port++)
            {
                connectionFilter.ComPortNumber = port;
                connectionFilter.AutoComPortDetection = false;
                result = AutoCsaEmdUnitManagerPhase2.Instance.OpenCSAConnection(connectionFilter);
                
                if (result.IsSucceed)
                {
                    break;
                }
                
                // انتظار قصير بين المحاولات
                Thread.Sleep(500);
            }
        }
    }
    catch (Exception ex)
    {
        // تسجيل الخطأ
        Logger.LogError($"فشل في الاتصال مع الجهاز: {ex.Message}");
        UpdateCsaConnectionStatus(CSAState.Disconnected);
        return;
    }

    //Activate CSA connection and specifying related events.
    AutoCsaEmdUnitManagerPhase2.Instance.ActivateCSAConnection(CsaMeterValueChanged);
}

// إضافة دالة للتحقق من حالة الاتصال
private void CheckConnectionStatus()
{
    if (!AutoCsaEmdUnitManagerPhase2.Instance.IsConnectionOpen || 
        !AutoCsaEmdUnitManagerPhase2.Instance.IsCsaEmdUnitConnected)
    {
        UpdateCsaConnectionStatus(CSAState.Disconnected);
        
        // محاولة إعادة الاتصال
        PerformCsaInitialization();
    }
    else
    {
        UpdateCsaConnectionStatus(CSAState.Connected);
    }
}

// تحسين دالة ConnectedEvent
private void ConnectedEvent(object sender)
{
    if (InvokeRequired)
    {
        try
        {
            if (IsDisposed) return; 
            Invoke(new AutoCsaEmdUnitManagerPhase2.OnConnected(ConnectedEvent), sender);
        }
        catch { }
    }
    else
    {
        // إضافة تسجيل للتأكد من الاتصال
        Logger.LogInfo("تم الاتصال بالجهاز بنجاح");
        
        // التحقق من أن الجهاز يرسل بيانات فعلاً
        if (AutoCsaEmdUnitManagerPhase2.Instance.IsCsaEmdUnitConnected)
        {
            UpdateCsaConnectionStatus(CSAState.Connected);
        }
    }
}

// تحسين دالة DisconnectedEvent
private void DisconnectedEvent(object sender)
{
    if (InvokeRequired)
    {
        try
        {
            if (IsDisposed) return; 
            Invoke(new AutoCsaEmdUnitManagerPhase2.OnDisconnected(DisconnectedEvent), sender);
        }
        catch { }
    }
    else
    {
        Logger.LogWarning("انقطع الاتصال مع الجهاز");
        UpdateCsaConnectionStatus(CSAState.Disconnected);
        
        // محاولة إعادة الاتصال التلقائي بعد 3 ثوانٍ
        Timer reconnectTimer = new Timer(3000);
        reconnectTimer.Elapsed += (s, e) =>
        {
            reconnectTimer.Stop();
            reconnectTimer.Dispose();
            
            if (!AutoTest.UseSimulatedReadings && 
                AutoTest.CurrentTestStatus != AutoTestStatus.InProgress)
            {
                PerformCsaInitialization();
            }
        };
        reconnectTimer.Start();
    }
}