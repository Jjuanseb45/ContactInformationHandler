using System;

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
