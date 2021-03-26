using ContactInfoHandler.Application.Dto.Persons.Employees;


using System.Threading.Tasks;

namespace ContactInfoHandler.Application.Core.Persons.Employees.Service
{
    public interface IEmployeeService
    {
        public Task<bool> InsertEmployee(EmployeeDto employee);
        public Task<bool> DeleteEmployee(EmployeeDto employee);
    }
}
