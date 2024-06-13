using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace PriceComparing.Repository
{
    public class UserRepoNonGenric
    {
        private readonly DatabaseContext _db;

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
    }
}