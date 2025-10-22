using GenericRepository;
using MediatR;
using System.Text;
using System.Text.RegularExpressions;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.VideoLinks.CrateVideoLinks;

internal sealed class CreateVideoLinkComamndHandler(IVideoLinkRepository videoLinkRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateVideoLinkCommand, Result<string>>
{
    private static readonly HashSet<string> AllowedVideoExtensions = new(StringComparer.OrdinalIgnoreCase)
    { ".mp4", ".mov", ".avi", ".mkv", ".webm", ".m4v" };

    public async Task<Result<string>> Handle(CreateVideoLinkCommand request, CancellationToken cancellationToken)
    {
        string? finalUrl = null;

        if (request.File is not null && request.File.Length > 0)
        {
            // Validate file is a video by content type or extension
            var ext = Path.GetExtension(request.File.FileName);
            var isVideoByMime = !string.IsNullOrWhiteSpace(request.File.ContentType) && request.File.ContentType.StartsWith("video/", StringComparison.OrdinalIgnoreCase);
            var isVideoByExt = !string.IsNullOrWhiteSpace(ext) && AllowedVideoExtensions.Contains(ext);
            if (!isVideoByMime && !isVideoByExt)
            {
                return Result<string>.Failure($"Only video files are allowed. Allowed extensions: {string.Join(", ", AllowedVideoExtensions)}");
            }

            var videosFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos");
            Directory.CreateDirectory(videosFolder);

            var safeFileName = MakeSafeFileName(request.File.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}_{safeFileName}";
            var filePath = Path.Combine(videosFolder, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await request.File.CopyToAsync(stream, cancellationToken);

            finalUrl = $"/videos/{uniqueFileName}";
        }
        else
        {
            return Result<string>.Failure("Video dosyası sağlanmalıdır.");
        }

        var video = new VideoLink
        {
            Url = finalUrl!,
            Description = request.Description,
            ServiceActionId = request.ServiceActionId,
            ServiceAction = null!,
            UpdatedTime = request.UpdatedTime,
            UpdatedBy = request.UpdatedBy,
            CreatedBy = request.CreatedBy,
            CreatedTime = request.CratedTime,
            CreateadAt = request.CreateadAt,
            UpdatedAt = request.UpdatedAt,
            IsDeleted = request.IsDeleted
        };

        await videoLinkRepository.AddAsync(video, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Video Link kaydı yapıldı";
    }

    private static string MakeSafeFileName(string originalName)
    {
        var name = Path.GetFileNameWithoutExtension(originalName);
        var ext = Path.GetExtension(originalName);

        var normalized = name.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var ch in normalized)
        {
            var cat = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(ch);
            if (cat != System.Globalization.UnicodeCategory.NonSpacingMark)
                sb.Append(ch);
        }
        var withoutDiacritics = sb.ToString().Normalize(NormalizationForm.FormC);
        var safe = Regex.Replace(withoutDiacritics, @"\s+", "-");
        safe = Regex.Replace(safe, @"[^A-Za-z0-9_\-\.]", "");
        var safeExt = string.IsNullOrWhiteSpace(ext) ? ".bin" : ext;
        return $"{safe}{safeExt}";
    }
}

