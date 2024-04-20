using MVC_Attendance.IRepository;
using MVC_Attendance.Models;
using System;

namespace MVC_Attendance.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly AttDbContext db;
        private readonly ITrackRepository trackRepository;
        public ScheduleRepository(ITrackRepository _trackRepository, AttDbContext _db)
        {
            trackRepository = _trackRepository;
            db = _db;
        }

        public int AddSchedule(Schedule schedule)
        {
            db.Add(schedule);
            db.SaveChanges();
            return schedule.Id;
        }

        public void DeleteSchedule(int scheduleId)
        {
            var schedule = db.Schedules.FirstOrDefault(s => s.Id == scheduleId);
            db.Schedules.Remove(schedule);
            db.SaveChanges();
        }

        public List<Schedule> GetTrackSchedule(int trackId, int intakeId)
        {
            DateTime currentDate = DateTime.Now; 
            DayOfWeek startOfWeek = DayOfWeek.Sunday; 

            DateTime startDateOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            while (startDateOfWeek.DayOfWeek != startOfWeek)
            {
                startDateOfWeek = startDateOfWeek.AddDays(-1);
            }
            startDateOfWeek = startDateOfWeek.AddDays(-1);
            Console.WriteLine(startDateOfWeek.Date);
            var currentWeekStartDate = DateOnly.FromDateTime(startDateOfWeek); 
            var trackStartDate = trackRepository.GetTrackStartDate(trackId, intakeId);
            var schedules =  db.Schedules.Where(s => s.TrackId == trackId && trackStartDate.CompareTo(s.Date) < 0 && currentWeekStartDate.CompareTo(s.Date) < 0).ToList();

            return schedules;
        }

        public List<Schedule> GetTrackScheduleBetweenDates(DateOnly startDate, DateOnly endDate, int trackId)
        {
            
            return db.Schedules.Where(s => s.Date.CompareTo(startDate) < 0 && s.Date.CompareTo(endDate) > 0 && s.TrackId == trackId).ToList();
        }

        public void UpdateSchedule(Schedule schedule)
        {
            db.Update(schedule);
            db.SaveChanges();
        }
    }
}
