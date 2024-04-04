using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Attendance.Models
{
    public enum PermissionType
    {
        Absence,
        Late
    }
    public enum PermissionStatus
    {
        Pending,
        Approved,
        Rejected
    }
    public class Permission
    {
        // Properties
        public int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; }
        public DateOnly date {  get; set; }
        public PermissionType Type { get; set; } = PermissionType.Late;
        public PermissionStatus Status { get; set; } = PermissionStatus.Pending;

        // Properties (Optional)
        public string? Reason { get; set; }
    }
}
