using MVC_Attendance.Models;

namespace MVC_Attendance.Repository
{
    public class SuperviseRepository : ISuperviseRepository
    {
        AttDbContext db;

        public SuperviseRepository(AttDbContext db)
        {
            this.db = db;
        }
        public List<Supervise> GetAll()
        {
            return db.Supervises.ToList();
        }

        public Supervise GetByTrackIntakeInstructor(int trackId, int intakeId, int instructorId)
        {
            return db.Supervises.FirstOrDefault(supervise =>
                supervise.TrackId == trackId &&
                supervise.IntakeId == intakeId &&
                supervise.InstructorId == instructorId);
        }

        public void Add(Supervise supervise )
        {
            db.Supervises.Add(supervise);
            db.SaveChanges();
        }
        public void Update(Supervise supervise)
        {
           
            db.Supervises.Update(supervise);
            db.SaveChanges();

        }
        public void Delete(int trackId, int intakeId, int instructorId)
        {
            Supervise supervise = GetByTrackIntakeInstructor(trackId, intakeId, instructorId);
            if (supervise != null)
            {
                db.Supervises.Remove(supervise);
                db.SaveChanges();
            }
        }



    }
}
