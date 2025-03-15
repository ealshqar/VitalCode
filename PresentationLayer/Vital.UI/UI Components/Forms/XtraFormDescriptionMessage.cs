using System;
using System.Windows.Forms;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormDescriptionMessage : DevExpress.XtraEditors.XtraForm
    {
        #region Properties
        
        /// <summary>
        /// Title
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

        /// <summary>
        /// Message
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
        /// Description
        /// </summary>
        public string Description
        {
            get
            {
                return memoEditDescription.Text;
            }
            set
            {
                memoEditDescription.Text = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public XtraFormDescriptionMessage()
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
        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handle keydown event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormDescriptionMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                Close();
            }
        }

        #endregion
    }
}