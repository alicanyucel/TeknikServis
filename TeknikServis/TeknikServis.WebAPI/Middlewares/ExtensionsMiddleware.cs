using Microsoft.AspNetCore.Identity;
using TeknikServis.Application.Constanst;
using TeknikServis.Domain.Entities;

namespace TeknikServis.WebAPI.Middlewares;

public static class ExtensionsMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
            foreach (var role in ConstantsRole.GetRoles())
            {
                if (!roleManager.RoleExistsAsync(role.Name!).GetAwaiter().GetResult())
                {
                    roleManager.CreateAsync(new AppRole
                    {
                        Id = role.Id,
                        Name = role.Name,
                        NormalizedName = role.NormalizedName,
                        UserRoles = new List<AppUserRole>(),              
                    }).GetAwaiter().GetResult();
                }
            }
            const string adminUserName = "admin";
            var user = userManager.Users.FirstOrDefault(p => p.UserName == adminUserName);
            if (user is null)
            {
                user = new AppUser
                {
                    UserName = adminUserName,
                    Email = "admin@admin.com",
                    FirstName = "Mudbey",
                    LastName = "Yazılım",
                    EmailConfirmed = true,
                    UserRoles= new List<AppUserRole>()
                };

                var createResult = userManager.CreateAsync(user, "Mudbey123.").GetAwaiter().GetResult();
                if (!createResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", createResult.Errors.Select(e => e.Description)));
                }
            }

            var adminRoleName = ConstantsRole
                .GetRoles()
                .First(r => r.Name == RoleNames.Admin)
                .Name!;

            if (!userManager.IsInRoleAsync(user, adminRoleName).GetAwaiter().GetResult())
            {
                var addRoleResult = userManager.AddToRoleAsync(user, adminRoleName).GetAwaiter().GetResult();
                if (!addRoleResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", addRoleResult.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}