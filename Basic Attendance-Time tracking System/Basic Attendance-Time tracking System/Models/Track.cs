using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basic_Attendance_Time_tracking_System.Models
{
    /// <summary>
    /// Track Model
    /// Track Class has the following properties:
    /// - ID: int (Primary Key) (Identity) (Required) (Unique) (Auto Generated) (Read Only)
    /// - Name: string
    /// - IsActive: bool
    /// - Description: string
    /// - Capacity: int
    /// </summary>
    public class Track
    {
        // Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; }
        [Length(minimumLength: 3, maximumLength: 100)]

        public string Name { get; set; }
        public bool IsActive { get; set; }

        // Properties(Optional)
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Description { get; set; }
        [Range(1, 1000)]
        public int Capacity { get; set; }

        // Foreign Key

        // Navigation Properties

    }
}
