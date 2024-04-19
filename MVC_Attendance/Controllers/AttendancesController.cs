using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Attendance.Models;
using MVC_Attendance.ViewModels;

namespace MVC_Attendance.Controllers
{
	[Authorize(Roles = "Student,Instructor,Employee,Admin")]
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
			var currentDate = DateOnly.FromDateTime(DateTime.Now);
			ViewBag.TodayDate = currentDate;
			var myRole = User.FindFirst(ClaimTypes.Role).Value;
			ViewBag.Role = myRole;
			var me = User.FindFirst(ClaimTypes.Email);
			var myId = _context.Users.FirstOrDefault(u => u.Email == me.Value).Id;
			var attDbContext = _context.Attendances.Include(a => a.Schedule).Include(a => a.User).Select(a => new Attend()
			{
				Id = a.Id,
				Date = a.Date,
				Schedule = a.Schedule,
				UserId = a.UserId,
				AttendanceTime = a.AttendanceTime,
				LeavingTime = a.LeavingTime,
				User = a.User, // for first and last name
							   // if attendance date > now && attendance time is null then status = absent 
							   // else if attendance date > now && attendance time is not null then status = present
							   // else if attendance date < now then status = (not yet)
				Status = a.Schedule.Date < currentDate
				 ? (a.AttendanceTime == null ? "Absent" : "Present")
				 : "Comming",
				// calcualte the NoofAbcencedayes

				// calcualte the AbsenceDegree

			}).ToList();
			// send the view bag to the UserStdent to count number of absences and the degree of absence
			var ourStuds = _context.Users
				.Include(u => u.Attendances).Where(u => u.Role == Role.Student)
				.ToList() // Execute query and load data into memory
				.OrderBy(u => u.Attendances.FirstOrDefault().Schedule.Date)
				.Select(u => new UserStudent
				{
					Id = u.Id,
					FirstName = u.FirstName,
					LastName = u.LastName,
					Phone = u.Phone,
					Address = u.Address,
					Role = u.Role,
					Attendances = u.Attendances,
					Presentdayes = u.Attendances.Count(a => a.AttendanceTime != null && a.Schedule.Date < DateOnly.FromDateTime(DateTime.Now)),
					Abcencedayes = u.Attendances.Count(a => a.AttendanceTime == null && a.Schedule.Date < DateOnly.FromDateTime(DateTime.Now)),
					AbsenceDegree = CalculateAbsenceDegree(u.Attendances.Count(a => a.AttendanceTime == null && a.Schedule.Date < DateOnly.FromDateTime(DateTime.Now)))
				})
				.ToList();
			if (myRole != "Admin")
			{
				attDbContext = attDbContext.Where(a => a.UserId == myId).ToList();
				ViewBag.ourStuds = ourStuds.FirstOrDefault(u => u.Id == myId);
				// View(attDbContext);
			}
			ViewBag.ourStuds = ourStuds;
			return View(attDbContext);
		}

		// GET: Attendances/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			var myRole = User.FindFirst(ClaimTypes.Role).Value;
			ViewBag.Role = myRole;

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

		[Authorize(Roles = "Admin")]
		// GET: Attendances/Create
		public IActionResult Create()
		{

            ViewData["ScheduleId"] = new SelectList(
				_context.Schedules.Select(s => new
				{
					Id = s.Id,
					DisplayText = $"{s.Date.ToString("yyyy-MM-dd")}"
				}),
				"Id",
				"DisplayText"
			);
            ViewData["UserId"] = new SelectList(
				_context.Users.Select(s => new { Id = s.Id, DisplayText = $"{s.FirstName} {s.LastName}" })
				, "Id", "DisplayText");
			return View();
		}

		// POST: Attendances/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Date,AttendanceTime,LeavingTime,ScheduleId,UserId")] Attendance attendance)
		{
			if (ModelState.IsValid)
			{
				_context.Add(attendance);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["ScheduleId"] = new SelectList(_context.Schedules, "Id", "Id", attendance.ScheduleId);
			ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", attendance.UserId);
			return View(attendance);
		}

		// GET: Attendances/Edit/5
		// [HttpGet("Edit/{id}")]
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
			ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", attendance.UserId);
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
		[Authorize(Roles = "Admin")]
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
		private static float CalculateAbsenceDegree(int noOfAbsenceDays)
		{
			if (noOfAbsenceDays <= 5)
			{
				return noOfAbsenceDays * 5f;
			}
			else if (noOfAbsenceDays > 5 && noOfAbsenceDays < 10)
			{
				return noOfAbsenceDays * 10f;
			}
			else if (noOfAbsenceDays >= 10)
			{
				return noOfAbsenceDays * 25f;
			}
			return 0f; // Default return value in case none of the conditions are met
		}
	}
}
