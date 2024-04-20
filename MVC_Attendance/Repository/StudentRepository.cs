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
        public StdIntakeTrack GetStdIntakeTrack(int studentId)
        {
            return db.StdIntakeTrack.Include(sit => sit.Track).Include(sit => sit.Intake).FirstOrDefault(sit => sit.StudentId == studentId);
        }

        public List<int> GetStudentsIdsInTrack(int trackId, int intakeId)
        {
            return db.StdIntakeTrack.Where(sit => sit.TrackId == trackId && sit.IntakeId == intakeId).Select(sit => sit.StudentId).ToList();
        }
        public List<Student> GetAllStudents()
        {
            return db.Students.ToList();
        }

        public Student GetStudentById(int id)
        {
            return db.Students.FirstOrDefault(s => s.Id == id);
        }

        public void AddStudent(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            db.Students.Update(student);
            db.SaveChanges();

        }

        public void DeleteStudent(int id)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                student = GetStudentById(id);
                db.Students.Remove(student);
                db.SaveChanges();
            }
        }

        public List<Student> GetStudentsByTrackId(int trackId)
        {

            var studentsInTrack = db.StdIntakeTrack
                .Where(sit => sit.TrackId == trackId)
                .Select(sit => sit.Student)
                .ToList();

            return studentsInTrack;
        }

        public void AddRangeOfStudents(IEnumerable<Student> students)
        {
            db.Students.AddRange(students);
            db.SaveChanges();
        }
    }
}


