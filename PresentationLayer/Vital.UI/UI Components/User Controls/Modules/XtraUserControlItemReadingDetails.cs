using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlItemReadingDetails : DevExpress.XtraEditors.XtraUserControl
    {
        #region Constructors

        public XtraUserControlItemReadingDetails()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Sets The current reading value on the gauges.
        /// </summary>
        public float ReadingValue
        {
            set
            {
                xtraUserControlReadingGauge.ReadingValue = value;
            }
        }


        /// <summary>
        /// Sets the current reading location on the gauges.
        /// </summary>
        public Lookup LocationLookup
        {
            set
            {
                xtraUserControlReadingGauge.LocationLookup = value;
            }
        }

        /// <summary>
        /// The yes and no label indicator will be shown or not.
        /// </summary>
        public bool ShowYesNoLabel
        {
            get
            {
                return xtraUserControlReadingGauge.ShowYesNoLabel;
            }
            set
            {
                xtraUserControlReadingGauge.ShowYesNoLabel = value;
            }
        }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Updates the reading value for the gauges.
        /// </summary>
        /// <param name="locationLookup">The location lookup.</param>
        /// <param name="readingValue">The reading value.</param>
        public void UpdateReading(Lookup locationLookup, float readingValue)
        {
            xtraUserControlReadingGauge.ReadingValue = readingValue;
            xtraUserControlReadingGauge.LocationLookup = locationLookup;
        }
        
        /// <summary>
        /// Set the status test status bar.
        /// </summary>
        public void SetReadingStatusBarMode(TestBarStateEnum state, string actionName, float secondsToWait)
        {
           
            xtraUserControlReadingGauge.SetReadingStatusBarMode(state, actionName, secondsToWait);
        }

        #endregion
    }
}
