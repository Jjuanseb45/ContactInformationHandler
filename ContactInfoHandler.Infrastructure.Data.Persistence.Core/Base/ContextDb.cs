using ContactInfoHandler.Dominio.Core.Areas;
using ContactInfoHandler.Dominio.Core.Base;
using ContactInfoHandler.Dominio.Core.Identifications;
using ContactInfoHandler.Dominio.Core.Persons.Customers;
using ContactInfoHandler.Dominio.Core.Persons.Employees;
using ContactInfoHandler.Dominio.Core.Persons.Providers;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base
{
    class ContextDb: DbContext, IContextDb
    {
        DbSettings _settings;
        public virtual DbSet<AreaOfWorkEntity> Areas { get; set; }
        public virtual DbSet<KindOfIdentificationEntity> KindsOfId { get; set; }
        public virtual DbSet<CustomerEntity> Customers { get; set; }
        public virtual DbSet<EmployeeEntity> Employees { get; set; }
        public virtual DbSet<ProviderEntity> Providers { get; set; }


        public ContextDb(IOptions<DbSettings> settings) =>
                    _settings = settings.Value; 
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(_settings.ConnectionString);


        public void Commit() => base.SaveChanges();               

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public new DbSet<T> Set<T>() where T: BaseEntity => base.Set<T>();

    }
}
