using System.ComponentModel.DataAnnotations.Schema;
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

            modelBuilder.Entity<Job>()
                .HasRequired(s => s.User)
                .WithMany(g => g.Jobs)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<User>().Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Job>().Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Role>().Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Job>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().HasKey(x => x.Id);
        }
    }
}
