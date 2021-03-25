using ContactInfoHandler.Dominio.Core.Base;
using ContactInfoHandler.Dominio.Core.Persons.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ContactInfoHandler.Dominio.Core.Areas
{
    public class AreaOfWorkEntity: BaseEntity
    {
        [Key]
        public Guid AreaId { get; set; }
        [NotMapped]
        public EmployeeEntity Reponsable { get; set; }
        public string AreaName { get; set; }
        public IEnumerable<EmployeeEntity> AreaEmployees { get; set; }
    }
}
