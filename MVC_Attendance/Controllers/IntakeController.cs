using Microsoft.AspNetCore.Mvc;

namespace MVC_Attendance.Controllers
{
    public class IntakeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
