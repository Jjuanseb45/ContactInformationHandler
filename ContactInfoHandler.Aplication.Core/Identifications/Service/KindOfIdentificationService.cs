using AutoMapper;
using ContactInfoHandler.Application.Core.Base.Exceptions;
using ContactInfoHandler.Application.Dto.Identifications;
using ContactInfoHandler.Dominio.Core.Identifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactInfoHandler.Application.Core.Identifications.Service
{
    internal class KindOfIdentificationService : IKindOfIdentificationService
    {
        private readonly IKindOfIdentificationRepository _kindOfIdRepo;
        IMapper Mapper;

        public KindOfIdentificationService(IKindOfIdentificationRepository _kindOfIdRepo, IMapper Mapper)
        {
            this._kindOfIdRepo = _kindOfIdRepo;
            this.Mapper = Mapper;
        }


        public async Task<bool> DeleteKindOfIdentification(KindOfIdentificationDto kindOfId)
        {
            if (kindOfId.IdentificationName == null) 
            {
                throw new DeniedDeleteNameNullException("Por favor Ingrese el nombre de la identificacion a eliminar");
            }

            if (kindOfId.IdentificationName.ToUpper() == "CEDULA" || kindOfId.IdentificationName.ToUpper() == "PASAPORTE" || kindOfId.IdentificationName.ToUpper() == "NIT") 
            {
                throw new DeleteDeniedKindOfIdImportantException("No puede eliminar una de las identificaciones fundamentales");
            }

            var KindOfIdToDelete = await _kindOfIdRepo.GetOne<KindOfIdentificationEntity>(x => x.KindOfIdentificationId == kindOfId.KindOfIdentificationId).ConfigureAwait(false);
            if (KindOfIdToDelete != null) { 
                await _kindOfIdRepo.Delete<KindOfIdentificationEntity>(KindOfIdToDelete);
                return true;
            }
            throw new ArgumentNullException("La identificacion que intenta eliminar no existe");            
        }

        public async Task<bool> VerifyExisting(KindOfIdentificationDto KindOfIdDto)
        {
            var EmployeeExists = await _kindOfIdRepo.SearchMatching<KindOfIdentificationEntity>(x => x.IdentificationName == KindOfIdDto.IdentificationName);
            if (EmployeeExists.Any()) { return true; } else { return false; }
        }

        public async Task<bool> InsertKindOfId(KindOfIdentificationDto kindOfId)
        {
            var areaA = _kindOfIdRepo.SearchMatching<KindOfIdentificationEntity>(x => x.KindOfIdentificationId == kindOfId.KindOfIdentificationId);
            var AreaToInsert = areaA.Result.Any();
            if (AreaToInsert)
            {
                throw new AlreadyExistingKindOfIDException("El tipo de identificacion ya existe");
            }
            await _kindOfIdRepo.Insert(new KindOfIdentificationEntity 
            { 
            
               KindOfIdentificationId = kindOfId.KindOfIdentificationId,
               IdentificationName = kindOfId.IdentificationName,
                      
            });
            return true;

        }

        public async Task<KindOfIdentificationDto> GetOne(KindOfIdentificationDto kindOfIdDto) 
        {
            var areaA = await _kindOfIdRepo.GetOne<KindOfIdentificationEntity>(x => x.IdentificationName == kindOfIdDto.IdentificationName).ConfigureAwait(false);
            return Mapper.Map<KindOfIdentificationDto>(areaA);

        }

        public async Task<IEnumerable<KindOfIdentificationDto>> GetKindOfId()
        {
            var response = await _kindOfIdRepo.GetAll<KindOfIdentificationEntity>().ConfigureAwait(false);
            if (response.ToArray().Length < 1)
            {
                throw new ArgumentException("No existen Id's");
            }
            return Mapper.Map<IEnumerable<KindOfIdentificationDto>>(response);
        }

        public async Task<bool> UpdateKindOfId(KindOfIdentificationDto kindOfId)
        {
            var entityToUpdate = await _kindOfIdRepo.GetOne<KindOfIdentificationEntity>(x => x.KindOfIdentificationId == kindOfId.KindOfIdentificationId).ConfigureAwait(false);

            if (entityToUpdate != null) {
                entityToUpdate.IdentificationName = kindOfId.IdentificationName;
                await _kindOfIdRepo.Update(entityToUpdate);
                return true;
            }
            throw new noExistingKindOfIdException("El area a actualizar no existe");
        }

    }
}
