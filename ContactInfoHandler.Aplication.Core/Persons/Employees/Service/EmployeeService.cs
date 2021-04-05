using AutoMapper;
using ContactInfoHandler.Application.Core.Base.Exceptions;
using ContactInfoHandler.Application.Dto.Persons.Employees;
using ContactInfoHandler.Dominio.Core.Identifications;
using ContactInfoHandler.Dominio.Core.Persons.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactInfoHandler.Application.Core.Persons.Employees.Service
{
    internal class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IKindOfIdentificationRepository _KindOfIdRepo;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository EmployeeRepo, IMapper Mapper, IKindOfIdentificationRepository KindOfIdRepo) { _mapper = Mapper; _employeeRepo = EmployeeRepo; _KindOfIdRepo = KindOfIdRepo; }

        public void AceptanceCriteriesForInsert(EmployeeDto employee)
        {
            var NitEntity = _KindOfIdRepo.GetOne<KindOfIdentificationEntity>(x => x.IdentificationName == "Nit").Result;

            var ExistingSameNameAndKindOfPerson = _employeeRepo.SearchMatching<EmployeeEntity>(x => x.KindOfPerson == employee.KindOfPerson && x.FirstName + x.SecondName + x.FirstLastName + x.SecondLastName == employee.FirstName + employee.SecondName + employee.FirstLastName + employee.SecondLastName).Result.Any();
            var ExistingSameKindIdAndIdNumber = _employeeRepo.SearchMatching<EmployeeEntity>(x => x.IdNumber == employee.IdNumber && x.KindOfIdentificationId == employee.KindOfIdentificationId).Result.Any();
            var ExistingEmployeeCode = _employeeRepo.SearchMatching<EmployeeEntity>(x => x.EmployeeCode == employee.EmployeeCode).Result.Any();

            if (ExistingSameKindIdAndIdNumber) { throw new PersonWithSameParametersExistingException("Ya existe una persona con el mismo numero y tipo de identificación"); }
            if (ExistingEmployeeCode) { throw new AlreadyExistingEmployeeCodeException("Ya existe un empleado con el mismo codigo"); }
            if (ExistingSameNameAndKindOfPerson) { throw new PersonWithSameParametersExistingException("Ya existe una persona con el mismo nombre y razon social"); }
            if (employee.SignUpDate == default) { throw new SignUpDateMissingException("Por favor ingrese la fecha de creación"); }
            if (employee.DateOfBirth == default) { throw new DateOfBirthMissingException("Por favor ingrese la fecha de nacimiento"); }
            if (employee.KindOfIdentificationId == NitEntity.KindOfIdentificationId) { throw new EmployeeWithNitException("El tipo de identificacion de una persona no puede ser Nit"); }
            if (employee.KindOfPerson == "Juridica") { throw new JuridicEmployeeException("Un empleado no puede ser juridico"); }
        }

        public async Task<bool> InsertEmployee(EmployeeDto employee)
        {
            if (employee.KindOfPerson != null)
            {
                switch (employee.KindOfPerson.ToUpper())
                {
                    case "JURIDICA": throw new JuridicEmployeeException("Un empleado no puede ser juridico, por favor revise la entrada");
                }
            }

            BaseService.ValidateKindOfPerson(employee);

            AceptanceCriteriesForInsert(employee);
            if (employee.EmployeeCode != default)
            {
                if (employee.AreaId != default)
                {
                    try
                    {
                        await _employeeRepo.Insert(_mapper.Map<EmployeeEntity>(employee)).ConfigureAwait(false);
                        return true;
                    }
                    catch
                    {
                        throw new AlreadyExistingEmployeeException("Ya existe el empleado que esta intentando ingresar");
                    }
                }
                throw new NoAreaEspecifiedException("El empleado ingresado debe tener un area");
            }
            throw new NoEspecifiedEmployeeCodeException("El empleado debe tener un codigo unico e irrepetible");
        }

        public async Task<bool> DeleteEmployee(EmployeeDto employee)
        {
            var EmployeeToDelete = await _employeeRepo.GetOne<EmployeeEntity>(x => x.IdEmployee == employee.IdEmployee).ConfigureAwait(false);
            if (EmployeeToDelete != null)
            {
                try
                {
                    await _employeeRepo.Delete(EmployeeToDelete);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            throw new NoExistingEmployee("El empleado que intenta eliminar no se encuentra registrado");
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            var response = await _employeeRepo.GetAll<EmployeeEntity>().ConfigureAwait(false);
            if (response.ToArray().Length < 1)
            {
                throw new NoExistingEmployeesRegisteredException("No existen empleados registrados");
            }
            return _mapper.Map<IEnumerable<EmployeeDto>>(response);
        }


        public async Task<bool> UpdateEmployee(EmployeeDto employeeDto)
        {
            if (employeeDto.KindOfPerson != null)
            {
                if (employeeDto.KindOfPerson.ToUpper() != "JURIDICA" && employeeDto.KindOfPerson.ToUpper() != "NATURAL")
                {
                    throw new InvalidKindOfPerson("La razón social solo puede ser Juridica o Natural");
                }
            }

            var EmployeeExists = await _employeeRepo.SearchMatching<EmployeeEntity>(x => x.IdEmployee == employeeDto.IdEmployee);
            if (!EmployeeExists.Any()) throw new NoExistingEmployee("El empleado que intenta actualizar no se encuentra registrado");

            var EmployeeToUpdate = EmployeeExists.FirstOrDefault();

            EmployeeToUpdate.IdNumber = employeeDto.IdNumber;
            EmployeeToUpdate.KindOfIdentificationId = employeeDto.KindOfIdentificationId;
            EmployeeToUpdate.KindOfPerson = employeeDto.KindOfPerson;
            EmployeeToUpdate.FirstName = employeeDto.FirstName;
            EmployeeToUpdate.SecondName = employeeDto.SecondName;
            EmployeeToUpdate.FirstLastName = employeeDto.FirstLastName;
            EmployeeToUpdate.SecondLastName = employeeDto.SecondLastName;
            EmployeeToUpdate.DateOfBirth = employeeDto.DateOfBirth;
            EmployeeToUpdate.SignUpDate = employeeDto.SignUpDate;
            EmployeeToUpdate.Salary = employeeDto.Salary;
            EmployeeToUpdate.EmployeeCode = employeeDto.EmployeeCode;
            EmployeeToUpdate.IdEmployee = employeeDto.IdEmployee;

            await _employeeRepo.Update(EmployeeToUpdate);
            return true;

            /*
            if (employeeDto.IdNumber != default) { EmployeeToUpdate.IdNumber = employeeDto.IdNumber; } else { EmployeeToUpdate.IdNumber = EmployeeToUpdate.IdNumber; }
            if (employeeDto.KindOfIdentificationId != default) { EmployeeToUpdate.KindOfIdentificationId = employeeDto.KindOfIdentificationId; } else { EmployeeToUpdate.KindOfIdentificationId = EmployeeToUpdate.KindOfIdentificationId; }
            if (employeeDto.KindOfPerson != default) { EmployeeToUpdate.KindOfPerson = employeeDto.KindOfPerson; } else { EmployeeToUpdate.KindOfPerson = EmployeeToUpdate.KindOfPerson; }
            if (employeeDto.FirstName != null) { EmployeeToUpdate.FirstName = employeeDto.FirstName; } else { EmployeeToUpdate.FirstName = EmployeeToUpdate.FirstName; }
            if (employeeDto.SecondName != null) { EmployeeToUpdate.SecondName = employeeDto.SecondName; } else { EmployeeToUpdate.SecondName = EmployeeToUpdate.SecondName; }
            if (employeeDto.FirstLastName != null) { EmployeeToUpdate.FirstLastName = employeeDto.FirstLastName; } else { EmployeeToUpdate.FirstLastName = EmployeeToUpdate.FirstLastName; }
            if (employeeDto.SecondLastName != null) { EmployeeToUpdate.SecondLastName = employeeDto.SecondLastName; } else { EmployeeToUpdate.SecondLastName = EmployeeToUpdate.SecondLastName; }
            if (employeeDto.DateOfBirth != default) { EmployeeToUpdate.DateOfBirth = employeeDto.DateOfBirth; } else { EmployeeToUpdate.DateOfBirth = EmployeeToUpdate.DateOfBirth; }
            if (employeeDto.SignUpDate != default) { EmployeeToUpdate.SignUpDate = employeeDto.SignUpDate; } else { EmployeeToUpdate.SignUpDate = EmployeeToUpdate.SignUpDate; }
            if (employeeDto.Salary != default) { EmployeeToUpdate.Salary = employeeDto.Salary; } else { EmployeeToUpdate.Salary = EmployeeToUpdate.Salary; }
            if (employeeDto.WorkPosition != default) { EmployeeToUpdate.WorkPosition = employeeDto.WorkPosition; } else { EmployeeToUpdate.WorkPosition = EmployeeToUpdate.WorkPosition; }
            if (employeeDto.EmployeeCode != default) { EmployeeToUpdate.EmployeeCode = employeeDto.EmployeeCode; } else { EmployeeToUpdate.EmployeeCode = EmployeeToUpdate.EmployeeCode; }
            if (employeeDto.IdEmployee != default) { EmployeeToUpdate.IdEmployee = employeeDto.IdEmployee; } else { EmployeeToUpdate.IdEmployee = EmployeeToUpdate.IdEmployee; }
            */
        }


    }
}
