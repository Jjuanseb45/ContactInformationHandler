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
        public override KindOfPerson KindOfPerson => KindOfPerson.Natural;
        public WorkPosition WorkPosition { get;  set; }
        public double Salary { get; set; }

        //RELACION CON EL AREA
        public Guid AreaId { get; set; }
        public AreaOfWorkEntity AreaOfWork { get; set; }

        //RELACION CON TIPO DE IDENTIFICACION
        public Guid KindOfIdentificationId { get; set; }
        public KindOfIdentificationEntity KindOfIdentification { get; set; }
    }
    public enum WorkPosition
    {
        CommonEmployee = 1,
        Administrator = 2,
        Manager = 3
    }
}
