using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class AlreadyExistingKindOfIDException : Exception
    {
        public AlreadyExistingKindOfIDException()
        {
        }

        public AlreadyExistingKindOfIDException(string message) : base(message)
        {
        }
    }
}
