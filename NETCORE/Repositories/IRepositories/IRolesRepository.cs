using NETCORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCORE.Repositories.IRepositories
{
    public interface IRolesRepository
    {
        Task<IEnumerable<Role>> Get();
        Task<Role> Get(string id);

        Task<int> Create(Role role);
        int Update(string id, Role role);
        int Delete(string id);
    }
}
