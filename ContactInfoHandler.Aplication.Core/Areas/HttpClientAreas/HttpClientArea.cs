using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ContactInfoHandler.Application.Dto.Areas;
using ContactInfoHandler.Infrastructure.Transversal.Configurator;
using ContactInfoHandler.Infrastructure.Transversal.GenericClass;
using Microsoft.Extensions.Options;

namespace ContactInfoHandler.Application.Core.Areas.HttpClientAreas
{
    public class HttpClientArea : HttpClientGenericBase<AreaOfWorkDto>, IHttpClientArea
    {
        public HttpClientArea(HttpClient cliente, IOptions<HttpClientSettings> settings) : base(cliente, settings)
        {
        }

        public override string Controller
        {
            get => "/AreaOfWork";
        }

        public async Task<IEnumerable<AreaOfWorkDto>> GetAll()
            => await Get("/GetAreas").ConfigureAwait(false);

        public async Task<bool> Post(AreaOfWorkDto areaOfWork)
            => await Post(areaOfWork, "/DeleteAreas").ConfigureAwait(false);
    }
}