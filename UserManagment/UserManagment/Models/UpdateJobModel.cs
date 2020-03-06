using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserManagment.Models
{
    public class UpdateJobModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("User")]
        public int UserId { get; set; }
    }
}