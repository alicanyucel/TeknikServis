using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Repositories;

namespace TeknikServis.Application.Features.Roles.GetAllRoles;

internal sealed class GetRolesQueryHandler : IRequestHandler<GetAllRoleQuery, List<GetAllRolesQueryResponse>>
{
    private readonly IRoleRepository _roleRepository;

    public GetRolesQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<List<GetAllRolesQueryResponse>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
    {
        var response =
            await _roleRepository.GetAll()
            .Select(r => new GetAllRolesQueryResponse(r.Id, r.Name ?? string.Empty))
            .ToListAsync(cancellationToken);
        return response;
    }
}