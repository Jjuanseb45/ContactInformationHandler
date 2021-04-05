using System;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class NoExistingCustomerException : Exception
    {
        public NoExistingCustomerException()
        {
        }

        public NoExistingCustomerException(string message) : base(message)
        {
        }
    }
}
