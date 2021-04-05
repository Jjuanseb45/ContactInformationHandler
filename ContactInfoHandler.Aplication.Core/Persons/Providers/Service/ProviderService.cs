using AutoMapper;
using ContactInfoHandler.Application.Core.Base.Exceptions;
using ContactInfoHandler.Application.Dto.Persons.Providers;
using ContactInfoHandler.Dominio.Core.Identifications;
using ContactInfoHandler.Dominio.Core.Persons.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactInfoHandler.Application.Core.Persons.Providers.Service
{
    internal class ProviderService : BaseService, IProviderService
    {
        private readonly IProviderRepository _providerRepo;
        private readonly IKindOfIdentificationRepository _kindOfIdRepo;
        IMapper Mapper;
        public ProviderService(IProviderRepository _providerRepo, IMapper Mapper, IKindOfIdentificationRepository KindOfIdRepo)
        {
            this._providerRepo = _providerRepo;
            this.Mapper = Mapper;
            _kindOfIdRepo = KindOfIdRepo;
        }

        public void AceptanceCriteriesForInsert(ProviderDto provider)
        {
            var NitEntity = _kindOfIdRepo.GetOne<KindOfIdentificationEntity>(x => x.IdentificationName == "Nit").Result;

            var ExistingSameKindIdAndIdNumber = _providerRepo.SearchMatching<ProviderEntity>(x => x.IdNumber == provider.IdNumber && x.KindOfIdentificationId == provider.KindOfIdentificationId).Result.Any();
            var ExistingSameNameAndKindOfPerson = _providerRepo.SearchMatching<ProviderEntity>(x => x.KindOfPerson == provider.KindOfPerson && x.FirstName + x.SecondName + x.FirstLastName + x.SecondLastName == provider.FirstName + provider.SecondName + provider.FirstLastName + provider.SecondLastName).Result.Any();

            if (ExistingSameKindIdAndIdNumber) { throw new PersonWithSameParametersExistingException("Ya existe una persona con el mismo numero y tipo de identificación"); }
            if (ExistingSameNameAndKindOfPerson) { throw new PersonWithSameParametersExistingException("Ya existe una persona con el mismo nombre y razon social"); }
            if (provider.SignUpDate == default) { throw new SignUpDateMissingException("Por favor ingrese la fecha de creación"); }
            if (provider.DateOfBirth == default) { throw new DateOfBirthMissingException("Por favor ingrese la fecha de nacimiento"); }
            if (provider.KindOfIdentificationId == NitEntity.KindOfIdentificationId) { throw new EmployeeWithNitException("El tipo de identificacion de una persona no puede ser Nit"); }
        }

        public async Task<bool> InsertProvider(ProviderDto provider)
        {

            BaseService.ValidateKindOfPerson(provider);

            AceptanceCriteriesForInsert(provider);

            var ProviderToInsert = await _providerRepo.GetOne<ProviderEntity>(x => x.IdProvider == provider.IdProvider).ConfigureAwait(false);

            if (ProviderToInsert == null)
            {
                await _providerRepo.Insert(Mapper.Map<ProviderEntity>(provider)).ConfigureAwait(false);
                return true;
            }
            throw new ProviderAlreadyOnDatabaseException("El proveedor ya esta registrado en la base de datos");
        }


        public async Task<bool> DeleteProvider(ProviderDto provider)
        {
            var ProviderToDelete = await _providerRepo.GetOne<ProviderEntity>(x => x.IdProvider == provider.IdProvider).ConfigureAwait(false);

            if (ProviderToDelete != null)
            {
                await _providerRepo.Delete(ProviderToDelete);
                return true;
            }
            throw new NoExistingProviderException("No existe el proovedor a eliminar");
        }

        public async Task<IEnumerable<ProviderDto>> GetProviders()
        {
            var ProvidersList = await _providerRepo.GetAll<ProviderEntity>().ConfigureAwait(false);
            if (ProvidersList.ToArray().Length < 1)
            {
                throw new NoExistingProviderException("No existen Proovedores");
            }
            return Mapper.Map<IEnumerable<ProviderDto>>(ProvidersList);
        }


        public async Task<bool> UpdateProvider(ProviderDto providerDto)
        {

            BaseService.ValidateKindOfPerson(providerDto);

            var ProviderExists = await _providerRepo.SearchMatching<ProviderEntity>(x => x.IdProvider == providerDto.IdProvider);
            if (!ProviderExists.Any()) throw new NoExistingProviderException("El proveedor que busca actualizar no existe");

            var ProviderToUpdate = ProviderExists.FirstOrDefault();
            if (providerDto.IdNumber != default) { ProviderToUpdate.IdNumber = providerDto.IdNumber; } else { ProviderToUpdate.IdNumber = ProviderToUpdate.IdNumber; }
            if (providerDto.KindOfIdentificationId != default) { ProviderToUpdate.KindOfIdentificationId = providerDto.KindOfIdentificationId; } else { ProviderToUpdate.KindOfIdentificationId = ProviderToUpdate.KindOfIdentificationId; }
            if (providerDto.KindOfPerson != default) { ProviderToUpdate.KindOfPerson = providerDto.KindOfPerson; } else { ProviderToUpdate.KindOfPerson = ProviderToUpdate.KindOfPerson; }
            if (providerDto.FirstName != null) { ProviderToUpdate.FirstName = providerDto.FirstName; } else { ProviderToUpdate.FirstName = ProviderToUpdate.FirstName; }
            if (providerDto.SecondName != null) { ProviderToUpdate.SecondName = providerDto.SecondName; } else { ProviderToUpdate.SecondName = ProviderToUpdate.SecondName; }
            if (providerDto.FirstLastName != null) { ProviderToUpdate.FirstLastName = providerDto.FirstLastName; } else { ProviderToUpdate.FirstLastName = ProviderToUpdate.FirstLastName; }
            if (providerDto.SecondLastName != null) { ProviderToUpdate.SecondLastName = providerDto.SecondLastName; } else { ProviderToUpdate.SecondLastName = ProviderToUpdate.SecondLastName; }
            if (providerDto.CompanyName != null) { ProviderToUpdate.CompanyName = providerDto.CompanyName; } else { ProviderToUpdate.CompanyName = ProviderToUpdate.CompanyName; }
            if (providerDto.ContactNumber != default) { ProviderToUpdate.ContactNumber = providerDto.ContactNumber; } else { ProviderToUpdate.ContactNumber = ProviderToUpdate.ContactNumber; }
            if (providerDto.DateOfBirth != default) { ProviderToUpdate.DateOfBirth = providerDto.DateOfBirth; } else { ProviderToUpdate.DateOfBirth = ProviderToUpdate.DateOfBirth; }
            if (providerDto.SignUpDate != default) { ProviderToUpdate.SignUpDate = providerDto.SignUpDate; } else { ProviderToUpdate.SignUpDate = ProviderToUpdate.SignUpDate; }


            await _providerRepo.Update(ProviderToUpdate);
            return true;
        }
    }
}

