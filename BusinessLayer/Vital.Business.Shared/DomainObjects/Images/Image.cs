using System.Reflection;

namespace Vital.Business.Shared.DomainObjects.Images
{
    public class Image : DomainEntity
    {
        #region Private Variables

        private byte[] _data;
        private string _extension;
        private string _path;
        private float _size;
        private string _description;
        private int _oldImageBoxWidth;
        private int _oldImageBoxHeight;
        
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the old image box width.
        /// </summary>
        public int OldImageBoxWidth
        {
            get
            {
                return _oldImageBoxWidth;
            }
            set
            {
                if(_oldImageBoxWidth != value)
                {
                    _oldImageBoxWidth = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the old image box height.
        /// </summary>
        public int OldImageBoxHeight
        {
            get
            {
                return _oldImageBoxHeight;
            }
            set
            {
                if(_oldImageBoxHeight != value)
                {
                    _oldImageBoxHeight = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);    
                }
            }
        }

        /// <summary>
        /// Get or set the Data.
        /// </summary>
        public byte[] Data
        {
            get { return _data; }
            set
            {
                if(_data != value)
                {
                    _data = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);    
                }
            }
        }

        /// <summary>
        /// Get or set the Extension.
        /// </summary>
        public string Extension
        {
            get { return _extension; }
            set
            {
                if(_extension != value)
                {
                    _extension = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);    
                }
            }
        }

        /// <summary>
        /// Get or set the Path.
        /// </summary>
        public string Path
        {
            get { return _path; }
            set
            {
                if(_path != value)
                {
                    _path = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);    
                }
            }
        }

        /// <summary>
        /// Get or set the Size.
        /// </summary>
        public float Size
        {
            get { return _size; }
            set
            {
                if(_size != value)
                {
                    _size = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);    
                }
            }
        }

        /// <summary>
        /// Get or set the Description.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                if(_description != value)
                {
                    _description = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }
        
        #endregion
    }
}
