using MVC_Attendance.Models;

namespace MVC_Attendance.Repository
{
    public interface IStudentRepository
    {
        List<Student> GetAll();

        Student GetById(int id);

        void Add(Student student);

        void Update(int id, Student student);

        void Delete(int id);

        public List<Student> GetStudentsByTrackId(int trackId);
    }
}
