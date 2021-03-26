using AutoMapper;
using ContactInfoHandler.Application.Dto.Persons.Providers;
using ContactInfoHandler.Dominio.Core.Persons.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactInfoHandler.Application.Core.Persons.Providers.Service
{
    public class ProviderService : IProviderService
    {
        IProviderRepository _providerRepo;
        IMapper Mapper;
        public ProviderService(IProviderRepository _providerRepo, IMapper Mapper) => this._providerRepo = _providerRepo;

        public void AceptanceCriteries(ProviderDto provider) 
        {
            var ExistingSameKindIdAndIdNumber = _providerRepo.SearchMatching<ProviderEntity>(x => x.IdNumber == provider.IdNumber && x.KindOfIdentification.KindOfIdentificationId == provider.KindOfIdentification.KindOfIdentificationId).Result.Any();
            var ExistingSameNameAndKindOfPerson = _providerRepo.SearchMatching<ProviderEntity>(x => x.KindOfPerson == provider.KindOfPerson && x.FirstName + x.SecondName + x.FirstLastName + x.SecondLastName == provider.FirstLastName + provider.SecondName + provider.FirstLastName + provider.SecondLastName).Result.Any();
            if (ExistingSameKindIdAndIdNumber || ExistingSameNameAndKindOfPerson)
            {
                throw new ArgumentException("Ya existe una entidad con parametros similares");
            }
            if (provider.SignUpDate == default || provider.DateOfBirth == default)
            {
                throw new ArgumentException("Por favor ingrese la fecha de nacimiento");
            }
            if (provider.KindOfIdentification.IdentificationName == "NIT")
            {
                throw new ArgumentException("Su tipo de identidad no puede ser NIT");
            }
        }

        public async Task<bool> InsertProvider(ProviderDto provider)
        {
            AceptanceCriteries(provider);
            await _providerRepo.Insert(new ProviderEntity
            {
                IdProvider = Guid.NewGuid(),
                SignUpDate = DateTimeOffset.Now,
                DateOfBirth = provider.DateOfBirth,
                CompanyName = provider.CompanyName,
                IdNumber = provider.IdNumber,
                ContactNumber = provider.ContactNumber,
                KindOfPerson = provider.KindOfPerson,
                KindOfIdentification = provider.KindOfIdentification,
                KindOfIdentificationId = provider.KindOfIdentificationId,
                FirstName = provider.FirstName,
                SecondName = provider.SecondName,
                FirstLastName = provider.FirstLastName,
                SecondLastName = provider.SecondLastName

            }).ConfigureAwait(false);
            return true;
        }
        

        public async Task<bool> DeleteProvider(ProviderDto provider)
        {
            var ProviderToDelete = await _providerRepo.GetOne<ProviderEntity>(x => x.IdProvider == provider.IdProvider).ConfigureAwait(false);
            try
            {
                await _providerRepo.Delete(ProviderToDelete);
                return true;
            }
            catch {
                return false;
                throw new ArgumentException("No se eliminó la entidad");
            }           
        }

        public async Task<IEnumerable<ProviderDto>> GetProviders()
        {
            var response = await _providerRepo.GetAll<ProviderEntity>().ConfigureAwait(false);
            if (response.ToArray().Length < 1)
            {
                throw new ArgumentException("No existen Areas");
            }
            return Mapper.Map<IEnumerable<ProviderDto>>(response);
        }

        public async Task<bool> UpdateProvider(ProviderDto provider, Guid providerId)
        {
            var entityToUpdate = await _providerRepo.GetOne<ProviderEntity>(x => x.IdProvider == providerId).ConfigureAwait(false);
            entityToUpdate.CompanyName = provider.CompanyName;
            entityToUpdate.ContactNumber = provider.ContactNumber;
            entityToUpdate.DateOfBirth = provider.DateOfBirth;
            entityToUpdate.SignUpDate = provider.SignUpDate;
            entityToUpdate.FirstName= provider.FirstName;
            entityToUpdate.SecondName = provider.SecondName;
            entityToUpdate.IdNumber = provider.IdNumber;
            entityToUpdate.KindOfPerson = provider.KindOfPerson;
            entityToUpdate.KindOfIdentification = provider.KindOfIdentification;
            entityToUpdate.KindOfIdentificationId = provider.KindOfIdentificationId;
            entityToUpdate.FirstLastName = provider.FirstLastName;
            entityToUpdate.SecondLastName = provider.SecondLastName;
            //AceptanceCriteries(Mapper.Map<ProviderDto>(entityToUpdate));
            await _providerRepo.Update(entityToUpdate);

            return true;
        }
    }
}

