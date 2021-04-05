using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class noExistingKindOfIdException : Exception
    {
        public noExistingKindOfIdException()
        {
        }

        public noExistingKindOfIdException(string message) : base(message)
        {
        }
    }
}
