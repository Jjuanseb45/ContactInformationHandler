using AutoMapper;
using ContactInfoHandler.Application.Dto.Areas;
using ContactInfoHandler.Dominio.Core.Areas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactInfoHandler.Application.Core.Areas.Service
{
    class AreaOfWorkService : IAreaOfWorkService
    {
        IAreaOfWorkRepository _repo;
        IMapper Mapper;

        public AreaOfWorkService(IAreaOfWorkRepository areaRepo, IMapper _mapper) { _repo = areaRepo; Mapper = _mapper; }

        public async Task<bool> DeleteArea(AreaOfWorkDto area)
        {
            var AreaToDelete = await _repo.GetOne<AreaOfWorkEntity>(x => x.AreaId == area.AreaId).ConfigureAwait(false);
            var numberOfEmployees = AreaToDelete.AreaEmployees.Count();
            if (numberOfEmployees <= 0 )
            {
                await _repo.Delete<AreaOfWorkEntity>(AreaToDelete);
                return true;
            }           
                return false;
                throw new ArgumentException("El area tiene un empleado participante, no se puede eliminar");            
        }


        public async Task<bool> InsertArea(AreaOfWorkDto area)
        {
            var areaA = _repo.SearchMatching<AreaOfWorkEntity>(x => x.AreaId == area.AreaId);
            var areas = areaA.Result;
            var AreaToInsert = areaA.Result.Any();
            if (AreaToInsert)
            {
                return false;
            }
            if (area.ResponsableEmployeeId == default) 
            {
                return false;
            }
            await _repo.Insert(new AreaOfWorkEntity { AreaId = area.AreaId, AreaEmployees = area.AreaEmployees, AreaName = area.AreaName, Reponsable = area.Reponsable, ResponsableEmployeeId= area.ResponsableEmployeeId });
            return true;

        }

        public async Task<IEnumerable<AreaOfWorkDto>> GetAreas()
        {
            var response = await _repo.GetAll<AreaOfWorkEntity>().ConfigureAwait(false);
            if (response.ToArray().Length < 1)
            {
                throw new ArgumentException("No existen Areas");
            }
            return Mapper.Map<IEnumerable<AreaOfWorkDto>>(response);
        }

        public async Task<bool> UpdateArea(AreaOfWorkDto area, Guid areaId)
        {
            var entityToUpdate = await _repo.GetOne<AreaOfWorkEntity>(x => x.AreaId == areaId).ConfigureAwait(false);
            entityToUpdate.AreaName = area.AreaName;
            entityToUpdate.Reponsable = area.Reponsable;
            entityToUpdate.AreaEmployees = area.AreaEmployees;

            await _repo.Update(entityToUpdate);

            return true;
        }

        public void ValidateNotNullArguments(Object o) {
            if (o == null || o== default) throw new ArgumentNullException("El capo faltante es obligatorio");
        }
    }
}
