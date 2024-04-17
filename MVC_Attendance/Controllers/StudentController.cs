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
            db.Students.Add(student);
            db.SaveChanges();

            // Redirect to the "Show" action
            return RedirectToAction("Show");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = db.Students.FirstOrDefault(std => std.Id == id);
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

            var student = viewModel.Student;

            db.Students.Update(student);
            db.SaveChanges();

            return RedirectToAction("Show");
        }

        public IActionResult Delete(int id)
        {
            var student = db.Students.FirstOrDefault(i => i.Id == id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Show");
        }




    }
}
