using System;

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
