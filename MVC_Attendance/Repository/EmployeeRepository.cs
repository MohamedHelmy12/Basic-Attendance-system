using MVC_Attendance.Models;

namespace MVC_Attendance.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        AttDbContext db;
        public EmployeeRepository(AttDbContext db)
        {
            this.db = db;
        }
        public List<Employee> GetAll()
        {
            return db.Employees.ToList();
        }
        public Employee GetById(int id)
        {
            return db.Employees.FirstOrDefault(emp => emp.Id == id);
        }
        public void Add(Employee emp)
        {
          emp.role=Role.Employee;
            db.Employees.Add(emp);
            db.SaveChanges();
        }
        public void Update(int id, Employee emp)
        {
            db.Employees.Update(emp);
            db.SaveChanges();

        }
        public void Delete(int id)
        {
            Employee emp = GetById(id);
            db.Employees.Remove(emp);
            db.SaveChanges();
        }
    }
}
