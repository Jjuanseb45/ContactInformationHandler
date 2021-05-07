using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ContactInfoHandler.Application.Dto.Persons.Employees;
using ContactInfoHandler.Infrastructure.Transversal.Configurator;
using ContactInfoHandler.Infrastructure.Transversal.GenericClass;
using Microsoft.Extensions.Options;

namespace ContactInfoHandler.Application.Core.Persons.Employees.HttpClientEmployees
{
    public class HttpClientEmployees : HttpClientGenericBase<EmployeeDto>, IHttpClientEmployees
    {
        public HttpClientEmployees(HttpClient cliente, IOptions<HttpClientSettings> settings) : base(cliente, settings)
        {
        }

        public override string Controller
        {
            get => "/Employee";
        }

        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            return await Get("GetEmployees").ConfigureAwait(false);
        }

        public async Task<bool> Post(EmployeeDto empleado)
        {
            return await Post(empleado, "DeleteEmployee").ConfigureAwait(false);
        }
    }
}