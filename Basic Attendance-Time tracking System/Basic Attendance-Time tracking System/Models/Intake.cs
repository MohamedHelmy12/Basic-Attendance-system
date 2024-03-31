using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Basic_Attendance_Time_tracking_System.Models
{
    /// <summary>
    /// Intake Model
    /// Intake class has the following properties:
    /// - ID: int (Primary Key) (Identity) (Required) (Unique) (Auto Generated) (Read Only)
    /// - Number : int
    /// - Description: string
    /// - StartDate: DateTime
    /// - EndDate: DateTime
    /// - Capacity: int
    /// - ITIProgramID: int (Foreign Key for ITIProgram class)
    /// </summary>
    public class Intake
    {
        // Properties 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID { get; }

        [Range(1, 1000)]
        public int Number { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        // [DateGreaterThan("StartDate",ErrorMessage ="The End Date must be greater than the Start Date")]
        public DateTime EndDate { get; set; }


        // Properties(Optional)        

        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Description { get; set; }

        [Range(1, 1000)]
        public int Capacity { get; set; }

        // Foreign Key

        // Navigation Properties


    }
}
