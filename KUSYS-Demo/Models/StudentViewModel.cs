using Microsoft.AspNetCore.Mvc.Rendering;

namespace KUSYS_Demo.Models
{
    public class StudentViewModel
    {
        public string SelectedStudent { get; set; }
        public List<SelectListItem> StudentsSelectList { get; set; }
    }
}
