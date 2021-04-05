using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class DateOfBirthMissingException : Exception
    {
        public DateOfBirthMissingException()
        {
        }

        public DateOfBirthMissingException(string message) : base(message)
        {
        }
    }
}
