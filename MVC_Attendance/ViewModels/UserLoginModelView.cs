using System.ComponentModel.DataAnnotations;

namespace MVC_Attendance.ViewModels
{
    public class UserLoginModelView
    {
        [Required]
        [EmailAddress]
        [StringLength(50, MinimumLength = 3)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
