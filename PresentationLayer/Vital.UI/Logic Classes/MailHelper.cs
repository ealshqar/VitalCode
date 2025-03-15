using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web.Caching;
using System.Windows.Forms;
using DevExpress.Office.Utils;
using DevExpress.XtraGauges.Core.Resources;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Export;
using DevExpress.XtraRichEdit.Services;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.AppInfos;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.Update.Managers;

namespace Vital.UI.Logic_Classes
{
    /// <summary>
    /// This class contains the needed methods to send an email.
    /// </summary>
    public class MailHelper
    {
        #region Public Methods

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="targetEmail">The target email.</param>
        /// <param name="name">The name of the sender.</param>
        /// <param name="phone"></param>
        /// <param name="subject">The subject.</param>
        /// <param name="richEditeControlMail">The rich edit control.</param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static ProcessResult SendMail(string targetEmail,string name, string email, string phone, string subject, RichEditControl richEditeControlMail)
        {
            try
            {
                var mailMessage = new MailMessage(UiHelperClass.GetVitalEmail().FeedbackSenderEmail, targetEmail)
                {
                    Subject = !string.IsNullOrEmpty(subject) ? subject : StaticKeys.MailClientNoSubject
                };

                //WE ARE SENDING A COPY TO OURSELF SINCE SEC EMAIL ACCOUNTS DOESN'T PUT THE EMAIL IN SENT FOLDER SO WE NEED A COPY
                //---------------------------------------------
                mailMessage.To.Add(UiHelperClass.GetVitalEmail().FeedbackSenderEmail);
                //---------------------------------------------

                var exporter = new RichEditMailMessageExporter(richEditeControlMail, mailMessage);

                exporter.Export(name, true);

                var mailSender = new SmtpClient
                {
                    Host = StaticKeys.SMTPAddress,
                    Credentials = new System.Net.NetworkCredential(UiHelperClass.GetVitalEmail().FeedbackSenderEmail,
                                                                   UiHelperClass.GetVitalEmail().FeedbackSenderPass),
                    EnableSsl = true,
                    Port = StaticKeys.PortNumber,
                };

                mailSender.Send(mailMessage);

                return new ProcessResult
                {
                    IsSucceed = true
                };
            }
            catch (Exception exc)
            {
                 return new ProcessResult
                           {
                             IsSucceed = false,
                             Message = exc.Message
                           };
            }
        }

