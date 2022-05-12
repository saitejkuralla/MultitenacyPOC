using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy.Exceptions
{
    public class IMMException : Exception
    {
        public IMMException()
        {

        }

        public IMMException(string message)
            : base(message)
        {

        }

        public IMMException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public IMMException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
