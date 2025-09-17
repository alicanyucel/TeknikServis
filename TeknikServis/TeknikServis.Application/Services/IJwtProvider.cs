using TeknikServis.Application.Features.Auth.Login;
using TeknikServis.Domain.Entities;

namespace TeknikServis.Application.Services
{
    public interface IJwtProvider
    {
        Task<LoginCommandResponse> CreateToken(AppUser user);
    }
}
