using System;
using System.Threading.Tasks;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Model.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KingsStoreApi.Configuration
{
    public static class DataInitializer
    {
        public async static void RunDataInitiazer(this IApplicationBuilder app, IConfiguration configuration)
        {
            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<User>>();

            var roleManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<Role>>();

            await SeedRolesAsync(roleManager);
            await SeedAdmin(userManager, roleManager, configuration);
        }

       private async static Task SeedRolesAsync(RoleManager<Role> roleManager)
        {
            await roleManager.CreateAsync(new Role { Name = Roles.Admin.ToString() });
            await roleManager.CreateAsync(new Role { Name = Roles.Customer.ToString() });
            await roleManager.CreateAsync(new Role { Name = Roles.Vendor.ToString() });
        }

        private async static Task SeedAdmin(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration)
        {
            var admin = new User
            {
                FullName = "King Alex",
                isActive = true,
                isAdmin = true,
                isVendor = false,
                Bio = "I love how colourful life can be when you create software...❤ ",
                Email = "ogubuikeAlex@gmail.com",
                UserName = "ogubuikeAlex@gmail.com",
                PhoneNumber = "09047617047",
                LastLogin = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var result = await userManager.CreateAsync(admin, configuration["AdminPassword"]);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
        }
    }
}
