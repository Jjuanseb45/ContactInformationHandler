using System;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class NoAreaHandlerEspecifiedException : Exception
    {
        public NoAreaHandlerEspecifiedException()
        {
        }

        public NoAreaHandlerEspecifiedException(string message) : base(message)
        {
        }
    }
}
