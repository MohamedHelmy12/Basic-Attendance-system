using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Attendance.Models
{
    public class Permission
    {
        public int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; }
        public DateOnly date {  get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
