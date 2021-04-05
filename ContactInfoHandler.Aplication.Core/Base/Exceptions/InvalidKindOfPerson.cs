using System;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class InvalidKindOfPerson : Exception
    {
        public InvalidKindOfPerson()
        {
        }

        public InvalidKindOfPerson(string message) : base(message)
        {
        }
    }
}
