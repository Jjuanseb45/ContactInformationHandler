using ContactInfoHandler.Dominio.Core.Persons.Employees;
using System;
using System.Collections.Generic;

namespace ContactInfoHandler.Application.Dto.Areas
{
    public class AreaOfWorkDto
    {
        public Guid AreaId { get; set; }
        public EmployeeEntity Reponsable { get; set; }
        public string AreaName { get; set; }
        public IEnumerable<EmployeeEntity> AreaEmployees { get; set; }

        //RELACION CON EL EMPLEADO REPONSABLE
        public Guid ResponsableEmployeeId { get; set; }
    }
}
