using System;

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
