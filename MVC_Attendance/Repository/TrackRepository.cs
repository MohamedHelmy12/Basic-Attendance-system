using MVC_Attendance.IRepository;
using MVC_Attendance.Models;

namespace MVC_Attendance.Repository
{
    public class TrackRepository:ITrackRepository
    {

        private AttDbContext db;
        public TrackRepository(AttDbContext _db)
        {
            db = _db;
        }
        public List<Track> GetAll()
        {
            return db.Tracks.ToList();
        }
        public Track GetById(int id)
        {
            return db.Tracks.FirstOrDefault( track => track.Id == id);
        }
        public void Add(Track track)
        {
            db.Tracks.Add(track);
            db.SaveChanges();
        }
        public void Update(int id,Track track)
        {
            db.Tracks.Update(track);
            db.SaveChanges();
        }
        public DateOnly GetTrackStartDate(int trackId, int intakeId)
        {
            return db.IntakesTracks.FirstOrDefault(it => it.TrackId == trackId && it.IntakeId == intakeId).StartDate;
        }
        public void Delete(int id)
        {
            Track track=  GetById(id);
            db.Tracks.Remove(track);
            db.SaveChanges();
        }
       
    }
}
