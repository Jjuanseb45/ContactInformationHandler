using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Persons.Providers.ProvidersHttpClient
{
    class ProvidersHttpClient
    {

        public ProvidersHttpClient(HttpClient cliente, IOptions<HttpClient> settings) : base(cliente, settings)
        {
        }
        protected override string Controller { get => "/Provider"; }
        public async Task<bool> DeleteOne(ProviderDto customerToDelete) => await Post("DeleteProvider").ConfigureAwait(false);
        public async Task<bool> InsertOne(ProviderDto customerToInsert) => await Post("InsertProvider").ConfigureAwait(false);
        public async Task<bool> UpdateOne(ProviderDto customerToUpdate) => await Post("UpdateProvider").ConfigureAwait(false);
    }
}