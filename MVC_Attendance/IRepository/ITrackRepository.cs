using MVC_Attendance.Models;

namespace MVC_Attendance.IRepository
{
    public interface ITrackRepository
    {
        DateOnly GetTrackStartDate(int trackId, int intakeId);
        List<Track> GetAll();

        Track GetById(int id);

        void Add(Track track);

        void Update(int id, Track track);

        void Delete(int id);
    }
}
