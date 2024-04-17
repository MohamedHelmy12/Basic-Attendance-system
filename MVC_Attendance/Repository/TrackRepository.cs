using MVC_Attendance.IRepository;
using MVC_Attendance.Models;

namespace MVC_Attendance.Repository
{
    public class TrackRepository : ITrackRepository
    {
        private AttDbContext db;
        public TrackRepository(AttDbContext _db)
        {
            db = _db;
        }
        public DateOnly GetTrackStartDate(int trackId, int intakeId)
        {
            return db.IntakesTracks.FirstOrDefault(it => it.TrackId == trackId && it.IntakeId == intakeId).StartDate;
        }
    }
}
