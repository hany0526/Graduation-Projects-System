using Graduation_Projects_System.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Graduation_Projects_System.Startup))]
namespace Graduation_Projects_System
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }
        public void CreateDefaultRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            
            if (!roleManager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "admin@gmail.com";
                user.name = "Admin";
                user.Email = "admin@gmail.com";
                user.Departmentid = 1;
                var Ch = userManager.Create(user, "admin123456@ADMIN");
                if (Ch.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
            
        }
    }
}
