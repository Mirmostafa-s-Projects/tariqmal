using System;
using System.Net;
using System.Runtime.Serialization;
using Mohammad.Web.Api.Exceptions;

namespace Mohammad.Projects.TariqMal.Api.Exceptions
{
    public abstract class AtmExceptionBase : ApiExceptionBase
    {
        protected AtmExceptionBase(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {
        }

        protected AtmExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public sealed class AtmException : AtmExceptionBase
    {
        public AtmException(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {
        }

        public AtmException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}