using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class DeniedDeleteNameNullException : Exception
    {
        public DeniedDeleteNameNullException()
        {
        }

        public DeniedDeleteNameNullException(string message) : base(message)
        {
        }
    }
}
