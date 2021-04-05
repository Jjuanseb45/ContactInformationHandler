using System;

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
