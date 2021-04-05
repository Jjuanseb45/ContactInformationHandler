using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class NoEspecifiedEmployeeCodeException : Exception
    {
        public NoEspecifiedEmployeeCodeException()
        {
        }

        public NoEspecifiedEmployeeCodeException(string message) : base(message)
        {
        }
    }
}
