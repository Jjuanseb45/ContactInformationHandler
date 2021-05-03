using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Persons.Customers.CustomersHttpClient
{
    class CustomersHttpClient: HttpClientGenericBase<CustomerDto>, ICustomersHttpClient
    {

        public CustomersHttpClient(HttpClient cliente, IOptions<HttpClient> settings): base(cliente, settings)
        {
        }
        protected override string Controller { get => "/Customer" ; }
        public async Task<IEnumerable<CustomerDto>> GetAll() => await Get("GetCustomers").ConfigureAwait(false);
        public async Task<bool> GetAll() => await Get("GetCustomers").ConfigureAwait(false);
        public async Task<bool> DeleteOne(CustomerDto customerToDelete) => await Post("DeleteCustomer").ConfigureAwait(false);
        public async Task<bool> InsertOne(CustomerDto customerToInsert) => await Post("InsertCustomer").ConfigureAwait(false);
        public async Task<bool> UpdateOne(CustomerDto customerToUpdate) => await Post("UpdateCustomer").ConfigureAwait(false);

    }
}
