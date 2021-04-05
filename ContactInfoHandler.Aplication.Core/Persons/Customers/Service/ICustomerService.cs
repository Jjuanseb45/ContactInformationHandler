using ContactInfoHandler.Application.Dto.Persons.Customers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactInfoHandler.Application.Core.Persons.Customers.Service
{
    public interface ICustomerService
    {
        public Task<bool> InsertCustomer(CustomerDto customer);
        public Task<bool> DeleteCustomer(CustomerDto customer);
        public Task<bool> UpdateCustomer(CustomerDto customerDto);
        public Task<IEnumerable<CustomerDto>> GetCustomers();
    }
}
