using KUSYS_Demo.Models.Domain;
using KUSYS_Demo.Models.DTO;
using KUSYS_Demo.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace KUSYS_Demo.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserAuthenticationService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager=signInManager;   
            this.userManager=userManager;   
            this.roleManager=roleManager;   
        }
        public async Task<Status> LoginAsync(LoginModel model)
        {
            var status = new Status();
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user==null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid user";
                return status;
            }
            if (!await userManager.CheckPasswordAsync(user,model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid password";
                return status;
            }
            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await signInManager.UserManager.GetRolesAsync(user);
                var authClaims = new List<Claim> {
                    new Claim(ClaimTypes.Email, model.Email)
            };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logged in successfully.";
                return status;
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "This user locked out.";
                return status;
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on loggin in.";
                return status;
            }
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<Status> RegistrationAsync(RegistrationModel model)
        {
            var status = new Status();
            // Find user by Email or Username???
            var userExist=await userManager.FindByEmailAsync(model.Email);
            if (userExist!=null)
            {
                status.StatusCode = 0;
                status.Message = "User is already exist!";
                return status;
            }
            ApplicationUser user = new ApplicationUser
            {
                SecurityStamp=Guid.NewGuid().ToString(),    
                Email=model.Email,  
                FirstName=model.FirstName,
                LastName=model.LastName,
                UserName=model.Username,
                EmailConfirmed=true
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed.";
                return status;
            }
            if (!await roleManager.RoleExistsAsync(model.Role))
            {
                await roleManager.CreateAsync(new IdentityRole(model.Role));
            }
            if (await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }
            status.StatusCode = 1;
            status.Message = "User has registered successfully.";
            return status;
        }
    }
}
