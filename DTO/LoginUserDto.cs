using System.ComponentModel.DataAnnotations;

namespace Sakan_project.DTO
{
    public class LoginUserDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

