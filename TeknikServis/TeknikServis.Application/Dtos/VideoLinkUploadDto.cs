using Microsoft.AspNetCore.Http;

namespace TeknikServis.Application.Dtos;

public class VideoLinkUploadDto
{
    public IFormFile File { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid ServiceActionId { get; set; }
}
