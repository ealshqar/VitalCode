using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.AutoTestSource;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormAddAutoResult : DevExpress.XtraEditors.XtraForm
    {
        #region Private Members

        private AutoTestSourceManager _autoTestSourceManager;

        #endregion

        #region Properties

        /// <summary>
        /// Search Items
        /// </summary>
        public BindingList<AutoItem> SearchItems
        {
            get
            {
                return gridControlAutoItems.DataSource as BindingList<AutoItem>;
            }
            set
            {
                gridControlAutoItems.DataSource = value;
            }
        }

        /// <summary>
        /// List of checked items
        /// </summary>
        public BindingList<AutoItem> CheckedItems
        {
            get
            {
                return SearchItems == null ? null : SearchItems.Where(item => item.IsChecked).ToBindingList();
            }
        }

        /// <summary>
        /// Search keyword value
        /// </summary>
        private string SearchKeyword
        {
            get
            {
                return textEditSearchKeyword.Text;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public XtraFormAddAutoResult()
        {
            InitializeComponent();
            CustomInitializeComponent();
        }

        #endregion

        #region Logic

        private void CustomInitializeComponent()
        {
            //Set Form Icon
            Icon = Icon.FromHandle(Resources.AutoTest_16.GetHicon());
            ShowIcon = true;

            SetCustomFonts();
        }

        /// <summary>
        /// Handle setting custom fonts included with Vital without installation on user computer
        /// </summary>
        private void SetCustomFonts()
        {
            simpleButtonAddSelected.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            simpleButtonSearch.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            simpleButtonCancel.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);

            //barAndDockingControllerMain.AppearancesDocking.PanelCaption.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            //barAndDockingControllerMain.AppearancesDocking.PanelCaptionActive.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);

            gridViewAutoItems.Appearance.FocusedRow.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            gridViewAutoItems.Appearance.HeaderPanel.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            gridViewAutoItems.Appearance.Row.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);

            layoutControlGroupMain.AppearanceGroup.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            layoutControlGroupMain.AppearanceItemCaption.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            textEditSearchKeyword.Properties.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
        }

        /// <summary>
        /// Perform the search logic for item
        /// </summary>
        private void PerformSearch()
        {
            if (string.IsNullOrEmpty(SearchKeyword))
            {
                UiHelperClass.ShowInformation("Please enter an item name to search for.");
                textEditSearchKeyword.Focus();
                return;
            }

            UiHelperClass.ShowWaitingPanel("Searching for matching items ...");

            SearchItems = _autoTestSourceManager.GetAutoItems(new AutoItemsFilter {Name = SearchKeyword});

            UiHelperClass.HideSplash();
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handle load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormAddAutoResult_Load(object sender, System.EventArgs e)
        {
            //Register mouse wheel event to allow capturing mouse wheel movement to close active editor in treelist control
            MouseWheel += OnMouseWheel;

            _autoTestSourceManager = new AutoTestSourceManager();
            Text = "Add Result";
            textEditSearchKeyword.Focus();
        }

        /// <summary>
        /// Handle search button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSearch_Click(object sender, System.EventArgs e)
        {
            PerformSearch();
        }

        /// <summary>
        /// Handle performing search by pressing enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEditSearchKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
            }
        }

        /// <summary>
        /// Handle confirming close while items are checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonAddSelected_Click(object sender, System.EventArgs e)
        {
            if (CheckedItems != null && CheckedItems.Any())
            {
                DialogResult = DialogResult.Yes;
            }
            else
            {
                UiHelperClass.ShowInformation("Please make sure at least one item is checked.");
            }
        }

        /// <summary>
        /// Handle cancel button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handle mouse wheel event to close active editor to allow scrolling to work without issues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mouseEventArgs"></param>
        private void OnMouseWheel(object sender, MouseEventArgs mouseEventArgs)
        {
            gridViewAutoItems.CloseEditor();
        }

        #endregion
    }
}