using ContactInfoHandler.Application.Core.Persons.Employees.Service;
using ContactInfoHandler.Application.Dto.Persons.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactInfoHandler.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeSvc;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeSvc)
        {
            _logger = logger;
            _employeeSvc = employeeSvc;
        }

        [HttpPost(nameof(InsertEmployee))]
        public async Task<bool> InsertEmployee(EmployeeDto employeeRequest) =>
            await _employeeSvc.InsertEmployee(employeeRequest).ConfigureAwait(false);
        
        [HttpPost(nameof(DeleteEmployee))]
        public async Task<bool> DeleteEmployee(EmployeeDto employeeRequest) =>
            await _employeeSvc.DeleteEmployee(employeeRequest).ConfigureAwait(false);

        [HttpGet(nameof(GetEmployees))]
        public async Task<IEnumerable<EmployeeDto>> GetEmployees() =>
            await _employeeSvc.GetEmployees().ConfigureAwait(false);

        [HttpPost(nameof(UpdateEmployee))]
        public async Task<bool> UpdateEmployee(EmployeeDto employeeRequest) =>
            await _employeeSvc.UpdateEmployee(employeeRequest).ConfigureAwait(false);
    }
}
