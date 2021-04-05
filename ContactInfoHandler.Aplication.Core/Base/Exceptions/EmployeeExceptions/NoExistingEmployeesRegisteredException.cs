using System;
using System.Collections.Generic;
using System.Text;

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
