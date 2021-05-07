using AutoMapper;
using ContactInfoHandler.Application.Dto.Areas;
using ContactInfoHandler.Dominio.Core.Areas;

namespace ContactInfoHandler.Application.Core.Areas.Mapping
{
    public class AreaOfWorkProfile: Profile
    {

        public AreaOfWorkProfile()
        {
            CreateMap<AreaOfWorkEntity, AreaOfWorkDto>().ReverseMap();
        }

    }
}
