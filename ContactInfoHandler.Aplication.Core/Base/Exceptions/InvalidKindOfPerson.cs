using System;
using System.Collections.Generic;
using System.Text;

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
