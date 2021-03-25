using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContactInfoHandler.Dominio.Core.Base
{
    public interface IBaseRepository<T> where T: BaseEntity
    {
        IUnitOfWork _unitOfWork { get; }
        public Task<T> Insert<T>(T entidad) where T: BaseEntity;
        public Task<bool> Delete<T>(T entidad) where T: BaseEntity;
        public Task<bool> Update<T>(T entidad) where T : BaseEntity;
        public Task<IEnumerable<T>> GetAll<T>() where T : BaseEntity;
        public Task<T> GetOne<T>(Expression<Func<T, bool>> response) where T : BaseEntity;
        public Task<IEnumerable<T>> SearchMatching<T>(Expression<Func<T, bool>> response) where T : BaseEntity;
    }
}
