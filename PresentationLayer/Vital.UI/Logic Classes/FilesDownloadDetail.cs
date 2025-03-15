using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Windows.Forms;
using Vital.Business.Managers;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.UI_Components.Forms;
using Vital.Update.Managers;

namespace Vital.UI.Logic_Classes
{
    public class FilesDownloadDetail
    {
        #region Fileds

        public string FilesUrl { get; set; }
        public FilesDownloadType DownloadType { get; set; }
        public bool UseFilesUrlAsFullURL { get; set; }
        public bool DownloadBasedOnListFile { get; set; }
        public List<string> FilesList { get; set; }
        public bool CacheFileListOnUpdateCheck { get; set; }
        public bool DeleteDownloadFolderOnCancel { get; set; }

        #endregion

        #region File Names

        public string DownloadFolderName { get; set; }
        public string LogFile { get; set; }
        public string ResultFile { get; set; }
        public string ListFile { get; set; }

        #endregion

        #region Contructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FilesDownloadDetail()
        {
            //By default FilesUrl will be used to locate a config file URL, however we can override this and use it as full URL instead based on this flag if that is need, in such cases it will be set to true.
            UseFilesUrlAsFullURL = false;
            DownloadBasedOnListFile = true;
            FilesList = new List<string>();
        }

        #endregion

        #region Paths

        /// <summary>
        /// Download folder path
        /// </summary>
        public string DownloadFolderPath
        {
            get
            {
                return Path.Combine(Application.StartupPath,DownloadFolderName);
            }
        }

        /// <summary>
        /// Returns path of the LogFilePath
        /// </summary>
        public string LogFilePath
        {
            get
            {
                return SetFolderFilePath(LogFile);
            }
        }

        /// <summary>
        /// Returns path of the ResultsFilePath
        /// </summary>
        public string ResultsFilePath
        {
            get
            {
                return SetFolderFilePath(ResultFile);
            }
        }

        /// <summary>
        /// Returns path of the ListFilePath
        /// </summary>
        public string ListFilePath
        {
            get
            {
                return SetFolderFilePath(ListFile);
            }
        }

        #endregion
        
        #region Path Checks

        /// <summary>
        /// Returns true if the download folder exits
        /// </summary>
        public bool DownloadFolderExists
        {
            get
            {
                return Directory.Exists(DownloadFolderPath);
            }
        }

        /// <summary>
        /// Returns true if the ResultsFilePath file exists
        /// </summary>
        public bool ResultsFileExists
        {
            get
            {
                return File.Exists(ResultsFilePath);
            }
        }

        /// <summary>
        /// Returns true if the LogFilePath file exists
        /// </summary>
        public bool LogFileExists
        {
            get
            {
                return File.Exists(LogFilePath);
            }
        }

        /// <summary>
        /// Returns true if the ListFilePath file exists
        /// </summary>
        public bool ListFileExists
        {
            get
            {
                return File.Exists(ListFilePath);
            }
        }

        #endregion

        #region Download Logic

