using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCORE.Repositories.IRepositories;

namespace NETCORE.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity, TRepository> : ControllerBase
        where TEntity : class
        where TRepository : IRepository<TEntity>
    {
        private IRepository<TEntity> _repo;

        public BaseController(TRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<TEntity>> Get() => await _repo.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id) => await _repo.Get(id);

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            
            var post = await _repo.Create(entity);
            if(post > 0)
            {
                return Ok(post);
            }
            return BadRequest(post);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TEntity>> Put(int id, TEntity entity)
        {
            var update = await _repo.Update(id, entity);
            if (update == 0)
            {
                return NotFound(update);
            }
            return Ok(update);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var delete = await _repo.Delete(id);
            if (delete == 0)
            {
                return NotFound(delete);
            }
            return Ok(delete);
        }
    }
}