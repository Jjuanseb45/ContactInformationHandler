using ContactInfoHandler.Dominio.Core.Areas;
using ContactInfoHandler.Dominio.Core.Identifications;
using ContactInfoHandler.Dominio.Core.Persons.Customers;
using ContactInfoHandler.Dominio.Core.Persons.Employees;
using ContactInfoHandler.Dominio.Core.Persons.Providers;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Areas;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Identifications;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Persons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base.Configuration
{
    public static class RepositoryConfigurator
    {
        public static void ConfigureRepositories(this IServiceCollection service, DbSettings settings)
        {
            service.TryAddTransient<ICustomersRepository, CustomersRepository>();
            service.TryAddTransient<IEmployeeRepository, EmployeeRepository>();
            service.TryAddTransient<IProviderRepository, ProviderRepository>();
            service.TryAddTransient<IAreaOfWorkRepository, AreaOfWorkRepository>();
            service.TryAddTransient<IKindOfIdentificationRepository, KindOfIdentificationRepository>();

            service.ConfigureContext(settings);
        }

        public static void ConfigureContext(this IServiceCollection service, DbSettings settings)
        {
            service.Configure<DbSettings>(o => o.CopyFrom(settings));
            service.TryAddScoped<IContextDb, ContextDb>();
        }
    }
}