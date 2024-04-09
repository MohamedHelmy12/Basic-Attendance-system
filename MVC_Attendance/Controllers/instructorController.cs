using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Attendance.IRepository;
using MVC_Attendance.Models;

namespace MVC_Attendance.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class instructorController : Controller
    {
        private IPermissionRepository permissionRepository;
        public instructorController(IPermissionRepository _permissionRepository)
        {
            permissionRepository = _permissionRepository;
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
            Console.WriteLine($"{studentId} {date} {status}");
            var permission = permissionRepository.GetPermission(studentId, ddate);
            
            permissionRepository.UpdatePermission(permission, status);
            return Content("Success");
        }
    }
}
