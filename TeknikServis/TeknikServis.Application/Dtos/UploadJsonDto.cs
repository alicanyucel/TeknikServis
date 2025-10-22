using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TeknikServis.Application.Dtos;

public class JsonUploadDto
{
    [Required]
    public required IFormFile File { get; set; }
}
