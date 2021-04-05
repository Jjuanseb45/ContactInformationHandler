using System;

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
