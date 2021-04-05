using System;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class SignUpDateMissingException : Exception
    {
        public SignUpDateMissingException()
        {
        }

        public SignUpDateMissingException(string message) : base(message)
        {
        }
    }
}
