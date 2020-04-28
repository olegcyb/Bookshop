using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Data
{
    public static class DataInitializer
    {
        public static async Task SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, BookShopDb context)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager, context);
        }
        public static async Task SeedUsers(UserManager<IdentityUser> userManager, BookShopDb context)
        {
            string username = "admin@gmail.com";
            string password = "mainadmin";
            if (await userManager.FindByNameAsync(username) == null)
            {
                User admin = new User() { UserName = username,Email=username };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    var bucket = new Bucket() { User = admin, UserId = admin.Id };
                    admin.Bucket = bucket;
                    await context.Buckets.AddAsync(bucket);
                    await context.SaveChangesAsync();
                    await userManager.AddToRoleAsync(admin, "Admin");
                    await userManager.AddToRoleAsync(admin, "Reader");
                }
            }
        }
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Reader", "Admin" };
            IdentityResult roleResult;
            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (roleExist == false)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
