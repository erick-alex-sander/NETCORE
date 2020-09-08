using NETCORE.Contexts;
using NETCORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCORE.Repositories
{
    public class DepartmentRepository : GeneralRepository<Department, MyContext>
    {
        public DepartmentRepository(MyContext context) : base(context)
        {

        }
    }
}
