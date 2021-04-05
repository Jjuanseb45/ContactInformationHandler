using System;
using System.Collections.Generic;
using System.Text;

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
