using System.Collections.Generic;
using System.Threading.Tasks;
using ContactInfoHandler.Application.Dto.Persons.Customers;

namespace ContactInfoHandler.Application.Core.Persons.Customers.HttpClientCustomers
{
    public interface IHttpClientCustomers
    {
        public Task<IEnumerable<CustomerDto>> GetAll();
        public Task<bool> Put(CustomerDto customer);
    }
}