using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCORE.Repositories;
using NETCORE.Repositories.IRepositories;
using Newtonsoft.Json;

namespace NETCORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private IUsersRepository _repository;

        public RegistersController(IUsersRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Registers
        [HttpGet]
        public Task<IEnumerable<ProfileViewModel>> Get()
        {
            return _repository.Get();
        }

        // GET: api/Registers/5
        [HttpGet("{id}")]
        public Task<ProfileViewModel> Get(string id)
        {
            return _repository.Get(id);
        }

        // POST: api/Registers
        [HttpPost]
        public IActionResult Post(RegisterViewModel userView)
        {
            try
            {
                var create = _repository.Create(userView);

                return Ok(create);
            }
            catch (Exception)
            {
                return BadRequest(0);
            }
            
        }

        // PUT: api/Registers/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, RegisterViewModel userView)
        {
            try
            {
                var update = _repository.Update(id, userView);
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

            catch(Exception)
            {
                return BadRequest(0);
            }
        }
    }
}
