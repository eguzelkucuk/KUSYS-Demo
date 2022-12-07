1) If you wwant to create an admin user, you can activate the registration code in UserAuthenticationController.cs file 

UserAuthenticationController.cs
---------------------------------------------


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
 
2) And kindly change the link in Registration.cshml like below:

Registration.cshtml
---------------------------------------------
 

<a class="btn btn-primary" href="/UserAuthentication/AdminReg">Login</a>

3) Students are can view their own pages. But first they must be added to the Students List with User email addresses.


