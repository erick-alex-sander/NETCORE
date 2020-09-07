using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NETCORE.Contexts;
using NETCORE.ViewModel;

namespace NETCORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly MyContext _context;
        public IConfiguration _configuration;

        public LoginsController(MyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", loginConfirmation.Id),
                        new Claim("UserName", loginConfirmation.UserName),
                        new Claim("Email", loginConfirmation.Email),
                        new Claim("Role", loginConfirmation.Role[0]),
                        new Claim("FirstName", loginConfirmation.FirstName),
                        new Claim("LastName", loginConfirmation.LastName),
                        new Claim("Phone", loginConfirmation.Phone)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"]
                        , claims, expires: DateTime.UtcNow.AddSeconds(30), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
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

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

    }
}
