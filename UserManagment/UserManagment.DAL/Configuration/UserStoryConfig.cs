using System.Data.Entity;
using UserManagment.Domain.Models;

namespace UserManagment.DAL.Configuration
{
    internal class UserStoryConfig
    {
        public void ConfigureDb(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasRequired(s => s.Role)
                .WithMany(g => g.Users)
                .HasForeignKey(s => s.RoleId);
        }
    }
}
