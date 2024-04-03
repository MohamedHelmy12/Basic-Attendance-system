using System.ComponentModel.DataAnnotations;

namespace MVC_Attendance.Models
{
    public class ITIProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Properties(Optional)
        //[StringLength(maximumLength: 100, MinimumLength = 3)]
        //public string Description { get; set; } = "This Program is on of ITI Programs";


        public virtual ICollection<Intake> Intakes { get; set; } = new List<Intake>();
    }
}
