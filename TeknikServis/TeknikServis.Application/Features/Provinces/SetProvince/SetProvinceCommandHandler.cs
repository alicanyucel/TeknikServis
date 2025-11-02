using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Provinces.SetProvince;

internal sealed class SetProvinceCommandHandler(IProvinceRepository provinceRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<SetProvinceCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SetProvinceCommand request, CancellationToken cancellationToken)
    {
        var existing = await provinceRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
        if (existing is null)
        {
            var entity = new Province
            {
                Id = request.Id,
                Name = request.Name,
                CountryId = request.CountryId,
                CreatedBy = "system",
                UpdatedBy = "system",
                CreateadAt = DateTime.UtcNow,
                CreatedTime = new TimeOnly(0, 0),
                UpdatedTime = new TimeOnly(0, 0)
            };
            await provinceRepository.AddAsync(entity, cancellationToken);
        }
        else
        {
            existing.Name = request.Name;
            existing.CountryId = request.CountryId;
            existing.UpdatedBy = "system";
            existing.UpdatedTime = new TimeOnly(0, 0);
            provinceRepository.Update(existing);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Province set edildi");
    }
}
