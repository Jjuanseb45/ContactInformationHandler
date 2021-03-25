using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Dto.Persons.Providers
{
    public class ProviderDto:PersonDto
    {
        public string CompanyName { get; set; }
        public long ContactNumber { get; set; }

    }
}
