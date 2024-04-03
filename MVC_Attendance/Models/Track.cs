using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_Attendance.Models
{
    public class Track
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Length(minimumLength: 3, maximumLength: 100)]
        public string Name { get; set; }
        //public bool Status { get; set; }

    }
}
