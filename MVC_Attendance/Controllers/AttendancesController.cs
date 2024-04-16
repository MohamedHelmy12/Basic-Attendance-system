using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Attendance.IRepository;
using MVC_Attendance.Models;

namespace MVC_Attendance.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly AttDbContext _context;

        public AttendancesController(AttDbContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> Index()
        {
            var attDbContext = _context.Attendances.Include(a => a.Schedule).Include(a => a.User);
            return View(await attDbContext.ToListAsync());
        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Schedule)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // Create Attendance
        public IActionResult Create()
        {
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Id");
            //         ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName" + "LastName");
            //return View();
            // Use LINQ to project the users to a new anonymous type with Id and FullName
            ViewData["UserId"] = new SelectList(_context.Users.Select(u => new {
                Id = u.Id,
                FullName = u.Id + ":" + u.FirstName + " " + u.LastName // Concatenate FirstName and LastName with a space
            }), "Id", "FullName");
            return View();
        }

        // POST: Attendances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,AttendanceTime,LeavingTime,ScheduleId,UserId")] Attendance attendance)
        {
			//if (ModelState.IsValid)
            //{
				_context.Add(attendance);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			//}
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Id", attendance.ScheduleId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", attendance.UserId);
			return View(attendance);
		}

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Id", attendance.ScheduleId);
            ViewData["UserId"] = new SelectList(_context.Users.Select(u => new {
                Id = u.Id,
                FullName = u.Id + ":" + u.FirstName + " " + u.LastName // Concatenate FirstName and LastName with a space
            }), "Id", "FullName");
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,AttendanceTime,LeavingTime,ScheduleId,UserId")] Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.Id))
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
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Id", attendance.ScheduleId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", attendance.UserId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Schedule)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.Id == id);
        }


		// _ With Repo _ //
		//private IAttendanceRepository attendanceRepository;
        //public AttendancesController(IAttendanceRepository _attendanceRepository)
        //{
		//	attendanceRepository = _attendanceRepository;
		//}
        // GET: Attendances
        //public IActionResult Index()
        //{
        //    return View(attendanceRepository.GetAllAttendances());
        //}
        // GET: Attendances/Create
        //public IActionResult Create()
        //{
        //    ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Id");
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
		//	return View();
		//}
	}   

}