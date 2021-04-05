using System;

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
