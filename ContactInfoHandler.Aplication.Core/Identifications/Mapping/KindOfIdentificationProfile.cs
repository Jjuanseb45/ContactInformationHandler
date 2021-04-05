using AutoMapper;
using ContactInfoHandler.Application.Dto.Identifications;
using ContactInfoHandler.Dominio.Core.Identifications;

namespace ContactInfoHandler.Application.Core.Identifications.Mapping
{
    public class KindOfIdentificationProfile : Profile
    {

        public KindOfIdentificationProfile()
        {
            CreateMap<KindOfIdentificationEntity, KindOfIdentificationDto>().ReverseMap();

        }

    }
}
