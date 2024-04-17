
using MVC_Attendance.Models;

namespace MVC_Attendance.IRepository
{
    public interface IInstructorRepository
    {
        Supervise GetSupervisionInfo(int insId);

        List<Instructor> GetAll();

        Instructor GetById(int id);

        void Add(Instructor instructor );

        void Update(int id, Instructor instructor );

        void Delete(int id);

    }

    
}
