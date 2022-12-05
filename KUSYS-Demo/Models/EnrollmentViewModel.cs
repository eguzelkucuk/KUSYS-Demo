using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS_Demo.Models
{
    [Keyless]

    public class EnrollmentViewModel
    {
        //[Key]
        //public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public virtual List<SelectListItem> StudentList { get; set; }
        public virtual List<SelectListItem> CourseList { get; set; }
    }
}
