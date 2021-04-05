using System;

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
