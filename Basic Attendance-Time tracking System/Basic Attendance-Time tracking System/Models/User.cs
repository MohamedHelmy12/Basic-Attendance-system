using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basic_Attendance_Time_tracking_System.Models
{
    public enum Role
    {
        Student,
        Instructor,
        Employee
    }
    /// <summary>
    /// User Model
    /// User class is an abstract class, classes Student and Instructor Employee inherit from it.
    /// that has the following properties:
    /// - ID: int (Primary Key) (Identity) (Required) (Unique) (Auto Generated) (Read Only)
    /// - FirstName: string
    /// - LastName: string
    /// - Email: string
    /// - Password: string
    /// - Phone: string
    /// - Address: string
    /// - Role: string
    /// </summary>
    public abstract class User
    {
        // Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID { get; }
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

    }
}
