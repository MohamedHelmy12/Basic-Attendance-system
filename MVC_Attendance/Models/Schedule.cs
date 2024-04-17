using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Attendance.Models
{
    public class Schedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public TimeOnly StartPeriod { get; set; }
        public DateOnly Date {  get; set; }
        public int TrackId { get; set; }

        [ForeignKey(nameof(TrackId))]
        public Track Track { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
