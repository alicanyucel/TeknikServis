using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Application.Constanst;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.Roles.CreateRole;

internal sealed class CreateRoleCommandHandler(
  RoleManager<AppRole> roleManager) : IRequestHandler<CreateRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        List<AppRole> currentRoles = await roleManager.Roles.ToListAsync(cancellationToken);

        List<AppRole> staticRoles = ConstantsRole.GetRoles();

        foreach (var role in currentRoles)
        {
            if (!staticRoles.Any(p => p.Name == role.Name))
            {
                await roleManager.DeleteAsync(role);
            }
        }

        foreach (var role in staticRoles)
        {
            if (!currentRoles.Any(p => p.Name == role.Name))
            {
                await roleManager.CreateAsync(role);
            }
        }
        return "Roller eklendi.";
    }
}
