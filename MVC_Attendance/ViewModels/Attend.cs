using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVC_Attendance.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Attendance.ViewModels
{
	public class Attend
	{
		public int Id { get; set; }

		public DateOnly Date { get; set; }

		public TimeOnly? AttendanceTime { get; set; } = null;

		public TimeOnly? LeavingTime { get; set; } = null;

		public int ScheduleId { get; set; }
        public string Status { get; set; }
		public Schedule Schedule { get; set; }
		public int UserId { get; set; }
		public virtual User User { get; set; }
	}
}
