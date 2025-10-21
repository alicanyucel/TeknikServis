using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TeknikServis.Application.Dtos;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Users.GetAllUser;

internal sealed class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, Result<List<UserDto>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

        public async Task<Result<List<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var userEntities = await _userRepository
            .GetAll()
            .Where(user => !user.IsDeleted)
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .ToListAsync(cancellationToken);

        var users = userEntities.Select(user => new UserDto(
            Id: user.Id,
            FirstName: user.FirstName,
            LastName: user.LastName,
            Email: user.Email ?? string.Empty,
            Roles: user.UserRoles?.Select(ur => ur.Role?.Name ?? string.Empty).ToList() ?? new List<string>(),
            IsDeleted: user.IsDeleted
        )).ToList();

        return users.Count == 0
            ? Result<List<UserDto>>.Failure("Hiç aktif kullanıcı bulunamadı.")
            : Result<List<UserDto>>.Succeed(users);
    }
}
