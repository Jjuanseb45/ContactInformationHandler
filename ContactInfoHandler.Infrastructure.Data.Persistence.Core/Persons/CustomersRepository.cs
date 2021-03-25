using ContactInfoHandler.Dominio.Core.Persons.Customers;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Persons
{
    public class CustomersRepository: BaseRepository<CustomerEntity>, ICustomersRepository
    {
        public CustomersRepository(IContextDb contexto) : base(contexto) { }
    }
}
