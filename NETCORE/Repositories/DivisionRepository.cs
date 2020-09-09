using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using NETCORE.Contexts;
using NETCORE.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NETCORE.Repositories
{
    public class DivisionRepository : GeneralRepository<Division, MyContext>
    {
        public DivisionRepository(MyContext context) : base(context)
        {

        }
        

        //SqlConnection conn = new SqlConnection(.ConnectionStrings["myConnection"].ConnectionString);
        //DynamicParameters parameters = new DynamicParameters();

        public override async Task<int> Create(Division division)
        {
            division.CreatedDate = DateTimeOffset.Now;
            division.Department = await _context.Departments.FindAsync(division.Department.Id);
            await _context.Divisions.AddAsync(division);
            var create = await _context.SaveChangesAsync();
            return create;
        }

        public override async Task<int> Update(int id, Division division)
        {
            var update = await _context.Set<Division>().Where(s => s.isDelete == false && s.Id == id).SingleOrDefaultAsync();
            if (update == null)
            {
                return 0;
            }
            update.Name = division.Name;
            update.UpdatedDate = DateTimeOffset.Now;
            update.Department = await _context.Departments.FindAsync(division.Department.Id);
            _context.Entry(update).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
