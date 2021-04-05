using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class JuridicEmployeeException : Exception
    {
        public JuridicEmployeeException()
        {
        }

        public JuridicEmployeeException(string message) : base(message)
        {
        }
    }
}
