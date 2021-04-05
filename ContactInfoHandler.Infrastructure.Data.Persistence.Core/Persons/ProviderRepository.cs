using ContactInfoHandler.Dominio.Core.Persons.Providers;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Persons
{
    internal class ProviderRepository: BaseRepository<ProviderEntity>, IProviderRepository
    {
        public ProviderRepository(IContextDb contexto) : base(contexto) { }
    }
}
