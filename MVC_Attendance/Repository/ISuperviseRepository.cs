using MVC_Attendance.Models;

namespace MVC_Attendance.Repository
{
    public interface ISuperviseRepository
    {
        List<Supervise> GetAll();

        Supervise GetById(int id);

        void Add(Supervise supervise );

        void Update(int id, Supervise supervise);

        void Delete(int id);

       
    }
}
