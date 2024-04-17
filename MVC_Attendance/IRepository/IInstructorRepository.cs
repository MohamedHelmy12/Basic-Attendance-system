using MVC_Attendance.Models;

namespace MVC_Attendance.IRepository
{
    public interface IInstructorRepository
    {
        Supervise GetSupervisionInfo(int insId);
        List<Instructor> GetAllInstructors();
        void AddInstructor(Instructor instructor);
        Instructor GetInstructorById(int id);
        void UpdateInstructor(Instructor instructor);
        void DeleteInstructor(int id);
    }
}
