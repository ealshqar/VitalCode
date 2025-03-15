namespace Vital.Business.Shared.Shared
{
    public class CacheResult
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the is succeed flag.
        /// </summary>
        public bool IsSucceed { get; set; }

        #endregion
    }
}
