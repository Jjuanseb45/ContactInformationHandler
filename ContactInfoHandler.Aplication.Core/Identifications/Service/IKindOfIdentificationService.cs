using ContactInfoHandler.Application.Dto.Identifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactInfoHandler.Application.Core.Identifications.Service
{
    public interface IKindOfIdentificationService
    {
        public Task<bool> DeleteKindOfIdentification(KindOfIdentificationDto kindOfId);
        public Task<bool> InsertKindOfId(KindOfIdentificationDto kindOfId);
        public Task<IEnumerable<KindOfIdentificationDto>> GetKindOfId();
        public Task<bool> UpdateKindOfId(KindOfIdentificationDto kindOfId);
        public Task<KindOfIdentificationDto> GetOne(KindOfIdentificationDto kindOfIdDto);
        public Task<bool> VerifyExisting(KindOfIdentificationDto KindOfIdDto);

    }
}
