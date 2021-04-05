using System;

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
