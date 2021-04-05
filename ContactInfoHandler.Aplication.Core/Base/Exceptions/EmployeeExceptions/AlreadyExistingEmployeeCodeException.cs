using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class AlreadyExistingEmployeeCodeException : Exception
    {
        public AlreadyExistingEmployeeCodeException()
        {
        }

        public AlreadyExistingEmployeeCodeException(string message) : base(message)
        {
        }
    }
}
