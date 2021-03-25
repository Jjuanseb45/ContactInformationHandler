using AutoMapper;
using ContactInfoHandler.Application.Dto.Areas;
using ContactInfoHandler.Dominio.Core.Areas;
using System;
using System.Collections.Generic;
using System.Text;

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
