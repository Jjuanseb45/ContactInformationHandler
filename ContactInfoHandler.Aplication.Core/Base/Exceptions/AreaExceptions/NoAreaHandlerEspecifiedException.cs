using System;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class AlreadyExistingAreaException : Exception
    {
        public AlreadyExistingAreaException()
        {
        }

        public AlreadyExistingAreaException(string message) : base(message)
        {
        }
    }
}
