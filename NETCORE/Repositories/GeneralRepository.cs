using Microsoft.EntityFrameworkCore;
using NETCORE.Base;
using NETCORE.Contexts;
using NETCORE.Models;
using NETCORE.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCORE.Repositories
{
    public class GeneralRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, BaseModel
        where TContext : MyContext
    {
        protected internal MyContext _context;
        public GeneralRepository(MyContext context)
        {
            _context = context;
        }

        public virtual async Task<int> Create(TEntity entity)
        {
            entity.CreatedDate = DateTimeOffset.Now;
            await _context.Set<TEntity>().AddAsync(entity);
            var create = await _context.SaveChangesAsync();
            return create;
        }

        public async Task<int> Delete(int id)
        {
            var delete = await Get(id);
            if (delete == null)
            {
                return 0;
            }
            delete.DeletedDate = DateTimeOffset.Now;
            delete.isDelete = true;

            _context.Entry(delete).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();
            return result;
            
        }

        public async Task<List<TEntity>> Get()
        {
            var get = await _context.Set<TEntity>().Where(s => s.isDelete == false).ToListAsync();
            if (!get.Count().Equals(0))
            {
                return get;
            }
            return null;
        }

        public async Task<TEntity> Get(int id)
        {
            var get = await _context.Set<TEntity>().Where(s => s.isDelete == false && s.Id == id).SingleOrDefaultAsync();
            if (get != null)
            {
                return get;
            }
            return null;
        }

        public virtual async Task<int> Update(int id, TEntity entity)
        {
            var update = await _context.Set<TEntity>().Where(s => s.isDelete == false && s.Id == id).SingleOrDefaultAsync();
            if (update == null)
            {
                return 0;
            }
            update.Name = entity.Name;
            update.UpdatedDate = DateTimeOffset.Now;
            _context.Entry(update).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
