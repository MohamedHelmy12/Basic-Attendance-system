using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Attendance.Models
{
    public class Supervise
    {
        public int TrackId { get; set; }

        [ForeignKey(nameof(TrackId))]
        public virtual Track Track { get; set; }
        public int IntakeId { get; set; }

        [ForeignKey(nameof(IntakeId))]
        public virtual Intake Intake { get; set; }

        public int InstructorId { get; set; }

        [ForeignKey(nameof(InstructorId))]
        public virtual Instructor Instructor { get; set; }
    }
}
