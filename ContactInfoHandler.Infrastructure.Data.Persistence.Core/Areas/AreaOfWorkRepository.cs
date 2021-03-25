using ContactInfoHandler.Dominio.Core.Areas;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Areas
{
    public class AreaOfWorkRepository: BaseRepository<AreaOfWorkEntity>, IAreaOfWorkRepository
    {
        public AreaOfWorkRepository(IContextDb contexto) : base(contexto) { }
    }
}
