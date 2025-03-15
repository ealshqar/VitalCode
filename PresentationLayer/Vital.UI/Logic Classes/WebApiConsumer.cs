using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web;
using Vital.Business.Shared.Shared;

namespace Vital.UI.Logic_Classes
{
    public static class WebApiConsumer
    {
        /// <summary>
        /// Post information to service
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="callback"></param>
        /// <param name="errorCallback"></param>
        public static void Post(string url, Dictionary<string, string> data, Action<HttpStatusCode> callback, Action<Exception> errorCallback)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(new Uri(url));

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                var postDataList = HttpUtility.ParseQueryString(String.Empty);

                foreach (var item in data)
                {
                    postDataList.Add(item.Key, item.Value);
                }

                var dataStr = postDataList.ToString();

                var dataBytes = new System.Text.UTF8Encoding().GetBytes(dataStr);
                request.ContentLength = dataBytes.Length;

                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(dataBytes, 0, dataBytes.Length);
                }

                request.BeginGetResponse((x) =>
                {
                    using (var response = (HttpWebResponse)request.EndGetResponse(x))
                    {
                        if (callback != null)
                            callback(response.StatusCode);
                    }

                }, null);
            }
            catch (Exception exception)
            {
                if (errorCallback != null)
                    errorCallback(exception);
            }

        }
    }
}
