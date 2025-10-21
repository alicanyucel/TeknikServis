using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Application.Dtos;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Users.GetByIdUser;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository
            .GetAll()
            .Where(x => x.Id == request.Id && !x.IsDeleted)
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(cancellationToken);

        if (userEntity is null)
            return Result<UserDto>.Failure("Kullanıcı bulunamadı veya silinmiş.");

        var userDto = new UserDto(
            Id: userEntity.Id,
            FirstName: userEntity.FirstName,
            LastName: userEntity.LastName,
            Email: userEntity.Email ?? string.Empty,
            Roles: userEntity.UserRoles?.Select(ur => ur.Role?.Name ?? string.Empty).ToList() ?? new List<string>(),
            UpdatedTime: userEntity.UpdatedTime ?? default,
            UpdatedBy: userEntity.UpdatedBy ?? string.Empty,
            CreatedBy: userEntity.CreatedBy ?? string.Empty,
            CratedTime: userEntity.CratedTime ?? default,
            CreateadAt: userEntity.CreateadAt ?? default,
            UpdatedAt: userEntity.UpdatedAt,
            IsDeleted: userEntity.IsDeleted
        );

        return Result<UserDto>.Succeed(userDto);
    }
}
