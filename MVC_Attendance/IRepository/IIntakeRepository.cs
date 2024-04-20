using MVC_Attendance.Models;

namespace MVC_Attendance.IRepository
{
    public interface IIntakeRepository
    {
        List<Intake> GetAll();

        Intake GetById(int id);

        void Add(Intake intake);

        void Update(int id, Intake intake);


    }
}
