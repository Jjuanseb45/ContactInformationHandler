using ContactInfoHandler.Application.Core.Areas.Service;
using ContactInfoHandler.Application.Core.Mapper.Configuration;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace ContactInfoHandler.Application.Core.Areas.Configuration
{   

    public static class AreaOfWorkConfiguration
    {
        public static void ConfigureAreaOfWork(this IServiceCollection services, DbSettings settings)
        {
            services.TryAddTransient<IAreaOfWorkService, AreaOfWorkService>();

            services.ConfigureMapper();
            services.ConfigureRepositories(settings);
        }
        
    }
}
