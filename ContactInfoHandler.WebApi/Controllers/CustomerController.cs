using ContactInfoHandler.Application.Core.Persons.Customers.Service;
using ContactInfoHandler.Application.Dto.Persons.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactInfoHandler.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost(nameof(InsertCustomer))]
        public async Task<bool> InsertCustomer(CustomerDto customerRequest) =>
            await _customerService.InsertCustomer(customerRequest).ConfigureAwait(false);
        
        [HttpPost(nameof(DeleteCustomer))]
        public async Task<bool> DeleteCustomer(CustomerDto customerRequest) =>
            await _customerService.DeleteCustomer(customerRequest).ConfigureAwait(false);

        [HttpGet(nameof(GetCustomers))]
        public async Task<IEnumerable<CustomerDto>> GetCustomers() =>
            await _customerService.GetCustomers().ConfigureAwait(false);

        [HttpPost(nameof(UpdateCustomer))]
        public async Task<bool> UpdateCustomer(CustomerDto customerRequest) =>
            await _customerService.UpdateCustomer(customerRequest).ConfigureAwait(false);

    }
}
