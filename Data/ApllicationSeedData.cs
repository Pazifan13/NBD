using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Data
{
    public static class ApplicationSeedData
    {
        public static async Task SeedAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            //Create Roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "Manager", "Designer","AdministrativeAssistant","Botanist","SalesPerson" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            //Create Users
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            if (userManager.FindByEmailAsync("keri@nbd.ca").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "keri@nbd.ca",
                    Email = "keri@nbd.ca"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }


            if (userManager.FindByEmailAsync("stan@nbd.ca").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "stan@nbd.ca",
                    Email = "stan@nbd.ca"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByEmailAsync("cheryl@nbd.ca").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "cheryl@nbd.ca",
                    Email = "cheryl@nbd.ca"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                }
            }

            if (userManager.FindByEmailAsync("cheryl@nbd.ca").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "cheryl@nbd.ca",
                    Email = "cheryl@nbd.ca"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                }
            }

            if (userManager.FindByEmailAsync("designer1@nbd.ca").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "designer1@nbd.ca",
                    Email = "designer1@nbd.ca"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Designer").Wait();
                }
            }

            if (userManager.FindByEmailAsync("sale1@nbd.ca").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "sale1@nbd.ca",
                    Email = "sale1@nbd.ca"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "SalesPerson").Wait();
                }
            }

            if (userManager.FindByEmailAsync("user1@outlook.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "user1@outlook.com",
                    Email = "user1@outlook.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;
                //Not in any role
            }
        }
    }
}
