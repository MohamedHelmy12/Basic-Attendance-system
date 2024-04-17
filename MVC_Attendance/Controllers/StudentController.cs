using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVC_Attendance.IRepository;
using MVC_Attendance.Models;

namespace MVC_Attendance.Controllers
{
    public class StudentController : Controller
    {
        private readonly AttDbContext db;
        private readonly IStudentRepository studentRepository;
        public StudentController(AttDbContext _db, IStudentRepository _studentRepository)
        {
            db = _db;
            studentRepository = _studentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show()
        {

            var studentViewModels = new List<StudentViewModel>();
            var students = db.Students.ToList();
            foreach (var student in students)
            {
                var StdIntakeTrack = db.StdIntakeTrack.FirstOrDefault(t => t.StudentId == student.Id);
                var studentViewModel = new StudentViewModel
                {
                    Student = student,
                    StdIntakeTrack = StdIntakeTrack
                };
                studentViewModels.Add(studentViewModel);
            }

            return View(studentViewModels);
        }
        [HttpGet]
        public IActionResult Create()
        {
            // Initialize a new StudentViewModel and pass it to the view
            ViewBag.Tracks = db.Tracks.ToList();
            ViewBag.intakes= db.Intakes.ToList();
            var viewModel = new StudentViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(StudentViewModel viewModel)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return View(viewModel); // Return the view with validation errors
            }

            // Extract the student object from the view model
            var student = viewModel.Student;

            // Add the student to the database
            studentRepository.AddStudent(student);

            // Redirect to the "Show" action
            return RedirectToAction("Show");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = studentRepository.GetStudentById(id);
            var viewModel = new StudentViewModel { Student = student };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(StudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel); // Return the view with validation errors
            }

            // Map properties from the view model to the student entity
            var student = new Student
            {
                Id = viewModel.Student.Id,
                UniversityID = viewModel.Student.UniversityID,
                Faculty = viewModel.Student.Faculty,
                Specialization = viewModel.Student.Specialization,
                GraduationYear = viewModel.Student.GraduationYear,
                FirstName = viewModel.Student.FirstName,
                LastName = viewModel.Student.LastName,
                Email = viewModel.Student.Email,
                Password = viewModel.Student.Password,
                Phone = viewModel.Student.Phone,
                Address = viewModel.Student.Address,
            };

            // Update the student using the repository
            studentRepository.UpdateStudent(student);

            return RedirectToAction("Show");
        }


        public IActionResult Delete(int id)
        {
            studentRepository.DeleteStudent(id);
            return RedirectToAction("Show");
        }

    }
}
