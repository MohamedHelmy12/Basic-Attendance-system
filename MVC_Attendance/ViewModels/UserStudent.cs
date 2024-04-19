using MVC_Attendance.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_Attendance.ViewModels
{
	public class UserStudent
	{
		public int Id { get; set; }

		public string FirstName { get; set; }

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
		public string? Address { get; set; }
		// Role Property is an Enum 

		public Role Role { get; set; }
		public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
		public int Presentdayes { get; set; }
		public int Abcencedayes { get; set; }
		public float AbsenceDegree { get; set; }


	}
}
