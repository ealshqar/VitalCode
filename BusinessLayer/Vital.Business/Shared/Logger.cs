using System;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared
{
    public static class Logger
    {
        public static ProcessResult LogError(Exception exception)
        {
            try
            {
                //ToDo: Add stuff for error logging.
                return new ProcessResult() {IsSucceed = true};
            }
            catch (Exception ex)
            {
                return new ProcessResult() {IsSucceed = false, Message = ex.Message};
                throw;
            }
        }
    }
}
