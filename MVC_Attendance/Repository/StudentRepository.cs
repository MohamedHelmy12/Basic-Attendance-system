using Microsoft.EntityFrameworkCore;
using MVC_Attendance.IRepository;
using MVC_Attendance.Models;

namespace MVC_Attendance.Repository
{
    public class StudentRepository : IStudentRepository
    {
        AttDbContext db;
        public StudentRepository(AttDbContext _db) 
        {
            db = _db;
        }
        public StdIntakeTrack GetStdIntakeTrack(int  studentId)
        {
           return db.StdIntakeTrack.Include(sit => sit.Track).Include(sit => sit.Intake).FirstOrDefault(sit => sit.StudentId == studentId);

        }
    }
}
