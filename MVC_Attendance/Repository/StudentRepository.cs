using Microsoft.EntityFrameworkCore;
using MVC_Attendance.IRepository;
using MVC_Attendance.Models;

namespace MVC_Attendance.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AttDbContext db;
        public StudentRepository(AttDbContext _db) 
        {
            db = _db;
        }
        public StdIntakeTrack GetStdIntakeTrack(int  studentId)
        {
           return db.StdIntakeTrack.Include(sit => sit.Track).Include(sit => sit.Intake).FirstOrDefault(sit => sit.StudentId == studentId);
        }

        public List<int> GetStudentsIdsInTrack(int trackId, int intakeId)
        {
            return db.StdIntakeTrack.Where(sit => sit.TrackId == trackId && sit.IntakeId == intakeId).Select(sit => sit.StudentId).ToList();
        }
    }
}
