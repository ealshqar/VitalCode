using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using Vital.Business.Shared.Shared;

namespace Vital.UI.UI_Components.User_Controls.BioDigital3DModel
{
    public partial class XtraUserControlBioDigital3DModelViewer : XtraUserControl
    {
        #region Private Members

        private XtraUserControlBioDigital3DModel _model;

        #endregion

        #region Constructor

        /// <summary>
        /// Construct a new instance of XtraUserControlBioDigital3DModelViewer.
        /// </summary>
        public XtraUserControlBioDigital3DModelViewer()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the placeholder image.
        /// </summary>
        public Image PlaceholderImage {
            get
            {
                return panelPlaceholder.BackgroundImage;
            }
            set
            {
                panelPlaceholder.BackgroundImage = value;
            } 
        }

        /// <summary>
        /// Gets the model status.
        /// </summary>
        public XtraUserControlBioDigital3DModel.BioDigital3DModelStatus? Status
        {
            get
            {
                if (_model == null)
                    return null;

                return _model.Status;
            }
        }

        /// <summary>
        /// Check if the model is ready.
        /// </summary>
        public bool Ready
        {
            get
            {
                return Status != null && Status.Value == XtraUserControlBioDigital3DModel.BioDigital3DModelStatus.Ready;
            }
        }

        #endregion

        #region Public Methods

        public void Open(XtraUserControlBioDigital3DModel model)
        {
            _model = model;
            _model.StatusChanged += Model_StatusChanged;
            _model.DownloadResourcesProgressChanged += Model_DownloadResourcesProgressChanged;

            UpdateCompoent();
        }

        /// <summary>
        /// Dispose the control.
        /// IMPORTANT: You must call this method on the parent form closed event to avoid conflicts when reuse the model.
        /// </summary>
        public void DisposeControl()
        {
            _model.ResetView();
            panelBrowserContainer.Controls.Remove(_model.Control);
        }

        /// <summary>
        /// Select an object in the model.
        /// </summary>
        /// <param name="objectId">The object Id.</param>
        /// <returns></returns>
        public ProcessResult SelectObject(string objectId)
        {
            return _model.SelectObject(objectId);
        }

        /// <summary>
        /// Enable / Disable the X-Ray mode.
        /// </summary>
        /// <param name="enabled">Is X-Ray mode enabled.</param>
        /// <returns></returns>
        public ProcessResult SetXRayMode(bool enabled)
        {
            return _model.SetXRayMode(enabled);
        }

        /// <summary>
        /// Resets the model view.
        /// </summary>
        /// <returns></returns>
        public ProcessResult ResetView()
        {
            return _model.ResetView();
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
            return _model.SetBgColor(primaryColorRgba, secondaryColorRgba, gradientType);
        }

        #endregion

        #region Private Workers

        /// <summary>
        /// Show the loading controls.
        /// </summary>
        private void SetLoadingMode()
        {
            layoutControlGroupProgressInfo.Visibility = LayoutVisibility.Always;
            layoutControlItemPorgressBar.Visibility = LayoutVisibility.Never;
            layoutControlItemMarqueeBar.Visibility = LayoutVisibility.Always;
            layoutControlGroupError.Visibility = LayoutVisibility.Never;
        }

        /// <summary>
        /// Update the components based on the model status.
        /// </summary>
        private void UpdateCompoent()
        {
            switch (_model.Status)
            {
                case XtraUserControlBioDigital3DModel.BioDigital3DModelStatus.Initializing:
                    SetLoadingMode();
                    labelControlStatus.Text = StaticKeys.Viewer3DModelInitializingMsg;
                    break;
                case XtraUserControlBioDigital3DModel.BioDigital3DModelStatus.DownloadingResources:
                    SetLoadingMode();
                    labelControlStatus.Text = StaticKeys.Viewer3DModelDownloadingResourcesMsg;
                    break;
                case XtraUserControlBioDigital3DModel.BioDigital3DModelStatus.InstallingResources:
                    SetLoadingMode();
                    labelControlStatus.Text = StaticKeys.Viewer3DModelInstallingResourcesMsg;
                    break;
                case XtraUserControlBioDigital3DModel.BioDigital3DModelStatus.PreparingModel:
                    SetLoadingMode();
                    labelControlStatus.Text = StaticKeys.Viewer3DModelPreparingModelMsg;
                    break;
                case XtraUserControlBioDigital3DModel.BioDigital3DModelStatus.LoadingModel:
                case XtraUserControlBioDigital3DModel.BioDigital3DModelStatus.Ready:
                    //Avoid cross threading issues that happened during testing
                    //TODO: Properly handle this moving forward
                    try
                    {
                        if (panelBrowserContainer.Controls.IndexOf(_model.Control) < 0)
                        {
                            panelBrowserContainer.Controls.Add(_model.Control);
                            panelBrowserContainer.Dock = DockStyle.Fill;
                            Controls.Remove(layoutControl1);
                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                    break;
                case XtraUserControlBioDigital3DModel.BioDigital3DModelStatus.NoInternet:
                case XtraUserControlBioDigital3DModel.BioDigital3DModelStatus.LoadingModelFailed:
                    //Avoid cross threading issues that happened during testing
                    //TODO: Properly handle this moving forward
                    try
                    {
                        layoutControlGroupProgressInfo.Visibility = LayoutVisibility.Never;
                        layoutControlGroupError.Visibility = LayoutVisibility.Always;
                        simpleButtonRetry.Enabled = true;
                        if (_model.Status == XtraUserControlBioDigital3DModel.BioDigital3DModelStatus.NoInternet)
                        {
                            pictureEditErrorIcon.EditValue = Properties.Resources.WirelessError;
                            labelControlErrorMessage.Text = StaticKeys.Viewer3DModelNoInternetMsg;
                        }
                        else
                        {
                            pictureEditErrorIcon.EditValue = Properties.Resources.ErrorLg;
                            labelControlErrorMessage.Text = StaticKeys.Viewer3DModelLoadingModelFailedMsg;
                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                    break;
            }
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handle download resources progress changed event.
        /// </summary>
        private void Model_DownloadResourcesProgressChanged(object sender, int progress)
        {
            if (InvokeRequired)
            {
                // Catch the invoking for DownloadResourcesProgressChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlBioDigital3DModel.DownloadResourcesProgressChangedHandler(Model_DownloadResourcesProgressChanged), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (layoutControlItemPorgressBar.Visibility != LayoutVisibility.Always)
                {
                    layoutControlItemPorgressBar.Visibility = LayoutVisibility.Always;
                    layoutControlItemMarqueeBar.Visibility = LayoutVisibility.Never;
                }

                progressBarControlDownlaodResources.EditValue = progress;
            }
        }

        /// <summary>
        /// Handle model status changed event.
        /// </summary>
        /// <param name="sender"></param>
        private void Model_StatusChanged(object sender)
        {
            if (InvokeRequired)
            {
                // Catch the invoking for StatusChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlBioDigital3DModel.StatusChangedHandler(Model_StatusChanged), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                UpdateCompoent();
            }
        }

        /// <summary>
        /// Handle the load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraUserControlBioDigital3DModelViewer_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handle retry loading model action.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonRetry_Click(object sender, EventArgs e)
        {
            simpleButtonRetry.Enabled = false;
            _model.Initialize();
        }

        #endregion

    }
}
