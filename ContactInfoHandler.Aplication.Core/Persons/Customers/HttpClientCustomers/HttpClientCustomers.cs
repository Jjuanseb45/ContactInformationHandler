using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ContactInfoHandler.Application.Dto.Persons.Customers;
using ContactInfoHandler.Infrastructure.Transversal.Configurator;
using ContactInfoHandler.Infrastructure.Transversal.GenericClass;
using Microsoft.Extensions.Options;

namespace ContactInfoHandler.Application.Core.Persons.Customers.HttpClientCustomers
{
    public class HttpClientCustomers: HttpClientGenericBase<CustomerDto>, IHttpClientCustomers
    {

        public HttpClientCustomers( HttpClient cliente, IOptions<HttpClientSettings> settings): base(cliente, settings)
        {
        }
        
        public override string Controller
        {
            get => "/Customer";
        }

        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            return await Get("/GetCustomers").ConfigureAwait(false);
        }

        public async Task<bool> Put(CustomerDto customer)
        {
            return await Put(customer, "/DeleteCustomers");
        }
    }
}