using System;
using System.Collections.Generic;
using System.Text;

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
