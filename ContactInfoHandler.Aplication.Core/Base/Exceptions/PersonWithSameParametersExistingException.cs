using System;
using System.Collections.Generic;
using System.Text;

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
