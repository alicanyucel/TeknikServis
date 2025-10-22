using AutoMapper;
using GenericRepository;
using MediatR;
using System.Text;
using System.Text.RegularExpressions;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.VideoLinks.UpdateVideoLinks;


internal sealed class UpdateVideoLinksCommandHandler(IVideoLinkRepository videoLinkRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateVideoLinkCommand, Result<string>>
{
    private static readonly HashSet<string> AllowedVideoExtensions = new(StringComparer.OrdinalIgnoreCase)
    { ".mp4", ".mov", ".avi", ".mkv", ".webm", ".m4v" };

    public async Task<Result<string>> Handle(UpdateVideoLinkCommand request, CancellationToken cancellationToken)
    {
        VideoLink? videoLink = await videoLinkRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (videoLink == null)
        {
            return Result<string>.Failure("Video link bulunamadi.");
        }
        mapper.Map(request, videoLink);

        if (request.File is not null && request.File.Length > 0)
        {
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

            videoLink.Url = $"/videos/{uniqueFileName}";
        }
        videoLink.Description = request.Description;
        videoLink.ServiceActionId = request.ServiceActionId;
        videoLink.UpdatedTime = request.UpdatedTime;
        videoLink.UpdatedBy = request.UpdatedBy;
        videoLink.UpdatedAt = request.UpdatedAt;
        videoLink.IsDeleted = request.IsDeleted;

        videoLinkRepository.Update(videoLink);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Video Link güncellendi.";
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