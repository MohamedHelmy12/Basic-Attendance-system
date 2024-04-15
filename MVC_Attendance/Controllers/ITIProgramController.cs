using Microsoft.AspNetCore.Mvc;

namespace MVC_Attendance.Controllers
{
    public class ITIProgramController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
