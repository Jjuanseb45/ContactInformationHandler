using ContactInfoHandler.Dominio.Core.Persons.Employees;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Persons
{
    internal class EmployeeRepository: BaseRepository<EmployeeEntity>, IEmployeeRepository
    {
        public EmployeeRepository(IContextDb contexto): base(contexto) { }
    }
}
