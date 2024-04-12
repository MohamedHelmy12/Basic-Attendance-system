using Microsoft.AspNetCore.Mvc;
using MVC_Attendance.Models;

namespace MVC_Attendance.Controllers
{
    public class InstructorController : Controller
    {
        AttDbContext db = new AttDbContext();
        public IActionResult Index()
        {
            var instructors=db.Instructors.ToList();
            return View(instructors);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Instructor instructor)
        {
            db.Instructors.Add(instructor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var instructor=db.Instructors.FirstOrDefault(x => x.Id == id);
            return View(instructor);
        }
        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            db.Instructors.Update(instructor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var instructor = db.Instructors.FirstOrDefault(x => x.Id == id);
            db.Remove(instructor);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        
    }
}
