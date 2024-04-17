using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Attendance.Models
{
    public class Attendance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly? AttendanceTime { get; set; } = null;

        public TimeOnly? LeavingTime { get; set; } = null;

        public int ScheduleId { get; set; }

        [ValidateNever]

        [ForeignKey(nameof(ScheduleId))]
        public Schedule Schedule { get; set; }
        public int UserId { get; set; }
        [ValidateNever] 
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
