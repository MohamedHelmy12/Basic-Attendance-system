using MVC_Attendance.Models;

namespace MVC_Attendance.IRepository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();

        Employee GetById(int id);

        void Add(Employee employe);

        void Update(int id, Employee employe);

        void Delete(int id);
    }
}
