using System.Data.Entity;
using UserManagment.DAL.Configuration;
using UserManagment.DAL.Initializers;
using UserManagment.Domain.Models;

namespace UserManagment.DAL.Contexts
{
    public class UserStoryContext : DbContext
    {
        public UserStoryContext() : base("UserStoryContext")
        {
            Database.SetInitializer(new UserDbInitializer());
        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserStoryConfig().ConfigureDb(modelBuilder);
        }
    }
}
