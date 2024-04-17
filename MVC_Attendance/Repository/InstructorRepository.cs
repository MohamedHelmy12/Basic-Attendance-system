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

        public void AddInstructor(Instructor instructor)
        {
            db.Instructors.Add(instructor);
            db.SaveChanges();
        }

        public void DeleteInstructor(int id)
        {
            var instructor = db.Instructors.FirstOrDefault(x => x.Id == id);
            db.Remove(instructor);
            db.SaveChanges();
        }

        public List<Instructor> GetAllInstructors()
        {
            var instructors = db.Instructors.ToList();
            return instructors;
        }

        public Instructor GetInstructorById(int id)
        {
            var instructor = db.Instructors.FirstOrDefault(x => x.Id == id);
            return instructor;
        }

        public Supervise GetSupervisionInfo(int insId)
        {
            var supervisionInfo = db.Supervises.Where(s => s.InstructorId == insId).OrderByDescending(s => s.IntakeId).FirstOrDefault();
            return supervisionInfo;
        }

        public void UpdateInstructor(Instructor instructor)
        {
            db.Instructors.Update(instructor);
            db.SaveChanges();
        }
    }
}
