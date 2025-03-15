using System.Reflection;
using Vital.Business.Shared.DomainObjects.Images;

namespace Vital.Business.Shared.DomainObjects.Items
{
    public class ItemDetails : DomainEntity
    {
        #region Private Variables

        private Image _image;
        private int _x;
        private int _y;
       
        #endregion

        #region Public Properties

        /// <summary>
        /// Get or set the Image.
        /// </summary>
        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the X.
        /// </summary>
        public int X
        {
            get { return _x; }
            set
            {
                if(_x != value)
                {
                    _x = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Get or set the Y.
        /// </summary>
        public int Y
        {
            get { return _y; }
            set
            {
                if(_y !=  value)
                {
                    _y = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        #endregion
    }
}
