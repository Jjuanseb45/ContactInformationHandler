using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.JsonConverter.Base.Exceptions
{
    public class NullPathToConvertException: Exception
    {        
            public NullPathToConvertException()
            {
            }

            public NullPathToConvertException(string message) : base(message)
            {
            }        
    }
}
