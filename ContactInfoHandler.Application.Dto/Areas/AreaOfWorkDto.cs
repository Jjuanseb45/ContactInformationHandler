using ContactInfoHandler.Application.Dto.Persons.Employees;
using ContactInfoHandler.Dominio.Core.Persons.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Dto.Areas
{
    public class AreaOfWorkDto
    {
        public Guid AreaId { get; set; }
        public EmployeeEntity Reponsable { get; set; }
        public string AreaName { get; set; }
        public ICollection<EmployeeEntity> AreaEmployees { get; set; }
    }
}
