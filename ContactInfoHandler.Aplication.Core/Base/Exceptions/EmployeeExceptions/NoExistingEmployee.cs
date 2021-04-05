using System;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class NoExistingEmployee : Exception
    {
        public NoExistingEmployee()
        {
        }

        public NoExistingEmployee(string message) : base(message)
        {
        }
    }
}
