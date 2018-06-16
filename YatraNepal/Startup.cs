using System;
using Microsoft.Owin;
using Owin;
using YatraNepal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;

[assembly: OwinStartupAttribute(typeof(YatraNepal.Startup))]
namespace YatraNepal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        //creating default user and admin roles for login
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            //create role admin at the start if not exist
            if (!roleManager.RoleExists("Admin"))
            {
                //creating role admin
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //create default superuser to maintain the web
                var user = new ApplicationUser();
                user.UserName = "qwert";
                user.Email = "qwert@gmail.com";

                string userPWD = "Qwert@1234";
                var chkUser = UserManager.Create(user, userPWD);

                //add role to user
                if(chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            //creating agent role
            if (!roleManager.RoleExists("Agent"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Agent";
                roleManager.Create(role);
            }
        }
    }
}
