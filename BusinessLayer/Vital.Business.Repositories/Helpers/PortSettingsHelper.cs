namespace Vital.Business.Repositories.Helpers
{
    internal class PortSettingsHelper
    {
        public int PortNumber { get; set; }

        public int BaudRate { get; set; }

        public int DataBits { get; set; }

        public bool Dtr { get; set; }

        public bool Rts { get; set; }
    }
}
