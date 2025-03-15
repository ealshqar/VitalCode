using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vital.Business.Shared.Shared;

namespace Vital.UI.Logic_Classes
{
    public class ProductInfo
    {
        /// <summary>
        /// Product Info Key
        /// </summary>
        public ProductInfoEnum Key { get; set; }

        /// <summary>
        /// File Contents
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// File Last modified datetime
        /// </summary>
        public DateTime LastModified { get; set; }
    }
}
