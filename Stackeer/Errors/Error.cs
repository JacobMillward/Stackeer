using System;

namespace Stackeer.Errors
{
    public class Error : IMessage
    {
        internal bool ShouldContinue { get; set; }

        public Exception? InnerException { get; }

        public string Message { get; } = "Error whilst processing message";

        public Error(string? msg = null, Exception? innerException = null) {
            if (msg != null)
            {
                Message = msg;
            }

            InnerException = innerException;
        }
    }
}