using AutoMapper;
using ContactInfoHandler.Application.Dto.Persons.Employees;
using ContactInfoHandler.Dominio.Core.Persons.Employees;

namespace ContactInfoHandler.Application.Core.Persons.Employees.Mapping
{

    class EmployeeProfile : Profile
        {
            public EmployeeProfile()
            {
                CreateMap<EmployeeEntity, EmployeeDto>().ReverseMap();

            }        
    }
}
