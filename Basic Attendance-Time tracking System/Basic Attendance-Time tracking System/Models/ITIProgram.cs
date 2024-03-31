using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basic_Attendance_Time_tracking_System.Models
{
    /// <summary>
    /// ITI Program Model
    /// has the following properties:
    /// - ID: int (Primary Key) (Identity) (Required) (Unique) (Auto Generated) (Read Only), (Foreign Key for Intake class)
    /// - Name: string
    /// - Description: string
    /// 
    /// </summary>
    public class ITIProgram
    {
        // Properties 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int ID { get; }
        public string Name { get; set; }

        // Properties(Optional)
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Description { get; set; } = "This Program is on of ITI Programs";

        // Foreign Key



        

        // Navigation Properties



    }
}
