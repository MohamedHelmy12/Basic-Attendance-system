using MVC_Attendance.Models;

namespace MVC_Attendance.IRepository
{
    public interface IAttendanceRepository
    {
        // Get all attendances
        IEnumerable<Attendance> GetAllAttendances();
        void AddAttendance(Schedule schedule, int userId);
        void AddScheduleForTrackStudents(Schedule schedule, List<int> TrackStudentsIds);
    }
}
