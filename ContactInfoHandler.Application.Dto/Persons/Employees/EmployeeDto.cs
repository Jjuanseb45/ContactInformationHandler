using System;

namespace ContactInfoHandler.Application.Dto.Persons.Employees
{
    public class EmployeeDto:PersonDto
    {
        public Guid IdEmployee { get; set; }
        public Guid EmployeeCode { get; set; }
        public double Salary { get; set; }
        //public override string KindOfPerson => "Natural";
        //RELACION CON EL AREA
        public Guid AreaId { get; set; }

        //RELACION CON TIPO DE IDENTIFICACION
        public Guid KindOfIdentificationId { get; set; }
    }

}
