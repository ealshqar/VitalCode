using System;
using System.Linq.Expressions;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;
using Vital.Business.Shared.Shared;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlVitalRichEdit : DevExpress.XtraEditors.XtraUserControl
    {
        #region PrivateMembers

        private object _dataSource;
        private string _dataMember;

        #endregion

        #region Constructor

        /// <summary>
        /// The constructor.
        /// </summary>
        public XtraUserControlVitalRichEdit()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the read only flag.
        /// </summary>
        public bool ReadOnly
        {
            get { return richEditControlNotes.ReadOnly; }
            set { richEditControlNotes.ReadOnly = value; }
        }

        /// <summary>
        /// Gets the Rtf format of the notes.
        /// </summary>
        public string NotesRtf
        {
            get
            {
                PostValue();
                return richEditControlNotes.RtfText;
            }
        }

        /// <summary>
        /// Gets the Control of the rich edit.
        /// </summary>
        public RichEditControl Control
        {
            get { return this.richEditControlNotes; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Bind the richEditControl.
        /// </summary>
        /// <param name="dataSource">The dataSource.</param>
        /// <param name="dataMember">The dataMember.</param>
        public ProcessResult Bind<T>(object dataSource, Expression<Func<T>> dataMemberExpression)
        {
            try
            {
                var dataMember = ExpressionHelper.GetPropertyName(dataMemberExpression);

                if (dataSource == null || string.IsNullOrEmpty(dataMember))
                    return ProcessResult.Failed;

                richEditControlNotes.ModifiedChanged -= richEditControlNotes_ModifiedChanged;

                richEditControlNotes.Modified = false;

                _dataSource = dataSource;
                _dataMember = dataMember;

                richEditControlNotes.RtfText = ExpressionHelper.GetPropertyValue(_dataSource, _dataMember) as string;

                richEditControlNotes.ModifiedChanged += richEditControlNotes_ModifiedChanged;

                return ProcessResult.Succeed;
            }
            catch(Exception exception)
            {
                return new ProcessResult{IsSucceed = false, Message = exception.Message};
            }
 
        }

        /// <summary>
        /// Post the changed to dataSource.
        /// </summary>
        public ProcessResult PostValue()
        {
            try
            {
                if (_dataSource == null || string.IsNullOrEmpty(_dataMember))
                    return ProcessResult.Failed;

                ExpressionHelper.SetPropertyValue(_dataSource, _dataMember, richEditControlNotes.RtfText);

                return ProcessResult.Succeed;
            }
            catch(Exception exception)
            {
                return new ProcessResult { IsSucceed = false, Message = exception.Message };
            }
        }

        #endregion
       
        #region Handlers

        /// <summary>
        /// Handel the first change on the richEditControlNote text (Rtf Text ..).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void richEditControlNotes_ModifiedChanged(object sender, EventArgs e)
        {
            PostValue();
        }

        #endregion

        
    }
}
