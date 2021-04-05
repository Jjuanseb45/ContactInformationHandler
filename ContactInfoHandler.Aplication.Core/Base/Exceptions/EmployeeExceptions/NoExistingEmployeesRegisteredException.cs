using System;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class NoExistingEmployeesRegisteredException : Exception
    {
        public NoExistingEmployeesRegisteredException()
        {
        }

        public NoExistingEmployeesRegisteredException(string message) : base(message)
        {
        }
    }
}
