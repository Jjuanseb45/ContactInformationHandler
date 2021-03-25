using ContactInfoHandler.Dominio.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Dominio.Core.Persons.Providers
{
    public interface IProviderRepository: Base.IBaseRepository<BasePersonEntity>
    {
    }
}
