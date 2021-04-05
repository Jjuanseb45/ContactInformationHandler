using System;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class DeleteDeniedKindOfIdImportantException : Exception
    {
        public DeleteDeniedKindOfIdImportantException()
        {
        }

        public DeleteDeniedKindOfIdImportantException(string message) : base(message)
        {
        }
    }
}
