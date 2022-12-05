using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KUSYS_Demo.Models;

namespace KUSYS_Demo.Models.Domain
{
    public class DatabaseContext:IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            :base(options)
        {

        }
        public DbSet<KUSYS_Demo.Models.Course> Course { get; set; }
        public DbSet<KUSYS_Demo.Models.Student> Student { get; set; }
        public DbSet<KUSYS_Demo.Models.Enrollment> Enrollment { get; set; }
        public DbSet<KUSYS_Demo.Models.EnrollmentViewModel> EnrollmentViewModel { get; set; }

    }

}
