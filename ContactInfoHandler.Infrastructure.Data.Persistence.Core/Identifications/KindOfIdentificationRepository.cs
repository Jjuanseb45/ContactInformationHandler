using ContactInfoHandler.Dominio.Core.Identifications;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Identifications
{
    public class KindOfIdentificationRepository: BaseRepository<KindOfIdentificationEntity>, IKindOfIdentificationRepository
    {
        public KindOfIdentificationRepository(IContextDb contexto):base(contexto) { }
    }
}
