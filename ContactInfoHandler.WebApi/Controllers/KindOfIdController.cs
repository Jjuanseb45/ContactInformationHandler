using ContactInfoHandler.Application.Core.Identifications.Service;
using ContactInfoHandler.Application.Core.Persons.Customers.Service;
using ContactInfoHandler.Application.Dto.Identifications;
using ContactInfoHandler.Application.Dto.Persons.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactInfoHandler.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KindOfIdController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IKindOfIdentificationService _kindOfIdentificationService;

        public KindOfIdController(ILogger<CustomerController> logger, IKindOfIdentificationService KindOfIdentificationService)
        {
            _logger = logger;
            _kindOfIdentificationService = KindOfIdentificationService;
        }

        [HttpPost(nameof(InsertKindOfIdentification))]
        public async Task<bool> InsertKindOfIdentification(KindOfIdentificationDto kindOfIdentificationDto) =>
            await _kindOfIdentificationService.InsertKindOfId(kindOfIdentificationDto).ConfigureAwait(false);
        
        [HttpPost(nameof(DeleteKindOfIdentification))]
        public async Task<bool> DeleteKindOfIdentification(KindOfIdentificationDto kindOfIdentificationDto) =>
            await _kindOfIdentificationService.DeleteKindOfIdentification(kindOfIdentificationDto).ConfigureAwait(false);

        [HttpGet(nameof(GetKindsOfIdentification))]
        public async Task<IEnumerable<KindOfIdentificationDto>> GetKindsOfIdentification() =>
            await _kindOfIdentificationService.GetKindOfId().ConfigureAwait(false);

        [HttpPost(nameof(UpdateKindOfIdentification))]
        public async Task<bool> UpdateKindOfIdentification(KindOfIdentificationDto kindOfIdentificationDto) =>
            await _kindOfIdentificationService.UpdateKindOfId(kindOfIdentificationDto).ConfigureAwait(false);

    }
}
