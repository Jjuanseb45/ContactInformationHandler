using ContactInfoHandler.Application.Dto.Base;
using ContactInfoHandler.Application.Dto.Persons.Customers;
using ContactInfoHandler.Application.Dto.Persons.Employees;
using ContactInfoHandler.Application.Dto.Persons.Providers;
using System;
using System.Collections.Generic;

namespace ContactInfoHandler.Application.Dto.Identifications
{
    public class KindOfIdentificationDto: DtoBase
    {
        public Guid KindOfIdentificationId { get; set; }
        public string IdentificationName { get; set; }
        public IEnumerable<ProviderDto> ProvidersList { get; set; }
        public IEnumerable<CustomerDto> CustomersList { get; set; }
        public IEnumerable<EmployeeDto> EmployeeEntities { get; set; }
    }
}
