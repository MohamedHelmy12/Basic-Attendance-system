using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Attendance.Models
{
    public class Attendance
    {
      
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly AttendanceTime { get; set; }

        public TimeOnly LeavingTime { get; set; }

        public int ScheduleId { get; set; }

        [ForeignKey(nameof(ScheduleId))]
        public Schedule Schedule { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
