using System.Resources;

namespace Vital.Common
{
    public class ResourceStringLoader
    {
        public static string LoadResourceString(string resource , string key)
        { 
            const string resourceFile = "~/CommonResources/CommonResources.resx";
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory;
            ResourceManager resourceManager = ResourceManager.CreateFileBasedResourceManager(resourceFile, filePath, null);
            string resourceValue = resourceManager.GetString(key);

            return resourceValue;
        }
    }
}
