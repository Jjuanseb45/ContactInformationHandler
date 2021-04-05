using ContactInfoHandler.Dominio.Core.Areas;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Areas
{
    internal class AreaOfWorkRepository: BaseRepository<AreaOfWorkEntity>, IAreaOfWorkRepository
    {
        public AreaOfWorkRepository(IContextDb contexto) : base(contexto) { }
    }
}
