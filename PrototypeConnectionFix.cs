// الحل الصحيح: تفعيل الاتصال مع الجهاز الجديد (Prototype)
// التعديلات المطلوبة في XtraFormAutoTest.cs

// 1. تعديل دالة PerformCsaInitialization
private void PerformCsaInitialization()
{
    //Only perform real hardware initializatin logic if the simulated reading mode is not enabled.
    if (AutoTest.UseSimulatedReadings) return;

    UpdateCsaConnectionStatus(CSAState.Detecting);

    //Set the default reading mode
    AutoCsaEmdUnitManagerPhase2.Instance.SetReadingMode(AutoCSAReadingMode.StableReading);

    // ===== الحل الصحيح: تفعيل Prototype بدلاً من CSA =====
    
    // تجربة الاتصال مع الجهاز الجديد (Prototype) أولاً
    bool prototypeConnected = TryConnectPrototype();
    
    if (!prototypeConnected)
    {
        // إذا فشل Prototype، جرب CSA (الجهاز القديم)
        TryConnectCSA();
    }
}

// 2. دالة جديدة للاتصال مع Prototype
private bool TryConnectPrototype()
{
    try
    {
        Logger.LogInfo("محاولة الاتصال مع الجهاز الجديد (Prototype)...");
        
        // إعدادات الجهاز الجديد
        var prototypeFilter = new SerialPortConnectionFilter(HardwareType.Prototype)
        {
            ComPortNumber = 0,      // كشف تلقائي أولاً
            BaudRate = 9600,        // السرعة العالية للجهاز الجديد
            DataBit = 8,
            Timeout = 3000,         // مهلة أطول
            Dtr = false,
            Rts = true,
            AutoComPortDetection = true
        };

        var result = AutoCsaEmdUnitManagerPhase2.Instance.OpenPrototypeConnection(prototypeFilter);
        
        if (result.IsSucceed)
        {
            // انتظار للتأكد من الاتصال
            Thread.Sleep(2000);
            
            if (AutoCsaEmdUnitManagerPhase2.Instance.IsPrototypeConnected)
            {
                Logger.LogInfo("نجح الاتصال مع الجهاز الجديد (Prototype)");
                
                // تفعيل معالجات الأحداث للـ Prototype
                ConnectPrototypeHandlers();
                
                return true;
            }
        }
        
        // إذا فشل الكشف التلقائي، جرب منافذ محددة
        return TryPrototypeSpecificPorts();
    }
    catch (Exception ex)
    {
        Logger.LogError($"خطأ في الاتصال مع Prototype: {ex.Message}");
        return false;
    }
}

