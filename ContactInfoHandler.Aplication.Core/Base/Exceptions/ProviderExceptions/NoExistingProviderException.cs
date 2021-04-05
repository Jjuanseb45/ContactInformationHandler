using System;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class ProviderAlreadyOnDatabaseException : Exception
    {
        public ProviderAlreadyOnDatabaseException()
        {
        }

        public ProviderAlreadyOnDatabaseException(string message) : base(message)
        {
        }
    }
}
