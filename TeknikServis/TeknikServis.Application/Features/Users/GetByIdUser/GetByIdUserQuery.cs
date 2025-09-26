using MediatR;
using TeknikServis.Domain.Entities;
using TS.Result;

namespace TeknikServis.Application.Features.Users.GetByIdUser;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<Result<AppUser>>;
