using AutoMapper;
using ContactInfoHandler.Application.Core.Base.Exceptions;
using ContactInfoHandler.Application.Dto.Persons.Customers;
using ContactInfoHandler.Dominio.Core.Identifications;
using ContactInfoHandler.Dominio.Core.Persons.Customers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactInfoHandler.Application.Core.Persons.Customers.Service
{
    internal class CustomerService : BaseService, ICustomerService
    {
        private readonly ICustomersRepository _repoCustomer;
        private readonly IKindOfIdentificationRepository _kindOfIdRepo;
        IMapper Mapper;
        public CustomerService(ICustomersRepository repoCustomer, IMapper Mapper, IKindOfIdentificationRepository KoiCustomer)
        {
            _repoCustomer = repoCustomer;
            this.Mapper = Mapper;
            _kindOfIdRepo = KoiCustomer;
        }
        public void AceptanceCriteriesForInsert(CustomerDto customer)
        {
            var NitEntity = _kindOfIdRepo.GetOne<KindOfIdentificationEntity>(x => x.IdentificationName == "Nit").Result;

            var ExistingSameKindIdAndIdNumber = _repoCustomer.SearchMatching<CustomerEntity>(x => x.IdNumber == customer.IdNumber && x.KindOfIdentificationId == customer.KindOfIdentificationId).Result.Any();
            var ExistingSameNameAndKindOfPerson = _repoCustomer.SearchMatching<CustomerEntity>(x => x.KindOfPerson == customer.KindOfPerson && x.FirstName + x.SecondName + x.FirstLastName + x.SecondLastName == customer.FirstName + customer.SecondName + customer.FirstLastName + customer.SecondLastName).Result.Any();

            if (ExistingSameKindIdAndIdNumber) { throw new PersonWithSameParametersExistingException("Ya existe una persona con el mismo numero y tipo de identificación"); }
            if (ExistingSameNameAndKindOfPerson) { throw new PersonWithSameParametersExistingException("Ya existe una persona con el mismo nombre y razon social"); }
            if (customer.SignUpDate == default) { throw new SignUpDateMissingException("Por favor ingrese la fecha de creación"); }
            if (customer.DateOfBirth == default) { throw new DateOfBirthMissingException("Por favor ingrese la fecha de nacimiento"); }
            if (customer.KindOfIdentificationId == NitEntity.KindOfIdentificationId) { throw new EmployeeWithNitException("El tipo de identificacion de una persona no puede ser Nit"); }

        }

        public async Task<bool> InsertCustomer(CustomerDto customer)
        {
            BaseService.ValidateKindOfPerson(customer);

            AceptanceCriteriesForInsert(customer);            

            var CustomerToInsert = await _repoCustomer.GetOne<CustomerEntity>(x => x.IdCustmer == customer.IdCustmer).ConfigureAwait(false);

            if (CustomerToInsert == null)
            {
                await _repoCustomer.Insert(Mapper.Map<CustomerEntity>(customer)).ConfigureAwait(false);
                return true;
            }
            throw new CustomerAlreadyOnDatabaseException();
        }


        public async Task<bool> DeleteCustomer(CustomerDto customer)
        {
            var CustomerToDelete = await _repoCustomer.GetOne<CustomerEntity>(x => x.IdCustmer == customer.IdCustmer).ConfigureAwait(false);
            if (CustomerToDelete != null)
            {
                await _repoCustomer.Delete(CustomerToDelete);
                return true;
            }
            throw new NoExistingCustomerException("No existe el cliente que busca eliminar");
        }


        public async Task<IEnumerable<CustomerDto>> GetCustomers()
        {
            var response = await _repoCustomer.GetAll<CustomerEntity>().ConfigureAwait(false);
            if (response.ToArray().Length < 1)
            {
                throw new NoExistingCustomerException("No existen Clientes");
            }
            return Mapper.Map<IEnumerable<CustomerDto>>(response);
        }


        public async Task<bool> UpdateCustomer(CustomerDto customerDto)
        {
            if (customerDto.KindOfPerson != null)
            {
                if (customerDto.KindOfPerson.ToUpper() != "JURIDICA" && customerDto.KindOfPerson.ToUpper() != "NATURAL")
                {
                    throw new InvalidKindOfPerson("La razón social solo puede ser Juridica o Natural");
                }
            }
            var AreaExist = await _repoCustomer.SearchMatching<CustomerEntity>(x => x.IdCustmer == customerDto.IdCustmer);
            if (!AreaExist.Any()) throw new NoExistingCustomerException("El cliente no existe");

            var AreaToUpdate = AreaExist.FirstOrDefault();

            if (customerDto.IdNumber != default) { AreaToUpdate.IdNumber = customerDto.IdNumber; } else { AreaToUpdate.IdNumber = AreaToUpdate.IdNumber; }
            if (customerDto.KindOfIdentificationId != default) { AreaToUpdate.KindOfIdentificationId = customerDto.KindOfIdentificationId; } else { AreaToUpdate.KindOfIdentificationId = AreaToUpdate.KindOfIdentificationId; }
            if (customerDto.KindOfPerson != default) { AreaToUpdate.KindOfPerson = customerDto.KindOfPerson; } else { AreaToUpdate.KindOfPerson = AreaToUpdate.KindOfPerson; }
            if (customerDto.FirstName != null) { AreaToUpdate.FirstName = customerDto.FirstName; } else { AreaToUpdate.FirstName = AreaToUpdate.FirstName; }
            if (customerDto.SecondName != null) { AreaToUpdate.SecondName = customerDto.SecondName; } else { AreaToUpdate.SecondName = AreaToUpdate.SecondName; }
            if (customerDto.FirstLastName != null) { AreaToUpdate.FirstLastName = customerDto.FirstLastName; } else { AreaToUpdate.FirstLastName = AreaToUpdate.FirstLastName; }
            if (customerDto.SecondLastName != null) { AreaToUpdate.SecondLastName = customerDto.SecondLastName; } else { AreaToUpdate.SecondLastName = AreaToUpdate.SecondLastName; }
            if (customerDto.FavoriteBrand != null) { AreaToUpdate.FavoriteBrand = customerDto.FavoriteBrand; } else { AreaToUpdate.FavoriteBrand = AreaToUpdate.FavoriteBrand; }
            if (customerDto.DateOfBirth != default) { AreaToUpdate.DateOfBirth = customerDto.DateOfBirth; } else { AreaToUpdate.DateOfBirth = AreaToUpdate.DateOfBirth; }
            if (customerDto.SignUpDate != default) { AreaToUpdate.SignUpDate = customerDto.SignUpDate; } else { AreaToUpdate.SignUpDate = AreaToUpdate.SignUpDate; }

            await _repoCustomer.Update(AreaToUpdate);
            return true;
        }
    }
}
