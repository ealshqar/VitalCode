namespace Vital.Business.Shared.Filters
{
    public class SerailPortNumberFilter
    {
        #region PrivateMembers

        private int _comPortNumber;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set ComPortNumber.
        /// </summary>
        public int ComPortNumber
        {
            get
            {
                return AutoComPortDetection.HasValue && AutoComPortDetection.Value ? 0 : _comPortNumber;
            }
            set { _comPortNumber = value; }
        }

        /// <summary>
        /// Gets auto com port detection 
        /// </summary>
        public bool? AutoComPortDetection { get; set; }

        #endregion
        
    }
}