        /// <summary>
        /// Adds some info about system and user to mail message
        /// </summary>
        /// <param name="mailMessage"></param>
        private static void AddAppInfoToMailMessage(MailMessage mailMessage)
        {
            mailMessage.Body = UiHelperClass.GetTechAndVitalAppInfo() + "\n\n" + mailMessage.Body;
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="targetEmail">The target email.</param>
        /// <param name="name">The name of the sender.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The message body</param>
        /// <returns></returns>
        public static ProcessResult SendExcecptionMailIfOnline(string targetEmail, string name, string subject, string body)
        {
            UiHelperClass.ShowWaitingPanel("Sending Error Details ...");

            if (UiHelperClass.IsInternetOnline())
            {
                try
                {
                    var mailMessage = new MailMessage();

                    mailMessage.From = new MailAddress(UiHelperClass.GetVitalEmail().FeedbackSenderEmail, name);
                    mailMessage.To.Add(targetEmail);
                    //WE ARE SENDING A COPY TO OURSELF SINCE SEC EMAIL ACCOUNTS DOESN'T PUT THE EMAIL IN SENT FOLDER SO WE NEED A COPY
                    //---------------------------------------------
                    mailMessage.To.Add(UiHelperClass.GetVitalEmail().FeedbackSenderEmail);
                    //---------------------------------------------
                    mailMessage.Subject = !string.IsNullOrEmpty(subject) ? subject : StaticKeys.MailClientNoSubject;
                    mailMessage.Body = !string.IsNullOrEmpty(body) ? body : StaticKeys.MailClientNoMessage;
                    
                    AddAppInfoToMailMessage(mailMessage);

                    UiHelperClass.HideSplash();
                    
                    //Get a screenshot of the system at the time of the error ... notice that we hide splash and show it again to prevent showing it in image
                    var screenShotStream = UiHelperClass.GetSystemScreenShot();
                    
                    UiHelperClass.ShowWaitingPanel("Sending Error Details ...");

                    if (screenShotStream != null)
                    {
                        //Create image linked resource
                        var screenshotResource = new LinkedResource(screenShotStream)
                        {
                            ContentId = StaticKeys.ExceptionImageContentId,
                            ContentType = new ContentType(MediaTypeNames.Image.Jpeg)
                        };

                        //Replace new lines with break tag to allow showing info correctly within email HTML
                        mailMessage.Body = mailMessage.Body.Replace(Environment.NewLine, "<br />").Replace("\r", "<br />").Replace("\n", "<br />");

                        var imageAlternateView = AlternateView.CreateAlternateViewFromString(string.Format(StaticKeys.ExceptionEmailHTMLStructure, mailMessage.Body, StaticKeys.ExceptionImageContentId), null, StaticKeys.ExceptionEmailHTMLMediaType);
                        imageAlternateView.LinkedResources.Add(screenshotResource);
                        mailMessage.AlternateViews.Add(imageAlternateView);
                    }
                    
                    var mailSender = new SmtpClient
                    {
                        Host = StaticKeys.SMTPAddress,
                        Credentials = new System.Net.NetworkCredential(UiHelperClass.GetVitalEmail().FeedbackSenderEmail,
                                                                       UiHelperClass.GetVitalEmail().FeedbackSenderPass),
                        EnableSsl = true,
                        Port = StaticKeys.PortNumber,
                    };

                    mailSender.Send(mailMessage);

                    return new ProcessResult
                    {
                        IsSucceed = true
                    };
                }
                catch (Exception exc)
                {
                    return new ProcessResult
                    {
                        IsSucceed = false,
                        Message = exc.Message
                    };
                }

                UiHelperClass.HideSplash();

                return ProcessResult.Succeed;
            }

            UiHelperClass.HideSplash();

            return ProcessResult.Failed;
        }

        /// <summary>
        /// Sends shipping report as an HTML body of an email but without images.
        /// </summary>
        /// <param name="report"></param>
        /// <param name="targetEmail"></param>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static ProcessResult SendShippingReportInHTML(XtraReport report, string targetEmail, string name, string subject)
        {
            return SendReportInHTML(report, 
                                    UiHelperClass.GetVitalEmail().ShippingSenderEmail,
                                    UiHelperClass.GetVitalEmail().ShippingSenderPass,
                                    StaticKeys.SMTPAddress,
                                    StaticKeys.PortNumber,
                                    false,
                                    true,
                                    targetEmail, 
                                    name, 
                                    subject);            
        }

        /// <summary>
        /// Sends feedback report as an HTML body of an email but without images.
        /// </summary>
        /// <param name="report"></param>
        /// <param name="targetEmail"></param>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static ProcessResult SendFeedbackReportInHTML(XtraReport report, string name, string subject)
        {
            //IMPORTANT: THIS DOESN'T WORK WITH IMAGES AND IS NOT IN USE
            return SendReportInHTML(report,
                                    UiHelperClass.GetVitalEmail().FeedbackSenderEmail,
                                    UiHelperClass.GetVitalEmail().FeedbackSenderPass,
                                    StaticKeys.SMTPAddress,
                                    StaticKeys.PortNumber,
                                    false,
                                    true,
                                    UiHelperClass.GetVitalEmail().FeedbackTargetEmail,
                                    name,
                                    subject);
        }

        /// <summary>
        /// Sends a report as an HTML body of an email but without images.
        /// </summary>
        /// <param name="report"></param>
        /// <param name="senderEmail"></param>
        /// <param name="port"></param>
        /// <param name="useDefaultCredentials"></param>
        /// <param name="enableSsl"></param>
        /// <param name="targetEmail"></param>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="senderPassword"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        public static ProcessResult SendReportInHTML(XtraReport report, 
                                                    string senderEmail,
                                                    string senderPassword,
                                                    string host,
                                                    int port,
                                                    bool useDefaultCredentials,
                                                    bool enableSsl,
                                                    string targetEmail, 
                                                    string name, 
                                                    string subject)
        {
            try
            {
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(senderEmail, name);
                mailMessage.To.Add(targetEmail);
                
                //WE ARE SENDING A COPY TO OURSELF SINCE SEC EMAIL ACCOUNTS DOESN'T PUT THE EMAIL IN SENT FOLDER SO WE NEED A COPY
                //---------------------------------------------
                mailMessage.To.Add(senderEmail);
                //---------------------------------------------

                mailMessage.Subject = !string.IsNullOrEmpty(subject) ? subject : StaticKeys.MailClientNoSubject;

                //Export report to HTML
                var memoryStream = new MemoryStream();
                report.ExportToHtml(memoryStream);

                var htmlMessage = Encoding.UTF8.GetString(memoryStream.ToArray());

                // Create the HTML view
                var htmlView = AlternateView.CreateAlternateViewFromString(htmlMessage,
                                                                                 Encoding.UTF8,
                                                                                 MediaTypeNames.Text.Html);
                // Create a plain text message for client that don't support HTML
                var plainView = AlternateView.CreateAlternateViewFromString(GetTextString(report),
                                                                                 Encoding.UTF8,
                                                                                 MediaTypeNames.Text.Plain);

                mailMessage.AlternateViews.Add(plainView);
                mailMessage.AlternateViews.Add(htmlView);
                mailMessage.IsBodyHtml = true;

                var mailSender = new SmtpClient()
                {
                    Host = StaticKeys.SMTPAddress,
                    EnableSsl = true,
                    Credentials = new System.Net.NetworkCredential(senderEmail, senderPassword),
                    Port = StaticKeys.PortNumber,
                };

                //THESE SETTINGS ARE DISABLED WHEN USING SEC EMAIL ACCOUNT, ENABLE THEM IF YOU WANT TO USE DIFFERENT ACCOUNT
                ////---------------------------------------------
                //mailSender.UseDefaultCredentials = useDefaultCredentials;
                //mailSender.DeliveryMethod = SmtpDeliveryMethod.Network;
                //mailSender.EnableSsl = enableSsl;
                ////---------------------------------------------

                mailSender.Send(mailMessage);

                return new ProcessResult
                {
                    IsSucceed = true
                };
            }
            catch (Exception exc)
            {
                return new ProcessResult
                {
                    IsSucceed = false,
                    Message = exc.Message
                };
            }
        }

