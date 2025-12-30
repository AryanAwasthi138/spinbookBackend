using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TableTennisBooking.Data;
using TT_Exp;

namespace TableTennisBooking.Models
{
    public  class SeedData
    {
        public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                {
                    roleManager.CreateAsync(new IdentityRole(role)).Wait();
                }
            }

            var adminEmail = configuration["Admin:Email"];
            var adminPassword = configuration["Admin:Password"];
            if (userManager.FindByEmailAsync(adminEmail).Result == null)
            {
                var adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Aryan",
                    LastName ="Awasthi",
                    LockoutEnabled = false,
                    PhoneNumber = "9770517850",
                    EmailConfirmed=true
                };
                try
                {
                    var result = userManager.CreateAsync(adminUser, adminPassword).Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                        Console.WriteLine("Role and Admin Created Successfully");
                    }
                } catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
    }
}
