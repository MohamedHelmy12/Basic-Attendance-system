using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVC_Attendance.Models;

namespace MVC_Attendance.Controllers
{
    public class StudentController : Controller
    {
        private readonly AttDbContext db;
        public StudentController(AttDbContext context)
        {
            db = context;

		}
        // AttDbContext db =new AttDbContext(new DbContextOptions<AttDbContext>());
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show()
        {
            var students= db.Students.ToList();
            return View(students);
        }
        [HttpGet]
        public IActionResult Create( Student std)
        {

            //db.Students.Add(std);
            //db.SaveChanges();
           return View(new Student());
        }
        [HttpPost]  
        public IActionResult create(Student std)
        {
            db.Students.Add(std);   
            db.SaveChanges();
            return RedirectToAction("Show");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = db.Students.FirstOrDefault(std => std.Id == id);
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student std)
        {
            db.Students.Update(std);
            db.SaveChanges();
            return RedirectToAction("show");
        }
        public IActionResult Delete(int id)
        {
            var std = db.Students.FirstOrDefault(i => i.Id == id);
            db.Students.Remove(std);

            db.SaveChanges();

            return RedirectToAction("show");
        }


    }
}