// 3. دالة لتجربة منافذ محددة للـ Prototype
private bool TryPrototypeSpecificPorts()
{
    Logger.LogInfo("تجربة منافذ محددة للجهاز الجديد...");
    
    // المنافذ الشائعة للأجهزة الحديثة
    int[] commonPorts = { 3, 4, 5, 6, 7, 8, 9, 10 };
    
    foreach (int port in commonPorts)
    {
        try
        {
            var prototypeFilter = new SerialPortConnectionFilter(HardwareType.Prototype)
            {
                ComPortNumber = port,
                BaudRate = 9600,
                DataBit = 8,
                Timeout = 2000,
                Dtr = false,
                Rts = true,
                AutoComPortDetection = false
            };

            Logger.LogInfo($"تجربة الجهاز الجديد على المنفذ COM{port}...");
            
            var result = AutoCsaEmdUnitManagerPhase2.Instance.OpenPrototypeConnection(prototypeFilter);
            
            if (result.IsSucceed)
            {
                Thread.Sleep(1500);
                
                if (AutoCsaEmdUnitManagerPhase2.Instance.IsPrototypeConnected)
                {
                    Logger.LogInfo($"نجح الاتصال مع الجهاز الجديد على COM{port}");
                    ConnectPrototypeHandlers();
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.LogDebug($"فشل على COM{port}: {ex.Message}");
        }
        
        // إغلاق المحاولة الفاشلة
        try
        {
            AutoCsaEmdUnitManagerPhase2.Instance.ClosePrototypeConnection();
        }
        catch { }
        
        Thread.Sleep(500);
    }
    
    return false;
}

// 4. دالة للاتصال مع CSA (الجهاز القديم) كبديل
private bool TryConnectCSA()
{
    try
    {
        Logger.LogInfo("محاولة الاتصال مع الجهاز القديم (CSA)...");
        
        var csaFilter = new SerialPortConnectionFilter(HardwareType.CSA)
        {
            ComPortNumber = 0,
            BaudRate = 1200,        // السرعة المنخفضة للجهاز القديم
            DataBit = 8,
            Timeout = 1500,
            Dtr = false,
            Rts = true,
            AutoComPortDetection = true
        };

        var result = AutoCsaEmdUnitManagerPhase2.Instance.OpenCSAConnection(csaFilter);
        
        if (result.IsSucceed)
        {
            Thread.Sleep(2000);
            
            if (AutoCsaEmdUnitManagerPhase2.Instance.IsCsaEmdUnitConnected)
            {
                Logger.LogInfo("نجح الاتصال مع الجهاز القديم (CSA)");
                
                // تفعيل معالجات الأحداث للـ CSA
                ConnectCSAHandlers();
                
                return true;
            }
        }
        
        return false;
    }
    catch (Exception ex)
    {
        Logger.LogError($"خطأ في الاتصال مع CSA: {ex.Message}");
        return false;
    }
}

// 5. تفعيل معالجات أحداث Prototype
private void ConnectPrototypeHandlers()
{
    // إزالة معالجات CSA أولاً
    AutoCsaEmdUnitManagerPhase2.Instance.Detecting -= DetectingEvent;
    AutoCsaEmdUnitManagerPhase2.Instance.Connected -= ConnectedEvent;
    AutoCsaEmdUnitManagerPhase2.Instance.Disconnected -= DisconnectedEvent;

    // تفعيل معالجات Prototype
    AutoCsaEmdUnitManagerPhase2.Instance.PrototypeDetecting += PrototypeDetectingEvent;
    AutoCsaEmdUnitManagerPhase2.Instance.PrototypeConnected += PrototypeConnectedEvent;
    AutoCsaEmdUnitManagerPhase2.Instance.PrototypeDisconnected += PrototypeDisconnectedEvent;
    AutoCsaEmdUnitManagerPhase2.Instance.PrototypResponseReceived += PrototypResponseReceived;
    
    Logger.LogInfo("تم تفعيل معالجات أحداث الجهاز الجديد");
}

// 6. تحسين دالة إغلاق الاتصالات
private void DisconnectCSAHandlers()
{
    // إيقاف كشف CSA
    AutoCsaEmdUnitManagerPhase2.Instance.CancelCSAAutoDetection();

    // إزالة معالجات CSA
    AutoCsaEmdUnitManagerPhase2.Instance.Detecting -= DetectingEvent;
    AutoCsaEmdUnitManagerPhase2.Instance.Connected -= ConnectedEvent;
    AutoCsaEmdUnitManagerPhase2.Instance.Disconnected -= DisconnectedEvent;
    AutoCsaEmdUnitManagerPhase2.Instance.ResponseReceived -= ResponseReceived;

    // إزالة معالجات Prototype
    AutoCsaEmdUnitManagerPhase2.Instance.PrototypeDetecting -= PrototypeDetectingEvent;
    AutoCsaEmdUnitManagerPhase2.Instance.PrototypeConnected -= PrototypeConnectedEvent;
    AutoCsaEmdUnitManagerPhase2.Instance.PrototypeDisconnected -= PrototypeDisconnectedEvent;
    AutoCsaEmdUnitManagerPhase2.Instance.PrototypResponseReceived -= PrototypResponseReceived;

    // إغلاق الاتصالات
    AutoCsaEmdUnitManagerPhase2.Instance.DisposeCSAConnection(CsaMeterValueChanged);
    AutoCsaEmdUnitManagerPhase2.Instance.ClosePrototypeConnection();
    AutoCsaEmdUnitManagerPhase2.Instance.CloseCSAConnection();
}

// 7. تحسين معالج الاستجابات
private void PrototypResponseReceived(object sender, PrototypeResponse response, string originData)
{
    if (InvokeRequired)
    {
        try
        {
            if (IsDisposed) return;
            Invoke(new AutoCsaEmdUnitManagerPhase2.OnPrototypResponseReceived(PrototypResponseReceived), 
                   sender, response, originData);
        }
        catch { }
    }
    else
    {
        Logger.LogInfo($"استجابة من الجهاز الجديد: {response} - البيانات: {originData}");
        
        // معالجة الاستجابات حسب النوع
        switch (response)
        {
            case PrototypeResponse.ValidMoisture:
                AddTemporaryNotesLine("فحص الرطوبة: صحيح");
                break;
            case PrototypeResponse.InvalidMoisture:
                AddTemporaryNotesLine("فحص الرطوبة: خطأ");
                break;
            case PrototypeResponse.ValidPressure:
                AddTemporaryNotesLine("فحص الضغط: صحيح");
                break;
            case PrototypeResponse.InvalidPressure:
                AddTemporaryNotesLine("فحص الضغط: خطأ");
                break;
        }
    }
}