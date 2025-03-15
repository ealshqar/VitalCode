using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using Vital.Business.Shared.Shared;

namespace Vital.UI.Logic_Classes
{
    class VitalHelper
    {
        /// <summary>
        /// Converts the Image to bytes.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public static byte[] ToBytesArray(Image image)
        {
            var memoreyStream = new MemoryStream();
            image.Save(memoreyStream, image.RawFormat);
            return memoreyStream.ToArray();
        }

        /// <summary>
        /// Converts the bytes array to an Image.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static Image ToImage(byte[] bytes)
        {
            var memoreyStream = new MemoryStream(bytes);
            var image = Image.FromStream(memoreyStream);
            return image;
        }

        /// <summary>
        /// Checks if the item has any notes to be shown.
        /// </summary>
        /// <param name="itemName">The item name.</param>
        /// <returns></returns>
        public static string CheckForNotes(string itemName)
        {
            var insertOnNoItems = new List<string>()
                                {
                                    "Vertebrae"
                                };

            if (insertOnNoItems.Contains(itemName))
                return StaticKeys.InsertOnNoNote;

            return string.Empty;

        }
    }
}
