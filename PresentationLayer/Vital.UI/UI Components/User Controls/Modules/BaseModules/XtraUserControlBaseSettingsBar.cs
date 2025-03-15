using System;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraEditors;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.User_Controls.Modules.BaseModules
{
    public partial class XtraUserControlBaseSettingsBar : XtraUserControl
    {

        #region PrivateMemebers

        private readonly SettingsManager _settingsManager;
        private int _yesLookupId;
        private int _noLookupId;
        private readonly bool _isInDesignMode;

        #endregion

        #region PublicProperties

        public bool IsInitialized { get; set; }

        public SettingsManager SettingsManagerObject
        {
            get { return _settingsManager; }
        }
        
        #endregion

        #region Constructors

        public XtraUserControlBaseSettingsBar()
        {
            InitializeComponent();

            _isInDesignMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);

            if (!_isInDesignMode)
            {
                _settingsManager = new SettingsManager();
            }
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set Binding.
        /// </summary>
        protected virtual void SetBinding()
        {
             
        }

        /// <summary>
        /// Fill the lookup ids.
        /// </summary>
        private void FillLookupIds()
        {
            var yesLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.Yes));
            var noLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.No));

            if (yesLookup == null || noLookup == null)
                return;

            _yesLookupId = yesLookup.Id;
            _noLookupId = noLookup.Id;

        }        

        #endregion

        #region Events

        /// <summary>
        /// Handle load event to Init the controls.
        /// </summary>
        private void XtraUserControlBaseSettingsBar_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(XtraUserControlBaseSettingsBar_Load), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (_isInDesignMode)
                    return;

                FillLookupIds();
                SetBinding();
                IsInitialized = true;
            }
        }

        #endregion
    }
}
