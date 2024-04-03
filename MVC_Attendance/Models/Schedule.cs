using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Attendance.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public TimeOnly StartDate { get; set; }
        public int TrackId { get; set; }

        [ForeignKey(nameof(TrackId))]
        public Track Track { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
