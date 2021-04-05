using AutoMapper;
using ContactInfoHandler.Application.Dto.Persons.Customers;
using ContactInfoHandler.Dominio.Core.Persons.Customers;

namespace ContactInfoHandler.Application.Core.Persons.Customers.Mapping
{
    class CustomerProfile:Profile
    {      
            public CustomerProfile()
            {
                CreateMap<CustomerEntity, CustomerDto>().ReverseMap();
            }        
    }
}
