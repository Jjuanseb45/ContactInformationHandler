using System;
using System.Collections.Generic;
using System.Text;

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
