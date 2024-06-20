using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace PriceComparing.Repository
{
    public class UserRepoNonGenric
    {
        private readonly DatabaseContext _db;
        private readonly UserManager<AuthUser> userManager;

        public UserRepoNonGenric(DatabaseContext db, UserManager<AuthUser> _userManager)
        {
            _db = db;
            userManager = _userManager;
        }

        public UserRepoNonGenric(DatabaseContext db)
        {
            _db = db;
        }

        public List<AuthUser> GetOnlyUser()
        {
            var users= _db.Users.ToList();
            var UserRoles =_db.UserRoles.ToList();
            var Roles =_db.Roles.ToList();
            List<AuthUser> Users = new List<AuthUser>();
            foreach(var user in users)
            {
                foreach(var userRole in UserRoles)
                {
                    if(userRole.UserId==user.Id &&
                        Roles.FirstOrDefault(a => a.Name == "User").Id == userRole.RoleId)
                    {
                        Users.Add(user);
                    }
                }
            }
            return Users;
        }


        public List<AuthUser> GetOnlyAdmin()
        {
            var users = _db.Users.ToList();
            var UserRoles = _db.UserRoles.ToList();
            var Roles = _db.Roles.ToList();
            List<AuthUser> Users = new List<AuthUser>();
            foreach (var user in users)
            {
                foreach (var userRole in UserRoles)
                {
                    if (userRole.UserId == user.Id &&
                        Roles.FirstOrDefault(a => a.Name == "Admin").Id == userRole.RoleId)
                    {
                        Users.Add(user);
                    }
                }
            }
            return Users;
        }


        public async  Task <AuthUser> GetById(string id)
        {
            var user = await _db.Users.FindAsync(id);
            return user;
        }

        public async Task UpdateUserAsync(AuthUser user, UpdateUserDTO UpdatedUser)
        {
            user.UserName = UpdatedUser.UserName;
            user.Email= UpdatedUser.Email;
            user.FName = UpdatedUser.FirstName;
            user.LName = UpdatedUser.LastName;
            user.Country =UpdatedUser.Country;
            user.PhoneNumber = UpdatedUser.PhoneNumber;
            user.PhoneCode=UpdatedUser.PhoneCode;
            user.Gender = UpdatedUser.Gender; 
          
            await _db.SaveChangesAsync();
          
       
        }
    }
}