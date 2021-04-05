using ContactInfoHandler.Dominio.Core.Base;
using ContactInfoHandler.Dominio.Core.Persons.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactInfoHandler.Dominio.Core.Areas
{
    public class AreaOfWorkEntity: BaseEntity
    {
        [Key]
        public Guid AreaId { get; set; }
        public string AreaName { get; set; }

        [Required]
        public Guid ResponsableEmployeeId { get; set; }
        public IEnumerable<EmployeeEntity> EmployeeLists { get; set; }

    }
}
