using MediatR;
using TeknikServis.Application.Dtos;
using TS.Result;

namespace TeknikServis.Application.Features.Users.GetByIdUser;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<Result<UserDto>>;
