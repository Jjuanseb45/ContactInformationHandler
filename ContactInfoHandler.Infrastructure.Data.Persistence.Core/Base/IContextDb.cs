using ContactInfoHandler.Dominio.Core.Areas;
using ContactInfoHandler.Dominio.Core.Base;
using ContactInfoHandler.Dominio.Core.Identifications;
using ContactInfoHandler.Dominio.Core.Persons.Customers;
using ContactInfoHandler.Dominio.Core.Persons.Employees;
using ContactInfoHandler.Dominio.Core.Persons.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base
{
    public interface IContextDb: IDisposable, IUnitOfWork
    {
        DbSet<AreaOfWorkEntity> Areas { get; }
        DbSet<KindOfIdentificationEntity> KindsOfId { get; }
        DbSet<CustomerEntity> Customers { get; }
        DbSet<EmployeeEntity> Employees { get; }
        DbSet<ProviderEntity> Providers { get;  }
    }
}
