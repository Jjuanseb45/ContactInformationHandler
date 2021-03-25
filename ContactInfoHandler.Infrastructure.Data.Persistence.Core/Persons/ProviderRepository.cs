using ContactInfoHandler.Dominio.Core.Persons.Providers;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Persons
{
    public class ProviderRepository: BaseRepository<ProviderEntity>, IProviderRepository
    {
        public ProviderRepository(IContextDb contexto) : base(contexto) { }
    }
}
