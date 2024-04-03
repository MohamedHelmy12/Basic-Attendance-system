using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Attendance.Models
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
        //public int StudentID { get; }
        public string UniversityID { get; set; }
        public string Faculty { get; set; }
        public string Specialization { get; set; }
        public int GraduationYear { get; set; }
        public double AbsenceDegree { get; set; }
        public int NumberOfAbsences { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    }
}
