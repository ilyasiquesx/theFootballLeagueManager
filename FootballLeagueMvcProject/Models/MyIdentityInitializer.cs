using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Models
{
    public class MyIdentityInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {

        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                userManager.CreateAsync(new ApplicationUser { UserName = "admin" }, "adminADMIN2020").Wait();
                var user = userManager.Users.FirstOrDefault();
                if (user != null)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                roleManager.CreateAsync(new ApplicationRole { Name = "Admin" }).Wait();

                roleManager.CreateAsync(new ApplicationRole { Name = "Moderator" }).Wait();

            }
        }
    }
}
