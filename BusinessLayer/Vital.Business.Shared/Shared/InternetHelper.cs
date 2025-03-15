using System.Net;

namespace Vital.Business.Shared.Shared
{
    public static class InternetHelper
    {
        /// <summary>
        /// Check internet online connectivity
        /// </summary>
        /// <returns></returns>
        public static bool IsInternetOnline()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
