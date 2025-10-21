using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.Users.CreateUser;

internal sealed class CreateUserCommandHandler(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    IMapper mapper
) : IRequestHandler<CreateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var appUser = mapper.Map<AppUser>(request);
        appUser.UserName = request.Email;
        appUser.EmailConfirmed = true;
        appUser.UpdatedAt=request.UpdatedAt;
        appUser.CreateadAt = request.CreateadAt;
        appUser.CratedTime = request.CratedTime;
        appUser.UpdatedBy = request.UpdatedBy;
        appUser.UpdatedTime = request.UpdatedTime;
        appUser.IsDeleted= request.IsDeleted;
        if (appUser.CratedTime == default)
        {
            appUser.CratedTime = TimeOnly.FromDateTime(DateTime.UtcNow);
        }
        appUser.CreatedBy = request.CreatedBy;
        appUser.IsDeleted = request.IsDeleted;

        var result = await userManager.CreateAsync(appUser, request.Password);

        if (!result.Succeeded)
            return Result<string>.Failure(string.Join(" | ", result.Errors.Select(e => e.Description)));

        // Assign requested roles (if any). Create missing roles first.
        if (request.Roles is not null && request.Roles.Any())
        {
            var distinctRoles = request.Roles.Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => r.Trim()).Distinct(StringComparer.OrdinalIgnoreCase);
            foreach (var roleName in distinctRoles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var createRoleResult = await roleManager.CreateAsync(new AppRole
                    {
                        Id = Guid.NewGuid(),
                        Name = roleName,
                        NormalizedName = roleName.ToUpperInvariant(),
                        UserRoles = new List<AppUserRole>()
                    });

                    if (!createRoleResult.Succeeded)
                        return Result<string>.Failure(string.Join(" | ", createRoleResult.Errors.Select(e => e.Description)));
                }

                var addRoleResult = await userManager.AddToRoleAsync(appUser, roleName);
                if (!addRoleResult.Succeeded)
                    return Result<string>.Failure(string.Join(" | ", addRoleResult.Errors.Select(e => e.Description)));
            }
        }
        return Result<string>.Succeed("Kullanıcı kaydı yapıldı");
    }
}