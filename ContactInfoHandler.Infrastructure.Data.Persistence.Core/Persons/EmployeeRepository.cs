using ContactInfoHandler.Dominio.Core.Persons.Employees;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Persons
{
    public class EmployeeRepository: BaseRepository<EmployeeEntity>, IEmployeeRepository
    {
        public EmployeeRepository(IContextDb contexto): base(contexto) { }
    }
}
