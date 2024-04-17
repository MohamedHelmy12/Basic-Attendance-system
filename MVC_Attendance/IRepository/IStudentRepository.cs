using MVC_Attendance.Models;

namespace MVC_Attendance.IRepository
{
    public interface IStudentRepository
    {
        StdIntakeTrack GetStdIntakeTrack(int studentId);
        List<int> GetStudentsIdsInTrack(int trackId, int intakeId);
        List<Student> GetAllStudents();
        Student GetStudentById(int id);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int id);
    }
}
