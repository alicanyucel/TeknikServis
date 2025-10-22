using MediatR;
using TeknikServis.Application.Features.LocationSenders;

namespace TeknikServis.Application.Features.UploadJsonLoader;

public class UploadJsonCommandHandler : IRequestHandler<UploadJsonCommand, string>
{
    private readonly LocationSeeder _seeder;

    public UploadJsonCommandHandler(LocationSeeder seeder)
    {
        _seeder = seeder;
    }

    public async Task<string> Handle(UploadJsonCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null || request.File.Length == 0)
            throw new ArgumentException("Dosya boş.");

        var tempPath = Path.GetTempFileName();
        try
        {
            await using (var stream = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None, 81920, useAsync: true))
            {
                await request.File.CopyToAsync(stream, cancellationToken);
            } // stream disposed here

            await _seeder.SeedFromJsonAsync(tempPath, "TÜRKİYE", cancellationToken);    
            return "JSON başarıyla işlendi.";
        }
        finally
        {
            try { if (File.Exists(tempPath)) File.Delete(tempPath); } catch { /* ignore cleanup errors */ }
        }
    }
}

