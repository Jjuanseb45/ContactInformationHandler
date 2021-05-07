using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Infrastructure.Transversal.Configurator
{
    class HttpClientGenericBaseConfigurator
    {
        public static void ConfigureHttpClientService(IService service, HttpClientSettings settings)
        {
            services.AddHttpClient<HttpGenericBaseClient>();
            services.Configure<HttpClientSettings>(o => o.CopyFrom(settings));
            service.AddTransient<IHttpClientGenericBase, HttpClientGenericBase>();
        }
    }
}
