using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Attendance.Models
{
    public class Student : User
    {
        [Required(ErrorMessage = "University ID is required.")]
        public string UniversityID { get; set; }

        [Required(ErrorMessage = "Faculty is required.")]
        public string Faculty { get; set; }

        [Required(ErrorMessage = "Specialization is required.")]
        public string Specialization { get; set; }

        [Display(Name = "Graduation Year")]
        [Range(1900, int.MaxValue, ErrorMessage = "Please enter a valid graduation year.")]
        public int? GraduationYear { get; set; }

        [Display(Name = "Absence Degree")]
        [Range(0, 100, ErrorMessage = "Please enter a value between 0 and 100.")]
        public double AbsenceDegree { get; set; }

        [Display(Name = "Number of Absences")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number of absences.")]
        public int NumberOfAbsences { get; set; }

        [NotMapped] // This property is not mapped to a database column
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
