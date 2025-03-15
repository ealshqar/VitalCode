using System;
using Vital.Business.Shared.Shared;

namespace Vital.UI.Logic_Classes
{
    public class DownloadedFileInfo
    {
        /// <summary>
        /// File Contents
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// File Last modified datetime
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// File download resut
        /// </summary>
        public FilesDownloadResult DownloadResut { get; set; }
    }
}
