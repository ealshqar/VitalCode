using System;
using System.Configuration;
using System.Windows.Forms;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.AppInfos;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;
using Vital.Update.Managers;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormAbout : DevExpress.XtraEditors.XtraForm
    {
        #region Private Variables

        private readonly ApplicationUpdateManager _updateManager;
        private readonly AppInfoManager _appInfoManager;

        #endregion

        #region Constructors

        public XtraFormAbout(AboutFormType aboutFormType = AboutFormType.About)
        {
            InitializeComponent();

            _updateManager = new ApplicationUpdateManager();
            _appInfoManager = new AppInfoManager();

            SetFormType(aboutFormType);
        }

        #endregion

        #region Private Methods

        private void SetFormType(AboutFormType aboutFormType)
        {
            switch (aboutFormType)
            {
                    case AboutFormType.ReleaseNotes:
                    Text = StaticKeys.ReleaseNotesFormTitle;
                    break;
                    case  AboutFormType.About:
                    Text = StaticKeys.AboutFormTitle;
                    break;
            }
        }

        #endregion

        #region Handlers

        /// <summary>
        /// On load event handler.
        /// </summary>
        private void XtraFormAbout_Load(object sender, EventArgs e)
        {
            var codeVersion = _updateManager.CurrentPublishVersion;
            var dbVersion = ((AppInfo) CacheHelper.SetOrGetCachableData(CachableDataEnum.DBVersion)).Value;
            var branch = ConfigurationManager.AppSettings["AppBranch"];
            simpleLabelItemVerisonInfo.Text = string.Format("Vital Version: {0} - Database Version: {1} - Branch:{2}", codeVersion,
                                                            dbVersion, branch);
            memoEditReleaseNotes.Lines =
                _appInfoManager.GetAppInfoByProperty(new AppInfoFilter() { Property = EnumNameResolver.Resolve(AppInfoKeys.ReleaseNotes) }).Value.Replace("\\n", "\n").Split(
                        '\n');
        }

        /// <summary>
        /// On key down event handler.
        /// </summary>
        private void XtraFormAbout_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                Close();
            }
        }

        /// <summary>
        /// On open link event handler.
        /// </summary>
        private void hyperLinkEditURL_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            const string mailPrefix = "mailto:";

            if (!e.EditValue.ToString().ToLower().StartsWith(mailPrefix))
            {
                e.EditValue = mailPrefix + e.EditValue;
            }

            Close();
        }

        #endregion
    }
}