using MVC_Attendance.Models;

namespace MVC_Attendance.IRepository
{
    public interface IScheduleRepository
    {
        List<Schedule> GetTrackSchedule(int trackId, int intakeId);
        int AddSchedule(Schedule schedule);
        void DeleteSchedule(int scheduleId);
        void UpdateSchedule(Schedule schedule);
        List<Schedule> GetTrackScheduleBetweenDates(DateOnly startDate, DateOnly endDate, int trackId);
    }
}
