using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_Attendance.Models;
using System.Configuration;
using System.Security.Claims;

namespace MVC_Attendance.Controllers

{
    //[User.Role("Admin")]
    [Authorize(Roles = "Student,Instructor,Employee,Admin")]
    public class PermissionController : Controller
    {
		private readonly AttDbContext _context;


        public PermissionController(AttDbContext context)
        {
			_context = context;
		}

		// GET: PermissionController
		public ActionResult Index()
        {
            // send the time in view bag
            ViewBag.TodayDate = DateOnly.FromDateTime(DateTime.Now);
			// var user = User.FindFirst(ClaimTypes.Role).Value;

			var myRole = User.FindFirst(ClaimTypes.Role).Value;
            var me = User.FindFirst(ClaimTypes.Email);
            var myId = _context.Users.FirstOrDefault(u => u.Email == me.Value).Id;
            var permissions = _context.Permissions.Include(a => a.Student).ToList();

			if (myRole != "Admin")
            {
                permissions = permissions.Where(p => p.StudentId == myId).ToList();
                return View(permissions);
            }
            return View(permissions);
        }

        // GET: PermissionController/Details/5
        public async Task<IActionResult> Details(int id, DateOnly date)
        {
            // Get permission by student id
            var permission = await _context.Permissions
                                             .Include(p => p.Student)
                                             .FirstOrDefaultAsync(p => p.StudentId == id && p.date == date);

            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }


        // GET: PermissionController/Create
        public async Task<ActionResult> Create()
        {
            // calling GetStudents method to get list of students
            var students = await _context.Students.ToListAsync();
            ViewBag.Students = students.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.FirstName + " " + s.LastName
            }).ToList();

            ViewBag.PermissionTypes = GetPermissionTypes();
            ViewBag.StatusTypes = GetStatusTypes();

            

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            // check if the values are of Studeent Id and date are already exist using the PermissionExists method
            if (PermissionExists(int.Parse(collection["StudentId"]), DateOnly.Parse(collection["date"])))
            {
				// message box to show the error message
				ModelState.AddModelError(string.Empty, "Permission already exists for this student and date.");
			}
            try
            {

                var permission = new Permission();

                // Manually bind properties from the collection
                permission.StudentId = int.Parse(collection["StudentId"]);
                permission.date = DateOnly.Parse(collection["date"]);
                permission.Type = Enum.Parse<PermissionType>(collection["Type"]);
                permission.Status = Enum.Parse<PermissionStatus>(collection["Status"]);
                permission.Reason = collection["Reason"];

                // Add and save changes
                _context.Permissions.Add(permission);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: PermissionController/Edit/5
        public async Task<IActionResult> Edit(int id, DateOnly date)
        {
            var permission = await _context.Permissions
                .Include(p => p.Student)
                .FirstOrDefaultAsync(p => p.StudentId == id && p.date == date);

            if (permission == null)
            {
                return NotFound();
            }

            ViewBag.PermissionTypes = GetPermissionTypes();
            ViewBag.StatusTypes = GetStatusTypes();
            return View(permission);
        }

        // POST: PermissionController/Edit/5
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( Permission permission,int? id)
        {
            //if (id != permission.StudentId)
            //{
            //    return NotFound();
            //}

            //if (!ModelState.IsValid)
            //{
            //    return View(permission);
            //}

            try
            {
                _context.Update(permission);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(permission.StudentId, permission.date))
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

        // GET: PermissionController/Delete/5
        public ActionResult Delete(int id, DateOnly date)
        {
            // Find permission by student id
            var permission = _context.Permissions.Include(p => p.Student).FirstOrDefault(p => p.StudentId == id && p.date == date);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        // POST: PermissionController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, DateOnly date)
        {
            try
            {
                // Find permission by student id
                var permission = _context.Permissions.FirstOrDefault(p => p.StudentId == id && p.date == date);
                if (permission == null)
                {
                    return NotFound();
                }

                // Remove and save changes
                _context.Permissions.Remove(permission);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // Get list of students
        //public void GetStudents(int selectedID = 0)
        //{
        //    // using Asynic and await to get list of students
        //    var students = _context.Students.ToListAsync().Result;

        //    SelectList studentsList = new SelectList(students, "Id", "Name", selectedID);
        //    ViewBag.Students = studentsList;
        //}

        private List<SelectListItem> GetPermissionTypes()
        {
            var permissionTypes = Enum.GetValues(typeof(PermissionType))
                                      .Cast<PermissionType>()
                                      .Select(p => new SelectListItem
                                      {
                                          Value = p.ToString(),
                                          Text = p.ToString()
                                      }).ToList();
            return permissionTypes;
        }

        private List<SelectListItem> GetStatusTypes()
        {
            var statusTypes = Enum.GetValues(typeof(PermissionStatus))
                                  .Cast<PermissionStatus>()
                                  .Select(s => new SelectListItem
                                  {
                                      Value = s.ToString(),
                                      Text = s.ToString()
                                  }).ToList();
            return statusTypes;
        }

        private bool PermissionExists(int id, DateOnly date)
        {
            return _context.Permissions.Any(p => p.StudentId == id && p.date == date);
        }
    }
}
