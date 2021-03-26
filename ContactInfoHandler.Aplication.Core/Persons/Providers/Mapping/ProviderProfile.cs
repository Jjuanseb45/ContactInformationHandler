using AutoMapper;
using ContactInfoHandler.Application.Dto.Persons.Providers;
using ContactInfoHandler.Dominio.Core.Persons.Providers;

namespace ContactInfoHandler.Application.Core.Persons.Providers.Mapping
{
    class ProviderProfile:Profile
    {
        public ProviderProfile()
        {
            CreateMap<ProviderEntity, ProviderDto>().ReverseMap();

        }

    }
}
