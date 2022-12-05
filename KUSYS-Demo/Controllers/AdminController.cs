using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.Controllers
{
    [Authorize(Roles = "admin")]

    public class AdminController : Controller
    {
        public IActionResult Display()
        {
            return View();
        }

        public IActionResult Students()
        {
            return View();
        }
        //public async Task<IActionResult> createStudent()
        //    {
        //        var model = new RegistrationModel
        //        {
        //            FirstName = "User1",
        //            LastName = "Surname1",
        //            Username = "user1",
        //            Email = "user1@gmail.com",
        //            Password = "User@1",

        //        };
        //        model.Role = "user";
        //        var result = await _service.RegistrationAsync(model);
        //        return Ok(result);

        //    }
        public IActionResult Courses()
        {
            return View();
        }

    }
}
