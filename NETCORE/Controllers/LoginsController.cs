using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCORE.Contexts;
using NETCORE.ViewModel;

namespace NETCORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly MyContext _context;

        public LoginsController(MyContext context)
        {
            _context = context;
        }
        

        // POST: api/Logins
        [HttpPost]
        public IActionResult Post(LoginVM login)
        {
            try
            {
                var userExist = _context.Users.SingleOrDefault(u => u.UserName == login.UserName);

                if (BCrypt.Net.BCrypt.Verify(login.Password , userExist.PasswordHash))
                {
                    var role = _context.Roles.Where(r => r.UserRoles.Any(ur => ur.UserId == userExist.Id)).Select(r => r.Name).ToArray();
                    var name = _context.Biodatas.Single(b => b.Id == userExist.Id);
                    LoginConfirmation loginConfirmation = new LoginConfirmation
                    {
                        Id = userExist.Id,
                        UserName = userExist.UserName,
                        Email = userExist.Email,
                        Role = role,
                        FirstName = name.FirstName,
                        LastName = name.LastName,
                        Phone = userExist.PhoneNumber
                    };
                    return Ok(loginConfirmation);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }
            }
            
            catch (Exception)
            {
                return NotFound("Username does not exist");
            }
            
        }

        // PUT: api/Logins/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
