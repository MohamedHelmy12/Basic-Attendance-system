using MVC_Attendance.Models;

namespace MVC_Attendance.viewModels
{
    public class SupervisorAddViewModel
    {
        public List<Instructor> Instructors { get; set; }
        public List<Track> Tracks { get; set; }
    }
}
