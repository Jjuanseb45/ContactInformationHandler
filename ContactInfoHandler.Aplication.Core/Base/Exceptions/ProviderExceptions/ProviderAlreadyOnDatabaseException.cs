using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class NoExistingProviderException : Exception
    {
        public NoExistingProviderException()
        {
        }

        public NoExistingProviderException(string message) : base(message)
        {
        }
    }
}
