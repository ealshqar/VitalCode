using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormDownloadFiles : DevExpress.XtraEditors.XtraForm
    {
        #region Fields

        private Queue<string> _urlsQuque;
        private WebClient _downloadClient;

        private int _currentFileCount;
        private int _filesCount;

        private string _lastURL;
        private string _lastFileName;

        #endregion

        #region Properties

        /// <summary>
        /// List Of URLS for files to download
        /// </summary>
        public List<string> UrlsToDownload { get; set; }

        /// <summary>
        /// The location to download files in
        /// </summary>
        public string DownloadLocation { get; set; }

        /// <summary>
        /// Download Process Result
        /// </summary>
        public ProcessResult DownloadProcessResult { get; set; }

        /// <summary>
        /// Enum based download result
        /// </summary>
        public FilesDownloadResult DownloadResult { get; set; }

        /// <summary>
        /// Indicates if a the download process will update a log file
        /// </summary>
        public bool UpdateLogFile { get; set; }

        /// <summary>
        /// The location of log file to update
        /// </summary>
        public string DownloadLogFilePath { get; set; }

        /// <summary>
        /// Indicates if the download can be cancelled
        /// </summary>
        public bool EnableCancel { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public XtraFormDownloadFiles(List<string> urlsToDownload, string downloadLocation, bool updateLogFile, string downloadLogFilePath, bool enableCancel)
        {
            InitializeComponent();
            UrlsToDownload = urlsToDownload;
            DownloadLocation = downloadLocation;
            UpdateLogFile = updateLogFile;
            DownloadLogFilePath = downloadLogFilePath;
            EnableCancel = enableCancel;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public XtraFormDownloadFiles()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Start Files Download from URL
        /// </summary>
        private void DownloadFiles()
        {
            _urlsQuque = new Queue<string>();

            foreach (var url in UrlsToDownload)
            {
                _urlsQuque.Enqueue(url);
            }

            _filesCount = _urlsQuque.Count;

            DownloadFileFromURL();
        }

        /// <summary>
        /// Download Single File from Queue
        /// </summary>
        private void DownloadFileFromURL()
        {
            if (_urlsQuque.Any())
            {
                _lastURL = _urlsQuque.Dequeue();
                _lastFileName = _lastURL.Substring(_lastURL.LastIndexOf("/") + 1, (_lastURL.Length - _lastURL.LastIndexOf("/") - 1));

                if (string.IsNullOrEmpty(_lastFileName)) return;

                simpleLabelItemInfo.Text = StaticKeys.DownloadingFile + " " + _lastFileName + "...";

                if (!Directory.Exists(DownloadLocation))
                {
                    try
                    {
                        Directory.CreateDirectory(DownloadLocation);
                    }
                    catch (Exception exception)
                    {
                        DownloadProcessResult.IsSucceed = false;
                        DownloadResult = FilesDownloadResult.FileAccessError;
                        DownloadProcessResult.Message = StaticKeys.CouldNotCreateDownloadLocation;
                        CloseDialog(DialogResult.No);
                    }
                }

                if (Directory.Exists(DownloadLocation))
                {
                    try
                    {
                        _downloadClient.DownloadFileAsync(new Uri(_lastURL), DownloadLocation + @"\" + _lastFileName);
                    }
                    catch (Exception exception)
                    {
                        DownloadProcessResult.IsSucceed = false;
                        DownloadResult = FilesDownloadResult.DownloadError;
                        DownloadProcessResult.Message = StaticKeys.FileDownloadError;
                        CloseDialog(DialogResult.No);
                    }
                }
            }
            else if (UrlsToDownload != null && UrlsToDownload.Any() && !_urlsQuque.Any())
            {
                simpleLabelItemInfo.Text = StaticKeys.DownloadCompleted;
                
                DownloadProcessResult.IsSucceed = true;
                DownloadResult = FilesDownloadResult.Sucess;
                DownloadProcessResult.Message = StaticKeys.DownloadCompleted;
                CloseDialog(DialogResult.Yes);
            }
        }

        /// <summary>
        /// Starts Download
        /// </summary>
        private void StartDownload()
        {
            if (!UiHelperClass.IsInternetOnline())
            {
                DownloadProcessResult.IsSucceed = false;
                DownloadResult = FilesDownloadResult.NoInternetConnection;
                DownloadProcessResult.Message = StaticKeys.NoInternetConnection;

                CloseDialog(DialogResult.No);
            }

            if (UrlsToDownload == null || !UrlsToDownload.Any())
            {
                DownloadProcessResult.IsSucceed = false;
                DownloadResult = FilesDownloadResult.NoFilesToDownload;
                DownloadProcessResult.Message = StaticKeys.NoFilesToDownload;

                CloseDialog(DialogResult.No);
            }

            new Thread(DownloadFiles).Start();
        }

        /// <summary>
        /// Closes Dialog with certain result
        /// </summary>
        /// <param name="dialogResult"></param>
        private void CloseDialog(DialogResult dialogResult)
        {
            BeginInvoke(
             (MethodInvoker)delegate()
             {
                 DialogResult = dialogResult;
                 Close();
             });
        }
        #endregion

        #region Handlers

        /// <summary>
        /// Handles the form loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormUpdate_Load(object sender, EventArgs e)
        {
            _downloadClient = new WebClient();
            _downloadClient.DownloadProgressChanged += client_DownloadProgressChanged;
            _downloadClient.DownloadFileCompleted += client_DownloadFileCompleted;
            DownloadProcessResult = new ProcessResult();

            simpleButtonCancel.Enabled = EnableCancel;

            StartDownload();
        }

        /// <summary>
        /// Handles window closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            _downloadClient.DownloadProgressChanged -= client_DownloadProgressChanged;
            _downloadClient.DownloadFileCompleted -= client_DownloadFileCompleted;
        }

        /// <summary>
        /// File Completed download
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                DownloadProcessResult.IsSucceed = false;
                DownloadResult = FilesDownloadResult.DownloadError;
                DownloadProcessResult.Message = StaticKeys.FileDownloadError;
                CloseDialog(DialogResult.No);
                return;
            }
            if (e.Cancelled)
            {
                DownloadProcessResult.IsSucceed = false;
                DownloadResult = FilesDownloadResult.Cancelled;
                DownloadProcessResult.Message = StaticKeys.DownloadCancelled;
                CloseDialog(DialogResult.OK);
                return;
            }

            _currentFileCount += 1;

            progressBarControlAllFiles.BeginInvoke(
            (MethodInvoker)delegate
            {
                progressBarControlAllFiles.EditValue = (double.Parse(_currentFileCount.ToString()) / double.Parse(_filesCount.ToString())) * 100;
                progressBarControlAllFiles.Refresh();
            });

            if (UpdateLogFile)
            {
                if (UiHelperClass.CheckFileExists(DownloadLogFilePath))
                {
                    File.AppendAllText(DownloadLogFilePath, _lastFileName + Environment.NewLine);
                }
            }
            
            DownloadFileFromURL();
        }

        /// <summary>
        /// Progress Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var bytesIn = double.Parse(e.BytesReceived.ToString());
            var totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            var percentage = bytesIn / totalBytes * 100;

            progressBarControlCurrentFile.BeginInvoke(
             (MethodInvoker)delegate()
             {
                 progressBarControlCurrentFile.EditValue = int.Parse(Math.Truncate(percentage).ToString());
                 progressBarControlCurrentFile.Refresh();
             });
        }

        /// <summary>
        /// Handles the cancel of the update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            _downloadClient.CancelAsync();
        }

        #endregion
    }
}