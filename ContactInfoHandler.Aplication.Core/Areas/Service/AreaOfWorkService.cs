using AutoMapper;
using ContactInfoHandler.Application.Core.Base.Exceptions;
using ContactInfoHandler.Application.Dto.Areas;
using ContactInfoHandler.Dominio.Core.Areas;
using ContactInfoHandler.Dominio.Core.Persons.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactInfoHandler.Application.Core.Areas.Service
{
    internal class AreaOfWorkService : IAreaOfWorkService
    {
        private readonly IAreaOfWorkRepository _repo;
        private readonly IEmployeeRepository _repoEmployees;
        IMapper Mapper;

        public AreaOfWorkService(IAreaOfWorkRepository areaRepo, IMapper _mapper, IEmployeeRepository repoEmployees) { _repo = areaRepo; Mapper = _mapper; _repoEmployees = repoEmployees; }

        public async Task<bool> DeleteArea(AreaOfWorkDto area)
        {
            try
            {
                var AreaToDelete = await _repo.GetOne<AreaOfWorkEntity>(x => x.AreaId == area.AreaId).ConfigureAwait(false);
                if (AreaToDelete != null)
                {
                    var employees = await _repoEmployees.SearchMatching<EmployeeEntity>(x => x.AreaId == area.AreaId).ConfigureAwait(false);
                    if (!employees.Any())
                    {
                        await _repo.Delete<AreaOfWorkEntity>(AreaToDelete);
                        return true;
                    }
                    return false;

                }
                return false;
            }
            catch (Exception exce)
            {
                return false;
            }
        }


        public async Task<bool> InsertArea(AreaOfWorkDto area)
        {

            var areaA = await _repo.SearchMatching<AreaOfWorkEntity>(x => x.AreaId == area.AreaId);

            if (areaA.Any())
            {
                throw new AlreadyExistingAreaException("El area que intenta insertar ya existe");
            }
            if (area.ResponsableEmployeeId != default)
            {
                await _repo.Insert(Mapper.Map<AreaOfWorkEntity>(area)).ConfigureAwait(false);
                return true;
            }
            throw new NoAreaHandlerEspecifiedException("El area debe tener una persona encargada");
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


        public async Task<bool> UpdateArea(AreaOfWorkDto areaDto)
        {
            var AreaExist = await _repo.SearchMatching<AreaOfWorkEntity>(x => x.AreaId == areaDto.AreaId);
            if (!AreaExist.Any()) return false;

            var AreaToUpdate = AreaExist.FirstOrDefault();
                if (areaDto.AreaName != null || areaDto.AreaName != default) { AreaToUpdate.AreaName = areaDto.AreaName; } else { AreaToUpdate.AreaName = AreaToUpdate.AreaName; }
                if (areaDto.ResponsableEmployeeId != default) { AreaToUpdate.ResponsableEmployeeId = areaDto.ResponsableEmployeeId; } else { AreaToUpdate.ResponsableEmployeeId = AreaToUpdate.ResponsableEmployeeId; }
                await _repo.Update(AreaToUpdate);
                return true;            
        }

        public async Task<AreaOfWorkDto> GetOne(AreaOfWorkDto areaOfWorkDto)
        {
            var areaEntity = await _repo.GetOne<AreaOfWorkEntity>(x => x.AreaName == areaOfWorkDto.AreaName).ConfigureAwait(false);
            return Mapper.Map<AreaOfWorkDto>(areaEntity);

        }

        public void ValidateNotNullArguments(Object o)
        {
            if (o == null || o == default) throw new ArgumentNullException("El capo faltante es obligatorio");
        }

    }
}
