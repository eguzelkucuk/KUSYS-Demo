using KUSYS_Demo.Models.DTO;
using KUSYS_Demo.Repositories.Abstract;
using KUSYS_Demo.Repositories.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _service;
        private readonly staticDatas _staticDatas;
        public string userRole;



        public UserAuthenticationController(IUserAuthenticationService service)
        {
            this._service = service;
            userRole = staticDatas.getUserRole();


        }
        //public IActionResult Registration()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid)  return View(model);
                model.Role = "user";
                var result=await this._service.RegistrationAsync(model);
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Registration));
            
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _service.LoginAsync(model);
            if (result.StatusCode==1)
            {

                if (userRole=="admin")
                {
                    return RedirectToAction("Display", "Dashboard");

                }

                return RedirectToAction("Display","Dashboard");   
            }
            else
            {
                TempData["msg"]=result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }


        // Admin Registration Block

        //public async Task<IActionResult> AdminReg()
        //{
        //    var model = new RegistrationModel
        //    {
        //        FirstName = "Admin",
        //        LastName = "Admin",
        //        Username = "Admin1",
        //        Email = "admin1@gmail.com",
        //        Password = "Admin@1",

        //    };
        //    model.Role = "admin";
        //    var result = await _service.RegistrationAsync(model);
        //    return Ok(result);

        //}

        // User Registration Block

        //public async Task<IActionResult> UserReg()
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

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);
        //    var result = await _service.ChangePasswordAsync(model, User.Identity.Name);
        //    TempData["msg"] = result.Message;
        //    return RedirectToAction(nameof(ChangePassword));
        //}

    }
}
