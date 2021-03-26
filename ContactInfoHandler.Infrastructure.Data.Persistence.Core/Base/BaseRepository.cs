using ContactInfoHandler.Dominio.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base
{
    public class BaseRepository<TGeneric> : IBaseRepository<TGeneric> where TGeneric : BaseEntity
    {
        private readonly IUnitOfWork _unidadDeTrabajo;
        public IUnitOfWork _unitOfWork => _unidadDeTrabajo;

        public BaseRepository(IUnitOfWork _UnitOfWork) { _unidadDeTrabajo = _UnitOfWork; }

        public async Task<bool> Delete<T>(T entidad) where T : BaseEntity
        {
            try
            {
                var EntityToDelete = _unidadDeTrabajo.Set<T>().First(x => x == entidad);
                _unidadDeTrabajo.Set<T>().Remove(EntityToDelete);
                _unidadDeTrabajo.Commit();
                return await Task.FromResult(true);
            }
            catch 
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : BaseEntity => await Task.FromResult(_unidadDeTrabajo.Set<T>().ToArray());     

        public async Task<T> GetOne<T>(Expression<Func<T, bool>> response) where T : BaseEntity
        {
            return await Task.FromResult(_unidadDeTrabajo.Set<T>().First(response));
        }

        public async Task<T> Insert<T>(T entidad) where T : BaseEntity
        {
            var response = await _unidadDeTrabajo.Set<T>().AddAsync(entidad);
            _unidadDeTrabajo.Commit();
            return response.Entity;
        }

        public async Task<IEnumerable<T>> SearchMatching<T>(Expression<Func<T, bool>> response) where T : BaseEntity=> 
            await Task.FromResult(_unidadDeTrabajo.Set<T>().Where(response));

        public async Task<bool> Update<T>(T entidad) where T : BaseEntity
        {
            try
            {
                var response = _unidadDeTrabajo.Set<T>().Update(entidad);
                _unidadDeTrabajo.Commit();
                return await Task.FromResult(true);
            }
            catch (Exception exc)
            {
                return await Task.FromResult(false);
            }
          
        }
    }
}
