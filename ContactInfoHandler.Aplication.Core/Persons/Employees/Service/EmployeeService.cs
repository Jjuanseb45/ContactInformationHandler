using AutoMapper;
using ContactInfoHandler.Application.Dto.Persons.Employees;
using ContactInfoHandler.Dominio.Core.Persons;
using ContactInfoHandler.Dominio.Core.Persons.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactInfoHandler.Application.Core.Persons.Employees.Service
{
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRepository _employeeRepo;
        IMapper Mapper;
        public EmployeeService(IEmployeeRepository _employeeRepo, IMapper Mapper) => this._employeeRepo = _employeeRepo;

        public void AceptanceCriteries(EmployeeDto employee)
        {
            var ExistingSameKindIdAndIdNumber = _employeeRepo.SearchMatching<EmployeeEntity>(x => x.IdNumber == employee.IdNumber && x.KindOfIdentification.KindOfIdentificationId == employee.KindOfIdentification.KindOfIdentificationId).Result.Any();
            var ExistingSameNameAndKindOfPerson = _employeeRepo.SearchMatching<EmployeeEntity>(x => x.KindOfPerson == employee.KindOfPerson && x.FirstName == employee.FirstLastName && x.SecondName == employee.SecondName && x.FirstLastName == employee.FirstLastName && x.SecondLastName ==employee.SecondLastName).Result.Any();
            if (ExistingSameKindIdAndIdNumber || ExistingSameNameAndKindOfPerson)
            {
                throw new ArgumentException("Ya existe una entidad con parametros similares");
            }
            if (employee.SignUpDate == default || employee.DateOfBirth == default)
            {
                throw new ArgumentException("Por favor ingrese la fecha de nacimiento");
            }
            if (employee.KindOfIdentification.IdentificationName == "NIT")
            {
                throw new ArgumentException("Su tipo de identidad no puede ser NIT");
            }
            if (employee.KindOfPerson == KindOfPerson.Juridica)
            {
                throw new ArgumentException("Un empleado no puede ser juridico");
            }
        }

        public async Task<bool> InsertEmployee(EmployeeDto employee)
        {
            AceptanceCriteries(employee);

            try
            {
                await _employeeRepo.Insert(new EmployeeEntity
                {
                    IdEmployee = new Guid(),
                    EmployeeCode = employee.EmployeeCode,
                    WorkPosition = employee.WorkPosition,
                    SignUpDate = DateTimeOffset.Now,
                    DateOfBirth = employee.DateOfBirth,
                    AreaId = employee.AreaId,
                    IdNumber = employee.IdNumber,
                    AreaOfWork = employee.AreaOfWork,
                    KindOfPerson = employee.KindOfPerson,
                    KindOfIdentificationId = employee.KindOfIdentificationId,
                    FirstName = employee.FirstName,
                    SecondName = employee.SecondName,
                    FirstLastName = employee.FirstLastName,
                    SecondLastName = employee.SecondLastName,
                    Salary = employee.Salary,
                }).ConfigureAwait(false);
                return true;
            }
            catch {
                return false;
            }
        }

        public async Task<bool> DeleteEmployee(EmployeeDto employee)
        {
            var EmployeeToDelete = await _employeeRepo.GetOne<EmployeeEntity>(x => x.IdEmployee == employee.IdEmployee).ConfigureAwait(false);
            try
            {
                await _employeeRepo.Delete(EmployeeToDelete);
                return true;
            }
            catch
            {
                return false;
                throw new ArgumentException("No se eliminó la entidad");
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            var response = await _employeeRepo.GetAll<EmployeeEntity>().ConfigureAwait(false);
            if (response.ToArray().Length < 1)
            {
                throw new ArgumentException("No existen Areas");
            }
            return Mapper.Map<IEnumerable<EmployeeDto>>(response);
        }

        public async Task<bool> UpdateEmployee(EmployeeDto employee, Guid employeeId)
        {
            var entityToUpdate = await _employeeRepo.GetOne<EmployeeEntity>(x => x.IdEmployee == employeeId).ConfigureAwait(false);
            entityToUpdate.IdEmployee = new Guid();
            entityToUpdate.EmployeeCode = employee.EmployeeCode;
            entityToUpdate.WorkPosition = employee.WorkPosition;
            entityToUpdate.SignUpDate = DateTimeOffset.Now;
            entityToUpdate.DateOfBirth = employee.DateOfBirth;
            entityToUpdate.AreaId = employee.AreaId;
            entityToUpdate.IdNumber = employee.IdNumber;
            entityToUpdate.AreaOfWork = employee.AreaOfWork;
            entityToUpdate.KindOfPerson = KindOfPerson.Natural;
            entityToUpdate.KindOfIdentification = employee.KindOfIdentification;
            entityToUpdate.KindOfIdentificationId = employee.KindOfIdentificationId;
            entityToUpdate.FirstName = employee.FirstName;
            entityToUpdate.SecondName = employee.SecondName;
            entityToUpdate.FirstLastName = employee.FirstLastName;
            entityToUpdate.SecondLastName = employee.SecondLastName;
            entityToUpdate.Salary = employee.Salary;
            //AceptanceCriteries(Mapper.Map<ProviderDto>(entityToUpdate));
            await _employeeRepo.Update(entityToUpdate);
            return true;
        }


    }
}
