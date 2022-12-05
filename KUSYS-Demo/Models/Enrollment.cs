using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace KUSYS_Demo.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }


        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

    }
}