        /// <summary>
        /// Read the download file from server and get its contents
        /// </summary>
        /// <returns></returns>
        public DownloadedFileInfo ReadDownloadFile(bool showWaitingPanel = false)
        {
            var downloadedFileInfo = new DownloadedFileInfo();

            try
            {
                if (!UiHelperClass.IsInternetOnline())
                {
                    if (showWaitingPanel)
                    {
                        UiHelperClass.ShowError("Internet Connection Needed","Please check your internet connection and try again.");
                    }

                    downloadedFileInfo.DownloadResut = FilesDownloadResult.NoInternetConnection;
                }
                else
                {
                    if (showWaitingPanel)
                    {
                        UiHelperClass.ShowWaitingPanel("Downloading Info ...");
                    }

                    //If UseFilesUrlAsFullURL is true, then treat FilesUrl as full URL (Without file name) otherwise treat it as a key to load the URL from the config file.
                    //This is useful for working with downloads from links outside the config file where the link is not necessarily always known.
                    var downloadUrlConfigValue = UseFilesUrlAsFullURL? FilesUrl :ConfigurationManager.AppSettings[FilesUrl];
                    var fileDownloadWebRequest = WebRequest.Create(downloadUrlConfigValue + ListFile);

                    var fileDownloadResponse = (HttpWebResponse)fileDownloadWebRequest.GetResponse();
                    var fileDownloadContentStream = fileDownloadResponse.GetResponseStream();

                    if (fileDownloadContentStream == null)
                    {
                        downloadedFileInfo.DownloadResut = FilesDownloadResult.NoFilesToDownload;
                    }
                    else
                    {
                        var fileDownloadReader = new StreamReader(fileDownloadContentStream);

                        var downloadFileContent = fileDownloadReader.ReadToEnd();

                        if (showWaitingPanel)
                        {
                            UiHelperClass.HideSplash();
                        }

                        downloadedFileInfo.Contents = downloadFileContent;
                        downloadedFileInfo.LastModified = fileDownloadResponse.LastModified;
                        downloadedFileInfo.DownloadResut = FilesDownloadResult.Sucess;
                    }
                }                
            }
            catch (Exception exception)
            {
                downloadedFileInfo.DownloadResut = FilesDownloadResult.ErrorOccurred;
            }

            return downloadedFileInfo;
        }

        /// <summary>
        /// Checks if there are upgrade files to download
        /// </summary>
        /// <returns></returns>
        public FileToDownloadCheck CheckIfUpdatesAreAvailable()
        {
            try
            {
                if (!UiHelperClass.IsInternetOnline())
                {
                    return FileToDownloadCheck.NoUpdateAvailable;
                }

                if (DownloadType == FilesDownloadType.Images)
                {
                    //In the case of images we don't need a file with the names of the images on the server, we just compare the local files
                    //against the images table in database and if there are missing files we download them from server if they exist.
                    FilesList = SelectFilesToDownload();
                }
                else
                {
                    //If UseFilesUrlAsFullURL is true, then treat FilesUrl as full URL (Without file name) otherwise treat it as a key to load the URL from the config file.
                    //This is useful for working with downloads from links outside the config file where the link is not necessarily always known.
                    var downloadUrlConfigValue = UseFilesUrlAsFullURL ? FilesUrl : ConfigurationManager.AppSettings[FilesUrl];
                    var fileDownloadWebRequest = WebRequest.Create(downloadUrlConfigValue + ListFile);

                    var fileDownloadResponse = fileDownloadWebRequest.GetResponse();
                    var fileDownloadContentStream = fileDownloadResponse.GetResponseStream();

                    if (fileDownloadContentStream == null)
                    {
                        return FileToDownloadCheck.UpdateCheckErrorOccured;
                    }

                    var fileDownloadReader = new StreamReader(fileDownloadContentStream);
                    var downloadFileContent = fileDownloadReader.ReadToEnd();

                    var filesListReader = new StringReader(downloadFileContent);

                    FilesList = SelectFilesToDownload(filesListReader);
                }
               
                if (FilesList == null)
                {
                    return FileToDownloadCheck.UpdateCheckErrorOccured;
                }

                return FilesList.Any() ? FileToDownloadCheck.UpdatesAvailable : FileToDownloadCheck.NoUpdateAvailable;
            }
            catch (Exception exception)
            {
                return FileToDownloadCheck.NoUpdateAvailable;
            }
        }

        /// <summary>
        /// Returns true if files are downloaded already or if it had to download them and download was successfull
        /// </summary>
        /// <returns></returns>
        public bool CheckIfDownloadedOrDownloadNow(bool allowCancel, bool deleteFilesIfExists = false)
        {
            if (GetDownloadState() == FilesDownloadState.Downloaded)
            {
                //Delete previous files if they exist
                if (deleteFilesIfExists)
                {
                    if (LogFileExists)
                    {
                        File.Delete(LogFilePath);
                    }

                    if (ResultsFileExists)
                    {
                        File.Delete(ResultsFilePath);
                    }

                    if (ListFileExists)
                    {
                        File.Delete(ListFilePath);
                    }
                }
                else
                {
                    return true;    
                }
            }

            return DownloadFiles(allowCancel) == FilesDownloadResult.Sucess;
        }

