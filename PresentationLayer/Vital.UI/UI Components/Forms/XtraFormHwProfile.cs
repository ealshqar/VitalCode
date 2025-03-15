using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraLayout.Utils;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormHwProfile : DevExpress.XtraEditors.XtraForm
    {
        #region Fields

        private HwProfile _hwProfile;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the hw profile.
        /// </summary>
        public HwProfile HwProfile
        {
            get
            {
                return _hwProfile;
            }
            set
            {
                _hwProfile = value;
            } 
        }

        /// <summary>
        /// Check if hw profile is new.
        /// </summary>
        public bool IsNewHwProfile { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// The Constructor.
        /// </summary>
        public XtraFormHwProfile()
        {
            InitializeComponent();
        }

        #endregion

        #region Initialization & Binding & Helpers

        /// <summary>
        /// Setting some properties.
        /// </summary>
        public void SetProperties()
        {
            textEditProfileName.Properties.MaxLength = 200;
        }

        /// <summary>
        /// Sets controls edit mode based Hw profile properties.
        /// </summary>
        private void SetEditMode()
        {
            if (HwProfile.IsSystemProfile)
            {
                textEditProfileName.Properties.ReadOnly = true;
                //spinEditMinReading.Properties.ReadOnly = true;
                //spinEditStabilityRange.Properties.ReadOnly = true;
                //spinEditStabilityTimeout.Properties.ReadOnly = true;
                //spinEditDisconnectedTimeout.Properties.ReadOnly = true;
                pictureEditHwProfileImage.Properties.ReadOnly = true;
                simpleButtonChangeHwProfileImage.Enabled = false;
            }

            barButtonItemReset.Visibility = HwProfile.IsSystemProfile ? BarItemVisibility.Always : BarItemVisibility.Never;
            layoutControlItemChangeImage.Visibility = HwProfile.IsSystemProfile ? LayoutVisibility.Never : LayoutVisibility.Always;

            if (HwProfile.IsDefault)
                checkEditUseAsDefualt.Properties.ReadOnly = true;

        }

        /// <summary>
        /// Bind the controls of the form.
        /// </summary>
        public void SetBinding()
        {

            if (_hwProfile == null)
                return;

            if (IsNewHwProfile)
                barButtonItemCancel.Visibility = BarItemVisibility.Always;

            Text = string.Format(StaticKeys.HwProfileTitle, IsNewHwProfile ? "New" : HwProfile.Name);

            UiHelperClass.ShowWaitingPanel(StaticKeys.BindingInformationMessgae);
            UiHelperClass.BindControl(textEditProfileName, _hwProfile, () => _hwProfile.Name);
            UiHelperClass.BindControl(spinEditMinReading, _hwProfile, () => _hwProfile.MinReading);
            UiHelperClass.BindControl(spinEditStabilityRange, _hwProfile, () => _hwProfile.StabilityRange);
            UiHelperClass.BindControl(spinEditStabilityTimeout, _hwProfile, () => _hwProfile.StabilityTimeout);
            UiHelperClass.BindControl(spinEditDisconnectedTimeout, _hwProfile, () => _hwProfile.DisconnectedTimeout);
            UiHelperClass.BindControl(checkEditUseAsDefualt, _hwProfile, () => _hwProfile.IsDefault);
            UiHelperClass.BindControl(pictureEditHwProfileImage, _hwProfile, () => _hwProfile.Image);

            dxErrorProviderMain.DataSource = _hwProfile;
            dxErrorProviderMain.ClearErrors();
            UiHelperClass.HideSplash();
        }

        #endregion

        #region Save related actions

        /// <summary>
        /// Performs logic of the done action
        /// </summary>
        private void DoneAction()
        {
            if(!HwProfile.Validate())
                return;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Performs logic of the cancel action
        /// </summary>
        private void CancelAction()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region Logic

        /// <summary>
        /// Performs logic of the cancel action
        /// </summary>
        private void ResetAction()
        {
            _hwProfile.RestoreDefaults();
        }

        #endregion

        #region Handler

        /// <summary>
        /// Close the form by done or cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ItemClickEventHandler(barManager_ItemClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (string.IsNullOrEmpty(e.Item.Name)) return;

                if (e.Item == barButtonItemDone)
                {
                    DoneAction();
                }
                else if (e.Item == barButtonItemCancel)
                {
                    CancelAction();
                }
                else if (e.Item == barButtonItemReset)
                {
                    ResetAction();
                }
            }
        }

        /// <summary>
        /// Handle click change image button to show the select file dialog.
        /// </summary>
        private void simpleButtonChangeHwProfileImage_Click(object sender, System.EventArgs e)
        {
            pictureEditHwProfileImage.LoadImage();
        }

        /// <summary> 
        /// Handle the form load to init and bind dialog.
        /// </summary>
        private void XtraFormHwProfile_Load(object sender, EventArgs e)
        {
            SetProperties();
            SetEditMode();
            SetBinding();
        }

        /// <summary>
        /// Handles the key down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormHwProfile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoneAction();
            }
            else if (IsNewHwProfile && e.KeyCode == Keys.Escape)
            {
                CancelAction();
            }
        }

        /// <summary>
        /// Applies logic when hardware gets active
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormHwProfile_Activated(object sender, EventArgs e)
        {
            if (HwProfile.IsSystemProfile)
            {
                spinEditMinReading.Focus();
            }
        }

        #endregion
        
    }
}
