using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using NETCORE.Contexts;
using NETCORE.Models;
using NETCORE.Repositories.IRepositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NETCORE.Repositories
{
    public class UsersRepository : IUsersRepository
    {

        DynamicParameters parameters = new DynamicParameters();
        private readonly IConfiguration _configuration;
        private readonly MyContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UsersRepository(IConfiguration configuration, MyContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public async Task<int> Create(RegisterViewModel userView)
        {
            User user = new User();
            Biodata biodata = new Biodata();

            try
            {
                user.UserName = userView.Id;
                user.Email = userView.Email;
                user.NormalizedUserName = user.UserName.ToUpper();
                user.NormalizedEmail = user.Email.ToUpper();
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userView.Password);
                user.PhoneNumber = userView.PhoneNumber;
                user.LockoutEnabled = true;
                user.EmailConfirmed = true;
                var create = await _context.Users.AddAsync(user);
                
                if (create.IsKeySet)
                {
                //var exist = await _roleManager.RoleExistsAsync("Employee");
                    UserRole userRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId =  _roleManager.FindByNameAsync("Sales").Result.Id
                    };

                    _context.UserRoles.Add(userRole);

                    biodata.FirstName = userView.FirstName;
                    biodata.LastName = userView.Lastname;
                    biodata.Address = userView.Address;
                    biodata.CreatedAt = DateTimeOffset.Now;
                    
                    _context.Biodatas.Add(biodata);
                    _context.SaveChanges();
                    return 1;

                }
                else
            {
                return 0;
            }
                

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
                var deleteUser = _context.Users.Single(u => u.UserName == id);
                var delete = _context.Biodatas.Single(b => b.Id == deleteUser.Id);
                delete.IsDelete = true;
                _context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<ProfileViewModel>> Get()
        {
            try
            {
                var getAll = await _context.Users
                    .Include(b => b.Biodata)
                    .Include(ur => ur.UserRoles)
                    .ThenInclude(r => r.Role)
                    .Where(u => u.Biodata.IsDelete == false)
                    .ToListAsync();

                List<ProfileViewModel> profileList = new List<ProfileViewModel>();

                foreach (var get in getAll)
                {
                    ProfileViewModel profile = new ProfileViewModel
                    {
                        UserName = get.UserName,
                        FirstName = get.Biodata.FirstName,
                        Lastname = get.Biodata.LastName,
                        Address = get.Biodata.Address,
                        Email = get.Email,
                        PhoneNumber = get.PhoneNumber,
                        BirthDate = get.Biodata.BirthDate,
                        University = get.Biodata.University,
                        Skill = get.Biodata.Skills,
                        Title = get.Biodata.Title,
                        CreatedDate = get.Biodata.CreatedAt,
                        UpdatedDate = get.Biodata.UpdatedAt,
                        Role = _context.Roles.Where(r => r.UserRoles.Any(ur => ur.UserId == get.Id)).Select(r => r.Name).ToArray()
                    };

                    profileList.Add(profile);
                }

                return profileList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ProfileViewModel> Get(string id)
        {
            var get = await _context.Users
                    .Include(b => b.Biodata)
                    .Include(ur => ur.UserRoles)
                    .ThenInclude(r => r.Role)
                    .Where(u => u.Biodata.IsDelete == false)
                    .SingleOrDefaultAsync(u => u.UserName == id);

            ProfileViewModel profile = new ProfileViewModel()
            {
                UserName = get.UserName,
                FirstName = get.Biodata.FirstName,
                Lastname = get.Biodata.LastName,
                Address = get.Biodata.Address,
                Email = get.Email,
                PhoneNumber = get.PhoneNumber,
                BirthDate = get.Biodata.BirthDate,
                University = get.Biodata.University,
                Skill = get.Biodata.Skills,
                Title = get.Biodata.Title,
                CreatedDate = get.Biodata.CreatedAt,
                UpdatedDate = get.Biodata.UpdatedAt,
                Role = _context.Roles.Where(r => r.UserRoles.Any(ur => ur.UserId == get.Id)).Select(r => r.Name).ToArray()
            };

            return profile;

        }

        public int Update(string id, RegisterViewModel userView)
        {
            User user = _context.Users.Single(u => u.UserName == id);
            Biodata biodata = _context.Biodatas.Find(user.Id);
            try
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userView.Password);
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
