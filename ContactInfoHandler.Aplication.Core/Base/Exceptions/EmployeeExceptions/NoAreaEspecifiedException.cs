using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class AlreadyExistingEmployeeException : Exception
    {
        public AlreadyExistingEmployeeException()
        {
        }

        public AlreadyExistingEmployeeException(string message) : base(message)
        {
        }
    }
}
