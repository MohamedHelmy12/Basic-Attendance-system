using Microsoft.AspNetCore.Mvc;
using MVC_Attendance.IRepository;
using MVC_Attendance.Models;

namespace MVC_Attendance.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository )
        {
            this.employeeRepository = employeeRepository;

        }
        // GET: Employee
        public IActionResult Index()
        {
          
            List<Employee> employees = employeeRepository.GetAll();

            return View(employees);
        }
        
        // GET: Employee/Details/5
        public IActionResult Details(int id)
        {
           
            Employee employee = employeeRepository.GetById(id);

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Add(employee);
                return RedirectToAction("Index");
            }
            else
            {

                return View(employee);
            }

        }

        // GET: Employee/Edit/5
        public IActionResult Edit(int id)
        {

            Employee employee = employeeRepository.GetById(id);

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Update(id, employee);
                return RedirectToAction("Index");
            }
            else
            {
                return View(employee);
            }

           
        }

        // GET: Employee/Delete/5
        public IActionResult Delete(int id)
        {
            
            employeeRepository.Delete(id);

            return RedirectToAction("Index");
        }

       
    }
}
