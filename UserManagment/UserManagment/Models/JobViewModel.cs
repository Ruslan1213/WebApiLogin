using UserManagment.Domain.Models;

namespace UserManagment.Models
{
    public class JobViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}