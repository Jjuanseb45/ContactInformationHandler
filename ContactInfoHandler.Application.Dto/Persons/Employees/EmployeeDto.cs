using ContactInfoHandler.Application.Dto.Areas;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Dto.Persons.Employees
{
    public class EmployeeDto:PersonDto
    {
        public override KindOfPerson KindOfPerson => KindOfPerson.Natural;
        public Guid EmployeeCode { get; set; }
        public WorkPosition WorkPosition { get; set; }
        public double Salary { get; set; }

        //RELACION CON EL AREA
        public Guid AreaId { get; set; }
        public AreaOfWorkDto AreaOfWork { get; set; }
    }
    public enum WorkPosition
    {
        CommonEmployee = 1,
        Administrator = 2,
        Manager = 3
    }
}
