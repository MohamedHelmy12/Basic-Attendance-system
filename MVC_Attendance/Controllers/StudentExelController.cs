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
                    FirstName = s.Name, // Assuming 'Name' in Excel corresponds to 'FirstName' in Student
                    Password = s.Password,
                    Email = s.Email,
                    Phone = s.Mobile, // Assuming 'Mobile' in Excel corresponds to 'Phone' in User
                    Faculty = s.Faculty,
                    GraduationYear = s.GraduationYear,
                    AbsenceDegree = Convert.ToInt32(s.StudentDegree), // Assuming 'StudentDegree' in Excel corresponds to 'StudentDegree' in Student
                    UniversityID = s.University // Assuming 'University' in Excel corresponds to 'UniversityID' in Student
                }).ToList();

                // Assuming 'studentRepo' is the repository for Student entities
                studentRepository.AddRangeOfStudents(students);

                // Redirect to the Index action of the Student controller
                return RedirectToAction("Show", "Student");



            }
            catch
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
