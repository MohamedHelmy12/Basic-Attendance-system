using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_Attendance.Models
{
    public class Intake
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Length(minimumLength: 3, maximumLength: 100)]
        public string Name { get; set; }
        // Properties(Optional)
        //[StringLength(maximumLength: 100, MinimumLength = 3)]
        //public string Description { get; set; }

        //[Range(1, 1000)]
        //public int Capacity { get; set; }

        public int ProgramId { get; set; }

        [ForeignKey(nameof(ProgramId))]
        public ITIProgram Program { get; set; }

    }
}
