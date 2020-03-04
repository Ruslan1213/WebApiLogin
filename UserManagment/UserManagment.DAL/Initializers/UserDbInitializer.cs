using System.Collections.Generic;
using UserManagment.DAL.Contexts;
using UserManagment.DAL.Extensions;
using UserManagment.Domain.Models;

namespace UserManagment.DAL.Initializers
{
    public class UserDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<UserStoryContext>
    {
        protected override void Seed(UserStoryContext context)
        {
            var roles = new List<Role>
            {
                new Role { Id = 1, Name = "User" },
                new Role { Id = 2, Name = "Manager" },
                new Role { Id = 3, Name = "Admin" }
            };

            roles.ForEach(x => context.Roles.Add(x));
            context.SaveChanges();

            var users = new List<User>
            {
                new User { Id = 1, Name = "Aleksandr", Email="alexandr.@gmail.com", Password = "aasdas".GetHashString(), RoleId = 1 },
                new User { Id = 2, Name = "Vasiliy", Email ="Vasiliy.@gmail.com", Password = "qwerty".GetHashString(), RoleId = 2  },
                new User { Id = 3, Name = "Ivan", Email ="Ivan.@gmail.com", Password = "123456".GetHashString(), RoleId = 3 }
            };

            users.ForEach(x => context.Users.Add(x));
            context.SaveChanges();
        }
    }
}
