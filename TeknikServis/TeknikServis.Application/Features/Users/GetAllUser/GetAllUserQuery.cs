using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.Users.GetAllUser;

public sealed record GetAllUserQuery : IRequest<Result<List<AppUser>>>;
