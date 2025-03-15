using System;
using Vital.Update.Shared;

namespace Vital.Update.Exceptions
{
    public class RestartEventNotHandledException : Exception
    {
        #region Constructors

        public RestartEventNotHandledException()
            : base(StaticKeys.ReatartEventHavNoHandler)
        {

        }

        #endregion
    }
}
