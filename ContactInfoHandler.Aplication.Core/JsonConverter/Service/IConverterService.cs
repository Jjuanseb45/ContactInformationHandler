using ContactInfoHandler.Application.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactInfoHandler.Application.Core.JsonConverter.Service
{
    public interface IConverterService
    {
        Task<string> ExportJson<T>(string path, T DtoToConvert) where T : IEnumerable<DtoBase>;
        Task<T> ImportJson<T>(string path) where T : IEnumerable<DtoBase>;        
    }
}
