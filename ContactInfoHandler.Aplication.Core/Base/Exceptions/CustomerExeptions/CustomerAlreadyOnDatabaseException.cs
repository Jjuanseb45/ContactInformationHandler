using System;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class CustomerAlreadyOnDatabaseException : Exception
    {
        public CustomerAlreadyOnDatabaseException()
        {
        }

        public CustomerAlreadyOnDatabaseException(string message) : base(message)
        {
        }
    }
}
