using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Backend.Models;

namespace Web.Backend.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            if (!roleManager.RoleExistsAsync("superadmin").Result)
            {
                await roleManager.CreateAsync(new IdentityRole("superadmin"));
            }
            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (!roleManager.RoleExistsAsync("user").Result)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            //Seed Default User
            var defaultUser = new User
            {
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "0867537750",
                FullName = "Super Admin",
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.Count(u => u.Email == defaultUser.Email) == 0)
            {
                IdentityResult result = await userManager.CreateAsync(defaultUser, "Qpzm1092@");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser, "superadmin");
                    await userManager.AddToRoleAsync(defaultUser, "admin");
                    await userManager.AddToRoleAsync(defaultUser, "user");
                }
            }
        }
    }
}
