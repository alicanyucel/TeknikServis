using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TeknikServis.Application.Dtos;

public sealed class DocumentLinkUploadDto
{
    [Required]
    public IFormFile File { get; set; } = default!;

    [Required]
    public string Url { get; set; } = default!;

    public string Description { get; set; } = default!;

    [Required]
    public Guid ServiceActionId { get; set; }
}
