using Microsoft.AspNetCore.Mvc;

namespace MVC_Attendance.Controllers
{
    public class adminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult std_table()
        {
            return View();
        }
        public IActionResult ins_table()
        {
            return View();
        }
        public IActionResult create_std()
        {
            return View();
        }
    }
}
