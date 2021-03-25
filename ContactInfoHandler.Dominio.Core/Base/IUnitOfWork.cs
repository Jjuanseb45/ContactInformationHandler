using Microsoft.EntityFrameworkCore;
using System;

namespace ContactInfoHandler.Dominio.Core.Base
{
    public interface IUnitOfWork: IDisposable
    {
        public void Commit();
        public void Rollback();
        public DbSet<T> Set<T>() where T : BaseEntity;
    }
}
