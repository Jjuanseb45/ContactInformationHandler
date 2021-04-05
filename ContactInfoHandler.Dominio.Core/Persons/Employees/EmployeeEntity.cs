using ContactInfoHandler.Dominio.Core.Areas;
using ContactInfoHandler.Dominio.Core.Identifications;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactInfoHandler.Dominio.Core.Persons.Employees
{
    public class EmployeeEntity: BasePersonEntity
    {
        [Key]
        public Guid IdEmployee { get; set; }
        public Guid EmployeeCode { get; set; }
//        public override string KindOfPerson => "Natural";
        public double Salary { get; set; }
        public Guid AreaId { get; set; }


    //RELACION CON TIPO DE IDENTIFICACION
    public Guid KindOfIdentificationId { get; set; }
    }

}
