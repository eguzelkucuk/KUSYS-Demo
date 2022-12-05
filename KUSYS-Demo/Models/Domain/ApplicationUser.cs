using Microsoft.AspNetCore.Identity;

namespace KUSYS_Demo.Models.Domain
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ? ProfilePicture { get; set; }
    }
}
