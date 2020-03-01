using System;
using System.Runtime.Serialization;

namespace Stackeer.Errors
{
    [Serializable]
    public class StackProcessorException : Exception
    {
        public Error? Error { get; }

        public StackProcessorException()
        {
        }

        public StackProcessorException(Error error) : base(error.Message, error.InnerException)
        {
            Error = error;
        }

        public StackProcessorException(string? message) : base(message)
        {
        }

        public StackProcessorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected StackProcessorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}