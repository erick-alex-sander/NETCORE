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

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the server key used to sign the JWT token is here, use more than 16 chars")),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        //[HttpPost]
        //public IActionResult Refresh(string token, string refreshToken)
        //{
        //    var principal = GetPrincipalFromExpiredToken(token);
        //    var username = principal.Identity.Name;
        //    var savedRefreshToken = GetRefreshToken(username); //retrieve the refresh token from a data store
        //    if (savedRefreshToken != refreshToken)
        //        throw new SecurityTokenException("Invalid refresh token");

        //    var newJwtToken = GenerateToken(principal.Claims);
        //    var newRefreshToken = GenerateRefreshToken();
        //    DeleteRefreshToken(username, refreshToken);
        //    SaveRefreshToken(username, newRefreshToken);

        //    return new ObjectResult(new
        //    {
        //        token = newJwtToken,
        //        refreshToken = newRefreshToken
        //    });
        //}
    }
}
