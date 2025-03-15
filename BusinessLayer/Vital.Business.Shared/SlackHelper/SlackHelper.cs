using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Slack.Webhooks;
using RestSharp;
using ServiceStack.Text;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.SlackHelper
{
    public static class SlackHelper
    {
        //***********************************************************************
        /*
         * VERY IMPORTANT:
         * During debugging make sure the following two things are set correctly, otherwise slack won't send events:
         * 1- Force the use of TLS1.2 (This is done in VitalInstanceController
         * 2- Make sure the debug folder contains the dll "RestSharp.dll"
         * 3- Make sure the build configuration is set to Debug>x86 [WE ARE NOT SURE THIS IS NEEDE ANYMORE SINCE PROD BUILD Configuration is set to Mixed Platforms]
         */
        //***********************************************************************
        #region Private Members

        private static SlackClient _slackClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        static SlackHelper()
        {
            CheckAndInitializeSlackClient();

            try
            {
                IRestResponse result = new RestResponse();

                result.ErrorMessage = TextExtensions.SerializeToString("test") + "DO NOT REMOVE THIS LINE SINCE WE NEED IT TO KEEP THE 'RestSharp' DLL REFERENCED TO MAKE SURE IT IS ADDED IN BIN DIRECTORY SINCE SLACK NEEDS IT THERE";
            }
            catch
            {
            }
        }

        #endregion

        #region Logic

        /// <summary>
        /// Check if the slack client is not intiailized and initialize it.
        /// </summary>
        private static void CheckAndInitializeSlackClient()
        {
            try
            {
                if (_slackClient == null)
                {
                    _slackClient = new SlackClient(ConfigurationManager.AppSettings[StaticKeys.SlackWebhookURLConfigKey]);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Send Vital event as a slack message
        /// </summary>
        /// <param name="vitalEvent"></param>
        /// <param name="userName"></param>
        /// <param name="techAndAppInfo"></param>
        /// <param name="parameters"></param>
        public static void SendSlackMessage(VitalEvent vitalEvent,string userName, Dictionary<string, object> techAndAppInfo, params object[] parameters)
        {
            try
            {
                if (InternetHelper.IsInternetOnline())
                {
                    var sendEventsToSlack = ConfigurationManager.AppSettings[StaticKeys.SendEventsToSlackConfigKey].ToBoolean();

                    if (sendEventsToSlack)
                    {
                        var eventDescriptor = EnumNameResolver.GetEnumNameOrDescription(vitalEvent);
                        var eventDescriptorItems = eventDescriptor.Split(',');
                        var eventMessage = eventDescriptorItems[1];
                        var formattedMessage = parameters== null || parameters.Length == 0? eventMessage : String.Format(eventMessage, parameters);

                        formattedMessage = userName + " - " + formattedMessage;

                        SendSlackMessage(userName, techAndAppInfo, formattedMessage);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Send slack message
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="techAndAppInfo"></param>
        /// <param name="message"></param>
        private static void SendSlackMessage(string userName, Dictionary<string, object> techAndAppInfo, string message)
        {
            try
            {
                Task.Factory.StartNew(() => SendSlackMessagePost(userName,techAndAppInfo, message));
            }
            catch
            {
            }
        }

        /// <summary>
        /// Send slack message
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="techAndAppInfo"></param>
        /// <param name="message"></param>
        private static ProcessResult SendSlackMessagePost(string userName,Dictionary<string, object> techAndAppInfo, string message)
        {
            var result = new ProcessResult();

            try
            {
                CheckAndInitializeSlackClient();

                //In slack messages, the Channel and the UserName are both optional because there default values are set in Slack Webhook integration
                //screen, because of this, below we have two keys for using custom channel or user names and also we have keys to indicate if we want
                //to use those custom names.
                var useSlackUserName = ConfigurationManager.AppSettings[StaticKeys.UseSlackUserNameConfigKey].ToBoolean();
                var useSlackChannel = ConfigurationManager.AppSettings[StaticKeys.UseSlackChannelConfigKey].ToBoolean();

                var slackMessage = new SlackMessage { Text = message };

                if (useSlackChannel)
                {
                    slackMessage.Channel = ConfigurationManager.AppSettings[StaticKeys.SlackChannelConfigKey];
                }

                if (useSlackUserName)
                {
                    //slackMessage.Username = ConfigurationManager.AppSettings[StaticKeys.SlackUserNameConfigKey];
                    slackMessage.Username = userName;
                }

                if (techAndAppInfo != null)
                {
                    var slackAttachment = new SlackAttachment
                    {
                        //Fallback = "Tech Info",
                        //Text = "New open task *[Urgent]*: <http://url_to_task|Test out Slack message attachments>",
                        Color = "good",
                        Fields = new List<SlackField>()
                    };

                    foreach (var keyValuePair in techAndAppInfo)
                    {
                        slackAttachment.Fields.Add(new SlackField
                        {
                            Title = keyValuePair.Key,
                            Value = keyValuePair.Value.ToString(),
                            Short = true
                        });
                    }
                    
                    slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };
                }

                //IMPORTANT: IF SLACK EVENTS ARE NOT WOKRING THEN MAKE SURE TO CHECK TOP OF THIS CLASS FOR IMPORTANT NOTES
                result.IsSucceed = _slackClient.Post(slackMessage);
            }
            catch (Exception exception)
            {
                result.IsSucceed = false;
                result.Message = exception.Message;
            }

            return result;
        }

        #endregion
    }
}
