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
            if (AreaToDelete.AreaEmployees.ToArray().Length < 1)
            {
                await _repo.Delete<AreaOfWorkEntity>(AreaToDelete);
                return true;
            }
            else
            {
                return false;
                throw new ArgumentException("El area tiene un empleado participante, no se puede eliminar");
            }
        }


        public async Task<bool> InsertArea(AreaOfWorkDto area)
        {
            var AreaToInsert = _repo.SearchMatching<AreaOfWorkEntity>(x => x.AreaId == area.AreaId).Result.Any();
            if (AreaToInsert)
            {
                return false;
            }
            ValidateNotNullArguments(area.AreaName);
            await _repo.Insert(new AreaOfWorkEntity { AreaId = new Guid(), AreaEmployees = area.AreaEmployees, AreaName = area.AreaName, Reponsable = area.Reponsable });
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

            return await _repo.Update(entityToUpdate);
        }

        public void ValidateNotNullArguments(Object o) {
            if (o == null) throw new ArgumentNullException("El capo faltante es obligatorio");
        }
    }
}
