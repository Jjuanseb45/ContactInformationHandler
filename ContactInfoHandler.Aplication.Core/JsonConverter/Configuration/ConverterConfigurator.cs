using ContactInfoHandler.Application.Core.JsonConverter.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace ContactInfoHandler.Application.Core.JsonConverter.Configuration
{
    public static class ConverterConfigurator
    {
        public static void ConfigureConverter(this IServiceCollection services)
        {
            services.TryAddTransient<IConverterService, ConverterService>();
        }
    }
}
