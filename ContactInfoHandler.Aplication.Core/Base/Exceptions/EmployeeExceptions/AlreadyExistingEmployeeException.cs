using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class NoAreaEspecifiedException : Exception
    {
        public NoAreaEspecifiedException()
        {
        }

        public NoAreaEspecifiedException(string message) : base(message)
        {
        }
    }
}
