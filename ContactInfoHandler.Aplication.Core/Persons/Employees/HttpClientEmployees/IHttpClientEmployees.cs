using System.Collections.Generic;
using System.Threading.Tasks;
using ContactInfoHandler.Application.Dto.Persons.Employees;

namespace ContactInfoHandler.Application.Core.Persons.Employees.HttpClientEmployees
{
    public interface IHttpClientEmployees
    {
        public Task<IEnumerable<EmployeeDto>> GetAll();
        public Task<bool> Post(EmployeeDto empleado);

    }
}