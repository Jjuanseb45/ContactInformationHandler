using ContactInfoHandler.Application.Core.Areas.Service;
using ContactInfoHandler.Application.Core.Persons.Customers.Service;
using ContactInfoHandler.Application.Dto.Areas;
using ContactInfoHandler.Application.Dto.Persons.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactInfoHandler.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AreaOfWorkController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IAreaOfWorkService _areaOfWorkService;

        public AreaOfWorkController(ILogger<CustomerController> logger, IAreaOfWorkService AreaOfWorkService)
        {
            _logger = logger;
            _areaOfWorkService = AreaOfWorkService;
        }

        [HttpPost(nameof(InsertAreaOfWork))]
        public async Task<bool> InsertAreaOfWork(AreaOfWorkDto areaOfWorkdto) =>
            await _areaOfWorkService.InsertArea(areaOfWorkdto).ConfigureAwait(false);
        
        [HttpPost(nameof(DeleteAreaOfWork))]
        public async Task<bool> DeleteAreaOfWork(AreaOfWorkDto areaOfWorkdto) =>
            await _areaOfWorkService.DeleteArea(areaOfWorkdto).ConfigureAwait(false);

        [HttpGet(nameof(GetAreasOfWork))]
        public async Task<IEnumerable<AreaOfWorkDto>> GetAreasOfWork() =>
            await _areaOfWorkService.GetAreas().ConfigureAwait(false);

        [HttpPost(nameof(UpdateAreaOfWork))]
        public async Task<bool> UpdateAreaOfWork(AreaOfWorkDto areaOfWorkdto) =>
            await _areaOfWorkService.UpdateArea(areaOfWorkdto).ConfigureAwait(false);

    }
}
