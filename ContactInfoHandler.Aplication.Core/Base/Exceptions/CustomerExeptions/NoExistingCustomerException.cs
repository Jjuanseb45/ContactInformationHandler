using System;
using System.Collections.Generic;
using System.Text;

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
