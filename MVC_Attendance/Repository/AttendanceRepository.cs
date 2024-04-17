using MVC_Attendance.IRepository;
using MVC_Attendance.Models;

namespace MVC_Attendance.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private AttDbContext db;
        public AttendanceRepository(AttDbContext _db)
        {
            db = _db;
        }
        public void AddAttendance(Schedule schedule, int userId)
        {
            db.Attendances.Add(new Attendance()
            {
                Date = schedule.Date,
                ScheduleId = schedule.Id,
                UserId = userId
            });
            db.SaveChanges();
        }

        public void AddScheduleForTrackStudents(Schedule schedule, List<int> TrackStudentsIds)
        {
            foreach(var studentId  in TrackStudentsIds)
            {
                AddAttendance(schedule, studentId);
            }
        }

        public List<Attendance> GetTrackAttendancesinSchedual(int scheduleId)
        {
            return db.Attendances.Where(a => a.ScheduleId == scheduleId).ToList();
        }
    }
}
