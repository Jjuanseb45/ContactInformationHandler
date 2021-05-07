using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactInfoHandler.Application.Dto.Base;

namespace ContactInfoHandler.Infrastructure.Transversal.GenericClass
{
    public interface IHttpClientGenericBase<T> where  T: DtoBase
    {
        Task<IEnumerable<T>> Get(string action);
        Task<bool> Post(T request, string action);
        Task<bool> Put(T request, string action);
        Task<bool> Patch(T request, string action);
        Task<bool> Delete(T request, string action);

    }
}