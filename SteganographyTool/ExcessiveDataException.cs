using System;
using System.Runtime.Serialization;

namespace SteganographyTool
{
    [Serializable]
    internal class ExcessiveDataException : Exception
    {
        public ExcessiveDataException()
        {
        }

        public ExcessiveDataException(string message) : base(message)
        {
        }

        public ExcessiveDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExcessiveDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}