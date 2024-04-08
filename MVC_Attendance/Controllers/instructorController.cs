using Microsoft.AspNetCore.Mvc;
using MVC_Attendance.IRepository;
using MVC_Attendance.Models;

namespace MVC_Attendance.Controllers
{
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

        public IActionResult StudetsPermissions(int insId)
        {
            List<Permission> permissions = permissionRepository.GetPendingPermissions();
            return View(permissions);
        }
        
    }
}
