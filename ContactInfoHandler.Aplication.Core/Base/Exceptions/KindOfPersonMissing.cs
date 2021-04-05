using System;
using System.Collections.Generic;
using System.Text;

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
