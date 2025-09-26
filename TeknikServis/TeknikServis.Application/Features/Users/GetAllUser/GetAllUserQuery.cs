using MediatR;
using TeknikServis.Application.Dtos;
using TS.Result;

namespace TeknikServis.Application.Features.Users.GetAllUser;

public sealed record GetAllUserQuery : IRequest<Result<List<UserDto>>>;
