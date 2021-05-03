using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Persons.Employees.EmployeesHttpClient.EmployeesHttpClient
{
    class EmployeesHttpClient : HttpClientGenericBase<EmployeeDto>, IEmployeesHttpClient
    {
    }
    protected override string Controller { get => "/Employee"; }


    public async Task<IEnumerable<EmployeeDto>> GetAll() => await Get("GetEmployees").ConfigureAwait(false);
    public async Task<bool> DeleteOne(EmployeeDto employeeToDelete) => await Post("DeleteEmployee").ConfigureAwait(false);
    public async Task<bool> InsertOne(EmployeeDto employeeToInsert) => await Post("InsertEmployee").ConfigureAwait(false);
    public async Task<bool> UpdateOne(EmployeeDto employeeToUpdate) => await Post("UpdateEmployee").ConfigureAwait(false);
}
