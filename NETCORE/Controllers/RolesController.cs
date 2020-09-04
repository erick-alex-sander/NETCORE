using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCORE.Models;
using NETCORE.Repositories.IRepositories;

namespace NETCORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private IRolesRepository _repository;

        public RolesController(IRolesRepository repository)
        {
            _repository = repository;
        }
        // GET: api/Roles
        [HttpGet]
        public Task<IEnumerable<Role>> Get()
        {
            return _repository.Get();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public Task<Role> Get(string id)
        {
            return _repository.Get(id);
        }

        // POST: api/Roles
        [HttpPost]
        public IActionResult Post(Role role)
        {
            try
            {
                var create = _repository.Create(role);
                return Ok(create);
            }
            catch (Exception)
            {
                return BadRequest(0);
            }
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, Role role)
        {
            try
            {
                var update = _repository.Update(id, role);
                return Ok(update);
            }
            catch (Exception)
            {
                return BadRequest(0);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var delete = _repository.Delete(id);
                return Ok(delete);
            }

            catch (Exception)
            {
                return BadRequest(0);
            }
        }
    }
}
