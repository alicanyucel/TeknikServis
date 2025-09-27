using MediatR;
using Microsoft.AspNetCore.Identity;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.Auth.AdminApproval;

public sealed class ApproveUserAsStandardCommandHandler(UserManager<AppUser> userManager) 
    : IRequestHandler<ApproveUserAsStandardCommand, Result<string>>
{
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task<Result<string>> Handle(ApproveUserAsStandardCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            return Result<string>.Failure("Kullanıcı bulunamadı.");

        var currentRoles = await _userManager.GetRolesAsync(user);
        if (currentRoles.Contains("User"))
            return Result<string>.Failure("Kullanıcı zaten onaylanmış.");

      
        if (currentRoles.Any())
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

       
        var result = await _userManager.AddToRoleAsync(user, "User");
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result<string>.Failure($"User rolü atanamadı: {errors}");
        }

        return Result<string>.Succeed("Kullanıcı başarıyla onaylandı.");
    }
}


