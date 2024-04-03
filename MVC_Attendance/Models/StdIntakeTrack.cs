using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Attendance.Models
{
    public class StdIntakeTrack
    {
        public int TrackId { get; set; }

        [ForeignKey(nameof(TrackId))]
        public virtual Track Track { get; set; }
        public int IntakeId { get; set; }

        [ForeignKey(nameof(IntakeId))]
        public virtual Intake Intake { get; set; }
        public int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set;}
    }
}
