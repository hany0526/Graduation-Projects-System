using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Graduation_Projects_System.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserType { get; set; }
        public string name { get; set; }

        public Department Department { get; set; }
        public int Departmentid { get; set; }

        public ApplicationUser leader { get; set; }
        public string leaderid { set; get; }

        public string file { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<Admin> Admins { get; set; }

        public System.Data.Entity.DbSet<Student> Students { get; set; }

        public System.Data.Entity.DbSet<Idea> Ideas { get; set; }

        public System.Data.Entity.DbSet<Professor> Professors { get; set; }

        public System.Data.Entity.DbSet<Department> Departments { get; set; }
    }
}