using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vital.Business.Shared.Filters
{
    public class ImagesFilter
    {
        /// <summary>
        /// Gets or sets the ImageId.
        /// </summary>
        public int ImageId { get; set; }

        /// <summary>
        /// Gets or sets the ImageData.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Gets or sets the ImageExtension.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the ImagePath.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the ImageSize.
        /// </summary>
        public float? Size { get; set; }

        /// <summary>
        /// Gets or sets the ImageOldImageBoxWidth.
        /// </summary>
        public int OldImageBoxWidth { get; set; }

        /// <summary>
        /// Gets or sets the ImageOldImageBoxHeight.
        /// </summary>
        public int OldImageBoxHeight { get; set; }

        /// <summary>
        /// Gets or sets the ImageDescription.
        /// </summary>
        public string Description { get; set; }

    }
}
