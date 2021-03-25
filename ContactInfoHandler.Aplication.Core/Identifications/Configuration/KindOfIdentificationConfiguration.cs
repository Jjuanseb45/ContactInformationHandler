using ContactInfoHandler.Application.Core.Identifications.Service;
using ContactInfoHandler.Application.Core.Mapper.Configuration;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ContactInfoHandler.Application.Core.Identifications.Configuration
{
    public static class KindOfIdentificationConfiguration
    {
        public static void ConfigureKindOfIdentification(this IServiceCollection services, DbSettings settings)
        {
            services.TryAddTransient<IKindOfIdentificationService, KindOfIdentificationService>();

            services.ConfigureMapper();
            services.ConfigureRepositories(settings);
        }
    }
}
