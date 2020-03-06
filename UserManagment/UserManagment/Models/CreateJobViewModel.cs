using System.ComponentModel.DataAnnotations;

namespace UserManagment.Models
{
    public class CreateJobViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}