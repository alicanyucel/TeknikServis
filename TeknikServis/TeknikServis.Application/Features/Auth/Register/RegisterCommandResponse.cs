namespace TeknikServis.Application.Features.Auth.Register;

public sealed class RegisterCommandResponse
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
}
