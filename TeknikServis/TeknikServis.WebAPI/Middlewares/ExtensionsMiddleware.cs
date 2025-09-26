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

            // Tüm rolleri seedle
            foreach (var role in ConstantsRole.GetRoles())
            {
                if (!roleManager.RoleExistsAsync(role.Name!).GetAwaiter().GetResult())
                {
                    roleManager.CreateAsync(new AppRole { Id = role.Id, Name = role.Name, NormalizedName = role.NormalizedName }).GetAwaiter().GetResult();
                }
            }

            // Admin kullanıcısı yoksa oluştur
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
                    EmailConfirmed = true
                };

                var createResult = userManager.CreateAsync(user, "Mudbey123.").GetAwaiter().GetResult();
                if (!createResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", createResult.Errors.Select(e => e.Description)));
                }
            }

            // Admin, User ve Customer rollerini ata
            var desiredRoles = new[] { RoleNames.Admin, RoleNames.User, RoleNames.Customer };
            foreach (var roleName in desiredRoles)
            {
                if (!userManager.IsInRoleAsync(user, roleName).GetAwaiter().GetResult())
                {
                    var addRoleResult = userManager.AddToRoleAsync(user, roleName).GetAwaiter().GetResult();
                    if (!addRoleResult.Succeeded)
                    {
                        throw new Exception(string.Join("; ", addRoleResult.Errors.Select(e => e.Description)));
                    }
                }
            }
        }
    }
}
