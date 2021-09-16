using System;
using System.Runtime.Serialization;
using Mohammad.Exceptions;

namespace Mohammad.Projects.TariqMal.Business.Exceptions
{
    public abstract class TariqMalExceptionBase : ExceptionBase
    {
        protected TariqMalExceptionBase()
        {
        }

        protected TariqMalExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected TariqMalExceptionBase(string message) : base(message)
        {
        }

        protected TariqMalExceptionBase(string message, Exception inner) : base(message, inner)
        {
        }

        protected TariqMalExceptionBase(string message, string instruction) : base(message, instruction)
        {
        }

        protected TariqMalExceptionBase(string message, Exception inner, string instruction) : base(message, inner, instruction)
        {
        }
    }

    public sealed class TariqMalException : TariqMalExceptionBase
    {
        public TariqMalException()
        {
        }

        public TariqMalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public TariqMalException(string message) : base(message)
        {
        }

        public TariqMalException(string message, Exception inner) : base(message, inner)
        {
        }

        public TariqMalException(string message, string instruction) : base(message, instruction)
        {
        }

        public TariqMalException(string message, Exception inner, string instruction) : base(message, inner, instruction)
        {
        }
    }
}