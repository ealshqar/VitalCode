using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlAutoTestResults : XtraUserControl
    {
        #region Public Properties

        /// <summary>
        /// TreeList Control
        /// </summary>
        public TreeList TreeList
        {
            get
            {
                return treeListTestingResults;
            }
        }

        /// <summary>
        /// Name Column
        /// </summary>
        public TreeListColumn TreeListColumnName
        {
            get
            {
                return treeListColumnName; 
            }
        }

        /// <summary>
        /// Determines if the last click was in node or not
        /// </summary>
        public bool LastClickInRow { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public XtraUserControlAutoTestResults()
        {
            InitializeComponent();
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handle mouse down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListTestingResults_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //This is used to determine if the click was in row or not because the TreeList doesn't allow unselecting all nodes and so when
            //performing context menu actions on an empty area it would still show as if a row was selected, this flags helps us know
            //whether the user clicked on a node or in an empty area.
            LastClickInRow = UiHelperClass.IsClickInRow(treeListTestingResults);
        }

        #endregion
    }
}
