using System;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class DeniedDeleteNameNullException : Exception
    {
        public DeniedDeleteNameNullException()
        {
        }

        public DeniedDeleteNameNullException(string message) : base(message)
        {
        }
    }
}
