using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class AreaHasEmployeesException : Exception
    {
        public AreaHasEmployeesException()
        {
        }

        public AreaHasEmployeesException(string message) : base(message)
        {
        }
    }
}
