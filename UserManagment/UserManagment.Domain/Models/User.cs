using System.Collections.Generic;

namespace UserManagment.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }
}