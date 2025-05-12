using System.ComponentModel.DataAnnotations;

namespace Sakan_project.DTO
{
    public class RegisterUserDto
    {

        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
       ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string Confirm_Password { get; set; }

        public string Role { get; set; } 

    }
}
