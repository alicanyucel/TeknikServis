using AutoMapper;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Users.GetByIdUser;

public sealed class GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetUserByIdQuery, Result<AppUser>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<AppUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetByExpressionAsync(
            x => x.Id == request.Id,
            cancellationToken
        );
        if (userEntity is null)
        return Result<AppUser>.Failure("Kullanıcı bulunamadı.");
        var user = _mapper.Map<AppUser>(userEntity);
        return Result<AppUser>.Succeed(user);
    }
}