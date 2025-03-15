using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading;
using CefSharp;
using Vital.Business.Shared.Shared;
using System.IO.Compression;

namespace Vital.UI.Logic_Classes
{
    public static class CefSharpInstaller
    {
        #region Enums

        /// <summary>
        /// Enum for the CefSharp installation process.
        /// </summary>
        public enum CefSharpInstallationStatus
        {
            Initializing,
            DownloadingResources,
            InstallingResources,
            Ready,
            NoInternet,
        }

        #endregion

        #region Private Memebers

        private static readonly string CefSharpRespurcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings[StaticKeys.CefSharpResourcesLoaclFileNameConfigKey]);
        private static bool _cefSharpInitialized;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the installation status.
        /// </summary>
        public static CefSharpInstallationStatus Status { get; private set; }

        /// <summary>
        /// Gets the file name with path for the file that determines if the cefSharp resources is installed.
        /// </summary>
        public static string CefSharpResouresInstalledFlagFileWithPath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,StaticKeys.CefSharpResouresInstalledFlagFileName);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Delegate for status changed handler delegate.
        /// </summary>
        public delegate void StatusChangedHandler();

        /// <summary>
        /// Control status changed event.
        /// </summary>
        public static event StatusChangedHandler StatusChanged;

        /// <summary>
        /// Download resources progress changed handler delegate.
        /// </summary>
        public delegate void DownloadResourcesProgressChangedHandler(int progress);

        /// <summary>
        /// Download resources progress changed event.
        /// </summary>
        public static event DownloadResourcesProgressChangedHandler DownloadResourcesProgressChanged;

        #endregion

        #region Public Moethods

        /// <summary>
        /// Downloads and install the resources if needed.
        /// </summary>
        public static void InstallAndInitializ()
        {
            if (!UiHelperClass.IsInternetOnline())
            {
                SetStatus(CefSharpInstallationStatus.NoInternet);
                return;
            }

            CheckResourcesThenInit();
        }


        #endregion

        #region Private Workers

        /// <summary>
        /// Init the CefSharp library.
        /// </summary>
        private static void InitCefSharp()
        {
            if (!_cefSharpInitialized)
            {
                Cef.Initialize(new CefSettings());
                _cefSharpInitialized = true;
            }

            SetStatus(CefSharpInstallationStatus.Ready);
        }

       

        /// <summary>
        /// Set the control status and trigger the StatusChanged event.
        /// </summary>
        /// <param name="status">The new status.</param>
        private static void SetStatus(CefSharpInstallationStatus status)
        {
            Status = status;

            if (StatusChanged != null)
                StatusChanged();
        }

        /// <summary>
        /// Check the resources, download them if not exists, and then init the browser control
        /// </summary>
        private static void CheckResourcesThenInit()
        {
            if (Status == CefSharpInstallationStatus.DownloadingResources || Status == CefSharpInstallationStatus.InstallingResources)
            {
                if (StatusChanged != null)
                    StatusChanged();

                return;
            }

            if (IsCefSharpResourcesInstalled())
            {
               InitCefSharp();
               return;
            }

            DownloadCefSharpResources();
        }

        /// <summary>
        /// Download CefSharp resources.
        /// </summary>
        private static void DownloadCefSharpResources()
        {
            SetStatus(CefSharpInstallationStatus.DownloadingResources);

            // Check and download the resources Zip file.
            if (File.Exists(CefSharpRespurcesPath))
            {
                // Resources Zip file exists, install the resources.
                StartCefSharpInstallationThread();
            }
            else
            {
                // Download then install the Resources.
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri(ConfigurationManager.AppSettings[StaticKeys.CefSharpResourcesUrlConfigKey]), CefSharpRespurcesPath);
                    client.DownloadProgressChanged += client_DownloadProgressChanged;
                    client.DownloadFileCompleted += client_DownloadFileCompleted;
                }
            }
        }

        /// <summary>
        /// Install the CefSharp Resources.
        /// </summary>
        private static void InstallCefSharpResources()
        {
            SetStatus(CefSharpInstallationStatus.InstallingResources);

            // Extract the resources from the zip file and overwrite the existing files.
            using (var archive = ZipFile.OpenRead(CefSharpRespurcesPath))
            {
                foreach (var entry in archive.Entries)
                {
                    var destinationPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, entry.FullName));

                    // Check and create directory if not exists.
                    var directory = Path.GetDirectoryName(destinationPath);
                    if (directory != null)
                        Directory.CreateDirectory(directory);

                    entry.ExtractToFile(destinationPath, true);
                }
            }

            // Mark the resources as insalled.
            MarkCefSharpResourcesAsInstalled();

            // Init the ChefSharp.
            InitCefSharp();
        }

        /// <summary>
        /// Checks if the CefSharp resources installed.
        /// </summary>
        /// <returns></returns>
        private static bool IsCefSharpResourcesInstalled()
        {
            try
            {
                return File.Exists(CefSharpResouresInstalledFlagFileWithPath);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Marks the CefSharp resources as installed.
        /// </summary>
        private static void MarkCefSharpResourcesAsInstalled()
        {
            try
            {
                // Create a file to indicate that the CefSahrp resources has successfully installed.
                // We implemented this using a physical file to solve an issue after the app get updated where it get moved to a new location so we need to re-download and install the CefSharp resources.
                // We cannot rely on a user settings flag because it would not get updated after the app gets an update.
                if (!IsCefSharpResourcesInstalled())
                    File.Create(CefSharpResouresInstalledFlagFileWithPath).Dispose();
            }
            catch
            {
                
            }
        }

        /// <summary>
        /// Starts the CefSharp installation thread.
        /// </summary>
        private static void StartCefSharpInstallationThread()
        {
            new Thread(InstallCefSharpResources).Start();
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handle the download resources completed event.
        /// </summary>
        private static void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                SetStatus(CefSharpInstallationStatus.NoInternet);
                return;
            }

            StartCefSharpInstallationThread();
        }

        /// <summary>
        /// Handle the download resources progress changed event.
        /// </summary>
        private static void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (DownloadResourcesProgressChanged != null)
                DownloadResourcesProgressChanged(e.ProgressPercentage);
        }

        #endregion
    }
}
