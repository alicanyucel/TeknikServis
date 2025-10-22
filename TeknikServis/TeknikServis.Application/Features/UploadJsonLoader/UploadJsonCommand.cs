using MediatR;
using Microsoft.AspNetCore.Http;

namespace TeknikServis.Application.Features.UploadJsonLoader;

public class UploadJsonCommand : IRequest<string>
{
    public IFormFile File { get; set; }

    public UploadJsonCommand(IFormFile file)
    {
        File = file;
    }
}
