using MVC_Attendance.Models;

namespace MVC_Attendance.Repository
{
    public class StudentRepository : IStudentRepository
    {
        AttDbContext db;

        public StudentRepository(AttDbContext db)
        {
            this.db = db;
        }
        public List<Student> GetAll()
        {
            return db.Students.ToList();
        }
        public Student GetById(int id)
        {
            return db.Students.FirstOrDefault(studen => studen.Id == id);
        }
        public void Add(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
        }
        public void Update(int id, Student student )
        {
            db.Students.Update(student);
            db.SaveChanges();

        }
        public void Delete(int id)
        {
            Student student = GetById(id);
            db.Students.Remove(student);
            db.SaveChanges();
        }

        public List<Student> GetStudentsByTrackId(int trackId)
        {
           
            var studentsInTrack = db.StdIntakeTrack
                .Where(sit => sit.TrackId == trackId)
                .Select(sit => sit.Student)
                .ToList();

            return studentsInTrack;
        }
    }
}
