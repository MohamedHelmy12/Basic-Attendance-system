using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Attendance.Models
{
    public class IntakesTracks
    {
        public int IntakeId { get; set; }

        [ForeignKey(nameof(IntakeId))]
        public virtual Intake Intake { get; set; }
        public int TrackId { get; set; }

        [ForeignKey(nameof(TrackId))]
        public virtual Track Track { get; set;}

        public string Status { get; set; } = "Active";
    }
}
