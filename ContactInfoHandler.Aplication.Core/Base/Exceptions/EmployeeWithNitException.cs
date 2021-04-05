using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class EmployeeWithNitException : Exception
    {
        public EmployeeWithNitException()
        {
        }

        public EmployeeWithNitException(string message) : base(message)
        {
        }
    }
}
