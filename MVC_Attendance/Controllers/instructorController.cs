using Microsoft.AspNetCore.Mvc;

namespace MVC_Attendance.Controllers
{
    public class instructorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
