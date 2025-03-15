using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormInformationMessage : XtraForm
    {
        #region Members

        /// <summary>
        /// Information Image
        /// </summary>
        public Image InfoImage
        {
            get
            {
                return layoutControlItemHeader.Image;
            }
            set
            {
                layoutControlItemHeader.Image = value;
            }
        }

        /// <summary>
        /// Header Text
        /// </summary>
        public string Header
        {
            get
            {
                return memoEditHeader.Text;
            }
            set
            {
                memoEditHeader.Text = value;
            }
        }

        /// <summary>
        /// Message Text
        /// </summary>
        public string Message
        {
            get
            {
                return memoEditMessage.Text;
            }
            set
            {
                memoEditMessage.Text = value;
            }
        }

        /// <summary>
        /// Window Title
        /// </summary>
        public string Title
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public XtraFormInformationMessage()
        {
            InitializeComponent();
        }

        #endregion

        #region Handlers
        
        /// <summary>
        /// Handle close button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handle load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormInformationMessage_Load(object sender, System.EventArgs e)
        {
            layoutControlItemHeader.Visibility = string.IsNullOrEmpty(Header)? LayoutVisibility.Never : LayoutVisibility.Always;
        }

        #endregion
    }
}