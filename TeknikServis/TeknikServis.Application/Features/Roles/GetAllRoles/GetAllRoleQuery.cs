using MediatR;

namespace TeknikServis.Application.Features.Roles.GetAllRoles;

public sealed record GetAllRoleQuery() : IRequest<List<GetAllRolesQueryResponse>>;
