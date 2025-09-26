using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Application.Dtos;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Users.GetAllUser;

internal sealed class GetAllUserQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetAllUserQuery, Result<List<UserDto>>>
{
    public async Task<Result<List<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var userEntities = await userRepository.GetAll().ToListAsync(cancellationToken);

        var users = userEntities.Select(user => new UserDto(
            Id: user.Id,
            FirstName: user.FirstName,
            LastName: user.LastName,
            Email: user.Email ?? string.Empty,
            IsDeleted: false
        )).ToList();

        return Result<List<UserDto>>.Succeed(users);
    }
}
