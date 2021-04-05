using System;
using System.Collections.Generic;
using System.Text;

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
