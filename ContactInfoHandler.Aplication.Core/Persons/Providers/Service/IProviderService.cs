using ContactInfoHandler.Application.Dto.Persons.Providers;
using System;
using System.Threading.Tasks;

namespace ContactInfoHandler.Application.Core.Persons.Providers.Service
{
    public interface IProviderService
    {
        public Task<bool> InsertProvider(ProviderDto provider);
        public Task<bool> UpdateProvider(ProviderDto provider);
        public Task<bool> DeleteProvider(ProviderDto provider);
    }
}
