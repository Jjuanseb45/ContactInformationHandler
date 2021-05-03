using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Areas.AreasHttpClient
{
    class AreaClienteHttp : HttpClientGenericBase<AreaOfWorkDto>, IAreaHttpClient
    {
        public AreaClienteHttp(HttpClient client, IOptions<HttpClient> settings) : base(client, settings)
        {
        }
        protected override string Controller { get => "/AreaOfWork"; }
        public async Task<IEnumerable<AreaOfWorkDto>> GetAll() => await Get("GetAreasOfWork").ConfigureAwait(false);
        public async Task<bool> DeleteOne(AreaOfWorkDto areaToDelete) => await Post("DeleteAreaOfWork").ConfigureAwait(false);
        public async Task<bool> InsertOne(AreaOfWorkDto areaToInsert) => await Post("InsertAreaOfWork").ConfigureAwait(false);
        public async Task<bool> UpdateOne(AreaOfWorkDto areaToUpdate) => await Post("UpdateAreaOfWork").ConfigureAwait(false);
    }
}
