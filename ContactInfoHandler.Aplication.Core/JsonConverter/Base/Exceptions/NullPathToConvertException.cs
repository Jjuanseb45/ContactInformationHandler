using System;

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
