using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCORE.Repositories.IRepositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<ProfileViewModel>> Get();
        Task<ProfileViewModel> Get(string id);

        Task<int> Create(RegisterViewModel userView);
        int Update(string id, RegisterViewModel userView);
        int Delete(string id);
    }
}
