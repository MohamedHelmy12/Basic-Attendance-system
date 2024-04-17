using MVC_Attendance.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_Attendance.ViewModels
{
    public class UserRegisterModelView
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(50)]
        [Required]
        public string? Fname { get; set; }

        [StringLength(50)]
        [Required]
        public string? Lname { get; set; }


        [DataType(DataType.Password)]
        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }

        public string? UniversityID { get; set; }
        public string? Faculty { get; set; }
        public string? Specialization { get; set; }
        public int? GraduationYear { get; set; }
        public EmployeeType? EmployeeType { get; set; }

    }
}
