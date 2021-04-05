using ContactInfoHandler.Application.Dto.Base;
using ContactInfoHandler.Application.Dto.Persons.Employees;
using System;
using System.Collections.Generic;

namespace ContactInfoHandler.Application.Dto.Areas
{
    public class AreaOfWorkDto: DtoBase
    {
        public Guid AreaId { get; set; }
        public string AreaName { get; set; }

        public Guid ResponsableEmployeeId { get; set; }
        public IEnumerable<EmployeeDto> EmployeeLists { get; set; }
    }
}
