using System;

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