        /// <summary>
        /// Export the current report as text
        /// </summary>
        private static string GetTextString(XtraReport report)
        {
            using (var MS = new MemoryStream())
            {
                report.ExportToText(MS);
                byte[] B = MS.ToArray();
                MS.Flush();
                return Encoding.UTF8.GetString(B);
            }
        }

        #endregion

        #region Private Classes

        /// <summary>
        /// This class represents the exporter of an email.
        /// </summary>
        private sealed class RichEditMailMessageExporter : IUriProvider
        {
            #region Private Variables

            readonly RichEditControl _control;
            readonly MailMessage _message;
            List<AttachementInfo> _attachments;
            int _imageId;

            #endregion

            #region Constructors

            /// <summary>
            /// The Constructor.
            /// </summary>
            /// <param name="control">The control.</param>
            /// <param name="message">The message.</param>
            public RichEditMailMessageExporter(RichEditControl control, MailMessage message)
            {
               Check.Argument.IsNotNull(control,"control");
               Check.Argument.IsNotNull(message, "message");
               _control = control;
               _message = message;

            }

            #endregion

            #region Public Methods

            /// <summary>
            /// Export.
            /// </summary>
            public void Export(string senderName, bool includeAppInfo)
            {
                _attachments = new List<AttachementInfo>();

                var htmlView = CreateHtmlView(senderName, includeAppInfo);

                _message.AlternateViews.Add(htmlView);
                _message.IsBodyHtml = true;
            }

