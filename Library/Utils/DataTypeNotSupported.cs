using System;
using System.Runtime.Serialization;

namespace Utils
{
    [Serializable]
    public class DataTypeNotSupported : Exception
    {
        public DataTypeNotSupported() : base("Data Type Not Supported")
        {
        }

        public DataTypeNotSupported(string message) : base(message)
        {
        }

        public DataTypeNotSupported(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DataTypeNotSupported(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}