namespace Basic_Attendance_Time_tracking_System.Models
{
    /// <summary>
    /// Student Model
    /// Student Class inherits from User class
    /// Student class has the following properties:
    /// - StudentID: int (Primary Key) (Identity) (Required) (Unique) (Auto Generated) (Read Only)
    /// - UniversityID: string
    /// - Faculty: string
    /// - Specialization: string
    /// - GraduationYear: int
    /// - Grade: double
    /// - NumberOfAbsences: int
    /// </summary>
    public class Student : User
    {
        // Properties 
        public int StudentID { get; }
        public string UniversityID { get; set; }
        public string Faculty { get; set; }
        public string Specialization { get; set; }
        public int GraduationYear { get; set; }
        public double Grade { get; set; }
        public int NumberOfAbsences { get; set; }

        // Properties(Optional)

        // Foreign Key

        // Navigation Properties

    
    }
}
