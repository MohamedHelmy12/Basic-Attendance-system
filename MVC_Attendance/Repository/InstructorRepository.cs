using MVC_Attendance.IRepository;
using MVC_Attendance.Models;

namespace MVC_Attendance.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly AttDbContext db;
        public InstructorRepository(AttDbContext _db)
        {
            db = _db;
        }
        public Supervise GetSupervisionInfo(int insId)
        {
            return db.Supervises.Where(s => s.InstructorId == insId).OrderByDescending(s => s.IntakeId).FirstOrDefault();
        }
    }
}
