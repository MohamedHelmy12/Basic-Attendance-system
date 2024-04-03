using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Attendance.Models
{
    public enum Role
    {
        Student,
        Instructor,
        Employee,
        Admin
    }
    public abstract class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string LastName { get; set; }

        // Adding Regular Expression to validate the email
        // [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        // Adding Regular Expression to validate the password
        // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one digit and one special character")]
        public string Password { get; set; }
        // Adding Regular Expression to validate the phone number
        // [RegularExpression(@"^01[0-2]{1}[0-9]{8}$", ErrorMessage = "Please enter a valid phone number")]
        public string Phone { get; set; }
        // Properties(Optional)
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Address { get; set; }
        // Role Property is an Enum 

        [EnumDataType(typeof(Role))]
        public Role role { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
