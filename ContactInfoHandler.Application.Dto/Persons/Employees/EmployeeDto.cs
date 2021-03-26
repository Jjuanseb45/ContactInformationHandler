using ContactInfoHandler.Dominio.Core.Areas;
using ContactInfoHandler.Dominio.Core.Identifications;
using ContactInfoHandler.Dominio.Core.Persons;
using ContactInfoHandler.Dominio.Core.Persons.Employees;
using System;

namespace ContactInfoHandler.Application.Dto.Persons.Employees
{
    public class EmployeeDto:PersonDto
    {
        public Guid IdEmployee { get; set; }
        public Guid EmployeeCode { get; set; }
        public override KindOfPerson KindOfPerson => KindOfPerson.Natural;
        public WorkPosition WorkPosition { get; set; }
        public double Salary { get; set; }

        //RELACION CON EL AREA
        public Guid AreaId { get; set; }
        public AreaOfWorkEntity AreaOfWork { get; set; }

        //RELACION CON TIPO DE IDENTIFICACION
        public Guid KindOfIdentificationId { get; set; }
        public KindOfIdentificationEntity KindOfIdentification { get; set; }
    }

}
