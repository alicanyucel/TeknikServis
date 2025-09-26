using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Users.GetAllUser;

internal sealed class GetAllCustomerQueryHandler(IUserRepository userRepository) : IRequestHandler<GetAllUserQuery, Result<List<AppUser>>>
{
    public async Task<Result<List<AppUser>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        List<AppUser> users = await userRepository.GetAll().ToListAsync(cancellationToken);
        return users.ToList();
    }
}
