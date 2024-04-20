using MVC_Attendance.Models;

namespace MVC_Attendance.viewModels
{
    public class SupervisorEditViewModel
    {
        public  Supervise Supervise { get; set; }
        public List<Instructor> Instructors { get; set; }
        public List<Track> Tracks { get; set; }
        public List<Intake> Intakes { get; set; }
    }
}
