

using System.ComponentModel.DataAnnotations;

namespace Sakan_project.models
{
    public class Owners
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Firstname { get; set; }

        [MaxLength(50)]
        public string Lastname { get; set; }
        [Required]
        [EmailAddress]
       
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
       ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]

        public int Passwordhash { get; set; }
        [Phone]
        public int Phonenumber { get; set; }
        public virtual ICollection<Apartments> Apartments { get; set; }
    }


}

