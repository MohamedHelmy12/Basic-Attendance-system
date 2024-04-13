using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Attendance.IRepository;
using MVC_Attendance.Models;
using System.Security.Claims;

namespace MVC_Attendance.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class instructorController : Controller
    {
        private IPermissionRepository permissionRepository;
        private IInstructorRepository instructorRepository;
        private IAccountRepository accountRepository;
        private IScheduleRepository scheduleRepository;
        private IAttendanceRepository attendanceRepository;
        public instructorController(IPermissionRepository _permissionRepository, IInstructorRepository _instructorRepository, IAccountRepository _accountRepository, IScheduleRepository _scheduleRepository, IAttendanceRepository _attendanceRepository)
        {
            permissionRepository = _permissionRepository;
            instructorRepository = _instructorRepository;
            accountRepository = _accountRepository;
            scheduleRepository = _scheduleRepository;
            attendanceRepository = _attendanceRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StudetsPermissions(int insId = 3)
        {
            List<Permission> permissions = permissionRepository.GetSupervisorStudentsPermissions(insId);
            
            return View(permissions);
        }

        public IActionResult UpdatePermission(int studentId, string date, string status)
        {
            DateOnly ddate = DateOnly.Parse(date);
            var permission = permissionRepository.GetPermission(studentId, ddate);
            
            permissionRepository.UpdatePermission(permission, status);
            return Content("Success");
        }
        [HttpGet]
        public ActionResult Schedule()
        {
            var insId = accountRepository.GetUserByEmail(User.FindFirst(ClaimTypes.Email).Value).Id;
            var insTrackIntake = instructorRepository.GetSupervisionInfo(insId);
            List<Schedule> schedules = scheduleRepository.GetTrackSchedule(insTrackIntake.TrackId, insTrackIntake.IntakeId);
            ViewBag.schedules = schedules;
            return View();
        }

        [HttpPost]
        public ActionResult Schedule(Schedule schedule)
        {
            //var insId = accountRepository.GetUserByEmail(User.FindFirst(ClaimTypes.Email).Value).Id;
            //var insTrackIntake = instructorRepository.GetSupervisionInfo(insId);
            //List<Schedule> schedules = scheduleRepository.GetTrackSchedule(insTrackIntake.TrackId, insTrackIntake.IntakeId);
            //ViewBag.schedules = schedules;
            scheduleRepository.AddSchedule(schedule);
            return RedirectToAction("Schedule");
        }
    }
}
