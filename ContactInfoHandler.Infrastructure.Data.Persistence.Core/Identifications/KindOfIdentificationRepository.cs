using ContactInfoHandler.Dominio.Core.Identifications;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Identifications
{
    internal class KindOfIdentificationRepository: BaseRepository<KindOfIdentificationEntity>, IKindOfIdentificationRepository
    {
        public KindOfIdentificationRepository(IContextDb contexto):base(contexto) { }
    }
}
