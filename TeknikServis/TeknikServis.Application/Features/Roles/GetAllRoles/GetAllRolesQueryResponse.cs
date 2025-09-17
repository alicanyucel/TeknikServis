namespace TeknikServis.Application.Features.Roles.GetAllRoles;

public sealed record GetAllRolesQueryResponse(
    Guid Id,
    string Name);