        /// <summary>
        /// Returns true if the files are downloaded and exist in location
        /// </summary>
        /// <returns></returns>
        public FilesDownloadState GetDownloadState()
        {
            FilesDownloadState result;

            UiHelperClass.ShowWaitingPanel("Checking downloaded Files ...");

            try
            {
                if (DownloadFolderExists)
                {
                    if (LogFileExists && ResultsFileExists)
                    {
                        var downloadResultStreamReader = new StreamReader(ResultsFilePath);
                        var downloadResult = downloadResultStreamReader.ReadToEnd();

                        result = downloadResult.Contains(StaticKeys.DownloadResultString) ? CheckDownloadCompitability() : FilesDownloadState.Failed;

                        downloadResultStreamReader.Close();
                    }
                    else
                    {
                        result = FilesDownloadState.DownloadFilesMissing;
                    }
                }
                else
                {
                    result = FilesDownloadState.DownloadFolderMissing;
                }
            }
            catch (Exception)
            {
                UiHelperClass.ShowInformation(StaticKeys.DownloadErrorOccured);
                result = FilesDownloadState.ErrorDuringCheck;
            }
            UiHelperClass.HideSplash();
            return result;
        }

        /// <summary>
        /// Downloads files
        /// </summary>
        private FilesDownloadResult DownloadFiles(bool allowCancel)
        {
            try
            {
                if (!DownloadFolderExists)
                {
                    Directory.CreateDirectory(DownloadFolderPath);
                }
            }
            catch (Exception)
            {
                UiHelperClass.ShowInformation(StaticKeys.FileAccessError);
                return FilesDownloadResult.FileAccessError;
            }

            if (DownloadFolderExists)
            {
                if (LogFileExists)
                {
                    File.WriteAllText(LogFilePath, String.Empty);
                }
                else
                {
                    var logStream = File.Create(LogFilePath);
                    logStream.Close();
                }

                if (ResultsFileExists)
                {
                    File.WriteAllText(ResultsFilePath, string.Empty);
                }
                else
                {
                    var resultStream = File.Create(ResultsFilePath);
                    resultStream.Close();
                }

                if (LogFileExists && ResultsFileExists)
                {
                    try
                    {
                        //If UseFilesUrlAsFullURL is true, then treat FilesUrl as full URL (Without file name) otherwise treat it as a key to load the URL from the config file.
                        //This is useful for working with downloads from links outside the config file where the link is not necessarily always known.
                        var downloadUrlConfigValue = UseFilesUrlAsFullURL ? FilesUrl : ConfigurationManager.AppSettings[FilesUrl];
                        var fullDownloadURL = downloadUrlConfigValue + ListFile;

                        //Based on this flag we determine if the list of files that we find during check for updates
                        //will be also used again during the download step or if the server will be checked again too
                        //during the download step, this could be useful if we want the download step to check each time to
                        //make sure we are downloading the right files, this could be important in the case something goes
                        //wrong the download and we try again.
                        if (!CacheFileListOnUpdateCheck)
                        {
                            FilesList = new List<string>();
                        }

                        if (DownloadBasedOnListFile)
                        {
                            var fileDownloadWebRequest = WebRequest.Create(fullDownloadURL);

                            var fileDownloadResponse = fileDownloadWebRequest.GetResponse();
                            var fileDownloadContentStream = fileDownloadResponse.GetResponseStream();

                            if (fileDownloadContentStream == null)
                            {
                                return FilesDownloadResult.ErrorOccurred;
                            }

                            var fileDownloadReader = new StreamReader(fileDownloadContentStream);
                            var downloadFileContent = fileDownloadReader.ReadToEnd();

                            var filesListReader = new StringReader(downloadFileContent);

                            if (!CacheFileListOnUpdateCheck)
                            {
                                FilesList = SelectFilesToDownload(filesListReader);
                            }
                        }
                        else
                        {
                            if (!CacheFileListOnUpdateCheck)
                            {
                                FilesList = new List<string> { fullDownloadURL };
                            }
                        }

                        if (FilesList == null)
                        {
                            return FilesDownloadResult.ErrorOccurred;
                        }

                        var filesDownloadDialog = new XtraFormDownloadFiles(FilesList, DownloadFolderPath, true, LogFilePath, allowCancel);

                        filesDownloadDialog.ShowDialog();

                        if (filesDownloadDialog.DownloadResult == FilesDownloadResult.Sucess)
                        {
                            if (CheckDownloadCompitability() == FilesDownloadState.Downloaded)
                            {
                                File.WriteAllText(ResultsFilePath, StaticKeys.DownloadResultString);
                            }

                            return FilesDownloadResult.Sucess;
                        }
                        else
                        {
                            //Clears the download folder in case the download was cancelled
                            if (filesDownloadDialog.DownloadResult == FilesDownloadResult.Cancelled)
                            {
                                DeleteDownloadsFolder();
                            }

                            return filesDownloadDialog.DownloadResult;
                        }
                    }
                    catch (Exception)
                    {
                        UiHelperClass.ShowInformation(StaticKeys.DownloadErrorOccured);
                        return FilesDownloadResult.ErrorOccurred;
                    }
                }
                else
                {
                    return DownloadFiles(allowCancel);
                }
            }
            else
            {
                return DownloadFiles(allowCancel);
            }
        }

