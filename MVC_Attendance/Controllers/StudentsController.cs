using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Attendance.Models;

namespace MVC_Attendance.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AttDbContext _context;

        public StudentsController(AttDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Password,Phone,Address,role,UniversityID,Faculty,Specialization,GraduationYear,AbsenceDegree,NumberOfAbsences")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UniversityID,Faculty,Specialization,GraduationYear,AbsenceDegree,NumberOfAbsences,Id,FirstName,LastName,Email,Password,Phone,Address,role")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        ////  Design and implement views for request permissions and view permissions
        //// GET: Students/RequestPermissions/5
        //public async Task<IActionResult> RequestPermissions(int id)
        //{
        //    var students = await _context.Students.ToListAsync();
        //    // find student permissions by student id
        //    var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }
        //    var permission = await _context.Permissions.Where(p => p.StudentId == id).ToListAsync();
        //    // send student as a viewbag
        //    ViewBag.student = student;
        //    return View();
        //}

        //// POST: Students/RequestPermissions
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> RequestPermissions([Bind("Id,FirstName,LastName,Email,Password,Phone,Address,role,UniversityID,Faculty,Specialization,GraduationYear,AbsenceDegree,NumberOfAbsences")] Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(student);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(student);
        //}

        //// GET: Students/ViewPermissions
        //public async Task<IActionResult> ViewPermissions()
        //{
        //    return View(await _context.Students.ToListAsync());
        //}

        //// GET: Students/ViewPermissions/5
        //public async Task<IActionResult> ViewPermissions(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var student = await _context.Students
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(student);
        //}

        //// POST: Students/ViewPermissions
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ViewPermissions([Bind("UniversityID,Faculty,Specialization,GraduationYear,AbsenceDegree,NumberOfAbsences,Id,FirstName,LastName,Email,Password,Phone,Address,role")] Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(student);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(student);
        //}
    }
}
