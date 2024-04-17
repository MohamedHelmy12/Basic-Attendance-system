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
            var supervisionInfo = db.Supervises.Where(s => s.InstructorId == insId).OrderByDescending(s => s.IntakeId).FirstOrDefault();
            return supervisionInfo;
        }

        public List<Instructor> GetAll()
        {
            return db.Instructors.ToList();
        }
        public Instructor GetById(int id)
        {
            return db.Instructors.FirstOrDefault(ins => ins.Id == id);
        }
        public void Add(Instructor instructor )
        {
            db.Instructors.Add(instructor);
            db.SaveChanges();
        }
        public void Update(int id, Instructor instructor )
        {
            db.Instructors.Update(instructor);
            db.SaveChanges();

        }
        public void Delete(int id)
        {
            Instructor instructor = GetById(id);
            db.Instructors.Remove(instructor);
            db.SaveChanges();
        }

       

    }
}