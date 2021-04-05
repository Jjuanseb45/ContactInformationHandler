using System;

namespace ContactInfoHandler.Application.Core.Base.Exceptions
{
    public class KindOfPersonMissing : Exception
    {
        public KindOfPersonMissing()
        {
        }

        public KindOfPersonMissing(string message) : base(message)
        {
        }
    }
}
