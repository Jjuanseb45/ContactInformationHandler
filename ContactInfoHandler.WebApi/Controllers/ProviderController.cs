using ContactInfoHandler.Application.Core.Persons.Providers.Service;
using ContactInfoHandler.Application.Dto.Persons.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ContactInfoHandler.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly ILogger<ProviderController> _logger;
        private readonly IProviderService _providerService;

        public ProviderController(ILogger<ProviderController> logger, IProviderService providerService)
        {
            _logger = logger;
            _providerService = providerService;
        }

        [HttpPost(nameof(InsertProvider))]
        public async Task<bool> InsertProvider(ProviderDto providerDto) =>
            await _providerService.InsertProvider(providerDto).ConfigureAwait(false);
        
        [HttpPost(nameof(DeleteProvider))]
        public async Task<bool> DeleteProvider(ProviderDto providerDto) =>
            await _providerService.DeleteProvider(providerDto).ConfigureAwait(false);

        [HttpPost(nameof(UpdateProvider))]
        public async Task<bool> UpdateProvider(ProviderDto providerDto) =>
            await _providerService.UpdateProvider(providerDto).ConfigureAwait(false);
    }
}
