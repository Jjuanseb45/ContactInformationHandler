using System;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class PersonWithSameParametersExistingException : Exception
    {
        public PersonWithSameParametersExistingException()
        {
        }

        public PersonWithSameParametersExistingException(string message) : base(message)
        {
        }
    }
}
