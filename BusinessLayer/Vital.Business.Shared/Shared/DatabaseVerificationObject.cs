namespace Vital.Business.Shared.Shared
{
    public class DatabaseVerificationObject
    {
        /// <summary>
        /// Gets or sets the Version
        /// </summary>
        public string Version
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the flag that indicates if the application branch keys are compatible.
        /// </summary>
        public bool IsApplicationBranchCompatible
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the application branch
        /// </summary>
        public ApplicationBranches ApplicationBranch
        {
            get; set;
        }
    }
}
