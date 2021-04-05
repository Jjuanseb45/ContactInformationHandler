using System;

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
