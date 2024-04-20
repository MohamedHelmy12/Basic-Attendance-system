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
        private IStudentRepository studentRepository;

        public instructorController(IPermissionRepository _permissionRepository, IInstructorRepository _instructorRepository, IAccountRepository _accountRepository, IScheduleRepository _scheduleRepository, IAttendanceRepository _attendanceRepository, IStudentRepository _studentRepository)
        {
            permissionRepository = _permissionRepository;
            instructorRepository = _instructorRepository;
            accountRepository = _accountRepository;
            scheduleRepository = _scheduleRepository;
            attendanceRepository = _attendanceRepository;
            studentRepository = _studentRepository;
        }
        public IActionResult Index()
        {
            var instructors=instructorRepository.GetAllInstructors();
            return View(instructors);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult StudetsPermissions(int insId = 3)
        {
            List<Permission> permissions = permissionRepository.GetSupervisorStudentsPermissions(insId);
    
            return View(permissions);
        }

        //[HttpPost]
        public IActionResult UpdatePermission(int studentID, string Date, PermissionStatus Status)
        {   
            DateOnly date = DateOnly.Parse(Date);
            var permission = permissionRepository.GetPermission(studentID, date);
            
            permissionRepository.UpdatePermission(permission, Status);
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
            var insId = accountRepository.GetUserByEmail(User.FindFirst(ClaimTypes.Email).Value).Id;
            var insTrackIntake = instructorRepository.GetSupervisionInfo(insId);
            schedule.TrackId = insTrackIntake.TrackId;
            try
            {
                scheduleRepository.AddSchedule(schedule);
                var trackStudentsIds = studentRepository.GetStudentsIdsInTrack(insTrackIntake.TrackId, insTrackIntake.IntakeId);
                attendanceRepository.AddScheduleForTrackStudents(schedule, trackStudentsIds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return RedirectToAction("Schedule");
        }
        [HttpPost]
        public IActionResult Create(Instructor instructor)
        {
            instructorRepository.AddInstructor(instructor);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var instructor=instructorRepository.GetInstructorById(id);
            return View(instructor);
        }
        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            instructorRepository.UpdateInstructor(instructor);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            instructorRepository.DeleteInstructor(id);
            return RedirectToAction("Index");

        }
        
    }
}
