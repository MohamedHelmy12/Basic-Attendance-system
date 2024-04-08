using MVC_Attendance.Models;

namespace MVC_Attendance.IRepository
{
    public interface IInstructorRepository
    {
        Supervise GetSupervisionInfo(int insId);

    }
}
