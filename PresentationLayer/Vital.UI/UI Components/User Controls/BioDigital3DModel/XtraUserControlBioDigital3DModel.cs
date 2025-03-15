using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;


namespace Vital.UI.UI_Components.User_Controls.BioDigital3DModel
{
    public partial class XtraUserControlBioDigital3DModel : DevExpress.XtraEditors.XtraUserControl
    {
        #region Enums

        /// <summary>
        /// Enum for the control status. 
        /// </summary>
        public enum BioDigital3DModelStatus
        {
            Initializing,
            PreparingForInstallation,
            DownloadingResources,
            InstallingResources,
            PreparingModel,
            LoadingModel,
            LoadingModelFailed,
            Ready,
            NoInternet,
        }

        #endregion

        #region Private Memebers

        private string _modelSourceUrl;
        private bool _hasError;
        private bool _xRayModeEnabled;

        #endregion

        #region Properties

        /// <summary>
        /// Determines if the control should initialize and load the model in background.
        /// </summary>
        public bool InitializeInBackground { get; set; }

        /// <summary>
        /// Gets the browser control.
        /// </summary>
        public ChromiumWebBrowser Control { get; private set; }

        /// <summary>
        /// Gets the control status.
        /// </summary>
        public BioDigital3DModelStatus Status { get; private set; }

        /// <summary>
        /// Gets the model load result.
        /// </summary>
        public ProcessResult ModelLoadResult { get; private set; }

        /// <summary>
        /// Gets the model Id.
        /// </summary>
        public string ModelId { get; private set; }

        /// <summary>
        /// Gets or sets the base object Id.
        /// </summary>
        public string BaseObjectId { get; private set; }

        /// <summary>
        /// Enable or disable the x-ray mode.
        /// </summary>
        public bool XRayModeEnabled
        {
            get
            {
                return _xRayModeEnabled;
            }
            set
            {
                SetXRayMode(value);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Delegate for status changed handler delegate.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void StatusChangedHandler(object sender);

        /// <summary>
        /// Control status changed event.
        /// </summary>
        public event StatusChangedHandler StatusChanged;

        /// <summary>
        /// Download resources progress changed handler delegate.
        /// </summary>
        public delegate void DownloadResourcesProgressChangedHandler(object sender, int progress);

        /// <summary>
        /// Download resources progress changed event.
        /// </summary>
        public event DownloadResourcesProgressChangedHandler DownloadResourcesProgressChanged;

        #endregion

        #region Delegates

        private delegate void SafeInitCefSharpBrowserDelegate();

        private delegate void SafeBrowserLoadErrorDelegate(object sender, LoadErrorEventArgs args);

        private delegate void SafeBrowserLoadingStateChangedDelegate(object sender, LoadingStateChangedEventArgs args);

        private delegate void SafeDownloadProgressChangedDelegate(int progress);

        private delegate void SafeCefSharpInstallerStatusChangedDelegate();

        #endregion

        #region Constructor

        /// <summary>
        /// Construct new XtraUserControlBioDigital3DModel.
        /// </summary>
        public XtraUserControlBioDigital3DModel()
        {
            InitializeComponent();

            CefSharpInstaller.StatusChanged += CefSharpInstaller_StatusChanged;
            CefSharpInstaller.DownloadResourcesProgressChanged += CefSharpInstaller_DownloadResourcesProgressChanged;

            SetStatus(BioDigital3DModelStatus.Initializing);
        }

        /// <summary>
        /// Construct new XtraUserControlBioDigital3DModel with model Id.
        /// </summary>
        /// <param name="modelId">The model Id.</param>
        /// <param name="baseObejctId">The base object Id</param>
        public XtraUserControlBioDigital3DModel(string modelId, string baseObejctId) : this()
        {
            SetModel(modelId, baseObejctId);
        }

        #endregion

        #region Public Moethods

        /// <summary>
        /// Downloads the resources if needed and initialize the model.
        /// </summary>
        public void Initialize()
        {
            if (!UiHelperClass.IsInternetOnline())
            {
                SetStatus(BioDigital3DModelStatus.NoInternet);
                return;
            }

            _hasError = false;

            CefSharpInstaller.InstallAndInitializ();
        }

        /// <summary>
        /// Sets the model URL and update the control.
        /// </summary>
        /// <param name="modelId">The model Id.</param>
        /// <param name="baseObejctId">The base object Id</param>
        public void SetModel(string modelId, string baseObejctId)
        {
            ModelId = modelId;
            BaseObjectId = baseObejctId;

            _modelSourceUrl = string.Format(ConfigurationManager.AppSettings[StaticKeys.BioDigitalBaseModelUrlConfigKey], modelId); ;

            if (Control != null)
                Control.Load(_modelSourceUrl);
        }

        /// <summary>
        /// Resets the model view.
        /// </summary>
        /// <returns></returns>
        public ProcessResult ResetView()
        {
            return ExecuteScriptAsync("reset();");
        }

        /// <summary>
        /// Select an object in the model.
        /// </summary>
        /// <param name="objectId">The object Id.</param>
        /// <returns></returns>
        public ProcessResult SelectObject(string objectId)
        {
            return ExecuteScriptAsync(string.Format("selectObject('{0}');", string.Format("{0}_{1}", BaseObjectId, objectId)));
        }

        /// <summary>
        /// Enable / Disable the X-Ray mode.
        /// </summary>
        /// <param name="enabled">Is X-Ray mode enabled.</param>
        /// <returns></returns>
        public ProcessResult SetXRayMode(bool enabled)
        {
            _xRayModeEnabled = enabled;
            return ExecuteScriptAsync(string.Format("setXRayMode({0});", enabled.ToString().ToLower()));
        }

        /// <summary>
        /// Sets the background color of the model.
        /// </summary>
        /// <param name="primaryColorRgba">The center of a radial gradient and the top of a linear gradient.</param>
        /// <param name="secondaryColorRgba">The color code.</param>
        /// <param name="gradientType">The type of gradient. Either "radial" or "linear"</param>
        /// <returns></returns>
        public ProcessResult SetBgColor(string primaryColorRgba, string secondaryColorRgba, string gradientType = "radial")
        {
            return ExecuteScriptAsync(string.Format("setBgColor('{0}', '{1}', '{2}');", primaryColorRgba, secondaryColorRgba, gradientType));
        }

        #endregion

        #region Private Workers


        /// <summary>
        /// Init the CefSharp browser.
        /// </summary>
        private void InitCefSharpBrowser()
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new SafeInitCefSharpBrowserDelegate(InitCefSharpBrowser));
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (string.IsNullOrEmpty(_modelSourceUrl))
                    throw new ArgumentException("ModelSourceUrl");

                Control = new ChromiumWebBrowser(_modelSourceUrl);
                Controls.Add(Control);
                Control.Dock = DockStyle.Fill;
                Dock = DockStyle.None;
                Width = 1;
                Height = 1;
                Control.LoadingStateChanged += Browser_LoadingStateChanged;
                Control.LoadError += Browser_LoadError;                

                SetStatus(BioDigital3DModelStatus.PreparingModel);
            }
        }

