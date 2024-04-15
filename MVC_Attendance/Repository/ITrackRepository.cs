using MVC_Attendance.Models;

namespace MVC_Attendance.Repository
{
    public interface ITrackRepository
    {
         List<Track> GetAll();

        Track GetById(int id);

         void Add(Track track);

        void Update(int id, Track track);

        void Delete(int id);
       
    }
}
