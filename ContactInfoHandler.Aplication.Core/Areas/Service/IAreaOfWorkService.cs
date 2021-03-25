﻿using ContactInfoHandler.Application.Dto.Areas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactInfoHandler.Application.Core.Areas.Service
{
    public interface IAreaOfWorkService
    {
        public Task<bool> DeleteArea(AreaOfWorkDto area);
        public Task<bool> InsertArea(AreaOfWorkDto area);
        public Task<IEnumerable<AreaOfWorkDto>> GetAreas();
        public Task<bool> UpdateArea(AreaOfWorkDto area, Guid areaId);
    }
}