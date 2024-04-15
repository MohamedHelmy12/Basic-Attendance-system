using MVC_Attendance.Models;

namespace MVC_Attendance.IRepository
{
    public interface IAttendanceRepository
    {
        void AddAttendance(Schedule schedule, int userId);
        void AddScheduleForTrackStudents(Schedule schedule, List<int> TrackStudentsIds);
    }
}