        /// <summary>
        /// Check if the list of files in download log are the same as the files in the download folder
        /// </summary>
        /// <returns></returns>
        private FilesDownloadState CheckDownloadCompitability()
        {
            UiHelperClass.ShowWaitingPanel("Checking Download Log Compatibility ...");

            try
            {
                var downloadLogStreamReader = new StreamReader(LogFilePath);

                string fileName;
                while ((fileName = downloadLogStreamReader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(fileName) || !File.Exists(SetFolderFilePath(fileName)))
                    {
                        return FilesDownloadState.DownloadFilesMissing;
                    }
                }
                downloadLogStreamReader.Close();
            }
            catch (Exception)
            {
                UiHelperClass.ShowInformation(StaticKeys.DownloadErrorOccured);
                return FilesDownloadState.ErrorDuringCheck;
            }

            UiHelperClass.HideSplash();

            return FilesDownloadState.Downloaded;
        }

        /// <summary>
        /// Select files to download
        /// </summary>
        /// <param name="filesListReader"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        private List<string> SelectFilesToDownload(StringReader filesListReader = null)
        {
            string fileName;
            var filesList = new List<string>();
            //If UseFilesUrlAsFullURL is true, then treat FilesUrl as full URL (Without file name) otherwise treat it as a key to load the URL from the config file.
            //This is useful for working with downloads from links outside the config file where the link is not necessarily always known.
            var downloadUrlConfigValue = UseFilesUrlAsFullURL ? FilesUrl : ConfigurationManager.AppSettings[FilesUrl];

            var appInfoManager = new AppInfoManager();
            Version vitalStoredVersion;

            switch (DownloadType)
            {
                case FilesDownloadType.Images:
                    
                    //1- Get the images list stored in DB, this is the reference we rely on to determine what images are needed in general.
                    var imagesManager = new ImagesManager();

                    //load the images from database without any filter since we need all the images
                    var images = imagesManager.GetImages(new ImagesFilter()).ToList();

                    try
                    {
                        //if the folder wasn't found for any reason then we create it
                        if (!DownloadFolderExists)
                        {
                            Directory.CreateDirectory(DownloadFolderPath);
                        }
                    }
                    catch (Exception)
                    {
                        UiHelperClass.ShowInformation(StaticKeys.FileAccessError);

                        return filesList;
                    }

                    //Proceed only if the images folder exists
                    if (DownloadFolderExists)
                    {
                        //2- Get list of images existing in local images folder.
                        var imagesDirectoryInfo = new DirectoryInfo(UiHelperClass.ImagesFolderPath);
                        var localImages = imagesDirectoryInfo.GetFiles().Select(f=>f.Name).ToList();

                        //3- Determine missing images that exist in database but not in folder
                        var missingImages = images.Where(image => localImages.All(localImage => localImage != image.Path)).Select(image => image.Path).ToList();

                        //Check with the server only if there are local images missing from the folder
                        if (missingImages.Any())
                        {
                            //4- Load list of images on server
                            var serverImages = UiHelperClass.GetFilesInURL(downloadUrlConfigValue);

                            //5- Locate missing images that are available on server
                            foreach (var missingImage in missingImages)
                            {
                                //If the image was found within the server images then add it to the download list
                                if (serverImages.Any(serverImage => serverImage == missingImage))
                                {
                                    filesList.Add(downloadUrlConfigValue + missingImage);
                                }
                            }    
                        }
                    }
                    break;
                case FilesDownloadType.ReleaseScripts:

                    try
                    {
                        vitalStoredVersion = UiHelperClass.GetVitalStoredVersion(appInfoManager);
                    }
                    catch (Exception)
                    {
                        return null;
                    }

                    if (vitalStoredVersion == null)
                    {
                        return null;
                    }

                    while ((fileName = filesListReader.ReadLine()) != null)
                    {
                        var fileVersion = UiHelperClass.GetReleaseScriptFileVersion(fileName);

                        if (fileVersion > vitalStoredVersion)
                        {
                            filesList.Add(downloadUrlConfigValue + fileName);
                        }
                    }

                break;
                    
                case FilesDownloadType.DataScripts:

                    Version dbCurrentVersion;

                    try
                    {
                        vitalStoredVersion = UiHelperClass.GetVitalStoredVersion(appInfoManager);
                        dbCurrentVersion = UiHelperClass.GetDbVersion(appInfoManager);
                    }
                    catch (Exception)
                    {
                        return null;
                    }

                    if (vitalStoredVersion == null || dbCurrentVersion == null)
                    {
                        return null;
                    }

                    while ((fileName = filesListReader.ReadLine()) != null)
                    {
                        var cleanFileName = Path.GetFileNameWithoutExtension(fileName);

                        //Validate that the file name has two sections in it
                        if (cleanFileName.Split(StaticKeys.DataScriptsFileNameSeparator).Count() != 2)
                        {
                            return null;
                        }

                        var fileVitalVersion = UiHelperClass.GetDataScriptVitalVersion(fileName);
                        var fileDbVersion = UiHelperClass.GetDataScriptDbVersion(fileName);

                        //Below we uses <= because checking on equal only causes the application to miss older dataupdates that are not equal in version
                        if (fileVitalVersion <= vitalStoredVersion && fileDbVersion > dbCurrentVersion)
                        {
                            filesList.Add(downloadUrlConfigValue + fileName);
                        }
                    }
                break;

                case FilesDownloadType.Database:

                    var updatesManager = new ApplicationUpdateManager();
                    Version vitalApplVersion;

                    try
                    {

                        vitalApplVersion = updatesManager.CurrentPublishVersion;
                    }
                    catch (Exception)
                    {
                        return null;
                    }

                    if (vitalApplVersion == null)
                    {
                        return null;
                    }

                    while ((fileName = filesListReader.ReadLine()) != null)
                    {
                        var fileVersion = UiHelperClass.GetReleaseScriptFileVersion(fileName);

                        if (fileVersion == vitalApplVersion)
                        {
                            filesList.Add(downloadUrlConfigValue + fileName);
                        }
                    }

                break;
            }

            return filesList;
        }

        #endregion

        #region General Logic

        /// <summary>
        /// Deletes the download folder and all the files inside
        /// </summary>
        public void DeleteDownloadsFolder()
        {
            //Only delete the folder if the options are set this way, this is important
            //to avoid deleting the folder for cases where we need to keep the files like the images
            //for example
            if (DeleteDownloadFolderOnCancel && DownloadFolderExists)
            {
                Directory.Delete(DownloadFolderPath, true);
            }
        }

        /// <summary>
        /// Returns true if the file path exists
        /// </summary>
        public static bool CheckFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// Gets the path of a file in the download folder
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string SetFolderFilePath(string file)
        {
            return Path.Combine(DownloadFolderPath,file);
        }

        #endregion
    }
}
