using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NETCORE.Contexts;
using NETCORE.Models;
using NETCORE.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCORE.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        DynamicParameters parameters = new DynamicParameters();
        private readonly IConfiguration _configuration;
        private readonly MyContext _context;
        private readonly RoleManager<Role> _roleManager;

        public RolesRepository(IConfiguration configuration, MyContext context, RoleManager<Role> roleManager)
        {
            _configuration = configuration;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<Role>> Get()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> Get(string id)
        {
            return await _context.Roles.SingleOrDefaultAsync(r => r.Name == id);
        }

        public async Task<int> Create(Role role)
        {
            try
            {
                var create = await _roleManager.CreateAsync(role);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(string id, Role role)
        {
            try
            {
                Role roleUpdate = _context.Roles.Single(r => r.Name == id);
                roleUpdate.Name = role.Name;
                _context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(string id)
        {
            try
            {
                var deleteRole = _context.Roles.Single(r => r.Name == id);
                _context.Roles.Remove(deleteRole);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
