using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCORE.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> Get();
        Task<T> Get(int id);

        Task<int> Create(T entity);
        Task<int> Update(int id, T entity);
        Task<int> Delete(int id);
    }
}
