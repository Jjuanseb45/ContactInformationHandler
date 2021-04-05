using ContactInfoHandler.Application.Dto.Persons.Employees;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactInfoHandler.Application.Core.Persons.Employees.Service
{
    public interface IEmployeeService
    {
        public Task<bool> InsertEmployee(EmployeeDto employee);
        public Task<bool> DeleteEmployee(EmployeeDto employee);
        public Task<IEnumerable<EmployeeDto>> GetEmployees();
        public Task<bool> UpdateEmployee(EmployeeDto employeeDto);
    }
}