            /// <summary>
            /// Creates the html view.
            /// </summary>
            /// <returns></returns>
            public AlternateView CreateHtmlView(string senderName, bool includeAppInfo)
            {
                _control.BeforeExport += OnBeforeExport;

                var htmlBody = _control.Document.GetHtmlText(_control.Document.Range, this);

                if (includeAppInfo)
                {
                    htmlBody = string.Format("{0}\n\n{1} {2}:\n\n{3}", UiHelperClass.GetTechAndVitalAppInfo(), StaticKeys.MailClientFirstLine, senderName, htmlBody);
                }
                else
                {
                    htmlBody = string.Format("{0} {1}:\n\n{2}", StaticKeys.MailClientFirstLine, senderName, htmlBody);
                }

                var view = AlternateView.CreateAlternateViewFromString(htmlBody, Encoding.UTF8, MediaTypeNames.Text.Html);

                _control.BeforeExport -= OnBeforeExport;

                foreach (var resource in _attachments.Select(info => new LinkedResource(info.Stream, info.MimeType)
                {
                    ContentId = info.ContentId
                }))
                {
                    view.LinkedResources.Add(resource);
                }

                return view;
            }

            /// <summary>
            /// Handles the before export.
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">The event arguments.</param>
            public static void OnBeforeExport(object sender, BeforeExportEventArgs e)
            {
                var options = e.Options as HtmlDocumentExporterOptions;

                if (options != null)
                {
                    options.Encoding = Encoding.UTF8;
                }
            }

            #endregion

            #region IUriProvider Members

            /// <summary>
            /// Creates an image URI.
            /// </summary>
            /// <param name="rootUri">The root URI.</param>
            /// <param name="image">The image.</param>
            /// <param name="relativeUri">The relative URI.</param>
            /// <returns></returns>
            public string CreateImageUri(string rootUri, OfficeImage image, string relativeUri)
            {
                var imageName = String.Format("image{0}", _imageId);

                _imageId++;

                var imageFormat = GetActualImageFormat(image.RawFormat);
                Stream stream = new MemoryStream(image.GetImageBytes(imageFormat));
                var mediaContentType = OfficeImage.GetContentType(imageFormat);
                var info = new AttachementInfo(stream, mediaContentType, imageName);

                _attachments.Add(info);

                return "cid:" + imageName;
            }

            /// <summary>
            /// Gets the actual image format.
            /// </summary>
            /// <param name="officeImageFormat"></param>
            /// <returns></returns>
            private static OfficeImageFormat GetActualImageFormat(OfficeImageFormat officeImageFormat)
            {
                if (officeImageFormat == OfficeImageFormat.Exif || officeImageFormat == OfficeImageFormat.MemoryBmp)
                    return OfficeImageFormat.Png;

                return officeImageFormat;
            }

            /// <summary>
            /// Creates a CSS URI.
            /// </summary>
            /// <param name="rootUri">The root URI.</param>
            /// <param name="styleText">The style text.</param>
            /// <param name="relativeUri">The relative URI.</param>
            /// <returns></returns>
            public string CreateCssUri(string rootUri, string styleText, string relativeUri)
            {
                return String.Empty;
            }

            #endregion
        }

        /// <summary>
        /// This class represents an email attachment.
        /// </summary>
        private sealed class AttachementInfo
        {
            #region Private Variables

            readonly Stream _stream;
            readonly string _mimeType;
            readonly string _contentId;

            #endregion

            #region Constructors

            /// <summary>
            /// The Constructor.
            /// </summary>
            /// <param name="stream"></param>
            /// <param name="mimeType"></param>
            /// <param name="contentId"></param>
            public AttachementInfo(Stream stream, string mimeType, string contentId)
            {
                _stream = stream;
                _mimeType = mimeType;
                _contentId = contentId;
            }

            #endregion

            #region Public properties

            /// <summary>
            /// Gets the stream.
            /// </summary>
            public Stream Stream
            {
                get { return _stream; }
            }

            /// <summary>
            /// Gets the mime type.
            /// </summary>
            public string MimeType
            {
                get { return _mimeType; }
            }

            /// <summary>
            /// Gets the content id.
            /// </summary>
            public string ContentId
            {
                get { return _contentId; }
            }

            #endregion
        }

        #endregion
    }
}
