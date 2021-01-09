using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Covid_19.Models
{
    public class ApplicationUser:IdentityUser
    {
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
            Database.SetInitializer(new SecurityInitializer());
        }
    }
    public class SecurityInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!context.Roles.Any(x => x.Name == "Admin"))
            {
                manager.Create(new IdentityRole("Admin"));
            }
            if (!context.Roles.Any(x => x.Name == "Member"))
            {
                manager.Create(new IdentityRole("Member"));
            }
            if (!context.Users.Any(x => x.UserName == "Administrator"))
            {
                var user = new ApplicationUser { UserName = "Administrator" };

                userManager.Create(user, "@Open1234");
                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }

}