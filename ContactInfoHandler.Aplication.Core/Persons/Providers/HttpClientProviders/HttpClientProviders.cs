using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ContactInfoHandler.Application.Dto.Areas;
using ContactInfoHandler.Infrastructure.Transversal.Configurator;
using ContactInfoHandler.Infrastructure.Transversal.GenericClass;
using Microsoft.Extensions.Options;

namespace ContactInfoHandler.Application.Core.Persons.Providers.HttpClientProviders
{
    public class HttpClientProviders: HttpClientGenericBase<AreaOfWorkDto>, IHttpClientProviders
    {
        public HttpClientProviders( HttpClient cliente, IOptions<HttpClientSettings> settings): base(cliente, settings)
        {
        }

        public override string Controller
        {
            get => "/Area";
        }

        public async Task<IEnumerable<AreaOfWorkDto>> GetAll()
        {
            return await Get("GetAreasOfWork").ConfigureAwait(false);
        }
        public async Task<bool> Put(AreaOfWorkDto request)
        {
            return await Post(request, "GetAreasOfWork").ConfigureAwait(false);
        }
        
    }
}