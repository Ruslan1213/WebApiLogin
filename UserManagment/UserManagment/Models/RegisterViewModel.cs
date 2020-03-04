using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserManagment.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "The Email is required")]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Name is required")]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The Password is required")]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 5)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Invalid Confirm Password field is required")]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 5)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "The RoleId is required")]
        public int RoleId { get; set; }
    }
}