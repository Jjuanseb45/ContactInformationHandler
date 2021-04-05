using ContactInfoHandler.Dominio.Core.Persons.Customers;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Persons
{
    internal class CustomersRepository: BaseRepository<CustomerEntity>, ICustomersRepository
    {
        public CustomersRepository(IContextDb contexto) : base(contexto) { }
    }
}
