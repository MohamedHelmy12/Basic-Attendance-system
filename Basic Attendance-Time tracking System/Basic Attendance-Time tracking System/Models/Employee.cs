namespace Basic_Attendance_Time_tracking_System.Models
{
    // Enum for EmployeeType
    public enum EmployeeType
    {
        StudentAffairs,
        Security
    }
    /// <summary>
    /// Employee Model
    /// Employee Class inherits from User class
    /// Employee class has the following properties:
    /// - EmployeeID: int (Primary Key) (Identity) (Required) (Unique) (Auto Generated) (Read Only)
    /// - EmployeeType: string
    /// - Salary: double
    /// - HireDate: DateTime
    /// </summary>
    public class Employee
    {
        // Properties
        public int EmployeeID { get; }
        public EmployeeType EmployeeType { get; set; }
        public double Salary { get; set; }
        public DateTime HireDate { get; set; }

        // Properties(Optional)
        
        // Foreign Key


        // Navigation Properties


    }
}
