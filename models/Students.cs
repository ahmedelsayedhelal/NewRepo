using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakan_project.models
{
    public class Students
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
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        public int Passwordhash { get; set; }

        public int Phonenumber { get; set; }


        public string Gender { get; set; }

        [ForeignKey("College")]
        public int Collegueid { get; set; }

        [ForeignKey("University")]

        public int Universtyid { get; set; }

        
    public virtual Colleges College { get; set; }
    public virtual Universities University { get; set; }
    public virtual ICollection<Studentbooking> StudentsBookings { get; set; }


}
}
