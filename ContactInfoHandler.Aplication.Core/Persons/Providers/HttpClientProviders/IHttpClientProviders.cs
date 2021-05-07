using System.Collections.Generic;
using System.Threading.Tasks;
using ContactInfoHandler.Application.Dto.Areas;

namespace ContactInfoHandler.Application.Core.Persons.Providers.HttpClientProviders
{
    public interface IHttpClientProviders
    {
        public Task<IEnumerable<AreaOfWorkDto>> GetAll();
        public Task<bool> Put(AreaOfWorkDto request);
    }
}