        /// <summary>
        /// Set the control status and trigger the StatusChanged event.
        /// </summary>
        /// <param name="status">The new status.</param>
        private void SetStatus(BioDigital3DModelStatus status)
        {
            Status = status;

            if (StatusChanged != null)
                StatusChanged(this);
        }

        /// <summary>
        /// Execute a JavaScript code on the model page.
        /// </summary>
        /// <param name="jsCode">The code.</param>
        /// <returns></returns>
        private ProcessResult ExecuteScriptAsync(string jsCode)
        {
            if (Control == null || Control.IsLoading)
                return new ProcessResult {IsSucceed = false, Message = "Model is not ready yet."};

            Control.ExecuteScriptAsync(jsCode);

            return ProcessResult.Succeed;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handle the load event of the control and init it.
        /// </summary>
        private void XtraUserControlBioDigital3DModelCache_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            if (InitializeInBackground)
                Initialize();
        }

        /// <summary>
        /// Handle the loading error for the model.
        /// </summary>
        private void Browser_LoadError(object sender, LoadErrorEventArgs e)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new SafeBrowserLoadErrorDelegate(Browser_LoadError), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                _hasError = true;
                ModelLoadResult = new ProcessResult {IsSucceed = false, Message = e.ErrorText};
                SetStatus(BioDigital3DModelStatus.LoadingModelFailed);
            }
        }

        /// <summary>
        /// Handle the loading done for the model.
        /// </summary>
        private void Browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new SafeBrowserLoadingStateChangedDelegate(Browser_LoadingStateChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.IsLoading || _hasError)
                    return;

                SetStatus(BioDigital3DModelStatus.LoadingModel);

                SetXRayMode(XRayModeEnabled);

                new Thread(ModelLoadStatusCheckerThread) {IsBackground = true}.Start();
               
            }
        }

        /// <summary>
        /// Thread to check the model loading status and update the control status when its ready.
        /// </summary>
        private void ModelLoadStatusCheckerThread()
        {
            while (Status == BioDigital3DModelStatus.LoadingModel)
            {
                try
                {
                    Control.GetMainFrame().EvaluateScriptAsync(@"isModelReady()").ContinueWith(t =>
                    {
                        var isModelReady = (bool) t.Result.Result;

                        if (!isModelReady || Status != BioDigital3DModelStatus.LoadingModel)
                            return;

                        ModelLoadResult = ProcessResult.Succeed;
                        SetStatus(BioDigital3DModelStatus.Ready);
                        //SetXRayMode(XRayModeEnabled);
                    });
                }
                catch
                {
                    
                }

                Thread.Sleep(1500);
            }
        }

        /// <summary>
        /// Handle he download resources progress changes.
        /// </summary>
        /// <param name="progress">The progress.</param>
        private void CefSharpInstaller_DownloadResourcesProgressChanged(int progress)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new SafeDownloadProgressChangedDelegate(CefSharpInstaller_DownloadResourcesProgressChanged), progress);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (DownloadResourcesProgressChanged != null)
                    DownloadResourcesProgressChanged(this, progress);
            }
        }

        /// <summary>
        /// Handle the installation process status changes and take actions based on it.
        /// </summary>
        private void CefSharpInstaller_StatusChanged()
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new SafeCefSharpInstallerStatusChangedDelegate(CefSharpInstaller_StatusChanged));
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                switch (CefSharpInstaller.Status)
                {
                    case CefSharpInstaller.CefSharpInstallationStatus.Initializing:
                        SetStatus(BioDigital3DModelStatus.PreparingForInstallation);
                        break;
                    case CefSharpInstaller.CefSharpInstallationStatus.DownloadingResources:
                        SetStatus(BioDigital3DModelStatus.DownloadingResources);
                        break;
                    case CefSharpInstaller.CefSharpInstallationStatus.InstallingResources:
                        SetStatus(BioDigital3DModelStatus.InstallingResources);
                        break;
                    case CefSharpInstaller.CefSharpInstallationStatus.NoInternet:
                        SetStatus(BioDigital3DModelStatus.NoInternet);
                        break;
                    case CefSharpInstaller.CefSharpInstallationStatus.Ready:
                        SetStatus(BioDigital3DModelStatus.Initializing);
                        InitCefSharpBrowser();
                        break;
                }
            }
        }


        #endregion
    }
}
