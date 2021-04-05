using ContactInfoHandler.Dominio.Core.Base;
using ContactInfoHandler.Dominio.Core.Persons.Customers;
using ContactInfoHandler.Dominio.Core.Persons.Employees;
using ContactInfoHandler.Dominio.Core.Persons.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactInfoHandler.Dominio.Core.Identifications
{
    public class KindOfIdentificationEntity: BaseEntity
    {
        [Key]
        public Guid KindOfIdentificationId { get; set; } 
        public string IdentificationName{ get; set; }
        public IEnumerable<ProviderEntity> ProvidersList { get; set; }
        public IEnumerable<CustomerEntity> CustomersList { get; set; }
        public IEnumerable<EmployeeEntity> EmployeeEntities { get; set; }
    }
}
