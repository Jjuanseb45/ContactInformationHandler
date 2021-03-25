using ContactInfoHandler.Application.Core.Mapper.Configuration;
using ContactInfoHandler.Application.Core.Persons.Customers.Service;
using ContactInfoHandler.Application.Core.Persons.Employees.Service;
using ContactInfoHandler.Application.Core.Persons.Providers.Service;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Application.Core.Persons.Configuration
{
    public static class PersonsConfigurator
    {
        public static void ConfigurePersons(this IServiceCollection services, DbSettings settings) {
            services.TryAddTransient<ICustomerService, CustomerService>();
            services.TryAddTransient<IEmployeeService, EmployeeService>();
            services.TryAddTransient<IProviderService, ProviderService>();

            services.ConfigureMapper();
            services.ConfigureRepositories(settings);
        }
    }
}
