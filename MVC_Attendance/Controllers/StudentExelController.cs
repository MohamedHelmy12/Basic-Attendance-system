using Ganss.Excel;
using Microsoft.AspNetCore.Mvc;
using MVC_Attendance.IRepository;
using MVC_Attendance.Models;
using OfficeOpenXml;
using System.ComponentModel;


namespace MVC_Attendance.Controllers
{
    public class StudentExelController : Controller
    {


        private readonly AttDbContext db;
        private readonly IStudentRepository studentRepository;
        public StudentExelController(AttDbContext _db, IStudentRepository _studentRepository)
        {
            db = _db;
            studentRepository = _studentRepository;
        }


        [HttpGet]
        public IActionResult AddBulk()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddBulk(IFormFile Sheet)
        {
            using (var fs = new FileStream("wwwroot\\Sheets\\" + Sheet.FileName, FileMode.OpenOrCreate))
            {
                Sheet.CopyTo(fs);

            }


            try
            {
                var students = new ExcelMapper("wwwroot\\Sheets\\" + Sheet.FileName).Fetch().Select(s => new Student
                {
                    Specialization = s.Specialization,
                    FirstName = s.FirstName, 
                    LastName = s.LastName,// Assuming 'Name' in Excel corresponds to 'FirstName' in Student
                    Password = s.Password.ToString(),
                    Email = s.Email,
                    Phone = s.Phone.ToString(), // Assuming 'Mobile' in Excel corresponds to 'Phone' in User
                    Faculty = s.Faculty,
                    GraduationYear = Convert.ToInt32(s.GraduationYear),
                    AbsenceDegree = Convert.ToDouble(s.AbsenceDegree), // Assuming 'StudentDegree' in Excel corresponds to 'StudentDegree' in Student
                    UniversityID = s.UniversityID,   // Assuming 'University' in Excel corresponds to 'UniversityID' in Student
                    NumberOfAbsences = Convert.ToInt32(s.NumberOfAbsences),
                    Address = s.Address,
                    Role = 0,
                   
                }).ToList();

                // Assuming 'studentRepo' is the repository for Student entities
                studentRepository.AddRangeOfStudents(students);

                // Redirect to the Index action of the Student controller
                return RedirectToAction("Show", "Student");



            }
            catch (Exception ex) { }
            {


                ViewBag.reasons = new List<string>
                {
                    "Duplicate Name",
                    "Invalid Data",
                    "Invalid file format"
                };
                return View("AdminError");
            }

        }






    }
}
