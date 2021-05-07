using System.Collections.Generic;
using System.Threading.Tasks;
using ContactInfoHandler.Application.Dto.Areas;

namespace ContactInfoHandler.Application.Core.Areas.HttpClientAreas
{
    public interface IHttpClientArea
    {
        public Task<IEnumerable<AreaOfWorkDto>> GetAll();
        public Task<bool> Post(AreaOfWorkDto areaOfWork);
    }
}